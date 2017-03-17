using BenchmarkDotNet.Running;

namespace PerfTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BenchmarkRunner.Run<Linq>();
            BenchmarkRunner.Run<LinqSelect>();
        }
    }
}
