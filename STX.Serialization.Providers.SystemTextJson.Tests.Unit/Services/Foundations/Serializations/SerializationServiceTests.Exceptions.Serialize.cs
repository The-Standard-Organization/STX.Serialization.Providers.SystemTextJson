// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using STX.Serialization.Providers.SystemTextJson.Models.Foundations.Serializations;
using Xunit;

namespace STX.Serialization.Providers.SystemTextJson.Tests.Unit.Services.Foundations.Serializations
{
    public partial class SerializationServiceTests
    {
        [Fact]
        public async Task ShouldThrowDependencyExceptionOnSerializeAsync()
        {
            CancellationToken cancellationToken = default;
            dynamic randomObject = CreateRandomObject();
            object inputObject = randomObject;
            MemoryStream randomOutputStream = new MemoryStream();
            string exceptionMessage = GetRandomString();
            Exception randomException = new Exception(exceptionMessage);

            JsonException dependencyException = new JsonException(
                message: exceptionMessage,
                innerException: randomException);

            var failedSerializationException =
                new FailedSerializationException(
                    message: "Failed serialization error occurred, contact support.",
                    innerException: dependencyException);

            var expectedSerializationDependencyException =
                new SerializationDependencyException(
                    message: "Serialization dependency error occurred, contact support.",
                    innerException: failedSerializationException);

            systemTextSerializationBrokerMock.Setup(service =>
                service.SerializeAsync(
                    It.IsAny<Stream>(),
                    It.IsAny<object>(),
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(dependencyException);

            // when
            ValueTask<string> serializationTask = this.serializationService
                .SerializeAsync<object, string>(inputObject, cancellationToken);

            SerializationDependencyException actualSerializationDependencyException =
                await Assert.ThrowsAsync<SerializationDependencyException>(() =>
                    serializationTask.AsTask());

            // then
            actualSerializationDependencyException.Should().BeEquivalentTo(expectedSerializationDependencyException);

            systemTextSerializationBrokerMock.Verify(service =>
                service.SerializeAsync(It.IsAny<Stream>(), It.IsAny<object>(), It.IsAny<CancellationToken>()),
                    Times.Once);

            systemTextSerializationBrokerMock.VerifyNoOtherCalls();
        }
    }
}
