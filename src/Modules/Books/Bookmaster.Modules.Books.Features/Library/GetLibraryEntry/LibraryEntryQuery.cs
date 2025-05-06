using Bookmaster.Common.Features.Messaging;

namespace Bookmaster.Modules.Books.Features.Library.GetLibraryEntry;

public sealed record LibraryEntryQuery(Guid Id) : IQuery<LibraryEntryResponse>;
