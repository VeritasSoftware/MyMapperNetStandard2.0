using System;
#if !NET4
using System.Threading.Tasks;
#endif
using MyMapper.Extensions;

namespace MyMapper.Converters
{
    /// <summary>
    /// EntityConverter : Converts an entity to another entity
    /// </summary>
    /// <typeparam name="TSource">The source entity</typeparam>
    /// <typeparam name="TDestination">The destination entity</typeparam>
    /// <exception cref="ArgumentNullException"></exception>
    public class EntityConverter<TSource, TDestination> : ITypeConverter<TSource, TDestination>
#if !NET4
                                                            , ITypeConverterAsync<TSource, TDestination>
#endif
        where TSource : class
        where TDestination : class, new()
    {        
        public TDestination Convert(TSource source, TDestination destination = null)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return destination == null ? source.AsDictionary(typeof(TSource)).ToObject<TDestination>()
                                       : source.AsDictionary(typeof(TSource)).ToObject(destination);
        }        
#if !NET4
        public async Task<TDestination> ConvertAsync(TSource source, TDestination destination = null)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return await Task.Run(() => this.Convert(source, destination));
        }        
#endif
    }    
}