using System.ComponentModel;

namespace VerbsPracticeApp.Models
{
    public class FinishedModel
    {
        [ReadOnly(true)]
        public int TotalCount { get; set; }

        [ReadOnly(true)]
        public int SuccessCount { get; set; }
    }
}