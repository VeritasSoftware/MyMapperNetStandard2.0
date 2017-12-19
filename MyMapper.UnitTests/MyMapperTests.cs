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
                Name = "XYZ",
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
                    },
                    FundKeys = new Dictionary<string, string>()
                    {
                        { "ABN", "123456" },
                        { "TFN", "9876543" }
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

            //Mapping source to new destination
            var response3 = mapper.Map(response1);

            Assert.IsTrue(response3.IDNumber == response1.ConsumerID);
            Assert.IsTrue(response3.Name == response1.Name);
            Assert.IsTrue(response3.TotalPurchases == response1.AvgNoOfPurchasesPerMonth * response1.PeriodInMonths);
            Assert.IsTrue(response3.Details.DateOfBirth == response1.Details.DOB);
            Assert.IsTrue(response3.Details.IsHandicapped == response1.Details.IsDisabled);
            Assert.IsTrue(response3.InsuranceInfo.MembershipNo == response1.InsuranceEmployment.EmploymentNumber);
            Assert.IsTrue(response3.InsuranceInfo.TaxNumber == response1.InsuranceEmployment.TaxNumber);
            Assert.IsTrue(response3.Fund.BankIdNo == response1.MutualFund.BankIdNo);
            Assert.IsTrue(response3.Fund.FundId == response1.MutualFund.FundId);
            Assert.IsTrue(response3.Fund.Name == response1.MutualFund.Name);
            Assert.IsTrue(response3.Fund.Address.StreetNo == response1.MutualFund.Address.StreetNo);
            Assert.IsTrue(response3.Fund.Address.State.Name == response1.MutualFund.Address.State.Name);
            Assert.IsTrue(response3.Fund.Address.State.Abbr == response1.MutualFund.Address.State.Abbr);
            Assert.IsTrue(response3.Fund.Address.Country.Name == response1.MutualFund.Address.Country.Name);
            Assert.IsTrue(response3.Fund.Address.Country.Abbr == response1.MutualFund.Address.Country.Abbr);
            Assert.IsTrue(response3.Fund.FundKeys["ABN"] == response1.MutualFund.FundKeys["ABN"]);
            Assert.IsTrue(response3.Fund.FundKeys["TFN"] == response1.MutualFund.FundKeys["TFN"]);
            Assert.IsTrue(response3.BankingInformation.Count == response1.BankingInfos.Count);
            Assert.IsTrue(response3.BankingInformation[0].AccountName == response1.BankingInfos[0].AccountName);
            Assert.IsTrue(response3.BankingInformation[0].AccountNumber == response1.BankingInfos[0].AccountNo);
            Assert.IsTrue(response3.BankingInformation[1].AccountName == response1.BankingInfos[1].AccountName);
            Assert.IsTrue(response3.BankingInformation[1].AccountNumber == response1.BankingInfos[1].AccountNo);

            //Mapping source to existing destination
            Response3 response3_1 = new Response3() { Existing = "Mapping to existing destination object" };

            mapper.Map(response1, response3_1);

            Assert.IsTrue(response3_1.IDNumber == response1.ConsumerID);
            Assert.IsTrue(response3_1.Name == response1.Name);
            Assert.IsTrue(response3_1.Existing == "Mapping to existing destination object");
            Assert.IsTrue(response3_1.TotalPurchases == response1.AvgNoOfPurchasesPerMonth * response1.PeriodInMonths);
            Assert.IsTrue(response3_1.Details.DateOfBirth == response1.Details.DOB);
            Assert.IsTrue(response3_1.Details.IsHandicapped == response1.Details.IsDisabled);
            Assert.IsTrue(response3_1.InsuranceInfo.MembershipNo == response1.InsuranceEmployment.EmploymentNumber);
            Assert.IsTrue(response3_1.InsuranceInfo.TaxNumber == response1.InsuranceEmployment.TaxNumber);
            Assert.IsTrue(response3_1.Fund.BankIdNo == response1.MutualFund.BankIdNo);
            Assert.IsTrue(response3_1.Fund.FundId == response1.MutualFund.FundId);
            Assert.IsTrue(response3_1.Fund.Name == response1.MutualFund.Name);
            Assert.IsTrue(response3_1.Fund.Address.StreetNo == response1.MutualFund.Address.StreetNo);
            Assert.IsTrue(response3_1.Fund.Address.State.Name == response1.MutualFund.Address.State.Name);
            Assert.IsTrue(response3_1.Fund.Address.State.Abbr == response1.MutualFund.Address.State.Abbr);
            Assert.IsTrue(response3_1.Fund.Address.Country.Name == response1.MutualFund.Address.Country.Name);
            Assert.IsTrue(response3_1.Fund.Address.Country.Abbr == response1.MutualFund.Address.Country.Abbr);
            Assert.IsTrue(response3_1.Fund.FundKeys["ABN"] == response1.MutualFund.FundKeys["ABN"]);
            Assert.IsTrue(response3_1.Fund.FundKeys["TFN"] == response1.MutualFund.FundKeys["TFN"]);
            Assert.IsTrue(response3_1.BankingInformation.Count == response1.BankingInfos.Count);
            Assert.IsTrue(response3_1.BankingInformation[0].AccountName == response1.BankingInfos[0].AccountName);
            Assert.IsTrue(response3_1.BankingInformation[0].AccountNumber == response1.BankingInfos[0].AccountNo);
            Assert.IsTrue(response3_1.BankingInformation[1].AccountName == response1.BankingInfos[1].AccountName);
            Assert.IsTrue(response3_1.BankingInformation[1].AccountNumber == response1.BankingInfos[1].AccountNo);

            //Mapping source to existing destination (this)
            Response4 response4 = new Response4() { IDNumber = "XYZ", AccountNumber = "123" };

            Response5 response5 = new Response5();

            response5.Map(response4);

            Assert.IsTrue(response5.IDNumber == response4.IDNumber);
            Assert.IsTrue(response5.AccNo == response4.AccountNumber);
        }

        [TestMethod]
        public async Task MyMapper_Async_Test()
        {
            var dob = DateTime.Now;

            IResponseMapper mapper = new ResponseMapper();

            Response1 response1 = new Response1
            {
                ConsumerID = 123,
                Name = "XYZ",
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
                    },
                    FundKeys = new Dictionary<string, string>()
                    {
                        { "ABN", "123456" },
                        { "TFN", "9876543" }
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

            //Mapping source to new destination
            var response3 = await mapper.MapAsync(response1);

            Assert.IsTrue(response3.IDNumber == response1.ConsumerID);
            Assert.IsTrue(response3.Name == response1.Name);
            Assert.IsTrue(response3.TotalPurchases == response1.AvgNoOfPurchasesPerMonth * response1.PeriodInMonths);
            Assert.IsTrue(response3.Details.DateOfBirth == response1.Details.DOB);
            Assert.IsTrue(response3.Details.IsHandicapped == response1.Details.IsDisabled);
            Assert.IsTrue(response3.InsuranceInfo.MembershipNo == response1.InsuranceEmployment.EmploymentNumber);
            Assert.IsTrue(response3.InsuranceInfo.TaxNumber == response1.InsuranceEmployment.TaxNumber);
            Assert.IsTrue(response3.Fund.BankIdNo == response1.MutualFund.BankIdNo);
            Assert.IsTrue(response3.Fund.FundId == response1.MutualFund.FundId);
            Assert.IsTrue(response3.Fund.Name == response1.MutualFund.Name);
            Assert.IsTrue(response3.Fund.Address.StreetNo == response1.MutualFund.Address.StreetNo);
            Assert.IsTrue(response3.Fund.Address.State.Name == response1.MutualFund.Address.State.Name);
            Assert.IsTrue(response3.Fund.Address.State.Abbr == response1.MutualFund.Address.State.Abbr);
            Assert.IsTrue(response3.Fund.Address.Country.Name == response1.MutualFund.Address.Country.Name);
            Assert.IsTrue(response3.Fund.Address.Country.Abbr == response1.MutualFund.Address.Country.Abbr);
            Assert.IsTrue(response3.Fund.FundKeys["ABN"] == response1.MutualFund.FundKeys["ABN"]);
            Assert.IsTrue(response3.Fund.FundKeys["TFN"] == response1.MutualFund.FundKeys["TFN"]);
            Assert.IsTrue(response3.BankingInformation.Count == response1.BankingInfos.Count);
            Assert.IsTrue(response3.BankingInformation[0].AccountName == response1.BankingInfos[0].AccountName);
            Assert.IsTrue(response3.BankingInformation[0].AccountNumber == response1.BankingInfos[0].AccountNo);
            Assert.IsTrue(response3.BankingInformation[1].AccountName == response1.BankingInfos[1].AccountName);
            Assert.IsTrue(response3.BankingInformation[1].AccountNumber == response1.BankingInfos[1].AccountNo);

            //Mapping source to existing destination
            Response3 response3_1 = new Response3() { Existing = "Mapping to existing destination object" };

            await mapper.MapAsync(response1, response3_1);

            Assert.IsTrue(response3_1.IDNumber == response1.ConsumerID);
            Assert.IsTrue(response3_1.Name == response1.Name);
            Assert.IsTrue(response3_1.Existing == "Mapping to existing destination object");
            Assert.IsTrue(response3_1.TotalPurchases == response1.AvgNoOfPurchasesPerMonth * response1.PeriodInMonths);
            Assert.IsTrue(response3_1.Details.DateOfBirth == response1.Details.DOB);
            Assert.IsTrue(response3_1.Details.IsHandicapped == response1.Details.IsDisabled);
            Assert.IsTrue(response3_1.InsuranceInfo.MembershipNo == response1.InsuranceEmployment.EmploymentNumber);
            Assert.IsTrue(response3_1.InsuranceInfo.TaxNumber == response1.InsuranceEmployment.TaxNumber);
            Assert.IsTrue(response3_1.Fund.BankIdNo == response1.MutualFund.BankIdNo);
            Assert.IsTrue(response3_1.Fund.FundId == response1.MutualFund.FundId);
            Assert.IsTrue(response3_1.Fund.Name == response1.MutualFund.Name);
            Assert.IsTrue(response3_1.Fund.Address.StreetNo == response1.MutualFund.Address.StreetNo);
            Assert.IsTrue(response3_1.Fund.Address.State.Name == response1.MutualFund.Address.State.Name);
            Assert.IsTrue(response3_1.Fund.Address.State.Abbr == response1.MutualFund.Address.State.Abbr);
            Assert.IsTrue(response3_1.Fund.Address.Country.Name == response1.MutualFund.Address.Country.Name);
            Assert.IsTrue(response3_1.Fund.Address.Country.Abbr == response1.MutualFund.Address.Country.Abbr);
            Assert.IsTrue(response3_1.Fund.FundKeys["ABN"] == response1.MutualFund.FundKeys["ABN"]);
            Assert.IsTrue(response3_1.Fund.FundKeys["TFN"] == response1.MutualFund.FundKeys["TFN"]);
            Assert.IsTrue(response3_1.BankingInformation.Count == response1.BankingInfos.Count);
            Assert.IsTrue(response3_1.BankingInformation[0].AccountName == response1.BankingInfos[0].AccountName);
            Assert.IsTrue(response3_1.BankingInformation[0].AccountNumber == response1.BankingInfos[0].AccountNo);
            Assert.IsTrue(response3_1.BankingInformation[1].AccountName == response1.BankingInfos[1].AccountName);
            Assert.IsTrue(response3_1.BankingInformation[1].AccountNumber == response1.BankingInfos[1].AccountNo);

            //Mapping source to existing destination (this)
            Response4 response4 = new Response4() { IDNumber = "XYZ", AccountNumber = "123" };

            Response5 response5 = new Response5();

            await response5.MapAsync(response4);

            Assert.IsTrue(response5.IDNumber == response4.IDNumber);
            Assert.IsTrue(response5.AccNo == response4.AccountNumber);
        }
    }
}
