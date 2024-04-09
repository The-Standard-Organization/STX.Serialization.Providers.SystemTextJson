// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LHDS.Core.Models.Foundations.Addresses.Exceptions;
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

        public async ValueTask<TOutput> SerializeAsync<TInput, TOutput>(
            TInput @object,
            CancellationToken cancellationToken = default)
        {
            MemoryStream outputStream = new MemoryStream();
            await systemTextSerializationBroker.SerializeAsync(outputStream, @object, cancellationToken);
            outputStream.Position = 0;

            switch (typeof(TOutput))
            {
                case Type _ when typeof(TOutput) == typeof(string):
                    return (TOutput)(object)Encoding.UTF8.GetString(outputStream.ToArray());

                case Type _ when typeof(TOutput) == typeof(byte[]):
                    return (TOutput)(object)outputStream.ToArray();

                case Type _ when typeof(TOutput) == typeof(Stream):
                    return (TOutput)(object)outputStream;

                default:
                    throw new InvalidOperationSerializationException($"Unsupported output type: {typeof(TOutput)}");
            }
        }

        public ValueTask<TOutput> DeserializeAsync<TInput, TOutput>(
            TInput json, CancellationToken cancellationToken = default) =>
            throw new NotImplementedException();
    }
}
