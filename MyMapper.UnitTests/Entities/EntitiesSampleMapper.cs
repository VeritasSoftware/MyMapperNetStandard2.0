﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyMapper.UnitTests.Entities
{
    #region Response1
    public class Details1
    {
        public DateTime DOB { get; set; }
        public bool IsDisabled { get; set; }
    }

    public class Address1
    {
        public string StreetNo { get; set; }

        public State1 State { get; set; }

        public Country1 Country { get; set; }
    }

    public class State1
    {
        public string Name { get; set; }
        public string Abbr { get; set; }
    }

    public class Country1
    {
        public string Name { get; set; }
        public string Abbr { get; set; }
    }

    public class Fund1
    {
        public int? BankIdNo { get; set; }
        public string Name { get; set; }
        public int FundId { get; set; }

        public Address1 Address { get; set; }

        public Dictionary<string, string> FundKeys { get; set; }
    }

    public class BankingInfo1
    {
        public string AccountNo { get; set; }
        //Automapped
        public string AccountName { get; set; }
    }

    public class InsuranceMutualFund
    {
        public string MutualFundNumber { get; set; }
        public string TaxNo { get; set; }
    }

    public class InsuranceSuperannuation
    {
        public string SuperannuationNumber { get; set; }
        public string TaxFileNumber { get; set; }
    }

    public class InsuranceEmployment
    {
        public string EmploymentNumber { get; set; }
        public string TaxNumber { get; set; }
    }

    public enum InsuranceType
    {
        MutualFund = 0,
        Superannuation = 1,
        Employment
    }

    public enum EmploymentCode
    {
        E = 0
    }   

    public class Response1
    {
        public int ConsumerID { get; set; }
        public string Name { get; set; }

        public int AvgNoOfPurchasesPerMonth { get; set; }
        public int PeriodInMonths { get; set; }

        public Details1 Details { get; set; }

        public Fund1 MutualFund { get; set; }

        //List
        public IList<BankingInfo1> BankingInfos { get; set; }

        public InsuranceMutualFund InsuranceMutualFund { get; set; }

        public InsuranceSuperannuation InsuranceSuperannuation { get; set; }

        public InsuranceEmployment InsuranceEmployment { get; set; }

        public InsuranceType InsuranceType { get; set; }

        public Dictionary<EmploymentCode, string> EmploymentCodes { get; set; }
    }
    #endregion

    #region Response3
    public class Details3
    {
        public DateTime DateOfBirth { get; set; }

        public bool IsHandicapped { get; set; }
    }

    public class Address3
    {
        public string StreetNo { get; set; }

        public State3 State { get; set; }

        public Country3 Country { get; set; }
    }

    public class State3
    {
        public string Name { get; set; }
        public string Abbr { get; set; }
    }

    public class Country3
    {
        public string Name { get; set; }
        public string Abbr { get; set; }
    }

    public class Fund3
    {
        public int? BankIdNo { get; set; }
        public string Name { get; set; }
        public int FundId { get; set; }

        public Address3 Address { get; set; }

        public Dictionary<string, string> FundKeys { get; set; }
    }

    public class BankingInfo3
    {
        public string AccountNumber { get; set; }
        //Automapped
        public string AccountName { get; set; }
    }

    public class InsuranceInfo
    {
        public string MembershipNo { get; set; }
        public string TaxNumber { get; set; }
    }

    public enum LabourCode
    {
        None = 0,
        ENG = 1       
    }

    public class Response3
    {
        public int IDNumber { get; set; }
        public string Name { get; set; }

        //Calculated Field
        public int TotalPurchases { get; set; }

        public Details3 Details { get; set; }

        //Automapped
        public Fund3 Fund { get; set; }

        //List
        public IList<BankingInfo3> BankingInformation { get; set; }

        public string Period { get; set; }

        public InsuranceInfo InsuranceInfo { get; set; }

        //Mapping to an existing destination object
        public string Existing { get; set; }

        public Dictionary<LabourCode, string> LabourCodes { get; set; }
    }   
    #endregion

    public class Response4
    {
        public string IDNumber { get; set; }

        public string AccountNumber { get; set; }
    }

    public class Response5
    {
        public string IDNumber { get; set; }   
        
        public string AccNo { get; set; }

        public void Map(Response4 response4)
        {
            Mapper<Response4, Response5>.Map(response4, this)
                                            .With(s => s.AccountNumber, (d, accountNo) => d.AccNo = accountNo)
                                        .Exec();
        }

        public async Task MapAsync(Response4 response4)
        {
            await Mapper<Response4, Response5>.MapAsync(response4, this)
                                                .With(s => s.AccountNumber, (d, accountNo) => d.AccNo = accountNo)
                                              .Exec();
        }
    }
}
