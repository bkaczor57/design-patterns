using Singleton.PerThread;

namespace Singleton.PerThread.Tests;

public class ThreadSingletonTests
{
    // W tym samym wątku Instance zawsze zwraca tę samą instancję
    [Fact]
    public void SameThread_ReturnsSameInstance()
    {
        var a = ThreadSingleton.Instance;
        var b = ThreadSingleton.Instance;

        Assert.Same(a, b);
        Assert.Equal(a.Id, b.Id);
    }

    // różne wątki dostają różne instancje (różne Id)
    // jeden singleton per wątek
    [Fact]
    public void DifferentThreads_GetDifferentInstances()
    {
        var mainInstance = ThreadSingleton.Instance;
        ThreadSingleton? otherInstance = null;

        var thread = new Thread(() =>
        {
            otherInstance = ThreadSingleton.Instance;
        });
        thread.Start();
        thread.Join();

        // Instancje z różnych wątków muszą być różne
        Assert.NotSame(mainInstance, otherInstance);
        Assert.NotEqual(mainInstance.Id, otherInstance!.Id);
    }

    // Wiele wątków - każdy ma swoją unikalną instancję z unikalnym Id
    [Fact]
    public async Task MultipleThreads_EachGetsUniqueInstance()
    {
        const int threadCount = 10;
        var ids = new Guid[threadCount];

        var tasks = Enumerable.Range(0, threadCount).Select(i => Task.Factory.StartNew(() =>
        {
            ids[i] = ThreadSingleton.Instance.Id;
        }, TaskCreationOptions.LongRunning)).ToArray(); // LongRunning wymusza nowy wątek

        await Task.WhenAll(tasks);

        // Wszystkie Id muszą być unikalne - każdy wątek ma swoje
        Assert.Equal(threadCount, ids.Distinct().Count());
    }

    // W obrębie jednego wątku wielokrotne wywołania zwracają tę samą instancję
    [Fact]
    public void SameThread_MultipleCalls_AlwaysSameInstance()
    {
        var original = ThreadSingleton.Instance;

        for (int i = 0; i < 50; i++)
        {
            Assert.Same(original, ThreadSingleton.Instance);
        }
    }

    // ManagedThreadId zapisany w instancji zgadza się z wątkiem który ją stworzył
    [Fact]
    public void Instance_StoresCorrectThreadId()
    {
        int? capturedThreadId = null;
        int? singletonThreadId = null;

        var thread = new Thread(() =>
        {
            capturedThreadId = Environment.CurrentManagedThreadId;
            singletonThreadId = ThreadSingleton.Instance.ManagedThreadId;
        });
        thread.Start();
        thread.Join();

        Assert.Equal(capturedThreadId, singletonThreadId);
    }
}
