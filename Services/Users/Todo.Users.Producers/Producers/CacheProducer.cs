using MassTransit;
using System;
using System.Threading.Tasks;
using Todo.Messages.Messages;
using Todo.Users.Producers.Contracts;
using Todo.Users.Producers.Interfaces.Producers;

namespace Todo.Users.Producers.Producers
{
    public class CacheProducer : ICacheProducer
    {
        public readonly ISendEndpointProvider endpointProvider;

        public CacheProducer(ISendEndpointProvider endpointProvider) => this.endpointProvider = endpointProvider;

        public async Task DeleteCacheAsync(string key)
        {
            ISendEndpoint endpoint = await endpointProvider.GetSendEndpoint(new Uri(ContractStrings.DeleteCacheQueue));

            await endpoint.Send<IDeleteCacheMessage>(new { Key = key });
        }
    }
}
