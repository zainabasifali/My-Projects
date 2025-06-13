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
    public class ProductPut
    {
        [SetUp]
        public void Setup()
        {
        }

        IPlaywright playwright;
        IPage page;

        [AllureStep]
        [Test]
        public async Task ProductPutResponse()
        {
            playwright = await Playwright.CreateAsync();
            {
                var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
                {
                    Headless = false,
                });

                var page = await browser.NewPageAsync();

                string apiURL = "http://localhost:3000/products/3";

                var updatedBody = new
                {
                    id = "3",
                    name = "Laptops Hp",
                    prize = 100
                };

                var response = await page.APIRequest.PutAsync(apiURL, new APIRequestContextOptions
                {
                    DataObject = updatedBody
                });

                if (response.Status == 200 || response.Status == 204)
                {
                    TestContext.WriteLine("Update Successful");
                }
                else
                {
                    TestContext.WriteLine($"Update Failed with Status Code: {response.Status}");
                }

                string responseBody = await response.TextAsync();
                TestContext.WriteLine("Response Body: " + responseBody);

                await page.CloseAsync();
            }
        }

    }
}
