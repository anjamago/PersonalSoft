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
    public string StartDate { set; get; }
    public string EndDate { set; get; }

    [BsonRepresentation(BsonType.ObjectId)]
    public string IdCliente { set; get; }

    [BsonRepresentation(BsonType.ObjectId)]
    public string IdVehicule { set; get; }

}   