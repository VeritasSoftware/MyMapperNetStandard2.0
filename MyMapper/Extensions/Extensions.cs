using MyMapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace System
{    
    /// <summary>
    /// Static class MyMapperObjectExtensions
    /// </summary>
    public static class MyMapperObjectExtensions
    {        
        /// <summary>
        /// MyMapper - Map extension method
        /// </summary>
        /// <typeparam name="TSource">The source type</typeparam>
        /// <typeparam name="TDestination">The destination type</typeparam>
        /// <param name="obj">The source object</param>
        /// <returns>The destination object <see cref="TDestination"/></returns>
        public static TDestination Map<TSource, TDestination>(this TSource obj)
            where TSource : class
            where TDestination : class, new()
        {
            if (obj == null)
                return null;

            IMyMapper<TSource, TDestination> mapper =
                                    new MyMapper<TSource, TDestination>();

            return mapper.Map(obj, true).Exec();
        }
#if !NET4
        /// <summary>
        /// MyMapper - MapAsync extension method
        /// </summary>
        /// <typeparam name="TSource">The source type</typeparam>
        /// <typeparam name="TDestination">The destination type</typeparam>
        /// <param name="obj">The source object</param>
        /// <returns>The destination object <see cref="TDestination"/></returns>
        public static async Task<TDestination> MapAsync<TSource, TDestination>(this TSource obj)
            where TSource : class
            where TDestination : class, new()
        {
            if (obj == null)
                return null;

            IMyMapper<TSource, TDestination> mapper =
                                    new MyMapper<TSource, TDestination>();

            return await Task.Run(() => mapper.Map(obj, true).Exec());            
        }
#endif
        /// <summary>
        /// MyMapper - Map extension method
        /// </summary>
        /// <typeparam name="TSource">The source type</typeparam>
        /// <typeparam name="TDestination">The destination type</typeparam>
        /// <param name="obj">The source object</param>
        /// <param name="map">MyMapper rules for the mapping</param>
        /// <param name="automap">Flag to indicate if to use automapping</param>
        /// <returns>The destination object <see cref="TDestination"/></returns>
        public static TDestination Map<TSource, TDestination>(this TSource obj,
                    Func<IMyMapperRules<TSource, TDestination>, TDestination> map = null,
                    bool automap = true
            )
            where TSource : class
            where TDestination : class, new()
        {
            if (obj == null)
                return null;

            IMyMapper<TSource, TDestination> mapper =
                                    new MyMapper<TSource, TDestination>();

            mapper.Map(obj, automap);

            if (map != null)
                return map(mapper);
            else
                return mapper.Exec();
        }
#if !NET4
        /// <summary>
        /// MyMapper - MapAsync extension method
        /// </summary>
        /// <typeparam name="TSource">The source type</typeparam>
        /// <typeparam name="TDestination">The destination type</typeparam>
        /// <param name="obj">The source object</param>
        /// <param name="map">MyMapper rules for the mapping</param>
        /// <param name="automap">Flag to indicate if to use automapping</param>
        /// <returns>The Task of the destination object <see cref="Task{TDestination}"/></returns>
        public static async Task<TDestination> MapAsync<TSource, TDestination>(this TSource obj,
                    Func<IMyMapperRules<TSource, TDestination>, TDestination> map = null,
                    bool automap = true
            )
            where TSource : class
            where TDestination : class, new()
        {
            if (obj == null)
                return null;

            IMyMapper<TSource, TDestination> mapper =
                                    new MyMapper<TSource, TDestination>();

            return await Task.Run(() =>
            {
                mapper.Map(obj, automap);

                if (map != null)
                    return map(mapper);
                else
                    return mapper.Exec();
            });            
        }
#endif
    }
}

