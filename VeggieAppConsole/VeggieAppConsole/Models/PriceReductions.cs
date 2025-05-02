using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace VeggieAppConsole.Models
{
    public class PriceReductions
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("DayOfWeek")]
        public int DayOfWeek { get; set; }

        [BsonElement("Reduction")]
        public double Reduction { get; set; }
    }
}
