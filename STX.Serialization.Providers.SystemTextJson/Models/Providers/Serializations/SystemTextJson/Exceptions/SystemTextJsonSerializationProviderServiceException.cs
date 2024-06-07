// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using STX.Serialization.Providers.Abstractions.Models.Exceptions;
using Xeptions;

namespace STX.Serialization.Providers.SystemTextJson.Models.Providers.Serializations.SystemTextJson.Exceptions
{
    public class SystemTextJsonSerializationProviderServiceException : Xeption, ISerializationServiceException
    {
        public SystemTextJsonSerializationProviderServiceException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}
