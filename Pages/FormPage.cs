using InputFormTests.Models;
using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InputFormTests.Pages
{
    public class FormPage
    {
        private readonly IPage _page;
        private readonly ILocator _firstNameInput;
        private readonly ILocator _lastNameInput;
        private readonly ILocator _ageInput;
        private readonly ILocator _countryInput;
        private readonly ILocator _notesInput;
        private readonly ILocator _submitButton;
        private readonly ILocator _firstNameCustomError;
        private readonly ILocator _lastNameCustomError;
        private readonly ILocator _ageCustomError;
        private readonly ILocator _countryCustomError;


        public FormPage(IPage page)
        {
            _page = page;
            _firstNameInput = _page.Locator("#firstname");
            _lastNameInput = _page.Locator("#surname");
            _ageInput = _page.Locator("#age");
            _countryInput = _page.Locator("#country");
            _notesInput = _page.Locator("#notes");
            _submitButton = _page.Locator("input[type=\"submit\"]");
            _firstNameCustomError = _page.Locator("[name=\"firstnamevalidation\"]");
            _lastNameCustomError = _page.Locator("[name=\"surnamevalidation\"]");
            _ageCustomError = _page.Locator("[name=\"agevalidation\"]");
        }
        public async Task GoTo()
        {
            await _page.GotoAsync("https://testpages.eviltester.com/styled/validation/input-validation.html");
        }
        public async Task SubmitForm(Submission submission)
        {
            await _firstNameInput.FillAsync(submission.FirstName);
            await _lastNameInput.FillAsync(submission.SecondName);
            await _ageInput.FillAsync(submission.Age.ToString());
            await _countryInput.SelectOptionAsync(submission.Country);
            await _notesInput.FillAsync(submission.Notes);
            await _submitButton.ClickAsync();
        }
        public async Task FirstNameErrorVisible()
        {
            bool isCustomErrorVisible = await _firstNameCustomError.IsVisibleAsync();
            string validationMessage = await _firstNameInput.EvaluateAsync<string>("input => input.validationMessage");
            bool isHtmlErrorVisible = !string.IsNullOrEmpty(validationMessage);
            Assert.IsTrue(isCustomErrorVisible || isHtmlErrorVisible);
        }
        public async Task LastNameErrorVisible()
        {
            bool isCustomErrorVisible = await _lastNameCustomError.IsVisibleAsync();
            string validationMessage = await _lastNameInput.EvaluateAsync<string>("input => input.validationMessage");
            bool isHtmlErrorVisible = !string.IsNullOrEmpty(validationMessage);
            Assert.IsTrue(isCustomErrorVisible || isHtmlErrorVisible);
        }
        public async Task AgeErrorVisible()
        {
            bool isCustomErrorVisible = await _ageCustomError.IsVisibleAsync();
            string validationMessage = await _ageInput.EvaluateAsync<string>("input => input.validationMessage");
            bool isHtmlErrorVisible = !string.IsNullOrEmpty(validationMessage);
            Console.WriteLine(isCustomErrorVisible);
            Console.WriteLine(isHtmlErrorVisible);
            Console.WriteLine(validationMessage);
            Assert.IsTrue(isCustomErrorVisible || isHtmlErrorVisible);
        }
    }
}
