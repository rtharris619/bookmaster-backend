using Microsoft.AspNetCore.Routing;

namespace Bookmaster.Common.Presentation.Endpoints;

public interface IEndpoint
{
    void MapEndpoint(IEndpointRouteBuilder routeBuilder);
}
