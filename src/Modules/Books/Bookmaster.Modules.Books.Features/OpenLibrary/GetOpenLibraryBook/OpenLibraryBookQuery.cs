using Bookmaster.Common.Features.Messaging;

namespace Bookmaster.Modules.Books.Features.OpenLibrary.GetOpenLibraryBook;

public sealed record OpenLibraryBookQuery(string BookKey) : IQuery<OpenLibraryBookResponse>;
