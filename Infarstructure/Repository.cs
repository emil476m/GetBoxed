using Dapper;
using Npgsql;

namespace Infarstructure;

public class Repository
{
    private readonly NpgsqlDataSource _dataSource;

    public Repository(NpgsqlDataSource dataSource)
    {
        _dataSource = dataSource;
    }

    public Box CreateBox(string name, string size, string description, float price, string boxImgUrl)
    {
        var sql =
            $@"INSERT INTO getboxed.box (name, size, description, price, boxImgUrl) VALUES(@name, @size, @description,@price, @boxImgUrl) RETURNING 
        boxid as {nameof(Box.boxId)}, 
        name as {nameof(Box.name)},
        size as {nameof(Box.size)}, 
        description as {nameof(Box.description)}, 
        price as {nameof(Box.price)}, 
        boximgurl as {nameof(Box.boxImgUrl)};";

        using (var conn = _dataSource.OpenConnection())
        {
            return conn.QueryFirst<Box>(sql, new { name, size, description, price, boxImgUrl });
        }
    }

    public IEnumerable<BoxFeed> GetBoxFeed()
    {
        var sql = $@"SELECT 
        boxid as {nameof(BoxFeed.boxId)}, 
        name as {nameof(BoxFeed.name)},
        size as {nameof(BoxFeed.size)}, 
        price as {nameof(BoxFeed.price)}, 
        boximgurl as {nameof(BoxFeed.boxImgUrl)} FROM getboxed.box";

        using (var conn = _dataSource.OpenConnection())
        {
            return conn.Query<BoxFeed>(sql);
        }
    }

    public bool DeleteBox(int boxId)
    {
        var sql = $@"DELETE FROM getboxed.box WHERE boxid = (@boxId);";

        using (var conn = _dataSource.OpenConnection())
        {
            return conn.Execute(sql, new { boxId }) == 1;
        }
    }


    public Box getBoxById(int boxId)
    {
        var sql = $@"SELECT 
        boxid as {nameof(Box.boxId)}, 
        name as {nameof(Box.name)},
        size as {nameof(Box.size)}, 
        description as {nameof(Box.description)}, 
        price as {nameof(Box.price)}, 
        boximgurl as {nameof(Box.boxImgUrl)} FROM getboxed.box WHERE boxid = @boxId";

        using (var conn = _dataSource.OpenConnection())
        {
            return conn.QueryFirst<Box>(sql, new { boxId });
        }
    }

    public IEnumerable<BoxFeed> Search(string searchTerm, int amount)
    {
        var sql = $@"
            SELECT 
            boxid as {nameof(BoxFeed.boxId)}, 
            name as {nameof(BoxFeed.name)},
            size as {nameof(BoxFeed.size)}, 
            price as {nameof(BoxFeed.price)}, 
            boximgurl as {nameof(BoxFeed.boxImgUrl)} FROM getboxed.box
            
            WHERE name LIKE @searchTerm
            LIMIT @amount
            ;";

        using (var conn = _dataSource.OpenConnection())
        {
            return conn.Query<BoxFeed>(sql, new { searchTerm = "%" + searchTerm + "%", amount });
        }
    }

    public float GetBoxPrice(int boxId)
    {
        var sql = $@"
            SELECT 
            price as {nameof(BoxFeed.price)} FROM getboxed.box
            
             WHERE boxid = @boxId;"
            ;

        using (var conn = _dataSource.OpenConnection())
        {
            return conn.QueryFirst<float>(sql, new { boxId });
        }
    }

    public Box UpdateBox(int boxId, string name, string size, string description, float price, string boxImgUrl)
    {
        var sql = @$"
UPDATE getboxed.box SET name = @name, size = @size, description = @description, price = @price, boxImgUrl = @boxImgUrl WHERE boxid = @boxId
RETURNING 
    boxid as {nameof(BoxFeed.boxId)}, 
            name as {nameof(BoxFeed.name)},
            size as {nameof(BoxFeed.size)}, 
            price as {nameof(BoxFeed.price)}, 
            boximgurl as {nameof(BoxFeed.boxImgUrl)};";

        using (var conn = _dataSource.OpenConnection())
        {
            return conn.QueryFirst<Box>(sql, new { boxId, name, size, description, price, boxImgUrl });
        }
    }

    public IEnumerable<OrderFeed> getOrderFeed()
    {
        var sql1 =
            $@"SELECT orderid as {nameof(OrderFeed.orderId)},
            pricesum as {nameof(OrderFeed.price)},
            customerid as {nameof(OrderFeed.customerId)}
       FROM getboxed.orderlist; ";

        using (var conn = _dataSource.OpenConnection())
        {
            return conn.Query<OrderFeed>(sql1) as List<OrderFeed>;
        }
    }

    public int CreateOrder(int orderCustomerId, float orderTotalPrice, List<Orders> orderBoxOrder //boxId, qty
    )
    {
        var sql =
            $@"INSERT INTO getboxed.orderlist (customerid, pricesum) VALUES(@orderCustomerId, @orderTotalPrice) RETURNING orderid; ";

        var sql2 =
            $@"INSERT INTO getboxed.boxorder (orderid,boxid,boxamount) VALUES(@orderId, @orderBoxId,@OrderboxAmount )";

        using (var conn = _dataSource.OpenConnection())
        {
            var transaction = conn.BeginTransaction();
            try
            {
                int orderId = conn.QueryFirst<int>(sql, new { orderCustomerId, orderTotalPrice });

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
            $@"SELECT orderid as {nameof(Order.orderOId)},
            pricesum as {nameof(Order.totalPrice)},
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
            $@"SELECT orderid as {nameof(Order.orderOId)},
            pricesum as {nameof(Order.totalPrice)},
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
                    order.BoxOrder = conn.Query<Orders>(sql2, new { orderId = order.orderOId }) as List<Orders>;
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
}