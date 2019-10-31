using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using System.Data.SqlClient;
using System.Data;

namespace Questionnaire
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public bool Win_closing = false;// Пере менная отвечающая за запрещение или разрешение закрытия окна
        private SqlConnection connection;
        const bool useMARS = false;
        private int id_Person;//переменная для хранения id если этот человек уже зарегестрированн в системе в этом учебном году
        public MainWindow()
        {
            InitializeComponent();
            ConnectionAsunc();
        }

        /// <summary>
        /// Подключение к базе данных
        /// </summary>
        private async void ConnectionAsunc()
        {
            try
            {
                var path = @"Data Source = DESKTOP-JH3BOL4\SQLEXPRESS;Initial Catalog=Questionare_DB;Integrated Security=True";
                connection = new SqlConnection(path + (useMARS ? ";MultipleActiveResultSets=True" : String.Empty));

                await connection.OpenAsync();
            }
            catch
            {

            }

        }


        /// <summary>
        /// Считывание школ из бд
        /// </summary>
        /// <returns></returns>
        public Schools LoadSchools()
        {
            var commandread_School = new SqlCommand("select * from Schools", connection);//строка выбора из бд
            SqlDataReader reader_School=null;
            try
            {
                reader_School = commandread_School.ExecuteReader();
            }
            catch(SqlException e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                
            }
            
            Schools schools = new Schools(reader_School);
            reader_School.Close();

            return schools;
        }

        /// <summary>
        /// Считывание классов из бд
        /// </summary>
        /// <returns></returns>
        public Klasses LoadKlases()
        {
            var commandread_Class = new SqlCommand("select * from Classes", connection);//строка выбора из бд
            var reader_Class = commandread_Class.ExecuteReader();
            Klasses klasses = new Klasses(reader_Class);
            reader_Class.Close();

            return klasses;
        }


        #region Считывание вопросов тестов из бд

        /// <summary>
        /// Считование вопросов первого теста Бека из бд
        /// </summary>
        /// <returns></returns>
        public Queue<string> Test1_Questions()
        {
            var commandread = new SqlCommand("select Question from Bek_Scale", connection);//строка выборки из бд
            var reader = commandread.ExecuteReader();
            Queue<string> queue_Questions = new Queue<string>();//создаю очередь из строк
            while (reader.Read())
            {
                queue_Questions.Enqueue(reader["Question"].ToString());//в очередь записываю вопросы теста
            }
            reader.Close();
            
            return queue_Questions;
        }

        /// <summary>
        /// Считование вопросов четвертого теста ЕГЭ из бд
        /// </summary>
        /// <returns></returns>
        public Queue<string> Test4_Questions()
        {
            var commandread = new SqlCommand("select Question_Text from EGE_Questions", connection);//строка выборки из бд
            var reader = commandread.ExecuteReader();
            Queue<string> queue_Questions = new Queue<string>();//создаю очередь из строк
            while (reader.Read())
            {
                queue_Questions.Enqueue(reader["Question_Text"].ToString());//в очередь записываю вопросы теста
            }
            reader.Close();

            return queue_Questions;
        }

        #endregion


        #region Проверка на наличие это го человека в базе уже и запись нового человека
        /// <summary>
        /// Запись нового человека в бд
        /// </summary>
        /// <param name="fio">ФИО человека</param>
        /// <param name="id_school">Id выбранной школы</param>
        /// <param name="id_class">Id выбранного класса</param>
        public void Insert_Person(string fio,int id_school,int id_class )
        {
            #region Проверяем есть ли такой человек в базе уже
            var command_Id = new SqlCommand("select Id from Children where FIO = @fio and Id_School = @id_School and Id_Class = @id_Class",connection);//строка выборки из бд
            command_Id.Parameters.AddWithValue("@fio", fio);//присваиваю переменной строки подключения значение fio
            command_Id.Parameters.AddWithValue("@id_School", id_school);//присваиваю переменной строки подключения значение id школы
            command_Id.Parameters.AddWithValue("@id_Class", id_class);//присваиваю переменной строки подключения значение id класса
            var reader_IdSecond = command_Id.ExecuteReader();
            #endregion

            if (reader_IdSecond.Read() )//Проверяем есть ли такой человек в базе уже
            {
                id_Person = Convert.ToInt32(reader_IdSecond["Id"]);//Если есть то его ID записываем в переменную
                reader_IdSecond.Close();
            }
            else
            {
                #region если такого человека в базе нет то записываем его как нового

                reader_IdSecond.Close();
                try
                {
                    var insert_Command = new SqlCommand("insert into [Children]([FIO],[Id_School],[Id_Class]) values(@fio,@id_School,@id_Class)", connection);//строка запроса к бд
                    insert_Command.Parameters.AddWithValue("@fio", fio);//присваиваю переменной строки подключения значение fio
                    insert_Command.Parameters.AddWithValue("@id_School", id_school);//присваиваю переменной строки подключения значение id школы
                    insert_Command.Parameters.AddWithValue("@id_Class", id_class);//присваиваю переменной строки подключения значение id класса
                    insert_Command.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                    MessageBox.Show(e.Message);
                }
                //дальше считываем id нового человека
                command_Id = new SqlCommand("select Id from Children where FIO = @fio and Id_School = @id_School and Id_Class = @id_Class", connection);
                command_Id.Parameters.AddWithValue("@fio",fio );//присваиваю переменной строки подключения значение fio
                command_Id.Parameters.AddWithValue("@id_School", id_school);//присваиваю переменной строки подключения значение id школы
                command_Id.Parameters.AddWithValue("@id_Class", id_class);//присваиваю переменной строки подключения значение id класса
                reader_IdSecond = command_Id.ExecuteReader();
                
                try
                {
                    reader_IdSecond.Read();
                    id_Person = Convert.ToInt32(reader_IdSecond["Id"]);//ID записываем в переменную
                    reader_IdSecond.Close();
                }
                catch (SqlException e)
                {
                    MessageBox.Show(e.Message);
                }
                #endregion
            }
        }
        #endregion


        #region Запись результатов теста в базу
        /// <summary>
        /// Запись результатов теста в базу
        /// </summary>
        /// <param name="results">Список ответов на тест</param>
        /// <param name="test_Number">Номер теста</param>
        public void Insert_Answers(List<int> results,int test_Number)
        {
            try
            {

                #region Создание строки для записи данных в бд

                DateTime date = DateTime.Now;

                string stroka_Insert = "insert into [Questionnaire_Answers]([Id_Children],[Test_Number],[Date]";//Строка для записи результатов в бд
                var i = 0;

                while (i < results.Count)//добовляем похожие столбцы циклом
                {
                    stroka_Insert += ",[Question" + (i + 1).ToString() + "]";//собираю похожие названия столбцов
                    i++;
                }

                stroka_Insert += ") values(@id_Children,@test_Number,@date";//добовляем уникальные переменные для значений
                i = 0;

                while (i < results.Count)//добовляем похожие переменные для значений ответов теста
                {
                    stroka_Insert += ",@question" + (i + 1).ToString();//собираю похожие названия столбцов
                    i++;
                }
                stroka_Insert += ")";//закрываем строку
                #endregion

                #region Задаю комманду для SQL запроса и присваиваю переменным данные

                var insert_CommandAnswers = new SqlCommand(stroka_Insert, connection);//строка запроса к бд
                insert_CommandAnswers.Parameters.AddWithValue("@id_Children", id_Person);
                insert_CommandAnswers.Parameters.AddWithValue("@test_Number", test_Number);
                insert_CommandAnswers.Parameters.AddWithValue("@date", date);

                for (i = 0; i < results.Count; i++)//Каждой переменной ответа теста присваиваю значения
                {
                    var stroka = "@question" + (i + 1).ToString();//собираю похожие

                    insert_CommandAnswers.Parameters.AddWithValue(stroka, results[i]);
                }
                
                insert_CommandAnswers.ExecuteNonQuery();
                #endregion
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.ToString());
            }
 
        }
        #endregion


        /// <summary>
        /// Считываем ответы на вопросы из бд
        /// </summary>
        /// <param name="selectString">строка выбора из бд</param>
        /// <returns></returns>
        public List<Answers_Data> Select_Answers(string selectString)
        {
            var command_Answers = new SqlCommand(selectString, connection);
            var reader_Answers = command_Answers.ExecuteReader();//получаю данные из бд

            List<Answers_Data> Answers = new List<Answers_Data>();//list элементом которого служит класс для хранения ответов

            Answers_Data answers_Data = new Answers_Data(); //класс для хранения ответов

            while (reader_Answers.Read())//пока мы можем читать данные мы будим их добовлять в класс
            {
                switch (Convert.ToInt32(reader_Answers[2]))//для каждого теста отличается количество ответов//будет дополненно еще 2 тестами
                {

                    case 1://Тест Бека
                        {
                            answers_Data = new Answers_Data(Convert.ToInt32(reader_Answers[0]), Convert.ToInt32(reader_Answers[1]), Convert.ToInt32(reader_Answers[2]), Convert.ToDateTime(reader_Answers[3]), Convert.ToInt32(reader_Answers[5]), Convert.ToInt32(reader_Answers[6]), Convert.ToInt32(reader_Answers[7]), Convert.ToInt32(reader_Answers[8]), Convert.ToInt32(reader_Answers[9]), Convert.ToInt32(reader_Answers[10]), Convert.ToInt32(reader_Answers[11]), Convert.ToInt32(reader_Answers[12]), Convert.ToInt32(reader_Answers[13]), Convert.ToInt32(reader_Answers[14]), Convert.ToInt32(reader_Answers[15]), Convert.ToInt32(reader_Answers[16]), Convert.ToInt32(reader_Answers[17]), Convert.ToInt32(reader_Answers[18]), Convert.ToInt32(reader_Answers[19]), Convert.ToInt32(reader_Answers[20]), Convert.ToInt32(reader_Answers[21]), Convert.ToInt32(reader_Answers[22]), Convert.ToInt32(reader_Answers[23]), Convert.ToInt32(reader_Answers[24]));
                            Answers.Add(answers_Data);
                        }
                        break;
                    case 4://Тест Еге
                        {
                            answers_Data = new Answers_Data(Convert.ToInt32(reader_Answers[0]), Convert.ToInt32(reader_Answers[1]), Convert.ToInt32(reader_Answers[2]), Convert.ToDateTime(reader_Answers[3]), Convert.ToInt32(reader_Answers[5]), Convert.ToInt32(reader_Answers[6]), Convert.ToInt32(reader_Answers[7]), Convert.ToInt32(reader_Answers[8]), Convert.ToInt32(reader_Answers[9]), Convert.ToInt32(reader_Answers[10]), Convert.ToInt32(reader_Answers[11]), Convert.ToInt32(reader_Answers[12]), Convert.ToInt32(reader_Answers[13]), Convert.ToInt32(reader_Answers[14]), Convert.ToInt32(reader_Answers[15]), Convert.ToInt32(reader_Answers[16]), Convert.ToInt32(reader_Answers[17]), Convert.ToInt32(reader_Answers[18]), Convert.ToInt32(reader_Answers[19]));
                            Answers.Add(answers_Data);
                        }
                        break;
                    
                }
            }
            
            reader_Answers.Close();//закрываем поток

            return Answers;
        }


        /// <summary>
        /// Считываем ответы на данные детей без ответов из бд
        /// </summary>
        /// <param name="selectString">строка выбора из бд</param>
        /// <returns></returns>
        public List<FioDate> SelectChildrensWithoutAnswers(string selectString)
        {

            var command_Data = new SqlCommand(selectString, connection);
            var reader = command_Data.ExecuteReader();//считываю данные из бд (данные о ребенке (фио, школа, класс,дата прохождения теста,id ответов в бд))

            List<FioDate> allFioDate = new List<FioDate>();//list элементами которого является класс для хранения информации о ребенке и даты прохождения теста
            while (reader.Read())//читаем данные
            {
                FioDate fioDate = new FioDate(reader[0].ToString().Trim(), reader[1].ToString().Trim(), reader[2].ToString().Trim(), Convert.ToDateTime(reader[3]), Convert.ToInt32(reader[4]), Convert.ToInt32(reader[5]));//собираем класс

                allFioDate.Add(fioDate);//добавляем класс в list
            }
            reader.Close();//закрываем считывание
            return allFioDate;
        }


        /// <summary>
        /// Считываем готовые результаты из бд для тех у кого уже есть результаты
        /// </summary>
        /// <param name="selectString"> строка выборки из бд</param>
        /// <returns></returns>
        public List<Results_Class> Select_Results(string selectString)
        {
            var command_Results = new SqlCommand(selectString, connection);
            var reader_Results = command_Results.ExecuteReader();//считываем данные из бд
            List<Results_Class> listResults = new List<Results_Class>();//list элементами которого является класс с результатами
            while(reader_Results.Read())//читаем данные
            {
                Results_Class results_Class = new Results_Class(Convert.ToInt32( reader_Results[0]),Convert.ToInt32( reader_Results[1]), reader_Results[2].ToString(), reader_Results[3].ToString(), reader_Results[4].ToString(), reader_Results[5].ToString(), reader_Results[6].ToString());//собираем класс
                listResults.Add(results_Class);//добавляем класс в list
            }
            reader_Results.Close();
            return listResults;
        }


        /// <summary>
        /// Записываем в бд обработанные результаты
        /// </summary>
        /// <param name="insert_String"> строка inserta в бд</param>
        /// <param name="results_Classes"> list класса с результатами для функции обновления данных от том что результаты обработанны</param>
        public  void Insert_Results(string insert_String,List<Results_Class> results_Classes)//запись результатов в бд
        {
            
            var insert_CommandResultsEge = new SqlCommand(insert_String, connection);//строка запроса к бд
            insert_CommandResultsEge.ExecuteNonQuery();//записываем результаты в бд

            Update_Info(results_Classes);
        }


        /// <summary>
        /// функция обновления данных от том что результаты обработанны
        /// </summary>
        /// <param name="results_Classes">list с результатами</param>
        private void Update_Info(List<Results_Class> results_Classes)  
        {
            var selectString = "select Id_Answer,Id from Results_Table Where";//строка выбора из бд
            foreach (Results_Class r in results_Classes)    //выбираем все id для каждого результата(бд сама их выдает)
            {
                selectString += " Id_Answer=" + r.Id_Answer + " or ";
            }
            selectString = selectString.Substring(0, selectString.Length - 3);//обрезаем строку выбора на (or_) для правильного выбора

            var command_ResId = new SqlCommand(selectString, connection);//считываем ID который получил только что записанный результат
            var reader_Id = command_ResId.ExecuteReader();

                foreach(Results_Class r in results_Classes)//изменяем информацию в классе
                {
                    if(reader_Id.Read())
                        if (r.Id_Answer == Convert.ToInt32(reader_Id[0]))
                            r.Id_Result = Convert.ToInt32(reader_Id[1]);//дописываем id строки результата из бд
                }


            reader_Id.Close();


            var update_String = " UPDATE Questionnaire_Answers SET Test_Result_Id = CASE Id ";//строка обновления информации
            foreach (Results_Class t in results_Classes)//собираем строку обновления
            {
                update_String+=" WHEN "+t.Id_Answer+" THEN "+t.Id_Result+" ";
            }
            update_String += " ELSE Test_Result_Id END";
            var update_Command = new SqlCommand(update_String, connection);
            update_Command.ExecuteNonQuery();//обновляем информацию о том что для данных ответов на тесты был получен результат
        }


        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = Win_closing;//Проверка на возможность закрытия основной формы(меняется в других окнах)
            
            connection.Close(); //Закрытие подключения к базе
        }


    }
}

public class FioDate //класс для хранения информациии о ребенке
{
    public string FIO {get;set;}
    public string SchoolNumber { get; set; }
    public string Klass_Name { get; set; }
    public DateTime Date { get; set; }
    public int TestNumber { get; set; }
    public int AnswerId { get; set; }


    public FioDate()
    { }

    public FioDate(string fio,string schoolNumber,string klassName,DateTime date,int testNumber,int answerId)
    {
        FIO = fio;SchoolNumber = schoolNumber;Klass_Name = klassName;Date = date;TestNumber = testNumber;AnswerId = answerId;
    }
}