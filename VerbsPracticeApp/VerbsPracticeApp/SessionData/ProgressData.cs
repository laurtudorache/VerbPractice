namespace VerbsPracticeApp.SessionData
{
    /// <summary>
    /// an instance of this class is kept in session to keep the progress of the user
    /// </summary>
    public class ProgressData
    {
        public ProgressData()
        {
            Indexes = new int[] { };
        }

        /// <summary>
        /// Total number of the tryouts
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// Number of successful answers
        /// </summary>
        public int SuccessCount { get; set; }

        /// <summary>
        /// Indexes of the verbs already presented. Use to prevent repetition.
        /// </summary>
        public int[] Indexes { get; set; }
    }

    public class SessionKeys
    {
        public const string UserProgressKey = "UserProgress";
    }
}