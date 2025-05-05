namespace Bookmaster.Modules.Books.Domain.Tags;

public interface ITagRepository
{
    Task<List<Tag>> GetTagsByNameListAsync(string[] tags, CancellationToken cancellationToken = default);

    void Insert(Tag tag);
}
