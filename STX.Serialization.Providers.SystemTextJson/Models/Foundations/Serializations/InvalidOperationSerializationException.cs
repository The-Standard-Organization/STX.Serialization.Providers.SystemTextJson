// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using Xeptions;

namespace LHDS.Core.Models.Foundations.Addresses.Exceptions
{
    public class InvalidOperationSerializationException : Xeption
    {
        public InvalidOperationSerializationException(string message)
            : base(message)
        { }
    }
}