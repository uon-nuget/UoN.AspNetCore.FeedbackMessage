using Microsoft.AspNetCore.Mvc;

namespace UoN.AspNetCore.FeedbackMessage
{
    public class FeedbackMessageAjaxController : Controller
    {
        public IActionResult Index(FeedbackMessageModel model)
        {
            this.SetFeedbackMessage(model.Message, model.Type);

            return PartialView("UoNFeedbackMessage");
        }
    }
}
