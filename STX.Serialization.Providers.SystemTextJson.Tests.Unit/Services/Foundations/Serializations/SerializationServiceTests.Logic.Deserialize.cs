// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Xunit;

namespace STX.Serialization.Providers.SystemTextJson.Tests.Unit.Services.Foundations.Serializations
{
    public partial class SerializationServiceTests
    {
        [Fact]
        public async Task ShouldDeserializeStreamToObjectAsync()
        {
            // given
            CancellationToken cancellationToken = default;
            string randomJsonString = GetRandomString();
            byte[] inputBytes = Encoding.UTF8.GetBytes(randomJsonString);
            Stream inputStream = new MemoryStream(inputBytes);
            dynamic randomObject = CreateRandomObject();
            object deserializedObject = randomObject;
            dynamic expectedResult = deserializedObject;

            systemTextSerializationBrokerMock.Setup(service =>
                service.DeserializeAsync<dynamic>(inputStream, cancellationToken))
                    .ReturnsAsync(deserializedObject);

            // when
            dynamic actualResult = await this.serializationService
                .DeserializeAsync<Stream, dynamic>(inputStream, cancellationToken);

            // then
            ((object)actualResult).Should().BeEquivalentTo((object)expectedResult);

            systemTextSerializationBrokerMock.Verify(service =>
                service.DeserializeAsync<dynamic>(inputStream, cancellationToken),
                    Times.Once);

            systemTextSerializationBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldDeserializeBytesToObjectAsync()
        {
            // given
            CancellationToken cancellationToken = default;
            string randomJsonString = GetRandomString();
            byte[] inputBytes = Encoding.UTF8.GetBytes(randomJsonString);
            Stream inputStream = new MemoryStream(inputBytes);
            dynamic randomObject = CreateRandomObject();
            object deserializedObject = randomObject;
            dynamic expectedResult = deserializedObject;

            systemTextSerializationBrokerMock.Setup(service =>
                service.DeserializeAsync<dynamic>(It.Is(SameStreamAs(inputStream)), cancellationToken))
                    .ReturnsAsync(deserializedObject);

            // when
            dynamic actualResult = await this.serializationService
                .DeserializeAsync<byte[], dynamic>(inputBytes, cancellationToken);

            // then
            ((object)actualResult).Should().BeEquivalentTo((object)expectedResult);

            systemTextSerializationBrokerMock.Verify(service =>
                service.DeserializeAsync<dynamic>(It.Is(SameStreamAs(inputStream)), cancellationToken),
                    Times.Once);

            systemTextSerializationBrokerMock.VerifyNoOtherCalls();
        }
    }
}
