using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;
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
        /// Controller extension method, so any controller action can do `this.AddFeedbackMessage()`
        /// </summary>
        /// <param name="model">A model containing all the properties of the Feedback Message to set</param>
        public static void AddFeedbackMessage(this Controller controller, FeedbackMessageModel model)
        {
            var feedbackMessages = controller.TempData.GetFeedbackMessages();
            if (feedbackMessages == null)
            {
                feedbackMessages = new List<FeedbackMessageModel>();
            }
            feedbackMessages.Add(model);
            controller.TempData[Key] = JsonConvert.SerializeObject(feedbackMessages);
        }

        /// <summary>
        /// Controller extension method, so any controller action can clear all set feedback messages
        /// </summary>
        public static void ClearFeedbackMessages(this Controller controller)
        {
            controller.TempData[Key] = null;
        }

        /// <summary>
        /// Controller extension method, so any controller action can do `this.AddFeedbackMessage()`
        /// </summary>
        /// <param name="message">The HTML content of the message</param>
        /// <param name="type">The type of alert</param>
        public static void AddFeedbackMessage(this Controller controller, string message, string type)
            => controller.AddFeedbackMessage(new FeedbackMessageModel { Message = message, Type = type });

        /// <summary>
        /// Controller extension method, so any controller action can do `this.AddFeedbackMessage()`
        /// </summary>
        /// <param name="message">The HTML content of the message</param>
        public static void AddFeedbackMessage(this Controller controller, string message)
            => controller.AddFeedbackMessage(new FeedbackMessageModel { Message = message });

        /// <summary>
        /// Controller extension method, so any controller action can do `this.AddFeedbackMessage()`
        /// </summary>
        /// <param name="message">The HTML content of the message</param>
        /// <param name="dismissable">Should the alert be able to be dismissed?</param>
        public static void AddFeedbackMessage(this Controller controller, string message, bool dismissable)
            => controller.AddFeedbackMessage(new FeedbackMessageModel { Message = message, Dismissable = dismissable });

        /// <summary>
        /// Controller extension method, so any controller action can do `this.AddFeedbackMessage()`
        /// </summary>
        /// <param name="message">The HTML content of the message</param>
        /// <param name="type">The type of alert</param>
        /// <param name="dismissable">Should the alert be able to be dismissed?</param>
        public static void AddFeedbackMessage(this Controller controller, string message, string type, bool dismissable)
            => controller.AddFeedbackMessage(new FeedbackMessageModel
            {
                Message = message,
                Type = type,
                Dismissable = dismissable
            });

        /// <summary>
        /// TempData extension, that handles casting, Key access for you
        /// </summary>
        /// <returns>The FeedbackMessageModel stored by `AddFeedbackMessage()`</returns>
        public static IList<FeedbackMessageModel> GetFeedbackMessages(this ITempDataDictionary tempData)
        {
            tempData.TryGetValue(Key, out var o);
            return o == null ? null : JsonConvert.DeserializeObject<IList<FeedbackMessageModel>>((string)o);
        }
    }
}