namespace System.Collections.Generic
{
    /// <summary>
    /// Static class MyMapperEnumerableExtensions
    /// </summary>
    public static class MyMapperEnumerableExtensions
    {
        /// <summary>
        /// MyMapper - Map extension method
        /// </summary>
        /// <typeparam name="TSourceList">The source list type</typeparam>
        /// <typeparam name="TDestinationList">The destination list type</typeparam>
        /// <param name="source">The source list</param>
        /// <param name="map">MyMapper rules for the mapping</param>
        /// <param name="automap">Flag to indicate if to use automapping</param>
        /// <returns>The destination list <see cref="IEnumerable{TDestinationList}"/></returns>
        public static IEnumerable<TDestinationList> Map<TSourceList, TDestinationList>(
                    this IEnumerable<TSourceList> source,
                    Func<IMyMapperRules<TSourceList, TDestinationList>, TDestinationList> map = null,
                    bool automap = true
            )
            where TSourceList: class
            where TDestinationList: class, new()
        {
            if (source == null)
                return null;

            IMyMapper<TSourceList, TDestinationList> mapper;

            return source.Select(src =>
            {
                if (src == null)
                    return null;

                mapper = new MyMapper<TSourceList, TDestinationList>();

                mapper.Map(src, automap);

                if (map != null)
                    return map(mapper);
                else
                    return mapper.Exec();
            });
        }
#if !NET4
        /// <summary>
        /// MyMapper - MapAsync extension method
        /// </summary>
        /// <typeparam name="TSourceList">The source list type</typeparam>
        /// <typeparam name="TDestinationList">The destination list type</typeparam>
        /// <param name="source">The source list</param>
        /// <param name="map">MyMapper rules for the mapping</param>
        /// <param name="automap">Flag to indicate if to use automapping</param>
        /// <returns>The destination list <see cref="Task{IEnumerable{TDestinationList}}"/></returns>
        public static async Task<IEnumerable<TDestinationList>> MapAsync<TSourceList, TDestinationList>(
                    this IEnumerable<TSourceList> source,
                    Func<IMyMapperRules<TSourceList, TDestinationList>, TDestinationList> map = null,
                    bool automap = true
            )
            where TSourceList : class
            where TDestinationList : class, new()
        {
            if (source == null)
                return null;

            IMyMapper<TSourceList, TDestinationList> mapper;

            return await Task.Run(() =>
            {
                return source.Select(src =>
                {
                    if (src == null)
                        return null;

                    mapper = new MyMapper<TSourceList, TDestinationList>();

                    mapper.Map(src, automap);

                    if (map != null)
                        return map(mapper);
                    else
                        return mapper.Exec();
                });
            });            
        }
#endif

        /// <summary>
        /// MyMapper - Map Parallel extension method - Uses PLINQ
        /// </summary>
        /// <typeparam name="TSourceList">The source list type</typeparam>
        /// <typeparam name="TDestinationList">The destination list type</typeparam>
        /// <param name="source">The source list</param>
        /// <param name="map">MyMapper rules for the mapping</param>
        /// <param name="automap">Flag to indicate if to use automapping</param>
        /// <returns>The destination list <see cref="IEnumerable{TDestinationList}"/></returns>
        public static IEnumerable<TDestinationList> MapParallel<TSourceList, TDestinationList>(
                    this IEnumerable<TSourceList> source,
                    Func<IMyMapperRules<TSourceList, TDestinationList>, TDestinationList> map = null,
                    bool automap = true
            )
            where TSourceList : class
            where TDestinationList : class, new()
        {
            if (source == null)
                return null;

            IMyMapper<TSourceList, TDestinationList> mapper =
                                    new MyMapper<TSourceList, TDestinationList>();

            return source.AsParallel().Select(src =>
            {
                if (src == null)
                    return null;

                mapper.Map(src, automap);

                if (map != null)
                    return map(mapper);
                else
                    return mapper.Exec();
            });
        }
    }
}

