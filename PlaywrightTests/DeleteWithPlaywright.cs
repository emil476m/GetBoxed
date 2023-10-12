using Dapper;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using Service;

namespace PlaywrightTests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]

// use to run codegen: .\bin\Debug\net8.0\playwright.ps1 codegen http://localhost:5000
public class DeleteWithPlaywright : PageTest
{
    [SetUp]
    public async Task SetUp()
    {
        Helper.TriggerRebuild();
        await Page.GotoAsync("http://localhost:5000/");
    }

    [Test]
    public async Task Dellete()
    {
        await Page.GotoAsync("http://localhost:5000/tabs/tabs/boxfeed");

        await Page.GetByTestId("detail_ best box").GetByRole(AriaRole.Button).ClickAsync();

        await Page.GetByTestId("editbtn_").GetByRole(AriaRole.Button).ClickAsync();

        await Page.GetByRole(AriaRole.Button, new() { Name = "Delete box" }).ClickAsync();

        await Page.GetByRole(AriaRole.Button, new() { Name = "yes" }).ClickAsync();

        using (new AssertionScope())
        {
            using (var conn = Helper.DataSource.OpenConnection())
            {
                (conn.ExecuteScalar<int>($"SELECT COUNT(*) FROM getboxed.box WHERE boxid = 1 AND isDeleted = false;") == 0)
                    .Should()
                    .BeTrue();
            }
        }
    }
}

