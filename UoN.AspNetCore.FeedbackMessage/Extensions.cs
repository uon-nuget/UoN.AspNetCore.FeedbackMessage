using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;

namespace UoN.AspNetCore.FeedbackMessage
{
    /// <summary>
    /// Extension methods for supporting Feedback Messages
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// TempData key
        /// </summary>
        public const string Key = "FeedbackMessage";

        /// <summary>
        /// Controller extension method, so any controller action can do `this.SetFeedBackMessage()`
        /// </summary>
        /// <param name="message">The plain text of the message</param>
        /// <param name="type">The type of alert</param>
        public static void SetFeedbackMessage(this Controller controller, string message, AlertTypes type)
        {
            controller.TempData[Key] = JsonConvert.SerializeObject(
                new FeedbackMessageModel(message, type));
        }

        /// <summary>
        /// TempData extension, that handles casting, Key access for you
        /// </summary>
        /// <returns>The FeedbackMessageModel stored by `SetFeedbackMessage()`</returns>
        public static FeedbackMessageModel GetFeedbackMessage(this ITempDataDictionary tempData)
        {
            tempData.TryGetValue(Key, out var o);
            return o == null ? null : JsonConvert.DeserializeObject<FeedbackMessageModel>((string)o);
        }
    }
}
