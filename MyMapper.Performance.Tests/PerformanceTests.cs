using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapper.Performance.Workbench;
using Mapper.Performance.Workbench.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NLog;

namespace MyMapper.Performance.Tests
{
    [TestClass]
    public class PerformanceTests
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        static Test_MyMapper myMapper = new Test_MyMapper();
        static Test_Automapper autoMapper = new Test_Automapper();

        public class DerivedSource : Source
        {

        }

        public class DerivedSourceProperty : SourceProperty
        {

        }

        [TestMethod]
        public async Task Performance_Comparison_Async_Test()
        {            
            //Populate test data
            var derivedSourceProperty = new DerivedSourceProperty
            {
                Int = 1,
                Long = 2900000000000000,
                Date = DateTime.Now,
                List = new List<string> { "1", "2" },
                Dictionary = new Dictionary<string, string> { { "1", "1" }, { "2", "2" } },
                String = " Test string"
            };

            DerivedSource source = new DerivedSource
            {
                Int = 1,
                Long = 2900000000000000,
                Date = DateTime.Now,
                List = new List<string> { "1", "2" },
                Dictionary = new Dictionary<string, string> { { "1", "1" }, { "2", "2" } },
                String = " Test string",
                Property = derivedSourceProperty,
                IntNullable = 1
            };

            var list = new List<DerivedSourceProperty>();
            source.ListProperty = list.Cast<SourceProperty>().ToList();

            for (int i = 0; i < 100; i++)
            {
                derivedSourceProperty = new DerivedSourceProperty
                {
                    Int = i,
                    Long = 2900000000000000,
                    Date = DateTime.Now,
                    List = new List<string> { "1", "2" },
                    Dictionary = new Dictionary<string, string> { { "1", "1" }, { "2", "2" } },
                    String = " Test string"
                };

                source.ListProperty.Add(derivedSourceProperty);
            }

            var dictionary = new Dictionary<int, SourceProperty>();
            source.DictionaryProperty = dictionary;

            for (int i = 0; i < 100; i++)
            {
                var sourceProperty = new SourceProperty
                {
                    Int = i,
                    Long = 2900000000000000,
                    Date = DateTime.Now,
                    List = new List<string> { "1", "2" },
                    Dictionary = new Dictionary<string, string> { { "1", "1" }, { "2", "2" } },
                    String = " Test string"
                };

                source.DictionaryProperty.Add(i, sourceProperty);
            }

            logger.Info("*********Async Test Run start*********");

            IWorkbench workbench = new Workbench() { NoOfIterations = 10 };

            logger.Info("MyMapper...");

            Destination destination;
            DestinationDifferent destinationDifferent;            

            await workbench.RunTestsAsync<Test_MyMapper>(myMapper, async mapper => destination = await mapper.MapAsync(source));
           
            logger.Info("MyMapper Different...");

            await workbench.RunTestsAsync<Test_MyMapper>(myMapper, async mapper => destinationDifferent = await mapper.MapDifferentAsync(source));
            
            logger.Info("Automapper...");

            await workbench.RunTestsAsync<Test_Automapper>(autoMapper, mapper => destination = mapper.Map(source));            

            logger.Info("Automapper Different...");

            await workbench.RunTestsAsync<Test_Automapper>(autoMapper, mapper => destinationDifferent = mapper.MapDifferent(source));            

            logger.Info("*********Test Run end*********");
        }        
    }
}
