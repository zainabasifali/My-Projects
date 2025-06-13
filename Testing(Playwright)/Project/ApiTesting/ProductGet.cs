using Allure.NUnit;
using Allure.NUnit.Attributes;
using Microsoft.Playwright;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ApiTesting
{
    [AllureNUnit]
    public class ProductGet
    {
        [SetUp]
        public void Setup()
        {
        }
        IPlaywright playwright;
        IPage page;

        [AllureStep]
        [Test]
        public async Task ProductGetResponse()
        {
            playwright = await Playwright.CreateAsync();
            {
                var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
                {
                    Headless = false,
                });

                var page = await browser.NewPageAsync();
                string apiURL = "http://localhost:3000/products/1";
                var response = await page.APIRequest.GetAsync(apiURL);
                if (response.Status == 200)
                {
                    TestContext.WriteLine("API is now responding");
                }
                else
                {
                    TestContext.WriteLine($"API is not responding {response.Status}");
                }

                string responseBody = await response.TextAsync();
                if (responseBody.Contains("Samsung galaxy s6"))
                {
                    TestContext.WriteLine("Body Text is validated");
                }
                else
                {
                    throw new Exception("Body Text is Failed");
                }
                await page.CloseAsync();

            }

        }

    }
}
