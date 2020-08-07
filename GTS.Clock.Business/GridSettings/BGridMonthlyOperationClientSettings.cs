using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Model.UI;
using GTS.Clock.Model.Security;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Model.AppSetting;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Business.Security;
using NHibernate;
using NHibernate.Criterion;
using System.Reflection;
using GTS.Clock.Infrastructure;
using System.Data;
using System.Data.SqlClient;

namespace GTS.Clock.Business.GridSettings
{
    /// <summary>
    /// تنظیمات کاربر جهت نمایش تعداد ستون در گزارش کارکرد ماهیانه
    /// </summary>
    public class BGridMonthlyOperationClientSettings : BaseBusiness<MonthlyOperationGridClientSettings>
    {
        private const string ExceptionSrc = "GTS.Clock.Business.GridSettings.BGridMonthlyOperationClientSettings";
        private decimal workingUserSettingsId = 0;
        private decimal workingLanguageIdId = 0;
        private EntityRepository<MonthlyOperationGridClientSettings> rep = new EntityRepository<MonthlyOperationGridClientSettings>(false);
        private UserRepository userRep = new UserRepository(false);       
        private ISession NHSession = NHibernateSessionManager.Instance.GetSession();
        int operationBatchSizeValue = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings[OperationBatchSize.OperationBatchSizeKey.ToString()]);
        TempRepository tempRepository = new TempRepository(false);
        
        /// <summary>
        /// نام کاربری
        /// </summary>
        public string UserName
        {
            get;
            set;
        }

        /// <summary>
        /// سازنده کلاس
        /// </summary>
        public BGridMonthlyOperationClientSettings()
        {
        }

        /// <summary>
        /// سازنده کلاس
        /// </summary>
        /// <param name="username">نام کاربری</param>
        public BGridMonthlyOperationClientSettings(string username)
        {
            this.UserName = username;
        }

