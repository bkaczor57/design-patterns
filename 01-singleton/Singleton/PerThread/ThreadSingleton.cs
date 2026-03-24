namespace Singleton.PerThread;

// Singleton per wątek - każdy wątek dostaje własna instancje
// ThreadLocal<T> z .NET - działa jak Lazy<T> ale per wątek
// Każdy wątek ma własny slot w pamięci i własna instancję
public sealed class ThreadSingleton
{
    // ThreadLocal tworzy osobną instancję dla każdego wątku
    // Inne wątki nie widzą nawzajem swoich instancji
    private static readonly ThreadLocal<ThreadSingleton> _instance =
        new(() => new ThreadSingleton());

    public static ThreadSingleton Instance => _instance.Value!;

    public Guid Id { get; }
    public int ManagedThreadId { get; }

    private ThreadSingleton()
    {
        Id = Guid.NewGuid();
        // Zapisujemy Id wątku który stworzył tę instancję
        ManagedThreadId = Environment.CurrentManagedThreadId;
    }
}
