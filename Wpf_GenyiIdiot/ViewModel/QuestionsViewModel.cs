using System.Windows;
using System.Windows.Input;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Wpf_GenyiIdiot.Model;
using Wpf_GenyiIdiot.Pages;
using Wpf_GenyiIdiot.Service;

namespace Wpf_GenyiIdiot.ViewModel
{
    public partial class QuestionsViewModel : ObservableObject
    {
        ObservableCollection<Question> _questions;
        public ICommand UpdateQuestionsCommand { get; }
        private Question selectedQuestion;
        public ObservableCollection<Question> Questions
        {
            get { return _questions; }
            set
            {
                _questions = value;
                OnPropertyChanged(nameof(Questions));
            }
        }

        public Question SelectedQuestion
        {
            get
            {
                return selectedQuestion;
            }

            set
            {
                selectedQuestion = value;
                OnPropertyChanged(nameof(selectedQuestion));
            }
        }

        public QuestionsViewModel()
        {
            Questions = new ObservableCollection<Question>(QuestionService.GetQuestions());
            UpdateQuestionsCommand = new RelayCommand(UpdateQuestionsList);
        }

        private RelayCommand addQuestionCommand;
        private RelayCommand deleteQuestionCommand;
        private RelayCommand editQuestionCommand;

        public IRelayCommand DeleteQuestionCommand => deleteQuestionCommand ??= new RelayCommand(RemoveQuestion);
        public IRelayCommand AddQuestionCommand => addQuestionCommand ??= new RelayCommand(AddNewQuestion);
        public IRelayCommand EditQuestionCommand => editQuestionCommand ??= new RelayCommand(EditSelectedQuestion);

        [RelayCommand]
        private void AddNewQuestion()
        {
            QuestionEdit editQuestion = new QuestionEdit(new QuestionsViewModel());
            SetAndOpenWindow(editQuestion);
            UpdateQuestionsList();
            OnPropertyChanged(nameof(Questions));
        }

        [RelayCommand]
        private void EditSelectedQuestion()
        {
            if (selectedQuestion != null)
            {
                QuestionEdit editQuestion = new QuestionEdit(selectedQuestion, new QuestionsViewModel());
                SetAndOpenWindow(editQuestion);
                UpdateQuestionsList();
                OnPropertyChanged(nameof(Questions));
            }
        }

        [RelayCommand]
        private void RemoveQuestion()
        {
            if (selectedQuestion != null)
            {
                QuestionService.DeleteQuestion(selectedQuestion);
                Questions.Remove(selectedQuestion);
            }
        }
       
        private void UpdateQuestionsList()
        {
            var newQuestions = QuestionService.GetQuestions();
            Questions.Clear();
            foreach (Question question in newQuestions)
            {
                Questions.Add(question);
            }
        }

        private void SetAndOpenWindow(Window window)
        {
            window.Owner = Application.Current.MainWindow;
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window.ShowDialog();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
