// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using STX.Serialization.Providers.SystemTextJson.Models.Foundations.Serializations;
using Xeptions;

namespace STX.Serialization.Providers.SystemTextJson.Services.Foundations.Serializations
{
    internal partial class SerializationService
    {
        private delegate ValueTask<T> ReturningValueTaskFunction<T>();

        private async ValueTask<T> TryCatch<T>(ReturningValueTaskFunction<T> returningFunction)
        {
            try
            {
                return await returningFunction();
            }
            catch (InvalidOperationSerializationException invalidOperationSerializationException)
            {
                throw CreateAndLogValidationException(invalidOperationSerializationException);
            }
            catch (NullSerializationException nullSerializationException)
            {
                throw CreateAndLogValidationException(nullSerializationException);
            }
        }

        private SerializationValidationException CreateAndLogValidationException(Xeption exception)
        {
            var serializationValidationException = new SerializationValidationException(
                message: "Serialization validation errors occurred, please try again.",
                innerException: exception);

            return serializationValidationException;
        }
    }
}
