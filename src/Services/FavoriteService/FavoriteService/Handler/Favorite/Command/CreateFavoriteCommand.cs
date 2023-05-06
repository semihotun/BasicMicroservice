using FavoriteService.Result;
using MediatR;

namespace FavoriteService.Handler.Favorite.Command
{
    public class CreateFavoriteCommand : IRequest<IResult>
    {
        public string BlogId { get; set; }

        public bool IsLike { get; set; }
    }
}
