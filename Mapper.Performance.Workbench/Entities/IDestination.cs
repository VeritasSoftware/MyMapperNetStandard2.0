using System;
using System.Collections.Generic;

namespace Mapper.Performance.Workbench.Entities
{    
    public interface IDestinationDifferent
    {
        int DestInt { get; set; }

        long DestLong { get; set; }

        DateTime DestDate { get; set; }

        string DestString { get; set; }

        IList<string> DestList { get; set; }

        IDictionary<string, string> DestDictionary { get; set; }

        IMapProperty DestProperty { get; set; }

        IList<DestinationProperty> DestListProperty { get; set; }
    }    
}
