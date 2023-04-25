using System.Windows;
using System.Windows.Controls;

using Wpf_GenyiIdiot.Model;
using Wpf_GenyiIdiot.Service;
using Wpf_GenyiIdiot.ViewModel;

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
            DataContext = new ResultsTableViewModel();
        }

        private void OnExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void DeleteSelectedRow(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                var res = dataGrid.SelectedItem;
                ((ObservableCollection<Result>)dataGrid.ItemsSource).Remove((Result)res);
                ResultService.RemoveResult((Result)res);
            }
        }
    }
}
