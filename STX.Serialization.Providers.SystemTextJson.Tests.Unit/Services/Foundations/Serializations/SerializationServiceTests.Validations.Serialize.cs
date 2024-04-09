// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System.IO;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Force.DeepCloner;
using STX.Serialization.Providers.SystemTextJson.Models.Foundations.Serializations;
using Xunit;

namespace STX.Serialization.Providers.SystemTextJson.Tests.Unit.Services.Foundations.Serializations
{
    public partial class SerializationServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnSerializeIfOutPutTypeNotSupportedAsync()
        {
            // given
            CancellationToken cancellationToken = default;
            dynamic randomObject = CreateRandomObject();
            object inputObject = randomObject;
            string randomSerializedOutput = GetRandomString();
            string expectedResult = randomSerializedOutput.DeepClone();
            MemoryStream randomOutputStream = new MemoryStream();

            var invalidOperationSerializationException = new InvalidOperationSerializationException(
                message: $"Unsupported output type: {typeof(int)}. " +
                    $"Supported types:  {typeof(string)}, {typeof(byte[])}, {typeof(Stream)}");

            var expectedSerializationValidationException =
                new SerializationValidationException(
                    message: "Serialization validation errors occurred, please try again.",
                    innerException: invalidOperationSerializationException);

            // when
            ValueTask<int> serializationTask = this.serializationService
                .SerializeAsync<object, int>(inputObject, cancellationToken);

            SerializationValidationException actualSerializationValidationException =
                await Assert.ThrowsAsync<SerializationValidationException>(() =>
                    serializationTask.AsTask());

            // then
            actualSerializationValidationException.Should().BeEquivalentTo(expectedSerializationValidationException);
            systemTextSerializationBrokerMock.VerifyNoOtherCalls();
        }
    }
}
