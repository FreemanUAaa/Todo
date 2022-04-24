using MassTransit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Todo.Messages.Messages;
using Todo.Users.Producers.Contracts;
using Todo.Users.Producers.Interfaces.Producers;

namespace Todo.Users.Producers.Producers
{
    public class UserCoverProducer : IUserCoverProducer
    {
        public readonly ISendEndpointProvider endpointProvider;

        public UserCoverProducer(ISendEndpointProvider endpointProvider) => this.endpointProvider = endpointProvider;

        public async Task DeleteUserCoverAsync(Guid userId)
        {
            ISendEndpoint endpoint = await endpointProvider.GetSendEndpoint(new Uri(ContractStrings.DeleteUserCoverQueue));

            await endpoint.Send<IDeleteUserCoverMessage>(new { UserId = userId });
        }
    }
}
