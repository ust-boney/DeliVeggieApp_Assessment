﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace VeggieAppConsole.Models
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("ItemId")]
        public int ItemId { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }
        
        [BsonElement("EntryDate")]
        public string EntityDate { get; set; }

        [BsonElement("Price")]
        public double Price { get; set; }
    }
}
