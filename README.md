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

## Dependencies

The library targets `netstandard2.0` and depends upon ASP.Net Core 2.0 MVC.

If you can use ASP.Net Core 2 MVC, you can use this library.

## Usage

1. Acquire the library via one of the methods below.
1. Use `this.SetFeedbackMessage()` inside an MVC Controller method.
1. Use the `<uon-feedbackmessage />` TagHelper in a a Razor View.
1. ????
1. PROFIT!

### NuGet

This library will be hosted on nuget.org from `1.0.0` at the latest.

### Build from source

We recommend building with the `dotnet` cli, but since the package targets `netstandard2.0` and depends only on ASP.Net Core 2.0 MVC, you should be able to build it in any tooling that supports those requirements.

- Have the .NET Core SDK 2.0 or newer
- `dotnet build`
- Optionally `dotnet pack`
- Reference the resulting assembly, or NuGet package.

### An example:

#### `MyController.cs`

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

#### `MyAction.cshtml`

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

## Contributing

Contributions are welcome.

If there are issues open, please feel free to make pull requests for them, and they will be reviewed.
