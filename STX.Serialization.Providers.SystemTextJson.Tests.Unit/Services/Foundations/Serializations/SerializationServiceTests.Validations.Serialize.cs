// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using STX.Serialization.Providers.SystemTextJson.Models.Foundations.Serializations;
using Xunit;

namespace STX.Serialization.Providers.SystemTextJson.Tests.Unit.Services.Foundations.Serializations
{
    public partial class SerializationServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnSerializeIfOutputTypeNotSupportedAsync()
        {
            // given
            CancellationToken cancellationToken = default;
            dynamic randomObject = CreateRandomObject();
            object inputObject = randomObject;

            var invalidOperationSerializationException = new InvalidOperationSerializationException(
                message: $"Unsupported output type: {typeof(Guid)}. " +
                    $"Supported types:  {typeof(string)}, {typeof(byte[])}, {typeof(Stream)}");

            var expectedSerializationValidationException =
                new SerializationValidationException(
                    message: "Serialization validation errors occurred, please try again.",
                    innerException: invalidOperationSerializationException);

            // when
            ValueTask<Guid> serializationTask = this.serializationService
                .SerializeAsync<object, Guid>(inputObject, cancellationToken);

            SerializationValidationException actualSerializationValidationException =
                await Assert.ThrowsAsync<SerializationValidationException>(() =>
                    serializationTask.AsTask());

            // then
            actualSerializationValidationException.Should().BeEquivalentTo(expectedSerializationValidationException);
            systemTextSerializationBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionOnSerializeIfInputIsNullAsync()
        {
            CancellationToken cancellationToken = default;
            object nullObject = null;
            object inputObject = nullObject;

            var nullSerializationException = new NullSerializationException(
                message: $"Input is null.");

            var expectedSerializationValidationException =
                new SerializationValidationException(
                    message: "Serialization validation errors occurred, please try again.",
                    innerException: nullSerializationException);

            // when
            ValueTask<Guid> serializationTask = this.serializationService
                .SerializeAsync<object, Guid>(inputObject, cancellationToken);

            SerializationValidationException actualSerializationValidationException =
                await Assert.ThrowsAsync<SerializationValidationException>(() =>
                    serializationTask.AsTask());

            // then
            actualSerializationValidationException.Should().BeEquivalentTo(expectedSerializationValidationException);
            systemTextSerializationBrokerMock.VerifyNoOtherCalls();
        }
    }
}
