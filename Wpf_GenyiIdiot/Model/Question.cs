namespace Wpf_GenyiIdiot.Model
{
    [Serializable]
    public class Question
    {
        public string Text { get; set; }
        public int Answer { get; set; }

        public Question()
        {
        }
        public Question(string text, int answer)
        {
            Text = text;
            Answer = answer;
        }

       

    }
}
