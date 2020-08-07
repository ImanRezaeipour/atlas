using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Runtime.Remoting.Messaging;
using GTS.Clock.Infrastructure;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.Log;
using GTS.Clock.Model;
using GTS.Clock.Model.Charts;
using GTS.Clock.Model.RequestFlow;
using GTS.Clock.Business.Charts;
using GTS.Clock.Business.RequestFlow;
using GTS.Clock.Model.MonthlyReport; 
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Business.Leave;
using GTS.Clock.Business.Security;
using GTS.Clock.Business.Presentaion_Helper.Proxy;
using GTS.Clock.Infrastructure.NHibernateFramework;
using System.Collections;
using NHibernate.Transform;

namespace GTS.Clock.Business.WorkedTime
{
    /// <summary>
    /// گزارش کارکرد مدیریتی ماهیانه
    /// </summary>
    public class BWorkedTime : MarshalByRefObject
    {
        IDataAccess accessPort = new BUser();
        const string ExceptionSrc = "GTS.Clock.Business.WorkedTime.BWorkedTime";
        ManagerRepository managerRepository = new ManagerRepository(false);
        LanguagesName sysLanguageResource;

        /// <summary>
        /// نام کاربری
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// مدیر مربوطه
        /// </summary>
        private Manager manager = null;

