using Microsoft.Playwright;
using NBomber.Contracts.Stats;
using NBomber.Contracts;
using NBomber.Http;
using NUnit.Framework;
using NBomber.CSharp;
using Project.MiscClass;
using Newtonsoft.Json.Linq;
using Allure.NUnit;
using Allure.NUnit.Attributes;

namespace Project.LoadTesting
{
    [AllureNUnit]
    public class LoadPurchaseTest
    {
        [AllureStep]
        [Test]
        public async Task Test01()
        {
            var s = "Load Test with Playwright in Demoblaze Application";
            var jsonData = JObject.Parse(File.ReadAllText("C:\\Users\\zaina\\Desktop\\Project\\Project\\data.json"));

            var playwright = await Playwright.CreateAsync();
            var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false
            });

            var scenario = Scenario.Create(s, async scenarioContext =>
            {
                string name = jsonData["name"]?.ToString();
                string country = jsonData["country"]?.ToString();
                string city = jsonData["city"]?.ToString();
                string card = jsonData["card"]?.ToString();
                string month = jsonData["month"]?.ToString();
                string year = jsonData["year"]?.ToString();

                var page = await browser.NewPageAsync();

                await page.GotoAsync("https://www.demoblaze.com/");
                await page.ClickAsync(LocatorClass.FirstProductLink);

                string dialogMessage = null;

                page.Dialog += async (_, dialog) =>
                {
                    dialogMessage = dialog.Message;
                    await dialog.AcceptAsync();
                };

                await page.ClickAsync(LocatorClass.AddToCartButton);

                await Task.Delay(1000);

                await page.ClickAsync(LocatorClass.cartNav);
                await page.ClickAsync(LocatorClass.placeOrderButton);

                await page.Locator(LocatorClass.orderNameInput).WaitForAsync(new() { State = WaitForSelectorState.Visible });
                await page.Locator(LocatorClass.orderNameInput).FillAsync(name);
                await page.Locator(LocatorClass.orderCountryInput).FillAsync(country);
                await page.Locator(LocatorClass.orderCityInput).FillAsync(city);
                await page.Locator(LocatorClass.orderCardInput).FillAsync(card);
                await page.Locator(LocatorClass.orderMonthInput).FillAsync(month);
                await page.Locator(LocatorClass.orderYearInput).FillAsync(year);

                await page.Locator(LocatorClass.purchaseButton).ClickAsync();

                return Response.Ok();
            })
            .WithLoadSimulations(LoadSimulation.NewRampingConstant(3, TimeSpan.FromSeconds(30)));

            var result = NBomberRunner
                .RegisterScenarios(scenario)
                .WithReportFolder("Reports")
                .WithReportFormats(ReportFormat.Html)
                .WithWorkerPlugins(new HttpMetricsPlugin(new[] { HttpVersion.Version1 }))
                .Run();

            Assert.True(result.ScenarioStats.Get(s).Ok.Latency.Percent75 < 10000);
        }

    }
}
