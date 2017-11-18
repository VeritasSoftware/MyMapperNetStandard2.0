using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace MyMapper
{
#if !NET4
    using System.Threading.Tasks;
#endif
    using MyMapper.Converters;

    /// <summary>
    /// MyMapper - Generic class
    /// </summary>
    /// <typeparam name="TSource">The source</typeparam>
    /// <typeparam name="TDestination">The destination</typeparam>    
    public class MyMapper<TSource, TDestination> : IMyMapper<TSource, TDestination>
        where TSource : class
        where TDestination : class, new()
    {
        TSource Source { get; set; }
        TDestination Destination { get; set; }

        /// <summary>
        /// Map source to destination
        /// </summary>
        /// <param name="source">The source</param>
        /// <param name="automap">Flag to use auto mapping (reflective)</param>
        /// <returns cref="IMyMapperRules">The mapper rules</returns>
        /// <exception cref="ArgumentNullException">Throws ArgumentNullException exception</exception>
        public IMyMapperRules<TSource, TDestination> Map(TSource source, bool automap = true)
        {
            if (source == null)
                throw new ArgumentNullException();

            this.Source = source;

            this.Destination = new TDestination();

            if (automap)
                this.Destination = new EntityConverter<TSource, TDestination>().Convert(this.Source);

            return this;
        }
#if !NET4
        public async Task<IMyMapperRules<TSource, TDestination>> MapAsync(TSource source, bool automap = true)
        {
            if (source == null)
                throw new ArgumentNullException();

            this.Source = source;

            this.Destination = new TDestination();

            if (automap)
                this.Destination = await new EntityConverter<TSource, TDestination>().ConvertAsync(this.Source);

            return this;
        }
#endif
        /// <summary>
        /// With
        /// </summary>
        /// <typeparam name="TProperty">The proerty</typeparam>
        /// <param name="source">The source</param>
        /// <param name="destination">The destination</param>
        /// <returns><see cref="IMyMapperRules<TSource, TDestination>"/></returns>
        public IMyMapperRules<TSource, TDestination> With<TProperty>(
                                                                        Func<TSource, TProperty> source,
                                                                        Action<TDestination, TProperty> destination
                                                                    )
        {
            var sourceProp = source(this.Source);

            destination(this.Destination, sourceProp);

            return this;
        }

        /// <summary>
        /// With
        /// </summary>
        /// <typeparam name="TSourceResult">The source result</typeparam>
        /// <typeparam name="TDestinationResult">The destination result</typeparam>
        /// <param name="source">The source</param>
        /// <param name="destination">The destination</param>
        /// <param name="map">The source to destination map</param>
        /// <returns><see cref="IMyMapperRules<TSource, TDestination>"/></returns>
        public IMyMapperRules<TSource, TDestination> With<TSourceResult, TDestinationResult>(
                                                        Func<TSource, TSourceResult> source,
                                                        Action<TDestination, TDestinationResult> destination,
                                                        Func<TSourceResult, TDestinationResult> map
                                                    )
            where TDestinationResult : class, new()
        {
            destination(this.Destination, map(source(this.Source)));

            return this;
        }

        /// <summary>
        /// With
        /// </summary>
        /// <typeparam name="TSourceResult">The source result type</typeparam>
        /// <typeparam name="TDestinationResult">The destination result type</typeparam>
        /// <param name="source">The source</param>
        /// <param name="destination">The destination</param>
        /// <param name="map">The source to destination map</param>
        /// <returns><see cref="IMyMapperRules<TSource, TDestination>"/></returns>
        public IMyMapperRules<TSource, TDestination> With<TSourceResult, TDestinationResult>(
                                                        Func<TSource, ICollection<TSourceResult>> source,
                                                        Action<TDestination, ICollection<TDestinationResult>> destination,
                                                        Func<TSourceResult, TDestinationResult> map
                                                    )
            where TSourceResult : class
            where TDestinationResult : class, new()
        {
            var sourceList = source(this.Source);

            var destinationList = sourceList.Select(map);

            destination(this.Destination, destinationList.ToList());

            return this;
        }

        /// <summary>
        /// Parallel With - Uses PLINQ
        /// </summary>
        /// <typeparam name="TSourceResult">The source result type</typeparam>
        /// <typeparam name="TDestinationResult">The destination result type</typeparam>
        /// <param name="source">The source</param>
        /// <param name="destination">The destination</param>
        /// <param name="map">The source to destination map</param>
        /// <returns><see cref="IMyMapperRules<TSource, TDestination>"/></returns>
        public IMyMapperRules<TSource, TDestination> ParallelWith<TSourceResult, TDestinationResult>(
                                                Func<TSource, ICollection<TSourceResult>> source,
                                                Action<TDestination, ICollection<TDestinationResult>> destination,
                                                Func<TSourceResult, TDestinationResult> map
                                            )
        where TSourceResult : class
        where TDestinationResult : class, new()
        {
            var sourceList = source(this.Source);

            var destinationList = sourceList.AsParallel().Select(map);

            destination(this.Destination, destinationList.ToList());

            return this;
        }

        /// <summary>
        /// With
        /// </summary>
        /// <typeparam name="TSourceResult">The source result type</typeparam>
        /// <typeparam name="TDestinationResult">The destination result type</typeparam>
        /// <param name="source">The source</param>
        /// <param name="destination">The destination</param>
        /// <param name="map">The source to destination map</param>
        /// <returns>><see cref="IMyMapperRules<TSource, TDestination>"/></returns>
        public IMyMapperRules<TSource, TDestination> With<TSourceResult, TDestinationResult>(
                                                        Func<TSource, IEnumerable<TSourceResult>> source,
                                                        Action<TDestination, IEnumerable<TDestinationResult>> destination,
                                                        Func<TSourceResult, TDestinationResult> map
                                                    )
            where TSourceResult : class
            where TDestinationResult : class, new()
        {
            var sourceList = source(this.Source);

            var destinationList = sourceList.Select(map);

            destination(this.Destination, destinationList.ToList());

            return this;
        }

        /// <summary>
        /// Parallel With - Uses PLINQ
        /// </summary>
        /// <typeparam name="TSourceResult">The source result type</typeparam>
        /// <typeparam name="TDestinationResult">The destination result type</typeparam>
        /// <param name="source">The source</param>
        /// <param name="destination">The destination</param>
        /// <param name="map">The source to destination map</param>
        /// <returns>><see cref="IMyMapperRules<TSource, TDestination>"/></returns>
        public IMyMapperRules<TSource, TDestination> ParallelWith<TSourceResult, TDestinationResult>(
                                                        Func<TSource, IEnumerable<TSourceResult>> source,
                                                        Action<TDestination, IEnumerable<TDestinationResult>> destination,
                                                        Func<TSourceResult, TDestinationResult> map
                                                    )
            where TSourceResult : class
            where TDestinationResult : class, new()
        {
            var sourceList = source(this.Source);

            var destinationList = sourceList.AsParallel().Select(map);

            destination(this.Destination, destinationList.ToList());

            return this;
        }

        /// <summary>
        /// With
        /// </summary>
        /// <typeparam name="TSourceResult">The source result type</typeparam>
        /// <typeparam name="TDestinationResult">The destination result type</typeparam>
        /// <param name="source">The source</param>
        /// <param name="destination">The destination</param>
        /// <param name="map">The source to destination map</param>
        /// <returns>><see cref="IMyMapperRules<TSource, TDestination>"/></returns>
        public IMyMapperRules<TSource, TDestination> With<TSourceResult, TDestinationResult>(
                                                        Func<TSource, IList<TSourceResult>> source,
                                                        Action<TDestination, IList<TDestinationResult>> destination,
                                                        Func<TSourceResult, TDestinationResult> map
                                                    )
            where TSourceResult : class
            where TDestinationResult : class, new()
        {
            var sourceList = source(this.Source);

            var destinationList = sourceList.Select(map);

            destination(this.Destination, destinationList.ToList());

            return this;
        }

        /// <summary>
        /// Parallel With - Uses PLINQ
        /// </summary>
        /// <typeparam name="TSourceResult">The source result type</typeparam>
        /// <typeparam name="TDestinationResult">The destination result type</typeparam>
        /// <param name="source">The source</param>
        /// <param name="destination">The destination</param>
        /// <param name="map">The source to destination map</param>
        /// <returns>><see cref="IMyMapperRules<TSource, TDestination>"/></returns>
        public IMyMapperRules<TSource, TDestination> ParallelWith<TSourceResult, TDestinationResult>(
                                                Func<TSource, IList<TSourceResult>> source,
                                                Action<TDestination, IList<TDestinationResult>> destination,
                                                Func<TSourceResult, TDestinationResult> map
                                            )
            where TSourceResult : class
            where TDestinationResult : class, new()
        {
            var sourceList = source(this.Source);

            var destinationList = sourceList.AsParallel().Select(map);

            destination(this.Destination, destinationList.ToList());

            return this;
        }

        /// <summary>
        /// With
        /// </summary>
        /// <typeparam name="TDestinationResult">The destination result type</typeparam>
        /// <param name="source">The source datatable</param>
        /// <param name="destination">The destination</param>
        /// <param name="map">The source to destination map</param>
        /// <returns>><see cref="IMyMapperRules<TSource, TDestination>"/></returns>
        public IMyMapperRules<TSource, TDestination> With<TDestinationResult>(
                                                        Func<TSource, DataTable> source,
                                                        Action<TDestination, IList<TDestinationResult>> destination,
                                                        Func<DataRow, TDestinationResult> map
                                                    )
            where TDestinationResult : class, new()
        {
            DataTable sourceList = source(this.Source);

            var destinationList = new List<TDestinationResult>();

            foreach (DataRow row in sourceList.Rows)
            {
                destinationList.Add(map(row));
            }

            destination(this.Destination, destinationList);

            return this;
        }

        /// <summary>
        /// With When
        /// </summary>
        /// <typeparam name="TProperty">The property type</typeparam>
        /// <param name="when">The when condition</param>
        /// <param name="source">The source</param>
        /// <param name="destination">The destination</param>
        /// <returns><see cref="IMyMapperRules<TSource, TDestination>"/></returns>
        public IMyMapperRules<TSource, TDestination> WithWhen<TProperty>(
                                                                        Func<TSource, bool> when,
                                                                        Func<TSource, TProperty> source,
                                                                        Action<TDestination, TProperty> destination
                                                                    )
        {
            if (!when(this.Source))
            {
                return this;
            }

            var sourceProp = source(this.Source);

            destination(this.Destination, sourceProp);

            return this;
        }

        /// <summary>
        /// When
        /// </summary>
        /// <param name="when">The when condition</param>
        /// <param name="then">The then action</param>
        /// <returns><see cref="IMyMapperRules<TSource, TDestination>"/></returns>
        public IMyMapperRules<TSource, TDestination> When(
                                                        Func<TSource, bool> when,
                                                        Action<IMyMapperRules<TSource, TDestination>> then
                                                    )
        {
            if (when(this.Source))
            {
                then(this);
            }

            return this;
        }

        /// <summary>
        /// Switch
        /// </summary>
        /// <typeparam name="TProperty">The property type</typeparam>
        /// <param name="on">The property for the switch</param>
        /// <returns><see cref="IMyMapperSwitch<TSource, TDestination, TProperty>"/></returns>
        public IMyMapperSwitch<TSource, TDestination, TProperty> Switch<TProperty>(Func<TSource, TProperty> on)
        {
            IMyMapperSwitch<TSource, TDestination, TProperty> sw = new MyMapperSwitch<TSource, TDestination, TProperty>(
                                                                                           this.Source,
                                                                                           on(this.Source),
                                                                                           this
                                                                                       );

            return sw;
        }

        /// <summary>
        /// Exec
        /// </summary>
        /// <returns>The destination <see cref="TDestination"/></returns>
        public TDestination Exec()
        {
            return this.Destination;
        }
#if !NET4
        /// <summary>
        /// Exec Async
        /// </summary>
        /// <returns>The destination <see cref="Task{TDestination}"/></returns>
        [Obsolete("ExecAsync is deprecated.", true)]
        public async Task<TDestination> ExecAsync()
        {
            return await Task.Run(() => this.Destination);
        }
#endif

        [Obsolete("Exec is deprecated.", true)]
        public TDestination Exec(TSource source, Func<TSource, IMyMapper<TSource, TDestination>, TDestination> map)
        {
            return map(source, this);
        }

        /// <summary>
        /// Exec
        /// </summary>
        /// <typeparam name="TConverter">The converter type</typeparam>
        /// <param name="source">The source</param>
        /// <returns>The destination <see cref="TDestination"/></returns>
        public TDestination Exec<TConverter>(TSource source)
            where TConverter : ITypeConverter<TSource, TDestination>, new()
        {
            return new TConverter().Convert(source);
        }
#if !NET4
        /// <summary>
        /// Exec Async
        /// </summary>
        /// <typeparam name="TConverter">The converter type</typeparam>
        /// <param name="source">The source</param>
        /// <returns>The destination <see cref="Task{TDestination}"/></returns>
        public async Task<TDestination> ExecAsync<TConverter>(TSource source)
            where TConverter : ITypeConverterAsync<TSource, TDestination>, new()
        {
            return await new TConverter().ConvertAsync(source);
        }
#endif
    }    
}