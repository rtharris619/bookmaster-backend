using System.Threading;
using Bookmaster.Common.Domain;
using Bookmaster.Common.Features.Messaging;
using Bookmaster.Modules.Books.Domain.Library;
using Bookmaster.Modules.Books.Domain.Tags;
using Bookmaster.Modules.Books.Features.Abstractions;

namespace Bookmaster.Modules.Books.Features.Library.AddTagsToLibraryEntry;

internal sealed class AddTagsToLibraryEntryCommandHandler(
    ILibraryEntryRepository libraryEntryRepository,
    ITagRepository tagRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<AddTagsToLibraryEntryCommand>
{
    public async Task<Result> Handle(AddTagsToLibraryEntryCommand request, CancellationToken cancellationToken)
    {
        LibraryEntry? libraryEntry = await libraryEntryRepository.GetAsync(request.LibraryEntryId, cancellationToken);

        if (libraryEntry is null)
        {
            return Result.Failure(LibraryEntryErrors.NotFound(request.LibraryEntryId));
        }

        string[]? tagsToAdd = request.Tags;

        if (tagsToAdd is null)
        {
            libraryEntry.RemoveAllTags();
        }
        else
        {
            List<Tag> tags = await GetLibraryEntryTags(tagsToAdd, tagRepository);
            libraryEntry.AddTags(tags);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    private async Task<List<Tag>> GetLibraryEntryTags(string[] tagNames, ITagRepository tagRepository)
    {
        List<Tag> tags = [];

        List<Tag> databaseTags = await tagRepository.GetTagsByNameListAsync(tagNames);

        foreach (string tagName in tagNames)
        {
            Tag? tag = databaseTags.Find(t => t.Name.ToLower().Equals(tagName.ToLower()));
            if (tag is null)
            {
                tag = Tag.Create(tagName);
                tagRepository.Insert(tag);
            }
            tags.Add(tag);
        }

        return tags;
    }
}
