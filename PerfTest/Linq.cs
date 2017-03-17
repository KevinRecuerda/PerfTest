using BenchmarkDotNet.Attributes;
using System.Collections.Generic;
using System.Linq;

namespace PerfTest
{
    using BenchmarkDotNet.Attributes.Jobs;
    using BenchmarkDotNet.Engines;

    [MemoryDiagnoser]
    [SimpleJob(RunStrategy.ColdStart, 1, 0, 5, 1, "Linq")]
    public class Linq
    {
        private List<Item1> items;

        [Setup]
        public void SetUp()
        {
            this.items = CreateItems(this.RowCount, this.ColumnCount);
        }

        private static List<Item1> CreateItems(int rowCount, int columnCount)
        {
            return Enumerable
                .Range(1, rowCount)
                .Select(x => new Item1()
                {
                    Id = $"Id{x}",
                    KeyValues = Enumerable.Range(1, columnCount)
                                          .Select(y => new KeyValuePair<string, double>($"Key{y}", (x-1)*columnCount + y))
                                          .ToList()
                })
                .ToList();
        }

        [Params(100, 1000, 10000)]
        public int RowCount { get; set; }

        [Params(10, 100, 1000)]
        public int ColumnCount { get; set; }

        [Benchmark(Description = "Linq")]
        public Dictionary<string, List<double>> GroupBy()
        {
            return this.items.SelectMany(x => x.KeyValues)
                             .GroupBy(x => x.Key)
                             .ToDictionary(x => x.Key, x => x.Select(y => y.Value).ToList());
        }

        [Benchmark(Description = "Basic")]
        public Dictionary<string, List<double>> Basic()
        {
            var result = new Dictionary<string, List<double>>();
            foreach (var item in this.items)
            {
                foreach (var keyValuePair in item.KeyValues)
                {
                    if (!result.ContainsKey(keyValuePair.Key))
                        result[keyValuePair.Key] = new List<double>();

                    result[keyValuePair.Key].Add(keyValuePair.Value);
                }
            }
            return result;
        }
    }

    public class Item1
    {
        public string Id { get; set; }
        public List<KeyValuePair<string, double>> KeyValues { get; set; }
    }
}
