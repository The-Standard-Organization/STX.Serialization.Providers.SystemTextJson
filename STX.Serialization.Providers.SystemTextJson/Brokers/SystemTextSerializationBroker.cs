// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace STX.Serialization.Providers.SystemTextJson.Brokers
{
    internal class SystemTextSerializationBroker : ISystemTextSerializationBroker
    {
        private readonly JsonSerializerOptions jsonSerializerOptions;

        public SystemTextSerializationBroker(JsonSerializerOptions jsonSerializerOptions) =>
            this.jsonSerializerOptions = jsonSerializerOptions;

        public async ValueTask SerializeAsync<T>(
            Stream utf8JsonStream,
            T @object,
            CancellationToken cancellationToken = default)
        {
            await JsonSerializer.SerializeAsync(
                utf8Json: utf8JsonStream,
                value: @object,
                options: jsonSerializerOptions,
                cancellationToken);
        }

        public async ValueTask<T> DeserializeAsync<T>(
            Stream stream,
            CancellationToken cancellationToken = default)
        {
            return await JsonSerializer.DeserializeAsync<T>(
                utf8Json: stream,
                options: jsonSerializerOptions,
                cancellationToken);
        }
    }
}
