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
using System.Windows.Shapes;

namespace Wpf_GenyiIdiot.Pages
{
    /// <summary>
    /// Логика взаимодействия для EnterNamePage.xaml
    /// </summary>
    public partial class EnterNamePage : Window
    {
        
        public EnterNamePage()
        {
            InitializeComponent();
        }

        void OnSaveButtonClick(object sender, RoutedEventArgs e)
        {
            if ( !string.IsNullOrWhiteSpace(inputTextBox.Text) || !string.IsNullOrEmpty(inputTextBox.Text) ) 
            {
                GamePage.Username = inputTextBox.Text;
            }
            this.Close();
        }

        void OnDontSaveButtonClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}