using System.Reflection;

namespace Bookmaster.Modules.Books.Features;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
