namespace UoN.AspNetCore.FeedbackMessage
{
    /// <summary>
    /// Represents a Feedback Message
    /// </summary>
    public class FeedbackMessageModel
    {
        /// <summary>
        /// Constructor, partly provided for JsonConvert
        /// </summary>
        /// <param name="message"></param>
        /// <param name="type"></param>
        public FeedbackMessageModel(string message, AlertTypes type)
        {
            Message = message;
            Type = type;
        }

        /// <summary>
        /// The plain text message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// The type of alert
        /// </summary>
        public AlertTypes Type { get; set; }

        /// <summary>
        /// CSS class string based on the type of alert.
        /// These strings have Bootstrap4 in mind, though most types will work with Bootstrap 3.
        /// If you're writing your own css, use the `alert-[type]` selector.
        /// </summary>
        public string Class => $"alert-{Type.ToString().ToLower()}";
    }
}
