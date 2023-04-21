﻿using System.Windows;

using Wpf_GenyiIdiot.Pages;

namespace Wpf_GenyiIdiot
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnSettingsButton_Click(object sender, RoutedEventArgs e)
        {
            SettingsPage settingsPage = new SettingsPage();
            Hide();
            settingsPage.ShowDialog();
            Show();
        }

        private void OnExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void OnResultsTableButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OnPlayGameButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void playGameButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}