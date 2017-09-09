using System;
using System.Threading.Tasks;
using MyMapper.Extensions;

namespace MyMapper.Converters
{
    /// <summary>
    /// EntityConverter : Converts an entity to another entity
    /// </summary>
    /// <typeparam name="TSource">The source entity</typeparam>
    /// <typeparam name="TDestination">The destination entity</typeparam>
    /// <exception cref="ArgumentNullException"></exception>
    public class EntityConverter<TSource, TDestination> : ITypeConverter<TSource, TDestination>, ITypeConverterAsync<TSource, TDestination>
        where TSource : class
        where TDestination : class, new()
    {        
        public TDestination Convert(TSource source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.AsDictionary(typeof(TSource)).ToObject<TDestination>();            
        }

        public async Task<TDestination> ConvertAsync(TSource source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return await Task.Run(() =>source.AsDictionary(typeof(TSource)).ToObject<TDestination>());
        }

    }    
}