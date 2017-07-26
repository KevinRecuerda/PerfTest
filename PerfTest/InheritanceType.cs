namespace PerfTest
{
    using BenchmarkDotNet.Attributes;
    using BenchmarkDotNet.Attributes.Jobs;
    using BenchmarkDotNet.Engines;

    [MemoryDiagnoser]
    [SimpleJob(RunStrategy.ColdStart, 1, 0, 5, 1, "InheritanceType")]
    public class InheritanceType
    {
        private Animal dog, fish;

        [Setup]
        public void SetUp()
        {
            this.dog = new Dog();
            this.fish = new Fish();
        }

        [Params(100, 1000, 10000, 100000, 1000000)]
        public int Count { get; set; }

        [Benchmark(Description = "Is type")]
        public void IsType()
        {
            for (var i = 0; i < this.Count; i++)
            {
                var r1 = this.IsType(this.dog);
                var r2 = this.IsType(this.fish);
            }
        }
        public bool IsType(Animal animal)
        {
            return animal is Mammal;
        }

        [Benchmark(Description = "Implemented method")]
        public void IsMethod()
        {
            for (var i = 0; i < this.Count; i++)
            {
                var r1 = this.IsMethod(this.dog);
                var r2 = this.IsMethod(this.fish);
            }
        }
        public bool IsMethod(Animal animal)
        {
            return animal.IsMammal();
        }
    }

    public abstract class Animal
    {
        public virtual bool IsMammal()
        {
            return false;
        }
    }

    public abstract class Mammal : Animal
    {
        public override bool IsMammal()
        {
            return true;
        }
    }

    public class Dog : Mammal {}

    public class Fish : Animal {}
}
