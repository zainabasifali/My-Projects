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
    public class ProductPost
    {
        [SetUp]
        public void Setup()
        {
        }
        IPlaywright playwright;
        IPage page;

        [AllureStep]
        [Test]
        public async Task ProductPostResponse()
        {
            playwright = await Playwright.CreateAsync();
            {
                var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
                {
                    Headless = false,
                });

                var page = await browser.NewPageAsync();
                string apiURL = "http://localhost:3000/products";
                var apiBody = new
                {
                    id = "3",
                    name = "Laptops Hp",
                    prize = 560
                }
            ;
                var response = await page.APIRequest.PostAsync(apiURL, new APIRequestContextOptions
                {
                    DataObject = apiBody
                });

                await page.CloseAsync();

            }

        }

    }
}
