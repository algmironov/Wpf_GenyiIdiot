using System.Windows;
using System.Windows.Input;

using Wpf_GenyiIdiot.Model;
using Wpf_GenyiIdiot.Service;
using Wpf_GenyiIdiot.ViewModel;

namespace Wpf_GenyiIdiot.Pages
{
    /// <summary>
    /// Логика взаимодействия для QuestionEdit.xaml
    /// </summary>
    public partial class QuestionEdit : Window
    {
        Question question = new Question();
        private readonly QuestionsViewModel viewModel;
        public QuestionEdit(Question question, QuestionsViewModel vm)
        {
            InitializeComponent();
            viewModel = vm;
            this.question = question;
            OnLoadToEdit();
            this.question = question;
            Title = "Редактирование вопроса";
            Closed += QuestionEdit_Closed;
        }

        private void QuestionEdit_Closed(object? sender, EventArgs e)
        {
            viewModel.UpdateQuestionsCommand.Execute(viewModel.Questions);
        }

        public QuestionEdit(QuestionsViewModel vm)
        {
            InitializeComponent();
            viewModel = vm;
            Title = "Добавление вопроса";
            OnLoadToCreate();
            Closed += QuestionEdit_Closed;
        }

        private void OnLoadToEdit()
        {
            questionTextPlace.Text = question.Text;
            answerTextPlace.Text = question.Answer.ToString();
        }

        private void OnLoadToCreate()
        {
            saveDataButton.Content = "Сохранить вопрос";
            questionTextPlace.Text = string.Empty;
            questionTextPlace.Focus();
            answerTextPlace.Text = string.Empty;
        }

        private void OnCloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SaveDataButton_Click(object sender, RoutedEventArgs e)
        {
            var editedQuestionText = questionTextPlace.Text;
            var editedAnswer = answerTextPlace.Text;
            int newAnswer;
            try
            {
                newAnswer = int.Parse(editedAnswer);
            }
            catch (ArgumentException)
            {
                MessageBox.Show("Ответ должен быть числом!", "Ошибка!");
                return;
            }

            if (question.Text != null)
            {
                if (editedQuestionText != question.Text || newAnswer != question.Answer)
                {
                    var editedQuestion = new Question(editedQuestionText, newAnswer);
                    QuestionService.EditQuestion(question, editedQuestion);
                    MessageBox.Show("Вопрос успешно отредактирован!", "Успех!", MessageBoxButton.OK);
                    Close();
                }
            }
            else
            {
                question.Answer = newAnswer;
                question.Text = editedQuestionText;
                QuestionService.AddQuestion(question);
                MessageBox.Show("Вопрос успешно Сохранен!", "Успех!", MessageBoxButton.OK);
                Close();
            }
            
            Close();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SaveDataButton_Click(sender, e);
            }
            if (e.Key == Key.Escape)
            {
                Close();
            }
        }
    }
}
