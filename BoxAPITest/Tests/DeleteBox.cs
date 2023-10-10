using Dapper;
using FluentAssertions;
using FluentAssertions.Execution;
using Infarstructure;
using NUnit.Framework;
using Service;

namespace BoxAPITest;

public class DeleteBox
{
    private HttpClient _httpClient;

    [SetUp]
    public void Setup()
    {
        _httpClient = new HttpClient();
    }
    
    [Test]
    public async Task deleteBoxTest()
    {
        Helper.TriggerRebuild();

        var box = new Box()
        {
            boxId = 1,
            name = "hello world",
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

        var url = "http://localhost:5000/box/1";
        HttpResponseMessage response;
        try
        {
            response = await _httpClient.DeleteAsync(url);
            TestContext.WriteLine("THE FULL BODY RESPONSE: " + await response.Content.ReadAsStringAsync());

        }
        catch (Exception e)
        {
            throw new Exception(Helper.NoResponseMessage, e);
        }

        using (new AssertionScope())
        {
            using (var conn = Helper.DataSource.OpenConnection())
            {
                (conn.ExecuteScalar<int>($"SELECT COUNT(*) FROM getboxed.box WHERE boxid = 1 AND isDeleted = false;") == 0)
                    .Should()
                    .BeTrue();
            }
            (await Helper.IsCorsFullyEnabledAsync(url)).Should().BeTrue();
            response.IsSuccessStatusCode.Should().BeTrue();
        }
    }
}