using BenchmarkDotNet.Running;

namespace PerfTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<Linq>();
        }
    }
}
