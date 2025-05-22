using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookmaster.Modules.Users.Domain.Users;

public sealed class Role
{
    public static Role SuperAdmin { get; } = new Role("SuperAdmin");
    public static Role Admin { get; } = new Role("Admin");
    public static Role Member { get; } = new Role("Member");

    private Role(string name)
    {
        Name = name;
    }

    private Role() {}

    public string Name { get; private set; }
}
