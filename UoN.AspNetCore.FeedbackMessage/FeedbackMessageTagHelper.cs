using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Newtonsoft.Json;

namespace UoN.AspNetCore.FeedbackMessage
{
    /// <inheritdoc />
    /// <summary>
    /// An ASP.NET Core Tag Helper for displaying a Feedback Message if one is set
    /// </summary>
    [HtmlTargetElement("uon-feedbackmessage")]
    public class FeedbackMessageTagHelper : TagHelper
    {
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        /// <inheritdoc />
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            try
            {
                var model = ViewContext.TempData.GetFeedbackMessage();
                if (model == null)
                {
                    output.TagName = string.Empty;
                    return;
                }

                output.TagMode = TagMode.StartTagAndEndTag;
                output.TagName = "div";
                output.Attributes.Add("class", $"alert {model.Class}");
                output.Content.SetContent(model.Message);
            }
            catch (JsonSerializationException)
            {
                //TODO logging?
                output.TagName = string.Empty;
            }
        }
    }
}