namespace System.Threading.Tasks
{
    /// <summary>
    /// Static class MyMapperTaskExtensions
    /// </summary>
    public static class MyMapperTaskExtensions
    {
        public static Task<IMyMapperRules<TSource, TDestination>> With<TSource, TDestination, TProperty>(
                                                                                    this Task<IMyMapperRules<TSource, TDestination>> task,
                                                                                    Func<TSource, TProperty> source,
                                                                                    Action<TDestination, TProperty> destination
                                                                                )
            where TSource : class
            where TDestination : class, new()
        {
            return task.ContinueWith(myMapper => myMapper.Result.With(source, destination));
        }

        public static Task<IMyMapperRules<TSource, TDestination>> With<TSource, TDestination, TSourceResult, TDestinationResult>(
                                                                                    this Task<IMyMapperRules<TSource, TDestination>> task,
                                                                                    Func<TSource, TSourceResult> source,
                                                                                    Action<TDestination, TDestinationResult> destination,
                                                                                    Func<TSourceResult, TDestinationResult> map
                                                                                )
           where TSource : class
           where TDestination : class, new()
           where TDestinationResult : class, new()
        {
            return task.ContinueWith(myMapper => myMapper.Result.With(source, destination, map));
        }

        public static Task<IMyMapperRules<TSource, TDestination>> With<TSource, TDestination, TSourceResult, TDestinationResult>(
                                                                                this Task<IMyMapperRules<TSource, TDestination>> task,
                                                                                Func<TSource, ICollection<TSourceResult>> source,
                                                                                Action<TDestination, ICollection<TDestinationResult>> destination,
                                                                                Func<TSourceResult, TDestinationResult> map
                                                                            )
            where TSource : class
            where TDestination : class, new()
            where TSourceResult : class
            where TDestinationResult : class, new()
        {
            return task.ContinueWith(myMapper => myMapper.Result.With(source, destination, map));
        }

        public static Task<IMyMapperRules<TSource, TDestination>> With<TSource, TDestination, TSourceResult, TDestinationResult>(
                                                                            this Task<IMyMapperRules<TSource, TDestination>> task,
                                                                            Func<TSource, IEnumerable<TSourceResult>> source,
                                                                            Action<TDestination, IEnumerable<TDestinationResult>> destination,
                                                                            Func<TSourceResult, TDestinationResult> map
                                                                        )
            where TSource : class
            where TDestination : class, new()
            where TSourceResult : class
            where TDestinationResult : class, new()
        {
            return task.ContinueWith(myMapper => myMapper.Result.With(source, destination, map));
        }

        public static Task<IMyMapperRules<TSource, TDestination>> With<TSource, TDestination, TSourceResult, TDestinationResult>(
                                                                            this Task<IMyMapperRules<TSource, TDestination>> task,
                                                                            Func<TSource, IList<TSourceResult>> source,
                                                                            Action<TDestination, IList<TDestinationResult>> destination,
                                                                            Func<TSourceResult, TDestinationResult> map
                                                                        )
            where TSource : class
            where TDestination : class, new()
            where TSourceResult : class
            where TDestinationResult : class, new()
        {
            return task.ContinueWith(myMapper => myMapper.Result.With(source, destination, map));
        }

        public static Task<IMyMapperRules<TSource, TDestination>> With<TSource, TDestination, TDestinationResult>(
                                                                            this Task<IMyMapperRules<TSource, TDestination>> task,
                                                                            Func<TSource, DataTable> source,
                                                                            Action<TDestination, IList<TDestinationResult>> destination,
                                                                            Func<DataRow, TDestinationResult> map
                                                                        )
            where TSource : class
            where TDestination : class, new()
            where TDestinationResult : class, new()
        {
            return task.ContinueWith(myMapper => myMapper.Result.With(source, destination, map));
        }

        public static Task<IMyMapperRules<TSource, TDestination>> With<TSource, TDestination, TSourceKey, TSourceValue, TDestinationKey, TDestinationValue>(
                                                                            this Task<IMyMapperRules<TSource, TDestination>> task,
                                                                            Func<TSource, Dictionary<TSourceKey, TSourceValue>> source,
                                                                            Action<TDestination, Dictionary<TDestinationKey, TDestinationValue>> destination,
                                                                            Func<TSourceKey, TDestinationKey> mapKey, Func<TSourceValue, TDestinationValue> mapValue
                                                                        )
            where TSource : class
            where TDestination : class, new()
        {
            return task.ContinueWith(myMapper => myMapper.Result.With(source, destination, mapKey, mapValue));
        }

