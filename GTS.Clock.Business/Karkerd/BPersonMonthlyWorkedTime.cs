using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Runtime.Remoting.Messaging;
using GTS.Clock.Infrastructure;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.Log;
using GTS.Clock.Model;
using GTS.Clock.Model.Charts;
using GTS.Clock.Model.RequestFlow;
using GTS.Clock.Business.Charts;
using GTS.Clock.Business.RequestFlow;
using GTS.Clock.Model.MonthlyReport;
using GTS.Clock.Business.Proxy;
using System.Globalization;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Business.Security;
using GTS.Clock.Model.Concepts;
using Microsoft.Practices.Unity;
using GTS.Clock.Business.Temp;
using GTS.Clock.Business.Presentaion_Helper.Proxy;
using NHibernate.Transform;

namespace GTS.Clock.Business.WorkedTime
{
    /// <summary>
    /// گزارش کارکرد ماهیانه
    /// </summary>
    public class BPersonMonthlyWorkedTime : MarshalByRefObject
    {
        private const string ExceptionSrc = "GTS.Clock.Business.WorkedTime.BPersonMonthlyWorkedTime";
        private PersonRepository personRepository = new PersonRepository(false);
        private LanguagesName sysLanguageResource;
        private LanguagesName localLanguageResource;
        private string Username { get; set; }
        private decimal workingPersonId = 0;
        private Manager manager = new Manager();
        private GTSEngineWS.TotalWebServiceClient gtsEngineWS = new GTS.Clock.Business.GTSEngineWS.TotalWebServiceClient();

        #region Constructor

        //[InjectionConstructor]
        /// <summary>
        /// سازنده کلاس
        /// </summary>
        /// <param name="personId">کلید اصلی پرسنل</param>
        public BPersonMonthlyWorkedTime(decimal personId)
        {
            this.sysLanguageResource = AppSettings.BLanguage.CurrentSystemLanguage;
            this.localLanguageResource = AppSettings.BLanguage.CurrentLocalLanguage;
            workingPersonId = personId;
        }

        /// <summary>
        /// برای تست استفاده میشود
        /// </summary>
        /// <param name="username"></param>
        //public BPersonMonthlyWorkedTime(string username)
        //{
        //    this.Username = username;
        //    if (AppSettings.BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
        //    {
        //        this.sysLanguageResource = LanguagesName.Parsi;
        //    }
        //    else if (AppSettings.BLanguage.CurrentSystemLanguage == LanguagesName.English)
        //    {
        //        this.sysLanguageResource = LanguagesName.English;
        //    }
        //} 

        #endregion

        /// <summary>
        /// سطرهای گزارش کارکرد ماهانه را برای یک دوره یک ماهه برمیگرداند
        /// </summary>
        /// <param name="year">سال</param>
        /// <param name="month">ماه</param>
        /// <param name="fromDate">از تاریخ</param>
        /// <param name="toDate">تا تاریخ</param>
        /// <param name="DailyRows">لیست سطرهای روز گزارش کارکرد</param>
        /// <param name="MonthlyRow">لیست سطرهای ماه گزارش کارکرد</param>
        public void GetPersonMonthlyReport(int year, int month, string fromDate, string toDate, out IList<PersonalMonthlyReportRow> DailyRows, out PersonalMonthlyReportRow MonthlyRow)
        {
            try
            {
                if (Utility.IsEmpty(fromDate) || Utility.IsEmpty(toDate))
                {
                    throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.PersonDateRangeIsNotDefiend, String.Format("برای شخص {0} رینج محاسبات تعریف نشده است", workingPersonId), ExceptionSrc);
                }
                if (IsValidPeson())
                {
                    DateTime date = new DateTime(year, month, Utility.GetEndOfMiladiMonth(year, month));
                    if (sysLanguageResource == LanguagesName.Parsi)
                    {
                        date = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, month, Utility.GetEndOfPersianMonth(year, month)));
                    }
                    DateTime from, to;
                    if (sysLanguageResource == LanguagesName.Parsi)
                    {
                        from = Utility.ToMildiDate(fromDate);
                        to = Utility.ToMildiDate(toDate);
                    }
                    else
                    {
                        from = Utility.ToMildiDateTime(fromDate);
                        to = Utility.ToMildiDateTime(toDate);
                    }
                    //BTemp bTemp = new BTemp();
                    //List<decimal> list = new List<decimal>();
                    //list.Add(this.workingPersonId);
                    //string operationGUID = bTemp.InsertTempList(list);
                    //gtsEngineWS.GTS_ExecutePersonsByToDateGUID(BUser.CurrentUser.UserName, operationGUID, to);
                    //bTemp.DeleteTempList(operationGUID);
                    gtsEngineWS.GTS_ExecuteByPersonID(BUser.CurrentUser.UserName, this.workingPersonId);
                    PersonalMonthlyReport Result = new PersonalMonthlyReport(this.workingPersonId, date, month, from, to);
                    Result.LanguageName = sysLanguageResource;

