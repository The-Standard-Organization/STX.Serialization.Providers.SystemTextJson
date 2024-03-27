// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

namespace STX.Serialization.Providers.SystemTextJson.Brokers
{
    internal interface ISystemTextSerializationBroker
    {
        string Serialize<T>(T @object);
    }
}
