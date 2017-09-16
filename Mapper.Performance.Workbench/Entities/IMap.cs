using System;
using System.Collections.Generic;

namespace Mapper.Performance.Workbench.Entities
{
    public interface IMap
    {
        int Int { get; set; }

        long Long { get; set; }

        DateTime Date { get; set; }

        string String { get; set; }

        IList<string> List { get; set; }

        IDictionary<string, string> Dictionary { get; set; }        
    }

    public interface IMap<IMapProperty>
    {
        IMapProperty Property { get; set; }

        IList<IMapProperty> ListProperty { get; set; }
    }

    public interface IMapProperty
    {
        int Int { get; set; }

        long Long { get; set; }

        DateTime Date { get; set; }

        string String { get; set; }

        IList<string> List { get; set; }

        IDictionary<string, string> Dictionary { get; set; }
    }
}
