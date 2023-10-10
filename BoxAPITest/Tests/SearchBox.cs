using Bogus;
using Dapper;
using FluentAssertions;
using FluentAssertions.Execution;
using Infarstructure;
using Newtonsoft.Json;
using NUnit.Framework;
using Service;

namespace BoxAPITest;

public class SearchBox
{
    private HttpClient _httpClient;

    [SetUp]
    public void Setup()
    {
        _httpClient = new HttpClient();
    }


    [TestCase("qqq asd", 5, 5)]
    [TestCase("dsklfj", 5, 0)]
    public async Task SuccessfullBoxSearch(string searchterm, int pageSize, int resultSize)
    {
        Helper.TriggerRebuild();
        var expected = new List<object>();
        for (var i = 1; i < 10; i++)
        {
            var box = new Box()
            {
                boxId = i,
                name = "asdasdl qqq asdlkjasdlk",
                size = new Faker().Random.Words(2),
                description = new Faker().Random.Words(10),
                price = 1,
                boxImgUrl = new Faker().Random.Word(),
                isDeleted = false
            };
            expected.Add(box);
            var sql = $@" 
            insert into getboxed.box (name, size, description, price, boxImgUrl, isDeleted) VALUES(@name, @size, @description,@price, @boxImgUrl, @IsDeleted);
            ";
            using (var conn = Helper.DataSource.OpenConnection())
            {
                conn.Execute(sql, box);
            }
        }

        var url = $"http://localhost:5000/box/Search?searchTerm={searchterm}&amount={pageSize}";
        
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

        var content = await response.Content.ReadAsStringAsync();
        IEnumerable<BoxFeed> boxes;
        try
        {
            boxes = JsonConvert.DeserializeObject<IEnumerable<BoxFeed>>(content) ??
                       throw new InvalidOperationException();
        }
        catch (Exception e)
        {
            throw new Exception(Helper.NoResponseMessage, e);
        }

        using (new AssertionScope())
        {
            boxes.Count().Should().Be(resultSize);
            response.IsSuccessStatusCode.Should().BeTrue();
            (await Helper.IsCorsFullyEnabledAsync(url)).Should().BeTrue();
        }
    }

    [TestCase("qq", 5)]
    [TestCase("dsklfj", -5)]
    public async Task BoxSearchFailBecauseOfDataValidation(string searchterm, int pageSize)
    {
        HttpResponseMessage response;
        try
        {
            response = await _httpClient.GetAsync(
                $"http://localhost:5000/box/Search?searchTerm={searchterm}&amount={pageSize}");
            TestContext.WriteLine("THE FULL BODY RESPONSE: " + await response.Content.ReadAsStringAsync());
        }
        catch (Exception e)
        {
            throw new Exception(Helper.NoResponseMessage, e);
        }

        using (new AssertionScope())
        {
            response.IsSuccessStatusCode.Should().BeFalse();
        }
    }
}