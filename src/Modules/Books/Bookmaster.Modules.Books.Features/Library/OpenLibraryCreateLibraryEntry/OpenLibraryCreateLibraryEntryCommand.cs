using Bookmaster.Common.Features.Messaging;

namespace Bookmaster.Modules.Books.Features.Library.OpenLibraryCreateLibraryEntry;

public sealed record OpenLibraryCreateLibraryEntryCommand(string OpenLibraryWorkKey) : ICommand<Guid>;
