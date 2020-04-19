using System;
using Dora.Repository.Benchmark.Benchmarks;

namespace Dora.Repository.Benchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkDotNet.Running.BenchmarkRunner.Run<CrudBenchmarks>();
        }
    }
}
