using BenchmarkDotNet.Attributes;
using System.Collections.Generic;
using System.Linq;

namespace PerfTest
{
    using BenchmarkDotNet.Attributes.Jobs;
    using BenchmarkDotNet.Engines;

    [MemoryDiagnoser]
    [SimpleJob(RunStrategy.ColdStart, 1, 0, 5, 1, "Linq.Select")]
    public class LinqSelect
    {
        private List<double> items;

        [Setup]
        public void SetUp()
        {
            this.items = CreateItems(this.ItemCount);
        }

        private static List<double> CreateItems(int itemCount)
        {
            return Enumerable
                .Range(1, itemCount)
                .Select(x => (double)x)
                .ToList();
        }

        [Params(1000, 10000, 100000, 1000000)]
        public int ItemCount { get; set; }

        [Benchmark(Description = "Linq")]
        public List<string> Select()
        {
            return this.items.Select(x => "id" + x).ToList();
        }

        [Benchmark(Description = "Basic")]
        public List<string> Basic()
        {
            var result = new List<string>();
            foreach (var item in this.items)
            {
                result.Add("id" + item);
            }
            return result;
        }
    }
}