                    DailyRows = Result.PersonalMonthlyReportRows;
                    MonthlyRow = DailyRows.FirstOrDefault();

                    foreach (PersonalMonthlyReportRow row in DailyRows)
                    {
                        //Day State Title
                        //fa:{0};en:{1}
                        if (!Utility.IsEmpty(row.DayStateTitle) && row.DayStateTitle.Contains(';'))
                        {
                            if (BLanguage.CurrentLocalLanguage == LanguagesName.Parsi)
                            {
                                row.DayStateTitle = Utility.Spilit(row.DayStateTitle, ';')[0].Replace("fa:", "");
                            }
                            else
                            {
                                row.DayStateTitle = Utility.Spilit(row.DayStateTitle, ';')[1].Replace("en:", "");
                            }
                        }
                        else
                        {
                            row.DayStateTitle = "";
                        }
                    }
                }
                else
                {
                    throw new IllegalServiceAccess(String.Format("این سرویس بعللت اعتبارسنجی قابل دسترسی نمیباشد. شناسه کاربری {0} میباشد", this.Username), ExceptionSrc);
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BPersonMonthlyWorkedTime", "GetPersonMonthlyReport");
                throw ex;
            }
        }

        /// <summary>
        /// گزارش کارکرد یک پرسنل در بازه تاریخی مشخص را بر می گرداند
        /// جهت استفاده در وب سرویس ارسال کارکرد به کارآزما پیاده سازی شده است
        /// </summary>
        /// <param name="fromDate">تاریخ ابتدا</param>
        /// <param name="toDate">تاریخ انتها</param>
        /// <param name="PersonId">کد پرسنلی</param>
        /// <returns></returns>
        public IList<PersonDateRangeReportProxy> GetPersonDateRangeReport(DateTime fromDate, DateTime toDate)
        {
            #region SqlQuery
            string SqlCommand = @" select  
                                     Prs_BarCode as Barcode
	                                ,FromDate
	                                ,ToDate
	                                ,d.ScndCnpValue_PersonId as PersonId
	                                ,Prs_CardNum  as CardNum
	                                ,CAST(SUM(gridFields_PresenceDuration) as int) AS PresenceDuration 
	                                ,CAST(SUM(gridFields_ImpureOperation) as int) AS ImpureOperation 
	                                ,CAST(SUM(gridFields_HourlyMission) as int) AS HourlyMission 
	                                ,CAST(SUM(gridFields_HourlyUnallowableAbsence) as int) AS HourlyUnallowableAbsence 
                                from(
	                                SELECT Prs_BarCode ,Prs_ID,ScndCnpValue_PeriodicFromDate as FromDate,ScndCnpValue_PeriodicToDate as ToDate,ThirdExit,ThirdEntrance,SecondExit,SecondEntrance,FirstExit,FirstEntrance ,[Date] ,ScndCnpValue_PersonId
		                                ,Prs_CardNum ,gridFields_PresenceDuration ,gridFields_ImpureOperation ,gridFields_HourlyMission ,gridFields_HourlyUnallowableAbsence 
	                                FROM (							        
			                                SELECT	Entrance.FirstEntrance, [Exit].FirstExit, 
					                                Entrance.SecondEntrance, [Exit].SecondExit,
					                                Entrance.ThirdEntrance, [Exit].ThirdExit,
					                                Entrance.ProceedTraffic_FromDate,
					                                Entrance.ProceedTraffic_PersonId	   	
			                                FROM (SELECT	ISNULL(SUM([1]), -1000) [FirstEntrance], 
							                                ISNULL(SUM([2]), -1000) [SecondEntrance], 
							                                ISNULL(SUM([3]), -1000) [ThirdEntrance], 
							                                ProceedTraffic_FromDate, ProceedTraffic_PersonId
					                                FROM (
							                                SELECT RANK() OVER (PARTITION BY ProceedTraffic_PersonId, ProceedTraffic_FromDate ORDER BY ProceedTrafficPair_From) Rk, *
							                                FROM TA_ProceedTraffic PrcTrf
							                                INNER JOIN TA_ProceedTrafficPair PrcTrfPair		ON PrcTrf.ProceedTraffic_ID = PrcTrfPair.ProceedTrafficPair_ProceedTrafficId
							                                INNER JOIN TA_Person							ON PrcTrf.ProceedTraffic_PersonId = TA_Person.Prs_ID
							                                WHERE TA_Person.Prs_ID = :PersonId
						                                ) AS PrcTrf
					                                PIVOT (SUM(ProceedTrafficPair_From) FOR RK IN([1], [2], [3]) ) as pvt
					                                GROUP BY ProceedTraffic_PersonId, ProceedTraffic_FromDate
				                                ) AS Entrance
			                                INNER JOIN (SELECT	ISNULL(SUM([1]), -1000) [FirstExit], 
								                                ISNULL(SUM([2]), -1000) [SecondExit], 
								                                ISNULL(SUM([3]), -1000) [ThirdExit], 
								                                ProceedTraffic_FromDate, ProceedTraffic_PersonId
						                                FROM (
								                                SELECT RANK() OVER (PARTITION BY ProceedTraffic_PersonId, ProceedTraffic_FromDate ORDER BY ProceedTrafficPair_From) Rk, *
								                                FROM TA_ProceedTraffic PrcTrf
								                                INNER JOIN TA_ProceedTrafficPair PrcTrfPair		ON PrcTrf.ProceedTraffic_ID = PrcTrfPair.ProceedTrafficPair_ProceedTrafficId
								                                INNER JOIN TA_Person							ON PrcTrf.ProceedTraffic_PersonId = TA_Person.Prs_ID
								                                WHERE TA_Person.Prs_ID = :PersonId
							                                ) AS PrcTrf
						                                PIVOT (SUM(ProceedTrafficPair_To) FOR RK IN([1], [2], [3])) as pvt
						                                GROUP BY ProceedTraffic_PersonId, ProceedTraffic_FromDate
					                                ) [Exit]
			                                ON Entrance.ProceedTraffic_FromDate = [Exit].ProceedTraffic_FromDate AND Entrance.ProceedTraffic_PersonId = [Exit].ProceedTraffic_PersonId
	                                ) PrcTraffic
	                                RIGHT JOIN ( SELECT		ScndCnpValue_PersonId
							                                ,ScndCnpValue_FromDate [Date]
							                                ,ScndCnpValue_PeriodicFromDate
							                                ,ScndCnpValue_PeriodicToDate
							                                ,ISNULL(SUM(gridFields_PresenceDuration), 0) AS gridFields_PresenceDuration 
							                                ,ISNULL(SUM(gridFields_ImpureOperation), 0) AS gridFields_ImpureOperation 
							                                ,ISNULL(SUM(gridFields_HourlyMission), 0) AS gridFields_HourlyMission 
							                                ,ISNULL(SUM(gridFields_HourlyUnallowableAbsence), 0) AS gridFields_HourlyUnallowableAbsence 
				                                FROM (	SELECT	[No],
								                                ScndCnpValues.ScndCnpValue_ID,
								                                CnpTmp.ConceptTmp_FnName,
								                                CnpTmp.ConceptTmp_EngName,
								                                CnpTmp.ConceptTmp_KeyColumnName,
								                                PeriodicCnpValue.ScndCnpValue_PeriodicFromDate,
								                                PeriodicCnpValue.ScndCnpValue_PeriodicToDate,
								                                PeriodicCnpValue.ScndCnpValue_PersonId,
								                                ScndCnpValues.ScndCnpValue_FromDate,
								                                ScndCnpValues.ScndCnpValue_ToDate,
								                                ScndCnpValues.ScndCnpValue_Value
						                                FROM (	SELECT	[No],
										                                PrdCnpTmpDetail_DtlCnpTmpId		AS ScndCnpValue_DailyScndCnpId,
										                                PeriodicCnp_KeyColumnName		AS ScndCnpValue_KeyColumnName,     
										                                PeriodicCnp_FromDate			AS ScndCnpValue_PeriodicFromDate,	   
										                                PeriodicCnp_ToDate				AS ScndCnpValue_PeriodicToDate,
										                                PeriodicCnp_CnpTmpId			AS ScndCnpValue_PeriodicScndCnpId, 
										                                PeriodicCnp_PersonId			AS ScndCnpValue_PersonId
								                                FROM dbo.TA_PeriodicCnpTmpDetail 
								                                INNER JOIN(	SELECT	PrsRangeAsg.[No],
													                                CalcDateRange_ID					AS PeriodicCnp_ID,
													                                PrsRangeAsg.PrsRangeAsg_PersonId	AS PeriodicCnp_PersonId, 
													                                CalcDateRange_ConceptTmpId			AS PeriodicCnp_CnpTmpId, 
													                                Concept.ConceptTmp_FnName			AS PeriodicCnp_KeyColumnName, 
													                                :fromDate							AS PeriodicCnp_FromDate,
													                                :toDate								AS PeriodicCnp_ToDate
											                                FROM (SELECT *  FROM dbo.TA_CalculationDateRange WHERE CalcDateRange_Order = 1) AS CalcDateRng
											                                INNER JOIN (SELECT * FROM dbo.TA_ConceptTemplate WHERE ConceptTmp_IsPeriodic = 1) AS Concept ON CalcDateRange_ConceptTmpId = Concept.ConceptTmp_ID		  
											                                INNER JOIN (SELECT * FROM (	SELECT	ROW_NUMBER() OVER (PARTITION BY PrsRangeAsg_PersonId ORDER BY PrsRangeAsg_FromDate DESC) AS [No], 
																				                                PrsRangeAsg_PersonId, PrsRangeAsg_CalcRangeGrpId
																		                                FROM TA_PersonRangeAssignment                                          
																		                                INNER JOIN TA_Person ON PrsRangeAsg_PersonId = TA_Person.Prs_ID		 					 
																		                                WHERE PrsRangeAsg_FromDate <= :toDate AND TA_Person.Prs_ID = :PersonId	
																	                                  ) AS [Range]
																                                 WHERE [Range].[No] = 1
														                                ) AS PrsRangeAsg			ON CalcDateRange_CalcRangeGrpId = PrsRangeAsg.PrsRangeAsg_CalcRangeGrpId
											                                INNER JOIN TA_CalculationRangeGroup		ON CalcDateRange_CalcRangeGrpId = CalcRangeGroup_ID
											                                WHERE ConceptTmp_KeyColumnName IS NOT NULL AND Len(ConceptTmp_KeyColumnName) <> 0              
										                                 ) AS PeriodicCnp	ON PrdCnpTmpDetail_PrdCnpTmpId = PeriodicCnp.PeriodicCnp_CnpTmpId
							                                ) AS PeriodicCnpValue     
						                                INNER JOIN TA_ConceptTemplate CnpTmp ON CnpTmp.ConceptTmp_ID = PeriodicCnpValue.ScndCnpValue_DailyScndCnpId
						                                CROSS APPLY dbo.TA_GetScndCnpValues(PeriodicCnpValue.ScndCnpValue_PersonId,PeriodicCnpValue.ScndCnpValue_DailyScndCnpId,PeriodicCnpValue.ScndCnpValue_PeriodicFromDate,PeriodicCnpValue.ScndCnpValue_PeriodicToDate) AS  ScndCnpValues 							        
			                                ) AS [Source]		
			                                PIVOT ( SUM(ScndCnpValue_Value) FOR ConceptTmp_KeyColumnName IN (gridFields_PresenceDuration ,gridFields_ImpureOperation ,gridFields_HourlyMission ,gridFields_HourlyUnallowableAbsence )) AS Result			
			                                GROUP BY ScndCnpValue_PersonId, ScndCnpValue_FromDate ,ScndCnpValue_PeriodicFromDate,ScndCnpValue_PeriodicToDate
		                                ) ScndCnpValue			ON PrcTraffic.ProceedTraffic_FromDate = ScndCnpValue.Date AND PrcTraffic.ProceedTraffic_PersonId = ScndCnpValue.ScndCnpValue_PersonId
	                                INNER JOIN TA_Person Prs	ON ScndCnpValue.ScndCnpValue_PersonId = Prs.Prs_ID 
	                                INNER JOIN TA_PersonDetail	ON Prs_ID = TA_PersonDetail.PrsDtl_ID
	                                INNER JOIN TA_PersonTASpec	ON Prs_ID = prsTA_ID 
	                                LEFT OUTER JOIN (	select CFP_PrsId, case when  CFP_CalculationIsValid=0 and CFP_Date<:toDate  then 'توجه : برای اطمینان از صحت محاسبات ، لازم به محاسبه می باشد.' else '' end  resultCalcValidation  
						                                from TA_Calculation_Flag_Persons 
						                                INNER JOIN TA_Temp temp ON CFP_PrsId = temp.temp_ObjectID
						                                INNER JOIN TA_Person ON CFP_PrsId = TA_Person.Prs_ID
						                                where TA_Person.Prs_ID = :PersonId
					                                ) validationCalc on ScndCnpValue.ScndCnpValue_PersonId =validationCalc.CFP_PrsId
                                ) as d
                                Where  ( gridFields_PresenceDuration <>-1000 and gridFields_PresenceDuration <> 0 )  or  ( gridFields_ImpureOperation <>-1000 and gridFields_ImpureOperation <> 0 )  or  ( gridFields_HourlyMission <>-1000 and gridFields_HourlyMission <> 0 )  or  ( gridFields_HourlyUnallowableAbsence <>-1000 and gridFields_HourlyUnallowableAbsence <> 0 )  
                                group by d.ScndCnpValue_PersonId,Prs_Barcode,FromDate,ToDate ,Prs_CardNum ";

            #endregion

            IList<PersonDateRangeReportProxy> ReportList = NHibernateSessionManager.Instance.GetSession().CreateSQLQuery(SqlCommand)
                                                                                     .SetParameter("PersonId", this.workingPersonId)
                                                                                     .SetParameter("fromDate", fromDate.Date)
                                                                                     .SetParameter("toDate", toDate.Date)
                                                                                     .SetResultTransformer(Transformers.AliasToBean(typeof(PersonDateRangeReportProxy)))
                                                                                     .List<PersonDateRangeReportProxy>();
            return ReportList;
        }

        /// <summary>
        /// نمای گرافیکی گزارش کارکرد ماهیانه کاربر جاری را برمی گرداند
        /// </summary>
        /// <param name="year">سال</param>
        /// <param name="month">ماه</param>
        /// <param name="fromDate">تاریخ ابتدا</param>
        /// <param name="toDate">تاریخ انتها</param>
        /// <returns>لیست سطرهای گزارش کارکرد</returns>
        public IList<PersonalMonthlyReportRow> GetPersonGanttChart(int year, int month, string fromDate, string toDate)
        {
            IList<PersonalMonthlyReportRow> DailyRows = new List<PersonalMonthlyReportRow>();

            try
            {
                if (Utility.IsEmpty(fromDate) || Utility.IsEmpty(toDate))
                {
                    throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.PersonDateRangeIsNotDefiend, String.Format("برای شخص {0} رینج محاسبات تعریف نشده است", workingPersonId), ExceptionSrc);
                }
                if (IsValidPeson())
                {
                    DateTime from, to;
                    if (sysLanguageResource == LanguagesName.Parsi)
                    {
                        from = Utility.ToMildiDate(fromDate);
                        to = Utility.ToMildiDate(toDate);
                    }
                    else
                    {
                        from = Utility.ToMildiDateTime(fromDate);
                        to = Utility.ToMildiDateTime(toDate);
                    }
                    gtsEngineWS.GTS_ExecuteByPersonID(BUser.CurrentUser.UserName, this.workingPersonId);
                    PersonalMonthlyReport Result = new PersonalMonthlyReport(this.workingPersonId, from, to);
                    Result.LanguageName = sysLanguageResource;

                    DailyRows = Result.PersonalGanttChartRows;

                }
                else
                {
                    throw new IllegalServiceAccess(String.Format("این سرویس بعللت اعتبارسنجی قابل دسترسی نمیباشد. شناسه کاربری {0} میباشد", this.Username), ExceptionSrc);
                }
                return DailyRows;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BPersonMonthlyWorkedTime", "GetPersonMonthlyReport");
                throw ex;
            }
        }

        /// <summary>
        /// بازه ساعت شبانه روز که در گانت چارت نمایش داده میشود
        /// </summary>
        /// <param name="min">حداقل</param>
        /// <param name="max">حداکثر</param>
        public void GetMinMaxHourForGanttChart(out int min, out int max)
        {
            min = 7 * 60;
            max = 23 * 60;
        }

        /// <summary>
        /// گزارش کارکرد یک روز پرسنل جاری را بر می گرداند
        /// </summary>
        /// <param name="dayDate">تاریخ</param>
        /// <returns>سطر گزارش کارکرد</returns>
        public PersonalMonthlyReportRow GetPersonDailyReport(DateTime dayDate)
        {
            try
            {
                IList<PersonalMonthlyReportRow> DailyRows = new List<PersonalMonthlyReportRow>();

                if (IsValidPeson())
                {
                    PersianDateTime p = new PersianDateTime(dayDate);
                    DateTime endOfMonth;
                    if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                    {
                        endOfMonth = PersianDateTime.GetEndOfShamsiMonth(p.Year, p.Month).GregorianDate;
                    }
                    else
                    {
                        endOfMonth = new DateTime(dayDate.Year, dayDate.Month, DateTime.DaysInMonth(dayDate.Year, dayDate.Month));
                    }

                    BTemp bTemp = new BTemp();
                    List<decimal> list = new List<decimal>();
                    list.Add(this.workingPersonId);
                    string operationGUID = bTemp.InsertTempList(list);
                    gtsEngineWS.GTS_ExecuteByPersonIdAndToDate(BUser.CurrentUser.UserName, this.workingPersonId, dayDate.AddDays(1));
                    PersonalMonthlyReport Result = new PersonalMonthlyReport(this.workingPersonId, endOfMonth, new PersianDateTime(endOfMonth).Month, dayDate, dayDate);
                    Result.LanguageName = sysLanguageResource;

                    DailyRows = Result.PersonalMonthlyReportRows;

                    return DailyRows.First();
                }
                else
                {
                    throw new IllegalServiceAccess(String.Format("این سرویس بعللت اعتبارسنجی قابل دسترسی نمیباشد. شناسه کاربری {0} میباشد", this.Username), ExceptionSrc);
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BPersonMonthlyWorkedTime", "GetPersonDailyReport");
                throw ex;
            }
        }

        //DNN NOTE
        /// <summary>
        /// جزئیات گزارش کارکرد یک روز را برمیگرداند
        /// </summary>
        /// <param name="date">تاریخ</param>
        /// <returns>لیست پروکسی جزئیات گزارش کارکد </returns>
        public IList<MonthlyDetailReportProxy> GetPersonMonthlyRowDetail(DateTime date)
        {
            //DNN Note
            IDashboardBRequest requestBusiness = new BRequest(this.workingPersonId);
            //End of DNN Note
            try
            {
                if (IsValidPeson())
                {
                    //DNN Note
                    decimal rowId = date.Date.Day * 100;
                    //End of DNN Note
                    PrsMonthlyRptRepository prsMonthlyRep = new PrsMonthlyRptRepository(false);
                    List<MonthlyDetailReportProxy> detailList = new List<MonthlyDetailReportProxy>();
                    IList<PersonalMonthlyReportRowDetail> list = prsMonthlyRep.LoadPairableScndcnpValue(this.workingPersonId, date.Date);
                    foreach (PersonalMonthlyReportRowDetail detail in list)
                    {
                        for (int i = 0; i < detail.Pairs.Count; i++)
                        {
                            if (!detail.ScndCnpName.Contains("خالص") && !detail.ScndCnpName.Contains("چارت") && detail.ScndShowInDetail)
                            {
                                MonthlyDetailReportProxy proxy = new MonthlyDetailReportProxy();
                                //DNN Note
                                rowId = rowId + 1;
                                proxy.ID = rowId;
                                //End of DNN Note
                                proxy.From = Utility.IntTimeToRealTime(detail.Pairs[i].From);
                                proxy.To = Utility.IntTimeToRealTime(detail.Pairs[i].To);
                                proxy.Name = detail.ScndCnpName;
                                proxy.Color = detail.Color;
                                if (detail.Pairs[i].From > 0 && detail.Pairs[i].To == 0)
                                {
                                    if (sysLanguageResource == LanguagesName.Parsi)
                                    {
                                        proxy.Description = "عدم وجود زوج مرتب بعلت خطای احتمالی در تعریف قوانین";
                                    }
                                    else
                                    {
                                        proxy.Description = "No pair is available maybe caused by rules definition";
                                    }
                                }
                                //DNN NOTE
                                if (proxy.Name == "غیبت ساعتی" || proxy.Name == "غیبت روزانه")
                                {
                                    int UnderReviewCount = requestBusiness.GetAllRequestCountInDay(this.workingPersonId, date, RequestState.UnderReview, detail.Pairs[i].From, detail.Pairs[i].To);
                                    int ConfirmedCount = requestBusiness.GetAllRequestCountInDay(this.workingPersonId, date, RequestState.Confirmed, detail.Pairs[i].From, detail.Pairs[i].To);
                                    if (UnderReviewCount > 0)
                                    {
                                        proxy.Description = proxy.Description + string.Format("تعداد {0} درخواست در حال بررسی می باشد. ", UnderReviewCount);
                                    }
                                    if (ConfirmedCount > 0)
                                    {
                                        proxy.Description = proxy.Description + string.Format("تعداد {0} درخواست تایید شده است. ", ConfirmedCount);
                                    }
                                }
                                //END OF DNN NOTE

                                detailList.Add(proxy);
                            }
                        }
                    }
                    return detailList;
                }
                else
                {
                    throw new IllegalServiceAccess(String.Format("این سرویس بعللت اعتبارسنجی قابل دسترسی نمیباشد. شناسه کاربری {0} میباشد", this.Username), ExceptionSrc);
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BPersonMonthlyWorkedTime", "GetPersonMonthlyRowDetail");
                throw ex;
            }
        }
        //END OF DNN NOte
        /// <summary>
        /// یک سال را دریافت میکند و 12 ماه را همراه با شروع و پایان آن برمیگرداند
        /// </summary>
        /// <param name="year">سال</param>
        /// <returns>لیست پروکسی بازه های زمانی در یک سال</returns>
        public IList<DateRangeOrderProxy> GetDateRangeOrder(int year)
        {
            using (NHibernateSessionManager.Instance.BeginTransactionOn())
            {
                try
                {
                    if (IsValidPeson())
                    {
                        IList<DateRangeOrderProxy> list = new List<DateRangeOrderProxy>();


                        for (int i = 1; i <= 12; i++)
                        {
                            DateRangeOrderProxy proxy = new DateRangeOrderProxy();
                            DateTime date = new DateTime(year, i, DateTime.DaysInMonth(year, i));
                            if (sysLanguageResource == LanguagesName.Parsi)
                            {
                                int endOfMonth = new PersianCalendar().GetDaysInMonth(year, i);
                                date = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, i, endOfMonth));
                            }
                            PersonalMonthlyReport report = personRepository.GetPersonalMonthlyReport(this.workingPersonId, date, i);
                            if (report.DataRangeIsValid)
                            {
                                proxy.Order = i;
                                proxy.Selected = false;
                                if (sysLanguageResource == LanguagesName.Parsi)
                                {
                                    proxy.FromDate = Utility.ToPersianDate(report.FromDate);
                                    proxy.ToDate = Utility.ToPersianDate(report.ToDate);
                                }
                                else
                                {
                                    proxy.FromDate = report.FromDate;
                                    proxy.ToDate = report.ToDate;
                                }
                                if (report.MinDate.Date <= DateTime.Now && DateTime.Now.Date <= report.MaxDate)
                                {
                                    proxy.Selected = true;
                                }
                                list.Add(proxy);
                            }
                        }
                        if (list.Count == 0)
                        {
                            UIValidationExceptions exeptions = new UIValidationExceptions();
                            exeptions.Add(new ValidationException(ExceptionResourceKeys.PersonnelCalculationDateRangeIsNotValuedForSelectedYear, "در سال انتخاب شده محدوده محاسبات به پرسنل تخصیص داده نشده است", ExceptionSrc));
                            throw exeptions;
                        }
                        list = list.OrderBy(x => x.Order).ToList();
                        if (list.Where(x => x.Selected).Count() == 0)
                        {
                            if (Utility.ToPersianDateTime(DateTime.Now).Year == year + 1)//سال قبل
                            {
                                list[list.Count - 1].Selected = true;
                            }
                            else
                            {
                                list[0].Selected = true;
                            }
                        }
                        NHibernateSessionManager.Instance.CommitTransactionOn();
                        return list;
                    }
                    else
                    {
                        throw new IllegalServiceAccess(String.Format("این سرویس بعللت اعتبارسنجی قابل دسترسی نمیباشد. شناسه کاربری {0} میباشد", this.Username), ExceptionSrc);
                    }
                }
                catch (Exception ex)
                {
                    NHibernateSessionManager.Instance.RollbackTransactionOn();
                    BaseBusiness<Entity>.LogException(ex, "BPersonMonthlyWorkedTime", "GetDateRangeOrder");
                    throw ex;
                }
            }
        }

        /// <summary>
        /// سر ستون رزورو فیلدها را در گزارش کارکرد ماهانه برمیگرداند
        /// </summary>
        /// <param name="field">فیلد رزرو شده</param>
        /// <returns>سرستون</returns>
        public string GetReservedFieldsName(ConceptReservedFields field)
        {
            try
            {
                string keyColumn = "";
                switch (field)
                {
                    case ConceptReservedFields.ReserveField1:
                        keyColumn = "gridFields_ReserveField1";
                        break;
                    case ConceptReservedFields.ReserveField2:
                        keyColumn = "gridFields_ReserveField2";
                        break;
                    case ConceptReservedFields.ReserveField3:
                        keyColumn = "gridFields_ReserveField3";
                        break;
                    case ConceptReservedFields.ReserveField4:
                        keyColumn = "gridFields_ReserveField4";
                        break;
                    case ConceptReservedFields.ReserveField5:
                        keyColumn = "gridFields_ReserveField5";
                        break;
                    case ConceptReservedFields.ReserveField6:
                        keyColumn = "gridFields_ReserveField6";
                        break;
                    case ConceptReservedFields.ReserveField7:
                        keyColumn = "gridFields_ReserveField7";
                        break;
                    case ConceptReservedFields.ReserveField8:
                        keyColumn = "gridFields_ReserveField8";
                        break;
                    case ConceptReservedFields.ReserveField9:
                        keyColumn = "gridFields_ReserveField9";
                        break;
                    case ConceptReservedFields.ReserveField10:
                        keyColumn = "gridFields_ReserveField10";
                        break;
                }
                SecondaryConceptRepository rep = new SecondaryConceptRepository(false);
                IList<SecondaryConcept> list = rep.Find().
                    Where(x => x.KeyColumnName != null && x.KeyColumnName != ""
                        && x.KeyColumnName == keyColumn).ToList<SecondaryConcept>();
                SecondaryConcept concept = list.FirstOrDefault();
                if (concept != null)
                {
                    if (BLanguage.CurrentLocalLanguage == LanguagesName.Parsi)
                    {
                        return concept.FnName;
                    }
                    else
                    {
                        return concept.EnName;
                    }
                }
                return String.Empty;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, this.GetType().Name, "GetReservedFieldsName");
                throw ex;
            }

        }

        /// <summary>
        /// سر ستون رزرو فیلدها را در گزارش کارکرد ماهانه برمیگرداند
        /// </summary>
        /// <returns>دیکشنری سرستون فیلدهای رزرو</returns>
        public IDictionary<ConceptReservedFields, string> GetReservedFieldsNames()
        {
            try
            {
                IList<string> reserveFields = new List<string>() { "gridFields_ReserveField1","gridFields_ReserveField2","gridFields_ReserveField3"
                                                                    ,"gridFields_ReserveField4","gridFields_ReserveField5","gridFields_ReserveField6"
                                                                    ,"gridFields_ReserveField7","gridFields_ReserveField8","gridFields_ReserveField9"
                                                                    ,"gridFields_ReserveField10"};


                SecondaryConceptRepository rep = new SecondaryConceptRepository(false);
                IList<SecondaryConcept> list = rep.GetAllByKeyNames(reserveFields);
                list = list == null ? new List<SecondaryConcept>() : list;

                IDictionary<ConceptReservedFields, string> dic = new Dictionary<ConceptReservedFields, string>();
                foreach (string field in reserveFields)
                {
                    ConceptReservedFields keyColumn = new ConceptReservedFields();
                    switch (field)
                    {
                        case "gridFields_ReserveField1":
                            keyColumn = ConceptReservedFields.ReserveField1;
                            break;
                        case "gridFields_ReserveField2":
                            keyColumn = ConceptReservedFields.ReserveField2;
                            break;
                        case "gridFields_ReserveField3":
                            keyColumn = ConceptReservedFields.ReserveField3;
                            break;
                        case "gridFields_ReserveField4":
                            keyColumn = ConceptReservedFields.ReserveField4;
                            break;
                        case "gridFields_ReserveField5":
                            keyColumn = ConceptReservedFields.ReserveField5;
                            break;
                        case "gridFields_ReserveField6":
                            keyColumn = ConceptReservedFields.ReserveField6;
                            break;
                        case "gridFields_ReserveField7":
                            keyColumn = ConceptReservedFields.ReserveField7;
                            break;
                        case "gridFields_ReserveField8":
                            keyColumn = ConceptReservedFields.ReserveField8;
                            break;
                        case "gridFields_ReserveField9":
                            keyColumn = ConceptReservedFields.ReserveField9;
                            break;
                        case "gridFields_ReserveField10":
                            keyColumn = ConceptReservedFields.ReserveField10;
                            break;
                    }

                    SecondaryConcept cnp = list.Where(x => x.KeyColumnName == field).FirstOrDefault();

                    if (cnp != null)
                    {
                        if (BLanguage.CurrentLocalLanguage == LanguagesName.Parsi)
                        {
                            dic.Add(keyColumn, cnp.FnName);
                        }
                        else
                        {
                            dic.Add(keyColumn, cnp.EnName);
                        }
                    }
                    else
                    {
                        dic.Add(keyColumn, String.Empty);
                    }
                }
                return dic;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, this.GetType().Name, "GetReservedFieldsNames");
                throw ex;
            }

        }

        /// <summary>
        /// بررسی دسترسی نمای شبکه ای گزارش کارکرد ماهیانه برای مدیر
        /// </summary>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckMonthlyOperationGridSchemaLoadAccess_onManagerState()
        {
        }

        /// <summary>
        /// بررسی دسترسی نمای گرافیکی گزارش کارکرد ماهیانه برای مدیر 
        /// </summary>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckMonthlyOperationGanttChartSchemaLoadAccess_onManagerState()
        {
        }

        /// <summary>
        /// بررسی دسترسی نمای شبکه ای گزارش کارکرد ماهیانه برای پرسنل 
        /// </summary>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckMonthlyOperationGridSchemaLoadAccess_onPersonnelState()
        {
        }

        /// <summary>
        /// بررسی دسترسی نمای گرافیکی گزارش کارکرد ماهیانه برای پرسنل 
        /// </summary>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckMonthlyOperationGanttChartSchemaLoadAccess_onPersonnelState()
        {
        }

        /// <summary>
        /// بررسی دسترسی جزئیات نمای گرافیکی گزارش کارکرد ماهیانه برای پرسنل 
        /// </summary>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckMonthlyOperationGridSchemaDetailsRowsAccess_onPersonnelState()
        {
        }

        #region private methods

        /// <summary>
        /// ماه تاریخ را بر اساس زبان سیستم بر می گرداند
        /// </summary>
        /// <param name="dt">تاریخ</param>
        /// <param name="sysLanguageResource">زبان سیستم</param>
        /// <returns>ماه</returns>
        private int GetOrder(DateTime dt, SysLanguageResource sysLanguageResource)
        {
            switch (sysLanguageResource)
            {
                case SysLanguageResource.English: return dt.Month;
                default: return (new PersianDateTime(dt)).Month;
            }
        }

        /// <summary>
        /// اگر شناسه پرسنل صفر باشد باید از شناسه کاربری بازیابی شود
        /// اگر شناسه پرسنل صفر نباشد معنایش این است که از برم مدیریتی گزارش کارکرد وارد شده ایم
        /// و میتوان آنها را اعتبارسنجی کرد
        /// </summary>
        /// <returns>بلی/خیر</returns>
        private bool IsValidPeson()
        {
            if (workingPersonId > 0)
            {
                return true;
            }
            else //for testing
            {
                if (Utility.IsEmpty(this.Username))
                {
                    this.Username = Security.BUser.CurrentUser.UserName;
                }
                UserRepository userRep = new UserRepository(false);
                Model.Security.User user = userRep.GetByUserName(this.Username);
                if (user != null && user.Person != null && user.Person.ID > 0)
                {
                    this.workingPersonId = user.Person.ID;
                    NHibernateSessionManager.Instance.ClearSession();
                    return true;
                }
            }
            return false;
        }

        #endregion

    }
}