        /// <summary>
        /// ویرایش تنظیمات کاربر جهت نمایش تعداد ستون در گزارش کارکرد ماهیانه
        /// </summary>
        /// <param name="userSettingList">لیست تنظیمات کاربر</param>
        /// <param name="monthlyOperationGridRoleSettings">تنظیمات گزارش کارکرد ماهیانه</param>
        public void UpdateMonthlyOperationGridClientSettings(IList<UserSettings> userSettingList, MonthlyOperationGridRoleSettings monthlyOperationGridRoleSettings)
        {
            try
            {               
                    string operationGUID = string.Empty;
                    string SQLCommand = string.Empty;
                    IList<decimal> ExistUserSettingList = new List<decimal>();
                    IList<decimal> userSettingIds = userSettingList.Select(x => x.ID).ToList<decimal>();
              if( userSettingList.Count < this.operationBatchSizeValue &&  this.operationBatchSizeValue < 2100)
              {
                  ExistUserSettingList = NHSession.QueryOver<MonthlyOperationGridClientSettings>()
                                                  .Where(x => x.UserSetting.ID.IsIn(userSettingList.Select(y => y.ID).ToArray()))
                                                  .Select(x => x.UserSetting.ID)
                                                  .List<decimal>();
              }
              else
              {
                  GTS.Clock.Model.Temp.Temp tempAlias = null;
                  UserSettings userSettingsAlias = null;
                  MonthlyOperationGridClientSettings mOperationGridClientSettingAlias = null;
                  operationGUID = this.tempRepository.InsertTempList(userSettingIds);
                  ExistUserSettingList = NHSession.QueryOver(() => mOperationGridClientSettingAlias)
                                                  .JoinAlias(() => mOperationGridClientSettingAlias.UserSetting, () => userSettingsAlias)
                                                  .JoinAlias(() => userSettingsAlias.TempList ,() => tempAlias)
                                                  .Where(() => tempAlias.OperationGUID == operationGUID)
                                                  .Select(x => x.UserSetting.ID)
                                                  .List<decimal>();
                  this.tempRepository.DeleteTempList(operationGUID);                            
              }
                   
                    IList<decimal> NotExistUserSettingList = userSettingList.Where(x => !ExistUserSettingList.Contains(x.ID)).ToList<UserSettings>().Select(x => x.ID).ToList<decimal>();
                    #region "UpdateGridMonthlyOperationClientSetting"
                    if (ExistUserSettingList.Count != 0)
                    {
                        if (ExistUserSettingList.Count < this.operationBatchSizeValue && this.operationBatchSizeValue < 2100)
                        {
                            #region "SQLCommand"
                            SQLCommand = @"UPDATE dbo.TA_GridMonthlyOperationGridClientSettings 
                                           SET   DayName =:DayName  ,
                                                 TheDate  =:TheDate ,             
                                                 FirstEntrance =:FirstEntrance ,  
                                                 FirstExit     =:FirstExit,
                                                 SecondEntrance =:SecondEntrance,
                                                 SecondExit =:SecondExit,
                                                 ThirdEntrance =:ThirdEntrance,
                                                 ThirdExit =:ThirdExit,
                                                 FourthEntrance =:FourthEntrance,
                                                 FourthExit =:FourthExit ,
                                                 FifthEntrance =:FifthEntrance,
                                                 FifthExit =:FifthExit,
                                                 LastExit =:LastExit,
                                                 NecessaryOperation =:NecessaryOperation,
                                                 PresenceDuration =:PresenceDuration,
                                                 HourlyPureOperation =:HourlyPureOperation,
                                                 DailyPureOperation =:DailyPureOperation,
                                                 ImpureOperation =:ImpureOperation,
                                                 AllowableOverTime =:AllowableOverTime,
                                                 UnallowableOverTime =:UnallowableOverTime,
                                                 HourlyAllowableAbsence =:HourlyAllowableAbsence,
                                                  HourlyUnallowableAbsence =:HourlyUnallowableAbsence,
                                                  DailyAbsence =:DailyAbsence,
                                                  HourlyMission =:HourlyMission,
                                                  DailyMission =:DailyMission,
                                                  HostelryMission  =:HostelryMission ,
                                                  HourlySickLeave =:HourlySickLeave,
                                                  DailySickLeave =:DailySickLeave,
                                                  HourlyMeritoriouslyLeave =:HourlyMeritoriouslyLeave,
                                                  DailyMeritoriouslyLeave =:DailyMeritoriouslyLeave,
                                                  HourlyWithoutPayLeave =:HourlyWithoutPayLeave,
                                                  DailyWithoutPayLeave =:DailyWithoutPayLeave,
                                                  HourlyWithPayLeave =:HourlyWithPayLeave,
                                                  DailyWithPayLeave =:DailyWithPayLeave,
                                                  Shift  =:Shift,
                                                  ReserveField1 =:ReserveField1,
                                                  ReserveField2 =:ReserveField2,
                                                  ReserveField3 =:ReserveField3,
                                                  ReserveField4 =:ReserveField4,
                                                  ReserveField5 =:ReserveField5,
                                                  ReserveField6 =:ReserveField6,          
                                                  ReserveField7 =:ReserveField7,
                                                  ReserveField8 =:ReserveField8,
                                                  ReserveField9 =:ReserveField9,
                                                  ReserveField10 =:ReserveField10,
                                                  DayState =:DayState
                                                  WHERE  UserSettingId  in (:userSettingsList)";
                            #endregion                            
                        }
                        else
                        {
                            operationGUID = this.tempRepository.InsertTempList(ExistUserSettingList);                                                                    
                            #region "SQLCommand"
                            SQLCommand = @"UPDATE  dbo.TA_GridMonthlyOperationGridClientSettings
                                            SET   DayName =:DayName  ,
                                                 TheDate  =:TheDate ,             
                                                 FirstEntrance =:FirstEntrance ,  
                                                 FirstExit     =:FirstExit,
                                                 SecondEntrance =:SecondEntrance,
                                                 SecondExit =:SecondExit,
                                                 ThirdEntrance =:ThirdEntrance,
                                                 ThirdExit =:ThirdExit,
                                                 FourthEntrance =:FourthEntrance,
                                                 FourthExit =:FourthExit ,
                                                 FifthEntrance =:FifthEntrance,
                                                 FifthExit =:FifthExit,
                                                 LastExit =:LastExit,
                                                 NecessaryOperation =:NecessaryOperation,
                                                 PresenceDuration =:PresenceDuration,
                                                 HourlyPureOperation =:HourlyPureOperation,
                                                 DailyPureOperation =:DailyPureOperation,
                                                 ImpureOperation =:ImpureOperation,
                                                 AllowableOverTime =:AllowableOverTime,
                                                 UnallowableOverTime =:UnallowableOverTime,
                                                 HourlyAllowableAbsence =:HourlyAllowableAbsence,
                                                  HourlyUnallowableAbsence =:HourlyUnallowableAbsence,
                                                  DailyAbsence =:DailyAbsence,
                                                  HourlyMission =:HourlyMission,
                                                  DailyMission =:DailyMission,
                                                  HostelryMission  =:HostelryMission ,
                                                  HourlySickLeave =:HourlySickLeave,
                                                  DailySickLeave =:DailySickLeave,
                                                  HourlyMeritoriouslyLeave =:HourlyMeritoriouslyLeave,
                                                  DailyMeritoriouslyLeave =:DailyMeritoriouslyLeave,
                                                  HourlyWithoutPayLeave =:HourlyWithoutPayLeave,
                                                  DailyWithoutPayLeave =:DailyWithoutPayLeave,
                                                  HourlyWithPayLeave =:HourlyWithPayLeave,
                                                  DailyWithPayLeave =:DailyWithPayLeave,
                                                  Shift  =:Shift,
                                                  ReserveField1 =:ReserveField1,
                                                  ReserveField2 =:ReserveField2,
                                                  ReserveField3 =:ReserveField3,
                                                  ReserveField4 =:ReserveField4,
                                                  ReserveField5 =:ReserveField5,
                                                  ReserveField6 =:ReserveField6,          
                                                  ReserveField7 =:ReserveField7,
                                                  ReserveField8 =:ReserveField8,
                                                  ReserveField9 =:ReserveField9,
                                                  ReserveField10 =:ReserveField10 ,
                                                  DayState =:DayState
                                                  WHERE  UserSettingId  in ( SELECT UserSettingId FROM TA_GridMonthlyOperationGridClientSettings 
                                                                                                                              Inner join
                                                                                                                              TA_Temp on UserSettingId = temp_ObjectID
                                                                                                                              where temp_OperationGUID = :operationGUID ) ";
                            #endregion                                                       
                        }
                        IQuery query = NHibernateSessionManager.Instance.GetSession().CreateSQLQuery(SQLCommand);
                        #region "GetPropertyInfo"
                        foreach (PropertyInfo pInfo in typeof(MonthlyOperationGridRoleSettings).GetProperties())
                        {
                            switch (pInfo.Name)
                            {
                                case "DayName":
                                    query.SetParameter("DayName", (bool)pInfo.GetValue(monthlyOperationGridRoleSettings, null));
                                    break;
                                case "TheDate":
                                    query.SetParameter("TheDate", (bool)pInfo.GetValue(monthlyOperationGridRoleSettings, null));
                                    break;
                                case "FirstEntrance":
                                    query.SetParameter("FirstEntrance", (bool)pInfo.GetValue(monthlyOperationGridRoleSettings, null));
                                    break;
                                case "FirstExit":
                                    query.SetParameter("FirstExit", (bool)pInfo.GetValue(monthlyOperationGridRoleSettings, null));
                                    break;
                                case "SecondEntrance":
                                    query.SetParameter("SecondEntrance", (bool)pInfo.GetValue(monthlyOperationGridRoleSettings, null));
                                    break;
                                case "SecondExit":
                                    query.SetParameter("SecondExit", (bool)pInfo.GetValue(monthlyOperationGridRoleSettings, null));
                                    break;
                                case "ThirdEntrance":
                                    query.SetParameter("ThirdEntrance", (bool)pInfo.GetValue(monthlyOperationGridRoleSettings, null));
                                    break;
                                case "ThirdExit":
                                    query.SetParameter("ThirdExit", (bool)pInfo.GetValue(monthlyOperationGridRoleSettings, null));
                                    break;
                                case "FourthEntrance":
                                    query.SetParameter("FourthEntrance", (bool)pInfo.GetValue(monthlyOperationGridRoleSettings, null));
                                    break;
                                case "FourthExit":
                                    query.SetParameter("FourthExit", (bool)pInfo.GetValue(monthlyOperationGridRoleSettings, null));
                                    break;
                                case "FifthEntrance":
                                    query.SetParameter("FifthEntrance", (bool)pInfo.GetValue(monthlyOperationGridRoleSettings, null));
                                    break;
                                case "FifthExit":
                                    query.SetParameter("FifthExit", (bool)pInfo.GetValue(monthlyOperationGridRoleSettings, null));
                                    break;
                                case "LastExit":
                                    query.SetParameter("LastExit", (bool)pInfo.GetValue(monthlyOperationGridRoleSettings, null));
                                    break;
                                case "NecessaryOperation":
                                    query.SetParameter("NecessaryOperation", (bool)pInfo.GetValue(monthlyOperationGridRoleSettings, null));
                                    break;
                                case "PresenceDuration":
                                    query.SetParameter("PresenceDuration", (bool)pInfo.GetValue(monthlyOperationGridRoleSettings, null));
                                    break;
                                case "HourlyPureOperation":
                                    query.SetParameter("HourlyPureOperation", (bool)pInfo.GetValue(monthlyOperationGridRoleSettings, null));
                                    break;
                                case "DailyPureOperation":
                                    query.SetParameter("DailyPureOperation", (bool)pInfo.GetValue(monthlyOperationGridRoleSettings, null));
                                    break;
                                case "ImpureOperation":
                                    query.SetParameter("ImpureOperation", (bool)pInfo.GetValue(monthlyOperationGridRoleSettings, null));
                                    break;
                                case "AllowableOverTime":
                                    query.SetParameter("AllowableOverTime", (bool)pInfo.GetValue(monthlyOperationGridRoleSettings, null));
                                    break;
                                case "UnallowableOverTime":
                                    query.SetParameter("UnallowableOverTime", (bool)pInfo.GetValue(monthlyOperationGridRoleSettings, null));
                                    break;
                                case "HourlyAllowableAbsence":
                                    query.SetParameter("HourlyAllowableAbsence", (bool)pInfo.GetValue(monthlyOperationGridRoleSettings, null));
                                    break;
                                case "HourlyUnallowableAbsence":
                                    query.SetParameter("HourlyUnallowableAbsence", (bool)pInfo.GetValue(monthlyOperationGridRoleSettings, null));
                                    break;
                                case "DailyAbsence":
                                    query.SetParameter("DailyAbsence", (bool)pInfo.GetValue(monthlyOperationGridRoleSettings, null));
                                    break;
                                case "HourlyMission":
                                    query.SetParameter("HourlyMission", (bool)pInfo.GetValue(monthlyOperationGridRoleSettings, null));
                                    break;
                                case "DailyMission":
                                    query.SetParameter("DailyMission", (bool)pInfo.GetValue(monthlyOperationGridRoleSettings, null));
                                    break;
                                case "HostelryMission":
                                    query.SetParameter("HostelryMission", (bool)pInfo.GetValue(monthlyOperationGridRoleSettings, null));
                                    break;
                                case "HourlySickLeave":
                                    query.SetParameter("HourlySickLeave", (bool)pInfo.GetValue(monthlyOperationGridRoleSettings, null));
                                    break;
                                case "DailySickLeave":
                                    query.SetParameter("DailySickLeave", (bool)pInfo.GetValue(monthlyOperationGridRoleSettings, null));
                                    break;
                                case "HourlyMeritoriouslyLeave":
                                    query.SetParameter("HourlyMeritoriouslyLeave", (bool)pInfo.GetValue(monthlyOperationGridRoleSettings, null));
                                    break;
                                case "DailyMeritoriouslyLeave":
                                    query.SetParameter("DailyMeritoriouslyLeave", (bool)pInfo.GetValue(monthlyOperationGridRoleSettings, null));
                                    break;
                                case "HourlyWithoutPayLeave":
                                    query.SetParameter("HourlyWithoutPayLeave", (bool)pInfo.GetValue(monthlyOperationGridRoleSettings, null));
                                    break;
                                case "DailyWithoutPayLeave":
                                    query.SetParameter("DailyWithoutPayLeave", (bool)pInfo.GetValue(monthlyOperationGridRoleSettings, null));
                                    break;
                                case "HourlyWithPayLeave":
                                    query.SetParameter("HourlyWithPayLeave", (bool)pInfo.GetValue(monthlyOperationGridRoleSettings, null));
                                    break;
                                case "DailyWithPayLeave":
                                    query.SetParameter("DailyWithPayLeave", (bool)pInfo.GetValue(monthlyOperationGridRoleSettings, null));
                                    break;
                                case "Shift":
                                    query.SetParameter("Shift", (bool)pInfo.GetValue(monthlyOperationGridRoleSettings, null));
                                    break;
                                case "ReserveField1":
                                    query.SetParameter("ReserveField1", (bool)pInfo.GetValue(monthlyOperationGridRoleSettings, null));
                                    break;
                                case "ReserveField2":
                                    query.SetParameter("ReserveField2", (bool)pInfo.GetValue(monthlyOperationGridRoleSettings, null));
                                    break;
                                case "ReserveField3":
                                    query.SetParameter("ReserveField3", (bool)pInfo.GetValue(monthlyOperationGridRoleSettings, null));
                                    break;
                                case "ReserveField4":
                                    query.SetParameter("ReserveField4", (bool)pInfo.GetValue(monthlyOperationGridRoleSettings, null));
                                    break;
                                case "ReserveField5":
                                    query.SetParameter("ReserveField5", (bool)pInfo.GetValue(monthlyOperationGridRoleSettings, null));
                                    break;
                                case "ReserveField6":
                                    query.SetParameter("ReserveField6", (bool)pInfo.GetValue(monthlyOperationGridRoleSettings, null));
                                    break;
                                case "ReserveField7":
                                    query.SetParameter("ReserveField7", (bool)pInfo.GetValue(monthlyOperationGridRoleSettings, null));
                                    break;
                                case "ReserveField8":
                                    query.SetParameter("ReserveField8", (bool)pInfo.GetValue(monthlyOperationGridRoleSettings, null));
                                    break;
                                case "ReserveField9":
                                    query.SetParameter("ReserveField9", (bool)pInfo.GetValue(monthlyOperationGridRoleSettings, null));
                                    break;
                                case "ReserveField10":
                                    query.SetParameter("ReserveField10", (bool)pInfo.GetValue(monthlyOperationGridRoleSettings, null));
                                    break;
                                case "DayState":
                                    query.SetParameter("DayState", (bool)pInfo.GetValue(monthlyOperationGridRoleSettings, null));
                                    break;
                                default:
                                    break;
                            }
                        }
                        #endregion
                        if (ExistUserSettingList.Count < this.operationBatchSizeValue && this.operationBatchSizeValue < 2100)
                            query.SetParameterList("userSettingsList", ExistUserSettingList.ToArray());
                        else
                            query.SetParameter("operationGUID", operationGUID);
                        query.ExecuteUpdate();
                        this.tempRepository.DeleteTempList(operationGUID);
                    }
                    #endregion
                    #region "InsertGridMonthlyOperationGridClientSetting"
                    if (NotExistUserSettingList.Count != 0)
                    {                       
                            #region "CreateDataTable"
                            DataTable dataTable = new DataTable();
                            dataTable.Columns.Add("UserSettingId", typeof(decimal));
                            dataTable.Columns.Add("DayName", typeof(bool));
                            dataTable.Columns.Add("TheDate", typeof(bool));
                            dataTable.Columns.Add("FirstEntrance", typeof(bool));
                            dataTable.Columns.Add("FirstExit", typeof(bool));
                            dataTable.Columns.Add("SecondEntrance", typeof(bool));
                            dataTable.Columns.Add("SecondExit", typeof(bool));
                            dataTable.Columns.Add("ThirdEntrance", typeof(bool));
                            dataTable.Columns.Add("ThirdExit", typeof(bool));
                            dataTable.Columns.Add("FourthEntrance", typeof(bool));
                            dataTable.Columns.Add("FourthExit", typeof(bool));
                            dataTable.Columns.Add("FifthEntrance", typeof(bool));
                            dataTable.Columns.Add("FifthExit", typeof(bool));
                            dataTable.Columns.Add("LastExit", typeof(bool));
                            dataTable.Columns.Add("NecessaryOperation", typeof(bool));
                            dataTable.Columns.Add("PresenceDuration", typeof(bool));
                            dataTable.Columns.Add("HourlyPureOperation", typeof(bool));
                            dataTable.Columns.Add("DailyPureOperation", typeof(bool));
                            dataTable.Columns.Add("ImpureOperation", typeof(bool));
                            dataTable.Columns.Add("AllowableOverTime", typeof(bool));
                            dataTable.Columns.Add("UnallowableOverTime", typeof(bool));
                            dataTable.Columns.Add("HourlyAllowableAbsence", typeof(bool));
                            dataTable.Columns.Add("HourlyUnallowableAbsence", typeof(bool));
                            dataTable.Columns.Add("DailyAbsence", typeof(bool));
                            dataTable.Columns.Add("HourlyMission", typeof(bool));
                            dataTable.Columns.Add("DailyMission", typeof(bool));
                            dataTable.Columns.Add("HostelryMission", typeof(bool));
                            dataTable.Columns.Add("HourlySickLeave", typeof(bool));
                            dataTable.Columns.Add("DailySickLeave", typeof(bool));
                            dataTable.Columns.Add("HourlyMeritoriouslyLeave", typeof(bool));
                            dataTable.Columns.Add("DailyMeritoriouslyLeave", typeof(bool));
                            dataTable.Columns.Add("HourlyWithoutPayLeave", typeof(bool));
                            dataTable.Columns.Add("DailyWithoutPayLeave", typeof(bool));
                            dataTable.Columns.Add("HourlyWithPayLeave", typeof(bool));
                            dataTable.Columns.Add("DailyWithPayLeave", typeof(bool));
                            dataTable.Columns.Add("Shift", typeof(bool));
                            dataTable.Columns.Add("ReserveField1", typeof(bool));
                            dataTable.Columns.Add("ReserveField2", typeof(bool));
                            dataTable.Columns.Add("ReserveField3", typeof(bool));
                            dataTable.Columns.Add("ReserveField4", typeof(bool));
                            dataTable.Columns.Add("ReserveField5", typeof(bool));
                            dataTable.Columns.Add("ReserveField6", typeof(bool));
                            dataTable.Columns.Add("ReserveField7", typeof(bool));
                            dataTable.Columns.Add("ReserveField8", typeof(bool));
                            dataTable.Columns.Add("ReserveField9", typeof(bool));
                            dataTable.Columns.Add("ReserveField10", typeof(bool));
                            dataTable.Columns.Add("DayState", typeof(bool));
                            #endregion
                            foreach(decimal userSettingId in NotExistUserSettingList)
                            {                       
                                DataRow dr = dataTable.NewRow();
                                dr["UserSettingId"] = userSettingId;
                                #region "GetPropertyInfo"
                                foreach (PropertyInfo pInfo in typeof(MonthlyOperationGridRoleSettings).GetProperties())
                                {
                                    switch (pInfo.Name)
                                    {
                                        case "DayName":
                                            dr["DayName"] = (bool)pInfo.GetValue(monthlyOperationGridRoleSettings , null);
                                            break;
                                        case "TheDate":
                                            dr["TheDate"] = (bool)pInfo.GetValue(monthlyOperationGridRoleSettings, null);
                                            break;
                                        case "FirstEntrance":
                                            dr["FirstEntrance"] = (bool)pInfo.GetValue(monthlyOperationGridRoleSettings, null);
                                            break;
                                        case "FirstExit":
                                            dr["FirstExit"] = (bool)pInfo.GetValue(monthlyOperationGridRoleSettings, null);
                                            break;
                                        case "SecondEntrance":
                                            dr["SecondEntrance"] = (bool)pInfo.GetValue(monthlyOperationGridRoleSettings, null);
                                            break;
                                        case "SecondExit":
                                            dr["SecondExit"] = (bool)pInfo.GetValue(monthlyOperationGridRoleSettings, null);
                                            break;
                                        case "ThirdEntrance":
                                            dr["ThirdEntrance"] = (bool)pInfo.GetValue(monthlyOperationGridRoleSettings, null);
                                            break;
                                        case "ThirdExit":
                                            dr["ThirdExit"] = (bool)pInfo.GetValue(monthlyOperationGridRoleSettings, null);
                                            break;
                                        case "FourthEntrance":
                                            dr["FourthEntrance"] = (bool)pInfo.GetValue(monthlyOperationGridRoleSettings, null);
                                            break;
                                        case "FourthExit":
                                            dr["FourthExit"] = (bool)pInfo.GetValue(monthlyOperationGridRoleSettings, null);
                                            break;
                                        case "FifthEntrance":
                                            dr["FifthEntrance"] = (bool)pInfo.GetValue(monthlyOperationGridRoleSettings, null);
                                            break;
                                        case "FifthExit":
                                            dr["FifthExit"] = (bool)pInfo.GetValue(monthlyOperationGridRoleSettings, null);
                                            break;
                                        case "LastExit":
                                            dr["LastExit"] = (bool)pInfo.GetValue(monthlyOperationGridRoleSettings, null);
                                            break;
                                        case "NecessaryOperation":
                                            dr["NecessaryOperation"] = (bool)pInfo.GetValue(monthlyOperationGridRoleSettings, null);
                                            break;
                                        case "PresenceDuration":
                                            dr["PresenceDuration"] = (bool)pInfo.GetValue(monthlyOperationGridRoleSettings, null);
                                            break;
                                        case "HourlyPureOperation":
                                            dr["HourlyPureOperation"] = (bool)pInfo.GetValue(monthlyOperationGridRoleSettings, null);
                                            break;
                                        case "DailyPureOperation":
                                            dr["DailyPureOperation"] = (bool)pInfo.GetValue(monthlyOperationGridRoleSettings, null);
                                            break;
                                        case "ImpureOperation":
                                            dr["ImpureOperation"] = (bool)pInfo.GetValue(monthlyOperationGridRoleSettings, null);
                                            break;
                                        case "AllowableOverTime":
                                            dr["AllowableOverTime"] = (bool)pInfo.GetValue(monthlyOperationGridRoleSettings, null);
                                            break;
                                        case "UnallowableOverTime":
                                            dr["UnallowableOverTime"] = (bool)pInfo.GetValue(monthlyOperationGridRoleSettings, null);
                                            break;
                                        case "HourlyAllowableAbsence":
                                            dr["HourlyAllowableAbsence"] = (bool)pInfo.GetValue(monthlyOperationGridRoleSettings, null);
                                            break;
                                        case "HourlyUnallowableAbsence":
                                            dr["HourlyUnallowableAbsence"] = (bool)pInfo.GetValue(monthlyOperationGridRoleSettings, null);
                                            break;
                                        case "DailyAbsence":
                                            dr["DailyAbsence"] = (bool)pInfo.GetValue(monthlyOperationGridRoleSettings, null);
                                            break;
                                        case "HourlyMission":
                                            dr["HourlyMission"] = (bool)pInfo.GetValue(monthlyOperationGridRoleSettings, null);
                                            break;
                                        case "DailyMission":
                                            dr["DailyMission"] = (bool)pInfo.GetValue(monthlyOperationGridRoleSettings, null);
                                            break;
                                        case "HostelryMission":
                                            dr["HostelryMission"] = (bool)pInfo.GetValue(monthlyOperationGridRoleSettings, null);
                                            break;
                                        case "HourlySickLeave":
                                            dr["HourlySickLeave"] = (bool)pInfo.GetValue(monthlyOperationGridRoleSettings, null);
                                            break;
                                        case "DailySickLeave":
                                            dr["DailySickLeave"] = (bool)pInfo.GetValue(monthlyOperationGridRoleSettings, null);
                                            break;
                                        case "HourlyMeritoriouslyLeave":
                                            dr["HourlyMeritoriouslyLeave"] = (bool)pInfo.GetValue(monthlyOperationGridRoleSettings, null);
                                            break;
                                        case "DailyMeritoriouslyLeave":
                                            dr["DailyMeritoriouslyLeave"] = (bool)pInfo.GetValue(monthlyOperationGridRoleSettings, null);
                                            break;
                                        case "HourlyWithoutPayLeave":
                                            dr["HourlyWithoutPayLeave"] = (bool)pInfo.GetValue(monthlyOperationGridRoleSettings, null);
                                            break;
                                        case "DailyWithoutPayLeave":
                                            dr["DailyWithoutPayLeave"] = (bool)pInfo.GetValue(monthlyOperationGridRoleSettings, null);
                                            break;
                                        case "HourlyWithPayLeave":
                                            dr["HourlyWithPayLeave"] = (bool)pInfo.GetValue(monthlyOperationGridRoleSettings, null);
                                            break;
                                        case "DailyWithPayLeave":
                                            dr["DailyWithPayLeave"] = (bool)pInfo.GetValue(monthlyOperationGridRoleSettings, null);
                                            break;
                                        case "Shift":
                                            dr["Shift"] = (bool)pInfo.GetValue(monthlyOperationGridRoleSettings, null);
                                            break;
                                        case "ReserveField1":
                                            dr["ReserveField1"] = (bool)pInfo.GetValue(monthlyOperationGridRoleSettings, null);
                                            break;
                                        case "ReserveField2":
                                            dr["ReserveField2"] = (bool)pInfo.GetValue(monthlyOperationGridRoleSettings, null);
                                            break;
                                        case "ReserveField3":
                                            dr["ReserveField3"] = (bool)pInfo.GetValue(monthlyOperationGridRoleSettings, null);
                                            break;
                                        case "ReserveField4":
                                            dr["ReserveField4"] = (bool)pInfo.GetValue(monthlyOperationGridRoleSettings, null);
                                            break;
                                        case "ReserveField5":
                                            dr["ReserveField5"] = (bool)pInfo.GetValue(monthlyOperationGridRoleSettings, null);
                                            break;
                                        case "ReserveField6":
                                            dr["ReserveField6"] = (bool)pInfo.GetValue(monthlyOperationGridRoleSettings, null);
                                            break;
                                        case "ReserveField7":
                                            dr["ReserveField7"] = (bool)pInfo.GetValue(monthlyOperationGridRoleSettings, null);
                                            break;
                                        case "ReserveField8":
                                            dr["ReserveField8"] = (bool)pInfo.GetValue(monthlyOperationGridRoleSettings, null);
                                            break;
                                        case "ReserveField9":
                                            dr["ReserveField9"] = (bool)pInfo.GetValue(monthlyOperationGridRoleSettings, null);
                                            break;
                                        case "ReserveField10":
                                            dr["ReserveField10"] = (bool)pInfo.GetValue(monthlyOperationGridRoleSettings, null);
                                            break;
                                        case "DayState":
                                            dr["DayState"] = (bool)pInfo.GetValue(monthlyOperationGridRoleSettings, null);
                                            break;
                                        default:
                                            break;                                            
                                    }                                   
                                }
                                #endregion
                                dataTable.Rows.Add(dr);
                            }
                            #region "Mapping"
                            using (NHibernateSessionManager.Instance.BeginTransactionOn())
                            {
                                try
                                {



                                    SqlBulkCopy TempBulkInsert = new System.Data.SqlClient.SqlBulkCopy((SqlConnection)NHibernateSessionManager.Instance.GetSession().Connection, SqlBulkCopyOptions.Default, (SqlTransaction)NHibernateSessionManager.Instance.GetTransaction().GetDbTransaction);
                                    TempBulkInsert.DestinationTableName = "dbo.TA_GridMonthlyOperationGridClientSettings";
                                    TempBulkInsert.ColumnMappings.Add("UserSettingId", "UserSettingId");
                                    TempBulkInsert.ColumnMappings.Add("DayName", "DayName");
                                    TempBulkInsert.ColumnMappings.Add("TheDate", "TheDate");
                                    TempBulkInsert.ColumnMappings.Add("FirstEntrance", "FirstEntrance");
                                    TempBulkInsert.ColumnMappings.Add("FirstExit", "FirstExit");
                                    TempBulkInsert.ColumnMappings.Add("SecondEntrance", "SecondEntrance");
                                    TempBulkInsert.ColumnMappings.Add("SecondExit", "SecondExit");
                                    TempBulkInsert.ColumnMappings.Add("ThirdEntrance", "ThirdEntrance");
                                    TempBulkInsert.ColumnMappings.Add("ThirdExit", "ThirdExit");
                                    TempBulkInsert.ColumnMappings.Add("FourthEntrance", "FourthEntrance");
                                    TempBulkInsert.ColumnMappings.Add("FourthExit", "FourthExit");
                                    TempBulkInsert.ColumnMappings.Add("FifthEntrance", "FifthEntrance");
                                    TempBulkInsert.ColumnMappings.Add("FifthExit", "FifthExit");
                                    TempBulkInsert.ColumnMappings.Add("LastExit", "LastExit");
                                    TempBulkInsert.ColumnMappings.Add("NecessaryOperation", "NecessaryOperation");
                                    TempBulkInsert.ColumnMappings.Add("PresenceDuration", "PresenceDuration");
                                    TempBulkInsert.ColumnMappings.Add("HourlyPureOperation", "HourlyPureOperation");
                                    TempBulkInsert.ColumnMappings.Add("DailyPureOperation", "DailyPureOperation");
                                    TempBulkInsert.ColumnMappings.Add("ImpureOperation", "ImpureOperation");
                                    TempBulkInsert.ColumnMappings.Add("AllowableOverTime", "AllowableOverTime");
                                    TempBulkInsert.ColumnMappings.Add("UnallowableOverTime", "UnallowableOverTime");
                                    TempBulkInsert.ColumnMappings.Add("HourlyAllowableAbsence", "HourlyAllowableAbsence");
                                    TempBulkInsert.ColumnMappings.Add("HourlyUnallowableAbsence", "HourlyUnallowableAbsence");
                                    TempBulkInsert.ColumnMappings.Add("DailyAbsence", "DailyAbsence");
                                    TempBulkInsert.ColumnMappings.Add("HourlyMission", "HourlyMission");
                                    TempBulkInsert.ColumnMappings.Add("DailyMission", "DailyMission");
                                    TempBulkInsert.ColumnMappings.Add("HostelryMission", "HostelryMission");
                                    TempBulkInsert.ColumnMappings.Add("HourlySickLeave", "HourlySickLeave");
                                    TempBulkInsert.ColumnMappings.Add("DailySickLeave", "DailySickLeave");
                                    TempBulkInsert.ColumnMappings.Add("HourlyMeritoriouslyLeave", "HourlyMeritoriouslyLeave");
                                    TempBulkInsert.ColumnMappings.Add("DailyMeritoriouslyLeave", "DailyMeritoriouslyLeave");
                                    TempBulkInsert.ColumnMappings.Add("HourlyWithoutPayLeave", "HourlyWithoutPayLeave");
                                    TempBulkInsert.ColumnMappings.Add("DailyWithoutPayLeave", "DailyWithoutPayLeave");
                                    TempBulkInsert.ColumnMappings.Add("HourlyWithPayLeave", "HourlyWithPayLeave");
                                    TempBulkInsert.ColumnMappings.Add("DailyWithPayLeave", "DailyWithPayLeave");
                                    TempBulkInsert.ColumnMappings.Add("Shift", "Shift");
                                    TempBulkInsert.ColumnMappings.Add("ReserveField1", "ReserveField1");
                                    TempBulkInsert.ColumnMappings.Add("ReserveField2", "ReserveField2");
                                    TempBulkInsert.ColumnMappings.Add("ReserveField3", "ReserveField3");
                                    TempBulkInsert.ColumnMappings.Add("ReserveField4", "ReserveField4");
                                    TempBulkInsert.ColumnMappings.Add("ReserveField5", "ReserveField5");
                                    TempBulkInsert.ColumnMappings.Add("ReserveField6", "ReserveField6");
                                    TempBulkInsert.ColumnMappings.Add("ReserveField7", "ReserveField7");
                                    TempBulkInsert.ColumnMappings.Add("ReserveField8", "ReserveField8");
                                    TempBulkInsert.ColumnMappings.Add("ReserveField9", "ReserveField9");
                                    TempBulkInsert.ColumnMappings.Add("ReserveField10", "ReserveField10");
                                    TempBulkInsert.WriteToServer(dataTable);   
                                    NHibernateSessionManager.Instance.CommitTransactionOn();
                                }

                                catch (Exception ex)
                                {
                                    NHibernateSessionManager.Instance.RollbackTransactionOn();
                                    //LogException(ex, "BRole", "UpdateGridMonthlyOperationGridRoleSettings");
                                    throw ex;
                                }
                            }
                            #endregion
                          //  TempBulkInsert.WriteToServer(dataTable);                                              
                    }
                    #endregion                   
            }
            catch(Exception ex)
            {               
                LogException(ex, "BGridMonthlyOperationClientSettings", "UpdateMonthlyOperationGridClientSettings");
                throw ex;
            }                                                                                     
        }
         
