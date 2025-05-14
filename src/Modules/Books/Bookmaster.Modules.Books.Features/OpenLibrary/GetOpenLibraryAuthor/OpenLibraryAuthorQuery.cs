using Bookmaster.Common.Features.Messaging;

namespace Bookmaster.Modules.Books.Features.OpenLibrary.GetOpenLibraryAuthor;

public sealed record OpenLibraryAuthorQuery(string AuthorKey)
    : IQuery<OpenLibraryAuthorResponse>;
