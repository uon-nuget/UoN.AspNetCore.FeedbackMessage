using Microsoft.AspNetCore.Mvc;
using UoN.AspNetCore.FeedbackMessage.Models;

namespace UoN.AspNetCore.FeedbackMessage.Controllers
{
    public class FeedbackMessageAjaxController : Controller
    {
        public IActionResult Index(string message, string type = null)
        {
            return ViewComponent("UonFeedbackMessage", new FeedbackMessageModel
            {
                Message = message,
                Type = type
            });
        }
    }
}
