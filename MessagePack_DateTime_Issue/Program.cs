using MessagePack;

namespace MessagePack_DateTime_Issue
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // source DateTimeKind : Unspecified
            DateTime unspecifiedDate = new DateTime(1985, 3, 4);           
            DateTime unspecifiedDateDeserialized = SerializeThenDeserialize(unspecifiedDate);
            DisplayResult(unspecifiedDate, unspecifiedDateDeserialized);


            // source DateTimeKind : Local
            DateTime localDate = new DateTime(1985, 3, 5, 0, 0, 0, DateTimeKind.Local);
            DateTime localDateDeserialized = SerializeThenDeserialize(localDate);
            DisplayResult(localDate, localDateDeserialized);

            // source DateTimeKind : Utc
            DateTime utcDate = new DateTime(1985, 3, 6, 0, 0, 0, DateTimeKind.Utc);
            DateTime utcDateDeserialized = SerializeThenDeserialize(utcDate);
            DisplayResult(utcDate, utcDateDeserialized);
        }

        static DateTime SerializeThenDeserialize(DateTime source)
        {
            byte[] bytes = MessagePackSerializer.Serialize(source);
            return MessagePackSerializer.Deserialize<DateTime>(bytes);
        }

        static void DisplayResult(DateTime source, DateTime deserialized)
        {
            Console.WriteLine($"Source Date: ({source}:{source.Kind}), Deserialized Date: ({deserialized}:{deserialized.Kind})");
        }
    }
}
