#if !NET4
using System.Threading.Tasks;
#endif

namespace MyMapper
{
    public interface ITypeConverter<TSource, TDestination>
        where TDestination : class, new()
    {
        TDestination Convert(TSource source, TDestination entity = null);
    }

#if !NET4
    public interface ITypeConverterAsync<TSource, TDestination>
        where TDestination : class, new()
    {
        Task<TDestination> ConvertAsync(TSource source, TDestination entity = null);
    }
#endif
}
