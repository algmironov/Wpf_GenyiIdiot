namespace Wpf_GenyiIdiot.Model
{
    [Serializable]
    public class Question : IEquatable<Question>
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

        public override string ToString()
        {
            return Text;
        }

        public override int GetHashCode()
        {
            return Text.GetHashCode();
        }

        public override bool Equals(object? obj)
        {
            if (obj == null)
            {
              return false;
            }
            if (obj is Question)
            {
                Question other = (Question)obj;
                return this.Text.Equals(other.Text) && this.Answer == other.Answer;
            }
            return false;
        }

        public bool Equals(Question? other)
        {
            if ((other is not null) && (other is Question))
            {
                return Text.Equals(other.Text) && Answer == other.Answer;
            }
            return false;
        }
    }
}
