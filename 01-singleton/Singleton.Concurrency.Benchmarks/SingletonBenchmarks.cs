using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using Singleton.Concurrency;

namespace Singleton.Concurrency.Benchmarks;

[MemoryDiagnoser]
[SimpleJob(RuntimeMoniker.Net80, warmupCount: 3, iterationCount: 10)]
public class SingletonBenchmarks
{
    [Params(1, 10, 50, 100)]
    public int ThreadCount { get; set; }

    // Measures how fast we can access Instance from a single thread (baseline)
    [Benchmark(Baseline = true, Description = "Single thread access")]
    public LazySingleton SingleThread() => LazySingleton.Instance;

    // Measures concurrent access with N tasks via Task.Run
    [Benchmark(Description = "Concurrent access (Task.Run)")]
    public async Task ConcurrentTaskRun()
    {
        var tasks = new Task<LazySingleton>[ThreadCount];
        for (int i = 0; i < ThreadCount; i++)
            tasks[i] = Task.Run(() => LazySingleton.Instance);

        await Task.WhenAll(tasks);
    }

    // Measures worst-case: all threads start at the exact same time via Barrier
    [Benchmark(Description = "Barrier contention (threads)")]
    public void BarrierContention()
    {
        var barrier = new Barrier(ThreadCount);
        var threads = new Thread[ThreadCount];

        for (int i = 0; i < ThreadCount; i++)
        {
            threads[i] = new Thread(() =>
            {
                barrier.SignalAndWait();
                _ = LazySingleton.Instance;
            });
            threads[i].Start();
        }

        foreach (var t in threads)
            t.Join();
    }

    // Measures access via Parallel.For (ThreadPool managed)
    [Benchmark(Description = "Parallel.For access")]
    public void ParallelForAccess()
    {
        Parallel.For(0, ThreadCount, i =>
        {
            _ = LazySingleton.Instance;
        });
    }
}
