using FavoriteService.Result;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using MongoDB;
using MongoDB.Driver;
using FavoriteService.Model;
using FavoriteService.Extension;
using System;

namespace FavoriteService.Handler.Favorite.Command
{
    public class CreateFavoriteCommandHandler : IRequestHandler<CreateFavoriteCommand, IResult>
    {
        private readonly IMongoDatabase _database;
        public CreateFavoriteCommandHandler(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task<IResult> Handle(CreateFavoriteCommand request, CancellationToken cancellationToken)
        {
            var data = new Model.Favorite()
            {
                BlogId=request.BlogId,
                IsLike=request.IsLike
            };
           await _database.Collection<Model.Favorite>()
                .InsertOneAsync(data, cancellationToken: cancellationToken);

            if (!string.IsNullOrEmpty(data.Id))
                return new SuccessResult("Added");
            else
                return new ErrorResult("Not Added");

        }
    }
}
