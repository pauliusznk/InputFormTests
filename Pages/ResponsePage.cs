using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Playwright.NUnit;
using InputFormTests.Models;

namespace InputFormTests.Pages
{
    public class ResponsePage
    {
        private readonly IPage _page;
        private readonly ILocator _firstNameResponse;
        private readonly ILocator _secondNameResponse;
        private readonly ILocator _ageResponse;
        private readonly ILocator _countryResponse;
        private readonly ILocator _notesResponse;
        public ResponsePage(IPage page)
        {
            _page = page;
            _firstNameResponse = _page.Locator("#firstname-value");
            _secondNameResponse = _page.Locator("#surname-value");
            _ageResponse = _page.Locator("#age-value");
            _countryResponse = _page.Locator("#country-value");
            _notesResponse = _page.Locator("#notes-value");
        }
        public async Task CheckResponse(Submission submission)
        {
            await CheckField(_firstNameResponse, submission.FirstName, "First name");
            await CheckField(_secondNameResponse, submission.SecondName, "Second name");
            await CheckField(_ageResponse, submission.Age.ToString(), "Age");
            await CheckField(_countryResponse, submission.Country, "Country");
            await CheckField(_notesResponse, submission.Notes, "Notes");
        }
        private async Task CheckField(ILocator fieldLocator, string expectedValue, string fieldName)
        {
            var actualValue = await fieldLocator.InnerTextAsync();
            if (actualValue != expectedValue)
                throw new Exception($"{fieldName} mismatch: expected '{expectedValue}', got '{actualValue}'.");
        }

    }
}
