using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MyMapper
{
    internal class SwitchThen<TSource, TDestination, TSourceProperty>
    where TSource : class
    where TDestination : class, new()
    {
        public Action<TDestination, TSourceProperty> Case { get; set; }
        public Action<IMyMapperRules<TSource, TDestination>> CaseMap { get; set; }
    }

    /// <summary>
    /// MyMapperSwitch - Generic class
    /// </summary>
    /// <typeparam name="TSource">The source</typeparam>
    /// <typeparam name="TDestination">The destination</typeparam>
    /// <typeparam name="TSourceProperty">The property</typeparam>
    internal class MyMapperSwitch<TSource, TDestination, TSourceProperty> : IMyMapperSwitch<TSource, TDestination, TSourceProperty>,
                                                                            IMyMapperSwitchElse<TSource, TDestination, TSourceProperty>,
                                                                            IMyMapperSwitchEnd<TSource, TDestination>
        where TSource : class
        where TDestination : class, new()
    {
        TSourceProperty Property { get; set; }
        TSource Source { get; set; }
        IMyMapper<TSource, TDestination> Mapper { get; set; }
        Action<TDestination, TSourceProperty> ElseThen { get; set; }
        Action<IMyMapperRules<TSource, TDestination>> ElseThenMap { get; set; }
        Dictionary<Func<TSourceProperty, bool>, SwitchThen<TSource, TDestination, TSourceProperty>> cases = new Dictionary<Func<TSourceProperty, bool>, SwitchThen<TSource, TDestination, TSourceProperty>>();

        public MyMapperSwitch(TSource source, TSourceProperty property, IMyMapper<TSource, TDestination> mapper)
        {
            this.Source = source;
            this.Property = property;
            this.Mapper = mapper;
        }

        public IMyMapperSwitchElse<TSource, TDestination, TSourceProperty> CaseMap(
                                                                                        Expression<Func<TSourceProperty, bool>> when,
                                                                                        Action<IMyMapperRules<TSource, TDestination>> then
                                                                                    )
        {
            SwitchThen<TSource, TDestination, TSourceProperty> switchThen = new SwitchThen<TSource, TDestination, TSourceProperty>();
            switchThen.CaseMap = then;

            cases.Add(when.Compile(), switchThen);

            return this;
        }

        public IMyMapperSwitchElse<TSource, TDestination, TSourceProperty> Case(
                                                                                    Expression<Func<TSourceProperty, bool>> when,
                                                                                    Action<TDestination, TSourceProperty> then
                                                                                )
        {
            SwitchThen<TSource, TDestination, TSourceProperty> switchThen = new SwitchThen<TSource, TDestination, TSourceProperty>();
            switchThen.Case = then;

            cases.Add(when.Compile(), switchThen);

            return this;
        }

        public IMyMapperSwitchEnd<TSource, TDestination> ElseMap(Action<IMyMapperRules<TSource, TDestination>> then)
        {
            ElseThenMap = then;

            return this;
        }

        public IMyMapperSwitchEnd<TSource, TDestination> Else(Action<TDestination, TSourceProperty> then)
        {
            ElseThen = then;

            return this;
        }

        public IMyMapperRules<TSource, TDestination> End()
        {
            foreach (var when in cases.Keys)
            {
                if (when != null && when(this.Property))
                {
                    var theCase = cases[when];

                    if (theCase.Case != null)
                    {
                        theCase.Case(this.Mapper.Exec(), this.Property);
                    }
                    else if (theCase.CaseMap != null)
                    {
                        theCase.CaseMap(this.Mapper);
                    }

                    return this.Mapper;
                }
            }

            if (ElseThen != null)
            {
                ElseThen(this.Mapper.Exec(), this.Property);
            }
            else if (ElseThenMap != null)
            {
                ElseThenMap(this.Mapper);
            }

            return this.Mapper;
        }
    }
}
