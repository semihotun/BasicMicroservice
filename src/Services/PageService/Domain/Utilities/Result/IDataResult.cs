
namespace Domain.Utilities
{
    public interface IDataResult<out T> : IResult
    {
        T Data { get; }
    }
}
