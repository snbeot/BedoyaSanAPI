using MongoDB.Bson.Serialization.Attributes;

namespace BedoyaSanAPI.Models
{
    public class TaskItem
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public List<string> SubTareas { get; set; }
    }
}
