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
    
    public Box CreateBox(string name, string size, string description, float price, string articleImgUrl)
    {
        var sql = $@"INSERT INTO box (name, size, description, price, articleimgurl) VALUES(@name, @size, @description,@price, @articleImgUrl) RETURNING 
        boxid as {nameof(Box.boxId)}, 
        name as {nameof(Box.name)},
        size as {nameof(Box.size)}, 
        description as {nameof(Box.description)}, 
        price as {nameof(Box.price)}, 
        articleimgurl as {nameof(Box.boxImgUrl)};";
        
        using (var conn = _dataSource.OpenConnection())
        {
            return conn.QueryFirst<Box>(sql, new {name, size, description, price, articleImgUrl});
        }
    }
}