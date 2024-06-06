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
    }
}
