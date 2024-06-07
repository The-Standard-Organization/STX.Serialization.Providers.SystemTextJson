// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using Xeptions;

namespace STX.Serialization.Providers.SystemTextJson.Models.Foundations.Serializations
{
    internal class SerializationDependencyValidationException : Xeption
    {
        public SerializationDependencyValidationException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}