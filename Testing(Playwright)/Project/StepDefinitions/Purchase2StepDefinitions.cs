using System;
using Microsoft.Playwright;
using NUnit.Framework;
using Project.MiscClass;
using Reqnroll;

namespace Project.StepDefinitions
{
    [Binding]
    public class Product2StepDefinitions : BaseClass
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
        [Given("User will go to demoblaze website")]
        public async Task GivenUserWillGoToDemoblazeWebsite()
        {
            await GoToUrl();
        }

        [When("Add product to cart")]
        public async Task WhenAddProductToCart()
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

        [When("Navigate to cart")]
        public async Task WhenNavigateToCart()
        {
            await page.ClickAsync(LocatorClass.cartNav);
            await Task.Delay(3000);
        }

        [When("Click place order")]
        public async Task WhenClickPlaceOrder()
        {
            await page.ClickAsync(LocatorClass.placeOrderButton);
        }

        [When("User will fill only name field of the purchase form")]
        public async Task WhenUserWillFillOnlyNameFieldOfThePurchaseForm()
        {
            string name = jsonData["name"]?.ToString();
            

            await _page.Locator(LocatorClass.orderNameInput).FillAsync(name);
           

            await _page.Locator(LocatorClass.purchaseButton).ClickAsync();
            await Task.Delay(3000);
        }

        [Then("User will see an error message to fill all fields")]
        public async Task ThenUserWillSeeAnErrorMessageToFillAllFields()
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
