using System;

namespace Wpf_GenyiIdiot.Model
{
    [Serializable]
    public class Result : IEquatable<Result>
    {
        public string Name { get; set; } = "Unknown";
        public int CorrectAnswersCount { get; set; } = 0;
        public int QuestionsAsked { get; set; }  = 0;
        public string Diagnosis { get; set; } = "Unknown";

        public Result(string name, int answersCount, int questionsAsked, string diagnosis)
        {
            Name = name;
            CorrectAnswersCount = answersCount;
            Diagnosis = diagnosis;
            QuestionsAsked = questionsAsked;
        }

        public Result()
        {

        }

        public override string ToString()
        {
            return $"{Name}, {CorrectAnswersCount} , {QuestionsAsked}, {Diagnosis}";
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (GetType() != obj.GetType())
                return false;
            Result other = obj as Result;

            return Name.Equals(other.Name) && CorrectAnswersCount == other.CorrectAnswersCount && QuestionsAsked == other.QuestionsAsked && Diagnosis.Equals(other.Diagnosis);
        }

        public bool Equals(Result other)
        {
            return other is not null &&
                   Name == other.Name &&
                   CorrectAnswersCount == other.CorrectAnswersCount &&
                   QuestionsAsked == other.QuestionsAsked &&
                   Diagnosis == other.Diagnosis;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, CorrectAnswersCount, Diagnosis);
        }
    }
}