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

namespace Questionnaire
{
    /// <summary>
    /// Логика взаимодействия для Buttons_Menu.xaml
    /// </summary>
    public partial class Buttons_Menu : UserControl
    {
        
        public Buttons_Menu()
        {
            InitializeComponent();
        }

        private void Butt_Test4_Click(object sender, RoutedEventArgs e)
        {
            
            Login_page login_Page = new Login_page();
            login_Page.Owner = Application.Current.MainWindow;
            login_Page.Show();
            
        }
    }
}