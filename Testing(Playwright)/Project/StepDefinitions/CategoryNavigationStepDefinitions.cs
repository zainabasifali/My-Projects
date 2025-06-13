using System;
using Microsoft.Playwright;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using Project.MiscClass;
using Reqnroll;

namespace Project.StepDefinitions
{
    [Binding]
    public class CategoryNavigationStepDefinitions : BaseClass
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

        [Given("that the user is on the homepage")]
        public async Task GivenThatTheUserIsOnTheHomepage()
        {
            GoToUrl();
        }

        [When("the user clicks on the Laptops category")]
        public async Task WhenTheUserClicksOnTheLaptopsCategory()
        {
            await _page.Locator(LocatorClass.categoryId).ClickAsync();

        }

        [Then("the user should see a list of products in the Laptops category")]
        public async Task ThenTheUserShouldSeeAListOfProductsInTheLaptopsCategory()
        {
            var productLocator = _page.Locator(LocatorClass.categoryLaptop);

            try
            {
                await productLocator.WaitForAsync(new() { Timeout = 5000 });

                bool isVisible = await productLocator.IsVisibleAsync();
                Assert.IsTrue(isVisible, "Expected laptop product 'Sony vaio i5' is not visible in the Laptops category.");
            }
            catch (TimeoutException)
            {
                Assert.Fail("Laptop product 'Sony vaio i5' did not appear within the expected time.");
            }
        }
    }
    }
