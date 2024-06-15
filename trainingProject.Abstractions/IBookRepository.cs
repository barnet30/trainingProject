using trainingProject.Abstractions.Models;

namespace trainingProject.Abstractions;

public interface IBookRepository
{
    Task<List<Book>> GetAllAsync();
    Task<Book> GetByIdAsync(long id);
    Task CreateAsync(Book book);
    Task UpdateAsync(long id, Book updatedBook);
    Task RemoveAsync(long id);
}