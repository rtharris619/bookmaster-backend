using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookmaster.Common.Domain;
using Bookmaster.Common.Presentation.Results;
using Bookmaster.Common.Presentation.Endpoints;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Bookmaster.Modules.Books.Features.GoogleBooks.GoogleBookSearch;
using Bookmaster.Common.Features.Messaging;

namespace Bookmaster.Modules.Books.Presentation.GoogleBooks;

internal sealed class GoogleBookSearch : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapGet(Endpoints.GoogleBooks + "/search", async (
            IQueryHandler<GoogleBookSearchQuery, GoogleBookSearchResponse> handler,
            CancellationToken cancellationToken,
            string q,
            string printType = "books",
            string projection = GoogleBookSearchProjection.Full,
            int startIndex = 0,
            int maxResults = 10) =>
        {
            Result<GoogleBookSearchResponse>? result = await handler.Handle(
                new GoogleBookSearchQuery(q, printType, projection, startIndex, maxResults),
                cancellationToken);

            if (result is null)
            {
                return Results.Problem(detail: "Error in retrieving books from Google Books API.");
            }            

            return result.Match(Results.Ok, ApiResults.Problem);
        });
    }
}
