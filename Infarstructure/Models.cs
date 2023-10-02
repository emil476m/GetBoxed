using System.ComponentModel.DataAnnotations;

namespace Infarstructure;

public class Models
{}

public class BoxFeed
{
    [Range(-1, int.MaxValue)]
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
    [Range(-1, int.MaxValue)]
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