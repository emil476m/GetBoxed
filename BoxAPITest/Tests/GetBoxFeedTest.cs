using Dapper;
using FluentAssertions;
using FluentAssertions.Execution;
using Infarstructure;
using Newtonsoft.Json;
using NUnit.Framework;
using Service;

namespace BoxAPITest;

public class GetBoxFeed
{
    private HttpClient _httpClient;

    [SetUp]
    public void Setup()
    {
        _httpClient = new HttpClient();
    }
    
    [Test]
    public async Task GetBoxFeedTest()
    {
        Helper.TriggerRebuild();
        var expected = new List<object>();
        for (var i = 1; i < 10; i++)
        {
            var box = new Box()
            {
                name = "Hello World",
                size = "Mock size",
                description = "test description",
                boxId = i,
                price = i+25*50/3,
                boxImgUrl = "someurl",
                isDeleted = false
            };
            expected.Add(box);
            var sql = $@" 
            insert into getboxed.box (name, size, description, price, boxImgUrl, isDeleted) VALUES(@name, @size, @description,@price, @boxImgUrl, @isDeleted);
            ";
            using (var conn = Helper.DataSource.OpenConnection())
            {
                conn.Execute(sql, box);
            }
        }

        var url = "http://localhost:5000/box";
        HttpResponseMessage response;
        try
        {
            response = await _httpClient.GetAsync(url);
            TestContext.WriteLine("THE FULL BODY RESPONSE: " + await response.Content.ReadAsStringAsync());

        }
        catch (Exception e)
        {
            throw new Exception(Helper.NoResponseMessage, e);
        }


        IEnumerable<BoxFeed> boxes;
        try
        {
            boxes = JsonConvert.DeserializeObject<IEnumerable<BoxFeed>>(await response.Content.ReadAsStringAsync()) ??
                       throw new InvalidOperationException();
        }
        catch (Exception e)
        {
            throw new Exception(Helper.BadResponseBody(await response.Content.ReadAsStringAsync()), e);
        }

        using (new AssertionScope())
        {
            foreach (var box in boxes)
            {
                //If you want to be super strict, you can also check that the response.content does not include an author
                box.size.Length.Should().BeLessThan(51);
                response.IsSuccessStatusCode.Should().BeTrue();
                box.boxId.Should().BeGreaterThan(0);
                (await Helper.IsCorsFullyEnabledAsync(url)).Should().BeTrue();

            }
        }
    }
}