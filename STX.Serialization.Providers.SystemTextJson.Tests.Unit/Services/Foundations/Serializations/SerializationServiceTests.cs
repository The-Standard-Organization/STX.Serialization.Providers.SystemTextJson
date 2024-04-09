// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq.Expressions;
using KellermanSoftware.CompareNetObjects;
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
        private readonly ICompareLogic compareLogic;

        public SerializationServiceTests()
        {
            this.compareLogic = new CompareLogic();
            this.systemTextSerializationBrokerMock = new Mock<ISystemTextSerializationBroker>();
            this.serializationService = new SerializationService(systemTextSerializationBrokerMock.Object);
        }


        static dynamic CreateRandomObject()
        {
            dynamic obj = new ExpandoObject();
            var random = new Random();

            for (int i = 0; i < random.Next(3, 8); i++)
            {
                string propName = "Property" + i;
                dynamic propValue = GenerateRandomValue(random);
                ((IDictionary<string, object>)obj)[propName] = propValue;
            }

            return obj;
        }

        static dynamic GenerateRandomValue(Random random)
        {
            int type = random.Next(5);

            switch (type)
            {
                case 0:
                    return Guid.NewGuid().ToString();
                case 1:
                    return DateTime.Now.AddDays(random.Next(-365, 365));
                case 2:
                    return random.Next(2) == 0;
                case 3:
                    return random.Next(1000);
                case 4:
                    return Guid.NewGuid();
                default:
                    return null;
            }
        }

        private static string GetRandomString() =>
            new MnemonicString(wordCount: GetRandomNumber()).GetValue();

        private static int GetRandomNumber() =>
            new IntRange(min: 2, max: 10).GetValue();

        private Expression<Func<MemoryStream, bool>> SameMemoryStreamAs(
            MemoryStream expectedStream)
        {
            return actualStream =>
                this.compareLogic.Compare(expectedStream, actualStream)
                    .AreEqual;
        }
    }
}
