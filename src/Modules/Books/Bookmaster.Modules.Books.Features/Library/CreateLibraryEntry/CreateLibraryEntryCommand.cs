using Bookmaster.Common.Features.Messaging;

namespace Bookmaster.Modules.Books.Features.Library.CreateLibraryEntry;

public sealed record CreateLibraryEntryCommand(string OpenLibraryWorkKey) : ICommand<Guid>;
