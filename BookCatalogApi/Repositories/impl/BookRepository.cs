using BookCatalogApi.Data;
using BookCatalogApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookCatalogApi.Repositories.impl;

public class BookRepository : IBookRepository
{
    private readonly AppDbContext _context;

    public BookRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Book>> GetAllAsync() => await _context.Books.ToListAsync();
    
    public async Task<Book?> GetByIdAsync(int id) => await _context.Books.FindAsync(id);
    
    public async Task<IEnumerable<Book>> GetByAuthorAsync(string author) => 
        await _context.Books.Where(b => b.Author.Contains(author)).ToListAsync();

    public async Task AddAsync(Book book)
    {
        await _context.Books.AddAsync(book);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Book book)
    {
        _context.Books.Update(book);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Book book)
    {
        _context.Books.Remove(book);
        await _context.SaveChangesAsync();
    }
}