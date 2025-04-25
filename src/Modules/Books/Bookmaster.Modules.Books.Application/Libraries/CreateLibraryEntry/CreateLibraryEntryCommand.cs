using Bookmaster.Common.Features.Messaging;

namespace Bookmaster.Modules.Books.Features.Libraries.CreateLibraryEntry;

public sealed record CreateLibraryEntryCommand(string GoogleBookId, Guid PersonId) : ICommand<Guid>;
