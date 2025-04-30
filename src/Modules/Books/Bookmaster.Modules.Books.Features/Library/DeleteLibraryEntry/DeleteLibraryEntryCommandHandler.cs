using Bookmaster.Common.Domain;
using Bookmaster.Common.Features.Messaging;
using Bookmaster.Modules.Books.Domain.Library;
using Bookmaster.Modules.Books.Features.Abstractions;

namespace Bookmaster.Modules.Books.Features.Library.DeleteLibraryEntry;

internal sealed class DeleteLibraryEntryCommandHandler(
    ILibraryEntryRepository libraryEntryRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteLibraryEntryCommand>
{
    public async Task<Result> Handle(DeleteLibraryEntryCommand request, CancellationToken cancellationToken)
    {
        LibraryEntry? libraryEntry = await libraryEntryRepository.GetAsync(request.Id, cancellationToken);

        if (libraryEntry is null)
        {
            return Result.Failure(LibraryEntryErrors.NotFound(request.Id));
        }

        libraryEntryRepository.Delete(libraryEntry);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
