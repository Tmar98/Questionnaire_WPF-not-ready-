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
        private ObservableCollection<School> observSchool;
        private ObservableCollection<Klass> observKlas;
        

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
        /// добавление в combobox _schoolBox элемент classa school из ObservableCollection
        ///       
        /// mW главное окно
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindow mW = (MainWindow)Application.Current.MainWindow;

            Schools schools = mW.Login_Data_Load().Item1;
            //observSchool = new ObservableCollection<School>(schools);
            _schoolBox.ItemsSource = schools;//observSchool;
        }


        private void _schoolBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MainWindow mW = (MainWindow)Application.Current.MainWindow;
            var index =_schoolBox.SelectedIndex + 1;
            Klasses klasses = mW.Login_Data_Load().Item2;
            List<Klass> kl = klasses.Where(k => k.Id_School == index).ToList();
            observKlas = new ObservableCollection<Klass>(kl);
            _classBox.ItemsSource = observKlas;
        }

        private void _entrenceButt_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mW = (MainWindow)Application.Current.MainWindow;
            mW.Butt_Menu.Visibility = Visibility.Hidden;
            mW.question_Page.Visibility = Visibility.Visible;
            this.Close();
        }
    }
}
