using System.Linq.Expressions;
using System.Threading;
using System.Timers;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

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
        int timeLeft = 0;
        List<Question> questionList = new List<Question>();
        List<Question> questionsToAsk = new List<Question>();
        Question question = new ();
        Random random = new ();
        int questionNumber = 0;
        int askedQuestionsCount = 0;
        Result result = new();
        DispatcherTimer timer = new DispatcherTimer();
        double progressBarStep = 0;

        public GamePage()
        {
            InitializeComponent();
            ProgressBarInit();
            GenerateQuestionList();
            TimerInit();
            UpdatePage();

        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            sendAnswerButton.Content = "Отправить ответ";
            progressBar.Value += progressBarStep;
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
                UpdatePage();
                ResetTimer();
            }
            else
            {
                timer.Stop();
                var diagnosis = DiagnosisStorage.GetDiagnosisByResult(result.CorrectAnswersCount, result.QuestionsAsked);
                result.Diagnosis = diagnosis;
                MessageBoxResult res = MessageBox.Show($"Вы ответили на {result.CorrectAnswersCount} вопросов из {result.QuestionsAsked}.\nВаш диагноз: {diagnosis}", "Игра окончена!", MessageBoxButton.OKCancel);
                if (res == MessageBoxResult.OK)
                {
                    EnterNamePage enterNamePage = new EnterNamePage();
                    enterNamePage.Owner = this;
                    enterNamePage.ShowDialog();
                    if (!string.IsNullOrEmpty(Username) || !string.IsNullOrWhiteSpace(Username))
                    {
                        result.Name = Username;
                        ResultService.SaveResult(result);
                    }
                    this.Close();
                }
            }
        }

        private void TimerInit()
        {
            timeLeft = timeForOneQuestion;
            
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            timeLeft--;

            
            sendAnswerButton.Content = $"Отправить ({timeLeft} {SecondsSpelling(timeLeft)})";
            
            if (timeLeft == 0)
            {
                timer.Stop();
                timeLeft = 10;
                SendButton_Click(sender, new RoutedEventArgs());
            }
        }

        private void ResetTimer()
        {
            timer.Stop();
            timeLeft = 10;
            timer.Start();
        }

        private void ProgressBarInit()
        {
            progressBar.Minimum = 0;
            progressBar.Maximum = 100;
            progressBarStep = 100 / questionsAmountToAsk;
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

        private static string SecondsSpelling(int num)
        {
            string seconds = "";
            switch (num)
            {
                case 0:
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                case 10:
                    seconds = "секунд";
                    break;
                case 1:
                    seconds = "секунда";
                    break;
                case 2:
                case 3:
                case 4:
                    seconds = "секунды";
                    break;
            }
            return seconds;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SendButton_Click(sender, e);
            }
        }
    }
}
