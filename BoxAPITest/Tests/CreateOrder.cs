namespace BoxAPITest;

using System.Net.Http.Json;
using FluentAssertions;
using FluentAssertions.Execution;
using Infarstructure;
using Newtonsoft.Json;
using NUnit.Framework;
using Service;

public class CreateOrder
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
        var order = new Order()
        {
            orderId = 1,
            customerId = 12,
            totalPrice = 50,

            orderDate = new DateTime(2023, 10, 1),

            BoxOrder = new List<Orders>()
        };
        
        Orders Order1 = new Orders();
        Order1.boxId = 101;
        Order1.amount = 1;
        
        order.BoxOrder.Add(Order1);

        var url = "http://localhost:5000/Order";

        HttpResponseMessage response;

        try
        {
            response = await _httpClient.PostAsJsonAsync(url, order);
            TestContext.WriteLine("THE FULL BODY RESPONSE: " + await response.Content.ReadAsStringAsync());
        }
        catch (Exception e)
        {
            throw new Exception(Helper.NoResponseMessage, e);
        }

        Order responseObject;
        try
        {
            responseObject = JsonConvert.DeserializeObject<Order>(
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
            responseObject.Should().BeEquivalentTo(order, Helper.MyBecause(responseObject, order));
        }
    }
}

