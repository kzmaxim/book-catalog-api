using AutoMapper;
using BookCatalogApi.DTOs;
using BookCatalogApi.Entities;
using BookCatalogApi.Repositories;
using BookCatalogApi.Wrappers;
using Microsoft.AspNetCore.Mvc;

namespace BookCatalogApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly IBookRepository _repository;
    private readonly IMapper _mapper;

    public BooksController(IBookRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<ReturnResult<IEnumerable<BookDto>>>> GetAll()
    {
        var books = await _repository.GetAllAsync();
        var dtos = _mapper.Map<IEnumerable<BookDto>>(books);
        return Ok(ReturnResult<IEnumerable<BookDto>>.Success(dtos));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ReturnResult<BookDto>>> GetById(int id)
    {
        var book = await _repository.GetByIdAsync(id);
        if (book == null)
            return NotFound(ReturnResult<BookDto>.Failure($"Book with ID {id} not found."));

        var dto = _mapper.Map<BookDto>(book);
        return Ok(ReturnResult<BookDto>.Success(dto));
    }

    [HttpGet("search")]
    public async Task<ActionResult<ReturnResult<IEnumerable<BookDto>>>> GetByAuthor([FromQuery] string author)
    {
        var books = await _repository.GetByAuthorAsync(author);
        var dtos = _mapper.Map<IEnumerable<BookDto>>(books);
        return Ok(ReturnResult<IEnumerable<BookDto>>.Success(dtos));
    }

    [HttpPost]
    public async Task<ActionResult<ReturnResult<BookDto>>> Create(BookCreateUpdateDto dto)
    {
        var bookEntity = _mapper.Map<Book>(dto);
        await _repository.AddAsync(bookEntity);
        
        var createdDto = _mapper.Map<BookDto>(bookEntity);
        return Ok(ReturnResult<BookDto>.Success(createdDto));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ReturnResult<BookDto>>> Update(int id, BookCreateUpdateDto dto)
    {
        var existingBook = await _repository.GetByIdAsync(id);
        if (existingBook == null)
            return NotFound(ReturnResult<BookDto>.Failure($"Book with ID {id} not found."));
        
        _mapper.Map(dto, existingBook);
        await _repository.UpdateAsync(existingBook);

        var updatedDto = _mapper.Map<BookDto>(existingBook);
        return Ok(ReturnResult<BookDto>.Success(updatedDto));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ReturnResult<bool>>> Delete(int id)
    {
        var book = await _repository.GetByIdAsync(id);
        if (book == null)
            return NotFound(ReturnResult<bool>.Failure($"Book with ID {id} not found."));

        await _repository.DeleteAsync(book);
        return Ok(ReturnResult<bool>.Success(true));
    }
}