using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyMapper.UnitTests.Entities;
using MyMapper.UnitTests.Mappers;

namespace MyMapper.UnitTests
{
    [TestClass]
    public class MyMapperTests
    {        
        [TestMethod]
        public void MyMapper_Test()
        {
            var dob = DateTime.Now;

            IResponseMapper mapper = new ResponseMapper();

            Response1 response1 = new Response1
            {
                ConsumerID = 123,
                Name = "Shan",
                AvgNoOfPurchasesPerMonth = 10,
                PeriodInMonths = 12,
                Details = new Details1()
                {
                    DOB = dob,
                    IsDisabled = true
                },
                MutualFund = new Fund1
                {
                    BankIdNo = 10,
                    Name = "XYZ",
                    FundId = 1,
                    Address = new Address1
                    {
                        StreetNo = "123N",
                        State = new State1
                        {
                            Name = "Victoria",
                            Abbr = "VIC"
                        },
                        Country = new Country1()
                        {
                            Name = "Australia",
                            Abbr = "AU"
                        }
                    }
                },
                InsuranceMutualFund = new InsuranceMutualFund { MutualFundNumber = "123", TaxNo = "456" },
                InsuranceSuperannuation = new InsuranceSuperannuation { SuperannuationNumber = "789", TaxFileNumber = "456" },
                InsuranceEmployment = new InsuranceEmployment { EmploymentNumber = "678", TaxNumber = "456" },
                InsuranceType = Entities.InsuranceType.Employment,
                BankingInfos = new List<BankingInfo1>()
                {
                    new BankingInfo1
                    {
                        AccountName = "ABC",
                        AccountNo = "1"
                    },
                    new BankingInfo1
                    {
                        AccountName = "XYZ",
                        AccountNo = "2"
                    }
                }
            };

            //The mapping
            var response3 = mapper.Map(response1);

            Assert.IsTrue(response3.IDNumber == 123);
            Assert.IsTrue(response3.TotalPurchases == 120);
            Assert.IsTrue(response3.Details.DateOfBirth == dob);
            Assert.IsTrue(response3.Details.IsHandicapped);
            Assert.IsTrue(response3.InsuranceInfo.MembershipNo == "678");
            Assert.IsTrue(response3.InsuranceInfo.TaxNumber == "456");
            Assert.IsTrue(response3.Fund.BankIdNo == response1.MutualFund.BankIdNo);
            Assert.IsTrue(response3.Fund.FundId == response1.MutualFund.FundId);
            Assert.IsTrue(response3.Fund.Name == response1.MutualFund.Name);
            Assert.IsTrue(response3.Fund.Address.StreetNo == response1.MutualFund.Address.StreetNo);
            Assert.IsTrue(response3.Fund.Address.State.Name == response1.MutualFund.Address.State.Name);
            Assert.IsTrue(response3.Fund.Address.State.Abbr == response1.MutualFund.Address.State.Abbr);
            Assert.IsTrue(response3.Fund.Address.Country.Name == response1.MutualFund.Address.Country.Name);
            Assert.IsTrue(response3.Fund.Address.Country.Abbr == response1.MutualFund.Address.Country.Abbr);
        }

        [TestMethod]
        public async Task MyMapper_Async_Test()
        {
            var dob = DateTime.Now;

            IResponseMapper mapper = new ResponseMapper();

            Response1 response1 = new Response1
            {
                ConsumerID = 123,
                Name = "Shan",
                AvgNoOfPurchasesPerMonth = 10,
                PeriodInMonths = 12,
                Details = new Details1()
                {
                    DOB = dob,
                    IsDisabled = true
                },
                MutualFund = new Fund1
                {
                    BankIdNo = 10,
                    Name = "XYZ",
                    FundId = 1,
                    Address = new Address1
                    {
                        StreetNo = "123N",
                        State = new State1
                        {
                            Name = "Victoria",
                            Abbr = "VIC"
                        },
                        Country = new Country1()
                        {
                            Name = "Australia",
                            Abbr = "AU"
                        }
                    }
                },
                InsuranceMutualFund = new InsuranceMutualFund { MutualFundNumber = "123", TaxNo = "456" },
                InsuranceSuperannuation = new InsuranceSuperannuation { SuperannuationNumber = "789", TaxFileNumber = "456" },
                InsuranceEmployment = new InsuranceEmployment { EmploymentNumber = "678", TaxNumber = "456" },
                InsuranceType = Entities.InsuranceType.Employment,
                BankingInfos = new List<BankingInfo1>()
                {
                    new BankingInfo1
                    {
                        AccountName = "ABC",
                        AccountNo = "1"
                    },
                    new BankingInfo1
                    {
                        AccountName = "XYZ",
                        AccountNo = "2"
                    }
                }
            };

            //The mapping
            var response3 = await mapper.MapAsync(response1);

            Assert.IsTrue(response3.IDNumber == 123);
            Assert.IsTrue(response3.TotalPurchases == 120);
            Assert.IsTrue(response3.Details.DateOfBirth == dob);
            Assert.IsTrue(response3.Details.IsHandicapped);
            Assert.IsTrue(response3.InsuranceInfo.MembershipNo == "678");
            Assert.IsTrue(response3.InsuranceInfo.TaxNumber == "456");
            Assert.IsTrue(response3.Fund.BankIdNo == response1.MutualFund.BankIdNo);
            Assert.IsTrue(response3.Fund.FundId == response1.MutualFund.FundId);
            Assert.IsTrue(response3.Fund.Name == response1.MutualFund.Name);
            Assert.IsTrue(response3.Fund.Address.StreetNo == response1.MutualFund.Address.StreetNo);
            Assert.IsTrue(response3.Fund.Address.State.Name == response1.MutualFund.Address.State.Name);
            Assert.IsTrue(response3.Fund.Address.State.Abbr == response1.MutualFund.Address.State.Abbr);
            Assert.IsTrue(response3.Fund.Address.Country.Name == response1.MutualFund.Address.Country.Name);
            Assert.IsTrue(response3.Fund.Address.Country.Abbr == response1.MutualFund.Address.Country.Abbr);
        }
    }
}
