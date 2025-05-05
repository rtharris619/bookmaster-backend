using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookmaster.Common.Domain;
using Bookmaster.Common.Presentation.Results;
using Bookmaster.Common.Presentation.Endpoints;
using Bookmaster.Modules.Books.Features.Books.GoogleBookSearch;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Bookmaster.Modules.Books.Presentation.Books;

internal sealed class GoogleBookSearch : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapGet(EndpointRoots.Books + "/search", async (
            ISender sender,
            string q,
            string printType = "books",
            string projection = GoogleBookSearchProjection.Full,
            int startIndex = 0,
            int maxResults = 10) =>
        {
            Result<GoogleBookSearchResponse>? result = await sender.Send(
                new GoogleBookSearchQuery(q, printType, projection, startIndex, maxResults));

            if (result is null)
            {
                return Results.Problem(detail: "Error in retrieving books from Google Books API.");
            }            

            return result.Match(Results.Ok, ApiResults.Problem);
        });
    }
}
