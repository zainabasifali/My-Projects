using Microsoft.Playwright;
using Newtonsoft.Json.Linq;

public class BaseClass
{
    public static IPage page;
    private static IPlaywright playwright;
    private static IBrowser browser;
    private static IBrowserContext context;
    public static JObject jsonData;

    public static async Task InitPageAsync()
    {
        playwright = await Playwright.CreateAsync();
        browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = false 
        });
        context = await browser.NewContextAsync();
        page = await context.NewPageAsync();
        jsonData = JObject.Parse(File.ReadAllText("C:\\Users\\zaina\\Desktop\\Project\\Project\\data.json"));
    }

    public async Task GoToUrl()
    {
        string url = jsonData["url"].ToString();
        await page.GotoAsync(url);
    }

    public async Task CloseBrowser()
    {
        await browser.CloseAsync();
        playwright.Dispose();
    }
}
