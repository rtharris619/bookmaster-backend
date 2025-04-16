using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookmaster.Common.Presentation.Endpoints;
using Bookmaster.Modules.Books.Features.Books.GoogleBookSearch;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Bookmaster.Modules.Books.Presentation.Books;

internal sealed class GoogleBookSearch : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapGet("books/googlebook-api-search", async (ISender sender, GoogleBookSearchRequest request) =>
        {
            await sender.Send(new GoogleBookSearchQuery(request.q, request.PrintType, request.MaxResults));
        });
    }

    internal sealed class GoogleBookSearchRequest
    {
        public string q { get; init; }
        public string PrintType { get; init; }
        public int MaxResults { get; init; }
    }
}