        /// <summary>
        /// سازنده کلاس
        /// </summary>
        public BWorkedTime()
        {
            if (AppSettings.BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
            {
                this.sysLanguageResource = LanguagesName.Parsi;
            }
            else if (AppSettings.BLanguage.CurrentSystemLanguage == LanguagesName.English)
            {
                this.sysLanguageResource = LanguagesName.English;
            }
        }

        /// <summary>
        /// سازنده کلاس
        /// </summary>
        /// <param name="username">نام کاربری</param>
        public BWorkedTime(string username)
        {
            this.Username = username;
            if (AppSettings.BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
            {
                this.sysLanguageResource = LanguagesName.Parsi;
            }
            else if (AppSettings.BLanguage.CurrentSystemLanguage == LanguagesName.English)
            {
                this.sysLanguageResource = LanguagesName.English;
            }
        }

        /// <summary>
        /// اگر کاربر فعلی مدیر باشد درختی که تحت مدیریت او است را با خصیصه "پدیداری" مشخص میکند
        /// </summary>
        /// <returns>درخت چارت سازمانی تحت مدیریت مدیر</returns>
        public Department GetManagerDepartmentTree()
        {
            try
            {
                if (InitManager())
                {
                    Department root = new BDepartment().GetManagerDepartmentTree(manager.ID);                
                    return root;
                }
                else if (InitOperator()) 
                {
                    Department root = new BDepartment().GetOperatorDepartmentTree(BUser.CurrentUser.Person.ID);
                    return root;
                }
                else
                {
                    throw new IllegalServiceAccess(String.Format("این سرویس تنها توسط مدیران قابل استفاده میباشد. شناسه کاربری {0} میباشد", this.Username), ExceptionSrc);
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BWorkedTime", "GetManagerDepartmentTree");
                throw ex;
            }
        }

        /// <summary>
        /// با دریافت یک گره از درخت تحت مدیریت مدیر تعداد اشخاص تحت مدیریت را برمیگرداند
        /// </summary>
        /// <param name="month">ماه</param>
        /// <param name="departmentID">کلید اصلی گره چارت سازمانی</param>
        /// <returns>تعداد اشخاص تحت مدیریت</returns>
        public int GetUnderManagmentByDepartmentCount(int month, decimal departmentID)
        {
            try
            {
                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    if (month <= 0)
                    {
                        month = Utility.ToPersianDateTime(DateTime.Now).Month;
                    }
                }
                else
                {
                    if (month <= 0)
                    {
                        month = DateTime.Now.Month;
                    }
                }
                if (InitManager())
                {
                    int Result = managerRepository.GetUnderManagmentByDepartmentCount(GridSearchFields.NONE, BUser.CurrentUser.Person.ID, departmentID, "", "");
                    return Result;
                }
                else if (InitOperator())
                {
                    int Result = managerRepository.GetUnderManagmentOperatorByDepartmentCount(GridSearchFields.NONE, BUser.CurrentUser.Person.ID, departmentID, "", "");
                    return Result;
                }
                else
                {
                    throw new IllegalServiceAccess(String.Format("این سرویس تنها توسط مدیران قابل استفاده میباشد. شناسه کاربری {0} میباشد", this.Username), ExceptionSrc);
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BWorkedTime", "GetUnderManagmentByDepartmentCount");
                throw ex;
            }
        }

        /// <summary>
        /// با دریافت یک گره از درخت تحت مدیریت مدیر تعداد اشخاص تحت مدیریت را برمیگرداند
        /// جریان اصلی را لحاظ میکند
        /// جانشین را برنمیگرداند
        /// </summary>
        /// <param name="month">ماه</param>
        /// <param name="departmentID">کلید اصلی گره چارت سازمانی</param>
        /// <returns>تعداد اشخاص تحت مدیریت</returns>
        public int GetJustUnderManagmentMainFlowByDepartmentCount(int month, decimal departmentID,decimal managerPersonId)
        {
            try
            {
                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    if (month <= 0)
                    {
                        month = Utility.ToPersianDateTime(DateTime.Now).Month;
                    }
                }
                else
                {
                    if (month <= 0)
                    {
                        month = DateTime.Now.Month;
                    }
                }
                if (IsManager(managerPersonId))
                {
                    int Result = managerRepository.GetUnderManagmentByDepartment_JustMainManagers_Count(GridSearchFields.NONE, managerPersonId, departmentID, "", "");
                    return Result;
                }
                else
                {
                    throw new IllegalServiceAccess(String.Format("این سرویس تنها توسط مدیران قابل استفاده میباشد. شناسه کاربری {0} میباشد", this.Username), ExceptionSrc);
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BWorkedTime", "GetJustUnderManagmentMainFlowByDepartmentCount");
                throw ex;
            }
        }

        /// <summary>
        /// با دریافت یک گره از درخت تحت مدیریت مدیر اشخاص تحت مدیریت را برمیگرداند
        /// این افراد مرتب شده بر اساس فیلد مشخص شده هستند 
        /// </summary>
        /// <param name="month">ماه</param>
        /// <param name="departmentID">کلید اصلی گره چارت سازمانی</param>
        /// <param name="pageIndex">شماره صفحه</param>
        /// <param name="pageSize">تعداد رکوردهای صفحه</param>
        /// <param name="orderField">فیلد مرتب سازی</param>
        /// <param name="orderType">نوع مرتب سازی</param>
        /// <returns>لیست پرسنل تحت مدیریت</returns>
        public IList<UnderManagementPerson> GetUnderManagmentByDepartment(int month, decimal departmentID, int pageIndex, int pageSize, GridOrderFields orderField, GridOrderFieldType orderType)
        {
            try
            {
                int year = 0;
                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    year = Utility.ToPersianDateTime(DateTime.Now).Year;
                    if (month <= 0)
                    {
                        month = Utility.ToPersianDateTime(DateTime.Now).Month;
                    }
                }
                else
                {
                    year = DateTime.Now.Year;
                    if (month <= 0)
                    {
                        month = DateTime.Now.Month;
                    }
                }

                if (orderField == GridOrderFields.NONE)
                    orderField = GridOrderFields.gridFields_BarCode;
                if (InitManager())
                {                   
                    IList<UnderManagementPerson> Result = managerRepository.GetUnderManagmentByDepartment(GridSearchFields.NONE, BUser.CurrentUser.Person.ID, departmentID, "", "", month, month > 0 ? 0 : Utility.ToDateRangeIndex(DateTime.Now, sysLanguageResource), DateTime.Now.ToString("yyyy/MM/dd"), orderField, orderType, pageIndex, pageSize);
                    foreach (UnderManagementPerson under in Result)
                    {
                        #region LoadBasicTraffic
                        GTS.Clock.Business.BaseInformation.BTraffic trafficBus = new BaseInformation.BTraffic();
                        IList<BasicTrafficProxy> trafficList = trafficBus.GetDayTraffics(under.PersonId, Utility.ToString(DateTime.Now));
                        int counter = 1;
                        foreach (BasicTrafficProxy trafic in trafficList)
                        {
                            switch (counter)
                            {
                                case 1:
                                    under.FirstEntrance = trafic.TheTime;
                                    break;
                                case 2:
                                    under.FirstExit = trafic.TheTime;
                                    break;
                                case 3:
                                    under.SecondEntrance = trafic.TheTime;
                                    break;
                                case 4:
                                    under.SecondExit = trafic.TheTime;
                                    break;
                                case 5:
                                    under.ThirdEntrance = trafic.TheTime;
                                    break;
                                case 6:
                                    under.ThirdExit = trafic.TheTime;
                                    break;
                                case 7:
                                    under.FourthEntrance = trafic.TheTime;
                                    break;
                                case 8:
                                    under.FourthExit = trafic.TheTime;
                                    break;
                                case 9:
                                    under.FifthEntrance = trafic.TheTime;
                                    break;
                                case 10:
                                    under.FifthExit = trafic.TheTime;
                                    break;

                            }
                            counter++;
                        }
                        under.LastExit = trafficList.LastOrDefault() != null ? trafficList.LastOrDefault().TheTime : "";
                        #endregion

                        under.RemainLeaveToYearEnd = this.GetReainLeaveToEndOfYear(under.PersonId, year, month);
                        under.RemainLeaveToMonthEnd = this.GetReaiLeaveToEndMonth(under.PersonId, year, month);

                        IList<InfoPeriodicScndCnpValue> infoPeriodicScndCnpValueList = NHibernateSessionManager.Instance.GetSession().GetNamedQuery("GetPeriodicScndCnpValueList")
                                                                                                                                     .SetParameter("date", under.Date)
                                                                                                                                     .SetParameter("dateRangeOrderIndex", under.DateRangeOrderIndex)
                                                                                                                                     .SetParameter("dateRangeOrder", under.DateRangeOrder)
                                                                                                                                     .SetParameter("prsId", under.PersonId)
                                                                                                                                     .SetResultTransformer(Transformers.AliasToBean(typeof(InfoPeriodicScndCnpValue)))
                                                                                                                                     .List<InfoPeriodicScndCnpValue>();
                        string defaultValue = "0";

                        InfoPeriodicScndCnpValue ipscv_NecessaryOperation = infoPeriodicScndCnpValueList.Where(x => x.ScndCnpValue_KeyColumnName == "gridFields_NecessaryOperation")
                                                                                                        .FirstOrDefault();
                        under.NecessaryOperation = ipscv_NecessaryOperation != null ? Utility.IntTimeToTime(ipscv_NecessaryOperation == null ? 0 : ipscv_NecessaryOperation.ScndCnpValue_PeriodicValue) : defaultValue;

                        InfoPeriodicScndCnpValue ipscv_HourlyPureOperation = infoPeriodicScndCnpValueList.Where(x => x.ScndCnpValue_KeyColumnName == "gridFields_HourlyPureOperation")
                                                                                                         .FirstOrDefault();
                        under.HourlyPureOperation = ipscv_HourlyPureOperation != null ? Utility.IntTimeToTime(ipscv_HourlyPureOperation == null ? 0 : ipscv_HourlyPureOperation.ScndCnpValue_PeriodicValue) : defaultValue;

                        InfoPeriodicScndCnpValue ipscv_DailyPureOperation = infoPeriodicScndCnpValueList.Where(x => x.ScndCnpValue_KeyColumnName == "gridFields_DailyPureOperation")
                                                                                                        .FirstOrDefault();
                        under.DailyPureOperation = ipscv_DailyPureOperation != null ? Utility.IntTimeToTime(ipscv_DailyPureOperation == null ? 0 : ipscv_DailyPureOperation.ScndCnpValue_PeriodicValue) : defaultValue;

                        InfoPeriodicScndCnpValue ipscv_ImpureOperation = infoPeriodicScndCnpValueList.Where(x => x.ScndCnpValue_KeyColumnName == "gridFields_ImpureOperation")
                                                                                                     .FirstOrDefault();
                        under.ImpureOperation = ipscv_ImpureOperation != null ? Utility.IntTimeToTime(ipscv_ImpureOperation == null ? 0 : ipscv_ImpureOperation.ScndCnpValue_PeriodicValue) : defaultValue;

                        InfoPeriodicScndCnpValue ipscv_AllowableOverTime = infoPeriodicScndCnpValueList.Where(x => x.ScndCnpValue_KeyColumnName == "gridFields_AllowableOverTime")
                                                                                                       .FirstOrDefault();
                        under.AllowableOverTime = ipscv_AllowableOverTime != null ? Utility.IntTimeToTime(ipscv_AllowableOverTime == null ? 0 : ipscv_AllowableOverTime.ScndCnpValue_PeriodicValue) : defaultValue;

                        InfoPeriodicScndCnpValue ipscv_UnallowableOverTime = infoPeriodicScndCnpValueList.Where(x => x.ScndCnpValue_KeyColumnName == "gridFields_UnallowableOverTime")
                                                                                                         .FirstOrDefault();
                        under.UnallowableOverTime = ipscv_UnallowableOverTime != null ? Utility.IntTimeToTime(ipscv_UnallowableOverTime == null ? 0 : ipscv_UnallowableOverTime.ScndCnpValue_PeriodicValue) : defaultValue;

                        InfoPeriodicScndCnpValue ipscv_HourlyAllowableAbsence = infoPeriodicScndCnpValueList.Where(x => x.ScndCnpValue_KeyColumnName == "gridFields_HourlyAllowableAbsence")
                                                                                                            .FirstOrDefault();
                        under.HourlyAllowableAbsence = ipscv_HourlyAllowableAbsence != null ? Utility.IntTimeToTime(ipscv_HourlyAllowableAbsence == null ? 0 : ipscv_HourlyAllowableAbsence.ScndCnpValue_PeriodicValue) : defaultValue;

                        InfoPeriodicScndCnpValue ipscv_HourlyUnallowableAbsence = infoPeriodicScndCnpValueList.Where(x => x.ScndCnpValue_KeyColumnName == "gridFields_HourlyUnallowableAbsence")
                                                                                                              .FirstOrDefault();
                        under.HourlyUnallowableAbsence = ipscv_HourlyUnallowableAbsence != null ? Utility.IntTimeToTime(ipscv_HourlyUnallowableAbsence == null ? 0 : ipscv_HourlyUnallowableAbsence.ScndCnpValue_PeriodicValue) : defaultValue;

                        InfoPeriodicScndCnpValue ipscv_DailyAbsence = infoPeriodicScndCnpValueList.Where(x => x.ScndCnpValue_KeyColumnName == "gridFields_DailyAbsence")
                                                                                                  .FirstOrDefault();
                        //under.DailyAbsence = ipscv_DailyAbsence != null ? Utility.IntTimeToTime(ipscv_DailyAbsence == null ? 0 : ipscv_DailyAbsence.ScndCnpValue_PeriodicValue) : defaultValue;
                        under.DailyAbsence = ipscv_DailyAbsence != null ? ipscv_DailyAbsence == null ? "0" : ipscv_DailyAbsence.ScndCnpValue_PeriodicValue.ToString() : defaultValue;
                        InfoPeriodicScndCnpValue ipscv_HourlyMission = infoPeriodicScndCnpValueList.Where(x => x.ScndCnpValue_KeyColumnName == "gridFields_HourlyMission")
                                                                                                   .FirstOrDefault();
                        under.HourlyMission = ipscv_HourlyMission != null ? Utility.IntTimeToTime(ipscv_HourlyMission == null ? 0 : ipscv_HourlyMission.ScndCnpValue_PeriodicValue) : defaultValue;

                        InfoPeriodicScndCnpValue ipscv_DailyMission = infoPeriodicScndCnpValueList.Where(x => x.ScndCnpValue_KeyColumnName == "gridFields_DailyMission")
                                                                                                  .FirstOrDefault();
                        //under.DailyMission = ipscv_DailyMission != null ? Utility.IntTimeToTime(ipscv_DailyMission == null ? 0 : ipscv_DailyMission.ScndCnpValue_PeriodicValue) : defaultValue;
                        under.DailyMission = ipscv_DailyMission != null ? ipscv_DailyMission == null ? "0" : ipscv_DailyMission.ScndCnpValue_PeriodicValue.ToString() : defaultValue;
                        InfoPeriodicScndCnpValue ipscv_HostelryMission = infoPeriodicScndCnpValueList.Where(x => x.ScndCnpValue_KeyColumnName == "gridFields_HostelryMission")
                                                                                                     .FirstOrDefault();
                        //under.HostelryMission = ipscv_HostelryMission != null ? Utility.IntTimeToTime(ipscv_HostelryMission == null ? 0 : ipscv_HostelryMission.ScndCnpValue_PeriodicValue) : defaultValue;
                        under.HostelryMission = ipscv_HostelryMission != null ? ipscv_HostelryMission == null ? "0" : ipscv_HostelryMission.ScndCnpValue_PeriodicValue.ToString() : defaultValue;
                        InfoPeriodicScndCnpValue ipscv_HourlySickLeave = infoPeriodicScndCnpValueList.Where(x => x.ScndCnpValue_KeyColumnName == "gridFields_HourlySickLeave")
                                                                                                     .FirstOrDefault();
                        under.HourlySickLeave = ipscv_HourlySickLeave != null ? Utility.IntTimeToTime(ipscv_HourlySickLeave == null ? 0 : ipscv_HourlySickLeave.ScndCnpValue_PeriodicValue) : defaultValue;

                        InfoPeriodicScndCnpValue ipscv_DailySickLeave = infoPeriodicScndCnpValueList.Where(x => x.ScndCnpValue_KeyColumnName == "gridFields_DailySickLeave")
                                                                                                    .FirstOrDefault();
                        //under.DailySickLeave = ipscv_DailySickLeave != null ? Utility.IntTimeToTime(ipscv_DailySickLeave == null ? 0 : ipscv_DailySickLeave.ScndCnpValue_PeriodicValue) : defaultValue;
                        under.DailySickLeave = ipscv_DailySickLeave != null ? ipscv_DailySickLeave == null ? "0" : ipscv_DailySickLeave.ScndCnpValue_PeriodicValue.ToString() : defaultValue;
                        InfoPeriodicScndCnpValue ipscv_HourlyMeritoriouslyLeave = infoPeriodicScndCnpValueList.Where(x => x.ScndCnpValue_KeyColumnName == "gridFields_HourlyMeritoriouslyLeave")
                                                                                                              .FirstOrDefault();
                        under.HourlyMeritoriouslyLeave = ipscv_HourlyMeritoriouslyLeave != null ? Utility.IntTimeToTime(ipscv_HourlyMeritoriouslyLeave == null ? 0 : ipscv_HourlyMeritoriouslyLeave.ScndCnpValue_PeriodicValue) : defaultValue;

                        InfoPeriodicScndCnpValue ipscv_DailyMeritoriouslyLeave = infoPeriodicScndCnpValueList.Where(x => x.ScndCnpValue_KeyColumnName == "gridFields_DailyMeritoriouslyLeave")
                                                                                                             .FirstOrDefault();
                        //under.DailyMeritoriouslyLeave = ipscv_DailyMeritoriouslyLeave != null ? Utility.IntTimeToTime(ipscv_DailyMeritoriouslyLeave == null ? 0 : ipscv_DailyMeritoriouslyLeave.ScndCnpValue_PeriodicValue) : defaultValue;
                        under.DailyMeritoriouslyLeave = ipscv_DailyMeritoriouslyLeave != null ? ipscv_DailyMeritoriouslyLeave == null ? "0" : ipscv_DailyMeritoriouslyLeave.ScndCnpValue_PeriodicValue.ToString() : defaultValue;
                        InfoPeriodicScndCnpValue ipscv_HourlyWithoutPayLeave = infoPeriodicScndCnpValueList.Where(x => x.ScndCnpValue_KeyColumnName == "gridFields_HourlyWithoutPayLeave")
                                                                                                           .FirstOrDefault();
                        under.HourlyWithoutPayLeave = ipscv_HourlyWithoutPayLeave != null ? Utility.IntTimeToTime(ipscv_HourlyWithoutPayLeave == null ? 0 : ipscv_HourlyWithoutPayLeave.ScndCnpValue_PeriodicValue) : defaultValue;

                        InfoPeriodicScndCnpValue ipscv_PresenceDuration = infoPeriodicScndCnpValueList.Where(x => x.ScndCnpValue_KeyColumnName == "gridFields_PresenceDuration")
                                                                                                      .FirstOrDefault();
                        under.PresenceDuration = ipscv_PresenceDuration != null ? Utility.IntTimeToTime(ipscv_PresenceDuration == null ? 0 : ipscv_PresenceDuration.ScndCnpValue_PeriodicValue) : defaultValue;

                        InfoPeriodicScndCnpValue ipscv_DailyWithoutPayLeave = infoPeriodicScndCnpValueList.Where(x => x.ScndCnpValue_KeyColumnName == "gridFields_DailyWithoutPayLeave")
                                                                                                          .FirstOrDefault();
                        //under.DailyWithoutPayLeave = ipscv_DailyWithoutPayLeave != null ? Utility.IntTimeToTime(ipscv_DailyWithoutPayLeave == null ? 0 : ipscv_DailyWithoutPayLeave.ScndCnpValue_PeriodicValue) : defaultValue;
                        under.DailyWithoutPayLeave = ipscv_DailyWithoutPayLeave != null ? ipscv_DailyWithoutPayLeave == null ? "0" : ipscv_DailyWithoutPayLeave.ScndCnpValue_PeriodicValue.ToString() : defaultValue;
                        InfoPeriodicScndCnpValue ipscv_HourlyWithPayLeave = infoPeriodicScndCnpValueList.Where(x => x.ScndCnpValue_KeyColumnName == "gridFields_HourlyWithPayLeave")
                                                                                                        .FirstOrDefault();
                        under.HourlyWithPayLeave = ipscv_HourlyWithPayLeave != null ? Utility.IntTimeToTime(ipscv_HourlyWithPayLeave == null ? 0 : ipscv_HourlyWithPayLeave.ScndCnpValue_PeriodicValue) : defaultValue;

                        InfoPeriodicScndCnpValue ipscv_DailyWithPayLeave = infoPeriodicScndCnpValueList.Where(x => x.ScndCnpValue_KeyColumnName == "gridFields_DailyWithPayLeave")
                                                                                                       .FirstOrDefault();
                        //under.DailyWithPayLeave = ipscv_DailyWithPayLeave != null ? Utility.IntTimeToTime(ipscv_DailyWithPayLeave == null ? 0 : ipscv_DailyWithPayLeave.ScndCnpValue_PeriodicValue) : defaultValue;
                        under.DailyWithPayLeave = ipscv_DailyWithPayLeave != null ? ipscv_DailyWithPayLeave == null ? "0" : ipscv_DailyWithPayLeave.ScndCnpValue_PeriodicValue.ToString() : defaultValue;
                        InfoPeriodicScndCnpValue ipscv_ReserveField1 = infoPeriodicScndCnpValueList.Where(x => x.ScndCnpValue_KeyColumnName == "gridFields_ReserveField1")
                                                                                                   .FirstOrDefault();
                        under.ReserveField1 = ipscv_ReserveField1 != null ? Utility.IntTimeToTime(ipscv_ReserveField1 == null ? 0 : ipscv_ReserveField1.ScndCnpValue_PeriodicValue) : defaultValue;

                        InfoPeriodicScndCnpValue ipscv_ReserveField2 = infoPeriodicScndCnpValueList.Where(x => x.ScndCnpValue_KeyColumnName == "gridFields_ReserveField2")
                                                                                                   .FirstOrDefault();
                        under.ReserveField2 = ipscv_ReserveField2 != null ? Utility.IntTimeToTime(ipscv_ReserveField2 == null ? 0 : ipscv_ReserveField2.ScndCnpValue_PeriodicValue) : defaultValue;

                        InfoPeriodicScndCnpValue ipscv_ReserveField3 = infoPeriodicScndCnpValueList.Where(x => x.ScndCnpValue_KeyColumnName == "gridFields_ReserveField3")
                                                                                                   .FirstOrDefault();
                        under.ReserveField3 = ipscv_ReserveField3 != null ? Utility.IntTimeToTime(ipscv_ReserveField3 == null ? 0 : ipscv_ReserveField3.ScndCnpValue_PeriodicValue) : defaultValue;

                        InfoPeriodicScndCnpValue ipscv_ReserveField4 = infoPeriodicScndCnpValueList.Where(x => x.ScndCnpValue_KeyColumnName == "gridFields_ReserveField4")
                                                                                                   .FirstOrDefault();
                        under.ReserveField4 = ipscv_ReserveField4 != null ? Utility.IntTimeToTime(ipscv_ReserveField4 == null ? 0 : ipscv_ReserveField4.ScndCnpValue_PeriodicValue) : defaultValue;

                        InfoPeriodicScndCnpValue ipscv_ReserveField5 = infoPeriodicScndCnpValueList.Where(x => x.ScndCnpValue_KeyColumnName == "gridFields_ReserveField5")
                                                                                                   .FirstOrDefault();
                        under.ReserveField5 = ipscv_ReserveField5 != null ? Utility.IntTimeToTime(ipscv_ReserveField5 == null ? 0 : ipscv_ReserveField5.ScndCnpValue_PeriodicValue) : defaultValue;

                        InfoPeriodicScndCnpValue ipscv_ReserveField6 = infoPeriodicScndCnpValueList.Where(x => x.ScndCnpValue_KeyColumnName == "gridFields_ReserveField6")
                                                                                                   .FirstOrDefault();
                        under.ReserveField6 = ipscv_ReserveField6 != null ? Utility.IntTimeToTime(ipscv_ReserveField6 == null ? 0 : ipscv_ReserveField6.ScndCnpValue_PeriodicValue) : defaultValue;

                        InfoPeriodicScndCnpValue ipscv_ReserveField7 = infoPeriodicScndCnpValueList.Where(x => x.ScndCnpValue_KeyColumnName == "gridFields_ReserveField7")
                                                                                                   .FirstOrDefault();
                        under.ReserveField7 = ipscv_ReserveField7 != null ? Utility.IntTimeToTime(ipscv_ReserveField7 == null ? 0 : ipscv_ReserveField7.ScndCnpValue_PeriodicValue) : defaultValue;

                        InfoPeriodicScndCnpValue ipscv_ReserveField8 = infoPeriodicScndCnpValueList.Where(x => x.ScndCnpValue_KeyColumnName == "gridFields_ReserveField8")
                                                                                                   .FirstOrDefault();
                        under.ReserveField8 = ipscv_ReserveField8 != null ? Utility.IntTimeToTime(ipscv_ReserveField8 == null ? 0 : ipscv_ReserveField8.ScndCnpValue_PeriodicValue) : defaultValue;

                        InfoPeriodicScndCnpValue ipscv_ReserveField9 = infoPeriodicScndCnpValueList.Where(x => x.ScndCnpValue_KeyColumnName == "gridFields_ReserveField9")
                                                                                                   .FirstOrDefault();
                        under.ReserveField9 = ipscv_ReserveField9 != null ? Utility.IntTimeToTime(ipscv_ReserveField9 == null ? 0 : ipscv_ReserveField9.ScndCnpValue_PeriodicValue) : defaultValue;

                        InfoPeriodicScndCnpValue ipscv_ReserveField10 = infoPeriodicScndCnpValueList.Where(x => x.ScndCnpValue_KeyColumnName == "gridFields_ReserveField10")
                                                                                                    .FirstOrDefault();
                        under.ReserveField10 = ipscv_ReserveField10 != null ? Utility.IntTimeToTime(ipscv_ReserveField10 == null ? 0 : ipscv_ReserveField10.ScndCnpValue_PeriodicValue) : defaultValue;


                    }
                    return Result;
                }
                else if (InitOperator())
                {                    
                    IList<UnderManagementPerson> Result = managerRepository.GetUnderManagmentOperatorByDepartment(GridSearchFields.NONE, BUser.CurrentUser.Person.ID, departmentID, "", "", month, month > 0 ? 0 : Utility.ToDateRangeIndex(DateTime.Now, sysLanguageResource), DateTime.Now.ToString("yyyy/MM/dd"), orderField, orderType, pageIndex, pageSize);
                    foreach (UnderManagementPerson under in Result)
                    {
                        #region LoadBasicTraffic
                        GTS.Clock.Business.BaseInformation.BTraffic trafficBus = new BaseInformation.BTraffic();
                        IList<BasicTrafficProxy> trafficList = trafficBus.GetDayTraffics(under.PersonId, Utility.ToString(DateTime.Now));
                        int counter = 1;
                        foreach (BasicTrafficProxy trafic in trafficList)
                        {
                            switch (counter)
                            {
                                case 1:
                                    under.FirstEntrance = trafic.TheTime;
                                    break;
                                case 2:
                                    under.FirstExit = trafic.TheTime;
                                    break;
                                case 3:
                                    under.SecondEntrance = trafic.TheTime;
                                    break;
                                case 4:
                                    under.SecondExit = trafic.TheTime;
                                    break;
                                case 5:
                                    under.ThirdEntrance = trafic.TheTime;
                                    break;
                                case 6:
                                    under.ThirdExit = trafic.TheTime;
                                    break;
                                case 7:
                                    under.FourthEntrance = trafic.TheTime;
                                    break;
                                case 8:
                                    under.FourthExit = trafic.TheTime;
                                    break;
                                case 9:
                                    under.FifthEntrance = trafic.TheTime;
                                    break;
                                case 10:
                                    under.FifthExit = trafic.TheTime;
                                    break;

                            }
                            counter++;
                        }
                        under.LastExit = trafficList.LastOrDefault() != null ? trafficList.LastOrDefault().TheTime : "";
                        #endregion

                        under.RemainLeaveToYearEnd = this.GetReainLeaveToEndOfYear(under.PersonId, year, month);
                        under.RemainLeaveToMonthEnd = this.GetReaiLeaveToEndMonth(under.PersonId, year, month);
                    }
                    return Result;
                }
                else
                {
                    throw new IllegalServiceAccess(String.Format("این سرویس تنها توسط مدیران قابل استفاده میباشد. شناسه کاربری {0} میباشد", this.Username), ExceptionSrc);
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BWorkedTime", "GetUnderManagmentByDepartment");
                throw ex;
            }
        }

        /// <summary>
        /// با دریافت یک گره از درخت تحت مدیریت مدیر اشخاص تحت مدیریت را برمیگرداند
        /// این افراد مرتب شده بر اساس فیلد مشخص شده هستند 
        /// فقط مدیران بررسی میشود و به جانشین نگاه نمیکند
        /// فقط جریان اصلی واکشی میشود
        /// </summary>
        /// <param name="month">ماه</param>
        /// <param name="departmentID">کلید اصلی گره چارت سازمانی</param>
        /// <param name="pageIndex">شماره صفحه</param>
        /// <param name="pageSize">تعداد رکوردهای صفحه</param>
        /// <param name="orderField">فیلد مرتب سازی</param>
        /// <param name="orderType">نوع مرتب سازی</param>
        /// <returns>لیست پرسنل تحت مدیریت</returns>
        public IList<UnderManagementPerson> GetJustUnderManagmentMainFlowByDepartment(int month, decimal departmentID,decimal managerPersonId, int pageIndex, int pageSize, GridOrderFields orderField, GridOrderFieldType orderType)
        {
            try
            {
                int year = 0;
                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    year = Utility.ToPersianDateTime(DateTime.Now).Year;
                    if (month <= 0)
                    {
                        month = Utility.ToPersianDateTime(DateTime.Now).Month;
                    }
                }
                else
                {
                    year = DateTime.Now.Year;
                    if (month <= 0)
                    {
                        month = DateTime.Now.Month;
                    }
                }

                if (orderField == GridOrderFields.NONE)
                    orderField = GridOrderFields.gridFields_BarCode;
                
                if (IsManager(managerPersonId))
                {
                    IList<UnderManagementPerson> Result = managerRepository.GetUnderManagmentByDepartment_JustMainManagers(GridSearchFields.NONE, managerPersonId, departmentID, "", "", month, month > 0 ? 0 : Utility.ToDateRangeIndex(DateTime.Now, sysLanguageResource), DateTime.Now.ToString("yyyy/MM/dd"), orderField, orderType, pageIndex, pageSize);
                    foreach (UnderManagementPerson under in Result)
                    {                                     

                        IList<InfoPeriodicScndCnpValue> infoPeriodicScndCnpValueList = NHibernateSessionManager.Instance.GetSession().GetNamedQuery("GetPeriodicScndCnpValueList")
                                                                                                                                     .SetParameter("date", under.Date)
                                                                                                                                     .SetParameter("dateRangeOrderIndex", under.DateRangeOrderIndex)
                                                                                                                                     .SetParameter("dateRangeOrder", under.DateRangeOrder)
                                                                                                                                     .SetParameter("prsId", under.PersonId)
                                                                                                                                     .SetResultTransformer(Transformers.AliasToBean(typeof(InfoPeriodicScndCnpValue)))
                                                                                                                                     .List<InfoPeriodicScndCnpValue>();
                        string defaultValue = "0";

                        InfoPeriodicScndCnpValue ipscv_NecessaryOperation = infoPeriodicScndCnpValueList.Where(x => x.ScndCnpValue_KeyColumnName == "gridFields_NecessaryOperation")
                                                                                                        .FirstOrDefault();
                        under.NecessaryOperation = ipscv_NecessaryOperation != null ? Utility.IntTimeToTime(ipscv_NecessaryOperation == null ? 0 : ipscv_NecessaryOperation.ScndCnpValue_PeriodicValue) : defaultValue;

                        InfoPeriodicScndCnpValue ipscv_HourlyPureOperation = infoPeriodicScndCnpValueList.Where(x => x.ScndCnpValue_KeyColumnName == "gridFields_HourlyPureOperation")
                                                                                                         .FirstOrDefault();
                        under.HourlyPureOperation = ipscv_HourlyPureOperation != null ? Utility.IntTimeToTime(ipscv_HourlyPureOperation == null ? 0 : ipscv_HourlyPureOperation.ScndCnpValue_PeriodicValue) : defaultValue;

                        InfoPeriodicScndCnpValue ipscv_DailyPureOperation = infoPeriodicScndCnpValueList.Where(x => x.ScndCnpValue_KeyColumnName == "gridFields_DailyPureOperation")
                                                                                                        .FirstOrDefault();
                        under.DailyPureOperation = ipscv_DailyPureOperation != null ? ipscv_DailyPureOperation == null ? "0" : ipscv_DailyPureOperation.ScndCnpValue_PeriodicValue.ToString() : defaultValue;
                        
                        InfoPeriodicScndCnpValue ipscv_ImpureOperation = infoPeriodicScndCnpValueList.Where(x => x.ScndCnpValue_KeyColumnName == "gridFields_ImpureOperation")
                                                                                                     .FirstOrDefault();
                        under.ImpureOperation = ipscv_ImpureOperation != null ? Utility.IntTimeToTime(ipscv_ImpureOperation == null ? 0 : ipscv_ImpureOperation.ScndCnpValue_PeriodicValue) : defaultValue;

                        InfoPeriodicScndCnpValue ipscv_AllowableOverTime = infoPeriodicScndCnpValueList.Where(x => x.ScndCnpValue_KeyColumnName == "gridFields_AllowableOverTime")
                                                                                                       .FirstOrDefault();
                        under.AllowableOverTime = ipscv_AllowableOverTime != null ? Utility.IntTimeToTime(ipscv_AllowableOverTime == null ? 0 : ipscv_AllowableOverTime.ScndCnpValue_PeriodicValue) : defaultValue;

                        InfoPeriodicScndCnpValue ipscv_UnallowableOverTime = infoPeriodicScndCnpValueList.Where(x => x.ScndCnpValue_KeyColumnName == "gridFields_UnallowableOverTime")
                                                                                                         .FirstOrDefault();
                        under.UnallowableOverTime = ipscv_UnallowableOverTime != null ? Utility.IntTimeToTime(ipscv_UnallowableOverTime == null ? 0 : ipscv_UnallowableOverTime.ScndCnpValue_PeriodicValue) : defaultValue;

                        InfoPeriodicScndCnpValue ipscv_HourlyAllowableAbsence = infoPeriodicScndCnpValueList.Where(x => x.ScndCnpValue_KeyColumnName == "gridFields_HourlyAllowableAbsence")
                                                                                                            .FirstOrDefault();
                        under.HourlyAllowableAbsence = ipscv_HourlyAllowableAbsence != null ? Utility.IntTimeToTime(ipscv_HourlyAllowableAbsence == null ? 0 : ipscv_HourlyAllowableAbsence.ScndCnpValue_PeriodicValue) : defaultValue;

                        InfoPeriodicScndCnpValue ipscv_HourlyUnallowableAbsence = infoPeriodicScndCnpValueList.Where(x => x.ScndCnpValue_KeyColumnName == "gridFields_HourlyUnallowableAbsence")
                                                                                                              .FirstOrDefault();
                        under.HourlyUnallowableAbsence = ipscv_HourlyUnallowableAbsence != null ? Utility.IntTimeToTime(ipscv_HourlyUnallowableAbsence == null ? 0 : ipscv_HourlyUnallowableAbsence.ScndCnpValue_PeriodicValue) : defaultValue;

                        InfoPeriodicScndCnpValue ipscv_DailyAbsence = infoPeriodicScndCnpValueList.Where(x => x.ScndCnpValue_KeyColumnName == "gridFields_DailyAbsence")
                                                                                                  .FirstOrDefault();
                        //under.DailyAbsence = ipscv_DailyAbsence != null ? Utility.IntTimeToTime(ipscv_DailyAbsence == null ? 0 : ipscv_DailyAbsence.ScndCnpValue_PeriodicValue) : defaultValue;
                        under.DailyAbsence = ipscv_DailyAbsence != null ? ipscv_DailyAbsence == null ? "0" : ipscv_DailyAbsence.ScndCnpValue_PeriodicValue.ToString() : defaultValue;
                        InfoPeriodicScndCnpValue ipscv_HourlyMission = infoPeriodicScndCnpValueList.Where(x => x.ScndCnpValue_KeyColumnName == "gridFields_HourlyMission")
                                                                                                   .FirstOrDefault();
                        under.HourlyMission = ipscv_HourlyMission != null ? Utility.IntTimeToTime(ipscv_HourlyMission == null ? 0 : ipscv_HourlyMission.ScndCnpValue_PeriodicValue) : defaultValue;

                        InfoPeriodicScndCnpValue ipscv_DailyMission = infoPeriodicScndCnpValueList.Where(x => x.ScndCnpValue_KeyColumnName == "gridFields_DailyMission")
                                                                                                  .FirstOrDefault();
                        //under.DailyMission = ipscv_DailyMission != null ? Utility.IntTimeToTime(ipscv_DailyMission == null ? 0 : ipscv_DailyMission.ScndCnpValue_PeriodicValue) : defaultValue;
                        under.DailyMission = ipscv_DailyMission != null ? ipscv_DailyMission == null ? "0" : ipscv_DailyMission.ScndCnpValue_PeriodicValue.ToString() : defaultValue;
                        InfoPeriodicScndCnpValue ipscv_HostelryMission = infoPeriodicScndCnpValueList.Where(x => x.ScndCnpValue_KeyColumnName == "gridFields_HostelryMission")
                                                                                                     .FirstOrDefault();
                        //under.HostelryMission = ipscv_HostelryMission != null ? Utility.IntTimeToTime(ipscv_HostelryMission == null ? 0 : ipscv_HostelryMission.ScndCnpValue_PeriodicValue) : defaultValue;
                        under.HostelryMission = ipscv_HostelryMission != null ? ipscv_HostelryMission == null ? "0" : ipscv_HostelryMission.ScndCnpValue_PeriodicValue.ToString() : defaultValue;
                        InfoPeriodicScndCnpValue ipscv_HourlySickLeave = infoPeriodicScndCnpValueList.Where(x => x.ScndCnpValue_KeyColumnName == "gridFields_HourlySickLeave")
                                                                                                     .FirstOrDefault();
                        under.HourlySickLeave = ipscv_HourlySickLeave != null ? Utility.IntTimeToTime(ipscv_HourlySickLeave == null ? 0 : ipscv_HourlySickLeave.ScndCnpValue_PeriodicValue) : defaultValue;

                        InfoPeriodicScndCnpValue ipscv_DailySickLeave = infoPeriodicScndCnpValueList.Where(x => x.ScndCnpValue_KeyColumnName == "gridFields_DailySickLeave")
                                                                                                    .FirstOrDefault();
                        //under.DailySickLeave = ipscv_DailySickLeave != null ? Utility.IntTimeToTime(ipscv_DailySickLeave == null ? 0 : ipscv_DailySickLeave.ScndCnpValue_PeriodicValue) : defaultValue;
                        under.DailySickLeave = ipscv_DailySickLeave != null ? ipscv_DailySickLeave == null ? "0" : ipscv_DailySickLeave.ScndCnpValue_PeriodicValue.ToString() : defaultValue;
                        InfoPeriodicScndCnpValue ipscv_HourlyMeritoriouslyLeave = infoPeriodicScndCnpValueList.Where(x => x.ScndCnpValue_KeyColumnName == "gridFields_HourlyMeritoriouslyLeave")
                                                                                                              .FirstOrDefault();
                        under.HourlyMeritoriouslyLeave = ipscv_HourlyMeritoriouslyLeave != null ? Utility.IntTimeToTime(ipscv_HourlyMeritoriouslyLeave == null ? 0 : ipscv_HourlyMeritoriouslyLeave.ScndCnpValue_PeriodicValue) : defaultValue;

                        InfoPeriodicScndCnpValue ipscv_DailyMeritoriouslyLeave = infoPeriodicScndCnpValueList.Where(x => x.ScndCnpValue_KeyColumnName == "gridFields_DailyMeritoriouslyLeave")
                                                                                                             .FirstOrDefault();
                        //under.DailyMeritoriouslyLeave = ipscv_DailyMeritoriouslyLeave != null ? Utility.IntTimeToTime(ipscv_DailyMeritoriouslyLeave == null ? 0 : ipscv_DailyMeritoriouslyLeave.ScndCnpValue_PeriodicValue) : defaultValue;
                        under.DailyMeritoriouslyLeave = ipscv_DailyMeritoriouslyLeave != null ? ipscv_DailyMeritoriouslyLeave == null ? "0" : ipscv_DailyMeritoriouslyLeave.ScndCnpValue_PeriodicValue.ToString() : defaultValue;
                        InfoPeriodicScndCnpValue ipscv_HourlyWithoutPayLeave = infoPeriodicScndCnpValueList.Where(x => x.ScndCnpValue_KeyColumnName == "gridFields_HourlyWithoutPayLeave")
                                                                                                           .FirstOrDefault();
                        under.HourlyWithoutPayLeave = ipscv_HourlyWithoutPayLeave != null ? Utility.IntTimeToTime(ipscv_HourlyWithoutPayLeave == null ? 0 : ipscv_HourlyWithoutPayLeave.ScndCnpValue_PeriodicValue) : defaultValue;

                        InfoPeriodicScndCnpValue ipscv_PresenceDuration = infoPeriodicScndCnpValueList.Where(x => x.ScndCnpValue_KeyColumnName == "gridFields_PresenceDuration")
                                                                                                      .FirstOrDefault();
                        under.PresenceDuration = ipscv_PresenceDuration != null ? Utility.IntTimeToTime(ipscv_PresenceDuration == null ? 0 : ipscv_PresenceDuration.ScndCnpValue_PeriodicValue) : defaultValue;

                        InfoPeriodicScndCnpValue ipscv_DailyWithoutPayLeave = infoPeriodicScndCnpValueList.Where(x => x.ScndCnpValue_KeyColumnName == "gridFields_DailyWithoutPayLeave")
                                                                                                          .FirstOrDefault();
                        //under.DailyWithoutPayLeave = ipscv_DailyWithoutPayLeave != null ? Utility.IntTimeToTime(ipscv_DailyWithoutPayLeave == null ? 0 : ipscv_DailyWithoutPayLeave.ScndCnpValue_PeriodicValue) : defaultValue;
                        under.DailyWithoutPayLeave = ipscv_DailyWithoutPayLeave != null ? ipscv_DailyWithoutPayLeave == null ? "0" : ipscv_DailyWithoutPayLeave.ScndCnpValue_PeriodicValue.ToString() : defaultValue;
                        InfoPeriodicScndCnpValue ipscv_HourlyWithPayLeave = infoPeriodicScndCnpValueList.Where(x => x.ScndCnpValue_KeyColumnName == "gridFields_HourlyWithPayLeave")
                                                                                                        .FirstOrDefault();
                        under.HourlyWithPayLeave = ipscv_HourlyWithPayLeave != null ? Utility.IntTimeToTime(ipscv_HourlyWithPayLeave == null ? 0 : ipscv_HourlyWithPayLeave.ScndCnpValue_PeriodicValue) : defaultValue;

                        InfoPeriodicScndCnpValue ipscv_DailyWithPayLeave = infoPeriodicScndCnpValueList.Where(x => x.ScndCnpValue_KeyColumnName == "gridFields_DailyWithPayLeave")
                                                                                                       .FirstOrDefault();
                        //under.DailyWithPayLeave = ipscv_DailyWithPayLeave != null ? Utility.IntTimeToTime(ipscv_DailyWithPayLeave == null ? 0 : ipscv_DailyWithPayLeave.ScndCnpValue_PeriodicValue) : defaultValue;
                        under.DailyWithPayLeave = ipscv_DailyWithPayLeave != null ? ipscv_DailyWithPayLeave == null ? "0" : ipscv_DailyWithPayLeave.ScndCnpValue_PeriodicValue.ToString() : defaultValue;
                        InfoPeriodicScndCnpValue ipscv_ReserveField1 = infoPeriodicScndCnpValueList.Where(x => x.ScndCnpValue_KeyColumnName == "gridFields_ReserveField1")
                                                                                                   .FirstOrDefault();
                        under.ReserveField1 = ipscv_ReserveField1 != null ? Utility.IntTimeToTime(ipscv_ReserveField1 == null ? 0 : ipscv_ReserveField1.ScndCnpValue_PeriodicValue) : defaultValue;

                        InfoPeriodicScndCnpValue ipscv_ReserveField2 = infoPeriodicScndCnpValueList.Where(x => x.ScndCnpValue_KeyColumnName == "gridFields_ReserveField2")
                                                                                                   .FirstOrDefault();
                        under.ReserveField2 = ipscv_ReserveField2 != null ? Utility.IntTimeToTime(ipscv_ReserveField2 == null ? 0 : ipscv_ReserveField2.ScndCnpValue_PeriodicValue) : defaultValue;

                        InfoPeriodicScndCnpValue ipscv_ReserveField3 = infoPeriodicScndCnpValueList.Where(x => x.ScndCnpValue_KeyColumnName == "gridFields_ReserveField3")
                                                                                                   .FirstOrDefault();
                        under.ReserveField3 = ipscv_ReserveField3 != null ? Utility.IntTimeToTime(ipscv_ReserveField3 == null ? 0 : ipscv_ReserveField3.ScndCnpValue_PeriodicValue) : defaultValue;

                        InfoPeriodicScndCnpValue ipscv_ReserveField4 = infoPeriodicScndCnpValueList.Where(x => x.ScndCnpValue_KeyColumnName == "gridFields_ReserveField4")
                                                                                                   .FirstOrDefault();
                        under.ReserveField4 = ipscv_ReserveField4 != null ? Utility.IntTimeToTime(ipscv_ReserveField4 == null ? 0 : ipscv_ReserveField4.ScndCnpValue_PeriodicValue) : defaultValue;

                        InfoPeriodicScndCnpValue ipscv_ReserveField5 = infoPeriodicScndCnpValueList.Where(x => x.ScndCnpValue_KeyColumnName == "gridFields_ReserveField5")
                                                                                                   .FirstOrDefault();
                        under.ReserveField5 = ipscv_ReserveField5 != null ? Utility.IntTimeToTime(ipscv_ReserveField5 == null ? 0 : ipscv_ReserveField5.ScndCnpValue_PeriodicValue) : defaultValue;

                        InfoPeriodicScndCnpValue ipscv_ReserveField6 = infoPeriodicScndCnpValueList.Where(x => x.ScndCnpValue_KeyColumnName == "gridFields_ReserveField6")
                                                                                                   .FirstOrDefault();
                        under.ReserveField6 = ipscv_ReserveField6 != null ? Utility.IntTimeToTime(ipscv_ReserveField6 == null ? 0 : ipscv_ReserveField6.ScndCnpValue_PeriodicValue) : defaultValue;

                        InfoPeriodicScndCnpValue ipscv_ReserveField7 = infoPeriodicScndCnpValueList.Where(x => x.ScndCnpValue_KeyColumnName == "gridFields_ReserveField7")
                                                                                                   .FirstOrDefault();
                        under.ReserveField7 = ipscv_ReserveField7 != null ? Utility.IntTimeToTime(ipscv_ReserveField7 == null ? 0 : ipscv_ReserveField7.ScndCnpValue_PeriodicValue) : defaultValue;

                        InfoPeriodicScndCnpValue ipscv_ReserveField8 = infoPeriodicScndCnpValueList.Where(x => x.ScndCnpValue_KeyColumnName == "gridFields_ReserveField8")
                                                                                                   .FirstOrDefault();
                        under.ReserveField8 = ipscv_ReserveField8 != null ? Utility.IntTimeToTime(ipscv_ReserveField8 == null ? 0 : ipscv_ReserveField8.ScndCnpValue_PeriodicValue) : defaultValue;

                        InfoPeriodicScndCnpValue ipscv_ReserveField9 = infoPeriodicScndCnpValueList.Where(x => x.ScndCnpValue_KeyColumnName == "gridFields_ReserveField9")
                                                                                                   .FirstOrDefault();
                        under.ReserveField9 = ipscv_ReserveField9 != null ? Utility.IntTimeToTime(ipscv_ReserveField9 == null ? 0 : ipscv_ReserveField9.ScndCnpValue_PeriodicValue) : defaultValue;

                        InfoPeriodicScndCnpValue ipscv_ReserveField10 = infoPeriodicScndCnpValueList.Where(x => x.ScndCnpValue_KeyColumnName == "gridFields_ReserveField10")
                                                                                                    .FirstOrDefault();
                        under.ReserveField10 = ipscv_ReserveField10 != null ? Utility.IntTimeToTime(ipscv_ReserveField10 == null ? 0 : ipscv_ReserveField10.ScndCnpValue_PeriodicValue) : defaultValue;


                    }
                    return Result;
                }               
                else
                {
                    throw new IllegalServiceAccess(String.Format("این سرویس تنها توسط مدیران قابل استفاده میباشد. شناسه کاربری {0} میباشد", this.Username), ExceptionSrc);
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BWorkedTime", "GetUnderManagmentByDepartment");
                throw ex;
            }
        }


        /// <summary>
        /// تعداد افراد تحت مدیریت را براساس کلمه جستجوشده برمیگرداند 
        /// </summary>
        /// <param name="month">ماه</param>
        /// <param name="searchKey">عبارت جستجو</param>
        /// <param name="searchType">فیلد جستجو شونده</param>
        /// <returns>تعداد افراد تحت مدیریت</returns>
        public int GetUnderManagmentBySearchCount(int month, string searchKey, GridSearchFields searchType)
        {
            try
            {
                if (InitManager())
                {
                    int Result = 0;
                    if (searchType == GridSearchFields.PersonName)
                    {
                        Result = managerRepository.GetUnderManagmentByDepartmentCount(GridSearchFields.PersonName, manager.ID, 0, searchKey, "");
                    }
                    else if (searchType == GridSearchFields.PersonCode)
                    {
                        Result = managerRepository.GetUnderManagmentByDepartmentCount(GridSearchFields.PersonCode, BUser.CurrentUser.Person.ID, 0, "", searchKey);
                    }
                    else
                    {
                        Result = managerRepository.GetUnderManagmentByDepartmentCount(GridSearchFields.Complex, BUser.CurrentUser.Person.ID, 0, searchKey, searchKey);
                        //Result = managerRepository.GetUnderManagmentByDepartmentCount(GridSearchFields.PersonName, BUser.CurrentUser.Person.ID, 0, searchKey, "");
                        //if (Result == 0)
                        //    Result = managerRepository.GetUnderManagmentByDepartmentCount(GridSearchFields.PersonCode, BUser.CurrentUser.Person.ID, 0, "", searchKey);
                    }
                    return Result;
                }
                else if (InitOperator())
                {
                    decimal oprPrsId = BUser.CurrentUser.Person.ID;
                    int Result = 0;
                    if (searchType == GridSearchFields.PersonName)
                    {
                        Result = managerRepository.GetUnderManagmentOperatorByDepartmentCount(GridSearchFields.PersonName, oprPrsId, 0, searchKey, "");
                    }
                    else if (searchType == GridSearchFields.PersonCode)
                    {
                        Result = managerRepository.GetUnderManagmentOperatorByDepartmentCount(GridSearchFields.PersonCode, oprPrsId, 0, "", searchKey);
                    }
                    else
                    {
                        Result = managerRepository.GetUnderManagmentOperatorByDepartmentCount(GridSearchFields.Complex, oprPrsId, 0, searchKey, searchKey);
                        //Result = managerRepository.GetUnderManagmentOperatorByDepartmentCount(GridSearchFields.PersonName, oprPrsId, 0, searchKey, "");
                        //if (Result == 0)
                        //    Result = managerRepository.GetUnderManagmentOperatorByDepartmentCount(GridSearchFields.PersonCode, oprPrsId, 0, "", searchKey);
                    }
                    return Result;
                }
                else
                {
                    throw new IllegalServiceAccess(String.Format("این سرویس تنها توسط مدیران قابل استفاده میباشد. شناسه کاربری {0} میباشد", this.Username), ExceptionSrc);
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BWorkedTime", "GetUnderManagmentBySearchCount");
                throw ex;
            }
        }

        /// <summary>
        /// افراد تحت مدیریت را براساس کلمه جستجوشده برمیگرداند
        /// </summary>
        /// <param name="month">ماه</param>
        /// <param name="searchKey">عبارت جستجو</param>
        /// <param name="searchType">فیلد جستجو شونده</param>
        /// <param name="pageIndex">شماره صفحه</param>
        /// <param name="pageSize">تعداد رکوردهای صفحه</param>
        /// <param name="orderField">فیلد مرتب سازی</param>
        /// <param name="orderType">نوع مرتب سازی</param>
        /// <returns>لیست افراد تحت مدیریت</returns>
        public IList<UnderManagementPerson> GetUnderManagmentBySearch(int month, string searchKey, GridSearchFields searchType, int pageIndex, int pageSize, GridOrderFields orderField, GridOrderFieldType orderType)
        {
            try
            {
                if (InitManager())
                {
                    if (orderField == GridOrderFields.NONE)
                        orderField = GridOrderFields.gridFields_BarCode;
                    IList<UnderManagementPerson> Result = new List<UnderManagementPerson>();
                    if (searchType == GridSearchFields.PersonName)
                    {
                        Result = managerRepository.GetUnderManagmentByDepartment(GridSearchFields.PersonName, BUser.CurrentUser.Person.ID, 0, searchKey, "", month, month > 0 ? 0 : Utility.ToDateRangeIndex(DateTime.Now, sysLanguageResource), DateTime.Now.ToString("yyyy/MM/dd"), orderField, orderType, pageIndex, pageSize);
                    }
                    else if (searchType == GridSearchFields.PersonCode)
                    {
                        Result = managerRepository.GetUnderManagmentByDepartment(GridSearchFields.PersonCode, BUser.CurrentUser.Person.ID, 0, "", searchKey, month, month > 0 ? 0 : Utility.ToDateRangeIndex(DateTime.Now, sysLanguageResource), DateTime.Now.ToString("yyyy/MM/dd"), orderField, orderType, pageIndex, pageSize);
                    }
                    else
                    {
                        Result = managerRepository.GetUnderManagmentByDepartment(GridSearchFields.Complex, BUser.CurrentUser.Person.ID, 0, searchKey, searchKey, month, month > 0 ? 0 : Utility.ToDateRangeIndex(DateTime.Now, sysLanguageResource), DateTime.Now.ToString("yyyy/MM/dd"), orderField, orderType, pageIndex, pageSize);
                        //Result = managerRepository.GetUnderManagmentByDepartment(GridSearchFields.PersonName, BUser.CurrentUser.Person.ID, 0, searchKey, "", month, month > 0 ? 0 : Utility.ToDateRangeIndex(DateTime.Now, sysLanguageResource), DateTime.Now.ToString("yyyy/MM/dd"), orderField, orderType, pageIndex, pageSize);
                        //if (Result == null || Result.Count == 0)
                        //    Result = managerRepository.GetUnderManagmentByDepartment(GridSearchFields.PersonCode, BUser.CurrentUser.Person.ID, 0, "", searchKey, month, month > 0 ? 0 : Utility.ToDateRangeIndex(DateTime.Now, sysLanguageResource), DateTime.Now.ToString("yyyy/MM/dd"), orderField, orderType, pageIndex, pageSize);
                    }
                    foreach (UnderManagementPerson under in Result)
                    {
                        #region LoadBasicTraffic
                        GTS.Clock.Business.BaseInformation.BTraffic trafficBus = new BaseInformation.BTraffic();
                        IList<BasicTrafficProxy> trafficList = trafficBus.GetDayTraffics(under.PersonId, Utility.ToString(DateTime.Now));
                        int counter = 1;
                        foreach (BasicTrafficProxy trafic in trafficList)
                        {
                            switch (counter)
                            {
                                case 1:
                                    under.FirstEntrance = trafic.TheTime;
                                    break;
                                case 2:
                                    under.FirstExit = trafic.TheTime;
                                    break;
                                case 3:
                                    under.SecondEntrance = trafic.TheTime;
                                    break;
                                case 4:
                                    under.SecondExit = trafic.TheTime;
                                    break;
                                case 5:
                                    under.ThirdEntrance = trafic.TheTime;
                                    break;
                                case 6:
                                    under.ThirdExit = trafic.TheTime;
                                    break;
                                case 7:
                                    under.FourthEntrance = trafic.TheTime;
                                    break;
                                case 8:
                                    under.FourthExit = trafic.TheTime;
                                    break;
                                case 9:
                                    under.FifthEntrance = trafic.TheTime;
                                    break;
                                case 10:
                                    under.FifthExit = trafic.TheTime;
                                    break;

                            }
                            counter++;
                        }
                        under.LastExit = trafficList.LastOrDefault() != null ? trafficList.LastOrDefault().TheTime : "";
                        #endregion
                    }
                    return Result;
                }
                else if (InitOperator()) 
                {
                    decimal oprPrsId = BUser.CurrentUser.Person.ID;
                    if (orderField == GridOrderFields.NONE)
                        orderField = GridOrderFields.gridFields_BarCode;
                    IList<UnderManagementPerson> Result = new List<UnderManagementPerson>();
                    if (searchType == GridSearchFields.PersonName)
                    {
                        Result = managerRepository.GetUnderManagmentOperatorByDepartment(GridSearchFields.PersonName, oprPrsId, 0, searchKey, "", month, month > 0 ? 0 : Utility.ToDateRangeIndex(DateTime.Now, sysLanguageResource), DateTime.Now.ToString("yyyy/MM/dd"), orderField, orderType, pageIndex, pageSize);
                    }
                    else if (searchType == GridSearchFields.PersonCode)
                    {
                        Result = managerRepository.GetUnderManagmentOperatorByDepartment(GridSearchFields.PersonCode, oprPrsId, 0, "", searchKey, month, month > 0 ? 0 : Utility.ToDateRangeIndex(DateTime.Now, sysLanguageResource), DateTime.Now.ToString("yyyy/MM/dd"), orderField, orderType, pageIndex, pageSize);
                    }
                    else
                    {
                        Result = managerRepository.GetUnderManagmentOperatorByDepartment(GridSearchFields.Complex, BUser.CurrentUser.Person.ID, 0, searchKey, searchKey, month, month > 0 ? 0 : Utility.ToDateRangeIndex(DateTime.Now, sysLanguageResource), DateTime.Now.ToString("yyyy/MM/dd"), orderField, orderType, pageIndex, pageSize);
                        //Result = managerRepository.GetUnderManagmentOperatorByDepartment(GridSearchFields.PersonName, oprPrsId, 0, searchKey, "", month, month > 0 ? 0 : Utility.ToDateRangeIndex(DateTime.Now, sysLanguageResource), DateTime.Now.ToString("yyyy/MM/dd"), orderField, orderType, pageIndex, pageSize);
                        //if (Result == null || Result.Count == 0)
                        //    Result = managerRepository.GetUnderManagmentOperatorByDepartment(GridSearchFields.PersonCode, oprPrsId, 0, "", searchKey, month, month > 0 ? 0 : Utility.ToDateRangeIndex(DateTime.Now, sysLanguageResource), DateTime.Now.ToString("yyyy/MM/dd"), orderField, orderType, pageIndex, pageSize);

                    }
                    foreach (UnderManagementPerson under in Result)
                    {
                        #region LoadBasicTraffic
                        GTS.Clock.Business.BaseInformation.BTraffic trafficBus = new BaseInformation.BTraffic();
                        IList<BasicTrafficProxy> trafficList = trafficBus.GetDayTraffics(under.PersonId, Utility.ToString(DateTime.Now));
                        int counter = 1;
                        foreach (BasicTrafficProxy trafic in trafficList)
                        {
                            switch (counter)
                            {
                                case 1:
                                    under.FirstEntrance = trafic.TheTime;
                                    break;
                                case 2:
                                    under.FirstExit = trafic.TheTime;
                                    break;
                                case 3:
                                    under.SecondEntrance = trafic.TheTime;
                                    break;
                                case 4:
                                    under.SecondExit = trafic.TheTime;
                                    break;
                                case 5:
                                    under.ThirdEntrance = trafic.TheTime;
                                    break;
                                case 6:
                                    under.ThirdExit = trafic.TheTime;
                                    break;
                                case 7:
                                    under.FourthEntrance = trafic.TheTime;
                                    break;
                                case 8:
                                    under.FourthExit = trafic.TheTime;
                                    break;
                                case 9:
                                    under.FifthEntrance = trafic.TheTime;
                                    break;
                                case 10:
                                    under.FifthExit = trafic.TheTime;
                                    break;

                            }
                            counter++;
                        }
                        under.LastExit = trafficList.LastOrDefault() != null ? trafficList.LastOrDefault().TheTime : "";
                        #endregion
                    }
                    return Result;
                }
                else
                {
                    throw new IllegalServiceAccess(String.Format("این سرویس تنها توسط مدیران قابل استفاده میباشد. شناسه کاربری {0} میباشد", this.Username), ExceptionSrc);
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BWorkedTime", "GetUnderManagmentByDepartment");
                throw ex;
            }
        }

        /// <summary>
        /// افراد تحت مدیریت را براساس کلمه جستجوشده برمیگرداند
        /// فقط جریان اصلی
        /// فقط مدیران و شامل جانشین نمیشود
        /// </summary>
        /// <param name="month">ماه</param>
        /// <param name="searchKey">عبارت جستجو</param>
        /// <param name="searchType">فیلد جستجو شونده</param>
        /// <param name="pageIndex">شماره صفحه</param>
        /// <param name="pageSize">تعداد رکوردهای صفحه</param>
        /// <param name="orderField">فیلد مرتب سازی</param>
        /// <param name="orderType">نوع مرتب سازی</param>
        /// <returns>لیست افراد تحت مدیریت</returns>
        public IList<UnderManagementPerson> GetJustUnderManagmentMainflowBySearch(int month, string searchKey,decimal managerPersonId, GridSearchFields searchType, int pageIndex, int pageSize, GridOrderFields orderField, GridOrderFieldType orderType)
        {
            try
            {
                if (IsManager(managerPersonId))
                {
                    if (orderField == GridOrderFields.NONE)
                        orderField = GridOrderFields.gridFields_BarCode;
                    IList<UnderManagementPerson> Result = new List<UnderManagementPerson>();
                    if (searchType == GridSearchFields.PersonName)
                    {
                        Result = managerRepository.GetUnderManagmentByDepartment_JustMainManagers(GridSearchFields.PersonName, managerPersonId, 0, searchKey, "", month, month > 0 ? 0 : Utility.ToDateRangeIndex(DateTime.Now, sysLanguageResource), DateTime.Now.ToString("yyyy/MM/dd"), orderField, orderType, pageIndex, pageSize);
                    }
                    else if (searchType == GridSearchFields.PersonCode)
                    {
                        Result = managerRepository.GetUnderManagmentByDepartment_JustMainManagers(GridSearchFields.PersonCode, managerPersonId, 0, "", searchKey, month, month > 0 ? 0 : Utility.ToDateRangeIndex(DateTime.Now, sysLanguageResource), DateTime.Now.ToString("yyyy/MM/dd"), orderField, orderType, pageIndex, pageSize);
                    }
                    else
                    {
                        Result = managerRepository.GetUnderManagmentByDepartment_JustMainManagers(GridSearchFields.Complex, managerPersonId, 0, searchKey, searchKey, month, month > 0 ? 0 : Utility.ToDateRangeIndex(DateTime.Now, sysLanguageResource), DateTime.Now.ToString("yyyy/MM/dd"), orderField, orderType, pageIndex, pageSize);
                        //Result = managerRepository.GetUnderManagmentByDepartment(GridSearchFields.PersonName, BUser.CurrentUser.Person.ID, 0, searchKey, "", month, month > 0 ? 0 : Utility.ToDateRangeIndex(DateTime.Now, sysLanguageResource), DateTime.Now.ToString("yyyy/MM/dd"), orderField, orderType, pageIndex, pageSize);
                        //if (Result == null || Result.Count == 0)
                        //    Result = managerRepository.GetUnderManagmentByDepartment(GridSearchFields.PersonCode, BUser.CurrentUser.Person.ID, 0, "", searchKey, month, month > 0 ? 0 : Utility.ToDateRangeIndex(DateTime.Now, sysLanguageResource), DateTime.Now.ToString("yyyy/MM/dd"), orderField, orderType, pageIndex, pageSize);
                    }
                    foreach (UnderManagementPerson under in Result)
                    {

                        IList<InfoPeriodicScndCnpValue> infoPeriodicScndCnpValueList = NHibernateSessionManager.Instance.GetSession().GetNamedQuery("GetPeriodicScndCnpValueList")
                                                                                                                                     .SetParameter("date", under.Date)
                                                                                                                                     .SetParameter("dateRangeOrderIndex", under.DateRangeOrderIndex)
                                                                                                                                     .SetParameter("dateRangeOrder", under.DateRangeOrder)
                                                                                                                                     .SetParameter("prsId", under.PersonId)
                                                                                                                                     .SetResultTransformer(Transformers.AliasToBean(typeof(InfoPeriodicScndCnpValue)))
                                                                                                                                     .List<InfoPeriodicScndCnpValue>();
                        string defaultValue = "0";

                        InfoPeriodicScndCnpValue ipscv_NecessaryOperation = infoPeriodicScndCnpValueList.Where(x => x.ScndCnpValue_KeyColumnName == "gridFields_NecessaryOperation")
                                                                                                        .FirstOrDefault();
                        under.NecessaryOperation = ipscv_NecessaryOperation != null ? Utility.IntTimeToTime(ipscv_NecessaryOperation == null ? 0 : ipscv_NecessaryOperation.ScndCnpValue_PeriodicValue) : defaultValue;

                        InfoPeriodicScndCnpValue ipscv_HourlyPureOperation = infoPeriodicScndCnpValueList.Where(x => x.ScndCnpValue_KeyColumnName == "gridFields_HourlyPureOperation")
                                                                                                         .FirstOrDefault();
                        under.HourlyPureOperation = ipscv_HourlyPureOperation != null ? Utility.IntTimeToTime(ipscv_HourlyPureOperation == null ? 0 : ipscv_HourlyPureOperation.ScndCnpValue_PeriodicValue) : defaultValue;

                        InfoPeriodicScndCnpValue ipscv_DailyPureOperation = infoPeriodicScndCnpValueList.Where(x => x.ScndCnpValue_KeyColumnName == "gridFields_DailyPureOperation")
                                                                                                        .FirstOrDefault();
                      //  under.DailyPureOperation = ipscv_DailyPureOperation != null ? Utility.ToInteger(ipscv_DailyPureOperation == null ? 0 : ipscv_DailyPureOperation.ScndCnpValue_PeriodicValue) : defaultValue;
                        under.DailyPureOperation = ipscv_DailyPureOperation != null ? ipscv_DailyPureOperation == null ? "0" : ipscv_DailyPureOperation.ScndCnpValue_PeriodicValue.ToString() : defaultValue;

                        InfoPeriodicScndCnpValue ipscv_ImpureOperation = infoPeriodicScndCnpValueList.Where(x => x.ScndCnpValue_KeyColumnName == "gridFields_ImpureOperation")
                                                                                                     .FirstOrDefault();
                        under.ImpureOperation = ipscv_ImpureOperation != null ? Utility.IntTimeToTime(ipscv_ImpureOperation == null ? 0 : ipscv_ImpureOperation.ScndCnpValue_PeriodicValue) : defaultValue;

                        InfoPeriodicScndCnpValue ipscv_AllowableOverTime = infoPeriodicScndCnpValueList.Where(x => x.ScndCnpValue_KeyColumnName == "gridFields_AllowableOverTime")
                                                                                                       .FirstOrDefault();
                        under.AllowableOverTime = ipscv_AllowableOverTime != null ? Utility.IntTimeToTime(ipscv_AllowableOverTime == null ? 0 : ipscv_AllowableOverTime.ScndCnpValue_PeriodicValue) : defaultValue;

                        InfoPeriodicScndCnpValue ipscv_UnallowableOverTime = infoPeriodicScndCnpValueList.Where(x => x.ScndCnpValue_KeyColumnName == "gridFields_UnallowableOverTime")
                                                                                                         .FirstOrDefault();
                        under.UnallowableOverTime = ipscv_UnallowableOverTime != null ? Utility.IntTimeToTime(ipscv_UnallowableOverTime == null ? 0 : ipscv_UnallowableOverTime.ScndCnpValue_PeriodicValue) : defaultValue;

                        InfoPeriodicScndCnpValue ipscv_HourlyAllowableAbsence = infoPeriodicScndCnpValueList.Where(x => x.ScndCnpValue_KeyColumnName == "gridFields_HourlyAllowableAbsence")
                                                                                                            .FirstOrDefault();
                        under.HourlyAllowableAbsence = ipscv_HourlyAllowableAbsence != null ? Utility.IntTimeToTime(ipscv_HourlyAllowableAbsence == null ? 0 : ipscv_HourlyAllowableAbsence.ScndCnpValue_PeriodicValue) : defaultValue;

                        InfoPeriodicScndCnpValue ipscv_HourlyUnallowableAbsence = infoPeriodicScndCnpValueList.Where(x => x.ScndCnpValue_KeyColumnName == "gridFields_HourlyUnallowableAbsence")
                                                                                                              .FirstOrDefault();
                        under.HourlyUnallowableAbsence = ipscv_HourlyUnallowableAbsence != null ? Utility.IntTimeToTime(ipscv_HourlyUnallowableAbsence == null ? 0 : ipscv_HourlyUnallowableAbsence.ScndCnpValue_PeriodicValue) : defaultValue;

                        InfoPeriodicScndCnpValue ipscv_DailyAbsence = infoPeriodicScndCnpValueList.Where(x => x.ScndCnpValue_KeyColumnName == "gridFields_DailyAbsence")
                                                                                                  .FirstOrDefault();
                        //under.DailyAbsence = ipscv_DailyAbsence != null ? Utility.IntTimeToTime(ipscv_DailyAbsence == null ? 0 : ipscv_DailyAbsence.ScndCnpValue_PeriodicValue) : defaultValue;
                        under.DailyAbsence = ipscv_DailyAbsence != null ? ipscv_DailyAbsence == null ? "0" : ipscv_DailyAbsence.ScndCnpValue_PeriodicValue.ToString() : defaultValue;
                        InfoPeriodicScndCnpValue ipscv_HourlyMission = infoPeriodicScndCnpValueList.Where(x => x.ScndCnpValue_KeyColumnName == "gridFields_HourlyMission")
                                                                                                   .FirstOrDefault();
                        under.HourlyMission = ipscv_HourlyMission != null ? Utility.IntTimeToTime(ipscv_HourlyMission == null ? 0 : ipscv_HourlyMission.ScndCnpValue_PeriodicValue) : defaultValue;

                        InfoPeriodicScndCnpValue ipscv_DailyMission = infoPeriodicScndCnpValueList.Where(x => x.ScndCnpValue_KeyColumnName == "gridFields_DailyMission")
                                                                                                  .FirstOrDefault();
                        //under.DailyMission = ipscv_DailyMission != null ? Utility.IntTimeToTime(ipscv_DailyMission == null ? 0 : ipscv_DailyMission.ScndCnpValue_PeriodicValue) : defaultValue;
                        under.DailyMission = ipscv_DailyMission != null ? ipscv_DailyMission == null ? "0" : ipscv_DailyMission.ScndCnpValue_PeriodicValue.ToString() : defaultValue;
                        InfoPeriodicScndCnpValue ipscv_HostelryMission = infoPeriodicScndCnpValueList.Where(x => x.ScndCnpValue_KeyColumnName == "gridFields_HostelryMission")
                                                                                                     .FirstOrDefault();
                        //under.HostelryMission = ipscv_HostelryMission != null ? Utility.IntTimeToTime(ipscv_HostelryMission == null ? 0 : ipscv_HostelryMission.ScndCnpValue_PeriodicValue) : defaultValue;
                        under.HostelryMission = ipscv_HostelryMission != null ? ipscv_HostelryMission == null ? "0" : ipscv_HostelryMission.ScndCnpValue_PeriodicValue.ToString() : defaultValue;
                        InfoPeriodicScndCnpValue ipscv_HourlySickLeave = infoPeriodicScndCnpValueList.Where(x => x.ScndCnpValue_KeyColumnName == "gridFields_HourlySickLeave")
                                                                                                     .FirstOrDefault();
                        under.HourlySickLeave = ipscv_HourlySickLeave != null ? Utility.IntTimeToTime(ipscv_HourlySickLeave == null ? 0 : ipscv_HourlySickLeave.ScndCnpValue_PeriodicValue) : defaultValue;

                        InfoPeriodicScndCnpValue ipscv_DailySickLeave = infoPeriodicScndCnpValueList.Where(x => x.ScndCnpValue_KeyColumnName == "gridFields_DailySickLeave")
                                                                                                    .FirstOrDefault();
                        //under.DailySickLeave = ipscv_DailySickLeave != null ? Utility.IntTimeToTime(ipscv_DailySickLeave == null ? 0 : ipscv_DailySickLeave.ScndCnpValue_PeriodicValue) : defaultValue;
                        under.DailySickLeave = ipscv_DailySickLeave != null ? ipscv_DailySickLeave == null ? "0" : ipscv_DailySickLeave.ScndCnpValue_PeriodicValue.ToString() : defaultValue;
                        InfoPeriodicScndCnpValue ipscv_HourlyMeritoriouslyLeave = infoPeriodicScndCnpValueList.Where(x => x.ScndCnpValue_KeyColumnName == "gridFields_HourlyMeritoriouslyLeave")
                                                                                                              .FirstOrDefault();
                        under.HourlyMeritoriouslyLeave = ipscv_HourlyMeritoriouslyLeave != null ? Utility.IntTimeToTime(ipscv_HourlyMeritoriouslyLeave == null ? 0 : ipscv_HourlyMeritoriouslyLeave.ScndCnpValue_PeriodicValue) : defaultValue;

                        InfoPeriodicScndCnpValue ipscv_DailyMeritoriouslyLeave = infoPeriodicScndCnpValueList.Where(x => x.ScndCnpValue_KeyColumnName == "gridFields_DailyMeritoriouslyLeave")
                                                                                                             .FirstOrDefault();
                        //under.DailyMeritoriouslyLeave = ipscv_DailyMeritoriouslyLeave != null ? Utility.IntTimeToTime(ipscv_DailyMeritoriouslyLeave == null ? 0 : ipscv_DailyMeritoriouslyLeave.ScndCnpValue_PeriodicValue) : defaultValue;
                        under.DailyMeritoriouslyLeave = ipscv_DailyMeritoriouslyLeave != null ? ipscv_DailyMeritoriouslyLeave == null ? "0" : ipscv_DailyMeritoriouslyLeave.ScndCnpValue_PeriodicValue.ToString() : defaultValue;
                        InfoPeriodicScndCnpValue ipscv_HourlyWithoutPayLeave = infoPeriodicScndCnpValueList.Where(x => x.ScndCnpValue_KeyColumnName == "gridFields_HourlyWithoutPayLeave")
                                                                                                           .FirstOrDefault();
                        under.HourlyWithoutPayLeave = ipscv_HourlyWithoutPayLeave != null ? Utility.IntTimeToTime(ipscv_HourlyWithoutPayLeave == null ? 0 : ipscv_HourlyWithoutPayLeave.ScndCnpValue_PeriodicValue) : defaultValue;

                        InfoPeriodicScndCnpValue ipscv_PresenceDuration = infoPeriodicScndCnpValueList.Where(x => x.ScndCnpValue_KeyColumnName == "gridFields_PresenceDuration")
                                                                                                      .FirstOrDefault();
                        under.PresenceDuration = ipscv_PresenceDuration != null ? Utility.IntTimeToTime(ipscv_PresenceDuration == null ? 0 : ipscv_PresenceDuration.ScndCnpValue_PeriodicValue) : defaultValue;

                        InfoPeriodicScndCnpValue ipscv_DailyWithoutPayLeave = infoPeriodicScndCnpValueList.Where(x => x.ScndCnpValue_KeyColumnName == "gridFields_DailyWithoutPayLeave")
                                                                                                          .FirstOrDefault();
                        //under.DailyWithoutPayLeave = ipscv_DailyWithoutPayLeave != null ? Utility.IntTimeToTime(ipscv_DailyWithoutPayLeave == null ? 0 : ipscv_DailyWithoutPayLeave.ScndCnpValue_PeriodicValue) : defaultValue;
                        under.DailyWithoutPayLeave = ipscv_DailyWithoutPayLeave != null ? ipscv_DailyWithoutPayLeave == null ? "0" : ipscv_DailyWithoutPayLeave.ScndCnpValue_PeriodicValue.ToString() : defaultValue;
                        InfoPeriodicScndCnpValue ipscv_HourlyWithPayLeave = infoPeriodicScndCnpValueList.Where(x => x.ScndCnpValue_KeyColumnName == "gridFields_HourlyWithPayLeave")
                                                                                                        .FirstOrDefault();
                        under.HourlyWithPayLeave = ipscv_HourlyWithPayLeave != null ? Utility.IntTimeToTime(ipscv_HourlyWithPayLeave == null ? 0 : ipscv_HourlyWithPayLeave.ScndCnpValue_PeriodicValue) : defaultValue;

                        InfoPeriodicScndCnpValue ipscv_DailyWithPayLeave = infoPeriodicScndCnpValueList.Where(x => x.ScndCnpValue_KeyColumnName == "gridFields_DailyWithPayLeave")
                                                                                                       .FirstOrDefault();
                        //under.DailyWithPayLeave = ipscv_DailyWithPayLeave != null ? Utility.IntTimeToTime(ipscv_DailyWithPayLeave == null ? 0 : ipscv_DailyWithPayLeave.ScndCnpValue_PeriodicValue) : defaultValue;
                        under.DailyWithPayLeave = ipscv_DailyWithPayLeave != null ? ipscv_DailyWithPayLeave == null ? "0" : ipscv_DailyWithPayLeave.ScndCnpValue_PeriodicValue.ToString() : defaultValue;
                        InfoPeriodicScndCnpValue ipscv_ReserveField1 = infoPeriodicScndCnpValueList.Where(x => x.ScndCnpValue_KeyColumnName == "gridFields_ReserveField1")
                                                                                                   .FirstOrDefault();
                        under.ReserveField1 = ipscv_ReserveField1 != null ? Utility.IntTimeToTime(ipscv_ReserveField1 == null ? 0 : ipscv_ReserveField1.ScndCnpValue_PeriodicValue) : defaultValue;

                        InfoPeriodicScndCnpValue ipscv_ReserveField2 = infoPeriodicScndCnpValueList.Where(x => x.ScndCnpValue_KeyColumnName == "gridFields_ReserveField2")
                                                                                                   .FirstOrDefault();
                        under.ReserveField2 = ipscv_ReserveField2 != null ? Utility.IntTimeToTime(ipscv_ReserveField2 == null ? 0 : ipscv_ReserveField2.ScndCnpValue_PeriodicValue) : defaultValue;

                        InfoPeriodicScndCnpValue ipscv_ReserveField3 = infoPeriodicScndCnpValueList.Where(x => x.ScndCnpValue_KeyColumnName == "gridFields_ReserveField3")
                                                                                                   .FirstOrDefault();
                        under.ReserveField3 = ipscv_ReserveField3 != null ? Utility.IntTimeToTime(ipscv_ReserveField3 == null ? 0 : ipscv_ReserveField3.ScndCnpValue_PeriodicValue) : defaultValue;

                        InfoPeriodicScndCnpValue ipscv_ReserveField4 = infoPeriodicScndCnpValueList.Where(x => x.ScndCnpValue_KeyColumnName == "gridFields_ReserveField4")
                                                                                                   .FirstOrDefault();
                        under.ReserveField4 = ipscv_ReserveField4 != null ? Utility.IntTimeToTime(ipscv_ReserveField4 == null ? 0 : ipscv_ReserveField4.ScndCnpValue_PeriodicValue) : defaultValue;

                        InfoPeriodicScndCnpValue ipscv_ReserveField5 = infoPeriodicScndCnpValueList.Where(x => x.ScndCnpValue_KeyColumnName == "gridFields_ReserveField5")
                                                                                                   .FirstOrDefault();
                        under.ReserveField5 = ipscv_ReserveField5 != null ? Utility.IntTimeToTime(ipscv_ReserveField5 == null ? 0 : ipscv_ReserveField5.ScndCnpValue_PeriodicValue) : defaultValue;

                        InfoPeriodicScndCnpValue ipscv_ReserveField6 = infoPeriodicScndCnpValueList.Where(x => x.ScndCnpValue_KeyColumnName == "gridFields_ReserveField6")
                                                                                                   .FirstOrDefault();
                        under.ReserveField6 = ipscv_ReserveField6 != null ? Utility.IntTimeToTime(ipscv_ReserveField6 == null ? 0 : ipscv_ReserveField6.ScndCnpValue_PeriodicValue) : defaultValue;

                        InfoPeriodicScndCnpValue ipscv_ReserveField7 = infoPeriodicScndCnpValueList.Where(x => x.ScndCnpValue_KeyColumnName == "gridFields_ReserveField7")
                                                                                                   .FirstOrDefault();
                        under.ReserveField7 = ipscv_ReserveField7 != null ? Utility.IntTimeToTime(ipscv_ReserveField7 == null ? 0 : ipscv_ReserveField7.ScndCnpValue_PeriodicValue) : defaultValue;

                        InfoPeriodicScndCnpValue ipscv_ReserveField8 = infoPeriodicScndCnpValueList.Where(x => x.ScndCnpValue_KeyColumnName == "gridFields_ReserveField8")
                                                                                                   .FirstOrDefault();
                        under.ReserveField8 = ipscv_ReserveField8 != null ? Utility.IntTimeToTime(ipscv_ReserveField8 == null ? 0 : ipscv_ReserveField8.ScndCnpValue_PeriodicValue) : defaultValue;

                        InfoPeriodicScndCnpValue ipscv_ReserveField9 = infoPeriodicScndCnpValueList.Where(x => x.ScndCnpValue_KeyColumnName == "gridFields_ReserveField9")
                                                                                                   .FirstOrDefault();
                        under.ReserveField9 = ipscv_ReserveField9 != null ? Utility.IntTimeToTime(ipscv_ReserveField9 == null ? 0 : ipscv_ReserveField9.ScndCnpValue_PeriodicValue) : defaultValue;

                        InfoPeriodicScndCnpValue ipscv_ReserveField10 = infoPeriodicScndCnpValueList.Where(x => x.ScndCnpValue_KeyColumnName == "gridFields_ReserveField10")
                                                                                                    .FirstOrDefault();
                        under.ReserveField10 = ipscv_ReserveField10 != null ? Utility.IntTimeToTime(ipscv_ReserveField10 == null ? 0 : ipscv_ReserveField10.ScndCnpValue_PeriodicValue) : defaultValue;


                    }

                    return Result;
                }
                else
                {
                    throw new IllegalServiceAccess(String.Format("این سرویس تنها توسط مدیران قابل استفاده میباشد. شناسه کاربری {0} میباشد", this.Username), ExceptionSrc);
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BWorkedTime", "GetUnderManagmentByDepartment");
                throw ex;
            }
        }


        /// <summary>
        /// مانده مرخصی یک پرسنل را تاانتهای سال برمیگرداند
        /// فرمت های خروجی بر اساس زبان سیستم
        /// {0} روز و {1} ساعت و {2} دقیقه
        /// {0} days and {1} hours and {2} minutes
        /// محاسبه نشده
        /// Not Calculated
        /// </summary>
        /// <param name="personId">کلید اصلی پرسنل</param>
        /// <param name="year">سال</param>
        /// <param name="month">ماه</param>
        /// <returns>مانده مرخصی</returns>
        private string GetReainLeaveToEndOfYear(decimal personId, int year, int month)
        {
            string remain = "";
            try
            {
                ILeaveInfo leaveInfo = new BRemainLeave();
                int day, minutes;
                leaveInfo.GetRemainLeaveToEndOfYear(personId, year, month,0, out day, out minutes);
                int hour = (minutes / 60);
                int min = minutes % 60;
                if (BLanguage.CurrentLocalLanguage == LanguagesName.Parsi)
                {
                    remain = String.Format(" {0} روز و {1} ساعت و {2} دقیقه", day, hour, min);
                }
                else if (BLanguage.CurrentLocalLanguage == LanguagesName.English)
                {
                    remain = String.Format(" {0} days and {1} hours and {2} minutes", day, hour, min);
                }
            }
            catch (InvalidDatabaseStateException ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BWorkdTime", "GetRemainLeaveToEndOfYear");
                if (BLanguage.CurrentLocalLanguage == LanguagesName.Parsi)
                {
                    remain = " محاسبه نشده";
                }
                else if (BLanguage.CurrentLocalLanguage == LanguagesName.English)
                {
                    remain = "Not Calculated";
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BWorkdTime", "GetRemainLeaveToEndOfYear");
                throw ex;
            }
            return remain;
        }

        /// <summary>
        /// مانده مرخصی یک پرسنل را تاانتهای ماه برمیگرداند
        /// فرمت های خروجی بر اساس زبان سیستم
        /// {0} روز و {1} ساعت و {2} دقیقه
        /// {0} days and {1} hours and {2} minutes
        /// محاسبه نشده
        /// Not Calculated
        /// </summary>
        /// <param name="personId">کلید اصلی پرسنل</param>
        /// <param name="year">سال</param>
        /// <param name="month">ماه</param>
        /// <returns>مانده مرخصی</returns>
        private string GetReaiLeaveToEndMonth(decimal personId, int year, int month)
        {
            string remain = "";
            try
            {
                ILeaveInfo leaveInfo = new BRemainLeave();
                int day, minutes;
                leaveInfo.GetRemainLeaveToEndOfMonth(personId, year, month,0, out day, out minutes);
                int hour = (minutes / 60);
                int min = minutes % 60;
                if (BLanguage.CurrentLocalLanguage == LanguagesName.Parsi)
                {
                    remain = String.Format(" {0} روز و {1} ساعت و {2} دقیقه", day, hour, min);
                }
                else if (BLanguage.CurrentLocalLanguage == LanguagesName.English)
                {
                    remain = String.Format(" {0} days and {1} hours and {2} minutes", day, hour, min);
                }
            }
            catch (InvalidDatabaseStateException ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BWorkdTime", "GetReaiLeaveToEndMonth");
                if (BLanguage.CurrentLocalLanguage == LanguagesName.Parsi)
                {
                    remain = " محاسبه نشده";
                }
                else if (BLanguage.CurrentLocalLanguage == LanguagesName.English)
                {
                    remain = "Not Calculated";
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BWorkdTime", "GetReaiLeaveToEndMonth");
                throw ex;
            }
            return remain;
        }

        /// <summary>
        /// سر ستون رزورو فیلدها را در گزارش کارکرد ماهانه برمیگرداند
        /// </summary>
        /// <param name="field">فیلد</param>
        /// <returns>سر ستون رزورو فیلد</returns>
        public string GetReservedFieldsName(ConceptReservedFields field)
        {
            try
            {
                return new BPersonMonthlyWorkedTime(0).GetReservedFieldsName(field);
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, this.GetType().Name, "GetReservedFieldsName");
                throw ex;
            }
        }

        /// <summary>
        /// بررسی دسترسی مدیر به عملیات ماهانه
        /// </summary>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckManagerMasterMonthlyOperationLoadAccess()
        {
        }

        /// <summary>
        /// بررسی دسترسی پرسنل به عملیات ماهانه 
        /// </summary>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckPersonnelMasterMonthlyOperationLoadAccess()
        {
        }

        #region private methods

        /// <summary>
        ///   بصورت بازگشتی درحت را پیمایش و شرط نمایش را بررسی میکند
        ///  اگر تشخیص داده شد که گره ای نباید نشان داده شود نیازی به پیمایش گره های فرزند نیست
        ///  زیرا این تشخیص شامل آنها نیز میشود
        /// </summary>
        /// <param name="department">چارت سازمان</param>
        /// <param name="flow">حریان کاری</param>
        /// <param name="containsChildList">لیست زیر گره های چارت سازمانی</param>
        private void SetVisibility(Department department, Flow flow, IList<Department> containsChildList)
        {
            BFlow bFlow = new BFlow();
            IList<Department> departmentsList = new BDepartment().GetAll();
            //IList<decimal> restrictionIds = accessPort.GetAccessibleDeparments();
            if (department.ChildList != null)
            {
                foreach (Department child in department.ChildList)
                {
                    if (!containsChildList.Contains(child))
                    {
                        child.Visible = child.Visible || false;//ممکن است در جریانهای قبلی مقدار یک گرفته باشد
                    }
                    else
                    {
                        child.Visible = true;
                        this.SetVisibility(child, flow, bFlow.GetDepartmentChilds(child.ID, flow.ID, departmentsList));
                    }
                }
            }
        }

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
        /// بررسی و مقداردهی مدیر از روی شناسه کاربری
        /// </summary>
        /// <returns></returns>
        private bool InitManager()
        {
            if (manager == null)
            {
                if (Utility.IsEmpty(this.Username))
                {
                    this.Username = Security.BUser.CurrentUser.UserName;
                }
                BManager businessManager = new BManager();
                manager = businessManager.GetManagerByUsername(this.Username);
            }
            if (manager.ID == 0)// جانشین
            {
                SubstituteRepository subRep = new SubstituteRepository(false);
                if (subRep.IsSubstitute(Security.BUser.CurrentUser.Person.ID))
                {
                    IList<Substitute> sub = subRep.GetSubstitute(Security.BUser.CurrentUser.Person.ID);
                    manager = sub.First().Manager;
                }
            }
            if (manager.ID > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// بررسی و مقداردهی اپراتور از روی شناسه کاربری
        /// </summary>
        /// <returns></returns>
        private bool InitOperator()
        {
            BOperator businessOperator = new BOperator();
            return businessOperator.IsOperator();
        }

        /// <summary>
        /// فقط مدیر را قبول میکند
        /// شامل جانشین نمیشود
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        private bool IsManager(decimal managerPersonId)
        {
            if (manager == null)
            {
                BManager businessManager = new BManager();
                manager = businessManager.GetManager(managerPersonId);
            }
           
            if (manager.ID > 0)
            {
                return true;
            }
            return false;
        }

        #endregion

    }
}