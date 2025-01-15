using MessagePack;

namespace MessagePack_DateTime_Issue2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Person person = new Person
            {
                Name = "Bill",
                BirthDay = new DateTime(1985, 3, 4, 0, 0, 0)
            };

            byte[] bytes = MessagePackSerializer.Serialize(person);
            Person personDeserialized = MessagePackSerializer.Deserialize<Person>(bytes);
            DisplayResult("person", person.BirthDay);
            DisplayResult("personDeserialized", personDeserialized.BirthDay);

        }

        private static void DisplayResult(string prefix, DateTime value)
        {
            Console.WriteLine($"{prefix} : {value}, DateTimeKind: {value.Kind}");
        }
    }

    public static class DateTimeExtensions
    {
        public static DateTime ConvertToLocalTime(this DateTime dateTime)
        {
            if (dateTime.Kind == DateTimeKind.Utc)
            {
                return dateTime.ToLocalTime();
            }
            else if (dateTime.Kind == DateTimeKind.Unspecified)
            {
                return DateTime.SpecifyKind(dateTime, DateTimeKind.Local);
            }
            else
            {
                return dateTime;
            }
        }
    }

    [MessagePackObject]
    public class Person
    {
        [Key(0)]
        public string Name { get; set; }

        private DateTime _birthDay;
        [Key(1)]
        public DateTime BirthDay
        {
            get => _birthDay;
            set => _birthDay = value.ConvertToLocalTime();
        }
    }
}
