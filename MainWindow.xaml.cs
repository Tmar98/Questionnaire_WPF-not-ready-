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
        private int test_Number = 0;
        private int id_Person;//переменная для хранения id если этот человек уже зарегестрированн в системе в этом учебном году
        public MainWindow()
        {
            InitializeComponent();
            Connection();
        }

        /// <summary>
        /// Подключение к базе данных
        /// </summary>
        private async void Connection()
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
        /// Считывание школ и класов из бд
        /// </summary>
        /// <returns></returns>
        public Tuple<Schools, Klasses> Login_Data_Load()
        {
            var commandread_School = new SqlCommand("select * from Schools", connection);
            var reader_School = commandread_School.ExecuteReader();
            Schools schools = new Schools(reader_School);
            reader_School.Close();
            var commandread_Class = new SqlCommand("select * from Classes", connection);
            var reader_Class = commandread_Class.ExecuteReader();
            Klasses klasses = new Klasses(reader_Class);
            reader_Class.Close();

            var tuple = new Tuple<Schools, Klasses>(schools, klasses);


            return tuple;//переменная хранящая list<школ>,list<класов>
        }


        #region Считывание вопросов тестов из бд
        /// <summary>
        /// Считование вопросов первого теста из бд
        /// </summary>
        /// <returns></returns>
        public Tuple<Queue<string>, int> Test1_Questions()
        {
            test_Number = 1;
            var commandread = new SqlCommand("select Question_Text from EGE_Questions", connection);
            var reader = commandread.ExecuteReader();
            var list_Questions = new List<string>();
            Queue<string> queue_Questions = new Queue<string>();
            while (reader.Read())
            {
                queue_Questions.Enqueue(reader["Question_Text"].ToString());
            }
            reader.Close();
            var tuple = new Tuple<Queue<string>, int>(queue_Questions, test_Number);
            return tuple;
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
            #region Запрашиваем Id человека с введенными параметрами
            var command_Id = new SqlCommand("select Id from Children where FIO = @fio and Id_School = @id_School and Id_Class = @id_Class",connection);
            command_Id.Parameters.AddWithValue("@fio", fio);
            command_Id.Parameters.AddWithValue("@id_School", id_school);
            command_Id.Parameters.AddWithValue("@id_Class", id_class);
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
                    var insert_Command = new SqlCommand("insert into [Children]([FIO],[Id_School],[Id_Class]) values(@fio,@id_School,@id_Class)", connection);
                    insert_Command.Parameters.AddWithValue("@fio", fio);
                    insert_Command.Parameters.AddWithValue("@id_School", id_school);
                    insert_Command.Parameters.AddWithValue("@id_Class", id_class);
                    insert_Command.ExecuteNonQuery();
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
        public void Insert_Results(List<int> results,int test_Number)
        {
            try
            {
                if (id_Person == 0)//Проверка зашел новый человек => из базы достать максимальный ID
                                   //Или зашел человек который уже был в базе и его ID уже в программе
                {
                    #region Выбираю максимальный ID => новый
                    var commandread_Id = new SqlCommand("select MAX(Children.Id) as Id from Children", connection);
                    var reader_Id = commandread_Id.ExecuteReader();
                    reader_Id.Read();
                    id_Person = Convert.ToInt32(reader_Id["Id"]);//записываю этот ID в переменную 
                    reader_Id.Close();
                    #endregion
                }

                #region Создание строки для записи данных в бд
                DateTime date = DateTime.Now;

                string stroka_Insert = "insert into [Questionnaire_Answers]([Id_Children],[Test_Number],[Date]";//Строка для записи результатов в бд
                var i = 0;

                while (i < results.Count)//добовляем похожие столбцы циклом
                {
                    stroka_Insert += ",[Question" + (i + 1).ToString() + "]";//собираю похожие
                    i++;
                }

                stroka_Insert += ") values(@id_Children,@test_Number,@date";//добовляем уникальные переменные для значений
                i = 0;

                while (i < results.Count)//добовляем похожие переменные для значений ответов теста
                {
                    stroka_Insert += ",@question" + (i + 1).ToString();//собираю похожие
                    i++;
                }
                stroka_Insert += ")";//закрываем строку
                #endregion

                #region Задаю комманду для SQL запроса и присваиваю переменным данные
                var insert_CommandAnswers = new SqlCommand(stroka_Insert, connection);
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

        public List<Answers_Data> Select_Answers()
        {
            var command_Answers = new SqlCommand("select * from Questionnaire_Answers where Id_Children>@id_Children", connection);
            command_Answers.Parameters.AddWithValue("@id_Children", 23);
            var reader_Answers = command_Answers.ExecuteReader();

            List<Answers_Data> Answers = new List<Answers_Data>();

            Answers_Data answers_Data = new Answers_Data();

            reader_Answers.Read();
            answers_Data = new Answers_Data(Convert.ToInt32(reader_Answers[0]), Convert.ToInt32(reader_Answers[1]), Convert.ToInt32(reader_Answers[2]),Convert.ToDateTime(reader_Answers[3]), Convert.ToInt32(reader_Answers[5]), Convert.ToInt32(reader_Answers[6]), Convert.ToInt32(reader_Answers[7]), Convert.ToInt32(reader_Answers[8]), Convert.ToInt32(reader_Answers[9]), Convert.ToInt32(reader_Answers[10]), Convert.ToInt32(reader_Answers[11]), Convert.ToInt32(reader_Answers[12]), Convert.ToInt32(reader_Answers[13]), Convert.ToInt32(reader_Answers[14]), Convert.ToInt32(reader_Answers[15]), Convert.ToInt32(reader_Answers[16]), Convert.ToInt32(reader_Answers[17]), Convert.ToInt32(reader_Answers[18]), Convert.ToInt32(reader_Answers[19]));
            Answers.Add(answers_Data);

            reader_Answers.Read();
            answers_Data = new Answers_Data(Convert.ToInt32(reader_Answers[0]), Convert.ToInt32(reader_Answers[1]), Convert.ToInt32(reader_Answers[2]), Convert.ToDateTime(reader_Answers[3]), Convert.ToInt32(reader_Answers[5]), Convert.ToInt32(reader_Answers[6]), Convert.ToInt32(reader_Answers[7]), Convert.ToInt32(reader_Answers[8]), Convert.ToInt32(reader_Answers[9]), Convert.ToInt32(reader_Answers[10]), Convert.ToInt32(reader_Answers[11]), Convert.ToInt32(reader_Answers[12]), Convert.ToInt32(reader_Answers[13]), Convert.ToInt32(reader_Answers[14]), Convert.ToInt32(reader_Answers[15]), Convert.ToInt32(reader_Answers[16]), Convert.ToInt32(reader_Answers[17]), Convert.ToInt32(reader_Answers[18]), Convert.ToInt32(reader_Answers[19]));
            reader_Answers.Close();
            Answers.Add(answers_Data);

            return Answers;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = Win_closing;//Проверка на возможность закрытия основной формы(меняется в других окнах)
            
            connection.Close(); //Закрытие подключения к базе
        }

        
    }
}
