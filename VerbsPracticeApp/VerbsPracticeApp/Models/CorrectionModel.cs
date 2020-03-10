using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace VerbsPracticeApp.Models
{
    public class CorrectionModel
    {
        [ReadOnly(true)]
        [Display(Name = "Infinitive")]
        public string Infinitive { get; set; }

        [ReadOnly(true)]
        public string English { get; set; }

        [Display(Name = "Imperfectum singular")]
        public TenseModel ImperfectumSingular { get; set; }

        [Display(Name = "Imperfectum plural")]
        public TenseModel ImperfectumPlural { get; set; }

        [Display(Name = "Perfectum")]
        public TenseModel Perfectum { get; set; }

        public int CorrectCount { get; set; }

        public bool IsAnswerCorrect => ImperfectumSingular.IsAnswerCorrect
            && ImperfectumPlural.IsAnswerCorrect
            && Perfectum.IsAnswerCorrect;
    }
}