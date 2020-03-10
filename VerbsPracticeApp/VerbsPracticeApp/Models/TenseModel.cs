namespace VerbsPracticeApp.Models
{
    public class TenseModel
    {
        public TenseModel(string original, string answer)
        {
            Original = original;
            Answer = answer;
        }

        public string Original { get; set; }

        public string Answer { get; set; }
        public bool IsAnswerCorrect => Original.Equals(Answer);
    }
}