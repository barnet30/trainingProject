using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using trainingProject.Common.Enums;

namespace trainingProject.Abstractions.Models;

public class Book
{
    [BsonId]
    [BsonRepresentation(BsonType.Int64)]
    public long Id { get; set; }
    
    [BsonElement("bookName")]
    public string BookName { get; set; }
    
    [BsonElement("auhtor")]
    public string Author { get; set; }
    
    [BsonElement("price")]
    public double Price { get; set; }
    
    [BsonElement("genres")]
    public Genre[] Genre { get; set; }
}