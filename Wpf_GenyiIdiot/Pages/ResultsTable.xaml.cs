using System.Windows;
using System.Windows.Controls;

using Wpf_GenyiIdiot.Model;
using Wpf_GenyiIdiot.Service;

namespace Wpf_GenyiIdiot.Pages
{
    /// <summary>
    /// Логика взаимодействия для ResultsTable.xaml
    /// </summary>
    public partial class ResultsTable : Window
    {
        public ResultsTable()
        {
            InitializeComponent();
            OnStartResultsLoad();
        }

        private void OnStartResultsLoad()
        {
            resultsDataGrid.HeadersVisibility = DataGridHeadersVisibility.Column;
            resultsDataGrid.ItemsSource = ResultService.GetResults();
            resultsDataGrid.IsReadOnly = true;

            var menu = new ContextMenu();
            resultsDataGrid.ContextMenu = menu;

            var deleteInMenu = new MenuItem();
            deleteInMenu.Header = "Удалить";
            deleteInMenu.Click += DeleteResultButton_Click;

            menu.Items.Add(deleteInMenu);
        }

        private void OnExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OnClearTable_Click(object sender, RoutedEventArgs e)
        {
            ResultService.ClearResults();
            OnStartResultsLoad();
        }

        private void DeleteResultButton_Click(object sender, RoutedEventArgs e)
        {
            ResultService.RemoveResult((Result)resultsDataGrid.SelectedItem);
            OnStartResultsLoad();
        }



    }
}
