using Wpf_GenyiIdiot.Storage;
using Wpf_GenyiIdiot.Model;

namespace Wpf_GenyiIdiot.Service
{
    public class QuestionService
    {
        static List<Question> questionList = new ();
        static readonly string pathToQuestions = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "questions.json");

        public static async Task<List<Question>> GetQuestions()
        {
            StreamReader reader = new (pathToQuestions, Encoding.UTF8);
            var contents = await reader.ReadToEndAsync();
            questionList = JsonSerializer.Deserialize<List<Question>>(contents);
            return questionList;
        }

        public static async Task<int> GetQuestionsCount()
        {
            StreamReader reader = new (pathToQuestions, Encoding.UTF8);
            var contents = await reader.ReadToEndAsync();
            questionList = JsonSerializer.Deserialize<List<Question>>(contents);
            return questionList.Count;
        }

        public static bool EditQuestion(Question oldQuestion, Question editedQuestion)
        {
            try
            {
                QuestionsStorage.AddQuestion(editedQuestion);
                QuestionsStorage.RemoveQuestion(oldQuestion);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool DeleteQuestion(Question question)
        {
            try
            {
                QuestionsStorage.RemoveQuestion(question);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool AddQuestion(Question question)
        {
            try
            {
                QuestionsStorage.AddQuestion(question);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
