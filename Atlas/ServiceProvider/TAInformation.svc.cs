using Atlas.ServiceProvider.Proxy;
using GTS.Clock.Business;
using GTS.Clock.Business.WorkedTime;
using GTS.Clock.Model.MonthlyReport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;


// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "TAInformation" in code, svc and config file together.
// NOTE: In order to launch WCF Test Client for testing this service, please select TAInformation.svc or TAInformation.svc.cs at the Solution Explorer and start debugging.
public class TAInformation : ITAInformation
{

    /// <summary>
    /// اطلاعات حضور غیاب شخص در بازه زمانی را برمیگرداند
    /// </summary>
    /// <param name="personCode">شماره پرسنلی</param>
    /// <param name="fromDate">تاریخ میلادی شروع</param>
    /// <param name="toDate">تاریخ میلادی پایان</param>
    public TAProxy GetTAInfo(string personCode, DateTime fromDate, DateTime toDate)
    {
        return this.Fill(personCode, fromDate, toDate);
    }

    /// <summary>
    /// بادریافت شماره پرسنلی ، کارکرد را برمیگرداند
    /// </summary>
    /// <param name="personCode">شماره پرسنلی</param>
    /// <param name="fromDate">تاریخ شروع</param>
    /// <param name="toDate">تاریخ پایان</param>
    /// <returns></returns>
    private TAProxy Fill(string personCode, DateTime fromDate, DateTime toDate)
    {

        BPerson personBusiness = new BPerson();
        var person = personBusiness.GetByBarcode(personCode);

        if (person == null)
            return new TAProxy();

        IList<PersonalMonthlyReportRow> PersonnelMonthlyOperationList = null;
        PersonalMonthlyReportRow PersonnelSummaryMonthlyOperation = null;
        BPersonMonthlyWorkedTime MonthlyOperationBusiness = new BPersonMonthlyWorkedTime(person.ID);

        var personDateRangeReportProxy = MonthlyOperationBusiness.GetPersonDateRangeReport(fromDate, toDate).FirstOrDefault();

        TAProxy proxy = new TAProxy();
        proxy.PersonCode = personCode;
        proxy.HozourTime = personDateRangeReportProxy.PresenceDuration;  //35 * 60;//"35:00"
        proxy.KasreKarTime = personDateRangeReportProxy.HourlyUnallowableAbsence; //2 * 60 + 50;//"02:50"
        proxy.MamuriatTime = personDateRangeReportProxy.HourlyMission; //12 * 60;//"12:00"
        proxy.KarkerdSum = personDateRangeReportProxy.ImpureOperation; //47 * 60;//12:00 + 35:00
        return proxy;
    }
}

