using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// <summary>
    /// Логика взаимодействия для GamePage.xaml
    /// </summary>
    public partial class GamePage : Window
    {
        public static string Username { get; set; } = string.Empty;
        int questionsAmountToAsk = Properties.Settings.Default.questionCountSetByUser;
        int timeForOneQuestion = Properties.Settings.Default.timeSetByUser;
        List<Question> questionList = new List<Question>();
        List<Question> questionsToAsk = new List<Question>();
        Question question = new ();
        Random random = new ();
        int questionNumber = 0;
        int askedQuestionsCount = 0;
        Result result = new();

        public GamePage()
        {
            InitializeComponent();
            ProgressBarInit();
            GenerateQuestionList();
            UpdatePage();

        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            int answer = 0;
            var accepted = false;
            try
            {
                answer = int.Parse(answerTextBox.Text);
                accepted = true;
            }
            catch (ArgumentException)
            {
              
            }
            catch (FormatException)
            {
                
            }
            finally
            {
                result.QuestionsAsked++;
                askedQuestionsCount++;
            }

            
            if (accepted)
            {
                if (question.Answer == answer)
                {
                    result.CorrectAnswersCount++;
                }
            }
            

            if (askedQuestionsCount != questionsAmountToAsk)
            {
                progressBar.Value = 0;
                UpdatePage();
            }
            else
            {
                var diagnosis = DiagnosisStorage.GetDiagnosisByResult(result.CorrectAnswersCount, result.QuestionsAsked);
                result.Diagnosis = diagnosis;
                MessageBoxResult res = MessageBox.Show($"Вы ответили на {result.CorrectAnswersCount} вопросов из {result.QuestionsAsked}.\nВаш диагноз: {diagnosis}", "Игра окончена!", MessageBoxButton.OKCancel);
                if (res == MessageBoxResult.OK)
                {
                    EnterNamePage enterNamePage = new EnterNamePage();
                    enterNamePage.Owner = this;
                    enterNamePage.ShowDialog();
                    result.Name = Username;
                    ResultService.SaveResult(result);
                    this.Close();
                }
            }
        }

        private void ProgressBarInit()
        {
            progressBar.Minimum = 0;
            progressBar.Maximum = 100;
            //int initialValue = 100 / questionNumber;
            //progressBar.Value += initialValue;
            
        }


        void GenerateQuestionList()
        {
            questionList = QuestionService.GetQuestions(questionsAmountToAsk);
            var tempList = new List<Question>();
            tempList.AddRange(questionList);
            for (int i = 0; i < questionList.Count; i++)
            {
                var index = random.Next(0, tempList.Count);
                questionsToAsk.Add(tempList.ElementAt(index));
                tempList.RemoveAt(index); ;
            }
        }

        Question GetNextQuestion()
        {
            var index = random.Next(0, questionsToAsk.Count);
            question = questionsToAsk[index];
            questionsToAsk.RemoveAt(index);
            return question;
        }

        void UpdatePage()
        {
            GetNextQuestion();

            questionNumber++;
            currentQuestionNumber.Content = questionNumber.ToString();

            paragraphText.Inlines.Clear();
            paragraphText.Inlines.Add(question.Text);
            //questionTextLabel.Content = question.Text;
            answerTextBox.Text = string.Empty;
            answerTextBox.Focus();
        }

        private void SendAnswerButton_MouseMove(object sender, MouseEventArgs e)
        {
            sendAnswerButton.BorderBrush = Brushes.Red;
            sendAnswerButton.Background = Brushes.Coral;
        }

        private void SendAnswerButton_MouseLeave(object sender, MouseEventArgs e)
        {
            sendAnswerButton.BorderBrush = Brushes.Blue;
            sendAnswerButton.Background = Brushes.AliceBlue;
        }
    }
}
