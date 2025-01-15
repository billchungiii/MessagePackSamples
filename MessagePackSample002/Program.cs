using MessagePack;

namespace MessagePackSample002
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //DateTime dateTime = new DateTime(1985, 3, 4);
            //Console.WriteLine(dateTime.Kind);
            //dateTime = DateTime.SpecifyKind(dateTime, DateTimeKind.Local);
            //Console.WriteLine(dateTime.Kind);
            //Console.WriteLine(dateTime);
            //dateTime = DateTime.Now;
            //Console.WriteLine(dateTime.Kind);
            //Console.ReadLine();
            var p1 = new Person
            {
                Name = "Bill",
                BirthDay = new DateTime(1985, 3, 4, 0, 0, 0),
                Address = new Address
                {
                    Street = "123 Main St",
                    City = "Redmond"
                }
            };

            byte[] b1 = MessagePackSerializer.Serialize(p1);

          
            var p1_1 = MessagePackSerializer.Deserialize<Person>(b1);           
            Console.WriteLine($"Name : {p1_1.Name}, Birthday :{p1_1.BirthDay}, Age : {p1_1.Age}, serialization length : {b1.Length}");
            Console.WriteLine($"Address : {p1_1.Address.Street}, {p1_1.Address.City}");
            Console.WriteLine(p1_1.BirthDay.Kind);

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

        [IgnoreMember]
        public int Age
        {
            get
            {
                DateTime today = DateTime.Today;
                int age = today.Year - BirthDay.Year;
                if (BirthDay.Date > today.AddYears(-age))
                {
                    age--;
                }
                return age;
            }
        }
        [Key(2)]
        public Address Address { get; set; }
    }
    [MessagePackObject]
    public class Address
    {
        [Key(0)]
        public string Street { get; set; }
        [Key(1)]
        public string City { get; set; }
    }
}
