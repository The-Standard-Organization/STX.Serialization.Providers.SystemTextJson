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
        public async Task ShouldThrowValidationExceptionOnDeserializeIfInputTypeNotSupportedAsync()
        {
            // given
            CancellationToken cancellationToken = default;
            Guid invalidInput = Guid.NewGuid();

            var invalidOperationSerializationException = new InvalidOperationSerializationException(
                message: $"Unsupported input type: {typeof(Guid)}. " +
                    $"Supported types:  {typeof(string)}, {typeof(byte[])}, {typeof(Stream)}");

            var expectedSerializationValidationException =
                new SerializationValidationException(
                    message: "Serialization validation errors occurred, please try again.",
                    innerException: invalidOperationSerializationException);

            // when
            ValueTask<dynamic> serializationTask = this.serializationService
                .DeserializeAsync<Guid, dynamic>(invalidInput, cancellationToken);

            SerializationValidationException actualSerializationValidationException =
                await Assert.ThrowsAsync<SerializationValidationException>(() =>
                    serializationTask.AsTask());

            // then
            actualSerializationValidationException.Should().BeEquivalentTo(expectedSerializationValidationException);
            systemTextSerializationBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task ShouldThrowValidationExceptionOnDeserializeIfStringInputIsNullOrInvalidAsync(
            string invalidText)
        {
            CancellationToken cancellationToken = default;
            string invalidString = invalidText;

            var invalidSerializationException =
                new InvalidSerializationException(
                    message: "Invalid input. Please correct the errors and try again.");

            invalidSerializationException.AddData(
                key: "json",
                values: "Text is required");

            var expectedSerializationValidationException =
                new SerializationValidationException(
                    message: "Serialization validation errors occurred, please try again.",
                    innerException: invalidSerializationException);

            // when
            ValueTask<dynamic> serializationTask = this.serializationService
                .DeserializeAsync<string, dynamic>(invalidString, cancellationToken);

            SerializationValidationException actualSerializationValidationException =
                await Assert.ThrowsAsync<SerializationValidationException>(() =>
                    serializationTask.AsTask());

            // then
            actualSerializationValidationException.Should().BeEquivalentTo(expectedSerializationValidationException);
            systemTextSerializationBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(null)]
        [InlineData(new byte[0])]
        public async Task ShouldThrowValidationExceptionOnDeserializeIfByteInputIsNullOrInvalidAsync(
            byte[] invalidBytes)
        {
            CancellationToken cancellationToken = default;
            byte[] invalidInput = invalidBytes;

            var invalidSerializationException =
                new InvalidSerializationException(
                    message: "Invalid input. Please correct the errors and try again.");

            invalidSerializationException.AddData(
                key: "json",
                values: "Bytes is required");

            var expectedSerializationValidationException =
                new SerializationValidationException(
                    message: "Serialization validation errors occurred, please try again.",
                    innerException: invalidSerializationException);

            // when
            ValueTask<dynamic> serializationTask = this.serializationService
                .DeserializeAsync<byte[], dynamic>(invalidInput, cancellationToken);

            SerializationValidationException actualSerializationValidationException =
                await Assert.ThrowsAsync<SerializationValidationException>(() =>
                    serializationTask.AsTask());

            // then
            actualSerializationValidationException.Should().BeEquivalentTo(expectedSerializationValidationException);
            systemTextSerializationBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("emptyMemoryStream")]
        public async Task ShouldThrowValidationExceptionOnDeserializeIfStreamInputIsNullOrInvalidAsync(
            object invalidBytes)
        {
            CancellationToken cancellationToken = default;
            Stream invalidInput = invalidBytes == null ? null : new MemoryStream();

            var invalidSerializationException =
                new InvalidSerializationException(
                    message: "Invalid input. Please correct the errors and try again.");

            invalidSerializationException.AddData(
                key: "json",
                values: "Stream is required");

            var expectedSerializationValidationException =
                new SerializationValidationException(
                    message: "Serialization validation errors occurred, please try again.",
                    innerException: invalidSerializationException);

            // when
            ValueTask<dynamic> serializationTask = this.serializationService
                .DeserializeAsync<Stream, dynamic>(invalidInput, cancellationToken);

            SerializationValidationException actualSerializationValidationException =
                await Assert.ThrowsAsync<SerializationValidationException>(() =>
                    serializationTask.AsTask());

            // then
            actualSerializationValidationException.Should().BeEquivalentTo(expectedSerializationValidationException);
            systemTextSerializationBrokerMock.VerifyNoOtherCalls();
        }
    }
}
