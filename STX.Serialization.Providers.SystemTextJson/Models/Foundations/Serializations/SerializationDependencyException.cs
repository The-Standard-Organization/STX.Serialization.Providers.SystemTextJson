// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using Xeptions;

namespace STX.Serialization.Providers.SystemTextJson.Models.Foundations.Serializations
{
    public class SerializationDependencyException : Xeption
    {
        public SerializationDependencyException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}