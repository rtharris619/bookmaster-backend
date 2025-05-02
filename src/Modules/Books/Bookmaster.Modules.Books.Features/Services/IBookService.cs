using Bookmaster.Modules.Books.Domain.Books;

namespace Bookmaster.Modules.Books.Features.Services;

public interface IBookService
{
    Task<List<Author>> GetAuthors(string[] googleBookAuthors, IAuthorRepository authorRepository);
    Task<List<BookCategory>> GetBookCategories(string[] googleBookCategories, IBookCategoryRepository bookCategoryRepository);
}
