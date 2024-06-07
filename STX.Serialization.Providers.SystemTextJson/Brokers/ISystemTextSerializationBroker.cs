// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace STX.Serialization.Providers.SystemTextJson.Brokers
{
    internal interface ISystemTextSerializationBroker
    {
        ValueTask SerializeAsync<T>(
            Stream utf8JsonStream,
            T @object,
            CancellationToken cancellationToken = default);

        ValueTask<T> DeserializeAsync<T>(
            Stream stream,
            CancellationToken cancellationToken = default);
    }
}
