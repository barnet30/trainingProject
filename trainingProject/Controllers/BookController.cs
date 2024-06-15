using Microsoft.AspNetCore.Mvc;
using trainingProject.Abstractions;
using trainingProject.Abstractions.Models;

namespace trainingProject.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookController
{
    private readonly IBookRepository _bookRepository;

    public BookController(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    [HttpGet]
    public async Task<List<Book>> GetAll()
    {
        return await _bookRepository.GetAllAsync();
    } 
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(long id)
    {
        var res = await _bookRepository.GetByIdAsync(id);
        if (res == null)
            return new NotFoundResult();
        else
            return new ObjectResult(res);
    } 
    
    [HttpPost]
    public async Task<IActionResult> Insert(Book newBook)
    {
        try
        {
            if (newBook.Id != default)
                return new BadRequestResult();
            await _bookRepository.CreateAsync(newBook);

            return new OkResult();
        }
        catch (Exception e)
        {
            return new ObjectResult(new
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = e.Message
            });
        }
    } 
}