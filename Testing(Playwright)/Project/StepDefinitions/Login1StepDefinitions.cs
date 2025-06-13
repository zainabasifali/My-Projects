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
    public class Login1StepDefinitions : BaseClass
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

        [Given("that go to url abc.com, navigate to the login page")]
        public async Task GivenThatGoToUrlAbc_ComNavigateToTheLoginPage()
        {
            await GoToUrl();
            await _page.Locator(LocatorClass.loginNav).ClickAsync();
        }

        [Given("enter username and password")]
        public async Task GivenEnterUsernameAndPassword()
        {
            string userName = jsonData["username"]?.ToString();
            string pass = jsonData["password"]?.ToString();

            await _page.Locator(LocatorClass.userName).FillAsync(userName);
            await _page.Locator(LocatorClass.password).FillAsync(pass);
        }

        [When("user clicks the login button")]
        public async Task WhenUserClicksTheLoginButton()
        {
            await _page.Locator(LocatorClass.loginButton).ClickAsync();
        }

        [Then("user should see a message {string}")]
        public async Task ThenUserShouldSeeAMessage(string expectedMessage)
        {
            string expectedLogin = jsonData["expectedLogin1Text"]?.ToString();
            Assert.That(expectedLogin, Is.EqualTo(expectedMessage));
        }
    }
}
