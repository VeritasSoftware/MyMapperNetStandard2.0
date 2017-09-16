using System;
using System.Collections.Generic;

namespace Mapper.Performance.Workbench.Entities
{
    public class Source : IMap, IMap<SourceProperty>
    {
        public int Int { get; set; }

        public int? IntNullable { get; set; }

        public long Long { get; set; }

        public DateTime Date { get; set; }

        public string String { get; set; }

        public IList<string> List { get; set; }

        public IDictionary<string, string> Dictionary { get; set; }

        public SourceProperty Property { get; set; }

        public IList<SourceProperty> ListProperty { get; set; }

        public IDictionary<int, SourceProperty> DictionaryProperty { get; set; }

        //public IList<SourceProperty> IMap<SourceProperty>.ListProperty
        //{
        //    get
        //    {
        //        throw new NotImplementedException();
        //    }

        //    set
        //    {
        //        throw new NotImplementedException();
        //    }
        //}

        //public IList<SourceProperty> IMap<SourceProperty>.ListProperty
        //{
        //    get
        //    {
        //        throw new NotImplementedException();
        //    }

        //    set
        //    {
        //        throw new NotImplementedException();
        //    }
        //}

        //public IList<SourceProperty> IMap<SourceProperty>.ListProperty
        //{
        //    get; set;
        //}
    }

    public class SourceProperty : IMapProperty
    {
        public int Int { get; set; }

        public long Long { get; set; }

        public DateTime Date { get; set; }

        public string String { get; set; }

        public IList<string> List { get; set; }

        public IDictionary<string, string> Dictionary { get; set; }
    }
}
