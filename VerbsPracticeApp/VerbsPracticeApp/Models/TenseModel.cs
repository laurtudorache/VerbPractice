using System;

namespace VerbsPracticeApp.Models
{
    public class TenseModel
    {
        public TenseModel(string referenceTense, string answer)
        {
            Reference = referenceTense ?? string.Empty;
            Answer = answer == null ? string.Empty : answer.Trim().ToLowerInvariant();
        }

        public string Reference { get; }

        public string Answer { get; }
        public bool IsAnswerCorrect => Reference.Equals(Answer, StringComparison.InvariantCultureIgnoreCase);
    }
}