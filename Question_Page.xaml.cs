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

namespace Questionnaire
{
    /// <summary>
    /// Логика взаимодействия для Question_Page.xaml
    /// </summary>
    public partial class Question_Page : UserControl
    {
        public Question_Page()
        {
            InitializeComponent();
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            MainWindow mW = (MainWindow)Application.Current.MainWindow;
            var tuple =mW.Test1_Questions();
            var reader = tuple.Item1;
            var test_Number = tuple.Item2;
            reader.Read();
            reader.Read();
            question_Lable.Style = this.Resources["lab2"] as Style;
            QuestionArea.Text = reader["Question_Text"].ToString();
            question_Lable.Style = this.Resources["lab3"] as Style;
            reader.Close();


        }
    }
}
