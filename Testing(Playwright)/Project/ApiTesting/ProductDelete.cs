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
    public class ProductDelete
    {
        [SetUp]
        public void Setup()
        {
        }
        IPlaywright playwright;
        IPage page;

        [AllureStep]
        [Test]
        public async Task ProductDeleteResponse()
        {
            playwright = await Playwright.CreateAsync();
            {
                var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
                {
                    Headless = false,
                });

                var page = await browser.NewPageAsync();
                string apiURL = "http://localhost:3000/products/3";
                var response = await page.APIRequest.DeleteAsync(apiURL);
                if (response.Status == 200)
                {
                    TestContext.WriteLine("API is now responding");
                }
                else
                {
                    TestContext.WriteLine($"API is not responding {response.Status}");
                }


                await page.CloseAsync();

            }

        }

    }
}
