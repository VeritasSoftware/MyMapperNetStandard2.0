using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using NLog;

namespace Mapper.Performance.Workbench
{
    public class Workbench : IWorkbench
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public int NoOfIterations { get; set; }

        public void RunTests<TMapper>(Action<TMapper> map)
             where TMapper : IMapper, new()
        {
            IList<long> times = new List<long>();

            Stopwatch sw = new Stopwatch();
            sw.Start();
            TMapper mapper = new TMapper();
            sw.Stop();
            long initTime = sw.ElapsedMilliseconds;
            logger.Info(mapper.Name + " initialization elapsed time milliseconds: " + sw.ElapsedMilliseconds); //+ " nanoseconds: " + sw.ElapsedNanoseconds());
            
            for (int i = 0; i < this.NoOfIterations; i++)
            {
                sw.Restart();

                map(mapper);

                sw.Stop();
                logger.Info(mapper.Name + " mapping elapsed time milliseconds: " + sw.ElapsedMilliseconds); //+ " nanoseconds: " + sw.ElapsedNanoseconds());
                times.Add(sw.ElapsedMilliseconds);
            }

            logger.Info(mapper.Name + " average elapsed time milliseconds: " + times.Average());// + " nanoseconds: " + ((initTime * 1000000) + times.Average() * 1000000));
        }

        public void RunTests<TMapper>(TMapper mapper, Action<TMapper> map)
             where TMapper : IMapper, new()
        {
            IList<long> times = new List<long>();

            Stopwatch sw = new Stopwatch();            

            for (int i = 0; i < this.NoOfIterations; i++)
            {
                sw.Restart();

                map(mapper);

                sw.Stop();
                logger.Info(mapper.Name + " mapping elapsed time milliseconds: " + sw.ElapsedMilliseconds); // + " nanoseconds: " + sw.ElapsedNanoseconds());
                times.Add(sw.ElapsedMilliseconds);
            }

            logger.Info(mapper.Name + " average elapsed time milliseconds: " + times.Average()); // + " nanoseconds: " + (times.Average() * 1000000));
        }

        public async Task RunTestsAsync<TMapper>(TMapper mapper, Action<TMapper> map)
             where TMapper : IMapper, new()
        {
            IList<long> times = new List<long>();

            Stopwatch sw = new Stopwatch();            

            await Task.Run(() =>
            {
                for (int i = 0; i < this.NoOfIterations; i++)
                {
                    sw.Restart();

                    map(mapper);

                    sw.Stop();
                    logger.Info(mapper.Name + " mapping elapsed time milliseconds: " + sw.ElapsedMilliseconds); // + " nanoseconds: " + sw.ElapsedNanoseconds());
                    times.Add(sw.ElapsedMilliseconds);
                }

            logger.Info(mapper.Name + " average elapsed time milliseconds: " + times.Average()); // + " nanoseconds: " + (times.Average() * 1000000));
            });                       
        }
    }
}
