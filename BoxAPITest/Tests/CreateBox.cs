using System.Net.Http.Json;
using FluentAssertions;
using FluentAssertions.Execution;
using Infarstructure;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Utilities;
using Newtonsoft.Json;
using NUnit.Framework;
using Service;

namespace BoxAPITest;

public class CreateBox
{
    
    private HttpClient _httpClient;
    [SetUp]
    public void Setup()
    {
        _httpClient = new HttpClient();
    }

    [Test]
    public async Task ShouldSuccessfullyCreateBox()
    {
        Helper.TriggerRebuild();
        var box = new Box()
        {
            boxId = 1,
            name = "testingAPI",
            size = "200x200x200mm",
    
            description = "its a box",
    
            price = 0,
    
            boxImgUrl = "picture of a box",
            
            isDeleted = false,
        };

        var url = "http://localhost:5000/box";
        
        HttpResponseMessage response;
        
        try
        {
            response = await _httpClient.PostAsJsonAsync(url, box);
            TestContext.WriteLine("THE FULL BODY RESPONSE: " + await response.Content.ReadAsStringAsync());
        }
        catch (Exception e)
        {
            throw new Exception(Helper.NoResponseMessage, e);
        }
        
        Box responseObject;
        try
        {
            responseObject = JsonConvert.DeserializeObject<Box>(
                await response.Content.ReadAsStringAsync()) ?? throw new InvalidOperationException();
        }
        catch (Exception e)
        {
            throw new Exception(Helper.BadResponseBody(await response.Content.ReadAsStringAsync()), e); 
        }
        
        using (new AssertionScope())
        {
            (await Helper.IsCorsFullyEnabledAsync(url)).Should().BeTrue();
            response.IsSuccessStatusCode.Should().BeTrue();
            responseObject.Should().BeEquivalentTo(box, Helper.MyBecause(responseObject, box));
        }
    }
    
    
    [TestCase("M", "Mock size", "Author who doesn't exist", 4, "url")]
    [TestCase("data validation for that a name must be less than 100 caracters... this is gonne be a long line " +
              "12345678910112131415161718192021222324252627282930313233343536373839404142434445464748495051525354555" +
              "657585960616263646566676869707172737475767778798081828384858687888990", "min length 6", "Rob this is a description", 3, "url")]
    [TestCase("asdlkjsadlksajdlksajdlksajdlksadjldskajasdkl", "Mock test for max size 123456789101121314151617181920212223242526272829303132333435363738394041424344454647484950", "Rob", 2, "url")]
    [TestCase("Mock name", "M", "description", 1, null)]
    [TestCase("Mock name", "Mock size", "A", 4, "url")]
    [TestCase("mock name", "Mock size", "Rob", -1, "url")]
    public async Task ShouldFailDueToDataValidation(string name, string size, string description, float price, string boxImgUrl)
    {
        var article = new Box()
        {
            name = name,
            size = size,
            description = description,
            boxId = 1,
            price = price,
            boxImgUrl = boxImgUrl,
            isDeleted = false
        };

        HttpResponseMessage response;
        try
        {
            response = await _httpClient.PostAsJsonAsync("http://localhost:5000/api/articles", article);
            TestContext.WriteLine("THE FULL BODY RESPONSE: " + await response.Content.ReadAsStringAsync());
        }
        catch (Exception e)
        {
            throw new Exception(Helper.NoResponseMessage, e);
        }
        
        response.IsSuccessStatusCode.Should().BeFalse();
    }
}