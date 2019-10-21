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
            var reader_School = commandread_School.ExecuteReader();
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
        /// Считование вопросов первого теста из бд
        /// </summary>
        /// <returns></returns>
        public Queue<string> Test1_Questions()
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
            var reader_Answers = command_Answers.ExecuteReader();

            List<Answers_Data> Answers = new List<Answers_Data>();

            Answers_Data answers_Data = new Answers_Data();

            while(reader_Answers.Read())
            {
                answers_Data = new Answers_Data(Convert.ToInt32(reader_Answers[0]), Convert.ToInt32(reader_Answers[1]), Convert.ToInt32(reader_Answers[2]), Convert.ToDateTime(reader_Answers[3]), Convert.ToInt32(reader_Answers[5]), Convert.ToInt32(reader_Answers[6]), Convert.ToInt32(reader_Answers[7]), Convert.ToInt32(reader_Answers[8]), Convert.ToInt32(reader_Answers[9]), Convert.ToInt32(reader_Answers[10]), Convert.ToInt32(reader_Answers[11]), Convert.ToInt32(reader_Answers[12]), Convert.ToInt32(reader_Answers[13]), Convert.ToInt32(reader_Answers[14]), Convert.ToInt32(reader_Answers[15]), Convert.ToInt32(reader_Answers[16]), Convert.ToInt32(reader_Answers[17]), Convert.ToInt32(reader_Answers[18]), Convert.ToInt32(reader_Answers[19]));
                Answers.Add(answers_Data);
            }
            
            reader_Answers.Close();

            return Answers;
        }

        /// <summary>
        /// Считываем ответы на данные детей без ответов из бд
        /// </summary>
        /// <param name="selectString">строка выбора из бд</param>
        /// <returns></returns>
        public SqlDataReader SelectChildrensWithoutAnswers(string selectString)
        {

            var command_Data = new SqlCommand(selectString, connection);
            var reader = command_Data.ExecuteReader();
            return reader;
        }


        public  void Insert_Results(List<Results_Class> results_Classes)
        {
            var insert_String = "INSERT INTO Results_Table (Id_Answer, Result1,Result2,Result3) VALUES ";// (@id_Answer,@result1,@result2,@result3)";
            foreach (Results_Class t in results_Classes)
            {
                switch (t.TestNumber)
                {
                    case 1:
                        {
                            insert_String += " ( " + t.Id_Answer.ToString() + ", '" + t.Result1.ToString() + "' ,'" + t.Result2.ToString() + "' ,'" + t.Result3.ToString() + "'),";
                        }
                        break;

                }

            }

            insert_String= insert_String.Substring(0,insert_String.Length - 1);
            var insert_CommandAnswers = new SqlCommand(insert_String, connection);//строка запроса к бд
            insert_CommandAnswers.ExecuteNonQuery();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = Win_closing;//Проверка на возможность закрытия основной формы(меняется в других окнах)
            
            connection.Close(); //Закрытие подключения к базе
        }


    }
}
