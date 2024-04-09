// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Xunit;

namespace STX.Serialization.Providers.SystemTextJson.Tests.Unit.Services.Foundations.Serializations
{
    public partial class SerializationServiceTests
    {
        [Fact]
        public async Task ShouldSerializeObjectAndReturnAsStringAsync()
        {
            // given
            CancellationToken cancellationToken = default;
            dynamic randomObject = CreateRandomObject();
            object inputObject = randomObject;
            string randomSerializedOutput = GetRandomString();
            string expectedResult = randomSerializedOutput.DeepClone();
            MemoryStream randomOutputStream = new MemoryStream();

            systemTextSerializationBrokerMock.Setup(service =>
                service.SerializeAsync(
                    It.Is(SameMemoryStreamAs(randomOutputStream)),
                    inputObject,
                    It.IsAny<CancellationToken>()))
                .Callback<Stream, object, CancellationToken>((outputStream, obj, token) =>
                {
                    byte[] bytes = Encoding.UTF8.GetBytes(randomSerializedOutput);
                    outputStream.Write(bytes, 0, bytes.Length);
                })
                .Returns(ValueTask.CompletedTask);

            // when
            string actualResult = await this.serializationService
                .SerializeAsync<object, string>(inputObject, cancellationToken);

            // then
            actualResult.Should().BeEquivalentTo(expectedResult);

            systemTextSerializationBrokerMock.Verify(service =>
                service.SerializeAsync(It.IsAny<Stream>(), inputObject, It.IsAny<CancellationToken>()),
                    Times.Once);
        }
    }
}
