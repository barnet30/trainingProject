using Microsoft.Extensions.Options;
using MongoDB.Driver;
using trainingProject.Abstractions;
using trainingProject.Abstractions.Models;
using trainingProject.Abstractions.Settings;

namespace trainingProject.Data;

public class BookRepository : IBookRepository
{
    private readonly IMongoCollection<Book> _booksCollection;
    private readonly IMongoCollection<Counter> _counterCollection;
    private readonly string _booksCollectionName;

    public BookRepository(IOptions<MongoSettings> mongoSettings)
    {
        var mongoClient = new MongoClient(mongoSettings.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(mongoSettings.Value.Database);

        _booksCollectionName = mongoSettings.Value.BooksCollectionName;
        _booksCollection = mongoDatabase.GetCollection<Book>(_booksCollectionName);
        _counterCollection = mongoDatabase.GetCollection<Counter>("counters");
    }

    public async Task<List<Book>> GetAllAsync()
    {
        return await _booksCollection.Find(_ => true).ToListAsync();
    }

    public async Task<Book> GetByIdAsync(long id)
    {
        return await _booksCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task CreateAsync(Book book)
    {
        try
        {
            var newId = await GetNextSequenceValue(_booksCollectionName);
            book.Id = newId;
            book.BookName += $"{newId}";
            await _booksCollection.InsertOneAsync(book);
        }
        catch (Exception e)
        {
            throw;
        }
    }

    public Task UpdateAsync(long id, Book updatedBook)
    {
        throw new NotImplementedException();
    }

    public Task RemoveAsync(long id)
    {
        throw new NotImplementedException();
    }

    private async Task<int> GetNextSequenceValue(string collectionName)
    {
        var filter = Builders<Counter>.Filter.Eq(x => x.Id, collectionName);
        var updated = Builders<Counter>.Update.Inc(x => x.Seq, 1);
        
        var options = new FindOneAndUpdateOptions<Counter>
        {
            IsUpsert = true,
            ReturnDocument = ReturnDocument.After
        };

        var lastCounter = await _counterCollection.FindOneAndUpdateAsync(filter, updated, options);
        return lastCounter.Seq;
    }
}