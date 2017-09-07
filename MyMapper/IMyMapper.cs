using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace MyMapper
{
    /// <summary>
    /// IMyMapperRules - Generic interface
    /// </summary>
    /// <typeparam name="TSource">The source</typeparam>
    /// <typeparam name="TDestination">The destination</typeparam>
    public interface IMyMapperRules<TSource, TDestination>
        where TSource : class
        where TDestination : class, new()
    {
        IMyMapperRules<TSource, TDestination> With<TProperty>(
                                                            Func<TSource, TProperty> source,
                                                            Action<TDestination, TProperty> destination
                                                        );

        IMyMapperRules<TSource, TDestination> With<TSourceResult, TDestinationResult>(
                                                        Func<TSource, TSourceResult> source,
                                                        Action<TDestination, TDestinationResult> destination,
                                                        Func<TSourceResult, TDestinationResult> map
                                                    )
            where TDestinationResult : class, new();

        IMyMapperRules<TSource, TDestination> With<TSourceResult, TDestinationResult>(
                                                        Func<TSource, ICollection<TSourceResult>> source,
                                                        Action<TDestination, ICollection<TDestinationResult>> destination,
                                                        Func<TSourceResult, TDestinationResult> map
                                                    )
            where TSourceResult : class
            where TDestinationResult : class, new();

        IMyMapperRules<TSource, TDestination> With<TSourceResult, TDestinationResult>(
                                                        Func<TSource, IEnumerable<TSourceResult>> source,
                                                        Action<TDestination, IEnumerable<TDestinationResult>> destination,
                                                        Func<TSourceResult, TDestinationResult> map
                                                    )
            where TSourceResult : class
            where TDestinationResult : class, new();

        IMyMapperRules<TSource, TDestination> With<TSourceResult, TDestinationResult>(
                                                        Func<TSource, IList<TSourceResult>> source,
                                                        Action<TDestination, IList<TDestinationResult>> destination,
                                                        Func<TSourceResult, TDestinationResult> map
                                                    )
            where TSourceResult : class
            where TDestinationResult : class, new();

        IMyMapperRules<TSource, TDestination> With<TDestinationResult>(
                                                        Func<TSource, DataTable> source,
                                                        Action<TDestination, IList<TDestinationResult>> destination,
                                                        Func<DataRow, TDestinationResult> map
                                                    )
            where TDestinationResult : class, new();

        IMyMapperRules<TSource, TDestination> ParallelWith<TSourceResult, TDestinationResult>(
                                                Func<TSource, ICollection<TSourceResult>> source,
                                                Action<TDestination, ICollection<TDestinationResult>> destination,
                                                Func<TSourceResult, TDestinationResult> map
                                            )
            where TSourceResult : class
            where TDestinationResult : class, new();

        IMyMapperRules<TSource, TDestination> ParallelWith<TSourceResult, TDestinationResult>(
                                                        Func<TSource, IEnumerable<TSourceResult>> source,
                                                        Action<TDestination, IEnumerable<TDestinationResult>> destination,
                                                        Func<TSourceResult, TDestinationResult> map
                                                    )
            where TSourceResult : class
            where TDestinationResult : class, new();

        IMyMapperRules<TSource, TDestination> ParallelWith<TSourceResult, TDestinationResult>(
                                                Func<TSource, IList<TSourceResult>> source,
                                                Action<TDestination, IList<TDestinationResult>> destination,
                                                Func<TSourceResult, TDestinationResult> map
                                            )
            where TSourceResult : class
            where TDestinationResult : class, new();

        IMyMapperRules<TSource, TDestination> WithWhen<TProperty>(
                                                                Func<TSource, bool> when,
                                                                Func<TSource, TProperty> source,
                                                                Action<TDestination, TProperty> destination
                                                            );

        IMyMapperRules<TSource, TDestination> When(
                                                    Func<TSource, bool> when,
                                                    Action<IMyMapperRules<TSource, TDestination>> then
                                             );

        IMyMapperSwitch<TSource, TDestination, TProperty> Switch<TProperty>(Func<TSource, TProperty> on);

        TDestination Exec();
    }
    /// <summary>
    /// IMyMapper - Generic interface
    /// </summary>
    /// <typeparam name="TSource">The source</typeparam>
    /// <typeparam name="TDestination">The destination</typeparam>
    public interface IMyMapper<TSource, TDestination> : IMyMapperRules<TSource, TDestination>
        where TSource : class
        where TDestination : class, new()
    {
        IMyMapperRules<TSource, TDestination> Map(TSource source, bool automap = true);

        Task<IMyMapperRules<TSource, TDestination>> MapAsync(TSource source, bool automap = true);

        [Obsolete("Exec is deprecated.", true)]
        TDestination Exec(TSource source, Func<TSource, IMyMapper<TSource, TDestination>, TDestination> map);

        TDestination Exec<TConverter>(TSource source)
            where TConverter : ITypeConverter<TSource, TDestination>, new();
    }
}
