using Bookmaster.Modules.Books.Domain.Tags;
using Bookmaster.Modules.Books.Infrastructure.Database;

namespace Bookmaster.Modules.Books.Infrastructure.Tags;

internal sealed class TagRepository(BooksDbContext context) : ITagRepository
{
    public void Insert(Tag tag)
    {
        context.Tags.Add(tag);
    }
}
