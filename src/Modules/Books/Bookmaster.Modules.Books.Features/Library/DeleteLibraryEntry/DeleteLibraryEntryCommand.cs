using Bookmaster.Common.Features.Messaging;

namespace Bookmaster.Modules.Books.Features.Library.DeleteLibraryEntry;

public sealed record DeleteLibraryEntryCommand(Guid Id) : ICommand;
