namespace trainingProject.Abstractions.Settings;

public sealed class MongoSettings
{
    public string ConnectionString { get; set; }
    public string Database { get; set; }
    public string BooksCollectionName { get; set; }
}