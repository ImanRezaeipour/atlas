using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.Log;
using GTS.Clock.Model;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Business.Security;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Infrastructure.Validation.Configuration;
using GTS.Clock.Infrastructure.Repository.Leave;
using GTS.Clock.Business.Temp;
using NHibernate;
using GTS.Clock.Model.Contracts;

namespace GTS.Clock.Business.Leave
{
    /// <summary>
    /// مانده مرخصی
    /// created at: 2012-01-29 5:18:22 PM
    /// by        : Farhad Salavati
    /// write your name here
    /// </summary>
    public class BRemainLeave : BaseBusiness<LeaveYearRemain>, ILeaveInfo
    {
        private const string ExceptionSrc = "GTS.Clock.Business.Leave.BRemainLeave";
        private EntityRepository<LeaveYearRemain> objectRep = new EntityRepository<LeaveYearRemain>();
        private LeaveYearRemainRepository LeaveYearRemainRep = new LeaveYearRemainRepository();
        IDataAccess dataAccessPort = new BUser();
        private ISession NHSession = NHibernateSessionManager.Instance.GetSession();
        int operationBatchSizeValue = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings[OperationBatchSize.OperationBatchSizeKey.ToString()]);
        private BTemp bTemp = new BTemp();
        int minutesInDay = 8 * 60;

        #region BaseBusiness Implementation

        /// <summary>
        /// اعتبارسنجی عملیات درج
        /// </summary>
        /// <param name="obj">مانده مرخصی سالانه</param>
        protected override void InsertValidate(LeaveYearRemain obj)
        {
            UIValidationExceptions exception = new UIValidationExceptions();


            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        /// <summary>
        /// اعتبارسنجی عملیات ویرایش
        /// </summary>
        /// <param name="obj">مانده مرخصی سالانه</param>
        protected override void UpdateValidate(LeaveYearRemain obj)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            // throw new IllegalServiceAccess("دسترسی به سرویس بروزرسانی مانده مرخصی غیر مجاز است",ExceptionSrc);

            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        /// <summary>
        /// اعتبارسنجی عملیات حذف
        /// </summary>
        /// <param name="obj">مانده مرخصی سالانه</param>
        protected override void DeleteValidate(LeaveYearRemain obj)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            //throw new NotImplementedException();

            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        /// <summary>
        /// اعتبارسنجی قوانین واسط کاربری
        /// </summary>
        /// <param name="obj">مانده مرخصی سالانه</param>
        /// <param name="action">نوع عملیات</param>
        protected override void UIValidate(LeaveYearRemain obj, UIActionType action)
        {
            UIValidator.DoValidate(obj);
        }

        /// <summary>
        /// بروزرسانی نشانه تغییرات
        /// </summary>
        /// <param name="obj">مانده مرخصی</param>
        /// <param name="action">نوع عملیات</param>
        protected override void UpdateCFP(LeaveYearRemain obj, UIActionType action)
        {
            base.UpdateCFP(obj.Person.ID, obj.Date);
        }

        #endregion

        #region GetAll

        /// <summary>
        /// مانده مرخصی را برای سالهای مشخص شده برای همه پرسنل را به صورت صفحه بندی شده برمیگرداند 
        /// </summary>
        /// <param name="fromYear">سال ابتدا</param>
        /// <param name="toYear">سال انتها</param>
        /// <param name="pageIndex">شماره صفحه</param>
        /// <param name="pageSize">تعداد رکورد های هر صفحه</param>
        /// <returns>لیست پروکسی مانده مرخصی</returns>
        public IList<RemainLeaveProxy> GetRemainLeave(int fromYear, int toYear, int pageIndex, int pageSize)
        {
            try
            {
                IList<LeaveYearRemain> list = new List<LeaveYearRemain>();

                DateTime fromDate, toDate;
                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    fromDate = Utility.ToMildiDate(String.Format("{0}/01/01", fromYear));
                    toDate = Utility.ToMildiDate(String.Format("{0}/01/01", toYear));
                }
                else
                {
                    fromDate = new DateTime(fromYear, 1, 1);
                    toDate = new DateTime(toYear, 1, 1);
                }

                list = LeaveYearRemainRep.GetAllLeaveYearRemain(BUser.CurrentUser.ID, fromDate, toDate, pageIndex, pageSize);

                if (list != null && list.Count > 0)
                {
                    list = list.OrderBy(x => x.Person.LastName).ThenBy(x => x.Date.Year).ToList();

                }
                return this.ConvertToProxy(list);

            }
            catch (Exception ex)
            {
                LogException(ex, "BRemainLeave", "GetRemainLeave");
                throw ex;
            }
        }

        /// <summary>
        /// مانده مرخصی را برای سالهای مشخص شده برای یک پرسنل به صورت صفحه بندی شده برمیگرداند 
        /// </summary>
        /// <param name="personId">کلید اصلی پرسنل</param>
        /// <param name="fromYear">سال ابتدا</param>
        /// <param name="toYear">سال انتها</param>
        /// <param name="pageIndex">شماره صفحه</param>
        /// <param name="pageSize">تعداد رکورد های هر صفحه</param>
        /// <returns>لیست پروکسی مانده مرخصی</returns>
        public IList<RemainLeaveProxy> GetRemainLeave(decimal personId, int fromYear, int toYear, int pageIndex, int pageSize)
        {
            try
            {
                IList<decimal> underManagmentList = new List<decimal>();
                underManagmentList.Add(personId);

                /*
                IList<LeaveYearRemain> list = new List<LeaveYearRemain>();
                if (underManagmentList != null && underManagmentList.Count > 0)
                {
                    DateTime fromDate, toDate;
                    if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                    {
                        fromDate = Utility.ToMildiDate(String.Format("{0}/01/01", fromYear));
                        toDate = Utility.ToMildiDate(String.Format("{0}/01/01", toYear));
                    }
                    else
                    {
                        fromDate = new DateTime(fromYear, 1, 1);
                        toDate = new DateTime(toYear, 1, 1);
                    }
                    list = objectRep.GetByCriteriaByPage(pageIndex, pageSize,
                                                                   new CriteriaStruct(Utility.GetPropertyName(() => new LeaveYearRemain().Date), fromDate, CriteriaOperation.GreaterEqThan),
                                                                   new CriteriaStruct(Utility.GetPropertyName(() => new LeaveYearRemain().Date), toDate, CriteriaOperation.LessEqThan),
                                                                   new CriteriaStruct(Utility.GetPropertyName(() => new LeaveYearRemain().PersonId), underManagmentList.ToArray<decimal>(), CriteriaOperation.IN));

                }
                if (list != null && list.Count > 0)
                {
                    list = list.OrderBy(x => x.Person.LastName).ThenBy(x => x.Date.Year).ToList();

                }
                return this.ConvertToProxy(list);
                 * */

                return this.GetRemainLeave(underManagmentList, fromYear, toYear, pageIndex, pageSize);
            }
            catch (Exception ex)
            {
                LogException(ex, "BRemainLeave", "GetRemainLeave");
                throw ex;
            }
        }

