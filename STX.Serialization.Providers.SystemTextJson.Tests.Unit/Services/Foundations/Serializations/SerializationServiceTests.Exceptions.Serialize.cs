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
                    message: "Failed serialization error occurred, please contact support.",
                    innerException: dependencyException);

            var expectedSerializationDependencyException =
                new SerializationDependencyException(
                    message: "Serialization dependency error occurred, please contact support.",
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

        [Fact]
        public async Task ShouldThrowServiceExceptionOnSerializeAsync()
        {
            CancellationToken cancellationToken = default;
            dynamic randomObject = CreateRandomObject();
            object inputObject = randomObject;
            MemoryStream randomOutputStream = new MemoryStream();
            string exceptionMessage = GetRandomString();
            Exception serviceException = new Exception(exceptionMessage);

            var failedSerializationServiceException =
                new FailedSerializationServiceException(
                    message: "Failed serialization service occurred, please contact support.",
                    innerException: serviceException);

            var expectedSerializationServiceException =
                new SerializationServiceException(
                    message: "Serialization service error occurred, please contact support.",
                    innerException: failedSerializationServiceException);

            systemTextSerializationBrokerMock.Setup(service =>
                service.SerializeAsync(
                    It.IsAny<Stream>(),
                    It.IsAny<object>(),
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(serviceException);

            // when
            ValueTask<string> serializationTask = this.serializationService
                .SerializeAsync<object, string>(inputObject, cancellationToken);

            SerializationServiceException actualSerializationServiceException =
                await Assert.ThrowsAsync<SerializationServiceException>(() =>
                    serializationTask.AsTask());

            // then
            actualSerializationServiceException.Should().BeEquivalentTo(expectedSerializationServiceException);

            systemTextSerializationBrokerMock.Verify(service =>
                service.SerializeAsync(It.IsAny<Stream>(), It.IsAny<object>(), It.IsAny<CancellationToken>()),
                    Times.Once);

            systemTextSerializationBrokerMock.VerifyNoOtherCalls();
        }
    }
}
