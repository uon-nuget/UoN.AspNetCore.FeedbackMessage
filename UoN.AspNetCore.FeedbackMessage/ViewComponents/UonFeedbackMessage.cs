using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UoN.AspNetCore.FeedbackMessage.Models;

namespace UoN.AspNetCore.FeedbackMessage.ViewComponents
{
    [ViewComponent(Name = "UonFeedbackMessage")]
    public class UonFeedbackMessage : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(string message, string type)
            => View(new FeedbackMessageModel
            {
                Message = message,
                Type = type
            });
    }
}
