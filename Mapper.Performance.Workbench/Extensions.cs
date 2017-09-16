using System.Diagnostics;

namespace Mapper.Performance.Workbench
{
    public static class Extensions
    {
        public static long ElapsedNanoseconds(this Stopwatch sw)
        {
            return sw != null ? sw.ElapsedMilliseconds * 1000000 : -1;            
        }
    }
}
