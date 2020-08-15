﻿using Pygma.Data.Domain.Entities;

 namespace Pygma.Data.Abstractions.Cache
{
    public interface IUsersCache: IPygmaCache<User>
    {
        User GetByEmail(string email);
    }
}
