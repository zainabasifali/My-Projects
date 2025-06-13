using System;
using Microsoft.Playwright;
using NUnit.Framework;
using Project.MiscClass;
using Reqnroll;

namespace Project.StepDefinitions
{
    [Binding]
    public class Purchase1StepDefinitions : BaseClass
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

        [Given("user will navigate to demoblaze website")]
        public async Task GivenUserWillNavigateToDemoblazeWebsite()
        {
            await GoToUrl();
        }

        [Given("user will add product to cart and navigate to cart")]
        public async Task GivenUserWillAddProductToCartAndNavigateToCart()
        {
            await page.ClickAsync(LocatorClass.FirstProductLink);
            Console.WriteLine("Executing When step");
            if (_page == null)
            {
                Assert.Fail("_page is null in the When step!");
                return;
            }
            _dialogMessageTcs = new TaskCompletionSource<string>();

            _page.Dialog += async (_, dialog) =>
            {
                _dialogMessageTcs.SetResult(dialog.Message);
                await dialog.AcceptAsync();
            };

            await page.ClickAsync(LocatorClass.AddToCartButton);
            
           

 
            await page.ClickAsync(LocatorClass.cartNav);
            await Task.Delay(3000);

        }

        [Given("user will click on place order")]
        public async Task GivenUserWillClickOnPlaceOrder()
        {
            await page.ClickAsync(LocatorClass.placeOrderButton);
        }

        [When("user will fill the purchase form")]
        public async Task WhenUserWillFillThePurchaseForm()
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

            await _page.Locator(LocatorClass.purchaseButton).ClickAsync();
            await Task.Delay(3000); 
        }

        [Then("user will see the confirm message")]
        public async Task ThenUserWillSeeTheConfirmMessage()
        {
            string expectedMessage = jsonData["successMessage"]?.ToString();

            var actualText = await _page.Locator(LocatorClass.confirmationMessage).TextContentAsync();

            Assert.That(actualText?.Trim(), Is.EqualTo(expectedMessage));
        }
    }
}