        /// <summary>
        /// تنظیمات را برمیگرداند
        /// اگر موجود نباشد ایجاد میکند
        /// </summary>
        /// <returns>تنظیمات</returns>
        public MonthlyOperationGridClientSettings GetMonthlyOperationGridClientSettings()
        {
            using (NHibernateSessionManager.Instance.BeginTransactionOn())
            {
                MonthlyOperationGridClientSettings Result = null;
                try
                {
                    if (ValidateUser())
                    {
                        EntityRepository<MonthlyOperationGridClientSettings> rep = new EntityRepository<MonthlyOperationGridClientSettings>(false);
                        IList<MonthlyOperationGridClientSettings> list = rep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new MonthlyOperationGridClientSettings().UserSetting), new UserSettings() { ID = workingUserSettingsId }));
                        if (list != null && list.Count > 0)
                        {
                            Result = list[0];
                        }
                        else//insert record
                        {
                            MonthlyOperationGridClientSettings settings = new MonthlyOperationGridClientSettings() { AllowableOverTime = true, DailyAbsence = true, DailyMeritoriouslyLeave = true, DailyMission = true, DailyPureOperation = true, DailySickLeave = true, DailyWithoutPayLeave = true, DailyWithPayLeave = true, TheDate = true, DayName = true, FifthEntrance = false, FifthExit = false, FirstEntrance = true, FirstExit = true, FourthEntrance = false, FourthExit = false, HostelryMission = true, HourlyAllowableAbsence = true, HourlyMeritoriouslyLeave = true, HourlyMission = true, HourlyPureOperation = true, HourlySickLeave = true, HourlyUnallowableAbsence = true, HourlyWithoutPayLeave = true, HourlyWithPayLeave = true, ImpureOperation = true, LastExit = true, NecessaryOperation = true, PresenceDuration = true, ReserveField1 = false, ReserveField10 = false, ReserveField2 = false, ReserveField3 = false, ReserveField4 = false, ReserveField5 = false, ReserveField6 = false, ReserveField7 = false, ReserveField8 = false, ReserveField9 = false, SecondEntrance = true, SecondExit = true, Shift = true, ThirdEntrance = false, ThirdExit = false, UnallowableOverTime = true };
                            settings.UserSetting = new UserSettings() { ID = workingUserSettingsId };
                            base.Insert(settings);
                            Result = settings;
                        }
                    }
                    else
                    {
                        throw new IllegalServiceAccess("کاربر یا تنظیمات کاربر در دیتابیس موجود نیست", ExceptionSrc);
                    }
                    NHibernateSessionManager.Instance.CommitTransactionOn();
                    return Result;
                }
                catch (Exception ex)
                {
                    NHibernateSessionManager.Instance.RollbackTransactionOn();
                    LogException(ex, "BGridMonthlyOperationClientSettings", "GetMonthlyOperationGridClientSettings");
                    throw ex;
                }
            }
        }

        /// <summary>
        /// تنظیمات مربوط به اندازه ستونهای گرید را برمیگرداند        
        /// </summary>
        /// <returns>تنظیمات</returns>
        public MonthlyOperationGridClientGeneralSettings GetMonthlyOperationGridGeneralClientSettings()
        {
            try
            {
                if (ValidateLanguage())
                {
                    EntityRepository<MonthlyOperationGridClientGeneralSettings> rep = new EntityRepository<MonthlyOperationGridClientGeneralSettings>(false);
                    IList<MonthlyOperationGridClientGeneralSettings> list = rep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new MonthlyOperationGridClientGeneralSettings().Language), new Languages() { ID = workingLanguageIdId }));
                    if (list != null && list.Count > 0)
                    {
                        return list[0];
                    }
                }
                else
                {
                    throw new IllegalServiceAccess("کاربر یا تنظیمات کاربر در دیتابیس موجود نیست", ExceptionSrc);
                }
                return null;
            }
            catch (Exception ex)
            {
                LogException(ex, "BGridMonthlyOperationClientSettings", "GetMonthlyOperationGridGeneralClientSettings");
                throw ex;
            }
        }

        /// <summary>
        /// اعتبارسنجی عملیات درج
        /// </summary>
        /// <param name="obj">تنظیمات</param>
        protected override void InsertValidate(MonthlyOperationGridClientSettings obj)
        {
            throw new IllegalServiceAccess("دسترسی به این سرویس مجاز نیمباشد", ExceptionSrc);
        }

        /// <summary>
        /// اعتبارسنجی عملیات ویرایش 
        /// </summary>
        /// <param name="clientSettings">تنظیمات</param>
        protected override void UpdateValidate(MonthlyOperationGridClientSettings clientSettings)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            if (!ValidateUser())
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.MonthlyOpCurentUserIsNotValid, " کاربر فعلی سیستم نامعتبر است", ExceptionSrc));
            }
            if (clientSettings.ID == 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.MonthlyOpIDMustSpecified, " شناسه تنظیمات باید مشخص شود", ExceptionSrc));
            }
            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        /// <summary>
        /// اعتبارسنجی عملیات حذف
        /// </summary>
        /// <param name="obj">تنظیمات</param>
        protected override void DeleteValidate(MonthlyOperationGridClientSettings obj)
        {
            throw new IllegalServiceAccess("دسترسی به این سرویس مجاز نیمباشد", ExceptionSrc);
        }

        /// <summary>
        /// آماده سازی تنظیمات جهت درج در دیتابیس
        /// </summary>
        /// <param name="clientSettings">تنظیمات</param>
        /// <param name="action">نوع عملیات</param>
        protected override void GetReadyBeforeSave(MonthlyOperationGridClientSettings clientSettings, UIActionType action)
        {
            if (ValidateUser() && action == UIActionType.EDIT)
            {
                clientSettings.UserSetting = new UserSettings() { ID = this.workingUserSettingsId };
            }
        }

        /// <summary>
        /// عملیات درج در دیتابیس
        /// </summary>
        /// <param name="obj">تنظیمات</param>
        protected override void Insert(MonthlyOperationGridClientSettings obj)
        {
            rep.WithoutTransactSave(obj);
        }

        /// <summary>
        /// اگر نام کاربری وجود نداشته باشد یا رکورد تنظیمات کاربر در دیتابیس  موجود نباشد غلط برمیگرداند
        /// </summary>
        /// <returns>بلی/خیر</returns>
        private bool ValidateUser()
        {
            if (this.workingUserSettingsId > 0)
                return true;
            if (Utility.IsEmpty(this.UserName))
            {
                User user = userRep.GetById(BUser.CurrentUser.ID, false);
              
                if (user != null && user.UserSetting != null && user.UserSetting.ID > 0)
                {
                    this.UserName = user.UserName;
                    this.workingUserSettingsId = user.UserSetting.ID;
                }
            }
            else
            {
                User user = userRep.GetByUserName(this.UserName);
                if (user != null && user.UserSetting != null && user.UserSetting.ID > 0)
                {
                    this.workingUserSettingsId = user.UserSetting.ID;
                }
            }
            if (this.workingUserSettingsId > 0)
            {
                NHibernateSessionManager.Instance.ClearSession();
                return true;
            }
            return false;

        }

        /// <summary>
        /// زبان انتخابی کاربر را اعتبارستجی میکند
        /// </summary>
        /// <returns>بلی/خیر</returns>
        private bool ValidateLanguage()
        {
            if (this.workingLanguageIdId > 0)
                return true;
            if (Utility.IsEmpty(this.UserName))
            {
                this.UserName = Security.BUser.CurrentUser.UserName;
                AppSettings.BLanguage blang = new GTS.Clock.Business.AppSettings.BLanguage();
                Languages lang = blang.GetLanguageByUsername(this.UserName);
                if (lang.ID > 0)
                {
                    this.workingLanguageIdId = lang.ID;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                AppSettings.BLanguage blang = new GTS.Clock.Business.AppSettings.BLanguage();
                Languages lang = blang.GetLanguageByUsername(this.UserName);
                if (lang.ID > 0)
                {
                    this.workingLanguageIdId = lang.ID;
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// اعمال تغییرات تنظیمات در دیتبایس برای پرسنل 
        /// </summary>
        /// <param name="monthlyOperationGridClientSettings">تنظمیات</param>
        /// <param name="UAT">نوع عملیات</param>
        /// <returns>کلید اصلی تنظیمات</returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal SaveChanges_onPersonnelState(MonthlyOperationGridClientSettings monthlyOperationGridClientSettings, UIActionType UAT)
        {
            return base.SaveChanges(monthlyOperationGridClientSettings, UAT);
        }

        /// <summary>
        /// اعمال تغییرات تنظیمات در دیتبایس برای مدیر  
        /// </summary>
        /// <param name="monthlyOperationGridClientSettings">تنظمیات</param>
        /// <param name="UAT">نوع عملیات</param>
        /// <returns>کلید اصلی تنظیمات</returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal SaveChanges_onManagerState(MonthlyOperationGridClientSettings monthlyOperationGridClientSettings, UIActionType UAT)
        {
            return base.SaveChanges(monthlyOperationGridClientSettings, UAT);
        }

    }
}
