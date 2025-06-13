using System;
using Microsoft.Playwright;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using Project.MiscClass;
using Reqnroll;

namespace Project.StepDefinitions
{
    [Binding]
    public class ContactStepDefinitions : BaseClass
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

        [Given("that go to url www.demoblaze.com, navigate to contact page")]
        public async Task GivenThatGoToUrlWww_Demoblaze_ComNavigateToContactPage()
        {
            await GoToUrl();
            await _page.Locator(LocatorClass.contactNav).ClickAsync();
        }

        [Given("fill the email, name and message")]
        public async Task GivenFillTheEmailNameAndMessage()
        {
            string userName = jsonData["username"]?.ToString();
            string email = jsonData["email"]?.ToString();
            string message = jsonData["message"]?.ToString();

            await _page.Locator(LocatorClass.ContactNameInput).FillAsync(userName);
            await _page.Locator(LocatorClass.ContactEmailInput).FillAsync(email);
            await _page.Locator(LocatorClass.ContactMessageTextarea).FillAsync(message);
        }

        [When("user click on send message")]
        public async Task WhenUserClickOnSendMessage()
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

            await _page.Locator(LocatorClass.ContactSendMessageButton).ClickAsync();
        }

        [Then("user should see Thanks for the message Alert")]
        public async Task ThenUserShouldSeeThanksForTheMessageAlert()
        {
            var timeoutTask = Task.Delay(5000);
            var completedTask = await Task.WhenAny(_dialogMessageTcs.Task, timeoutTask);
            var expectedContactText = jsonData["expectedContactText"]?.ToString();
            if (completedTask == timeoutTask)
            {
                Assert.Fail("Alert dialog did not appear within the expected time.");
            }

            string alertText = await _dialogMessageTcs.Task;

            Assert.That(alertText, Is.EqualTo(expectedContactText));
            await Task.Delay(3000);
        }
    }
}
