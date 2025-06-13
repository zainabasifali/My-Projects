using System;
using Microsoft.Playwright;
using NUnit.Framework;
using Project.MiscClass;
using Reqnroll;

namespace Project.StepDefinitions 
{
    [Binding]
    public class Purchase3StepDefinitions : BaseClass
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
        [Given("user will navigate on demoblaze website")]
        public async Task GivenUserWillNavigateOnDemoblazeWebsite()
        {
            await GoToUrl();
        }

        [Given("user will add a product to cart")]
        public async Task GivenUserWillAddAProductToCart()
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

        [Given("user will click on cart")]
        public async Task GivenUserWillClickOnCart()
        {
            await page.ClickAsync(LocatorClass.cartNav);
            await Task.Delay(3000);
        }

        [Given("user will place order")]
        public async Task GivenUserWillPlaceOrder()
        {
            await page.ClickAsync(LocatorClass.placeOrderButton);
        }

        [When("user will not fill any fields in purchase form")]
        public void WhenUserWillNotFillAnyFieldsInPurchaseForm()
        {
            
        }

        [When("user will click on purchase")]
        public async Task WhenUserWillClickOnPurchase()
        {
            await _page.Locator(LocatorClass.purchaseButton).ClickAsync();
            await Task.Delay(3000);
        }

        [Then("user will see an error message alert")]
        public async Task ThenUserWillSeeAnErrorMessageAlert()
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
