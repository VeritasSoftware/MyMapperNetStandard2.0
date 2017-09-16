using System;
using System.Collections.Generic;

namespace Mapper.Performance.Workbench.Entities
{
    public class Destination : IMap, IMap<DestinationProperty>
    {
        public int Int { get; set; }

        public int? IntNullable { get; set; }

        public long Long { get; set; }

        public DateTime Date { get; set; }

        public string String { get; set; }

        public IList<string> List { get; set; }

        public IDictionary<string, string> Dictionary { get; set; }

        public DestinationProperty Property { get; set; }

        public IList<DestinationProperty> ListProperty { get; set; }

        public IDictionary<int, DestinationProperty> DictionaryProperty { get; set; }
    }

    /// <summary>
    /// All properties of the class have different names from the source. 
    /// </summary>
    public class DestinationDifferent : IDestinationDifferent
    {
        public int DestInt { get; set; }

        public long DestLong { get; set; }

        public DateTime DestDate { get; set; }

        public string DestString { get; set; }

        public IList<string> DestList { get; set; }

        public IDictionary<string, string> DestDictionary { get; set; }

        public IMapProperty DestProperty { get; set; }

        public IList<DestinationProperty> DestListProperty { get; set; }

        //public IList<IMapProperty> IDestinationDifferent.DestListProperty
        //{
        //    get; set;
        //    //get
        //    //{
        //    //    throw new NotImplementedException();
        //    //}

        //    //set
        //    //{
        //    //    throw new NotImplementedException();
        //    //}
        //}
    }

    public class DestinationProperty : IMapProperty
    {
        public int Int { get; set; }

        public long Long { get; set; }

        public DateTime Date { get; set; }

        public string String { get; set; }

        public IList<string> List { get; set; }

        public IDictionary<string, string> Dictionary { get; set; }
    }
}
