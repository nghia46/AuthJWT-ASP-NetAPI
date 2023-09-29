using MongoDB.Bson.Serialization.Attributes;
using System.Text;

namespace Auth_Pro
{
    public class User
    {
        [BsonId]
        public Guid id { get; set; }
        [BsonElement]
        public string? name { get; set; }
        [BsonElement]
        public string? password { get; set; }
        [BsonElement]
        public string? role { get; set; }
    }
}
