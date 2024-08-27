using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace PdCdpDemoPayment.Models;

public class Claim
{
    [BsonId(IdGenerator = typeof(ObjectIdGenerator))]
    [property: JsonIgnore(Condition = JsonIgnoreCondition.Always)]
    public ObjectId Id { get; init; } = default!;

    public string ClaimId { get; set; } = default!;

    public string Name { get; set; } = default!;
}
