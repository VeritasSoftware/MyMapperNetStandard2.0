/////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////    Mapper.cs     //////////////////////////////////////////////
///////////////////////////////////////// Author: Shantanu //////////////////////////////////////////////
///////////////////////////////////////// Date: 7-Oct-2015 //////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Threading.Tasks;

namespace MyMapper
{
    /// <summary>
    /// MyMapper - Generic static class
    /// </summary>
    /// <typeparam name="TSource">The source</typeparam>
    /// <typeparam name="TDestination">The destination</typeparam>
    public static class Mapper<TSource, TDestination>
        where TSource : class
        where TDestination : class, new()
    {
        /// <summary>
        /// Map source to destination
        /// </summary>
        /// <param name="source">The source</param>
        /// <param name="automap">Flag to use auto mapping (reflective)</param>
        /// <returns cref="IMyMapperRules">The mapper rules</returns>
        /// <exception cref="ArgumentNullException">Throws ArgumentNullException exception</exception>
        public static IMyMapperRules<TSource, TDestination> Map(TSource source, bool automap = true)
        {
            IMyMapper<TSource, TDestination> mapper = new MyMapper<TSource, TDestination>();            

            mapper.Map(source, automap);            

            return mapper as IMyMapperRules<TSource, TDestination>;
        }

        /// <summary>
        /// Map source to destination async
        /// </summary>
        /// <param name="source">The source</param>
        /// <param name="automap">Flag to use auto mapping (reflective)</param>
        /// <returns cref="IMyMapperRules">The mapper rules</returns>
        /// <exception cref="ArgumentNullException">Throws ArgumentNullException exception</exception>
        public async static Task<IMyMapperRules<TSource, TDestination>> MapAsync(TSource source, bool automap = true)
        {
            IMyMapper<TSource, TDestination> mapper = new MyMapper<TSource, TDestination>();

            await mapper.MapAsync(source, automap);

            return mapper as IMyMapperRules<TSource, TDestination>;
        }

        [Obsolete("Exec is deprecated.", true)]
        public static TDestination Exec(TSource source, Func<TSource, IMyMapper<TSource, TDestination>, TDestination> map)
        {
            IMyMapper<TSource, TDestination> mapper = new MyMapper<TSource, TDestination>();

            return mapper.Exec(source, map);
        }

        public static TDestination Exec<TConverter>(TSource source)
            where TConverter : ITypeConverter<TSource, TDestination>, new()
        {
            IMyMapper<TSource, TDestination> mapper = new MyMapper<TSource, TDestination>();

            return mapper.Exec<TConverter>(source);
        }
    }
}