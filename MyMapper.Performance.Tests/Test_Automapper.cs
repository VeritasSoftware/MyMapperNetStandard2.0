using System;
using System.Threading.Tasks;
using Mapper.Performance.Workbench.Entities;

namespace MyMapper.Performance.Tests
{
    public class Test_Automapper : Mapper.Performance.Workbench.IMapper
    {
        public string Name { get; set;  }

        public Test_Automapper()
        {
            Name = "Automapper";

            AutoMapper.Mapper.Initialize(config =>
            {
                config.CreateMap<Source, Destination>();                        
                        
                config.CreateMap<SourceProperty, DestinationProperty>();                

                config.CreateMap<Source, DestinationDifferent>()
                        .ForMember(d => d.DestInt, opt => opt.MapFrom(s => s.Int))
                        .ForMember(d => d.DestLong, opt => opt.MapFrom(s => s.Long))
                        .ForMember(d => d.DestString, opt => opt.MapFrom(s => s.String))
                        .ForMember(d => d.DestDate, opt => opt.MapFrom(s => s.Date))
                        .ForMember(d => d.DestList, opt => opt.MapFrom(s => s.List))
                        .ForMember(d => d.DestDictionary, opt => opt.MapFrom(s => s.Dictionary))
                        .ForMember(d => d.DestProperty, opt => opt.MapFrom(s => s.Property))
                        .ForMember(d => d.DestListProperty, opt => opt.MapFrom(s => s.ListProperty));
            });
        }

        public Destination Map (Source source)
        {
            return AutoMapper.Mapper.Map<Source, Destination>(source);
        }

        public DestinationDifferent MapDifferent(Source source)
        {
            return AutoMapper.Mapper.Map<Source, DestinationDifferent>(source);
        }

        public Task<Destination> MapAsync(Source source)
        {
            throw new NotImplementedException();
        }
    }
}
