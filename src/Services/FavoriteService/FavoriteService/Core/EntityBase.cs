using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FavoriteService.Core
{
    public abstract class EntityBase : IEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
    }
}
