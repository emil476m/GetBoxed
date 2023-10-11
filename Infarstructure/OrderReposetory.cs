using Dapper;
using Npgsql;

namespace Infarstructure;

public class OrderReposetory
{
    
    private readonly NpgsqlDataSource _dataSource;

    public OrderReposetory(NpgsqlDataSource dataSource)
    {
        _dataSource = dataSource;
    }
    
     public IEnumerable<OrderFeed> getOrderFeed()
    {
        var sql1 =
            $@"SELECT orderid as {nameof(OrderFeed.orderId)},
            pricesum as {nameof(OrderFeed.price)},
            orderDate as {nameof(OrderFeed.orderDate)},            
            customerid as {nameof(OrderFeed.customerId)}
       FROM getboxed.orderlist; ";

        using (var conn = _dataSource.OpenConnection())
        {
            return conn.Query<OrderFeed>(sql1) as List<OrderFeed>;
        }
    }

    public int CreateOrder(int orderCustomerId, DateTime orderDate, float orderTotalPrice, List<Orders> orderBoxOrder)//boxId, qty)
    {
        var sql =
            $@"INSERT INTO getboxed.orderlist (customerid, pricesum, orderDate) VALUES(@orderCustomerId, @orderTotalPrice, @orderDate) RETURNING orderid; ";

        var sql2 =
            $@"INSERT INTO getboxed.boxorder (orderid,boxid,boxamount) VALUES(@orderId, @orderBoxId,@OrderboxAmount )";

        using (var conn = _dataSource.OpenConnection())
        {
            var transaction = conn.BeginTransaction();
            try
            {
                int orderId = conn.QueryFirst<int>(sql, new { orderCustomerId, orderTotalPrice, orderDate});

                foreach (var item in orderBoxOrder)
                {
                    conn.Query(sql2, new { orderId, orderBoxId = item.boxId, OrderboxAmount = item.amount });
                }

                transaction.Commit();
                return orderId;
            }
            catch (Exception e)
            {
                transaction.Rollback();
                throw e;
            }
        }
    }

    public Order GetOrderById(int orderId)
    {
        var sql1 =
            $@"SELECT orderid as {nameof(Order.orderId)},
            pricesum as {nameof(Order.totalPrice)},
            orderDate as {nameof(Order.orderDate)},
            customerid as {nameof(Order.customerId)}
       FROM getboxed.orderlist WHERE orderid = @orderId ";

        var sql2 =
            $@"SELECT boxamount as {nameof(Orders.amount)},
       boxid as {nameof(Orders.boxId)} FROM getboxed.boxorder WHERE orderid = @orderId";


        using (var conn = _dataSource.OpenConnection())
        {
            var transaction = conn.BeginTransaction();
            try
            {
                Order confirmedOrder = conn.QueryFirst<Order>(sql1, new { orderId });

                confirmedOrder.BoxOrder = conn.Query<Orders>(sql2, new { orderId }) as List<Orders>;

                transaction.Commit();
                return confirmedOrder;
            }
            catch (Exception e)
            {
                transaction.Rollback();
                throw e;
            }
        }
    }

    public IEnumerable<Order> GetOrderByCustoemrId(int customerId)
    {
        var sql1 =
            $@"SELECT orderid as {nameof(Order.orderId)},
            pricesum as {nameof(Order.totalPrice)},
            orderDate as {nameof(Order.orderDate)},
            orderDate as {nameof(Order.orderDate)},
            customerid as {nameof(Order.customerId)}
       FROM getboxed.orderlist WHERE customerid = @customerId ";

        var sql2 =
            $@"SELECT boxamount as {nameof(Orders.amount)},
       boxid as {nameof(Orders.boxId)} FROM getboxed.boxorder WHERE orderid = @orderId";

        List<Order> customersOrders = new List<Order>();

        using (var conn = _dataSource.OpenConnection())
        {
            var transaction = conn.BeginTransaction();
            try
            {
                customersOrders = conn.Query<Order>(sql1, new { customerId }) as List<Order>;

                foreach (var order in customersOrders)
                {
                    order.BoxOrder = conn.Query<Orders>(sql2, new { orderId = order.orderId }) as List<Orders>;
                }

                transaction.Commit();
                return customersOrders;
            }
            catch (Exception e)
            {
                transaction.Rollback();
                throw e;
            }
        }
    }
    
    public Customer CreateCustomer(string name, string mail, string tlf, string address)
    {
        var sql =
            $@"INSERT INTO getboxed.customer (name, mail, tlf, address) VALUES(@name, @mail, @tlf,@address) RETURNING 
        customerId as {nameof(Customer.customerId)}, 
        name as {nameof(Customer.name)},
        mail as {nameof(Customer.mail)}, 
        tlf as {nameof(Customer.tlf)}, 
        address as {nameof(Customer.address)};";

        using (var conn = _dataSource.OpenConnection())
        {
            return conn.QueryFirst<Customer>(sql, new { name, mail, tlf, address});
        }
    }
}