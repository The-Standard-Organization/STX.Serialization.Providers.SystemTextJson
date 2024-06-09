// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System.Collections;
using STX.Serialization.Providers.Abstractions.Models.Exceptions;
using Xeptions;

namespace STX.Serialization.Providers.SystemTextJson.Models.Providers.Serializations.SystemTextJson.Exceptions
{
    public class SystemTextJsonSerializationProviderValidationException : Xeption, ISerializationValidationException
    {
        public SystemTextJsonSerializationProviderValidationException(string message, Xeption innerException)
            : base(message, innerException)
        { }

        public SystemTextJsonSerializationProviderValidationException(
            string message,
            Xeption innerException,
            IDictionary data)
            : base(message, innerException, data)
        { }
    }
}
