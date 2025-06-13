using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using Project.MiscClass;
using Reqnroll;

namespace Project.StepDefinitions
{
    [Binding]
    public class DeleteProductFromCart : BaseClass
    {
        private IPage _page;
        private string _productName;

        [BeforeScenario]
        public async Task Setup()
        {
            if (page == null)
            {
                await InitPageAsync();
            }

            _page = page;
        }

        [Given("the User is on the homepage")]
        public async Task GivenTheUserIsOnTheHomepage()
        {
            await GoToUrl();
        }

        [When("the user clicks on the product")]
        public async Task WhenTheUserClicksOnTheProduct()
        {
            _productName = jsonData["productName1"].ToString();
            await _page.Locator($"//a[text()='{_productName}']").ClickAsync();
        }

        [When("the user clicks the Add to cart button")]
        public async Task WhenTheUserClicksTheAddToCartButton()
        {
            await _page.Locator(LocatorClass.AddToCartButton).ClickAsync();
        }

        [When("accepts the alert popup")]
        public async Task WhenAcceptsTheAlertPopup()
        {
            _page.Dialog += async (_, dialog) => await dialog.AcceptAsync();
            await Task.Delay(2000);
        }

        [When("the user navigates to the cart page")]
        public async Task WhenTheUserNavigatesToTheCartPage()
        {
            await _page.Locator(LocatorClass.cartNav).ClickAsync();
        }

        [When("the user deletes the added product from the cart")]
        public async Task WhenTheUserDeletesTheAddedProductFromTheCart()
        {
            var deleteBtn = _page.Locator($"//td[text()='{_productName}']/following-sibling::td/a[text()='Delete']");
            await deleteBtn.ClickAsync();
            await Task.Delay(2000); 
        }

        [Then("the product with same name no longer visible in the cart")]
        public async Task ThenTheProductWithSameNameNoLongerVisibleInTheCart()
        {
            var productLocator = _page.Locator($"//td[text()='{_productName}']");
            var isVisible = await productLocator.IsVisibleAsync();
            Assert.IsFalse(isVisible, $"Product '{_productName}' is still visible in the cart.");
        }
    }
}
