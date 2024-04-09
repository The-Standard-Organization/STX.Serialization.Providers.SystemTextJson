// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using Xeptions;

namespace STX.Serialization.Providers.SystemTextJson.Models.Foundations.Serializations
{
    public class NullSerializationException : Xeption
    {
        public NullSerializationException(string message)
            : base(message)
        { }
    }
}