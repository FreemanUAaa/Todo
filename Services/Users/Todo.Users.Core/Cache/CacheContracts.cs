using System;

namespace Todo.Users.Core.Cache
{
    public static class CacheContracts
    {
        public static string GetUserDetailsKey(Guid userId) => $"user-details:{userId}";
    }
}
