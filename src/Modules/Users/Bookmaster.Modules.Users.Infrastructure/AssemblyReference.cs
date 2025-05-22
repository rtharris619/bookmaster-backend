using System.Reflection;

namespace Bookmaster.Modules.Users.Infrastructure;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
