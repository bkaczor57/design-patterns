using System.Text.Json;
using System.Text.Json.Serialization;

namespace Singleton.Serialization;

// Singleton odporny na serializację i deserializację
// Problem: JsonSerializer po deserializacji tworzy NOWY obiekt (nowe Id, nowa referencja)
// Rozwiązanie: JsonConverter<T> który przy deserializacji zwraca istniejącą instancję
// To jest odpowiednik "ReadResolve" z Javy ale w stylu .NET
public sealed class SerializableSingleton
{
    private static readonly Lazy<SerializableSingleton> _instance =
        new(() => new SerializableSingleton());

    public static SerializableSingleton Instance => _instance.Value;

    public Guid Id { get; }

    public string Data { get; set; } = "default";

    private SerializableSingleton()
    {
        Id = Guid.NewGuid();
    }
}

// JsonConverter który przy deserializacji ZAWSZE zwraca istniejącą instancję singletona
// Zamiast tworzyć nowy obiekt, ignoruje dane z JSON i zwraca Instance
public class SingletonConverter<T> : JsonConverter<T> where T : class
{
    private readonly Func<T> _getInstance;

    public SingletonConverter(Func<T> getInstance) => _getInstance = getInstance;

    public override T? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
         // Deserializuj do anonimowego obiektu, ale zwróć singleton
        using var doc = JsonDocument.ParseValue(ref reader);
        return _getInstance();
    }

    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    {
        // przekazywanie od razu opcji w JsonSerializer w przykładzie powoduje nieskończoną rekurencje wiec przypisujemy najpierw do zmiennej
        var clean = new JsonSerializerOptions(options);
        clean.Converters.Clear();
        JsonSerializer.Serialize(writer, value, clean);
    }
    }
