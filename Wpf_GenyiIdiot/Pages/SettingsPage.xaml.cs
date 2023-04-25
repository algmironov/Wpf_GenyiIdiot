using System.Windows;
using System.Windows.Controls;

using Wpf_GenyiIdiot.Model;
using Wpf_GenyiIdiot.Service;
using Wpf_GenyiIdiot.ViewModel;

namespace Wpf_GenyiIdiot.Pages
{
    public partial class SettingsPage : Window
    {
        int defaultInGameQuestionsCount = Properties.Settings.Default.inGameQuestionsCount;
        int defaultInGameTimePerQuestion = Properties.Settings.Default.timePerQuestion;

        public SettingsPage()
        {
            InitializeComponent();
            DataContext = new QuestionsViewModel();
            OnLoadSettings();
        }
        private void OnLoadSettings()
        {
            inGameQuestionsCountTextBox.Text = Properties.Settings.Default.questionCountSetByUser.ToString();
            timeForQuestionTextBox.Text = Properties.Settings.Default.timeSetByUser.ToString();
        }

        private void BackToMainMenuButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SaveChangesButton_Click(object sender, RoutedEventArgs e)
        {
            var maxAvailableQuestions = QuestionService.GetQuestionsCount();
            var minQuestionsCountPerGame = 10;
            var maxTimePerQuestion = 60;
            var minTimePerQuestion = 5;

            try
            {
                Properties.Settings.Default.questionCountSetByUser = int.Parse(inGameQuestionsCountTextBox.Text);
            }
            catch (ArgumentException) 
            {
                MessageBox.Show("Значение должно быть числом!", "Ошибка!");
            }

            try
            {
                Properties.Settings.Default.timeSetByUser = int.Parse(timeForQuestionTextBox.Text);
            }
            catch (ArgumentException)
            {
                MessageBox.Show("Значение должно быть числом!", "Ошибка!");
            }

            if ((Properties.Settings.Default.questionCountSetByUser > maxAvailableQuestions) || (Properties.Settings.Default.questionCountSetByUser < minQuestionsCountPerGame))
            {
                MessageBox.Show($"Значение должно быть в диапазоне от {minQuestionsCountPerGame} до {maxAvailableQuestions}", "Ошибка!");
            }

            if ((Properties.Settings.Default.timeSetByUser > maxTimePerQuestion) ||(Properties.Settings.Default.timeSetByUser < minTimePerQuestion))
            {
                MessageBox.Show($"Значение должно быть в диапазоне от {minTimePerQuestion} до {maxTimePerQuestion}", "Ошибка!");
            }
        }

        private void ResetToDefaultsButton_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.questionCountSetByUser = defaultInGameQuestionsCount;
            Properties.Settings.Default.timeSetByUser = defaultInGameTimePerQuestion;

            OnLoadSettings();
        }
    }
}
