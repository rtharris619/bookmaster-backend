using Bookmaster.Common.Features.Messaging;

namespace Bookmaster.Modules.Books.Features.OpenLibrary.GetOpenLibraryWork;

public sealed record OpenLibraryWorkQuery(string WorkKey)
    : IQuery<OpenLibraryWorkResponse>;
