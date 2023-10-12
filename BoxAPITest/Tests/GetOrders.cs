using Dapper;

namespace BoxAPITest;

using System.Net.Http.Json;
using FluentAssertions;
using FluentAssertions.Execution;
using Infarstructure;
using Newtonsoft.Json;
using NUnit.Framework;
using Service;

public class GetOrders
{
    private HttpClient _httpClient;

    [SetUp]
    public void Setup()
    {
        _httpClient = new HttpClient();
    }

    [Test]
    public async Task getOredrs()
    {
        
        // resetting the database and adding boxes to the database for the test
        Helper.TriggerRebuildgetOrderTest();
        for (var i = 1; i < 10; i++)
        {
            var box = new Box()
            {
                name = "Hello World",
                size = "Mock size",
                description = "test description",
                boxId = i,
                price = i + 25 * 50 / 3,
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
        }
        
        // creates the orderlist used in the orders
        var orderlist = new List<Orders>();
        for (int i = 1; i < 10; i++)
        {
            var order = new Orders()
            {
                amount = i,
                boxId = i,
            };
            orderlist.Add(order);
        }

        // creates the expected result and orders in the database
        var expected = new List<Order>();
        for (int i = 1; i == 10; i++)
        {
            var order = new Order()
            {
                customerId = 12,
                BoxOrder = orderlist,
                totalPrice = orderlist[i].amount,
            };
            var sql =
                $@"INSERT INTO getboxed.orderlist (customerid, pricesum, orderDate) VALUES(@orderCustomerId, @orderTotalPrice, @orderDate) RETURNING orderid; ";

            var sql2 =
                $@"INSERT INTO getboxed.boxorder (orderid,boxid,boxamount) VALUES(@orderId, @orderBoxId,@OrderboxAmount )";


            using (var conn = Helper.DataSource.OpenConnection())
            {
                var transaction = conn.BeginTransaction();
                try
                {
                    int orderId =
                        conn.QueryFirst<int>(sql, new { orderCustomerId=order.customerId, orderTotalPrice = order.totalPrice, orderDate=order.orderDate });

                    foreach (var item in order.BoxOrder)
                    {
                        conn.Query(sql2, new { orderId, orderBoxId = item.boxId, OrderboxAmount = item.amount });
                    }

                    transaction.Commit();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw e;
                }
            }
        }
        
        var url = "http://localhost:5000/Order";
        
        
        // gets the response from the api
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
        
        IEnumerable<OrderFeed> orders;
        try
        {
            orders = JsonConvert.DeserializeObject<IEnumerable<OrderFeed>>(await response.Content.ReadAsStringAsync()) ??
                    throw new InvalidOperationException();
        }
        catch (Exception e)
        {
            throw new Exception(Helper.BadResponseBody(await response.Content.ReadAsStringAsync()), e);
        }
        
        // checks if the response matches the expected orders
        using (new AssertionScope())
        {
            int count = 0;
            foreach (var order in orders)
            {
                (await Helper.IsCorsFullyEnabledAsync(url)).Should().BeTrue();
                order.orderId.Should().BePositive();
                order.customerId.Should().BePositive();
                for (int i = 1; i == expected.Count+1; i++)
                {
                    expected[i].orderId.Should().BePositive().Should().BeEquivalentTo(order.orderId);
                }
            }
        }
        Helper.TriggerRebuild();
    }

}