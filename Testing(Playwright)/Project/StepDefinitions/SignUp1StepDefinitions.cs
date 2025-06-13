using System;
using Microsoft.Playwright;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using Project.MiscClass;
using Reqnroll;

namespace Project.StepDefinitions
{
    [Binding]
    public class SignUp1StepDefinitions : BaseClass
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

        [Given("that go to url www.demoblaze.com, navigate to SignUp page")]
        public async Task GivenThatGoToUrlWww_Demoblaze_ComNavigateToSignUpPage()
        {
            await GoToUrl();
            await _page.Locator(LocatorClass.signUpNav).ClickAsync();
        }

        [Given("fill valid username and password")]
        public async Task GivenFillValidUsernameAndPassword()
        {
            string username = jsonData["signupUsername"]?.ToString();
            string pass = jsonData["password"]?.ToString();

            await _page.Locator(LocatorClass.SignupUsernameInput).FillAsync(username);
            await _page.Locator(LocatorClass.SignupPasswordInput).FillAsync(pass);
        }

        [When("user clicks on signup button")]
        public async Task WhenUserClicksOnSignupButton()
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

            await _page.Locator(LocatorClass.SignupSubmitButton).ClickAsync();
        }

        [Then("user should see signUp successful alert")]
        public async Task ThenUserShouldSeeSignUpSuccessfulAlert()
        {
            var timeoutTask = Task.Delay(5000);
            var completedTask = await Task.WhenAny(_dialogMessageTcs.Task, timeoutTask);
            var expectedSignUpText = jsonData["expectedSignUp1Text"]?.ToString();

            if (completedTask == timeoutTask)
            {
                Assert.Fail("Alert dialog did not appear within the expected time.");
            }

            string alertText = await _dialogMessageTcs.Task;

            Assert.That(alertText, Is.EqualTo(expectedSignUpText));
            await Task.Delay(3000);
        }
    }
}
