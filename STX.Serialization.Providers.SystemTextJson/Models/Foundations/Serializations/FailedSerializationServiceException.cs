// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
using Xeptions;

namespace STX.Serialization.Providers.SystemTextJson.Models.Foundations.Serializations
{
    public class FailedSerializationServiceException : Xeption
    {
        public FailedSerializationServiceException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}