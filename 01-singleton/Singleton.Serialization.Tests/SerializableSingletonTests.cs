using System.Text.Json;
using Singleton.Serialization;

namespace Singleton.Serialization.Tests;

public class SerializableSingletonTests
{
    // Options z naszym converterem - klucz do rozwiązania problemu serializacji
    private readonly JsonSerializerOptions _options = new()
    {
        Converters = { new SingletonConverter<SerializableSingleton>(() => SerializableSingleton.Instance) }
    };

    // Serializacja do JSON działa i zawiera właściwości singletona
    [Fact]
    public void Serialize_ProducesValidJson()
    {
        var instance = SerializableSingleton.Instance;

        var json = JsonSerializer.Serialize(instance, _options);

        Assert.Contains("Id", json);
        Assert.Contains("Data", json);
        Assert.Contains(instance.Id.ToString(), json);
    }

    // GŁÓWNY TEST - deserializacja Z converterem zwraca TĘ SAMĄ instancję
    // Converter ignoruje dane z JSON i zwraca Instance - odpowiednik ReadResolve z Javy
    [Fact]
    public void Deserialize_WithConverter_ReturnsSameInstance()
    {
        var original = SerializableSingleton.Instance;

        var json = JsonSerializer.Serialize(original, _options);
        var deserialized = JsonSerializer.Deserialize<SerializableSingleton>(json, _options);

        // Po deserializacji to MUSI być ta sama instancja
        Assert.Same(original, deserialized);
        Assert.Equal(original.Id, deserialized!.Id);
    }

    // BEZ convertera - pokazujemy PROBLEM który rozwiązujemy
    // JsonSerializer nie może utworzyć obiektu bo konstruktor jest prywatny
    [Fact]
    public void Deserialize_WithoutConverter_Throws()
    {
        var json = JsonSerializer.Serialize(SerializableSingleton.Instance);

        Assert.ThrowsAny<NotSupportedException>(() =>
            JsonSerializer.Deserialize<SerializableSingleton>(json));
    }

    // Wielokrotna serializacja/deserializacja - zawsze ta sama instancja
    [Fact]
    public void Deserialize_MultipleRoundTrips_AlwaysSameInstance()
    {
        var original = SerializableSingleton.Instance;

        for (int i = 0; i < 5; i++)
        {
            var json = JsonSerializer.Serialize(original, _options);
            var deserialized = JsonSerializer.Deserialize<SerializableSingleton>(json, _options);

            Assert.Same(original, deserialized);
        }
    }
}
