// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System.Threading;
using System.Threading.Tasks;

namespace STX.Serialization.Providers.SystemTextJson.Services.Foundations.Serializations
{
    internal interface ISerializationService
    {
        ValueTask<TOutput> SerializeAsync<TInput, TOutput>(
            TInput @object,
            CancellationToken cancellationToken = default);

        ValueTask<TOutput?> DeserializeAsync<TInput, TOutput>(
            TInput json,
            CancellationToken cancellationToken = default);
    }
}
