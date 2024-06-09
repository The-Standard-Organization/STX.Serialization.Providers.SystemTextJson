// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using STX.Serialization.Providers.Abstractions.Models.Exceptions;
using Xeptions;

namespace STX.Serialization.Providers.SystemTextJson.Models.Providers.Serializations.SystemTextJson.Exceptions
{
    public class SystemTextJsonSerializationProviderDependencyException : Xeption, ISerializationDependencyException
    {
        public SystemTextJsonSerializationProviderDependencyException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}