        public static Task<IMyMapperRules<TSource, TDestination>> ParallelWith<TSource, TDestination, TSourceResult, TDestinationResult>(
                                                                            this Task<IMyMapperRules<TSource, TDestination>> task,
                                                                            Func<TSource, ICollection<TSourceResult>> source,
                                                                            Action<TDestination, ICollection<TDestinationResult>> destination,
                                                                            Func<TSourceResult, TDestinationResult> map
                                                                        )
            where TSource : class
            where TDestination : class, new()
            where TSourceResult : class
            where TDestinationResult : class, new()
        {
            return task.ContinueWith(myMapper => myMapper.Result.ParallelWith(source, destination, map));
        }

        public static Task<IMyMapperRules<TSource, TDestination>> ParallelWith<TSource, TDestination, TSourceResult, TDestinationResult>(
                                                                            this Task<IMyMapperRules<TSource, TDestination>> task,
                                                                            Func<TSource, IEnumerable<TSourceResult>> source,
                                                                            Action<TDestination, IEnumerable<TDestinationResult>> destination,
                                                                            Func<TSourceResult, TDestinationResult> map
                                                                        )
            where TSource : class
            where TDestination : class, new()
            where TSourceResult : class
            where TDestinationResult : class, new()
        {
            return task.ContinueWith(myMapper => myMapper.Result.ParallelWith(source, destination, map));
        }

        public static Task<IMyMapperRules<TSource, TDestination>> ParallelWith<TSource, TDestination, TSourceResult, TDestinationResult>(
                                                                            this Task<IMyMapperRules<TSource, TDestination>> task,
                                                                            Func<TSource, IList<TSourceResult>> source,
                                                                            Action<TDestination, IList<TDestinationResult>> destination,
                                                                            Func<TSourceResult, TDestinationResult> map
                                                                        )
            where TSource : class
            where TDestination : class, new()
            where TSourceResult : class
            where TDestinationResult : class, new()
        {
            return task.ContinueWith(myMapper => myMapper.Result.ParallelWith(source, destination, map));
        }

        public static Task<IMyMapperRules<TSource, TDestination>> WithWhen<TSource, TDestination, TProperty>(
                                                                                    this Task<IMyMapperRules<TSource, TDestination>> task,
                                                                                    Func<TSource, bool> when,
                                                                                    Func<TSource, TProperty> source,
                                                                                    Action<TDestination, TProperty> destination
                                                                                )

            where TSource : class
            where TDestination : class, new()
        {
            return task.ContinueWith(myMapper => myMapper.Result.WithWhen(when, source, destination));
        }

        public static Task<IMyMapperRules<TSource, TDestination>> When<TSource, TDestination>(
                                                                                    this Task<IMyMapperRules<TSource, TDestination>> task,
                                                                                    Func<TSource, bool> when,
                                                                                    Action<IMyMapperRules<TSource, TDestination>> then
                                                                                )

            where TSource : class
            where TDestination : class, new()
        {
            return task.ContinueWith(myMapper => myMapper.Result.When(when, then));
        }

        public static Task<IMyMapperSwitch<TSource, TDestination, TProperty>> Switch<TSource, TDestination, TProperty>(
                                                                                    this Task<IMyMapperRules<TSource, TDestination>> task,
                                                                                    Func<TSource, TProperty> on
                                                                                )

            where TSource : class
            where TDestination : class, new()
        {
            return task.ContinueWith(myMapper => myMapper.Result.Switch(on));
        }

