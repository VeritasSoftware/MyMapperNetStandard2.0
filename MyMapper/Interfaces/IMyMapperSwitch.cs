using System;
using System.Linq.Expressions;

namespace MyMapper
{
    /// <summary>
    /// IMyMapperSwitch - Generic interface
    /// </summary>
    /// <typeparam name="TSource">The source</typeparam>
    /// <typeparam name="TDestination">The destination</typeparam>
    /// <typeparam name="TSourceProperty">The property</typeparam>
    public interface IMyMapperSwitch<TSource, TDestination, TSourceProperty>
        where TSource : class
        where TDestination : class, new()
    {
        IMyMapperSwitchElse<TSource, TDestination, TSourceProperty> CaseMap(Func<TSourceProperty, bool> when, Action<IMyMapperRules<TSource, TDestination>> then);

        IMyMapperSwitchElse<TSource, TDestination, TSourceProperty> Case(Func<TSourceProperty, bool> when, Action<TDestination, TSourceProperty> then);
    }

    /// <summary>
    /// IMyMapperSwitchElse - Generic interface
    /// </summary>
    /// <typeparam name="TSource">The source</typeparam>
    /// <typeparam name="TDestination">The destination</typeparam>
    /// <typeparam name="TSourceProperty">The property</typeparam>
    public interface IMyMapperSwitchElse<TSource, TDestination, TSourceProperty>
        where TSource : class
        where TDestination : class, new()
    {
        IMyMapperSwitchElse<TSource, TDestination, TSourceProperty> CaseMap(Func<TSourceProperty, bool> when, Action<IMyMapperRules<TSource, TDestination>> then);

        IMyMapperSwitchElse<TSource, TDestination, TSourceProperty> Case(Func<TSourceProperty, bool> when, Action<TDestination, TSourceProperty> then);

        IMyMapperSwitchEnd<TSource, TDestination> ElseMap(Action<IMyMapperRules<TSource, TDestination>> then);

        IMyMapperSwitchEnd<TSource, TDestination> Else(Action<TDestination, TSourceProperty> then);

        IMyMapperRules<TSource, TDestination> End();
    }

    /// <summary>
    /// IMyMapperSwitchEnd - Generic interface
    /// </summary>
    /// <typeparam name="TSource">The source</typeparam>
    /// <typeparam name="TDestination">The destination</typeparam>
    public interface IMyMapperSwitchEnd<TSource, TDestination>
        where TSource : class
        where TDestination : class, new()
    {
        IMyMapperRules<TSource, TDestination> End();
    }
}
