using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using Service;

namespace PlaywrightTests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]

// use to run codegen: .\bin\Debug\net8.0\playwright.ps1 codegen http://localhost:5000

public class Tests : PageTest
{
    
    [Test]
    public async Task TestCreateBox()
    {
        Helper.TriggerRebuild();
        
        await Page.GotoAsync("http://localhost:5000/");

        await Page.GotoAsync("http://localhost:5000/tabs/tabs/boxfeed");

        await Page.Locator("ion-fab-button").GetByRole(AriaRole.Img).Nth(1).ClickAsync();
        
        await Page.GetByLabel("Box Name").FillAsync("PlaywrightTestingBox69");
        

        await Page.GetByLabel("Size").FillAsync("20; 20; 69;");
        

        await Page.GetByLabel("Price").FillAsync("60");
        

        await Page.GetByLabel("Description").FillAsync("A box used for testing");
        

        await Page.GetByLabel("Image").FillAsync("a image that does not exit used for testing");

        await Page.GetByRole(AriaRole.Button, new() { Name = "Create Box" }).ClickAsync();
        
        //make expectations

        await Expect(Page.GetByText("PlaywrightTestingBox69")).ToBeVisibleAsync();
        await Expect(Page.GetByText("$ 60")).ToBeVisibleAsync();
    }
    
    [Test]
    public async Task TestEditBox()
    {
        Helper.TriggerRebuild();
        
        await Page.GotoAsync("http://localhost:5000/");

        await Page.GotoAsync("http://localhost:5000/tabs/tabs/boxfeed");

        await Page.Locator("ion-toolbar").Filter(new() { HasText = "best box" }).GetByRole(AriaRole.Button).ClickAsync();

        await Page.GetByRole(AriaRole.Button).Nth(1).ClickAsync();

        await Page.GetByLabel("Box Name").ClickAsync();

        await Page.GetByLabel("Box Name").FillAsync("best boxTestPlayWright");

        await Page.GetByRole(AriaRole.Button, new() { Name = "Update Box" }).ClickAsync();

        await Page.Locator("ion-title").Filter(new() { HasText = "best boxTestPlayWright" }).Locator("div").ClickAsync();
        //make expectations

        await Expect(Page.GetByText("PlaywrightTestingBox69")).ToBeVisibleAsync();
        await Expect(Page.GetByText("$ 60")).ToBeVisibleAsync();
    }
    
    
}