using System;
using System.Threading.Tasks;

namespace Mapper.Performance.Workbench
{
    public interface IWorkbench
    {
        void RunTests<TMapper>(Action<TMapper> map)
             where TMapper : IMapper, new();        

        void RunTests<TMapper>(TMapper mapper, Action<TMapper> map)
            where TMapper : IMapper, new();

        Task RunTestsAsync<TMapper>(TMapper mapper, Action<TMapper> map)
            where TMapper : IMapper, new();
    }
}
