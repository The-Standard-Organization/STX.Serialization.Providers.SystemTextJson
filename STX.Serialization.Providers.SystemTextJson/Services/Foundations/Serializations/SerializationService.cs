// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using STX.Serialization.Providers.SystemTextJson.Brokers;
using STX.Serialization.Providers.SystemTextJson.Models.Foundations.Serializations;

namespace STX.Serialization.Providers.SystemTextJson.Services.Foundations.Serializations
{
    internal partial class SerializationService : ISerializationService
    {
        private readonly ISystemTextSerializationBroker systemTextSerializationBroker;

        public SerializationService(ISystemTextSerializationBroker systemTextSerializationBroker) =>
            this.systemTextSerializationBroker = systemTextSerializationBroker;

        public ValueTask<TOutput> SerializeAsync<TInput, TOutput>(
            TInput @object,
            CancellationToken cancellationToken = default) =>
            TryCatch((ReturningValueTaskFunction<TOutput>)(async () =>
            {
                ValidateInputIsNotNull(@object);

                return await SerializeDataAsync<TInput, TOutput>(@object, cancellationToken);
            }));

        private async ValueTask<TOutput> SerializeDataAsync<TInput, TOutput>(
            TInput @object,
            CancellationToken cancellationToken)
        {
            MemoryStream outputStream = new MemoryStream();

            switch (typeof(TOutput))
            {
                case Type _ when typeof(TOutput) == typeof(string):
                    await Serialize(@object, outputStream, cancellationToken);
                    return (TOutput)(object)Encoding.UTF8.GetString(outputStream.ToArray());

                case Type _ when typeof(TOutput) == typeof(byte[]):
                    await Serialize(@object, outputStream, cancellationToken);
                    return (TOutput)(object)outputStream.ToArray();

                case Type _ when typeof(TOutput) == typeof(Stream):
                    await Serialize(@object, outputStream, cancellationToken);
                    return (TOutput)(object)outputStream;

                default:
                    throw new InvalidOperationSerializationException(
                        $"Unsupported output type: {typeof(TOutput)}. " +
                        $"Supported types:  {typeof(string)}, {typeof(byte[])}, {typeof(Stream)}");
            }
        }

        private async Task Serialize<TInput>(
            TInput @object,
            MemoryStream outputStream,
            CancellationToken cancellationToken) =>
            await systemTextSerializationBroker.SerializeAsync(outputStream, @object, cancellationToken);

        public ValueTask<TOutput> DeserializeAsync<TInput, TOutput>(
            TInput json, CancellationToken cancellationToken = default) =>
            throw new NotImplementedException();
    }
}
