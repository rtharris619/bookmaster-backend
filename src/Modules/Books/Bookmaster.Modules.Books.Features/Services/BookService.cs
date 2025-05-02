using Bookmaster.Modules.Books.Domain.Books;

namespace Bookmaster.Modules.Books.Features.Services;

public sealed class BookService : IBookService
{
    public async Task<List<Author>> GetAuthors(string[] googleBookAuthors, IAuthorRepository authorRepository)
    {
        List<Author> authors = [];

        foreach (string googleBookAuthor in googleBookAuthors)
        {
            Author? author = await authorRepository.GetByNameAsync(googleBookAuthor);
            if (author is null)
            {
                author = Author.Create(googleBookAuthor);
                authorRepository.Insert(author);
            }
            authors.Add(author);
        }

        return authors;
    }

    public async Task<List<BookCategory>> GetBookCategories(string[] googleBookCategories, IBookCategoryRepository bookCategoryRepository)
    {
        List<BookCategory> categories = [];
        foreach (string googleBookCategory in googleBookCategories)
        {
            BookCategory? category = await bookCategoryRepository.GetByNameAsync(googleBookCategory);
            if (category is null)
            {
                category = BookCategory.Create(googleBookCategory);
                bookCategoryRepository.Insert(category);
            }
            categories.Add(category);
        }
        return categories;
    }
}
