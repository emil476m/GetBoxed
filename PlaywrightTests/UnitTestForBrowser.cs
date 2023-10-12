using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Dapper;
using Infarstructure;
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
    private HttpClient _httpClient;
    [SetUp]
    public void Setup()
    {
        _httpClient = new HttpClient();
    }
    
    [Test]
    public async Task TestCreateBox()
    {
        Helper.TriggerRebuild();
        
        await Page.GotoAsync("http://localhost:5000/");

        await Page.GotoAsync("http://localhost:5000/tabs/tabs/boxfeed");

        await Page.GetByTestId("addbtn_").GetByRole(AriaRole.Img).Nth(1).ClickAsync();
        
        await Page.GetByTestId("boxName_").Locator("label").FillAsync("PlaywrightTestingBox69");
        

        await Page.GetByTestId("boxSize_").Locator("label").FillAsync("20; 20; 69;");
        

        await Page.GetByTestId("boxPrice_").Locator("label").FillAsync("60");
        

        await Page.GetByTestId("boxDesc_").Locator("label").FillAsync("A box used for testing");
        

        await Page.GetByTestId("boxImg_").Locator("label").FillAsync("a image that does not exit used for testing");

        await Page.GetByRole(AriaRole.Button, new() { Name = "Create Box" }).ClickAsync();
        
        //make expectations

        await Expect(Page.GetByTestId("card_ PlaywrightTestingBox69")).ToBeVisibleAsync();
        
        await Expect(Page.GetByText("$ 60")).ToBeVisibleAsync();
    }
    
    
    
    [Test]
    public async Task TestEditBox()
    {
        Helper.TriggerRebuild();

        var boxName = "BoxTobeUpdated";
        
        var box = new Box()
        {
            boxId = 1,
            name = boxName,
            size = "Mock size",
            description = "mock description",
            price = 1,
            boxImgUrl = "someurl",
            isDeleted = false
        };
        var sql = $@" 
            insert into getboxed.box (name, size, description, price, boxImgUrl, isDeleted) VALUES(@name, @size, @description,@price, @boxImgUrl, @isDeleted);
            ";
        using (var conn = Helper.DataSource.OpenConnection())
        {
            conn.Execute(sql, box);
        }
        
        
        Page.SetDefaultTimeout(3000);
        var str = "best boxPlaywrightTestingUpdate";
        await Page.GotoAsync("http://localhost:5000/");

        //await Page.WaitForTimeoutAsync(5000);

        await Page.GetByTestId("detail_ "+boxName).GetByRole(AriaRole.Button).ClickAsync();
        await Page.WaitForTimeoutAsync(5000);

        await Page.GetByTestId("editbtn_").GetByRole(AriaRole.Button).ClickAsync();
       
        await Page.GetByTestId("boxName_").Locator("input").FillAsync(str);
        
        await Page.GetByTestId("boxSize_").Locator("input").FillAsync("X5 ; Y5 ; Z10");

        await Page.GetByTestId("boxPrice_").Locator("input").FillAsync("20");

        await Page.GetByTestId("boxDesc_").Locator("input").FillAsync("Best box description");

        await Page.GetByTestId("boxImg_").Locator("input").FillAsync("img url");

        //await Page.WaitForTimeoutAsync(5000);

       await Page.GetByText("Update Box").ClickAsync();
       await Page.WaitForTimeoutAsync(3000);
       
       //await Page.GetByRole(AriaRole.Button, new() { Name = "Update Box" }).ClickAsync();
       

       // make expectations
       
        await Expect(Page.GetByTestId("card_ "+str)).ToBeVisibleAsync();
        await Expect(Page.GetByTestId("card_ " + boxName)).ToBeHiddenAsync();
    }
    
    
}