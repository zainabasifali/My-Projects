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
    public class UserLogoutStepDefinitions : BaseClass
    {
        private IPage _page;

        [BeforeScenario]
        public async Task Setup()
        {
            if (page == null)
            {
                await InitPageAsync();
            }

            _page = page;

        }

        [Given("the user is logged in")]
        public async Task GivenTheUserIsLoggedIn()
        {
            await GoToUrl();
            await _page.Locator(LocatorClass.loginNav).ClickAsync();
            await Task.Delay(1000);

            await _page.Locator(LocatorClass.userName).FillAsync(jsonData["username"].ToString());
            await _page.Locator(LocatorClass.password).FillAsync(jsonData["password"].ToString());
            await _page.Locator(LocatorClass.loginButton).ClickAsync();

            await _page.WaitForSelectorAsync(LocatorClass.loginSuccess);
        }

        [When("the user clicks the button")]
        public async Task WhenTheUserClicksTheLogoutButton()
        {
           await _page.Locator(LocatorClass.logoutNav).ClickAsync();

        }


        [Then("the login username should not be visible")]
        public async Task ThenTheLoginUsernameShouldNotBeVisible()
        {
            await _page.WaitForSelectorAsync(LocatorClass.loginSuccess, new() { State = WaitForSelectorState.Hidden, Timeout = 5000 });

            var isVisible = await _page.Locator(LocatorClass.loginSuccess).IsVisibleAsync();
            Assert.IsFalse(isVisible, "Login username is still visible after logout, logout might have failed.");
        }

    }
}
