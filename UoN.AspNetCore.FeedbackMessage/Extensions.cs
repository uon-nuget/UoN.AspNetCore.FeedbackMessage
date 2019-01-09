using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;
using System.Collections.Generic;
using UoN.AspNetCore.FeedbackMessage.Models;

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
        /// <param name="model">A list of type model containing all the properties of the Feedback Message to set</param>
        public static void SetFeedbackMessages(
            this Controller controller,
            IList<FeedbackMessageModel> model)
            => controller.TempData[Key] = JsonConvert.SerializeObject(model);

        /// <summary>
        /// TempData extension, that handles casting, Key access for you
        /// </summary>
        /// <returns>The FeedbackMessageModel stored by `SetFeedbackMessage()`</returns>
        public static List<FeedbackMessageModel> GetFeedbackMessages(this ITempDataDictionary tempData)
        {
            tempData.TryGetValue(Key, out var o);
            return o == null ? null : JsonConvert.DeserializeObject<List<FeedbackMessageModel>>((string)o);
        }
    }
}
