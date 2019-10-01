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
        private Queue<string> queue_Questions;
        public Question_Page()
        {
            InitializeComponent();
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            MainWindow mW = (MainWindow)Application.Current.MainWindow;
            mW.Win_closing = true;
            var tuple =mW.Test1_Questions();
            queue_Questions = tuple.Item1;
            var test_Number = tuple.Item2;

            var question =queue_Questions.Dequeue();
            QuestionArea.Text = question;
            if (test_Number==1)
            {
                button1.Visibility = Visibility.Visible;
                label1.Content = "Согласен";
                button2.Visibility = Visibility.Visible;
                button3.Visibility = Visibility.Visible;
                button4.Visibility = Visibility.Visible;
                button5.Visibility = Visibility.Visible;
            }

        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
