// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System.Text.Json;
using Moq;
using STX.Serialization.Providers.SystemTextJson.Brokers;
using STX.Serialization.Providers.SystemTextJson.Services.Foundations.Serializations;
using Tynamix.ObjectFiller;

namespace STX.Serialization.Providers.SystemTextJson.Tests.Unit.Services.Foundations.Serializations
{
    public partial class SerializationServiceTests
    {
        private readonly Mock<ISystemTextSerializationBroker> systemTextSerializationBrokerMock;
        private readonly SerializationService serializationService;

        public SerializationServiceTests()
        {
            this.systemTextSerializationBrokerMock = new Mock<ISystemTextSerializationBroker>();
            this.serializationService = new SerializationService(systemTextSerializationBrokerMock.Object);
        }


        static dynamic CreateRandomObject()
        {
            var filler = new Filler<object>();

            return filler.Create();
        }

        private static string ConvertObjectToJson(dynamic randomObject) =>
            JsonSerializer.Serialize(randomObject);

        private static string GetRandomString() =>
            new MnemonicString(wordCount: GetRandomNumber()).GetValue();

        private static int GetRandomNumber() =>
            new IntRange(min: 2, max: 10).GetValue();
    }
}
