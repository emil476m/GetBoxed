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
            return (List<int>)conn.Query<int>(sql, new { month });
        }
    }

    public int getDataToBoxes(List<int> orderIds, int boxId)
    {
        var sql1 = $@"SELECT boxamount FROM getboxed.boxorder WHERE orderid = @orderId AND boxid = @boxId";

        using (var conn = _dataSource.OpenConnection())
        {
            int boxesSoldPrMonth = 0;

            foreach (var orderId in orderIds)
            {
                List<int> amounts = (List<int>)conn.Query<int>(sql1, param: new { orderId, boxId});
                foreach (var i in amounts)
                {
                    boxesSoldPrMonth += i;
                }
            }

            return boxesSoldPrMonth;
        }
    }
    
    public string getboxname(int boxId)
    {
        var sql2 = $@"SELECT name FROM getboxed.box WHERE boxid = @boxid";

        using (var conn = _dataSource.OpenConnection())
        {
            return conn.QuerySingle<string>(sql2, new { boxId });
        }
    }
}