using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace Infarstructure;

public class Models
{}

public class BoxFeed
{
    public int boxId { get; set; }
    
    [MinLength(3)]
    [MaxLength(100)]
    public string name { get; set; }
    
    [MinLength(6)]
    [MaxLength(50)]
    public string size { get; set; }
    
    [Range(0.0, float.MaxValue)]
    public float price { get; set; }
    
    public string boxImgUrl { get; set; }
}

public class Box
{
    public int boxId { get; set; }
    
    [MinLength(3)]
    [MaxLength(100)]
    public string name { get; set; }
    
    [MinLength(6)]
    [MaxLength(50)]
    public string size { get; set; }
    
    [MinLength(3)]
    [MaxLength(500)]
    public string description { get; set; }
    
    [Range(0.0, float.MaxValue)]
    public float price { get; set; }
    
    public string boxImgUrl { get; set; }
}

public class OrderFeed
{
    public int orderId { get; set; }
    public int customerId { get; set; }
    public float price { get; set; }
    public DateTime orderDate { get; set; }
    
}

public class Order
{
    public int orderId { get; set; }
    public int customerId { get; set; }
    public float totalPrice { get; set; }

    public DateTime orderDate { get; set; }
    public List<Orders> BoxOrder { get; set; }
}

public class Orders
{
    public int amount { get; set; }
    public int boxId { get; set; }
}
