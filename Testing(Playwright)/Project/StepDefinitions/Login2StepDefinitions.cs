using System;
using Microsoft.Playwright;
using Newtonsoft.Json.Linq;
using Project.MiscClass;
using Reqnroll;
using NUnit.Framework;

namespace Project.StepDefinitions
{
    [Binding]
    public class Login2StepDefinitions : BaseClass
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

        [Given("that go to url demoblaze.com, navigate to the login page")]
        public async Task GivenThatGoToUrlDemoblaze_ComNavigateToTheLoginPage()
        {
            await GoToUrl();
            await _page.Locator(LocatorClass.loginNav).ClickAsync();
        }

        [Given("enter wrong username and correct password")]
        public async Task GivenEnterWrongUsernameAndCorrectPassword()
        {
            string userName = jsonData["Wrongusername"]?.ToString();
            string pass = jsonData["password"]?.ToString();

            await _page.Locator(LocatorClass.userName).FillAsync(userName);
            await _page.Locator(LocatorClass.password).FillAsync(pass);
        }

        [When("user click the login button")]
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

        [Then("user should see a alert box saying User does not exist.")]
        public async Task ThenUserShouldSeeAAlertBoxSayingUserDoesNotExist_()
        {
            var timeoutTask = Task.Delay(5000);
            var completedTask = await Task.WhenAny(_dialogMessageTcs.Task, timeoutTask);
            var expectedLoginText = jsonData["expectedLogin2Text"]?.ToString();
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
