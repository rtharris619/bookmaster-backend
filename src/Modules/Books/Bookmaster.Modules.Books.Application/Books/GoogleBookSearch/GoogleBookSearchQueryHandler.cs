using Bookmaster.Common.Domain;
using Bookmaster.Common.Features.Messaging;

namespace Bookmaster.Modules.Books.Features.Books.GoogleBookSearch;

internal sealed class GoogleBookSearchQueryHandler
    : IQueryHandler<GoogleBookSearchQuery, IReadOnlyCollection<GoogleBookSearchResponse>>
{
    public Task<Result<IReadOnlyCollection<GoogleBookSearchResponse>>> Handle(GoogleBookSearchQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
