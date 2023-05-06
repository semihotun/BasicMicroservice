using FavoriteService.Core;

namespace FavoriteService.Model
{
    public class Favorite : EntityBase
    {
        public string BlogId { get; set; }

        public bool IsLike { get; set; }
    }
}
