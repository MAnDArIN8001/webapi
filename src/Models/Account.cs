using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace src.Models {
    public class Account {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId id {get; set;}
        
        public string firstName {get; set;} = "default";
        public string secondName {get; set;} = "default";
        public string mail {get; set;} = "default";
        public string password {get; set;} = "default";
    }
}