        /// <summary>
        /// مانده مرخصی را برای سالهای مشخص شده برای یک پرسنل برمیگرداند  
        /// </summary>
        /// <param name="personId">کلید اصلی پرسنل</param>
        /// <param name="fromYear">سال ابتدا</param>
        /// <param name="toYear">سال انتها</param>
        /// <returns>لیست پروکسی مانده مرخصی</returns>
        public IList<RemainLeaveProxy> GetRemainLeave(decimal personId, int fromYear, int toYear)
        {
            try
            {
                IList<decimal> underManagmentList = new List<decimal>();
                underManagmentList.Add(personId);

                /*
                IList<LeaveYearRemain> list = new List<LeaveYearRemain>();
                if (underManagmentList != null && underManagmentList.Count > 0)
                {
                    DateTime fromDate, toDate;
                    if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                    {
                        fromDate = Utility.ToMildiDate(String.Format("{0}/01/01", fromYear));
                        toDate = Utility.ToMildiDate(String.Format("{0}/01/01", toYear));
                    }
                    else
                    {
                        fromDate = new DateTime(fromYear, 1, 1);
                        toDate = new DateTime(toYear, 1, 1);
                    }
                    list = objectRep.GetByCriteriaByPage(pageIndex, pageSize,
                                                                   new CriteriaStruct(Utility.GetPropertyName(() => new LeaveYearRemain().Date), fromDate, CriteriaOperation.GreaterEqThan),
                                                                   new CriteriaStruct(Utility.GetPropertyName(() => new LeaveYearRemain().Date), toDate, CriteriaOperation.LessEqThan),
                                                                   new CriteriaStruct(Utility.GetPropertyName(() => new LeaveYearRemain().PersonId), underManagmentList.ToArray<decimal>(), CriteriaOperation.IN));

                }
                if (list != null && list.Count > 0)
                {
                    list = list.OrderBy(x => x.Person.LastName).ThenBy(x => x.Date.Year).ToList();

                }
                return this.ConvertToProxy(list);
                 * */

                return this.GetRemainLeave(underManagmentList, fromYear, toYear);
            }
            catch (Exception ex)
            {
                LogException(ex, "BRemainLeave", "GetRemainLeave");
                throw ex;
            }
        }

        /// <summary>
        /// مانده مرخصی را برای سالها و افراد جستجو شده  برمیگرداند
        /// حد اکثر 1000 رکورد بر میگرداند
        /// </summary>
        /// <param name="proxy">پروکسی جستجوی افراد</param>
        /// <param name="fromDate">تاریخ ابتدا</param>
        /// <param name="toDate">تاریخ انتها</param>
        /// <returns>لیست پروکسی مانده مرخصی</returns>
        public IList<RemainLeaveProxy> GetRemainLeave(PersonAdvanceSearchProxy proxy, int fromYear, int toYear, int pageIndex, int pageSize)
        {
            try
            {
                IList<decimal> underManagmentList = new List<decimal>();
                ISearchPerson searchTool = new BPerson();
                int count = searchTool.GetPersonInAdvanceSearchCount(proxy);
                IList<Person> list = searchTool.GetPersonInAdvanceSearch(proxy, 0, count);
                var l = from p in list
                        select p.ID;
                underManagmentList = l.ToList<decimal>();

                return this.GetRemainLeave(underManagmentList, fromYear, toYear, pageIndex, pageSize);
            }
            catch (Exception ex)
            {
                LogException(ex, "BRemainLeave", "GetRemainLeave");
                throw ex;
            }
        }

        /// <summary>
        /// مانده مرخصی را برای سالها و افراد جستجو شده  برمیگرداند
        /// حد اکثر 1000 رکورد بر میگرداند
        /// </summary>
        /// <param name="quickSearchKey"></param>
        /// <param name="fromDate">تاریخ ابتدا</param>
        /// <param name="toDate">تاریخ انتها</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns>لیست پروکسی مانده مرخصی</returns>
        public IList<RemainLeaveProxy> GetRemainLeave(string quickSearchKey, int fromYear, int toYear, int pageIndex, int pageSize)
        {
            try
            {
                if (Utility.IsEmpty(quickSearchKey))
                {
                    return this.GetRemainLeave(fromYear, toYear, pageIndex, pageSize);
                }

                IList<decimal> underManagmentList = new List<decimal>();
                ISearchPerson searchTool = new BPerson();
                int count = searchTool.GetPersonInQuickSearchCount(quickSearchKey);
                IList<Person> list = searchTool.QuickSearchByPage(0, count, quickSearchKey);
                var l = from p in list
                        select p.ID;
                underManagmentList = l.ToList<decimal>();

                return this.GetRemainLeave(underManagmentList, fromYear, toYear, pageIndex, pageSize);
            }
            catch (Exception ex)
            {
                LogException(ex, "BRemainLeave", "GetRemainLeave");
                throw ex;
            }
        }

        /// <summary>
        /// مانده مرخصی پرسنل تحت مدیریت را در بازه سالی مشخص به صورت صفحه بندی شده برمی گرداند 
        /// </summary>
        /// <param name="underManagmentList">لیست پرسنل تحت مدیریت</param>
        /// <param name="fromYear">سال ابتدا</param>
        /// <param name="toYear">سال انتها</param>
        /// <param name="pageIndex">شماره صفحه</param>
        /// <param name="pageSize">تعداد رکوردهای هر صفحه</param>
        /// <returns>لیست پروکسی مانده مرخصی</returns>
        private IList<RemainLeaveProxy> GetRemainLeave(IList<decimal> underManagmentList, int fromYear, int toYear, int pageIndex, int pageSize)
        {
            try
            {
                IList<LeaveYearRemain> result = new List<LeaveYearRemain>();
                if (underManagmentList != null && underManagmentList.Count > 0)
                {
                    DateTime fromDate, toDate;
                    if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                    {
                        fromDate = Utility.ToMildiDate(String.Format("{0}/01/01", fromYear));
                        toDate = Utility.ToMildiDate(String.Format("{0}/01/01", toYear));
                    }
                    else
                    {
                        fromDate = new DateTime(fromYear, 1, 1);
                        toDate = new DateTime(toYear, 1, 1);
                    }

                    IList<decimal> accessableIDs = underManagmentList;

                    if (accessableIDs.Count < this.operationBatchSizeValue && this.operationBatchSizeValue < 2100)
                    {
                        result = objectRep.GetByCriteriaByPage(pageIndex, pageSize,
                                                                        new CriteriaStruct(Utility.GetPropertyName(() => new LeaveYearRemain().Date), fromDate, CriteriaOperation.GreaterEqThan),
                                                                        new CriteriaStruct(Utility.GetPropertyName(() => new LeaveYearRemain().Date), toDate, CriteriaOperation.LessEqThan),
                                                                        new CriteriaStruct(Utility.GetPropertyName(() => new LeaveYearRemain().PersonId), underManagmentList.ToArray<decimal>(), CriteriaOperation.IN));
                    }
                    else
                    {
                        GTS.Clock.Model.Temp.Temp tempAlias = null;
                        LeaveYearRemain leaveYearRemainAlias = null;
                        Person personAlias = null;
                        string operationGUID = bTemp.InsertTempList(accessableIDs);
                        result = NHSession.QueryOver<LeaveYearRemain>(() => leaveYearRemainAlias)
                                          .JoinAlias(() => leaveYearRemainAlias.Person, () => personAlias)
                                          .JoinAlias(() => personAlias.TempList, () => tempAlias)
                                          .Where(() => tempAlias.OperationGUID == operationGUID && leaveYearRemainAlias.Date >= fromDate && leaveYearRemainAlias.Date <= toDate)
                                          .Skip(pageIndex * pageSize)
                                          .Take(pageSize)
                                          .List<LeaveYearRemain>();
                        bTemp.DeleteTempList(operationGUID);
                    }
                }
                if (result != null && result.Count > 0)
                {
                    result = result.OrderBy(x => x.Person.LastName).ThenBy(x => x.Date.Year).ToList();
                }
                return this.ConvertToProxy(result);
            }
            catch (Exception ex)
            {
                LogException(ex, "BRemainLeave", "GetRemainLeave");
                throw ex;
            }
        }

