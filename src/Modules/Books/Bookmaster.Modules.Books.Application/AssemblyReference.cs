using System.Reflection;

namespace Bookmaster.Modules.Books.Application;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
