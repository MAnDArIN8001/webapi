using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace api.Models {
    public class Student {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId id {get; set;}
        public int age {get; set;}

        public float avrMark {get; set;}

        public string firstName {get; set;} = "default";
        public string secondName {get; set;} = "default";
    }
}