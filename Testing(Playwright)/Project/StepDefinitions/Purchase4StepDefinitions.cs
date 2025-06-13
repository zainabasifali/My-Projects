using System;
using Microsoft.Playwright;
using NUnit.Framework;
using Project.MiscClass;
using Reqnroll;

namespace Project.StepDefinitions
{
    [Binding]
    public class Purchase4StepDefinitions : BaseClass
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
        [Given("user will go to the url demobalze.com")]
        public async Task GivenUserWillGoToTheUrlDemobalze_Com()
        {
            await GoToUrl();
        }

        [Given("User will add product to cart")]
        public async Task GivenUserWillAddProductToCart()
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
        }

        [Given("user will click on cart nav")]
        public async Task GivenUserWillClickOnCartNav()
        {
            await page.ClickAsync(LocatorClass.cartNav);
            await Task.Delay(3000);
        }

        [Given("user will place his order")]
        public async Task GivenUserWillPlaceHisOrder()
        {
            await page.ClickAsync(LocatorClass.placeOrderButton);
        }

        [When("user will fill not fill name field")]
        public async Task WhenUserWillFillNotFillNameField()
        {
           
            string country = jsonData["country"]?.ToString();
            string city = jsonData["city"]?.ToString();
            string card = jsonData["card"]?.ToString();
            string month = jsonData["month"]?.ToString();
            string year = jsonData["year"]?.ToString();

            await _page.Locator(LocatorClass.orderCountryInput).FillAsync(country);
            await _page.Locator(LocatorClass.orderCityInput).FillAsync(city);
            await _page.Locator(LocatorClass.orderCardInput).FillAsync(card);
            await _page.Locator(LocatorClass.orderMonthInput).FillAsync(month);
            await _page.Locator(LocatorClass.orderYearInput).FillAsync(year);

           
        }

        [When("user will click on purchase button")]
        public async Task WhenUserWillClickOnPurchaseButton()
        {
            await _page.Locator(LocatorClass.purchaseButton).ClickAsync();
            await Task.Delay(3000);
        }

        [Then("an error message will appear")]
        public async Task ThenAnErrorMessageWillAppear()
        {
            var timeoutTask = Task.Delay(5000);
            var completedTask = await Task.WhenAny(_dialogMessageTcs.Task, timeoutTask);
            var expectedErrText = jsonData["expectedPurchaseErrorText"]?.ToString();

            if (completedTask == timeoutTask)
            {
                Assert.Fail("Alert dialog did not appear within the expected time.");
            }

            string alertText = await _dialogMessageTcs.Task;

            Assert.That(alertText, Is.EqualTo(expectedErrText));
            await Task.Delay(3000);
        }
    }
}
