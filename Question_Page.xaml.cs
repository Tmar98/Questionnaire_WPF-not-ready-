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
        private List<int> results = new List<int>();
        private MainWindow mW = (MainWindow)Application.Current.MainWindow;
        private int test_Number;
        public Question_Page()
        {
            InitializeComponent();
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
           
            mW.Win_closing = true;
            var tuple =mW.Test1_Questions();
            queue_Questions = tuple.Item1;
            test_Number = tuple.Item2;

            var question =queue_Questions.Dequeue();
            QuestionArea.Text = question;
            
            if (test_Number==1)
            {
                button1.Visibility = Visibility.Visible;
                textBlock1.Text = "Согласен";
                button2.Visibility = Visibility.Visible;
                textBlock2.Text = "Скорее согласен, чем не согласен";
                button3.Visibility = Visibility.Visible;
                textBlock3.Text = "Затрудняюсь ответить";
                button4.Visibility = Visibility.Visible;
                textBlock4.Text = "Скорее не согласен, чем согласен";
                button5.Visibility = Visibility.Visible;
                textBlock5.Text = "Полностью не согласен";
            }
            
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            results.Add(1);
            if (queue_Questions.Count !=0 )
            {
                
                var question = queue_Questions.Dequeue();
                QuestionArea.Text = question;
            }
            else

                End_Test();
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            results.Add(2);
            if (queue_Questions.Count != 0)
            {
                
                var question = queue_Questions.Dequeue();
                QuestionArea.Text = question;

            }
            else

                End_Test();
        }

        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            results.Add(3);
            if (queue_Questions.Count != 0)
            {
                
                var question = queue_Questions.Dequeue();
                QuestionArea.Text = question;

            }
            else

                End_Test();
        }

        private void Button4_Click(object sender, RoutedEventArgs e)
        {
            results.Add(4);
            if (queue_Questions.Count != 0)
            {
                
                var question = queue_Questions.Dequeue();
                QuestionArea.Text = question;

            }
            else

                End_Test(); 
        }

        private void Button5_Click(object sender, RoutedEventArgs e)
        {
            results.Add(5);
            if (queue_Questions.Count != 0)
            {

                var question = queue_Questions.Dequeue();
                QuestionArea.Text = question;

            }
            else
                End_Test();


        }

        private void End_Test()
        {
            mW.Insert_Results(results, test_Number);
            mW.question_Page.Visibility = Visibility.Hidden;
            MessageBox.Show("Тест пройден");
            mW.Butt_Menu.Visibility = Visibility.Visible;
        }
    }
}
