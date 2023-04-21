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

namespace Wpf_GenyiIdiot.Pages
{
    /// <summary>
    /// Логика взаимодействия для QuestionEdit.xaml
    /// </summary>
    public partial class QuestionEdit : Window
    {
        Question question = new Question();
        public QuestionEdit(Question question)
        {
            InitializeComponent();
            this.question = question;
            OnLoadToEdit();
            this.question = question;
            Title = "Редактирование вопроса";
        }

        public QuestionEdit()
        {
            InitializeComponent();
            Title = "Добавление вопроса";
        }

        private void OnLoadToEdit()
        {
            questionTextPlace.Text = question.Text;
            answerTextPlace.Text = question.Answer.ToString();
        }

        private void OnLoadToCreate()
        {
            questionTextPlace.Text = string.Empty;
            answerTextPlace.Text = string.Empty;
        }

        private void OnCloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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
                    this.Close();
                }
            }
            else
            {
                question.Answer = newAnswer;
                question.Text = editedQuestionText;
                QuestionService.AddQuestion(question);
                MessageBox.Show("Вопрос успешно Сохранен!", "Успех!", MessageBoxButton.OK);
            }
            

        }
    }
}
