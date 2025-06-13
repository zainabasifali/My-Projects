using System;
using Microsoft.Playwright;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using Project.MiscClass;
using Reqnroll;
namespace Project.StepDefinitions
{
    [Binding]
    public class AddToCartStepDefinitions : BaseClass
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

        [Given("the user is on the homepage")]
        public async Task GivenTheUserIsOnTheHomepage()
        {
            await GoToUrl();
        }

        [When("the user clicks on the first product")]
        public async Task WhenTheUserClicksOnTheFirstProduct()
        {
            await page.ClickAsync(LocatorClass.FirstProductLink);
        }

        [When("the user clicks the {string} button")]
        public async Task WhenTheUserClicksTheButton(string p0)
        {
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

        [Then("the alert message should match the one in data file")]
        public async Task ThenTheAlertMessageShouldMatchTheOneInDataFile()
        {
            var timeoutTask = Task.Delay(5000);
            var completedTask = await Task.WhenAny(_dialogMessageTcs.Task, timeoutTask);
            var expectedCartText = jsonData["expectedCartText"]?.ToString();

            if (completedTask == timeoutTask)
            {
                Assert.Fail("Alert dialog did not appear within the expected time.");
            }

            string alertText = await _dialogMessageTcs.Task;

            Assert.That(alertText, Is.EqualTo(expectedCartText));
            await Task.Delay(3000);
        }

    }
}
