using Bookmaster.Common.Features.Messaging;

namespace Bookmaster.Modules.Books.Features.OpenLibrary.GetOpenLibraryEditions;

public sealed record OpenLibraryEditionQuery(string WorkKey, int? Limit = 3, int? Offset = 0)
    : IQuery<OpenLibraryEditionResponse>;
