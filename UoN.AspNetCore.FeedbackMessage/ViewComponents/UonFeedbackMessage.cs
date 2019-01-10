using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UoN.AspNetCore.FeedbackMessage.Models;

namespace UoN.AspNetCore.FeedbackMessage.ViewComponents
{
    [ViewComponent(Name = "UonFeedbackMessage")]
    public class UonFeedbackMessage : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(
            IEnumerable<FeedbackMessageModel> feedbackMessageModels)
            => View(feedbackMessageModels);
    }
}