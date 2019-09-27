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
        //public ConectionDB ConectionDB;
            
        public Login_page()
        {
            InitializeComponent();

            //ConectionDB.playdb();
        }


        private void _input_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.updatePlaceholderVisibility();
        }

        private void updatePlaceholderVisibility()
        {
            bool textEmpty = string.IsNullOrEmpty(this._input.Text);
            bool focused = Keyboard.FocusedElement == this._input;

            if (textEmpty && !focused)
                this._placehoder.Visibility = Visibility.Visible;
            else
                this._placehoder.Visibility = Visibility.Collapsed;
        }

        private void _entrenceButt_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mW = (MainWindow)Application.Current.MainWindow;
            mW.Butt_Menu.Visibility = Visibility.Hidden;
            mW.question_Page.Visibility = Visibility.Visible;
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindow mW = (MainWindow)Application.Current.MainWindow;

            var table = mW.Login_Data_Load();

            _schoolBox.DataContext = table.DefaultView;

            var klasses = new Klasses(mW.Log());
            
            foreach(Klass kl in klasses)
            {
                
                _classBox.Items.Add(kl.Klass_Name);
            }
            
        }

        private void _schoolBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var index =_schoolBox.SelectedIndex + 1;
           // _classBox.Items.Add();
        }
    }
}
