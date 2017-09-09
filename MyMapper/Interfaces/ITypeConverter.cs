using System.Threading.Tasks;

namespace MyMapper
{
    public interface ITypeConverter<TSource, TDestination>
    {
        TDestination Convert(TSource source);
    }

    public interface ITypeConverterAsync<TSource, TDestination>
    {
        Task<TDestination> ConvertAsync(TSource source);
    }
}
