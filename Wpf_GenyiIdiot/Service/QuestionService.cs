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
                QuestionsStorage.AddQuestion(editedQuestion);
                if (QuestionsStorage.RemoveQuestion(oldQuestion))
                {
                    return true;
                }
                else
                {
                    return false;
                }
                
            }
            catch
            {
                return false;
            }
        }

        public static bool DeleteQuestion(Question question)
        {
            if (QuestionsStorage.RemoveQuestion(question))
            {
                return true;
            }
            return false;
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
