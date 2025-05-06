namespace Bookmaster.Modules.Books.Features.Library.GetLibraryEntry;

public sealed record LibraryEntryResponse(
    Guid Id,
    Guid BookId,
    Guid PersonId,
    string GoogleBookId,
    string Title,
    string? SubTitle,
    string? Description,
    string[] Authors,
    string FlattenedAuthors,
    string[] Categories,
    string FlattenedCategories,
    string[]? Tags,
    string? FlattenedTags,
    int PageCount,
    string? Thumbnail,
    string? Publisher,
    string? PublishedDate,
    string Language,
    string? GoogleBookInfoLink,
    string? GoogleBookPreviewLink);
