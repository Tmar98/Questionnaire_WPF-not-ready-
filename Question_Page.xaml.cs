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
        private Queue<string> queue_Questions;//очередь с вопросами
        private List<int> results = new List<int>();//list ответов
        private MainWindow mW = (MainWindow)Application.Current.MainWindow;//главное окно
        private int test_Number;//номер теста
        private bool visible = true;//видимо было ли окно
        public Question_Page()
        {
            InitializeComponent();
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (visible)//если окно было невидемо то true
            {
                results.Clear();//очищаем list с ответами
                mW.Win_closing = true;//заприщаем закрывать окно

                switch (test_Number)//в зависемости от выбранного теста считываются вопросы
                {
                    case 1://Тест Бека
                        {
                            queue_Questions = mW.Test1_Questions();
                        }
                        break;
                    case 4://Тест Егэ
                        {
                            queue_Questions = mW.Test4_Questions();
                        }
                        break;
                }


                QuestionArea.Text = queue_Questions.Dequeue();//в поле с вопросом записываем первый элемент в очереди


                switch (test_Number)//в зависимости от теста происходит разное отображение
                {
                    case 1://Тест Бека
                        {
                            button1.Visibility = Visibility.Visible;
                            textBlock1.Text = "Верно";

                            button5.Visibility = Visibility.Visible;
                            textBlock5.Text = "Неверно";
                        }
                        break;
                    case 4://Тест Егэ
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
                        break;
                }
                visible = false;//задаем то что сейчас окно видимо и при скрытии usercontrol мы не проходили все снова
            }
            else
            {
                visible = true;//заходим при скрытии usercontrol
            }
            
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            results.Add(5);//при нажатии на кнопку в list ответов добавляется ответ
            if (queue_Questions.Count !=0 )//проверка есть ли в очереди еще вопросы
            {
                
                QuestionArea.Text = queue_Questions.Dequeue();//ввыводим следующий в очереди вопрос
            }
            else

                End_Test();//заканчиваем прохождение теста
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            results.Add(4);//при нажатии на кнопку в list ответов добавляется ответ
            if (queue_Questions.Count != 0)//проверка есть ли в очереди еще вопросы
            {
                
                QuestionArea.Text = queue_Questions.Dequeue();//ввыводим следующий в очереди вопрос

            }
            else

                End_Test();//заканчиваем прохождение теста
        }

        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            results.Add(3);//при нажатии на кнопку в list ответов добавляется ответ
            if (queue_Questions.Count != 0)//проверка есть ли в очереди еще вопросы
            {
                
                QuestionArea.Text = queue_Questions.Dequeue();//ввыводим следующий в очереди вопрос

            }
            else

                End_Test();//заканчиваем прохождение теста
        }

        private void Button4_Click(object sender, RoutedEventArgs e)
        {
            results.Add(2);//при нажатии на кнопку в list ответов добавляется ответ
            if (queue_Questions.Count != 0)//проверка есть ли в очереди еще вопросы
            {
                
                QuestionArea.Text = queue_Questions.Dequeue();//ввыводим следующий в очереди вопрос

            }
            else

                End_Test(); //заканчиваем прохождение теста
        }

        private void Button5_Click(object sender, RoutedEventArgs e)
        {
            results.Add(1);//при нажатии на кнопку в list ответов добавляется ответ
            if (queue_Questions.Count != 0)//проверка есть ли в очереди еще вопросы
            {

                QuestionArea.Text = queue_Questions.Dequeue();//ввыводим следующий в очереди вопрос

            }
            else
                End_Test();//заканчиваем прохождение теста


        }

        /// <summary>
        /// заканчиваем прохождение теста
        /// </summary>
        private void End_Test()
        {
                                            //Скрываем все кнопки
            button1.Visibility = Visibility.Hidden;
            button2.Visibility = Visibility.Hidden;
            button3.Visibility = Visibility.Hidden;
            button4.Visibility = Visibility.Hidden;
            button5.Visibility = Visibility.Hidden;

            mW.Insert_Answers(results, test_Number);//Записываем полученные результаты в бд
            mW.question_Page.Visibility = Visibility.Hidden;//скрытии usercontrol
            MessageBox.Show("Тест пройден");//оповещаем о том что тест пройден
            mW.Butt_Menu.Visibility = Visibility.Visible;//показываем основное меню с выбором теста
        }

        /// <summary>
        /// Функция по определению какой тест был выбран пользователем
        /// </summary>
        /// <param name="test"> номер теста</param>
        public void AddNomer_Testa(int test)
        {
            test_Number = test;//в глобальную переменную записываем номер теста
        }

    }
}
