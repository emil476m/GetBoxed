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
            $@"INSERT INTO getboxed.box (name, size, description, price, boxImgUrl, isDeleted) VALUES(@name, @size, @description,@price, @boxImgUrl, false) RETURNING 
        boxid as {nameof(Box.boxId)}, 
        name as {nameof(Box.name)},
        size as {nameof(Box.size)}, 
        description as {nameof(Box.description)}, 
        price as {nameof(Box.price)},
        boximgurl as {nameof(Box.boxImgUrl)}, 
        isDeleted as {nameof(Box.isDeleted)};";

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
        boximgurl as {nameof(BoxFeed.boxImgUrl)} FROM getboxed.box WHERE isDeleted = false";

        using (var conn = _dataSource.OpenConnection())
        {
            return conn.Query<BoxFeed>(sql);
        }
    }

    public bool DeleteBox(int boxId)
    {
        var sql = @$"UPDATE getboxed.box SET isDeleted = true WHERE boxid = @boxId";

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
            
            WHERE name LIKE @searchTerm AND isDeleted = false
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

    boxid as {nameof(Box.boxId)}, 
            name as {nameof(Box.name)},
            size as {nameof(Box.size)},
            description as {nameof(Box.description)}, 
            price as {nameof(Box.price)}, 
            boximgurl as {nameof(Box.boxImgUrl)};";

        using (var conn = _dataSource.OpenConnection())
        {
            return conn.QueryFirst<Box>(sql, new { boxId, name, size, description, price, boxImgUrl });
        }
    }
}