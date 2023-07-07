using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Entities.Models;

public class Policy
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string policyNumber { set; get; }
    public string CreateDate { set; get; }
    public string IdCliente { set; get; }
    public string IdVehicule { set; get; }

}