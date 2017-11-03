# UoN.AspNetCore.FeedbackMessage

[![License](https://img.shields.io/badge/licence-MIT-blue.svg)](https://opensource.org/licenses/MIT)
[![Build Status](https://travis-ci.org/UniversityOfNottingham/UoN.AspNetCore.FeedbackMessage.svg?branch=develop)](https://travis-ci.org/UniversityOfNottingham/UoN.AspNetCore.FeedbackMessage)

## What is it?

Reusable bits for ASP.NET Core to make displaying feedback messages after action redirects easy.

## What are its features?

It provides the following:

- An enum of Bootstrap 4 based Alert Types
- A model class representing a Feedback Message
- Extension methods for setting and getting `FeedbackMessageModel`s at `TempData["FeedbackMessage"]`
- A TagHelper for rendering any Feedback Message at `TempData["FeedbackMessage"]`
- A Controller for returning a partial view (constructed by the TagHelper); useful for requesting via AJAX to add to a page.
- An extension method for adding the controller (and partial view) to MVC.

## Dependencies

The library targets `netstandard2.0` and depends upon ASP.Net Core 2.0 MVC.

If you can use ASP.Net Core 2 MVC, you can use this library.

## Usage

### Acquiring the library

#### NuGet

This library is available from [nuget.org](https://www.nuget.org/packages/UoN.AspNetCore.FeedbackMessage/)

#### Build from source

We recommend building with the `dotnet` cli, but since the package targets `netstandard2.0` and depends only on ASP.Net Core 2.0 MVC, you should be able to build it in any tooling that supports those requirements.

- Have the .NET Core SDK 2.0 or newer
- `dotnet build`
- Optionally `dotnet pack`
- Reference the resulting assembly, or NuGet package.

### Standard Server-side usage

1. Acquire the library via one of the methods above.
1. Use `this.SetFeedbackMessage()` inside an MVC Controller method.
1. Import TagHelpers from this assembly
    - add the following to a Razor View, or to `_ViewImports.cshtml`:
    - `@addTagHelper *, UoN.AspNetCore.FeedbackMessage`
1. Use the `<uon-feedbackmessage />` TagHelper in a a Razor View.
1. ????
1. PROFIT!

#### An example:

`MyController.cs`

``` csharp
public class MyController : Controller
{
    ...

    public IActionResult MyAction()
    {
        this.SetFeedbackMessage("Hello!", AlertTypes.Info);
        return View();
    }
}
```

`MyAction.cshtml`

``` html
<div>
    <uon-feedbackmessage />

    The rest of my HTML content is here and very interesting.
</div>
```

- If a Feedback Message is not set, `<uon-feedbackmessage />` will simply collapse into nothing.
- If a Feedback Message is set, `<uon-feedbackmessage />` will turn into something like the following:

``` html
<div class="alert alert-info">
    Hello!
</div>
```

### AJAX usage

1. Acquire the library via one of the methods above.
1. Use `services.AddMvc().AddAjaxFeedbackMessageSupport()` inside `Startup.ConfigureServices()`
1. Optionally specify an MVC Route template for the `FeedbackMessageAjaxController`, else it will default to `/FeedbackMessageAjax` as per MVC default conventions
1. Write some javascript that makes an AJAX request, and puts the result onto the page.
1. ????
1. PROFIT!

#### An example:

`Startup.cs`

```csharp
public void ConfigureServices(IServiceCollection services)
        {
            ...

            services.AddMvc()
                .AddAjaxFeedbackMessageSupport(services);

            ...
        }
```

`feedback-message.js`

```javascript
//global function that assumes we have jquery...
window.feedbackMessage = (message, type) => {
    let feedback = $("#feedback-message"); //this is a div on the page

    $.get({
        url: "/FeedbackMessageAjax",
        data: { "message": message, "type": type },
        success: function(content) {
            //use animation to make it clear the message has changed if there was already one there!
            feedback.fadeOut(200, "swing", function() {
                feedback.html(content);

                feedback.fadeIn(100);
            });
        }
    });
};

...

//some situation in which we want a feedback message
window.feedbackMessage("Hello!", "info");
```

## Contributing

Contributions are welcome.

If there are issues open, please feel free to make pull requests for them, and they will be reviewed.
