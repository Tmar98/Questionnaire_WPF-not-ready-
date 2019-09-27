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
        private int test_Number;
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
            Connection();
            
            
        }


        private async void Connection()
        {
            
            var path = @"Data Source = DESKTOP-JH3BOL4\SQLEXPRESS;Initial Catalog=Questionare_DB;Integrated Security=True";
            connection = new SqlConnection(path + (useMARS ? ";MultipleActiveResultSets=True" : String.Empty));
            
                await connection.OpenAsync();

          
        }
        
        public DataTable  Login_Data_Load()
        {
            var commandread_School = new SqlCommand("select * from Schools", connection);
            var table = new DataTable();
            using (var reader_School = commandread_School.ExecuteReader())
            {
                
                table.Columns.Add("Id");
                table.Columns.Add("School_Number");
                while (reader_School.Read())
                {
                    var row = table.NewRow();

                    row["Id"] = Int32.Parse(reader_School["ID"].ToString());
                    row["School_Number"] = reader_School["School_Number"].ToString();
                    table.Rows.Add(row);
                }

            }

            



            return table;

            
        }
        public SqlDataReader Log()
        {
            var commandread_Class = new SqlCommand("select * from Classes", connection);
            var reader_Class = commandread_Class.ExecuteReader();
            return reader_Class;
        }


        //public Tuple<Schools,SqlDataReader> Login_Data_Load()
        //{
        //    var commandread_School = new SqlCommand("select * from Schools", connection);
        //    var reader_School = commandread_School.ExecuteReader();
        //    Schools schools = new Schools(reader_School);
        //    var commandread_Class = new SqlCommand("select * from Classes", connection);
        //    var reader_Class = commandread_Class.ExecuteReader();

        //    var tuple = new Tuple<Schools, SqlDataReader>(schools, reader_Class);
        //    return tuple;
        //}

        public Tuple< SqlDataReader,int> Test1_Questions()
        {
            var commandread = new SqlCommand("select Question_Text from EGE_Questions", connection);
            var reader = commandread.ExecuteReader();
            var tuple = new Tuple<SqlDataReader, int>(reader, test_Number);
            return tuple;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            
            
            
            
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = Win_closing;
            
            connection.Close(); 
        }

        
    }
}
