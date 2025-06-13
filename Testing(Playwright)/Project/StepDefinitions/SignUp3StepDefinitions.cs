using System;
using Microsoft.Playwright;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using Project.MiscClass;
using Reqnroll;

namespace Project.StepDefinitions
{
    [Binding]
    public class SignUp3StepDefinitions : BaseClass
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

        [Given("that go to url www.Demoblaze.com, navigate to SignUp page")]
        public async Task GivenThatGoToUrlWww_Demoblaze_ComNavigateToSignUpPage()
        {
            await GoToUrl();
            await _page.Locator(LocatorClass.signUpNav).ClickAsync();
        }

        [Given("keep username and password Empty")]
        public async Task GivenKeepUsernameAndPasswordEmpty()
        {
            await _page.Locator(LocatorClass.SignupUsernameInput).FillAsync("");
            await _page.Locator(LocatorClass.SignupPasswordInput).FillAsync("");
        }

        [When("user click on Signup button")]
        public async Task WhenUserClickOnSignupButton()
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

        [Then("user should see Fill in all the fields alert")]
        public async Task ThenUserShouldSeeFillInAllTheFieldsAlert()
        {
            var timeoutTask = Task.Delay(5000);
            var completedTask = await Task.WhenAny(_dialogMessageTcs.Task, timeoutTask);
            var expectedSignUpText = jsonData["expectedLogin4Text"]?.ToString();

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
