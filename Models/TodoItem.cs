using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TodoApi.Models
{
    public class TodoItem
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement]
        public string userName {get; set;}

        [BsonElement]
        public string todo { get; set; }

        [BsonElement]
        public bool isDone {get; set;}

        [BsonElement]
        public bool hasAttachment { get; set; }
    }

}