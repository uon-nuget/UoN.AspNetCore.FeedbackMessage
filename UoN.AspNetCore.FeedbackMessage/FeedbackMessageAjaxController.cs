using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;

namespace UoN.AspNetCore.FeedbackMessage
{
    public class FeedbackMessageAjaxController : Controller
    {
        private readonly IRazorViewEngine _viewEngine;

        public FeedbackMessageAjaxController(IRazorViewEngine viewEngine)
        {
            _viewEngine = viewEngine;
        }

        public IActionResult Index(string message, string type)
        {
            AlertTypes t;
            try
            {
                t = (AlertTypes) Enum.Parse(typeof(AlertTypes), type, true);
            }
            //These are exceptions that might throw if type can't be parsed as an AlertTypes value
            catch (Exception e) when (
                e is ArgumentException ||
                e is OverflowException)
            {
                t = AlertTypes.Secondary; //default
            }

            this.SetFeedbackMessage(message, t);

            return PartialView("UoNFeedbackMessage");
        }
    }
}
