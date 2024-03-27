// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System.Text.Json;

namespace STX.Serialization.Providers.SystemTextJson.Brokers
{
    internal class SystemTextSerializationBroker : ISystemTextSerializationBroker
    {
        private readonly JsonSerializerOptions jsonSerializerOptions;

        public SystemTextSerializationBroker(JsonSerializerOptions jsonSerializerOptions)
        {
            this.jsonSerializerOptions = jsonSerializerOptions;
        }

        public string Serialize<T>(T @object) =>
            JsonSerializer.Serialize(@object, jsonSerializerOptions);
    }
}