        public static Task<IMyMapperSwitchElse<TSource, TDestination, TSourceProperty>> Case<TSource, TDestination, TSourceProperty>(
                                                                            this Task<IMyMapperSwitch<TSource, TDestination, TSourceProperty>> task,
                                                                            Func<TSourceProperty, bool> when, Action<TDestination, TSourceProperty> then
                                                                        )

            where TSource : class
            where TDestination : class, new()
        {
            return task.ContinueWith(myMapper => myMapper.Result.Case(when, then));
        }

        public static Task<IMyMapperSwitchElse<TSource, TDestination, TSourceProperty>> CaseMap<TSource, TDestination, TSourceProperty>(
                                                                            this Task<IMyMapperSwitch<TSource, TDestination, TSourceProperty>> task,
                                                                            Func<TSourceProperty, bool> when, Action<IMyMapperRules<TSource, TDestination>> then
                                                                        )

            where TSource : class
            where TDestination : class, new()
        {
            return task.ContinueWith(myMapper => myMapper.Result.CaseMap(when, then));
        }

        public static Task<IMyMapperSwitchElse<TSource, TDestination, TSourceProperty>> Case<TSource, TDestination, TSourceProperty>(
                                                                            this Task<IMyMapperSwitchElse<TSource, TDestination, TSourceProperty>> task,
                                                                            Func<TSourceProperty, bool> when, Action<TDestination, TSourceProperty> then
                                                                        )

            where TSource : class
            where TDestination : class, new()
        {
            return task.ContinueWith(myMapper => myMapper.Result.Case(when, then));
        }

        public static Task<IMyMapperSwitchElse<TSource, TDestination, TSourceProperty>> CaseMap<TSource, TDestination, TSourceProperty>(
                                                                            this Task<IMyMapperSwitchElse<TSource, TDestination, TSourceProperty>> task,
                                                                            Func<TSourceProperty, bool> when, Action<IMyMapperRules<TSource, TDestination>> then
                                                                        )

            where TSource : class
            where TDestination : class, new()
        {
            return task.ContinueWith(myMapper => myMapper.Result.CaseMap(when, then));
        }

        public static Task<IMyMapperSwitchEnd<TSource, TDestination>> ElseMap<TSource, TDestination, TSourceProperty>(
                                                                            this Task<IMyMapperSwitchElse<TSource, TDestination, TSourceProperty>> task,
                                                                            Action<IMyMapperRules<TSource, TDestination>> then
                                                                        )

            where TSource : class
            where TDestination : class, new()
        {
            return task.ContinueWith(myMapper => myMapper.Result.ElseMap(then));
        }

        public static Task<IMyMapperSwitchEnd<TSource, TDestination>> Else<TSource, TDestination, TSourceProperty>(
                                                                            this Task<IMyMapperSwitchElse<TSource, TDestination, TSourceProperty>> task,
                                                                            Action<TDestination, TSourceProperty> then
                                                                        )

            where TSource : class
            where TDestination : class, new()
        {
            return task.ContinueWith(myMapper => myMapper.Result.Else(then));
        }

        public static Task<IMyMapperRules<TSource, TDestination>> End<TSource, TDestination, TSourceProperty>(
                                                                            this Task<IMyMapperSwitchElse<TSource, TDestination, TSourceProperty>> task
                                                                        )

            where TSource : class
            where TDestination : class, new()
        {
            return task.ContinueWith(myMapper => myMapper.Result.End());
        }

        public static Task<IMyMapperRules<TSource, TDestination>> End<TSource, TDestination>(
                                                                            this Task<IMyMapperSwitchEnd<TSource, TDestination>> task
                                                                        )

            where TSource : class
            where TDestination : class, new()
        {
            return task.ContinueWith(myMapper => myMapper.Result.End());
        }

        public static Task<TDestination> Exec<TSource, TDestination>(
                                                                        this Task<IMyMapperRules<TSource, TDestination>> task
                                                                     )

            where TSource : class
            where TDestination : class, new()
        {
            return task.ContinueWith(myMapper => myMapper.Result.Exec());
        }
    }
}