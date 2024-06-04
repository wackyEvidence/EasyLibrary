namespace EasyLibrary.Core.Abstractions
{
    public interface IMapper<TSource, TDestination>
    {
        TDestination Map(TSource source);
    }
}
