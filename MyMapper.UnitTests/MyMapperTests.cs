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

            Response1 source = new Response1
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
                InsuranceType = InsuranceType.Employment,
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
                },
                EmploymentCodes = new Dictionary<EmploymentCode, string>() { { EmploymentCode.E, "Engineer" } }
            };

            //Mapping source to new destination
            var destination = mapper.Map(source);

            Assert.IsTrue(destination.IDNumber == source.ConsumerID);
            Assert.IsTrue(destination.Name == source.Name);
            Assert.IsTrue(destination.TotalPurchases == source.AvgNoOfPurchasesPerMonth * source.PeriodInMonths);
            Assert.IsTrue(destination.Details.DateOfBirth == source.Details.DOB);
            Assert.IsTrue(destination.Details.IsHandicapped == source.Details.IsDisabled);
            Assert.IsTrue(source.PeriodInMonths == 12);
            Assert.IsTrue(destination.Period == "Year");
            Assert.IsTrue(source.InsuranceType == InsuranceType.Employment);
            Assert.IsTrue(destination.InsuranceInfo.MembershipNo == source.InsuranceEmployment.EmploymentNumber);
            Assert.IsTrue(destination.InsuranceInfo.TaxNumber == source.InsuranceEmployment.TaxNumber);
            Assert.IsTrue(destination.Fund.BankIdNo == source.MutualFund.BankIdNo);
            Assert.IsTrue(destination.Fund.FundId == source.MutualFund.FundId);
            Assert.IsTrue(destination.Fund.Name == source.MutualFund.Name);
            Assert.IsTrue(destination.Fund.Address.StreetNo == source.MutualFund.Address.StreetNo);
            Assert.IsTrue(destination.Fund.Address.State.Name == source.MutualFund.Address.State.Name);
            Assert.IsTrue(destination.Fund.Address.State.Abbr == source.MutualFund.Address.State.Abbr);
            Assert.IsTrue(destination.Fund.Address.Country.Name == source.MutualFund.Address.Country.Name);
            Assert.IsTrue(destination.Fund.Address.Country.Abbr == source.MutualFund.Address.Country.Abbr);
            Assert.IsTrue(destination.Fund.FundKeys["ABN"] == source.MutualFund.FundKeys["ABN"]);
            Assert.IsTrue(destination.Fund.FundKeys["TFN"] == source.MutualFund.FundKeys["TFN"]);
            Assert.IsTrue(destination.BankingInformation.Count == source.BankingInfos.Count);
            Assert.IsTrue(destination.BankingInformation[0].AccountName == source.BankingInfos[0].AccountName);
            Assert.IsTrue(destination.BankingInformation[0].AccountNumber == source.BankingInfos[0].AccountNo);
            Assert.IsTrue(destination.BankingInformation[1].AccountName == source.BankingInfos[1].AccountName);
            Assert.IsTrue(destination.BankingInformation[1].AccountNumber == source.BankingInfos[1].AccountNo);
            Assert.IsTrue(destination.LabourCodes.Count == source.EmploymentCodes.Count);
            Assert.IsTrue(destination.LabourCodes[LabourCode.ENG] == source.EmploymentCodes[EmploymentCode.E]);

            //Mapping source to existing destination
            Response3 destination_1 = new Response3() { Existing = "Mapping to existing destination object" };

            mapper.Map(source, destination_1);

            Assert.IsTrue(destination.IDNumber == source.ConsumerID);
            Assert.IsTrue(destination.Name == source.Name);
            Assert.IsTrue(destination_1.Existing == "Mapping to existing destination object");
            Assert.IsTrue(destination.TotalPurchases == source.AvgNoOfPurchasesPerMonth * source.PeriodInMonths);
            Assert.IsTrue(destination.Details.DateOfBirth == source.Details.DOB);
            Assert.IsTrue(destination.Details.IsHandicapped == source.Details.IsDisabled);
            Assert.IsTrue(source.PeriodInMonths == 12);
            Assert.IsTrue(destination.Period == "Year");
            Assert.IsTrue(source.InsuranceType == InsuranceType.Employment);
            Assert.IsTrue(destination.InsuranceInfo.MembershipNo == source.InsuranceEmployment.EmploymentNumber);
            Assert.IsTrue(destination.InsuranceInfo.TaxNumber == source.InsuranceEmployment.TaxNumber);
            Assert.IsTrue(destination.Fund.BankIdNo == source.MutualFund.BankIdNo);
            Assert.IsTrue(destination.Fund.FundId == source.MutualFund.FundId);
            Assert.IsTrue(destination.Fund.Name == source.MutualFund.Name);
            Assert.IsTrue(destination.Fund.Address.StreetNo == source.MutualFund.Address.StreetNo);
            Assert.IsTrue(destination.Fund.Address.State.Name == source.MutualFund.Address.State.Name);
            Assert.IsTrue(destination.Fund.Address.State.Abbr == source.MutualFund.Address.State.Abbr);
            Assert.IsTrue(destination.Fund.Address.Country.Name == source.MutualFund.Address.Country.Name);
            Assert.IsTrue(destination.Fund.Address.Country.Abbr == source.MutualFund.Address.Country.Abbr);
            Assert.IsTrue(destination.Fund.FundKeys["ABN"] == source.MutualFund.FundKeys["ABN"]);
            Assert.IsTrue(destination.Fund.FundKeys["TFN"] == source.MutualFund.FundKeys["TFN"]);
            Assert.IsTrue(destination.BankingInformation.Count == source.BankingInfos.Count);
            Assert.IsTrue(destination.BankingInformation[0].AccountName == source.BankingInfos[0].AccountName);
            Assert.IsTrue(destination.BankingInformation[0].AccountNumber == source.BankingInfos[0].AccountNo);
            Assert.IsTrue(destination.BankingInformation[1].AccountName == source.BankingInfos[1].AccountName);
            Assert.IsTrue(destination.BankingInformation[1].AccountNumber == source.BankingInfos[1].AccountNo);
            Assert.IsTrue(destination.LabourCodes.Count == source.EmploymentCodes.Count);
            Assert.IsTrue(destination.LabourCodes[LabourCode.ENG] == source.EmploymentCodes[EmploymentCode.E]);

            //Mapping source to existing destination (this)
            Response4 source_2 = new Response4() { IDNumber = "XYZ", AccountNumber = "123" };

            Response5 destination_2 = new Response5();

            destination_2.Map(source_2);

            Assert.IsTrue(destination_2.IDNumber == source_2.IDNumber);
            Assert.IsTrue(destination_2.AccNo == source_2.AccountNumber);
        }

        [TestMethod]
        public async Task MyMapper_Async_Test()
        {
            var dob = DateTime.Now;

            IResponseMapper mapper = new ResponseMapper();

            Response1 source = new Response1
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
                },
                EmploymentCodes = new Dictionary<EmploymentCode, string>() { { EmploymentCode.E, "Engineer" } }
            };

            //Mapping source to new destination
            var destination = await mapper.MapAsync(source);

            Assert.IsTrue(destination.IDNumber == source.ConsumerID);
            Assert.IsTrue(destination.Name == source.Name);
            Assert.IsTrue(destination.TotalPurchases == source.AvgNoOfPurchasesPerMonth * source.PeriodInMonths);
            Assert.IsTrue(destination.Details.DateOfBirth == source.Details.DOB);
            Assert.IsTrue(destination.Details.IsHandicapped == source.Details.IsDisabled);
            Assert.IsTrue(source.PeriodInMonths == 12);
            Assert.IsTrue(destination.Period == "Year");
            Assert.IsTrue(source.InsuranceType == InsuranceType.Employment);
            Assert.IsTrue(destination.InsuranceInfo.MembershipNo == source.InsuranceEmployment.EmploymentNumber);
            Assert.IsTrue(destination.InsuranceInfo.TaxNumber == source.InsuranceEmployment.TaxNumber);
            Assert.IsTrue(destination.Fund.BankIdNo == source.MutualFund.BankIdNo);
            Assert.IsTrue(destination.Fund.FundId == source.MutualFund.FundId);
            Assert.IsTrue(destination.Fund.Name == source.MutualFund.Name);
            Assert.IsTrue(destination.Fund.Address.StreetNo == source.MutualFund.Address.StreetNo);
            Assert.IsTrue(destination.Fund.Address.State.Name == source.MutualFund.Address.State.Name);
            Assert.IsTrue(destination.Fund.Address.State.Abbr == source.MutualFund.Address.State.Abbr);
            Assert.IsTrue(destination.Fund.Address.Country.Name == source.MutualFund.Address.Country.Name);
            Assert.IsTrue(destination.Fund.Address.Country.Abbr == source.MutualFund.Address.Country.Abbr);
            Assert.IsTrue(destination.Fund.FundKeys["ABN"] == source.MutualFund.FundKeys["ABN"]);
            Assert.IsTrue(destination.Fund.FundKeys["TFN"] == source.MutualFund.FundKeys["TFN"]);
            Assert.IsTrue(destination.BankingInformation.Count == source.BankingInfos.Count);
            Assert.IsTrue(destination.BankingInformation[0].AccountName == source.BankingInfos[0].AccountName);
            Assert.IsTrue(destination.BankingInformation[0].AccountNumber == source.BankingInfos[0].AccountNo);
            Assert.IsTrue(destination.BankingInformation[1].AccountName == source.BankingInfos[1].AccountName);
            Assert.IsTrue(destination.BankingInformation[1].AccountNumber == source.BankingInfos[1].AccountNo);
            Assert.IsTrue(destination.LabourCodes.Count == source.EmploymentCodes.Count);
            Assert.IsTrue(destination.LabourCodes[LabourCode.ENG] == source.EmploymentCodes[EmploymentCode.E]);

            //Mapping source to existing destination
            Response3 destination_1 = new Response3() { Existing = "Mapping to existing destination object" };

            await mapper.MapAsync(source, destination_1);

            Assert.IsTrue(destination.IDNumber == source.ConsumerID);
            Assert.IsTrue(destination.Name == source.Name);
            Assert.IsTrue(destination_1.Existing == "Mapping to existing destination object");
            Assert.IsTrue(destination.TotalPurchases == source.AvgNoOfPurchasesPerMonth * source.PeriodInMonths);
            Assert.IsTrue(destination.Details.DateOfBirth == source.Details.DOB);
            Assert.IsTrue(destination.Details.IsHandicapped == source.Details.IsDisabled);
            Assert.IsTrue(source.PeriodInMonths == 12);
            Assert.IsTrue(destination.Period == "Year");
            Assert.IsTrue(source.InsuranceType == InsuranceType.Employment);
            Assert.IsTrue(destination.InsuranceInfo.MembershipNo == source.InsuranceEmployment.EmploymentNumber);
            Assert.IsTrue(destination.InsuranceInfo.TaxNumber == source.InsuranceEmployment.TaxNumber);
            Assert.IsTrue(destination.Fund.BankIdNo == source.MutualFund.BankIdNo);
            Assert.IsTrue(destination.Fund.FundId == source.MutualFund.FundId);
            Assert.IsTrue(destination.Fund.Name == source.MutualFund.Name);
            Assert.IsTrue(destination.Fund.Address.StreetNo == source.MutualFund.Address.StreetNo);
            Assert.IsTrue(destination.Fund.Address.State.Name == source.MutualFund.Address.State.Name);
            Assert.IsTrue(destination.Fund.Address.State.Abbr == source.MutualFund.Address.State.Abbr);
            Assert.IsTrue(destination.Fund.Address.Country.Name == source.MutualFund.Address.Country.Name);
            Assert.IsTrue(destination.Fund.Address.Country.Abbr == source.MutualFund.Address.Country.Abbr);
            Assert.IsTrue(destination.Fund.FundKeys["ABN"] == source.MutualFund.FundKeys["ABN"]);
            Assert.IsTrue(destination.Fund.FundKeys["TFN"] == source.MutualFund.FundKeys["TFN"]);
            Assert.IsTrue(destination.BankingInformation.Count == source.BankingInfos.Count);
            Assert.IsTrue(destination.BankingInformation[0].AccountName == source.BankingInfos[0].AccountName);
            Assert.IsTrue(destination.BankingInformation[0].AccountNumber == source.BankingInfos[0].AccountNo);
            Assert.IsTrue(destination.BankingInformation[1].AccountName == source.BankingInfos[1].AccountName);
            Assert.IsTrue(destination.BankingInformation[1].AccountNumber == source.BankingInfos[1].AccountNo);
            Assert.IsTrue(destination.LabourCodes.Count == source.EmploymentCodes.Count);
            Assert.IsTrue(destination.LabourCodes[LabourCode.ENG] == source.EmploymentCodes[EmploymentCode.E]);

            //Mapping source to existing destination (this)
            Response4 source_2 = new Response4() { IDNumber = "XYZ", AccountNumber = "123" };

            Response5 destination_2 = new Response5();

            await destination_2.MapAsync(source_2);

            Assert.IsTrue(destination_2.IDNumber == source_2.IDNumber);
            Assert.IsTrue(destination_2.AccNo == source_2.AccountNumber);
        }
    }
}
