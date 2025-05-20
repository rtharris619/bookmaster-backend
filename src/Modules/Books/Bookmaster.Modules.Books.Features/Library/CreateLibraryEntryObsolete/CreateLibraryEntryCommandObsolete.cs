using Bookmaster.Common.Features.Messaging;

namespace Bookmaster.Modules.Books.Features.Library.CreateLibraryEntryObsolete;

public sealed record CreateLibraryEntryCommandObsolete(string GoogleBookId, Guid PersonId) : ICommand<Guid>;
