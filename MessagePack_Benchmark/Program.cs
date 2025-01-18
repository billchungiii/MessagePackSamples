using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using MessagePack;
using System.Text.Json;

namespace MessagePack_Benchmark
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<SerializationBenchmark>();
        }
    }

    [MemoryDiagnoser]
    public class SerializationBenchmark
    {
        private Person person;

        [GlobalSetup]
        public void Setup()
        {
            person = new Person
            {
                Name = "Bill",
                BirthDate = new DateTime(1985, 3, 4, 0, 0, 0),
                IsEmployed = true,
                Salary = 1000.50m,
                Address = new Address
                {
                    Street = "123 Main St",
                    City = "Redmond"
                },
                PhoneNumber = "123-456-7890",
                Skills = new List<string> { "C#", "JavaScript", "Python", "C++", "Ada", "Delphi" }
            };
        }

        [Benchmark]       
        public void ExecMessagePack()
        {
            var bytes = MessagePackSerializer.Serialize(person);
            var personDeserialized = MessagePackSerializer.Deserialize<Person>(bytes);
        }

        [Benchmark]
        
        public void ExecJson()
        {
            var json = JsonSerializer.Serialize(person);
            var personDeserialized = JsonSerializer.Deserialize<Person>(json);
        }
    }
    
    [MessagePackObject]
    public class Person
    {
        [Key(0)]
        public string Name { get; set; }
        [Key(1)]
        public DateTime BirthDate { get; set; }
        [Key(2)]
        public bool IsEmployed { get; set; }
        [Key(3)]
        public decimal Salary { get; set; }
        [Key(4)]
        public List<string> Skills { get; set; }
        [Key(5)]
        public Address Address { get; set; }
        [Key(6)]
        public string PhoneNumber { get; set; }

        public Person()
        {
            Skills = new List<string>();
        }
    }

    [MessagePackObject]
    public class Address
    {
        [Key(0)]
        public string Street { get; set; }
        [Key(1)]
        public string City { get; set; }
        [Key(2)]
        public string Country { get; set; }
    }
}
