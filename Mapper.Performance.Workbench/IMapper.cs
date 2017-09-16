using System.Threading.Tasks;
using Mapper.Performance.Workbench.Entities;

namespace Mapper.Performance.Workbench
{
    public interface IMapper
    {
        string Name { get; set; }

        Destination Map(Source source);

        Task<Destination> MapAsync(Source source);

        DestinationDifferent MapDifferent(Source source);        
    }
}
