using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using UoN.AspNetCore.FeedbackMessage.Models;

namespace UoN.AspNetCore.FeedbackMessage.Controllers
{
    public class FeedbackMessageAjaxController : Controller
    {
        [HttpPost]
        public IActionResult Index([FromBody] IEnumerable<FeedbackMessageModel> feedbackMessageModels)
            => ViewComponent("UonFeedbackMessage", feedbackMessageModels);
    }
}
