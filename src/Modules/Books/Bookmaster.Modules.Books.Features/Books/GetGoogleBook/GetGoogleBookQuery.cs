using Bookmaster.Common.Features.Messaging;
using Bookmaster.Modules.Books.Features.Books.GoogleBookSearch;

namespace Bookmaster.Modules.Books.Features.Books.GetGoogleBook;

public sealed record GetGoogleBookQuery(string GoogleBookId)
    : IQuery<GoogleBookSearchResponseItem>;
