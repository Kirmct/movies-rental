using MongoDB.Bson.Serialization.Attributes;

namespace MoviesRental.Query.Domain.Models;
public class Director
{
    [BsonId]
    public string Id { get; set; }
    public string FullName { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime DeletedAt { get; set; }
}
