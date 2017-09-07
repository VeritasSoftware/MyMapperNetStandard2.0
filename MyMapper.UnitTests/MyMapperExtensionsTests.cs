using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyMapper.UnitTests.Entities;
using MyMapper.UnitTests.Mappers;

namespace MyMapper.UnitTests
{
    [TestClass]
    public class MyMapperExtensionsTests
    {        
        [TestMethod]
        public void MyMapper_Object_Extension_Test()
        {
            var dateOfBirth = DateTime.Now;

            Details1 details1 = new Details1 { DOB = dateOfBirth, IsDisabled = false };

            //Specify MyMapper mapping rules - turn off automapping
            //Details1 and Details3 have different property names - specify mapping rules
            
            Details3 details3 = new Mapper().Map(details1);

            Assert.IsTrue(details3.DateOfBirth == dateOfBirth);
            Assert.IsTrue(details3.IsHandicapped == false);

            Fund1 fund1 = new Fund1
            {
                FundId = 1,
                Name = "Sun",
                Address = new Address1
                {
                    StreetNo = "10",
                    State = new State1 { Name = "New York", Abbr = "NY" },
                    Country = new Country1 { Name = "United States of America", Abbr = "USA" }
                }
                    ,
                BankIdNo = 1000
            };

            //Using automapping (for classes with the same property names).
            //Fund1 and Fund3 (and their contained classes) have the same property names

            Fund3 fund3 = new Mapper().Map(fund1);

            Assert.IsTrue(fund3.Name == "Sun");
        }

        [TestMethod]
        public async Task MyMapper_Object_AsyncExtension_Test()
        {
            var dateOfBirth = DateTime.Now;

            Details1 details1 = new Details1 { DOB = dateOfBirth, IsDisabled = false };

            //Specify MyMapper mapping rules - turn off automapping
            //Details1 and Details3 have different property names - specify mapping rules

            Details3 details3 = await new Mapper().MapAsync(details1);

            Assert.IsTrue(details3.DateOfBirth == dateOfBirth);
            Assert.IsTrue(details3.IsHandicapped == false);

            Fund1 fund1 = new Fund1
            {
                FundId = 1,
                Name = "Sun",
                Address = new Address1
                {
                    StreetNo = "10",
                    State = new State1 { Name = "New York", Abbr = "NY" },
                    Country = new Country1 { Name = "United States of America", Abbr = "USA" }
                }
                    ,
                BankIdNo = 1000
            };

            //Using automapping (for classes with the same property names).
            //Fund1 and Fund3 (and their contained classes) have the same property names

            Fund3 fund3 = await new Mapper().MapAsync(fund1);

            Assert.IsTrue(fund3.Name == "Sun");
        }

        [TestMethod]
        public void MyMapper_IEnumerable_Extension_Test()
        {
            List<Details1> detailsList = new List<Details1>()
                {
                    new Details1 { DOB = DateTime.Now, IsDisabled = false },
                    new Details1 { DOB = DateTime.Now.AddDays(-1), IsDisabled = true }
            };

            //Specify MyMapper mapping rules - turn off automapping
            //Details1 and Details3 have different property names - specify mapping rules
            
            var resultDetailsList = new Mapper().Map(detailsList);

            Assert.IsTrue(resultDetailsList.Count() == 2);

            List<Fund1> fundsList = new List<Fund1>
            {
                new Fund1 {
                    FundId = 1,
                    Name = "Sun",
                    Address = new Address1
                    {
                        StreetNo = "10",
                        State = new State1 { Name = "New York", Abbr = "NY" },
                        Country = new Country1 { Name = "United States of America", Abbr = "USA" }
                    }
                    , BankIdNo = 1000
                },
                new Fund1 {
                    FundId = 1,
                    Name = "WorkPro",
                    Address = new Address1
                    {
                        StreetNo = "10",
                        State = new State1 { Name = "California", Abbr = "CA" },
                        Country = new Country1 { Name = "United States of America", Abbr = "USA" }
                    }
                    , BankIdNo = 2000
                }
            };

            //Using automapping (for classes with the same property names).
            //Fund1 and Fund3 (and their contained classes) have the same property names

            var resultFundList = new Mapper().Map(fundsList);

            Assert.IsTrue(resultFundList.Count() == 2);
        }              

        [TestMethod]
        public async Task MyMapper_IEnumerable_AsyncExtension_Test()
        {                    
            List<Details1> detailsList = new List<Details1>()
            {
                new Details1 { DOB = DateTime.Now, IsDisabled = false },
                new Details1 { DOB = DateTime.Now.AddDays(-1), IsDisabled = true }
            };

            //Specify MyMapper mapping rules - turn off automapping
            //Details1 and Details3 have different property names - specify mapping rules

            var resultDetailsList = await new Mapper().MapAsync(detailsList);

            Assert.IsTrue(resultDetailsList.Count() == 2);

            List<Fund1> fundsList = new List<Fund1>
            {
                new Fund1 {
                    FundId = 1,
                    Name = "Sun",
                    Address = new Address1
                    {
                        StreetNo = "10",
                        State = new State1 { Name = "New York", Abbr = "NY" },
                        Country = new Country1 { Name = "United States of America", Abbr = "USA" }
                    }
                    , BankIdNo = 1000
                },
                new Fund1 {
                    FundId = 1,
                    Name = "WorkPro",
                    Address = new Address1
                    {
                        StreetNo = "10",
                        State = new State1 { Name = "California", Abbr = "CA" },
                        Country = new Country1 { Name = "United States of America", Abbr = "USA" }
                    }
                    , BankIdNo = 2000
                }
            };

            //Using automapping (for classes with the same property names).
            //Fund1 and Fund3 (and their contained classes) have the same property names

            var resultFundList = await new Mapper().MapAsync(fundsList);

            Assert.IsTrue(resultFundList.Count() == 2);
        }

        [TestMethod]
        public void MyMapper_IEnumerable_Extension_Null_Test()
        {
            List<Details1> detailsList = null;

            //Specify MyMapper mapping rules - turn off automapping
            //Details1 and Details3 have different property names - specify mapping rules            

            var resultDetailsList = new Mapper().Map(detailsList);

            Assert.IsTrue(resultDetailsList == null);

            List<Fund1> fundsList = null;

            //Using automapping (for classes with the same property names).
            //Fund1 and Fund3 (and their contained classes) have the same property names

            var resultFundList = new Mapper().Map(fundsList);

            Assert.IsTrue(resultFundList == null);
        }

    }
}
