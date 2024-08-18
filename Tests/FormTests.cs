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

namespace InputFormTests.Tests
{
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
        public async Task CorrectSubmissionMinimumLengthValues()
        {
            Submission submission = new Submission(StringGenerator.GenerateRandomString(1), StringGenerator.GenerateRandomString(11), 18, "Afghanistan", StringGenerator.GenerateRandomString(1));
            await _formPage.GoTo();
            await _formPage.SubmitForm(submission);
            await _responsePage.CheckResponse(submission);
        }
        [Test]
        public async Task CorrectSubmissionMaximumLengthValues()
        {
            Submission submission = new Submission(StringGenerator.GenerateRandomString(89), StringGenerator.GenerateRandomString(79), 80, "Zimbabwe", StringGenerator.GenerateRandomString(2000));
            await _formPage.GoTo();
            await _formPage.SubmitForm(submission);
            await _responsePage.CheckResponse(submission);
        }
        [Test]
        public async Task IncorrectSubmissionEmptyFirstName()
        {
            Submission submission = new Submission("", StringGenerator.GenerateRandomString(79), 80, "Zimbabwe", StringGenerator.GenerateRandomString(1));
            await _formPage.GoTo();
            await _formPage.SubmitForm(submission);
            await _formPage.FirstNameErrorVisible();
            await _responsePage.AssertPageIsNotLoaded();
        }
        [Test]
        public async Task IncorrectSubmissionFirstNameTooLong()
        {
            Submission submission = new Submission(StringGenerator.GenerateRandomString(90), StringGenerator.GenerateRandomString(79), 80, "Zimbabwe", StringGenerator.GenerateRandomString(1));
            await _formPage.GoTo();
            await _formPage.SubmitForm(submission);
            await _formPage.FirstNameErrorVisible();
            await _responsePage.AssertPageIsNotLoaded();
        }
        [Test]
        public async Task IncorrectSubmissionEmptyLastName()
        {
            Submission submission = new Submission(StringGenerator.GenerateRandomString(20), "", 80, "Zimbabwe", StringGenerator.GenerateRandomString(1));
            await _formPage.GoTo();
            await _formPage.SubmitForm(submission);
            await _formPage.LastNameErrorVisible();
            await _responsePage.AssertPageIsNotLoaded();
        }
        [Test]
        public async Task IncorrectSubmissionTooShortLastName()
        {
            Submission submission = new Submission(StringGenerator.GenerateRandomString(20), StringGenerator.GenerateRandomString(10), 80, "Zimbabwe", StringGenerator.GenerateRandomString(1));
            await _formPage.GoTo();
            await _formPage.SubmitForm(submission);
            await _formPage.LastNameErrorVisible();
            await _responsePage.AssertPageIsNotLoaded();
        }
        [Test]
        public async Task IncorrectSubmissionTooLongLastName()
        {
            Submission submission = new Submission(StringGenerator.GenerateRandomString(20), StringGenerator.GenerateRandomString(80), 80, "Zimbabwe", StringGenerator.GenerateRandomString(1));
            await _formPage.GoTo();
            await _formPage.SubmitForm(submission);
            await _formPage.LastNameErrorVisible();
            await _responsePage.AssertPageIsNotLoaded();
        }
        [Test]
        public async Task IncorrectSubmissionEmptyAge()
        {
            Submission submission = new Submission(StringGenerator.GenerateRandomString(20), StringGenerator.GenerateRandomString(20), null, "Lithuania", StringGenerator.GenerateRandomString(1));
            await _formPage.GoTo();
            await _formPage.SubmitForm(submission);
            await Task.Delay(3000);
            await _formPage.AgeErrorVisible();
            await _responsePage.AssertPageIsNotLoaded();
        }
        [Test]
        public async Task IncorrectSubmissionTooLittleAge()
        {
            Submission submission = new Submission(StringGenerator.GenerateRandomString(20), StringGenerator.GenerateRandomString(20), 17, "Lithuania", StringGenerator.GenerateRandomString(1));
            await _formPage.GoTo();
            await _formPage.SubmitForm(submission);
            await Task.Delay(3000);
            await _formPage.AgeErrorVisible();
            await _responsePage.AssertPageIsNotLoaded();
        }
        [Test]
        public async Task IncorrectSubmissionTooBigAge()
        {
            Submission submission = new Submission(StringGenerator.GenerateRandomString(20), StringGenerator.GenerateRandomString(20), 81, "Lithuania", StringGenerator.GenerateRandomString(1));
            await _formPage.GoTo();
            await _formPage.SubmitForm(submission);
            await Task.Delay(3000);
            await _formPage.AgeErrorVisible();
            await _responsePage.AssertPageIsNotLoaded();
        }
    }
}