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
        public Schools schools;
        private SqlConnection connection;
        const bool useMARS = false;
        private int test_Number = 0;
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


        #region
        ///
        /// регион считывания вопросов тестов из бд
        /// 


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


        public void Insert_Person(string fio,int id_school,int id_class )
        {
            try
            {
                var insert_Command = new SqlCommand("insert into [Children]([FIO],[Id_School],[Id_Class]) values(@fio,@id_School,@id_Class)", connection);
                insert_Command.Parameters.AddWithValue("@fio",fio);
                insert_Command.Parameters.AddWithValue("@id_School", id_school);
                insert_Command.Parameters.AddWithValue("@id_Class", id_class);
                insert_Command.ExecuteNonQuery();
            }
            catch(SqlException e)
            {
                MessageBox.Show(e.Message);
            }
        }
        

        public void Insert_Results(List<int> results,int test_Number)
        {
            var commandread_Id = new SqlCommand("select MAX(Children.Id) as Id from Children", connection);
            var reader_Id = commandread_Id.ExecuteReader();
            reader_Id.Read();
            #region
            //пока не доделанно

            //
            //MessageBox.Show(reader_Id["Id"].ToString());
            DateTime date = DateTime.Now;
            MessageBox.Show(date.ToString());
            var insert_CommandAnswers =new SqlCommand( "insert into [Questionnaire_Answers]([Id_Children],[Test_Number],[Date],[Question1],[Question2],[Question3],[Question4],[Question5],[Question6],[Question7],[Question8],[Question9],[Question10],[Question11],[Question12],[Question13],[Question14],[Question15],[Question16],[Question17],[Question18],[Question19],[Question20],[Question21],[Question22],[Question23],[Question24],[Question25],[Question26],[Question27],[Question28],[Question29],[Question30],[Question31],[Question32],[Question33],[Question34],[Question35],[Question36],[Question37],[Question38],[Question39],[Question40])" +
                "values(@id_Children,@test_Number,@date,@question1,@question2,@question3,@question4,@question5,@question6,@question7,@question8,@question9,@question10,@question11,@question12,@question13,@question14,@question15,@question16,@question17,@question18,@question19,@question20,@question21,@question22,@question23,@question24,@question25,@question26,@question27,@question28,@question29,@question30,@question31,@question32,@question33,@question34,@question35,@question36,@question37,@question38,@question39,@question40)",connection);

            insert_CommandAnswers.Parameters.AddWithValue("@id_Children", Convert.ToInt32( reader_Id["Id"]));
            insert_CommandAnswers.Parameters.AddWithValue("@test_Number", test_Number);
            insert_CommandAnswers.Parameters.AddWithValue("@date", date);
            for(var i=0;i<40;i++)
            {
                var stroka = "@question" + (i+1).ToString();
                if (i < results.Count)
                {
                    insert_CommandAnswers.Parameters.AddWithValue(stroka, results[i]);
                }
                else
                {
                    insert_CommandAnswers.Parameters.AddWithValue(stroka, null);
                }
            }
            #endregion
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = Win_closing;
            
            connection.Close(); 
        }

        
    }
}
