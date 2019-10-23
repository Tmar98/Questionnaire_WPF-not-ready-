﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для Results_Page.xaml
    /// </summary>
    public partial class Results_Page : UserControl
    {
        private MainWindow mW = (MainWindow)Application.Current.MainWindow;
        string[] massSelects = new string[] {" "," "," "," "," " };//Массив для добавления условий в выборку людей
        bool dalee = true;//что бы нельзя было добавить строку если небыл выбран класс
        public Results_Page()
        {
            InitializeComponent();
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            mW.Win_closing = false;
            label.Content = "Выбрать всех ";
        }

        /// <summary>
        /// при нажатии на кнопку происходит расчет баллов и вывод их в таблицу
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            

            List<Answers_Data> answers = mW.Select_Answers(Create_SelectStringAnswers(massSelects));//ответы детей
            var bdREsults = mW.Select_Results(Create_SelectStringResults(massSelects));
            if (answers.Count != 0 || bdREsults.Count !=0)//если при выборке был выбран хотя бы одно прохождение теста то отбражать
            {
                List<Named_Results> gridResults = new List<Named_Results>();
                var i = 0;
                
                if (bdREsults.Count != 0)
                {

                    var resultsFIO = mW.SelectChildrensWithoutAnswers(Create_SelecyString_FIO_HasResults(massSelects));
                    i = 0;
                    while (resultsFIO.Count > i)//собираем class для отображения в таблице данных уже посмотренных
                    {
                        List<Results_Class> rs = bdREsults.Where(r => r.Id_Answer == Convert.ToInt32(resultsFIO[i][5])).ToList();//ищем результаты для задaнного id ответов на тест
                        Named_Results named_Results = new Named_Results(gridResults.Count + 1, resultsFIO[i][0].ToString(), resultsFIO[i][1].ToString(), resultsFIO[i][2].ToString(), Convert.ToDateTime(resultsFIO[i][3]), Convert.ToInt32(resultsFIO[i][4]), rs[0]);//добовляем к данным детей их результаты
                        gridResults.Add(named_Results);
                        i++;
                    }

                }
                if (answers.Count != 0)
                {
                    List<Results_Class> results = new List<Results_Class>();

                    var res1 = 0;
                    var res2 = 0;
                    var res3 = 0;
                    List<int> testNum = new List<int>();
                    foreach (var answer in answers)//считаем баллы для каждого
                    {
                        res1 = answer.Question1 + answer.Question7 + answer.Question10 + answer.Question13 + Turn_Around(answer.Question4);
                        res2 = answer.Question2 + answer.Question5 + answer.Question11 + answer.Question14 + Turn_Around(answer.Question8);
                        res3 = answer.Question3 + answer.Question12 + Turn_Around(answer.Question6) + Turn_Around(answer.Question9) + Turn_Around(answer.Question15);
                        var results_Class = Point_Results(res1, res2, res3, answer.Id, answer.Test_Number);
                        results.Add(results_Class);//передаем их в list результатов
                        testNum.Add(answer.Test_Number);
                    }


                    var allFIODate = mW.SelectChildrensWithoutAnswers(Create_SelectStringFIO(massSelects));//получаем данные детей которые проходили тест

                    mW.Insert_Results(results, testNum);


                    i = 0;
                    while (allFIODate.Count > i)//собираем class для отображения в таблице
                    {
                        List<Results_Class> rs = results.Where(r => r.Id_Answer == Convert.ToInt32(allFIODate[i][5])).ToList();//ищем результаты для задaнного id ответов на тест
                        Named_Results named_Results = new Named_Results(gridResults.Count + 1, allFIODate[i][0].ToString(), allFIODate[i][1].ToString(), allFIODate[i][2].ToString(), Convert.ToDateTime(allFIODate[i][3]), Convert.ToInt32(allFIODate[i][4]), rs[0]);//добовляем к данным детей их результаты
                        gridResults.Add(named_Results);
                        i++;
                    }
                }
                #region смотрим номера тестов которые отображать
                List<int> testNumbers = new List<int>();
                foreach (Named_Results n in gridResults)//для каждого именновоного результата записываем номера в лист
                {
                    testNumbers.Add(n.TestNumber);
                }

                testNumbers = testNumbers.OrderBy(it => it).ToList();//сортируем лист по возростанию
                var testnum = 0;
                if (testNumbers[0] == testNumbers[testNumbers.Count - 1])//если номера тестов отличаются то в отображении таблицы идет ветка default
                {
                    testnum = testNumbers[0];
                }
                else
                {
                    testnum = 0;
                }

                #endregion


                while (dataGrid.Columns.Count > 5)//если количество солбцов привышает заданные в xml то мы их удаляем
                {
                    dataGrid.Columns.RemoveAt(dataGrid.Columns.Count - 1);
                }


                switch (testnum)//для каждого теста свое количество сталбцов //testnum это номер отображения таблицы 0 - выводяться несколько разных тестов
                {
                    case 1:
                        {
                            dataGrid.AutoGenerateColumns = false;

                            DataGridTextColumn c0 = new DataGridTextColumn();
                            c0.Header = "Result1";
                            c0.Width = 110;
                            c0.Binding = new Binding("Result1");
                            dataGrid.Columns.Add(c0);

                            DataGridTextColumn c1 = new DataGridTextColumn();
                            c1.Header = "Result2";
                            c1.Width = 110;
                            c1.Binding = new Binding("Result2");
                            dataGrid.Columns.Add(c1);

                            DataGridTextColumn c2 = new DataGridTextColumn();
                            c2.Header = "Result3";
                            c2.Width = 110;
                            c2.Binding = new Binding("Result3");
                            dataGrid.Columns.Add(c2);



                            var style = new Style(typeof(TextBlock));
                            style.Setters.Add(new Setter(TextBlock.TextWrappingProperty, TextWrapping.Wrap));
                            c0.ElementStyle = style;
                            c1.ElementStyle = style;
                            c2.ElementStyle = style;

                        };
                        break;
                    default:
                        {
                            dataGrid.AutoGenerateColumns = false;

                            DataGridTextColumn c0 = new DataGridTextColumn();
                            c0.Header = "Result1";
                            c0.Width = 110;
                            c0.Binding = new Binding("FIO");
                            dataGrid.Columns.Add(c0);

                            DataGridTextColumn c1 = new DataGridTextColumn();
                            c1.Header = "Result1";
                            c1.Width = 110;
                            c1.Binding = new Binding("Result1");
                            dataGrid.Columns.Add(c1);

                            var style = new Style(typeof(TextBlock));
                            style.Setters.Add(new Setter(TextBlock.TextWrappingProperty, TextWrapping.Wrap));
                            c1.ElementStyle = style;

                            c0.ElementStyle = style;
                        }
                        break;
                }


                dataGrid.ItemsSource = gridResults;//передаем в таблицу данные детей объедененые с их ответами

                
            }
            else
            {
                dataGrid.Items.Clear();//очиска таблицы
                MessageBox.Show("По заданному условию не найдено данных");
            }
            //очищаем массив условий и текст условия
            massSelects[0] = " ";
            massSelects[1] = " ";
            massSelects[2] = " ";
            massSelects[3] = " ";
            label.Content = "Выбрать всех ";

        }

        /// <summary>
        /// обратный порядок баллов
        /// </summary>
        /// <param name="iteam">балл</param>
        /// <returns></returns>
        private int Turn_Around(int iteam)
        {
            
            if (iteam==1)
            {
                iteam = 5;
            }
            else if(iteam==2)
            {
                iteam = 4;
            }
            else if(iteam==4)
            {
                iteam = 2;
            }
            else if(iteam==5)
            {
                iteam = 1;
            }
            else
            {
                iteam = 3;
            }
            return iteam;
        }

        /// <summary>
        /// Вычисление результатов теста 1
        /// </summary>
        /// <param name="res1">Первый результат</param>
        /// <param name="res2">Второй результат</param>
        /// <param name="res3">Третий результат</param>
        /// <param name="id">id строки ответов в бд</param>
        /// <returns></returns>
        private Results_Class Point_Results(int res1,int res2,int res3,int id,int testNumber)
        {
            string[] Results_array = new string[]//массив с первой частью результата
            {
            "Осведомленность и умелость в процедурных вопросах сдачи ЕГЭ",
            "Способность к самоорганизации и самоконтролю",
            "Экзаменационная тревожность"
             };

            int[] array = new int[]
            {res1,res2,res3
            };

            for (var i = 0; i < 3; i++)//для каждого начального вычесляется кол баллов и дописывается результат
            {
                if (array[i] <6)
                {
                    Results_array[i] += " очень низкий (" + array[i].ToString() + " б)";
                }
                else if(array[i]>5 && array[i]<11)
                {
                    Results_array[i] += " низкий (" + array[i].ToString() + " б)";
                }
                else if(array[i]>10 && array[i]<16)
                {
                    Results_array[i] += " средний (" + array[i].ToString() + " б)";
                }
                else if(array[i]>15 && array[i]<21)
                {
                    Results_array[i] += " выше среднего (" + array[i].ToString() + " б)";
                }
                else if(array[i]>20 && array[i]<26)
                {
                    Results_array[i] += " высокий (" + array[i].ToString() + " б)";
                }
            }
            Results_Class results_Class = new Results_Class(id,Results_array[0], Results_array[1], Results_array[2]);
            return results_Class;
        }

        /// <summary>
        /// при выборе любого элемента combobox появляются элементы для ввода нужной информации
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _selectionType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            switch (_selectionType.SelectedIndex+1)//+1 так как элементы combobox начинаются с 0
            {
                case 1://ФИО
                    {
                        textBox.Visibility = Visibility.Visible;
                        comboBox.Visibility = Visibility.Hidden;
                        date.Visibility = Visibility.Hidden;
                    }
                    break;
                case 2://Школа
                {
                        comboBox.Visibility = Visibility.Visible;
                        textBox.Visibility = Visibility.Hidden;
                        date.Visibility = Visibility.Hidden;
                        comboBox.ItemsSource= mW.LoadSchools();
                        comboBox.DisplayMemberPath = "School_Number";
                }
                    break;
                case 3://Класс
                    {
                        textBox.Visibility = Visibility.Hidden;
                        date.Visibility = Visibility.Hidden;
                        if (massSelects[1] == " ")//смотрим была ли указана до этого школа если нет то нужно сначала выбрать ее
                        {
                            dalee = false;
                            comboBox.Visibility = Visibility.Visible;
                            label1.Visibility = Visibility.Visible;
                            label1.Content = "Выберите школу";
                            comboBox.ItemsSource = mW.LoadSchools();
                            comboBox.DisplayMemberPath = "School_Number";
                            button1.Visibility = Visibility.Visible;
                        }
                        else//если школа выбрана то на основе этой школы отображаются классы
                        {
                            comboBox.Visibility = Visibility.Visible;
                            Klasses klasses = mW.LoadKlases();
                            List<Klass> kl = klasses.Where(k => k.Id_School == Convert.ToInt32( massSelects[1].Substring(10,massSelects[1].Length-11))).ToList();//выбор класов у которых id школы соответствует выбранной школе
                            comboBox.ItemsSource = kl;
                            comboBox.DisplayMemberPath = "Klass_Name";
                        }
                    }
                    break;
                case 4://Дата
                    {
                        date.Visibility = Visibility.Visible;
                        comboBox.Visibility = Visibility.Hidden;
                        textBox.Visibility = Visibility.Hidden;
                    }
                    break;
                case 5:
                    {
                        date.Visibility = Visibility.Hidden;
                        textBox.Visibility = Visibility.Hidden;
                        comboBox.Visibility = Visibility.Visible;
                        List<string> testName = new List<string> {"1","2","3" };
                        comboBox.ItemsSource = testName;
                    }
                    break;
            }
                
        }

        /// <summary>
        /// Принажатии на кнопку добавить добовляется условие поиска в бд
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Addcondition_Click(object sender, RoutedEventArgs e)
        {
            if (dalee)//когда идет выбор школы для выбора класса из _selectionType_SelectionChanged case3 не дает ввести выбранную школу как класс
            {
                
                switch (_selectionType.SelectedIndex + 1)//+1 так как элементы combobox начинаются с 0
                {
                    case 1://ФИО
                        {
                            massSelects[_selectionType.SelectedIndex] = " and c.FIO='" + textBox.Text + "' ";//добавляем в массив готовую часть с условием
                            label.Content +="c ФИО '"+ textBox.Text + "' ";//добавляем к условию выбранное условие для понимания какое условие есть для выборки людей из бд
                            textBox.Visibility = Visibility.Hidden;
                        }
                        break;
                    case 2://Школа
                        {
                            massSelects[_selectionType.SelectedIndex] = " and s.Id=" + (comboBox.SelectedIndex+1).ToString() + " ";
                            label.Content += "из школы №"+comboBox.Text.Trim()+" ";//добавляем к условию выбранное условие для понимания какое условие есть для выборки людей из бд
                            comboBox.Visibility = Visibility.Hidden;
                        }
                        break;
                    case 3://Класс
                        {
                            massSelects[_selectionType.SelectedIndex] = " and cl.Id=" + (comboBox.SelectedIndex+1).ToString() + " ";
                            label.Content += "из "+comboBox.Text.Trim()+" класса ";//добавляем к условию выбранное условие для понимания какое условие есть для выборки людей из бд
                            comboBox.Visibility = Visibility.Hidden;
                            label1.Visibility = Visibility.Hidden;
                            button1.Visibility = Visibility.Hidden;
                        }
                        break;
                    case 4://Дата
                        {
                            massSelects[_selectionType.SelectedIndex] = "and q.Date between '" +  date.SelectedDate.ToString() + "' and '" + date.SelectedDate.ToString().Substring(0, 10)+" 23:59:59' ";
                            label.Content += "кто проходил тест "+ date.SelectedDate.ToString().Substring(0, 10)+" ";//добавляем к условию выбранное условие для понимания какое условие есть для выборки людей из бд
                            date.Visibility = Visibility.Hidden;
                        }
                        break;
                    case 5:
                        {
                            massSelects[_selectionType.SelectedIndex] = "and q.Test_Number=" + (comboBox.SelectedIndex + 1).ToString() + " ";
                            label.Content += "кто прохадил тест '" + comboBox.Text.Trim() + "' ";
                            comboBox.Visibility = Visibility.Hidden;
                        }
                        break;
                }
            }
            

            
        }

        /// <summary>
        /// Создание строки запроса к бд с введенными условиями для получения данных детей
        /// </summary>
        /// <param name="massSelects">массив в котором находятся готовые части условий</param>
        /// <returns></returns>
        private string Create_SelectStringFIO(string[] massSelects)
        {
            var selectString = "select c.FIO, s.School_Number,cl.Class_Name,q.Date,q.Test_Number,q.Id from Children c join Schools s on c.Id_School = s.Id " + massSelects[1]+
                " join Classes cl on c.Id_Class = cl.Id " + massSelects[2]+ 
                " join Questionnaire_Answers q on c.Id=q.Id_Children and q.Test_Result_Id IS Null "+ massSelects[3]+ massSelects[0] + massSelects[4];

            return selectString;
        }

        /// <summary>
        /// Создание строки запроса к бд с введенными условиями для получения ответов на тест
        /// </summary>
        /// <param name="massSelects">массив в котором находятся готовые части условий</param>
        /// <returns></returns>
        private string Create_SelectStringAnswers(string[] massSelects)
        {
            var selectString = "select * from Questionnaire_Answers q join Children c on q.Id_Children = c.Id "+ massSelects[0] + 
                " join Schools s on c.Id_School = s.Id " + massSelects[1] +
                " join Classes cl on c.Id_Class = cl.Id " + massSelects[2] +
                " where q.Test_Result_Id IS Null " + massSelects[3] + massSelects[4];

            return selectString;
        }

        /// <summary>
        /// Выборка информации о детях у которых уже есть результат
        /// </summary>
        /// <param name="massSelects">массив в котором находятся готовые части условий</param>
        /// <returns></returns>
        private string Create_SelecyString_FIO_HasResults  (string[] massSelects)
        {
            var selectString = "select c.FIO, s.School_Number,cl.Class_Name,q.Date,q.Test_Number,q.Id from Children c join Schools s on c.Id_School = s.Id " + massSelects[1] +
                " join Classes cl on c.Id_Class = cl.Id " + massSelects[2] +
                " join Questionnaire_Answers q on c.Id=q.Id_Children and q.Test_Result_Id IS NOT Null " + massSelects[3] + massSelects[0] + massSelects[4];

            return selectString;
        }

        /// <summary>
        /// Выборка результатов
        /// </summary>
        /// <param name="massSelects">массив в котором находятся готовые части условий</param>
        /// <returns></returns>
        private string Create_SelectStringResults(string[] massSelects)
        {
            var selectString = "select r.Id,r.Id_Answer,r.Result1,r.Result2,r.Result3,r.Result4,r.Result5 from Results_Table r " +
                " join Questionnaire_Answers q on r.Id_Answer = q.Id " + massSelects[3] + massSelects[4] +
                " join Children c on q.Id_Children = c.Id " + massSelects[0] +
                " join Schools s on c.Id_School = s.Id " + massSelects[1] +
                " join Classes cl on c.Id_Class = cl.Id " + massSelects[2] +
                " where q.Test_Result_Id IS NOT Null ";

            return selectString;
        }

        /// <summary>
        /// кнопка для выбора класса после уточнения в какой школе искать класс
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            button1.Visibility = Visibility.Hidden;
            label1.Content = "Выберите класс";
            Klasses klasses = mW.LoadKlases();
            List<Klass> kl = klasses.Where(k => k.Id_School == comboBox.SelectedIndex + 1).ToList();//выбор класов у которых id школы соответствует выбранной школе
            comboBox.ItemsSource = kl;
            comboBox.DisplayMemberPath = "Klass_Name";
            dalee = true;//открывает доступ к добавлению условия на класс 
        }

        
    }
}                      