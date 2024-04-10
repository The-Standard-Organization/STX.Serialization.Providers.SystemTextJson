// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
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
                        message: "Failed serialization error occurred, please contact support.",
                        innerException: jsonException);

                throw CreateDependencyException(failedSerializationException);
            }
            catch (Exception exception)
            {
                var failedSerializationServiceException =
                    new FailedSerializationServiceException(
                        message: "Failed serialization service occurred, please contact support.",
                        innerException: exception);

                throw CreateServiceException(failedSerializationServiceException);
            }
        }

        private static SerializationValidationException CreateValidationException(Xeption exception)
        {
            var serializationValidationException = new SerializationValidationException(
                message: "Serialization validation errors occurred, please try again.",
                innerException: exception);

            return serializationValidationException;
        }

        private static SerializationDependencyException CreateDependencyException(Xeption exception)
        {
            var serializationDependencyException =
                new SerializationDependencyException(
                    message: "Serialization dependency error occurred, please contact support.",
                    innerException: exception);

            return serializationDependencyException;
        }

        private static SerializationServiceException CreateServiceException(Xeption exception)
        {
            var serializationServiceException =
                new SerializationServiceException(
                    message: "Serialization service error occurred, please contact support.",
                    innerException: exception);

            return serializationServiceException;
        }
    }
}
