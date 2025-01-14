using MessagePack;
using System.Text;
using System.Text.Json;

namespace MessagePackSample001
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /// Person class is serialized with key as index
            var p1 = new Person { Name = "Bill", Age = 36 };            
            var b1 = MessagePackSerializer.Serialize(p1);           
            var p1_1 = MessagePackSerializer.Deserialize<Person>(b1);
            Console.WriteLine($"Name : {p1_1.Name}, Age : {p1_1.Age}, serialization length : {b1.Length}");

            /// Person2 class is serialized with key as property name
            var p2 = new Person2 { Name = "Bill", Age = 36 };
            var b2 = MessagePackSerializer.Serialize(p2);
            var p2_1 = MessagePackSerializer.Deserialize<Person2>(b2);
            Console.WriteLine($"Name : {p2_1.Name}, Age : {p2_1.Age}, serialization length : {b2.Length}");

            /// Person class is serialized by System.Text.Json
            var json = JsonSerializer.Serialize(p1);
            var jsonBytes = Encoding.UTF8.GetBytes(json);
            var p1_2 = JsonSerializer.Deserialize<Person>(jsonBytes);
            Console.WriteLine($"Name : {p1_2.Name}, Age : {p1_2.Age}, serialization length : {jsonBytes.Length}");
        }
    }
}

[MessagePackObject]
public class Person
{
    [Key(0)]
    public string Name { get; set; }

    [Key(1)]
    public int Age { get; set; }
}
[MessagePackObject(keyAsPropertyName: true)]
public class Person2
{
    public string Name { get; set; }
    public int Age { get; set; }
}
