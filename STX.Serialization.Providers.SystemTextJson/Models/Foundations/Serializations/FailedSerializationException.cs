// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
using Xeptions;

namespace STX.Serialization.Providers.SystemTextJson.Models.Foundations.Serializations
{
    public class FailedSerializationException : Xeption
    {
        public FailedSerializationException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}