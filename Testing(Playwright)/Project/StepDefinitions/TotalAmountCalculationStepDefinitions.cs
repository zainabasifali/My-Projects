using System;
using Microsoft.Playwright;
using NUnit.Framework;
using Project.MiscClass;
using Reqnroll;

namespace Project.StepDefinitions
{
    [Binding]
    public class TotalAmountCalculationStepDefinitions : BaseClass
    {
        private IPage _page;
        private TaskCompletionSource<string> _dialogMessageTcs;
        string totalAmountinCart;

        [BeforeScenario]
        public async Task Setup()
        {
            if (page == null)
            {
                await InitPageAsync();
            }

            _page = page;

        }
        [Given("the user goes to demoblaze website")]
        public async Task GivenTheUserGoesToDemoblazeWebsite()
        {
            await GoToUrl();
        }

        [Given("adds multiple products to cart")]
        public async Task GivenAddsMultipleProductsToCart()
        {
            await _page.ClickAsync(LocatorClass.FirstProductLink);
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

            await _page.ClickAsync(LocatorClass.AddToCartButton);
            await _page.ClickAsync(LocatorClass.homeNav);
            await _page.ClickAsync(LocatorClass.SecondProductLink);
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

            await _page.ClickAsync(LocatorClass.AddToCartButton);




        }

        [Given("clicks on cart nav")]
        public async Task GivenClicksOnCartNav()
        {
           

            await _page.ClickAsync(LocatorClass.cartNav); // ? use _page
            await Task.Delay(2000);

            var element = _page.Locator(LocatorClass.TotalAmountinCart); // ? correct object
            await element.WaitForAsync();

            string rawText = await element.TextContentAsync();
            Console.WriteLine("Cart amount found: " + rawText);

            totalAmountinCart = "Amount: " + rawText + " USD";

        }

        [Given("clics on place order")]
        public async Task GivenClicsOnPlaceOrder()
        {
            await _page.ClickAsync(LocatorClass.placeOrderButton);
        }

        [When("Fills the purchase form correctly")]
        public async Task WhenFillsThePurchaseFormCorrectly()
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

        [Then("the user should see the corect amount on the order confirmation card")]
        public async Task ThenTheUserShouldSeeTheCorectAmountOnTheOrderConfirmationCard()
        {
            await _page.Locator(LocatorClass.TotalAmountinOrderConfirmation).WaitForAsync(new() { Timeout = 10000 });

            var confirmationText = await _page.Locator(LocatorClass.TotalAmountinOrderConfirmation).TextContentAsync();
            Console.WriteLine("Order Confirmation Text: " + confirmationText);

            var match = System.Text.RegularExpressions.Regex.Match(confirmationText, @"Amount:\s*\d+\s*USD");
            string amountLine = match.Success ? match.Value : null;

            Console.WriteLine("Amount Extracted: " + amountLine);
            Console.WriteLine("Cart Total Stored: " + totalAmountinCart);

            Assert.That(totalAmountinCart?.Trim(), Is.EqualTo(amountLine?.Trim()));


        }
    }
}
