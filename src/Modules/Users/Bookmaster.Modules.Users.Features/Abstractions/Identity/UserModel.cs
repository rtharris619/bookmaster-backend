﻿namespace Bookmaster.Modules.Users.Features.Abstractions.Identity;

public sealed record UserModel(string Email, string Password, string FirstName, string LastName);
