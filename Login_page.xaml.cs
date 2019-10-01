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
using System.Windows.Shapes;

namespace Questionnaire
{
    /// <summary>
    /// Логика взаимодействия для Login_page.xaml
    /// </summary>
    public partial class Login_page : Window
    {
        public School school;
        public Schools schools;
        public Klasses klasses;
        public Klass klass;
        public bool exit = false;

        public Login_page()
        {
            InitializeComponent();
            
        }

        /// <summary>
        /// Текст подсказка
        /// </summary>
        private void _input_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.updatePlaceholderVisibility();
        }


        /// <summary>
        /// Текст подсказка
        /// </summary>
        private void updatePlaceholderVisibility() 
        {
            bool textEmpty = string.IsNullOrEmpty(this._input.Text);
            bool focused = Keyboard.FocusedElement == this._input;

            if (textEmpty)
                this._placehoder.Visibility = Visibility.Visible;
            else
                this._placehoder.Visibility = Visibility.Collapsed;
        }


        /// <summary>
        /// добавление в combobox _schoolBox элементы classa school из переменной classa schols 
        ///       
        /// mW главное окно
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindow mW = (MainWindow)Application.Current.MainWindow;

            Schools schools = mW.Login_Data_Load().Item1;
            _schoolBox.ItemsSource = schools;
        }

        /// <summary>
        /// при выборе школы появляются классы которые соответствуют этой школе
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _schoolBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MainWindow mW = (MainWindow)Application.Current.MainWindow;
            var index =_schoolBox.SelectedIndex + 1;//индекс выбранной школы
            Klasses klasses = mW.Login_Data_Load().Item2;
            List<Klass> kl = klasses.Where(k => k.Id_School == index).ToList();//выбор класов у которых id школы соответствует выбранной школе
            _classBox.ItemsSource = kl;
        }

        /// <summary>
        /// при нажатии на кнопку показывается страничка с вопросами а окно с входом закрывается
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _entrenceButt_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mW = (MainWindow)Application.Current.MainWindow;
            
            if (string.IsNullOrEmpty(_input.Text))//происходит проверка введены ли все данные и вставляет их в базу данных
            {
                mW.Win_closing = true;
                MessageBox.Show("Введите имя", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (string.IsNullOrEmpty(_schoolBox.Text))
            {
                mW.Win_closing = true;
                MessageBox.Show("Выберите школу", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (string.IsNullOrEmpty(_classBox.Text))
            {
                mW.Win_closing = true;
                MessageBox.Show("Выберите класс", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if(String_check(_input.Text))
            {
                    
                mW.Insert_Person(String_Helper(_input.Text), _schoolBox.SelectedIndex + 1, _classBox.SelectedIndex + 1);//передает введенные данные в команду для вставки в бд
                mW.question_Page.Visibility = Visibility.Visible;//показывается страница с вопросами
                this.Close();//это окно закрывается
            }
                
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MainWindow mW = (MainWindow)Application.Current.MainWindow;
            
                mW.Win_closing = false;
            
        }

        /// <summary>
        /// проверка на количество введеной информации в ФИО
        /// </summary>
        /// <param name="text">введенные фамилия имя и отчество</param>
        /// <returns></returns>
        private bool String_check(string text)
        {
            bool fio = false;
            string[] str = text.Split(' ');//массив из [фамилия][имя]][отчество]
            if (str.Length < 5 && str.Length > 2)
            {
                fio = true;
            }
            else
            {
                MessageBox.Show("Введите ФИО в формате (Фамилия Имя Отчество)или(Фамилия Фамилия2 Имя Отчество)","Внимание",MessageBoxButton.OK,MessageBoxImage.Warning);
            }

            return fio;
        }


        /// <summary>
        /// состовляет укороченую версию ФИО и возводит первые символы в верхний регистр
        /// </summary>
        /// <param name="text">введенные фамилия имя и отчество</param>
        /// <returns></returns>
        private string String_Helper(string text)
        {
            string fio = null;
            string[] str = text.Split(' ');//массив из [фамилия][имя]][отчество]
            for (int i = 0; i < str.Length; i++)//цикл на возведение первой буквы каждого слова в верхний регистр
            {
                str[i] = Char.ToUpper(str[i][0]).ToString() + str[i].Substring(1);
            }


            if (str.Length == 3)//проверка на количество фамилий
            {
                fio = str[0] + " " + str[1][0] + "." + str[2][0] + ".";//одна фамилия
            }
            else if (str.Length == 4)
            {
                fio = str[0] + " " + str[1] + " " + str[2][0] + "." + str[3][0] + ".";//двойная фамилия
            }

            return fio;
        }
    }
}
