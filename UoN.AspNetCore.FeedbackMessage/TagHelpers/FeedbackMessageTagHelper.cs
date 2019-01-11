using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UoN.AspNetCore.FeedbackMessage.Models;
using UoN.AspNetCore.FeedbackMessage.ViewComponents;

namespace UoN.AspNetCore.FeedbackMessage
{
    /// <inheritdoc />
    /// <summary>
    /// An ASP.NET Core Tag Helper for displaying a Feedback Message if one is set
    /// </summary>
    [HtmlTargetElement("uon-feedbackmessage")]
    public class FeedbackMessageTagHelper : TagHelper
    {
        private readonly IViewComponentHelper _viewComponentHelper;

        public FeedbackMessageTagHelper(IViewComponentHelper viewComponentHelper)
        {
            _viewComponentHelper = viewComponentHelper;
        }

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        [HtmlAttributeName("message")]
        public string Message { get; set; }

        [HtmlAttributeName("type")]
        public string Type { get; set; }

        [HtmlAttributeName("use-tempdata")]
        public bool UseTempData { get; set; } = true;

        [HtmlAttributeName("dismissable")]
        public bool Dismissable { get; set; } = true;

        /// <inheritdoc />
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            // this tag heper doesn't actually output anything.
            // It does render our ViewComponent afterwards, if there is any content for it.
            output.SuppressOutput();
            IList<FeedbackMessageModel> model = null;
            // TempData takes precedence over static attributes, if we're set to use it
            if (UseTempData)
            {
                try
                {
                    model = ViewContext.TempData.GetFeedbackMessages();
                }
                catch (JsonSerializationException)
                {
                    // We don't care; model will be null and so we'll pick up outside this block
                }
            }
            // Either we're not using TempData, or none was successfully set
            if (model is null && !string.IsNullOrWhiteSpace(Message))
                model = new List<FeedbackMessageModel>
                {
                    new FeedbackMessageModel()
                    {
                        Message = Message,
                        Type = Type,
                        Dismissable = Dismissable
                    }
                };
            if (model is null) return;
            ((IViewContextAware)_viewComponentHelper).Contextualize(ViewContext);
            var content = await _viewComponentHelper.InvokeAsync(
                typeof(UonFeedbackMessage), model);
            output.Content.SetHtmlContent(content);
        }
    }
}
