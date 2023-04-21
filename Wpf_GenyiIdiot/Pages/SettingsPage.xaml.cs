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

using Wpf_GenyiIdiot.Model;
using Wpf_GenyiIdiot.Service;
using Wpf_GenyiIdiot.Storage;

namespace Wpf_GenyiIdiot.Pages
{
    public partial class SettingsPage : Window
    {
        int defaultInGameQuestionsCount = Properties.Settings.Default.inGameQuestionsCount;
        int defaultInGameTimePerQuestion = Properties.Settings.Default.timePerQuestion;

        public SettingsPage()
        {
            InitializeComponent();
            OnStartLoadQuestionsList();
            OnLoadSettings();
        }

        private void OnStartLoadQuestionsList()
        {
            List<Question> questions = QuestionService.GetQuestions();
            //List<string> questionTextForTextBox = questions.Select(x => x.Text).ToList();
            //questionsListBox.ItemsSource = questionTextForTextBox;
            questionsListBox.ItemsSource = questions;
            var menu = new ContextMenu();
            questionsListBox.ContextMenu = menu;
            
            var editInMenu = new MenuItem();
            editInMenu.Header = "Редактировать";
            editInMenu.Click += EditQuestionButton_Click;

            var deleteInMenu = new MenuItem();
            deleteInMenu.Header = "Удалить";
            deleteInMenu.Click += DeleteQuestionButton_Click;

            var addInMenu = new MenuItem();
            addInMenu.Header = "Добавить вопрос";
            addInMenu.Click += AddQuestionButton_Click;

            menu.Items.Add(editInMenu);
            menu.Items.Add(deleteInMenu);
            menu.Items.Add(addInMenu);
        }

        private void OnLoadSettings()
        {
            inGameQuestionsCountTextBox.Text = Properties.Settings.Default.questionCountSetByUser.ToString();
            timeForQuestionTextBox.Text = Properties.Settings.Default.timeSetByUser.ToString();
        }

        private void EditQuestionButton_Click(object sender, RoutedEventArgs e)
        {
            List<Question> questions = QuestionService.GetQuestions();
            Question questionToEdit = questions.Where(q => q.Text == questionsListBox.SelectedItem.ToString()).First();
            QuestionEdit questionEditPage = new QuestionEdit(questionToEdit);
            questionEditPage.ShowDialog();
            OnStartLoadQuestionsList();
        }

        private void AddQuestionButton_Click(object sender, RoutedEventArgs e)
        {
            QuestionEdit questionEditPage = new QuestionEdit();
            questionEditPage.ShowDialog();
            OnStartLoadQuestionsList();
        }

        private void DeleteQuestionButton_Click(object sender, RoutedEventArgs e)
        {
            List<Question> questions = QuestionService.GetQuestions();
            Question questionToDelete = questions.Where(q => q.Text == questionsListBox.SelectedItem.ToString()).First();
            if (QuestionService.DeleteQuestion(questionToDelete))
            {
                MessageBox.Show("Удаление завершено", "Вопрос успешно удален!");
            }
            else
            {
                MessageBox.Show("Во время удаления вопроса произошла ошибка!", "Ошибка!");
            }
            OnStartLoadQuestionsList();
        }

        private void BackToMainMenuButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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
