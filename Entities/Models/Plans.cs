using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Entities.Models;

public class Plans
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("Name")]
    public string PlanName { set; get; }
    public List<string> ToppingsId { set; get; }
    public string TotalCoverage { set; get; }


}