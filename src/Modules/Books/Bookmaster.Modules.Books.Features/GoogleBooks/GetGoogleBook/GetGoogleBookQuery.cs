using Bookmaster.Common.Features.Messaging;
using Bookmaster.Modules.Books.Features.GoogleBooks.GoogleBookSearch;

namespace Bookmaster.Modules.Books.Features.GoogleBooks.GetGoogleBook;

public sealed record GetGoogleBookQuery(string GoogleBookId)
    : IQuery<GoogleBookSearchResponseItem>;
