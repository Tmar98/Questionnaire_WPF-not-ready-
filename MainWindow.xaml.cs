﻿using System;
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

        /// <summary>
        /// Считование вопросов из бд
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
        
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = Win_closing;
            
            connection.Close(); 
        }

        
    }
}
