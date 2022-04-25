using System;
using MediatR;
using Todo.Users.Core.Cache;
using Todo.Users.Core.Interfaces.Caching;

namespace Todo.Users.Application.Handlers.Users.Queries.GetUserDetails
{
    public class GetUserDetailsQuery : IRequest<GetUserDetailsVm>, ICacheableMediatorQuery
    {
        public Guid UserId { get; set; }

        public string CacheKey => CacheContracts.GetUserDetailsKey(UserId);
    }
}
