using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Entities.Models;

public class Vehicles
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string IdUser { get; set; }
    public string Type { set; get; }
    public string Plaque { set; get; }
    public int Model { set; get; }
    public bool Inspection { set; get; }
}