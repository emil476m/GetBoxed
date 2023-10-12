using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using Service;

namespace PlaywrightTests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]

// use to run codegen: .\bin\Debug\net8.0\playwright.ps1 codegen http://localhost:5000
public class BrowserTestForGraph : PageTest
{
    [SetUp]
    public async Task SetUp()
    {
        await Page.GotoAsync("http://localhost:5000/");
    }

    [Test]
    public async Task CanSwitchToGraphPage()
    {
        await Page.GotoAsync("http://localhost:5000/tabs/tabs/boxfeed");

        await Page.Locator("#tab-button-graph svg").ClickAsync();

        Assert.AreEqual("http://localhost:5000/tabs/tabs/graphs", Page.Url);
    }

    
    [Test]
    public async Task GraphCanvasVisable()
    {
        await Page.GotoAsync("http://localhost:5000/tabs/tabs/graphs");

        await Page.Locator("canvas").IsVisibleAsync();
        
        await Page.Locator("canvas").ClickAsync(new LocatorClickOptions
        {
            Position = new Position
            {
                X = 690,
                Y = 209,
            },
        });
    }
}