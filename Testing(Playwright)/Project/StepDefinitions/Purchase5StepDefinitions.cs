using System;
using Allure.Net.Commons;
using Microsoft.Playwright;
using NUnit.Framework;
using Project.MiscClass;
using Reqnroll;

namespace Project.StepDefinitions
{
    [Binding]
    public class Purchase5StepDefinitions : BaseClass
    {
        private IPage _page;
        private TaskCompletionSource<string> _dialogMessageTcs;

        [BeforeScenario]
        public async Task Setup()
        {
            if (page == null)
            {
                await InitPageAsync();
            }

            _page = page;

        }

        [Given("the user is on the Demoblaze homepage")]
        public async Task GivenTheUserIsOnTheDemoblazeHomepage()
        {
            await GoToUrl();
        }

        [When("the user clicks on the {string} link in the navbar")]
        public async Task WhenTheUserClicksOnTheLinkInTheNavbar(string link)
        {
            if (link.ToLower() == "cart")
            {
                await _page.Locator(LocatorClass.cartNav).ClickAsync();
                await Task.Delay(2000); 
            }

        }

        [When("the user click the {string} button")]
        public async Task WhenTheUserClickTheButton(string buttonText)
        {
            if (buttonText.ToLower().Contains("place order"))
            {
                await _page.Locator(LocatorClass.placeOrderButton).ClickAsync();

                await _page.WaitForSelectorAsync(LocatorClass.orderModal, new PageWaitForSelectorOptions { Timeout = 5000 });
                await _page.WaitForSelectorAsync(LocatorClass.orderNameInput, new PageWaitForSelectorOptions { Timeout = 5000 });
            }

        }

        [Then("a modal form should appear for entering purchase details")]
        public async Task ThenAModalFormShouldAppearForEnteringPurchaseDetails()
        {
            bool isVisible = await _page.Locator(LocatorClass.orderModal).IsVisibleAsync();

            if (!isVisible)
            {
                Console.WriteLine("Modal not visible. HTML snapshot:");
                string html = await _page.ContentAsync();
                Console.WriteLine(html);

                await _page.ScreenshotAsync(new PageScreenshotOptions { Path = "modal_not_visible.png" });
            }

            Assert.IsTrue(isVisible, "The order form modal is not visible.");

        }

        [When("the user fills in the form with data from {string}")]
        public async Task WhenTheUserFillsInTheFormWithDataFrom(string fileName)
        {
            string name = jsonData["name"]?.ToString();
            string country = jsonData["country"]?.ToString();
            string city = jsonData["city"]?.ToString();
            string card = jsonData["card"]?.ToString();
            string month = jsonData["month"]?.ToString();
            string year = jsonData["year"]?.ToString();

            await _page.Locator(LocatorClass.orderNameInput).FillAsync(name);
            await _page.Locator(LocatorClass.orderCountryInput).FillAsync(country);
            await _page.Locator(LocatorClass.orderCityInput).FillAsync(city);
            await _page.Locator(LocatorClass.orderCardInput).FillAsync(card);
            await _page.Locator(LocatorClass.orderMonthInput).FillAsync(month);
            await _page.Locator(LocatorClass.orderYearInput).FillAsync(year);
        }

        [When("the user clicks the  button")]
        public async Task WhenTheUserClicksTheButtonWithoutLabel()
        {
            await _page.Locator(LocatorClass.purchaseButton).ClickAsync();
            await Task.Delay(3000); 
        }

        [Then("a confirmation modal should appear with the message")]
        public async Task ThenAConfirmationModalShouldAppearWithTheMessage()
        {
            string expectedMessage = jsonData["successMessage"]?.ToString();

            var actualText = await _page.Locator(LocatorClass.confirmationMessage).TextContentAsync();

            Assert.That(actualText?.Trim(), Is.EqualTo(expectedMessage));
        }

    }
}
