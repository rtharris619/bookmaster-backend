﻿using Bookmaster.Common.Domain;

namespace Bookmaster.Modules.Users.Domain.Users;

public sealed class User : Entity
{
    private readonly List<Role> _roles = [];

    private User() { }

    public Guid Id { get; private set; }

    public string Email { get; private set; }

    public string FirstName { get; private set; }

    public string LastName { get; private set; }

    public string IdentityId { get; private set; }

    public IReadOnlyCollection<Role> Roles => [.. _roles];

    public static User Create(string email, string firstName, string lastName, string identityId)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = email,
            FirstName = firstName,
            LastName = lastName,
            IdentityId = identityId,
        };

        user._roles.Add(Role.Member);

        user.Raise(new UserRegisteredDomainEvent(user.Id));

        return user;
    }
}