        /// <summary>
        /// مانده مرخصی پرسنل تحت مدیریت را در بازه سالی مشخص برمی گرداند 
        /// </summary>
        /// <param name="underManagmentList">لیست پرسنل تحت مدیریت</param>
        /// <param name="fromYear">سال ابتدا</param>
        /// <param name="toYear">سال انتها</param>
        /// <returns>لیست پروکسی مانده مرخصی</returns>
        private IList<RemainLeaveProxy> GetRemainLeave(IList<decimal> underManagmentList, int fromYear, int toYear)
        {
            try
            {
                IList<LeaveYearRemain> result = new List<LeaveYearRemain>();
                if (underManagmentList != null && underManagmentList.Count > 0)
                {
                    DateTime fromDate, toDate;
                    if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                    {
                        fromDate = Utility.ToMildiDate(String.Format("{0}/01/01", fromYear));
                        toDate = Utility.ToMildiDate(String.Format("{0}/01/01", toYear));
                    }
                    else
                    {
                        fromDate = new DateTime(fromYear, 1, 1);
                        toDate = new DateTime(toYear, 1, 1);
                    }

                    IList<decimal> accessableIDs = underManagmentList;

                    if (accessableIDs.Count < this.operationBatchSizeValue && this.operationBatchSizeValue < 2100)
                    {
                        result = objectRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new LeaveYearRemain().Date), fromDate, CriteriaOperation.GreaterEqThan),
                                                                        new CriteriaStruct(Utility.GetPropertyName(() => new LeaveYearRemain().Date), toDate, CriteriaOperation.LessEqThan),
                                                                        new CriteriaStruct(Utility.GetPropertyName(() => new LeaveYearRemain().PersonId), underManagmentList.ToArray<decimal>(), CriteriaOperation.IN));
                    }
                    else
                    {
                        GTS.Clock.Model.Temp.Temp tempAlias = null;
                        LeaveYearRemain leaveYearRemainAlias = null;
                        Person personAlias = null;
                        string operationGUID = bTemp.InsertTempList(accessableIDs);
                        result = NHSession.QueryOver<LeaveYearRemain>(() => leaveYearRemainAlias)
                                          .JoinAlias(() => leaveYearRemainAlias.Person, () => personAlias)
                                          .JoinAlias(() => personAlias.TempList, () => tempAlias)
                                          .Where(() => tempAlias.OperationGUID == operationGUID && leaveYearRemainAlias.Date >= fromDate && leaveYearRemainAlias.Date <= toDate)
                                          .List<LeaveYearRemain>();
                        bTemp.DeleteTempList(operationGUID);
                    }
                }
                if (result != null && result.Count > 0)
                {
                    result = result.OrderBy(x => x.Person.LastName).ThenBy(x => x.Date.Year).ToList();
                }
                return this.ConvertToProxy(result);
            }
            catch (Exception ex)
            {
                LogException(ex, "BRemainLeave", "GetRemainLeave");
                throw ex;
            }
        }

        #endregion

        #region Count

        /// <summary>
        /// مانده مرخصی را برای سالهای مشخص شده برمیگرداند
        /// </summary>
        /// <param name="fromYear">سال ابتدا</param>
        /// <param name="toYear">سال انتها</param>
        /// <returns>مانده مرخصی</returns>
        public int GetRemainLeaveCount(int fromYear, int toYear)
        {
            try
            {
                return this.GetRemainLeaveCount("", fromYear, toYear);
            }
            catch (Exception ex)
            {
                LogException(ex, "BRemainLeave", "GetRemainLeaveCount");
                throw ex;
            }
        }

        /// <summary>
        /// مانده مرخصی را برای سالهای مشخص شده برمیگرداند
        /// </summary>
        /// <param name="fromYear">سال ابتدا</param>
        /// <param name="toYear">سال انتها</param>
        /// <returns>مانده مرخصی</returns>
        public int GetRemainLeaveCount(decimal personId, int fromYear, int toYear)
        {
            try
            {
                IList<decimal> underManagmentList = new List<decimal>();
                underManagmentList.Add(personId);

                /*
                if (underManagmentList != null && underManagmentList.Count > 0)
                {
                    DateTime fromDate, toDate;
                    if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                    {
                        fromDate = Utility.ToMildiDate(String.Format("{0}/01/01", fromYear));
                        toDate = Utility.ToMildiDate(String.Format("{0}/01/01", toYear));
                    }
                    else
                    {
                        fromDate = new DateTime(fromYear, 1, 1);
                        toDate = new DateTime(toYear, 1, 1);
                    }
                    count = objectRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new LeaveYearRemain().Date), fromDate, CriteriaOperation.GreaterEqThan),
                                                         new CriteriaStruct(Utility.GetPropertyName(() => new LeaveYearRemain().Date), toDate, CriteriaOperation.LessEqThan),
                                                         new CriteriaStruct(Utility.GetPropertyName(() => new LeaveYearRemain().PersonId), underManagmentList.ToArray<decimal>(), CriteriaOperation.IN));
                }
                return count;
                 * */
                return this.GetRemainLeaveCount(underManagmentList, fromYear, toYear);
            }
            catch (Exception ex)
            {
                LogException(ex, "BRemainLeave", "GetRemainLeaveCount");
                throw ex;
            }
        }

        /// <summary>
        /// مانده مرخصی را برای سالها و افراد جستجو شده  برمیگرداند
        /// حد اکثر 1000 برمیگردد
        /// </summary>
        /// <param name="proxy">پروکسی جستجوی پرسنل</param>
        /// <param name="fromDate">تاریخ ابتدا</param>
        /// <param name="toDate">تاریخ انتها</param>
        /// <returns>مانده مرخصی</returns>
        public int GetRemainLeaveCount(PersonAdvanceSearchProxy proxy, int fromYear, int toYear)
        {
            try
            {
                IList<decimal> underManagmentList = new List<decimal>();
                ISearchPerson searchTool = new BPerson();
                int count = searchTool.GetPersonInAdvanceSearchCount(proxy);
                IList<Person> list = searchTool.GetPersonInAdvanceSearch(proxy, 0, count);
                var l = from p in list
                        select p.ID;
                underManagmentList = l.ToList<decimal>();

                return this.GetRemainLeaveCount(underManagmentList, fromYear, toYear);
            }
            catch (Exception ex)
            {
                LogException(ex, "BRemainLeave", "GetRemainLeaveCount");
                throw ex;
            }
        }

        /// <summary>
        /// مانده مرخصی را برای سالها و افراد جستجو شده  برمیگرداند
        /// </summary>
        /// <param name="quickSearchKey">عبارت جستجوی سریع</param>
        /// <param name="fromDate">تاریخ ابتدا</param>
        /// <param name="toDate">تاریخ انتها</param>
        /// <param name="pageIndex">شماره صفحه</param>
        /// <param name="pageSize">تعداد رکوردهای هر صفحه</param>
        /// <returns>مانده مرخصی</returns>
        public int GetRemainLeaveCount(string quickSearchKey, int fromYear, int toYear)
        {
            try
            {
                IList<decimal> underManagmentList = new List<decimal>();
                ISearchPerson searchTool = new BPerson();
                int count = searchTool.GetPersonInQuickSearchCount(quickSearchKey);
                IList<Person> list = searchTool.QuickSearchByPage(0, count, quickSearchKey);
                var l = from p in list
                        select p.ID;
                underManagmentList = l.ToList<decimal>();

                return this.GetRemainLeaveCount(underManagmentList, fromYear, toYear);
            }
            catch (Exception ex)
            {
                LogException(ex, "BRemainLeave", "GetRemainLeaveCount");
                throw ex;
            }
        }

        /// <summary>
        /// مانده مرخصی را بر می گرداند
        /// </summary>
        /// <param name="underManagmentList">لیست پرسنل تحت مدیریت</param>
        /// <param name="fromYear">سال ابتدا</param>
        /// <param name="toYear">سال انتها</param>
        /// <returns>مانده مرخصی</returns>
        private int GetRemainLeaveCount(IList<decimal> underManagmentList, int fromYear, int toYear)
        {
            try
            {
                int count = 0;
                if (underManagmentList != null && underManagmentList.Count > 0)
                {
                    DateTime fromDate, toDate;
                    if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                    {
                        fromDate = Utility.ToMildiDate(String.Format("{0}/01/01", fromYear));
                        toDate = Utility.ToMildiDate(String.Format("{0}/01/01", toYear));
                    }
                    else
                    {
                        fromDate = new DateTime(fromYear, 1, 1);
                        toDate = new DateTime(toYear, 1, 1);
                    }
                    IList<decimal> accessableIDs = underManagmentList;

                    if (accessableIDs.Count < this.operationBatchSizeValue && this.operationBatchSizeValue < 2100)
                    {
                        count = objectRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new LeaveYearRemain().Date), fromDate, CriteriaOperation.GreaterEqThan),
                                                             new CriteriaStruct(Utility.GetPropertyName(() => new LeaveYearRemain().Date), toDate, CriteriaOperation.LessEqThan),
                                                             new CriteriaStruct(Utility.GetPropertyName(() => new LeaveYearRemain().PersonId), underManagmentList.ToArray<decimal>(), CriteriaOperation.IN));
                    }
                    else
                    {
                        GTS.Clock.Model.Temp.Temp tempAlias = null;
                        LeaveYearRemain leaveYearRemainAlias = null;
                        Person personAlias = null;
                        string operationGUID = bTemp.InsertTempList(accessableIDs);
                        count = NHSession.QueryOver<LeaveYearRemain>(() => leaveYearRemainAlias)
                                        .JoinAlias(() => leaveYearRemainAlias.Person, () => personAlias)
                                        .JoinAlias(() => personAlias.TempList, () => tempAlias)
                                        .Where(() => tempAlias.OperationGUID == operationGUID && leaveYearRemainAlias.Date >= fromDate && leaveYearRemainAlias.Date <= toDate)
                                        .RowCount();
                        bTemp.DeleteTempList(operationGUID);
                    }
                }
                return count;
            }
            catch (Exception ex)
            {
                LogException(ex, "BRemainLeave", "GetRemainLeaveCount");
                throw ex;
            }
        }

        #endregion

        /// <summary>
        /// عملیات درج مانده مرخصی سالانه در دیتابیس
        /// </summary>
        /// <param name="year">سال</param>
        /// <param name="personId">کلید اصلی </param>
        /// <param name="dayOK">مقدار روز</param>
        /// <param name="hourOK">مقدار ساعت</param>
        /// <returns>کلید اصلی مانده مرخصی</returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertLeaveYear(int year, decimal personId, string dayOK, string hourOK)
        {
            try
            {
                UIValidationExceptions exception = new UIValidationExceptions();
                if (personId > 0)
                {
                    int count = this.GetRemainLeaveCount(personId, year, year);
                    if (count == 0)
                    {
                        LeaveYearRemain leaveyear = new LeaveYearRemain();
                        if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                        {
                            leaveyear.Date = Utility.ToMildiDate(String.Format("{0}/01/01", year));
                        }
                        else
                        {
                            leaveyear.Date = new DateTime(year, 1, 1);
                        }
                        leaveyear.Person = new Person() { ID = personId };

                        leaveyear.DayRemainOK = Utility.ToInteger(dayOK);
                        leaveyear.MinuteRemainOK = Utility.RealTimeToIntTime(hourOK);
                        leaveyear.DayRemainOKOrginal = Utility.ToInteger(dayOK);
                        leaveyear.MinuteRemainOKOrginal = Utility.RealTimeToIntTime(hourOK);
                        this.SaveChanges(leaveyear, UIActionType.ADD);
                        return leaveyear.ID;
                    }
                    else
                    {
                        exception.Add(new ValidationException(ExceptionResourceKeys.RemainLeaveExists, "طلب سالانه مرخصی برای این شخص و سال مشخص شده موجود میباشد ", ExceptionSrc));
                        throw exception;
                    }
                }
                else
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.RemainLeavePersonNotSelected, "پرسنلی برای انجام عملیات انتخاب نشده است ", ExceptionSrc));
                    throw exception;
                }
            }
            catch (Exception ex)
            {
                LogException(ex, "BRemainLeave", "InsertLeaveYear");
                throw ex;
            }
        }

        /// <summary>
        /// علمیات ویرایش مانده مرخصی سالانه در دیتابیس
        /// </summary>
        /// <param name="remainLeaveId">کلید اصلی مانده مرخصی</param>
        /// <param name="dayOK">مقدار روز</param>
        /// <param name="hourOK">مقدار ساعت</param>
        /// <returns>کلید اصلی مانده مرخصی</returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal UpdateLeaveYear(decimal remainLeaveId, string dayOK, string hourOK)
        {
            try
            {
                LeaveYearRemain leaveYear = this.GetByID(remainLeaveId);
                if (leaveYear.DayRemainOKOrginal == 0)
                    leaveYear.DayRemainOKOrginal = leaveYear.DayRemainOK;
                if (leaveYear.MinuteRemainOKOrginal == 0)
                    leaveYear.MinuteRemainOKOrginal = leaveYear.MinuteRemainOK;
                leaveYear.DayRemainOK = Utility.ToInteger(dayOK);
                leaveYear.MinuteRemainOK = Utility.RealTimeToIntTime(hourOK);

                this.SaveChanges(leaveYear, UIActionType.EDIT);
                return leaveYear.ID;
            }
            catch (Exception ex)
            {
                LogException(ex, "BRemainLeave", "UpdateLeaveYear");
                throw ex;
            }
        }

        #region Transfer To Next Year

        /// <summary>
        /// اتقال مرخصی به سال بعد
        /// </summary>
        /// <param name="personId">کلید اصلی پرسنل</param>
        /// <param name="fromYear">از سال</param>
        /// <param name="toYear">تا سال</param>
        /// <returns>کلید اصلی مانده مرخصی</returns>
        public decimal TransferToNextYear(decimal personId, int fromYear, int toYear)
        {
            try
            {
                DateTime fromDate, toDate;
                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    fromDate = Utility.ToMildiDate(String.Format("{0}/01/01", fromYear));
                    toDate = Utility.ToMildiDate(String.Format("{0}/01/01", toYear));
                }
                else
                {
                    fromDate = new DateTime(fromYear, 1, 1);
                    toDate = new DateTime(toYear, 1, 1);
                }
                return this.TransferToNextYear(personId, fromDate, toDate, fromYear, toYear);
            }
            catch (Exception ex)
            {
                LogException(ex, "BRemainLeave", "TransferToNextYear");
                throw ex;
            }
        }

        /// <summary>
        ///  اتقال مرخصی به سال بعد
        /// </summary>
        /// <param name="proxy">پروکسی جستجوی پرسنل</param>
        /// <param name="fromYear">سال مبدا</param>
        /// <param name="toYear">سال مقصد</param>
        /// <returns>تعداد پرسنلی که انتقال مرخصی آنها انجام شده است</returns>
        public int TransferToNextYear(PersonAdvanceSearchProxy proxy, int fromYear, int toYear)
        {
            try
            {
                DateTime fromDate, toDate;
                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    fromDate = Utility.ToMildiDate(String.Format("{0}/01/01", fromYear));
                    toDate = Utility.ToMildiDate(String.Format("{0}/01/01", toYear));
                }
                else
                {
                    fromDate = new DateTime(fromYear, 1, 1);
                    toDate = new DateTime(toYear, 1, 1);
                }

                IList<decimal> underManagmentList = new List<decimal>();
                ISearchPerson searchTool = new BPerson();
                int count = searchTool.GetPersonInAdvanceSearchCount(proxy);
                IList<Person> list = searchTool.GetPersonInAdvanceSearch(proxy, 0, count);
                var l = from p in list
                        select p.ID;
                underManagmentList = l.ToList<decimal>();
                int counter = 0;
                foreach (decimal personId in underManagmentList)
                {
                    try
                    {
                        this.TransferToNextYear(personId, fromDate, toDate, fromYear, toYear);
                        counter++;
                    }
                    catch (UIValidationExceptions ex)
                    {
                        LogException(ex);
                    }
                }
                return counter;
            }
            catch (Exception ex)
            {
                LogException(ex, "BRemainLeave", "TransferToNextYear");
                throw ex;
            }
        }

        /// <summary>
        ///  اتقال مرخصی به سال بعد
        /// </summary>
        /// <param name="searchKey">عبارت جستجوی پرسنل</param>
        /// <param name="fromYear">سال مبدا</param>
        /// <param name="toYear">سال مقصد</param>
        /// <returns>تعداد پرسنلی که انتقال مرخصی آنها انجام شده است</returns>
        public int TransferToNextYear(string searchKey, int fromYear, int toYear)
        {
            DateTime fromDate, toDate;
            if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
            {
                fromDate = Utility.ToMildiDate(String.Format("{0}/01/01", fromYear));
                toDate = Utility.ToMildiDate(String.Format("{0}/01/01", toYear));
            }
            else
            {
                fromDate = new DateTime(fromYear, 1, 1);
                toDate = new DateTime(toYear, 1, 1);
            }

            IList<decimal> underManagmentList = new List<decimal>();
            ISearchPerson searchTool = new BPerson();
            int count = searchTool.GetPersonInQuickSearchCount(searchKey);
            IList<Person> list = searchTool.QuickSearchByPage(0, count, searchKey);
            var l = from p in list
                    select p.ID;
            underManagmentList = l.ToList<decimal>();
            int counter = 0;
            foreach (decimal personId in underManagmentList)
            {
                try
                {
                    this.TransferToNextYear(personId, fromDate, toDate, fromYear, toYear);
                    counter++;
                }
                catch (UIValidationExceptions ex)
                {
                    LogException(ex);
                }
            }
            return counter;
        }

        ///// <summary>
        ///// انتقال مانده مرخصی
        ///// حتما باید قوانین 82 و 3017 در شروع سال جدید فعال و دارای پارامتر باشد 
        ///// توجه شود که قبل از فراخوانی محاسبات باید انجام شده باشد
        ///// </summary>
        ///// <param name="personId"></param>
        ///// <param name="fromDate"></param>
        ///// <param name="toDate"></param>
        ///// <param name="fromYear"></param>
        ///// <param name="toYear"></param>
        ///// <returns></returns>
        //private decimal TransferToNextYear(decimal personId, DateTime fromDate, DateTime toDate, int fromYear, int toYear)
        //{
        //    try
        //    {
        //        DateTime date = DateTime.Now;
        //        if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
        //        {
        //            date = Utility.ToMildiDate(String.Format("{0}/01/01", toYear));
        //        }
        //        else
        //        {
        //            date = new DateTime(toYear, 1, 1);
        //        }


        //        UIValidationExceptions exception = new UIValidationExceptions();
        //        if (toYear - fromYear != 1)
        //        {
        //            exception.Add(new ValidationException(ExceptionResourceKeys.RemainTransferFromToYearDiffrenceMoreThanOne, "اختلاف سال شروع و پایان برای انتقال مانده مرخصی به سال بعد باید یک باشد", ExceptionSrc));
        //        }

        //        int fromYearCount = this.GetRemainLeaveCount(personId, fromYear, fromYear);
        //        LeaveYearRemain toLeaveYear = objectRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new LeaveYearRemain().Date), fromDate),
        //                                                              new CriteriaStruct(Utility.GetPropertyName(() => new LeaveYearRemain().Date), toDate),
        //                                                              new CriteriaStruct(Utility.GetPropertyName(() => new LeaveYearRemain().PersonId), personId)).FirstOrDefault();
        //        if (toLeaveYear == null)
        //        {
        //            toLeaveYear = new LeaveYearRemain();
        //        }
        //        if (fromYearCount == 0)
        //        {
        //            exception.Add(new ValidationException(ExceptionResourceKeys.RemainTransferFromYearIsNotExists, "طلب سالانه مرخصی برای سال شروع مشخص شده موجود نمیباشد ", ExceptionSrc));
        //        }

        //        if (exception.Count > 0)
        //        {
        //            throw exception;
        //        }

        //        Person prs = new BPerson().GetByID(personId);
        //        prs.InitializeForAccessRules(fromDate, toDate);
        //        int maxTransfer = 0;
        //        int leaveInday = 0;

        //        #region Get From Rule Parameters
        //        if (prs.AssignedRuleList != null)
        //        {
        //            IRuleRepository ruleRep = Rule.GetRuleRepository(false);
        //            IList<AssignedRuleParameter> ruleParameterList = ruleRep.GetAssginedRuleParamList(fromDate, toDate);
        //            AssignedRule ar = prs.AssignedRuleList.Where(x => x.FromDate <= date && x.ToDate >= date && x.IdentifierCode == 3009).FirstOrDefault();
        //            if (ar != null)
        //            {
        //                IList<AssignedRuleParameter> asp = ruleParameterList.Where(x => x.RuleId == ar.RuleId && x.FromDate <= date && x.ToDate >= date).ToList();
        //                //IList<AssignedRuleParameter> asp = ar.RuleParameterList.Where(x => x.FromDate <= date && x.ToDate >= date).ToList();                       
        //                if (asp != null)
        //                {
        //                    AssignedRuleParameter firstParam = asp.Where(x => x.Name.ToLower().Equals("first")).FirstOrDefault();

        //                    if (firstParam != null)
        //                        maxTransfer = Utility.ToInteger(firstParam.Value);
        //                }
        //            }
        //            ar = prs.AssignedRuleList.Where(x => x.FromDate <= date && x.ToDate >= date && x.IdentifierCode == 3017).FirstOrDefault();
        //            if (ar != null)
        //            {
        //                IList<AssignedRuleParameter> asp = ruleParameterList.Where(x => x.RuleId == ar.RuleId && x.FromDate <= date && x.ToDate >= date).ToList();
        //                //IList<AssignedRuleParameter> asp = ar.RuleParameterList.Where(x => x.FromDate <= date && x.ToDate >= date).ToList();
        //                if (asp != null)
        //                {
        //                    AssignedRuleParameter firstParam = asp.Where(x => x.Name.ToLower().Equals("first")).FirstOrDefault();

        //                    if (firstParam != null)
        //                        leaveInday = Utility.ToInteger(firstParam.Value);
        //                }
        //            }
        //        }

        //        #endregion

        //        toLeaveYear.Date = toDate;
        //        toLeaveYear.Person = new Person() { ID = personId };

        //        int realDay, realMinute;

        //        this.GetRemainLeaveToEndOfYear(personId, fromYear, 12, out realDay, out realMinute);
        //        LeaveInfo lastRemainInfo = prs.GetLeaveYearRemain(date.AddDays(-1));



        //        toLeaveYear.DayRemainReal = realDay;
        //        toLeaveYear.MinuteRemainReal = realMinute;

        //        toLeaveYear.DayRemainOK = realDay;
        //        toLeaveYear.MinuteRemainOK = realMinute;

        //        if (maxTransfer > 0 && leaveInday > 0)
        //        {
        //            //با فرض اینکه قبلا محاسبات انجام شده است
        //            //GTSEngineWS.TotalWebServiceClient gtsEngineWS = new GTS.Clock.Business.GTSEngineWS.TotalWebServiceClient();
        //            //gtsEngineWS.GTS_ExecuteByPersonID(BUser.CurrentUser.UserName, personId);

        //            toLeaveYear.DayRemainOK = lastRemainInfo.Day;
        //            toLeaveYear.MinuteRemainOK = lastRemainInfo.Minute;

        //            //مانده مرخصی امسال
        //            int curentYearRemain = (realDay * leaveInday + realMinute) - (lastRemainInfo.Day * leaveInday - lastRemainInfo.Minute);
        //            if (curentYearRemain < 0)
        //                curentYearRemain = 0;
        //            if (maxTransfer * leaveInday > curentYearRemain)
        //            {
        //                toLeaveYear.DayRemainOK += (curentYearRemain / leaveInday);
        //                toLeaveYear.MinuteRemainOK += (curentYearRemain % leaveInday);
        //            }
        //            else
        //            {
        //                toLeaveYear.DayRemainOK += maxTransfer;
        //                toLeaveYear.MinuteRemainOK += 0;
        //            }
        //        }

        //        NHibernateSessionManager.Instance.GetSession().Evict(prs);

        //        decimal leaveRemainID = 0;
        //        if (toLeaveYear.ID != 0)
        //        {
        //            leaveRemainID = this.SaveChanges(toLeaveYear, UIActionType.EDIT);
        //        }
        //        else
        //        {
        //            leaveRemainID = this.SaveChanges(toLeaveYear, UIActionType.ADD);
        //        }
        //        this.ExtraLeaveYearRemainsExistanceValidate(leaveRemainID, toDate, personId);
        //        NHibernateSessionManager.Instance.ClearSession();
        //        return toLeaveYear.ID;
        //    }
        //    catch (Exception ex)
        //    {
        //        LogException(ex, "BRemainLeave", "TransferToNextYear");
        //        throw ex;
        //    }
        //}

        #endregion

        /// <summary>
        /// انتقال مانده مرخصی
        /// حتما باید قوانین 82 و 3017 در شروع سال جدید فعال و دارای پارامتر باشد 
        /// توجه شود که قبل از فراخوانی محاسبات باید انجام شده باشد
        /// </summary>
        /// <param name="personId">کلید اصلی پرسنل</param>
        /// <param name="fromDate">تاریخ ابتدا</param>
        /// <param name="toDate">تاریخ انتها</param>
        /// <param name="fromYear">سال ابتدا</param>
        /// <param name="toYear">سال انتها</param>
        /// <returns>کلید اصلی مانده مرخصی انتقال یافته شده</returns>
        private decimal TransferToNextYear(decimal personId, DateTime fromDate, DateTime toDate, int fromYear, int toYear)
        {
            try
            {
                DateTime date = DateTime.Now;
                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    date = Utility.ToMildiDate(String.Format("{0}/01/01", toYear));
                }
                else
                {
                    date = new DateTime(toYear, 1, 1);
                }


                UIValidationExceptions exception = new UIValidationExceptions();
                if (toYear - fromYear != 1)
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.RemainTransferFromToYearDiffrenceMoreThanOne, "اختلاف سال شروع و پایان برای انتقال مانده مرخصی به سال بعد باید یک باشد", ExceptionSrc));
                }

                int fromYearCount = this.GetRemainLeaveCount(personId, fromYear, fromYear);
                LeaveYearRemain toLeaveYear = objectRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new LeaveYearRemain().Date), fromDate),
                                                                      new CriteriaStruct(Utility.GetPropertyName(() => new LeaveYearRemain().Date), toDate),
                                                                      new CriteriaStruct(Utility.GetPropertyName(() => new LeaveYearRemain().PersonId), personId)).FirstOrDefault();
                if (toLeaveYear == null)
                {
                    toLeaveYear = new LeaveYearRemain();
                }
                if (fromYearCount == 0)
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.RemainTransferFromYearIsNotExists, "طلب سالانه مرخصی برای سال شروع مشخص شده موجود نمیباشد ", ExceptionSrc));
                }

                if (exception.Count > 0)
                {
                    throw exception;
                }

                Person prs = new BPerson().GetByID(personId);
                prs.InitializeForAccessRules(fromDate, toDate);
                int maxTransfer = 0;
                int leaveInday = 0;

                #region Get From Rule Parameters
                if (prs.AssignedRuleList != null)
                {
                    IRuleRepository ruleRep = Rule.GetRuleRepository(false);
                    IList<AssignedRuleParameter> ruleParameterList = ruleRep.GetAssginedRuleParamList(fromDate, toDate);
                    AssignedRule ar = prs.AssignedRuleList.Where(x => x.FromDate <= date && x.ToDate >= date && x.IdentifierCode == 3009).FirstOrDefault();
                    if (ar != null)
                    {
                        IList<AssignedRuleParameter> asp = ruleParameterList.Where(x => x.RuleId == ar.RuleId && x.FromDate <= date && x.ToDate >= date).ToList();
                        //IList<AssignedRuleParameter> asp = ar.RuleParameterList.Where(x => x.FromDate <= date && x.ToDate >= date).ToList();                       
                        if (asp != null)
                        {
                            AssignedRuleParameter firstParam = asp.Where(x => x.Name.ToLower().Equals("first")).FirstOrDefault();

                            if (firstParam != null)
                                maxTransfer = Utility.ToInteger(firstParam.Value);
                        }
                    }
                    ar = prs.AssignedRuleList.Where(x => x.FromDate <= date && x.ToDate >= date && x.IdentifierCode == 3017).FirstOrDefault();
                    if (ar != null)
                    {
                        IList<AssignedRuleParameter> asp = ruleParameterList.Where(x => x.RuleId == ar.RuleId && x.FromDate <= date && x.ToDate >= date).ToList();
                        //IList<AssignedRuleParameter> asp = ar.RuleParameterList.Where(x => x.FromDate <= date && x.ToDate >= date).ToList();
                        if (asp != null)
                        {
                            AssignedRuleParameter firstParam = asp.Where(x => x.Name.ToLower().Equals("first")).FirstOrDefault();

                            if (firstParam != null)
                                leaveInday = Utility.ToInteger(firstParam.Value);
                        }
                    }
                }

                #endregion

                toLeaveYear.Date = toDate;
                toLeaveYear.Person = new Person() { ID = personId };

                int realDay, realMinute;

                this.GetRemainLeaveToEndOfYear(personId, fromYear, 12, 0, out realDay, out realMinute);
                LeaveInfo lastRemainInfo = prs.GetLeaveYearRemain(date.AddDays(-1));



                toLeaveYear.DayRemainReal = realDay;
                toLeaveYear.MinuteRemainReal = realMinute;

                toLeaveYear.DayRemainOK = realDay;
                toLeaveYear.MinuteRemainOK = realMinute;

                //با فرض اینکه قبلا محاسبات انجام شده است
                //GTSEngineWS.TotalWebServiceClient gtsEngineWS = new GTS.Clock.Business.GTSEngineWS.TotalWebServiceClient();
                //gtsEngineWS.GTS_ExecuteByPersonID(BUser.CurrentUser.UserName, personId);

                //مانده مرخصی امسال
                int curentYearRemain = (realDay * leaveInday + realMinute) - (lastRemainInfo.Day * leaveInday + lastRemainInfo.Minute);
                if (curentYearRemain < 0)
                    curentYearRemain = 0;
                if (maxTransfer > 0 && leaveInday > 0 && curentYearRemain > 0 && curentYearRemain > maxTransfer)
                {


                    toLeaveYear.DayRemainOK = lastRemainInfo.Day;
                    toLeaveYear.MinuteRemainOK = lastRemainInfo.Minute;


                    if (maxTransfer * leaveInday > curentYearRemain)
                    {
                        toLeaveYear.DayRemainOK += (curentYearRemain / leaveInday);
                        toLeaveYear.MinuteRemainOK += (curentYearRemain % leaveInday);
                    }
                    else
                    {
                        toLeaveYear.DayRemainOK += maxTransfer;
                        toLeaveYear.MinuteRemainOK += 0;
                    }
                }

                NHibernateSessionManager.Instance.GetSession().Evict(prs);

                decimal leaveRemainID = 0;
                if (toLeaveYear.ID != 0)
                {
                    leaveRemainID = this.SaveChanges(toLeaveYear, UIActionType.EDIT);
                }
                else
                {
                    leaveRemainID = this.SaveChanges(toLeaveYear, UIActionType.ADD);
                }
                this.ExtraLeaveYearRemainsExistanceValidate(leaveRemainID, toDate, personId);
                NHibernateSessionManager.Instance.ClearSession();
                return toLeaveYear.ID;
            }
            catch (Exception ex)
            {
                LogException(ex, "BRemainLeave", "TransferToNextYear");
                throw ex;
            }
        }

        /// <summary>
        /// میزان سوخت شدن مانده مرخصی پایان سال بیشتر از سقف مجاز
        /// </summary>
        /// <param name="finalLeaveYearRemainID">کلید اصلی مانده مرخصی پایان سال</param>
        /// <param name="targetDate">تاریخ</param>
        /// <param name="personnelID">کلید اصلی پرسنل</param>
        private void ExtraLeaveYearRemainsExistanceValidate(decimal finalLeaveYearRemainID, DateTime targetDate, decimal personnelID)
        {
            IList<LeaveYearRemain> ExtraLeaveYearRemainsList = this.LeaveYearRemainRep.GetExtraLeaveYearRemains(finalLeaveYearRemainID, targetDate, personnelID);
            foreach (LeaveYearRemain ExtraLeaveYearRemainItem in ExtraLeaveYearRemainsList)
            {
                this.SaveChanges(ExtraLeaveYearRemainItem, UIActionType.DELETE);
            }
        }

        /// <summary>
        /// مانده مرخصی سالهای قبل را به پروکسی تبدیل میکند
        /// </summary>
        /// <param name="list">لیست مانده مرخصی</param>
        /// <returns>لیست پروکسی مانده مرخصی</returns>
        private IList<RemainLeaveProxy> ConvertToProxy(IList<LeaveYearRemain> list)
        {


            IList<RemainLeaveProxy> proxyList = new List<RemainLeaveProxy>();
            foreach (LeaveYearRemain leave in list)
            {
                RemainLeaveProxy proxy = new RemainLeaveProxy();

                proxy.ID = leave.ID;
                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    proxy.Year = Utility.ToPersianDateTime(leave.Date).Year;
                    proxy.Date = Utility.ToPersianDate(leave.Date);
                }
                else
                {
                    proxy.Year = leave.Date.Year;
                    proxy.Date = leave.Date.ToShortDateString();
                }

                proxy.Person = leave.Person;

                proxy.RealDay = leave.DayRemainReal.ToString();
                proxy.RealHour = Utility.IntTimeToTime(leave.MinuteRemainReal, true);

                proxy.ConfirmedDay = leave.DayRemainOK.ToString();
                proxy.ConfirmedHour = Utility.IntTimeToTime(leave.MinuteRemainOK, true);

                proxyList.Add(proxy);
            }
            return proxyList;
        }

        /// <summary>
        /// بررسی دسترسی به مانده مرخصی
        /// </summary>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckLeaveRemainsLoadAccess()
        {
        }

        /// <summary>
        /// انتقال مانده مرخصی به سال بعد
        /// </summary>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void TransferToNextYear()
        {
        }

        #region ILeaveInfo Members

        /// <summary>
        /// مانده مرخصی تا انتهای ماه جاری
        /// </summary>
        /// <param name="personId">کلید اصلی پرسنل</param>
        /// <param name="year">سال</param>
        /// <param name="month">ماه</param>
        /// <param name="day">روز</param>
        /// <param name="minutes">دقیق</param>
        public void GetRemainLeaveToEndOfMonth(decimal personId, int year, int month, int toDay, out int day, out int minutes)
        {
            try
            {
                PersonRepository prsRep = new PersonRepository();

                DateTime endYear = new DateTime();
                DateTime startYear = new DateTime();
                DateTime endMonth = new DateTime();
                DateTime beginYearContract = new DateTime();
                DateTime endYearContract = new DateTime();

                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    if (toDay == 0)
                        toDay = Utility.GetEndOfPersianMonth(year, month);
                    endMonth = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, month, toDay));
                    startYear = Utility.ToMildiDate(String.Format("{0}/1/1", year));
                    endYear = Utility.ToMildiDate(String.Format("{0}/12/{1}", year, Utility.GetEndOfPersianMonth(year, 12)));
                }
                else
                {
                    if (toDay == 0)
                        toDay = Utility.GetEndOfMiladiMonth(year, month);
                    endMonth = new DateTime(year, month, toDay);
                    startYear = new DateTime(year, 1, 1);
                    endYear = new DateTime(year, 12, Utility.GetEndOfMiladiMonth(year, 12));
                }

                day = 0;
                minutes = 0;
                try
                {
                    Person prs = prsRep.GetById(personId, false);
                    beginYearContract = prs.GetBeginOfLeaveYear(endMonth);
                    endYearContract = prs.GetEndOfLeaveYear(endMonth);
                    prs.CalcDateZone = new DateRange(startYear, endYear, startYear, endYear);
                    prsRep.EnableEfectiveDateFilter(prs.ID, prs.CalcDateZone.FromDate, prs.CalcDateZone.ToDate, startYear, endYear, prs.CalcDateZone.FromDate.AddDays(-20), prs.CalcDateZone.ToDate.AddDays(+20));

                    LeaveInfo linfo = prs.GetRemainLeaveToDateUI(endMonth);
                    day = linfo.Day;
                    minutes = linfo.Minute;
                }
                catch (InvalidDatabaseStateException ex)
                {
                    if (ex.FatalExceptionIdentifier == UIFatalExceptionIdentifiers.LeaveLCRDoesNotExists)
                    {
                        day = 0;
                        minutes = 0;
                        LogException(ex);
                    }
                    else throw ex;
                }
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw ex;
            }
        }

        /// <summary>
        /// مانده مرخصی تا پایان ماه جاری را بر می گرداند
        /// </summary>
        /// <param name="personId">کلید اصلی پرسنل</param>
        /// <param name="year">سال</param>
        /// <param name="month">ماه</param>
        /// <param name="minutes">خروجی بر حسب تعداد دقایق</param>
        public void GetRemainLeaveToEndOfMonth(decimal personId, int year, int month, out int minutes)
        {
            try
            {
                PersonRepository prsRep = new PersonRepository();

                DateTime endYear = new DateTime();
                DateTime startYear = new DateTime();
                DateTime endMonth = new DateTime();

                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {

                    endMonth = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, month, Utility.GetEndOfPersianMonth(year, month)));
                    startYear = Utility.ToMildiDate(String.Format("{0}/1/1", year));
                    endYear = Utility.ToMildiDate(String.Format("{0}/12/{1}", year, Utility.GetEndOfPersianMonth(year, 12)));
                }
                else
                {
                    endMonth = new DateTime(year, month, Utility.GetEndOfMiladiMonth(year, month));
                    startYear = new DateTime(year, 1, 1);
                    endYear = new DateTime(year, 12, Utility.GetEndOfMiladiMonth(year, 12));
                }


                minutes = 0;
                try
                {
                    Person prs = prsRep.GetById(personId, false);
                    prs.CalcDateZone = new DateRange(startYear, endYear, startYear, endYear);
                    prsRep.EnableEfectiveDateFilter(prs.ID, prs.CalcDateZone.FromDate, prs.CalcDateZone.ToDate, startYear, endYear, prs.CalcDateZone.FromDate.AddDays(-20), prs.CalcDateZone.ToDate.AddDays(+20));

                    minutes = prs.GetRemainLeaveToUiValidation(endMonth);

                }
                catch (InvalidDatabaseStateException ex)
                {
                    if (ex.FatalExceptionIdentifier == UIFatalExceptionIdentifiers.LeaveLCRDoesNotExists)
                    {

                        minutes = 0;
                        LogException(ex);
                    }
                    else throw ex;
                }
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw ex;
            }
        }

        /// <summary>
        /// مرخصی مصرف شده از ابتدای سال و ماه مشخص را برمی گرداند
        /// </summary>
        /// <param name="personId">کلید اصلی پرسنل</param>
        /// <param name="year">سال</param>
        /// <param name="month">ماه</param>
        /// <param name="day">تعداد روز را بر می گرداند</param> 
        /// <param name="minutes">تعدا دقایق را بر می گرداند</param> 
        public void GetRemainLeaveToEndOfYear(decimal personId, int year, int month, int toDay, out int day, out int minutes)
        {
            try
            {

                PersonRepository prsRep = new PersonRepository();

                DateTime endYear = new DateTime();
                DateTime startYear = new DateTime();
                DateTime endMonth = new DateTime();
                DateTime beginYearContract = new DateTime();
                DateTime endYearContract = new DateTime();
                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    if (toDay == 0)
                        toDay = Utility.GetEndOfPersianMonth(year, month);
                    endMonth = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, month, toDay));
                    startYear = Utility.ToMildiDate(String.Format("{0}/1/1", year));
                    endYear = Utility.ToMildiDate(String.Format("{0}/12/{1}", year, Utility.GetEndOfPersianMonth(year, 12)));
                }
                else
                {
                    if (toDay == 0)
                        toDay = Utility.GetEndOfMiladiMonth(year, month);
                    endMonth = new DateTime(year, month, toDay);
                    startYear = new DateTime(year, 1, 1);
                    endYear = new DateTime(year, 12, Utility.GetEndOfMiladiMonth(year, 12));
                }

                day = 0;
                minutes = 0;
                try
                {
                    Person prs = prsRep.GetById(personId, false);
                    //DateTime contractDate;
                    //if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                    //{
                    //    contractDate = Utility.ToMildiDate(year + "/" + month + "/" + toDay);
                    //}
                    //else
                    //{
                    //    contractDate = Utility.ToMildiDateTime(year + "/" + month + "/" + toDay);
                    //}
                    beginYearContract = prs.GetBeginOfLeaveYear(endMonth);
                    endYearContract = prs.GetEndOfLeaveYear(endMonth);
                    if (endYearContract == Utility.GTSMinStandardDateTime)
                        endYearContract = endYear;
                    prs.CalcDateZone = new DateRange(startYear, endYear, startYear, endYear);
                    prsRep.EnableEfectiveDateFilter(prs.ID, prs.CalcDateZone.FromDate, prs.CalcDateZone.ToDate, startYear, endYear, prs.CalcDateZone.FromDate.AddDays(-20), prs.CalcDateZone.ToDate.AddDays(+20));

                    LeaveInfo linfo = prs.GetRemainLeaveToEndOfYearUI(beginYearContract, endYearContract);
                    day = linfo.Day;
                    minutes = linfo.Minute;
                    PersonContractAssignment personContractAssigment = prs.PersonContractAssignmentList.FirstOrDefault(c => c.FromDate <= endMonth && (c.ToDate == Utility.GTSMinStandardDateTime || c.ToDate >= endMonth));
                    if (personContractAssigment == null)
                    {
                        day = 0;
                        minutes = 0;
                    }
                }
                catch (InvalidDatabaseStateException ex)
                {
                    if (ex.FatalExceptionIdentifier == UIFatalExceptionIdentifiers.LeaveLCRDoesNotExists)
                    {
                        day = 0;
                        minutes = 0;
                        LogException(ex);
                    }
                    else throw ex;
                }
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw ex;
            }
        }

        /// <summary>
        /// مرخصی مصرف شده از ابتدای سال را برمی گرداند
        /// </summary>
        /// <param name="personId">کلید اصلی پرسنل</param>
        /// <param name="year">سال</param>
        /// <param name="day">تعداد روز را بر می گرداند</param> 
        /// <param name="minutes">تعدا دقایق را بر می گرداند</param> 
        public void GetUsedLeaveToEndOfYear(decimal personId, int year, int month, int toDay, out int day, out int minutes)
        {
            try
            {

                PersonRepository prsRep = new PersonRepository();

                DateTime endYear = new DateTime();
                DateTime startYear = new DateTime();
                DateTime endMonth = new DateTime();
                DateTime beginYearContract = new DateTime();
                DateTime endYearContract = new DateTime();
                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    if (toDay == 0)
                        toDay = Utility.GetEndOfPersianMonth(year, month);
                    endMonth = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, month, toDay));
                    startYear = Utility.ToMildiDate(String.Format("{0}/1/1", year));
                    endYear = Utility.ToMildiDate(String.Format("{0}/12/{1}", year, Utility.GetEndOfPersianMonth(year, 12)));
                }
                else
                {
                    if (toDay == 0)
                        toDay = Utility.GetEndOfMiladiMonth(year, month);
                    startYear = new DateTime(year, 1, 1);
                    endYear = new DateTime(year, 12, Utility.GetEndOfMiladiMonth(year, 12));
                }
                day = 0;
                minutes = 0;
                try
                {
                    Person prs = prsRep.GetById(personId, false);
                    //DateTime contractDate;
                    //if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                    //{
                    //    contractDate = Utility.ToMildiDate(year + "/" + month + "/" + toDay);
                    //}
                    //else
                    //{
                    //    contractDate = Utility.ToMildiDateTime(year + "/" + month + "/" + toDay);
                    //}
                    beginYearContract = prs.GetBeginOfLeaveYear(endMonth);
                    endYearContract = prs.GetEndOfLeaveYear(endMonth);
                    if (endYearContract == Utility.GTSMinStandardDateTime)
                        endYearContract = endYear;
                    prs.CalcDateZone = new DateRange(startYear, endYear, startYear, endYear);
                    prsRep.EnableEfectiveDateFilter(prs.ID, prs.CalcDateZone.FromDate, prs.CalcDateZone.ToDate, startYear, endYear, prs.CalcDateZone.FromDate.AddDays(-20), prs.CalcDateZone.ToDate.AddDays(+20));

                    LeaveCalcResult lastLcr = prs.GetLastLCR(beginYearContract, endMonth);
                    lastLcr.Day += lastLcr.Minute / lastLcr.LeaveMinuteInDay;
                    lastLcr.Minute = lastLcr.Minute % lastLcr.LeaveMinuteInDay;

                    day = lastLcr.DayUsed;
                    minutes = lastLcr.MinuteUsed;
                    PersonContractAssignment personContractAssigment = prs.PersonContractAssignmentList.FirstOrDefault(c => c.FromDate <= endMonth && (c.ToDate == Utility.GTSMinStandardDateTime || c.ToDate >= endMonth));
                    if (personContractAssigment == null)
                    {
                        day = 0;
                        minutes = 0;
                    }
                }
                catch (InvalidDatabaseStateException ex)
                {
                    if (ex.FatalExceptionIdentifier == UIFatalExceptionIdentifiers.LeaveLCRDoesNotExists)
                    {
                        day = 0;
                        minutes = 0;
                        LogException(ex);
                    }
                    else throw ex;
                }
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw ex;
            }
        }

        #endregion
    }
}
