using Microsoft.Playwright.NUnit;
using Microsoft.Playwright;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using InputFormTests.Pages;
using InputFormTests.Utilities;
using InputFormTests.Models;

namespace InputFormTests.Tests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class FormTests : PageTest
{
    private FormPage _formPage;
    private ResponsePage _responsePage;
    [SetUp]
    public void SetUp()
    {
        _formPage = new FormPage(Page);
        _responsePage = new ResponsePage(Page);
    }

    [Test]
    public async Task Submit()
    {
        Submission submission = new Submission("asdasdasdasdasd", "asdasdasdasdasd", 18, "Lithuania", "");
        await _formPage.GoTo();
        await _formPage.SubmitForm(submission);
        await Task.Delay(3000);
        await _responsePage.CheckResponse(submission);
    }
        //    [Test]
        //    public async Task GetStartedLink()
        //    {
        //        await Page.GotoAsync("https://playwright.dev");

        //        // Click the get started link.
        //        await Page.GetByRole(AriaRole.Link, new() { Name = "Get started" }).ClickAsync();

        //        // Expects page to have a heading with the name of Installation.
        //        await Expect(Page.GetByRole(AriaRole.Heading, new() { Name = "Installation" })).ToBeVisibleAsync();
        //    }
    }