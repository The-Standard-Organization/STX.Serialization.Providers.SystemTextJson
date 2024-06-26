﻿// ----------------------------------------------------------------------------------
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
            TryCatch(async () =>
            {
                ValidateInputIsNotNull(@object);

                return await SerializeDataAsync<TInput, TOutput>(@object, cancellationToken);
            });

        public ValueTask<TOutput> DeserializeAsync<TInput, TOutput>(
            TInput json, CancellationToken cancellationToken = default) =>
            TryCatch(async () =>
            {
                ValidateInput(json);

                return await DeserializeDataAsync<TInput, TOutput>(json, cancellationToken);
            });

        private async ValueTask<TOutput> SerializeDataAsync<TInput, TOutput>(
            TInput @object,
            CancellationToken cancellationToken)
        {
            MemoryStream outputStream = new MemoryStream();

            switch (typeof(TOutput))
            {
                case Type outputType when outputType == typeof(string):
                    await Serialize(@object, outputStream, cancellationToken);

                    return (TOutput)(object)Encoding.UTF8.GetString(outputStream.ToArray());

                case Type outputType when outputType == typeof(byte[]):
                    await Serialize(@object, outputStream, cancellationToken);

                    return (TOutput)(object)outputStream.ToArray();

                case Type outputType when outputType == typeof(Stream):
                    await Serialize(@object, outputStream, cancellationToken);

                    return (TOutput)(object)outputStream;

                default:
                    throw new InvalidOperationSerializationException(
                        $"Unsupported output type: {typeof(TOutput)}. " +
                        $"Supported types:  {typeof(string)}, {typeof(byte[])}, {typeof(Stream)}");
            }
        }

        private async ValueTask<TOutput> DeserializeDataAsync<TInput, TOutput>(
            TInput json,
            CancellationToken cancellationToken)
        {
            switch (typeof(TInput))
            {
                case Type iputType when iputType == typeof(string):
                    {
                        byte[] jsonBytes = Encoding.UTF8.GetBytes($"{json}");
                        Stream jsonStream = new MemoryStream(buffer: jsonBytes);

                        return await Deserialize<TOutput>(jsonStream, cancellationToken);
                    }

                case Type iputType when iputType == typeof(byte[]):
                    {
                        Stream jsonStream = new MemoryStream(buffer: json as byte[]);

                        return await Deserialize<TOutput>(jsonStream, cancellationToken);
                    }

                case Type iputType when iputType == typeof(Stream):
                    {
                        var jsonStream = json as Stream;
                        jsonStream.Position = 0;

                        return await Deserialize<TOutput>(jsonStream, cancellationToken);
                    }

                default:
                    throw new InvalidOperationSerializationException(
                        $"Unsupported input type: {typeof(TInput)}. " +
                        $"Supported types:  {typeof(string)}, {typeof(byte[])}, {typeof(Stream)}");
            }
        }

        private async ValueTask Serialize<TInput>(
            TInput @object,
            MemoryStream outputStream,
            CancellationToken cancellationToken) =>
            await systemTextSerializationBroker.SerializeAsync(outputStream, @object, cancellationToken);

        private async ValueTask<TOutput> Deserialize<TOutput>(
            Stream jsonStream,
            CancellationToken cancellationToken) =>
            await systemTextSerializationBroker.DeserializeAsync<TOutput>(jsonStream, cancellationToken);
    }
}
