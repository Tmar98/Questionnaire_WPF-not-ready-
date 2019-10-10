using System;
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
        MainWindow mW = (MainWindow)Application.Current.MainWindow;
        public Results_Page()
        {
            InitializeComponent();
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

            //dataGrid.AutoGenerateColumns = false;
            //DataGridTextColumn c1 = new DataGridTextColumn();
            //c1.Header = "Id";
            //c1.Binding = new Binding("Id");
            //c1.Width = 110;
            //dataGrid.Columns.Add(c1);
            //DataGridTextColumn c2 = new DataGridTextColumn();
            //c2.Header = "Result1";
            //c2.Width = 110;
            //c2.Binding = new Binding("Result1");
            //dataGrid.Columns.Add(c2);

            
            mW.Win_closing = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<Answers_Data> answers = mW.Select_Answers();
            List<Results_Class> results = new List<Results_Class>();
            
            var res1 = 0;
            var res2 = 0;
            var res3 = 0;

            foreach (var answer in answers)
            {
                res1 = answer.Question1 + answer.Question7 + answer.Question10 + answer.Question13+Turn_Around(answer.Question4);
                res2 = answer.Question2 + answer.Question5 + answer.Question11 + answer.Question14 + Turn_Around(answer.Question8);
                res3 = answer.Question3 + answer.Question12 + Turn_Around(answer.Question6) + Turn_Around(answer.Question9) + Turn_Around(answer.Question15);
                var results_Class = Point_Results( res1, res2, res3, answer.Id);
                results.Add(results_Class);
            }




            dataGrid.ItemsSource = results;
        }

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

        private Results_Class Point_Results(int res1,int res2,int res3,int id)
        {
            string[] Results_array = new string[]
            {
            "Осведомленность и умелость в процедурных вопросах сдачи ЕГЭ",
            "Способность к самоорганизации и самоконтролю",
            "Экзаменационная тревожность"
             };

            int[] array = new int[]
            {res1,res2,res3
            };

            for (var i = 0; i < 3; i++)
            {
                if (array[i] <6)
                {
                    Results_array[i] += " очень низкий (" + array[i].ToString() + "б)";
                }
                else if(array[i]>5 && array[i]<11)
                {
                    Results_array[i] += " низкий (" + array[i].ToString() + "б)";
                }
                else if(array[i]>10 && array[i]<16)
                {
                    Results_array[i] += " средний (" + array[i].ToString() + "б)";
                }
                else if(array[i]>15 && array[i]<21)
                {
                    Results_array[i] += " выше среднего (" + array[i].ToString() + "б)";
                }
                else if(array[i]>20 && array[i]<26)
                {
                    Results_array[i] += " высокий (" + array[i].ToString() + "б)";
                }
            }
            Results_Class results_Class = new Results_Class(id,Results_array[0], Results_array[1], Results_array[2]);
            return results_Class;
        }
    }
}
