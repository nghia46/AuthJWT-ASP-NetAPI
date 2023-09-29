using MongoDB.Bson.Serialization.Attributes;

namespace Auth_Pro
{
    public class UserView
    {
        public string? name { get; set; }
        public string? password { get; set; }
        public string? role { get; set; }
    }
}
