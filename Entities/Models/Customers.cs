using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace Entities.Models;

public class Customers
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    
    public string Name { set; get; }
    public int Identification { set; get; }
    public string City { set; get; }
    public string Address { set; get; }
    
    
}