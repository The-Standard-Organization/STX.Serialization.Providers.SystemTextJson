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
            string randomOutput = GetRandomString();
            string expectedResult = randomOutput.DeepClone();
            using var inputStream = new MemoryStream();
            using var randomOutputStream = new MemoryStream();

            systemTextSerializationBrokerMock.Setup(service =>
                service.SerializeAsync(randomOutputStream, inputObject, It.IsAny<CancellationToken>()))
                .Callback<Stream, object, CancellationToken>((s, obj, token) =>
                {
                    s = new MemoryStream(Encoding.UTF8.GetBytes(randomOutput));
                })
                .Returns(ValueTask.CompletedTask);

            // when
            string actualResult = await this.serializationService.SerializeAsync<object, string>(inputObject);


            // then
            actualResult.Should().BeEquivalentTo(expectedResult);
        }
    }
}
