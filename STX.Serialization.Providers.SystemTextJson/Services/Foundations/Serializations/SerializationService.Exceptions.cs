// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System.Text.Json;
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
                throw CreateValidationException(invalidOperationSerializationException);
            }
            catch (NullSerializationException nullSerializationException)
            {
                throw CreateValidationException(nullSerializationException);
            }
            catch (JsonException jsonException)
            {
                var failedSerializationException =
                    new FailedSerializationException(
                        message: "Failed serialization error occurred, contact support.",
                        innerException: jsonException);

                throw CreateDependencyException(failedSerializationException);
            }
        }

        private SerializationValidationException CreateValidationException(Xeption exception)
        {
            var serializationValidationException = new SerializationValidationException(
                message: "Serialization validation errors occurred, please try again.",
                innerException: exception);

            return serializationValidationException;
        }

        private SerializationDependencyException CreateDependencyException(Xeption exception)
        {
            var serializationDependencyException =
                new SerializationDependencyException(
                    message: "Serialization dependency error occurred, contact support.",
                    innerException: exception);

            return serializationDependencyException;
        }
    }
}
