// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using STX.Serialization.Providers.SystemTextJson.Models.Foundations.Serializations;

namespace STX.Serialization.Providers.SystemTextJson.Services.Foundations.Serializations
{
    internal partial class SerializationService
    {
        private static void ValidateInputIsNotNull<TInput>(TInput @object)
        {
            if (@object is null)
            {
                throw new NullSerializationException(message: $"Input of type {typeof(int)} is null.");
            }
        }
    }
}
