namespace Todo.Users.Producers.Contracts
{
    public static class ContractStrings
    {
        public static string DeleteUserCoverQueue => "queue:delete-user-cover";

        public static string DeleteCacheQueue => "queue:delete-cache";
    }
}
