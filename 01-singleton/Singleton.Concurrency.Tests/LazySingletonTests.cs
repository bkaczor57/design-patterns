using System.Runtime.CompilerServices;
using Singleton.Concurrency;

namespace Singleton.Concurrency.Tests;

public class LazySingletonTests
{
    // ==================== UNIT TESTS ================

    // Basic test to check if instance is notnull
    [Fact]
    public void Instance_IsNotNull()
    {
        Assert.NotNull(LazySingleton.Instance);
    }

    // Checks that two calls to Instance return the exact same object (reference equality)
    [Fact]
    public void Instance_CalledTwice_ReturnsSameReference()
    {
        var first = LazySingleton.Instance;
        var second = LazySingleton.Instance;

        Assert.Same(first, second);
    }

    // Same check but using ReferenceEquals explicitly. other way to prove its the same
    [Fact]
    public void Instance_CalledTwice_ReferenceEqualsReturnsTrue()
    {
        var first = LazySingleton.Instance;
        var second = LazySingleton.Instance;

        Assert.True(ReferenceEquals(first, second));
    }

    // Checks that the Guid Id is the same - proves the constructor ran only once
    [Fact]
    public void Instance_CalledTwice_HasSameId()
    {
        var first = LazySingleton.Instance;
        var second = LazySingleton.Instance;

        Assert.Equal(first.Id, second.Id);
    }

    // Verifies the returned type is exactly LazySingleton
    [Fact]
    public void Instance_HasCorrectType()
    {
        var instance = LazySingleton.Instance;

        Assert.IsType<LazySingleton>(instance);
        Assert.Equal(typeof(LazySingleton), instance.GetType());
    }

    // Uses RuntimeHelpers.GetHashCode - the CLRs internal identity hash (based on memory address)
    [Fact]
    public void Instance_CalledTwice_SameIdentityHashCode()
    {
        var first = LazySingleton.Instance;
        var second = LazySingleton.Instance;

        Assert.Equal(
            RuntimeHelpers.GetHashCode(first),
            RuntimeHelpers.GetHashCode(second)
        );
    }

    // Checks multiple calls in a loop - all should return the same reference
    [Fact]
    public void Instance_CalledManyTimes_AlwaysSameReference()
    {
        var original = LazySingleton.Instance;

        for (int i = 0; i < 100; i++)
        {
            Assert.Same(original, LazySingleton.Instance);
        }
    }

    // ================ CONCURRENCY TESTS ====================

    // 100 threads start at the same time using Barrier and all must get the same instance
    [Fact]
    public async Task ConcurrentAccess_WithBarrier_AllGetSameInstance()
    {
        const int threadCount = 100;
        var instances = new LazySingleton[threadCount];
        var barrier = new Barrier(threadCount);

        var tasks = Enumerable.Range(0, threadCount).Select(i => Task.Run(() =>
        {
            barrier.SignalAndWait();
            instances[i] = LazySingleton.Instance;
        })).ToArray();

        await Task.WhenAll(tasks);

        Assert.All(instances, inst => Assert.Same(instances[0], inst));
    }

    // Parallel.For test - checks Guid identity across threads
    [Fact]
    public void ParallelAccess_AllThreadsGetSameId()
    {
        const int threadCount = 50;
        var ids = new Guid[threadCount];

        Parallel.For(0, threadCount, i =>
        {
            ids[i] = LazySingleton.Instance.Id;
        });

        Assert.All(ids, id => Assert.Equal(ids[0], id));
    }

    // ========= FUNCTIONAL TESTS ============

    // Functional test - type check across threads (no proxy or different type returned)
    // sprawdzenie czy typ jest inny w różnych wątkach
    // w razie co, wątek mógłby dostać inny typ obiektu
    [Fact]
    public async Task FunctionalTest_ConcurrentAccess_TypeIsAlwaysCorrect()
    {
        const int threadCount = 50;
        var types = new Type[threadCount];

        var tasks = Enumerable.Range(0, threadCount).Select(i => Task.Run(() =>
        {
            types[i] = LazySingleton.Instance.GetType();
        })).ToArray();

        await Task.WhenAll(tasks);

        Assert.All(types, t => Assert.Equal(typeof(LazySingleton), t));
    }
}
