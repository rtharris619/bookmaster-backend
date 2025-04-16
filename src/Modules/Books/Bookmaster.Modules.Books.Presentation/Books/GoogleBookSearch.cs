using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookmaster.Common.Domain;
using Bookmaster.Common.Presentation.ApiResults;
using Bookmaster.Common.Presentation.Endpoints;
using Bookmaster.Modules.Books.Features.Books.GoogleBookSearch;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Bookmaster.Modules.Books.Presentation.Books;

internal sealed class GoogleBookSearch : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapGet("books/googlebook-api-search", async (ISender sender, string q, string printType, int maxResults = 2) =>
        {
            Result<GoogleBookSearchResponse> result = await sender.Send(new GoogleBookSearchQuery(q, printType, maxResults));

            return result.Match(Results.Ok, ApiResults.Problem);
        });
    }

    internal sealed class GoogleBookSearchRequest
    {
        public string q { get; init; }
        public string PrintType { get; init; }
        public int MaxResults { get; init; }
    }
}
