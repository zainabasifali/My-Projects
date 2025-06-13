using System;
using Microsoft.Playwright;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using Project.MiscClass;
using Reqnroll;

namespace Project.StepDefinitions
{
    [Binding]
    public class Login6StepDefinitions : BaseClass
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
        [Given("that you Go to Url www.demoblaze.com, navigate to the login page")]
        public async Task GivenThatYouGoToUrlWww_Demoblaze_ComNavigateToTheLoginPage()
        {
            await GoToUrl();
            await _page.Locator(LocatorClass.loginNav).ClickAsync();
        }

        [Given("keep username and password empty")]
        public async Task GivenKeepUsernameAndPasswordEmpty()
        {
            await _page.Locator(LocatorClass.userName).FillAsync("");
            await _page.Locator(LocatorClass.password).FillAsync("");
        }

        [When("User click the Login button")]
        public async Task WhenUserClickTheLoginButton()
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

            await _page.Locator(LocatorClass.loginButton).ClickAsync();
        }

        [Then("User should see a Alert box saying Please fill out Username and Password.")]
        public async Task ThenUserShouldSeeAAlertBoxSayingPleaseFillOutUsernameAndPassword_()
        {
            var timeoutTask = Task.Delay(5000);
            var completedTask = await Task.WhenAny(_dialogMessageTcs.Task, timeoutTask);
            var expectedLoginText = jsonData["expectedLogin4Text"]?.ToString();

            if (completedTask == timeoutTask)
            {
                Assert.Fail("Alert dialog did not appear within the expected time.");
            }

            string alertText = await _dialogMessageTcs.Task;

            Assert.That(alertText, Is.EqualTo(expectedLoginText));
            await Task.Delay(3000);
        }
    }
}
