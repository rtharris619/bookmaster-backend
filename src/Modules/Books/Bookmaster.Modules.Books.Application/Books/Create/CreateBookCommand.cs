using Bookmaster.Common.Features.Messaging;
using Bookmaster.Modules.Books.Domain.Books;

namespace Bookmaster.Modules.Books.Features.Books.Create;

public sealed record CreateBookCommand(string GoogleBookId) : ICommand<Guid>;
