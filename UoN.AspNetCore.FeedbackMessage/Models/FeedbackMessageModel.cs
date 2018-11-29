namespace UoN.AspNetCore.FeedbackMessage.Models
{
    /// <summary>
    /// Represents a Feedback Message
    /// </summary>
    public class FeedbackMessageModel
    {
        public static readonly string DefaultType = "secondary";

        /// <summary>
        /// The plain text message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Optional type of alert. e.g. if you pass "info",
        /// the CSS class "alert-info" will be applied to the rendered output.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Should the alert contain classes and markup which make it able to be dismissed.
        /// Works out the box with Bootstrap 4.
        /// </summary>
        public bool Dismissable { get; set; } = true;

        /// <summary>
        /// CSS class string based on the type of alert.
        /// Works out the box with defined Bootstrap theme colors
        /// If you're writing your own css, use the `alert-[type]` selector.
        /// </summary>
        public string Class => $"alert-{(string.IsNullOrWhiteSpace(Type) ? DefaultType : Type)}";
    }
}
