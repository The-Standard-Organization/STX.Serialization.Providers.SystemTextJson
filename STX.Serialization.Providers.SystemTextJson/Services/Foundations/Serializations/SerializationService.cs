// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
using System.Threading;
using System.Threading.Tasks;
using STX.Serialization.Providers.SystemTextJson.Brokers;

namespace STX.Serialization.Providers.SystemTextJson.Services.Foundations.Serializations
{
    internal class SerializationService : ISerializationService
    {
        private readonly ISystemTextSerializationBroker systemTextSerializationBroker;

        public SerializationService(ISystemTextSerializationBroker systemTextSerializationBroker)
        {
            this.systemTextSerializationBroker = systemTextSerializationBroker;
        }

        public ValueTask<TOutput> SerializeAsync<TInput, TOutput>(
            TInput @object,
            CancellationToken cancellationToken = default) =>
            throw new NotImplementedException();

        public ValueTask<TOutput> DeserializeAsync<TInput, TOutput>(
            TInput json, CancellationToken cancellationToken = default) =>
            throw new NotImplementedException();
    }
}
