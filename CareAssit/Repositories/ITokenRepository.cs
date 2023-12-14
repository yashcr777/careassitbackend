﻿using Microsoft.AspNetCore.Identity;

namespace CareAssit.Repositories
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
