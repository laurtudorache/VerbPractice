using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace VerbsPracticeApp.Models
{
    public class VerbModel
    {
        [ReadOnly(true)]
        public int TotalCount { get; set; }

        [ReadOnly(true)]
        public int SuccessCount { get; set; }

        [ReadOnly(true)]
        [Display(Name = "Infinitive")]
        public string Infinitive { get; set; }

        [ReadOnly(true)]
        public string English { get; set; }

        [Display(Name = "Imperfectum singular")]
        public string ImperfectumSingular { get; set; }

        [Display(Name = "Imperfectum plural")]
        public string ImperfectumPlural { get; set; }

        [Display(Name = "Perfectum")]
        public string Perfectum { get; set; }
    }
}