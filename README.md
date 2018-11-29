# UoN.AspNetCore.FeedbackMessage

[![License](https://img.shields.io/badge/licence-MIT-blue.svg)](https://opensource.org/licenses/MIT)
[![Build Status](https://travis-ci.org/UniversityOfNottingham/UoN.AspNetCore.FeedbackMessage.svg?branch=develop)](https://travis-ci.org/UniversityOfNottingham/UoN.AspNetCore.FeedbackMessage)

A Razor Class Library for ASP.NET Core to make displaying feedback messages after action redirects easy.

# Features

Feedback Message alert features:

- Alerts can be rendered in any [Bootstrap 4] style (including those set as custom theme colors), or with any custom css class in the form `alert-[type]`.
- Messages can feature HTML content.
- Alerts can include a "close" button, to dismiss them. This works out the box with [Bootstrap 4], or can otherwise be implemented separately.
- Can be used for static alerts as well as Session based `TempData` driven alerts.
- Can be used via AJAX to build alert markup without reloading the page.

It provides the following code items:

- A model class representing a configurable Feedback Message.
- Extension methods for setting and getting `FeedbackMessageModel`s at `TempData["FeedbackMessage"]`.
- A View for rendering a `FeedbackMessageModel` as a [Bootstrap 4] style alert.
- A ViewComponent for rendering the above View, or a local override.
- A TagHelper for rendering the above ViewComponent with a model either from `TempData["FeedbackMessage"]` or as configured by attributes.
- A Controller for returning the above ViewComponent as configured by the HTTP request; useful for requesting via AJAX to add the result to a page.

# Dependencies

The library targets `netstandard2.0` and depends upon ASP.Net Core 2.1 MVC.

If you can use ASP.Net Core 2.1 MVC, you can use this library.

# Installation

## NuGet

This library is available from [nuget.org](https://www.nuget.org/packages/UoN.AspNetCore.FeedbackMessage/)

## Build from source

We recommend building with the `dotnet` cli, but since the package targets `netstandard2.0` and depends only on ASP.Net Core 2.1 MVC, you should be able to build it in any tooling that supports those requirements.

- Have the .NET Core SDK 2.1 or newer
- `dotnet build`
- Optionally `dotnet pack`
- Reference the resulting assembly, or NuGet package.

# Usage

## Static usage in a Razor View

While not the most useful thing this library can do, it can be used as a TagHelper for bootstrap alert markup in a Razor view:

1. Acquire the library via one of the methods above.
1. Import TagHelpers from this assembly
    - add the following to a Razor View, or to `_ViewImports.cshtml`:
    - `@addTagHelper *, UoN.AspNetCore.FeedbackMessage`
1. Use the `<uon-feedbackmessage />` TagHelper in a Razor View.
1. ????
1. PROFIT!

### TagHelper parameters

By default, an empty `<uon-feedbackmessage />` TagHelper will only render an alert if a FeedbackMessage is set in `TempData`. This behaviour is configurable.

- `message` - sets a static message on this FeedbackMessage tag.
    - this will always be rendered, unless `use-tempdata` is true, and a message is set in TempData.
- `type` - sets an alert type (informing the CSS class) to use when rendering the above `message`.
    - defaults to `secondary`.
- `dismissable` - sets whether the alert is dismissable when rendering the above message.
    - defaults to `true`.
- `use-tempdata` - Specifies whether this FeedbackMessage should render messages in TempData, when they exist.
    - defaults to `true`.
    - if `false`, only the message as configured by the above attributes will be shown, TempData content will have no effect on this tag.
    - if `true`, when there is TempData content, it will be shown, otherwise the message as configured above will be shown.

## Standard Server-side usage

1. Acquire the library via one of the methods above.
1. Ensure the [ASP.NET Core Session Middleware] is configured in your project.
1. Use `this.SetFeedbackMessage()` inside an MVC Controller method.
1. Import TagHelpers from this assembly
    - add the following to a Razor View, or to `_ViewImports.cshtml`:
    - `@addTagHelper *, UoN.AspNetCore.FeedbackMessage`
1. Use the `<uon-feedbackmessage />` TagHelper in a Razor View.
1. ????
1. PROFIT!

Refer to the TagHelper parameters above to see how the TagHelper can be further configured.

### `SetFeedbackMessage()` parameters

- `message` - sets a message in TempData.
- `type` - sets an alert type (informing the CSS class) to use when rendering the above `message`.
    - defaults to `secondary`.
- `dismissable` - sets whether the alert is dismissable when rendering the above message.
    - defaults to `true`.

### An example:

`MyController.cs`

``` csharp
public class MyController : Controller
{
    ...

    public IActionResult MyAction()
    {
        this.SetFeedbackMessage("It's working!", "success");
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
<div class="alert alert-success">
    Hello!
</div>
```

## AJAX usage

1. Acquire the library via one of the methods above.
1. Optionally specify an MVC Route template for the `FeedbackMessageAjaxController`, else it will default to `/FeedbackMessageAjax` as per MVC default conventions
1. Write some javascript that makes an AJAX request, and puts the result onto the page.
1. ????
1. PROFIT!

### `/FeedbackMessageAjax` Request parameters

- `message` - sets a message for the markup to be returned in the AJAX response.
- `type` - sets an alert type (informing the CSS class) to use when rendering the above `message`.
    - defaults to `secondary`.
- `dismissable` - sets whether the alert is dismissable when rendering the above message.
    - defaults to `true`.

### An example:

`feedback-message.js`

```javascript
//global function that assumes we have jquery...
window.feedbackMessage = (message, type, dismissable) => {
    let feedback = $("#feedback-message"); //this is a div on the page

    $.get({
        url: "/FeedbackMessageAjax",
        data: { "message": message, "type": type, "dismissable": dismissable },
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

# Roadmap

The following are future work that could be contributed to the library:

- Make lots of things configurable
    - CSS Classes (ore than just `alert-[type]`)
    - TagHelper wrapping tags? might be handy for AJAX use
    - AJAX Controller Action route
        - this is currently configurable via MVC, but I don't remember how and it's not documented.
    - Default alert type (right now this is fixed at `secondary`)
    - Default TagHelper TempData usage (right now this is on by default)
    - Allow turning off HTML content, and setting the default behaviour
- Add some features:
    - Maybe switch from `HTML.Raw()` to use a Markdown parser instead
        - this will limit HTLM content to be safer, I think?
        - as well as providing simpler syntax for common use
        - and a built in way of rendering escaped code if desired
        - probably makes the configuration option for plain text only redundant.
    - Explore and document overriding the view rendered by the ViewComponent.
        - in theory, being a Razor Class Library allows this, but some guidance / examples would be nice.

# Contributing

Contributions are welcome.

If there are issues open, please feel free to make pull requests for them, and they will be reviewed.

Also implementations for anything on the above roadmap will be considered.


[Bootstrap 4]: https://getbootstrap.com
[ASP.NET Core Session Middleware]: https://docs.microsoft.com/en-us/aspnet/core/fundamentals/app-state?view=aspnetcore-2.1