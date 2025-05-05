using Bookmaster.Modules.Books.Domain.Tags;
using Bookmaster.Modules.Books.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Bookmaster.Modules.Books.Infrastructure.Tags;

internal sealed class TagRepository(BooksDbContext context) : ITagRepository
{
    public async Task<List<Tag>> GetTagsByNameListAsync(string[] tags, CancellationToken cancellationToken = default)
    {
        string[] lowercaseTags = [.. tags.Select(tag => tag.ToLower())];

        return await context.Tags
            .Where(tag => lowercaseTags.Contains(tag.Name.ToLower()))
            .ToListAsync(cancellationToken);
    }

    public void Insert(Tag tag)
    {
        context.Tags.Add(tag);
    }
}
