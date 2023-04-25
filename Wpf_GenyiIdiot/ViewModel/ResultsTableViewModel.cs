using System.Windows.Input;
using Wpf_GenyiIdiot.Model;
using Wpf_GenyiIdiot.Service;
using CommunityToolkit.Mvvm.Input;

namespace Wpf_GenyiIdiot.ViewModel
{
    public partial class ResultsTableViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Result> _results;

        public ObservableCollection<Result> Results
        {
            get { return _results; }
            set
            {
                _results = value;
                OnPropertyChanged(nameof(Results));
            }
        }

        public ICommand ClearTableCommand { get; set; }
        public ICommand DeleteResultCommand { get; set; }

        public ResultsTableViewModel()
        {
            Results = new ObservableCollection<Result>(ResultService.GetResults());

            ClearTableCommand = new RelayCommand(ClearTable);
            DeleteResultCommand = new RelayCommand<Result?>(RemoveResult);
        }

        private void ClearTable()
        {
            ResultService.ClearResults();
            Results.Clear();
        }

        [RelayCommand]
        private void RemoveResult(Result? result)
        {
            if (result != null) 
            {
                ResultService.RemoveResult(result);
                Results.Remove(result);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
