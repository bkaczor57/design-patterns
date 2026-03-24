namespace Singleton.Concurrency;

// Singleton based on Lazy<T> .NET
public sealed class LazySingleton
{
    // Lazy<T> tworzy instancję dopiero przy pierwszym odczycie .Value
    // i zapewnia thread-safety - nie trzeba pisać żadnych locków
    private static readonly Lazy<LazySingleton> _instance =
        new(() => new LazySingleton());

    // Właściwość zwracająca jedyną instancję
    public static LazySingleton Instance => _instance.Value;

    // atrybut z guid do dodatkowego sprawdzenai czy to ta sama inst
    public Guid Id { get; }

    // Private tak by nitk nie mogl uruchomic
    private LazySingleton()
    {
        Id = Guid.NewGuid();
    }
}
