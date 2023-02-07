namespace TestSources.Deserialization
{
    internal interface IJsonDeserializer
    {
        T? Deserialize<T>(string jsonString);
    }
}