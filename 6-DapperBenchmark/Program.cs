using BenchmarkDotNet.Running;

namespace _6_DapperBenchmark
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var result = BenchmarkRunner.Run<OrmPerformanceTest>();
            Console.WriteLine(result);

            Console.ReadLine();
        }
    }
}
