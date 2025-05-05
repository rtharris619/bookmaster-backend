using Bookmaster.Common.Features.Messaging;

namespace Bookmaster.Modules.Books.Features.Library.AddTagsToLibraryEntry;

public sealed record AddTagsToLibraryEntryCommand(Guid LibraryEntryId, string[]? Tags) : ICommand;
