using System;
using System.Threading.Tasks;
using Mapper.Performance.Workbench;
using Mapper.Performance.Workbench.Entities;

namespace MyMapper.Performance.Tests
{
    public class Test_MyMapper : IMapper
    {
        public string Name { get; set; }

        public Test_MyMapper()
        {
            Name = "MyMapper";
        }

        public Destination Map (Source source)
        {
            return Mapper<Source, Destination>.Map(source).Exec();

            //Using extensions
            //return source.Map<Source, Destination>();
        }

        public async Task<Destination> MapAsync(Source source)
        {
            return await Mapper<Source, Destination>.MapAsync(source).Exec();

            //Using extensions
            //return await source.MapAsync<Source, Destination>();
        }

        public DestinationDifferent MapDifferent(Source source)
        {
            return Mapper<Source, DestinationDifferent>.Map(source, false)
                                                            .With(s => s.Int, (d, p) => d.DestInt = p)
                                                            .With(s => s.Long, (d, p) => d.DestLong = p)
                                                            .With(s => s.String, (d, p) => d.DestString = p)
                                                            .With(s => s.Date, (d, p) => d.DestDate = p)
                                                            .With(s => s.List, (d, p) => d.DestList = p)
                                                            .With(s => s.Dictionary, (d, p) => d.DestDictionary = p)
                                                            .With(s => s.Property, (d, p) => d.DestProperty = p, MapDifferentProperty)
                                                            .With(s => s.ListProperty, (d, p) => d.DestListProperty = p, MapDifferentProperty)
                                                       .Exec();
        }

        public async Task<DestinationDifferent> MapDifferentAsync(Source source)
        {
            return await Mapper<Source, DestinationDifferent>.MapAsync(source, false)
                                                            .With(s => s.Int, (d, p) => d.DestInt = p)
                                                            .With(s => s.Long, (d, p) => d.DestLong = p)
                                                            .With(s => s.String, (d, p) => d.DestString = p)
                                                            .With(s => s.Date, (d, p) => d.DestDate = p)
                                                            .With(s => s.List, (d, p) => d.DestList = p)
                                                            .With(s => s.Dictionary, (d, p) => d.DestDictionary = p)
                                                            .With(s => s.Property, (d, p) => d.DestProperty = p, MapDifferentProperty)
                                                            .With(s => s.ListProperty, (d, p) => d.DestListProperty = p, MapDifferentProperty)
                                                       .Exec();
        }

        private DestinationProperty MapDifferentProperty(SourceProperty sourceProperty)
        {
            return Mapper<SourceProperty, DestinationProperty>.Map(sourceProperty, false)
                                                                    .With(s => s.Int, (d, p) => d.Int = p)
                                                                    .With(s => s.Long, (d, p) => d.Long = p)
                                                                    .With(s => s.String, (d, p) => d.String = p)
                                                                    .With(s => s.Date, (d, p) => d.Date = p)
                                                                    .With(s => s.List, (d, p) => d.List = p)
                                                                    .With(s => s.Dictionary, (d, p) => d.Dictionary = p)
                                                              .Exec();
        }
    }
}
