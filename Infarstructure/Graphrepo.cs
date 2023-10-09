using Dapper;
using Npgsql;

namespace Infarstructure;

public class Graphrepo
{
    private readonly NpgsqlDataSource _dataSource;

    public Graphrepo(NpgsqlDataSource dataSource)
    {
        _dataSource = dataSource;
    }

    public List<int> getAllBoxes()
    {
        var sql = $@"SELECT boxid FROM getboxed.box";

        using (var conn = _dataSource.OpenConnection())
        {
            return (List<int>)conn.Query<int>(sql);
        }
    }

    public List<int> getORdersInASpecifikMonth(int month)
    {
        var sql = $@"SELECT orderid FROM getboxed.orderlist WHERE EXTRACT(MONTH FROM orderdate) = @month";

        using (var conn = _dataSource.OpenConnection())
        {
            return (List<int>)conn.Query<int>(sql, new {month});
        }
    }

    public int getDataToBoxes( int month, List<int> orderIds)
    {
        
        var sql1 = $@"SELECT boxamount as {nameof(Orders.amount)},
       boxid as {nameof(Orders.boxId)} FROM getboxed.boxorder WHERE orderid = @orderId && boxid = @boxid";
        
        
        throw new NotImplementedException();
    }
}