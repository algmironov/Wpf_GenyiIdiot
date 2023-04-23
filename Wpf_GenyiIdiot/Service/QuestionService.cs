using Wpf_GenyiIdiot.Storage;
using Wpf_GenyiIdiot.Model;

namespace Wpf_GenyiIdiot.Service
{
    public class QuestionService
    {
        static List<Question> questionList = new ();
        static readonly string pathToQuestions = @"Resources\questions.json";

        public static List<Question> GetQuestions()
        {
            var content = DataDealer.GetDataFromJson(pathToQuestions);
            questionList = JsonSerializer.Deserialize<List<Question>>(content);
            return questionList;
        }

        public static List<Question> GetQuestions(int amount)
        {
            var content = DataDealer.GetDataFromJson(pathToQuestions);
            questionList = JsonSerializer.Deserialize<List<Question>>(content);
            if (amount >= questionList.Count)
            {
                return questionList;
            }
            else
            {
                List<Question> result = new List<Question>();
                Random random = new Random();
                for (int i = 0; i < amount; i++)
                {
                    var index = random.Next(0, questionList.Count);
                    result.Add(questionList[index]);
                    questionList.RemoveAt(index);
                }
                return result;
            }
        }

        public static int GetQuestionsCount()
        {
            var contents = DataDealer.GetDataFromJson(pathToQuestions);
            questionList = JsonSerializer.Deserialize<List<Question>>(contents);
            return questionList.Count;
        }

        public static bool EditQuestion(Question oldQuestion, Question editedQuestion)
        {
            try
            {
                return QuestionsStorage.AddQuestion(editedQuestion) && QuestionsStorage.RemoveQuestion(oldQuestion); 
            }
            catch
            {
                return false;
            }
        }

        public static bool DeleteQuestion(Question question)
        {
            return QuestionsStorage.RemoveQuestion(question);
        }

        public static bool AddQuestion(Question question)
        {
            return QuestionsStorage.AddQuestion(question);
        }
    }
}
