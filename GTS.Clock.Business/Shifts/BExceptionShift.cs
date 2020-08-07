﻿using System;
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
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Business.Security;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Infrastructure.Validation.Configuration;
using GTS.Clock.Business.Presentaion_Helper.Proxy;
using System.Reflection;
using System.Globalization;
using NHibernate.Criterion;


namespace GTS.Clock.Business.Shifts
{
    /// <summary>
    /// created at: 4/14/2012 3:44:44 PM
    /// by        : Farhad Salavati
    /// write your name here
    /// </summary>
    public class BExceptionShift : BaseBusiness<ShiftException>
    {
        private const string ExceptionSrc = "GTS.Clock.Business.Shifts.BExceptionShift";
        private ShiftExceptionRepository objectRep = new ShiftExceptionRepository(false);
        private ISearchPerson PersonSearchBusiness = (ISearchPerson)(new BPerson());
        private BPerson bPeson = new BPerson();



        /// <summary>
        /// لیست شیفتهای استثنا را برمیگرداند
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        public IList<ShiftException> GetExceptionShiftList(decimal personId, string fromDate, string toDate)
        {
            try
            {
                DateTime from, to;
                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    from = Utility.ToMildiDate(fromDate);
                    to = Utility.ToMildiDate(toDate);
                }
                else
                {
                    from = Utility.ToMildiDateTime(fromDate);
                    to = Utility.ToMildiDateTime(toDate);
                }

                IList<ShiftException> list = objectRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new ShiftException().Person), new Person() { ID = personId }),
                                                                     new CriteriaStruct(Utility.GetPropertyName(() => new ShiftException().Date), from, CriteriaOperation.GreaterEqThan),
                                                                     new CriteriaStruct(Utility.GetPropertyName(() => new ShiftException().Date), to, CriteriaOperation.LessEqThan));

                foreach (ShiftException shift in list)
                {
                    if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                    {
                        shift.TheDate = Utility.ToPersianDate(shift.Date);
                    }
                    else
                    {
                        shift.TheDate = Utility.ToString(shift.Date);
                    }
                    shift.PersonName = shift.Person.Name;
                    if (shift.Shift != null)
                    {
                        shift.ShiftName = shift.Shift.Name;
                        shift.ShiftPairs = shift.Shift.ToString();
                    }
                    else
                    {
                        shift.Shift = new Shift();
                    }
                }
                return list;

            }
            catch (Exception ex)
            {
                LogException(ex, "BExceptionShift", "GetShiftList");
                throw ex;
            }
        }

        #region Exchange

        /// <summary>
        /// برای یک شخص در یک بازه تاریخی شیفت استثنا درج میکند
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="shiftId"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        public void InsertByPerson(decimal personId, decimal shiftId, string fromDate, string toDate)
        {
            UIValidationExceptions exception = new UIValidationExceptions();
            DateTime from, to;
            if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
            {
                from = Utility.ToMildiDate(fromDate);
                to = Utility.ToMildiDate(toDate);
            }
            else
            {
                from = Utility.ToMildiDateTime(fromDate);
                to = Utility.ToMildiDateTime(toDate);
            }
            if (personId == 0)
            {
                exception.Add(ExceptionResourceKeys.ExceptionShiftPersonIdRequierd, "شناسه پرسنل مشخص نشده است", ExceptionSrc);
            }
            if (shiftId == 0)
            {
                exception.Add(ExceptionResourceKeys.ExceptionShiftShiftIdRequierd, "شناسه شیفت مشخص نشده است", ExceptionSrc);
            }
            if (from > to)
            {
                exception.Add(ExceptionResourceKeys.ExceptionShiftDateInvalid, "بازه تاریخ اشتباه است", ExceptionSrc);
            }
            if (exception.Count > 0)
            {
                throw exception;
            }


            using (NHibernateSessionManager.Instance.BeginTransactionOn())
            {
                try
                {

                    objectRep.DeleteExceptionShift(personId, from, to);
                    for (DateTime date = from; date <= to; date = date.AddDays(1))
                    {
                        ShiftException exShift = new ShiftException();
                        exShift.RegistrationDate = DateTime.Now.Date;
                        exShift.Date = date;
                        exShift.Person = new Person() { ID = personId };
                        exShift.Shift = new Shift() { ID = shiftId };
                        exShift.UserID = BUser.CurrentUser.ID;
                        base.SaveChanges(exShift, UIActionType.ADD);
                    }
                    NHibernateSessionManager.Instance.CommitTransactionOn();
                }
                catch (Exception ex)
                {
                    NHibernateSessionManager.Instance.RollbackTransactionOn();
                    LogException(ex, "BExceptionShift", "InsertByPerson");
                    throw ex;
                }
            }
        }

        /// <summary>
        /// شیفت یک نفر را در دو روز عوض میکند
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="firstDay"></param>
        /// <param name="seCondDate"></param>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void ExchangeDayByPerson(decimal personId, string firstDay, string secondDay)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            if (personId == 0)
            {
                exception.Add(ExceptionResourceKeys.ExceptionShiftPersonIdRequierd, "شناسه پرسنل مشخص نشده است", ExceptionSrc);
            }           
            if (exception.Count > 0)
            {
                throw exception;
            }
            using (NHibernateSessionManager.Instance.BeginTransactionOn())
            {
                try
                {
                    DateTime firstDate, secondDate;
                    if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                    {
                        firstDate = Utility.ToMildiDate(firstDay);
                        secondDate = Utility.ToMildiDate(secondDay);
                    }
                    else
                    {
                        firstDate = Utility.ToMildiDateTime(firstDay);
                        secondDate = Utility.ToMildiDateTime(secondDay);
                    }

                    objectRep.DeleteExceptionShift(personId, firstDate, firstDate);
                    objectRep.DeleteExceptionShift(personId, secondDate, secondDate);

                    ShiftRepository shiftRep = new ShiftRepository();
                    decimal? shift1 = shiftRep.GetShiftIdByPersonId(personId, firstDate);
                    decimal? shift2 = shiftRep.GetShiftIdByPersonId(personId, secondDate);

                    ShiftException exShift = new ShiftException();
                    exShift.RegistrationDate = DateTime.Now.Date;
                    exShift.Date = secondDate;
                    exShift.Person = new Person() { ID = personId };
                    exShift.UserID = BUser.CurrentUser.ID;
                    if (shift1 != null && shift1 > 0)
                    {
                        exShift.Shift = new Shift() { ID = (decimal)shift1 };
                    }
                    else
                    {
                        exShift.Shift = null;
                    }
                    base.SaveChanges(exShift, UIActionType.ADD);

                    exShift = new ShiftException();
                    exShift.RegistrationDate = DateTime.Now.Date;
                    exShift.Date = firstDate;
                    exShift.Person = new Person() { ID = personId };
                    exShift.UserID = BUser.CurrentUser.ID;
                    if (shift2 != null && shift2 > 0)
                    {
                        exShift.Shift = new Shift() { ID = (decimal)shift2 };
                    }
                    else
                    {
                        exShift.Shift = null;
                    }
                    base.SaveChanges(exShift, UIActionType.ADD);

                    NHibernateSessionManager.Instance.CommitTransactionOn();
                }
                catch (Exception ex)
                {
                    NHibernateSessionManager.Instance.RollbackTransactionOn();
                    LogException(ex, "BExceptionShift", "ExchangeDayByPerson");
                    throw ex;
                }
            }
        }

        /// <summary>
        /// شیفت یک گروه کاری را در دو روز عوض میکند
        /// </summary>
        /// <param name="workGroupId"></param>
        /// <param name="firstDay"></param>
        /// <param name="secondDay"></param>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void ExchangeDayByWorkGroup(decimal workGroupId, string firstDay, string secondDay)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            if (workGroupId == 0)
            {
                exception.Add(ExceptionResourceKeys.ExceptionShiftWorkGroupIdRequierd, "شناسه گروه کاری مشخص نشده است", ExceptionSrc);
            }
            if (firstDay == secondDay)
            {
                exception.Add(ExceptionResourceKeys.SelectedDateIsSimilar, "تاریخ های انتخاب شده یکی می باشند", ExceptionSrc);
            }
            if (exception.Count > 0)
            {
                throw exception;
            }
            using (NHibernateSessionManager.Instance.BeginTransactionOn())
            {
                try
                {
                    DateTime firstDate, secondDate;
                    if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                    {
                        firstDate = Utility.ToMildiDate(firstDay);
                        secondDate = Utility.ToMildiDate(secondDay);
                    }
                    else
                    {
                        firstDate = Utility.ToMildiDateTime(firstDay);
                        secondDate = Utility.ToMildiDateTime(secondDay);
                    }

                    ISearchPerson searchTool = new BPerson();
                    PersonAdvanceSearchProxy proxy = new PersonAdvanceSearchProxy();
                    proxy.WorkGroupId = workGroupId;
                    proxy.WorkGroupFromDate = Utility.ToString(firstDate);
                    int count = searchTool.GetPersonInAdvanceSearchCount(proxy);
                    IList<Person> persons = searchTool.GetPersonInAdvanceSearch(proxy, 0, count);
                    foreach (Person prs in persons)
                    {
                        decimal personId = prs.ID;
                        objectRep.DeleteExceptionShift(personId, firstDate, firstDate);
                        objectRep.DeleteExceptionShift(personId, secondDate, secondDate);

                        ShiftRepository shiftRep = new ShiftRepository();
                        decimal? shift1 = shiftRep.GetShiftIdByPersonId(personId, firstDate);
                        decimal? shift2 = shiftRep.GetShiftIdByPersonId(personId, secondDate);

                        ShiftException exShift = new ShiftException();
                        exShift.RegistrationDate = DateTime.Now.Date;
                        exShift.Date = secondDate;
                        exShift.Person = new Person() { ID = personId };
                        exShift.UserID = BUser.CurrentUser.ID;
                        if (shift1 != null && shift1 > 0)
                        {
                            exShift.Shift = new Shift() { ID = (decimal)shift1 };
                        }
                        else
                        {
                            exShift.Shift = null;
                        }
                        base.SaveChanges(exShift, UIActionType.ADD);

                        exShift = new ShiftException();
                        exShift.RegistrationDate = DateTime.Now.Date;
                        exShift.Date = firstDate;
                        exShift.Person = new Person() { ID = personId };
                        exShift.UserID = BUser.CurrentUser.ID;
                        if (shift2 != null && shift2 > 0)
                        {
                            exShift.Shift = new Shift() { ID = (decimal)shift2 };
                        }
                        else
                        {
                            exShift.Shift = null;
                        }
                        base.SaveChanges(exShift, UIActionType.ADD);
                    }

                    NHibernateSessionManager.Instance.CommitTransactionOn();
                }
                catch (Exception ex)
                {
                    NHibernateSessionManager.Instance.RollbackTransactionOn();
                    LogException(ex, "BExceptionShift", "ExchangeDayByWorkGroup");
                    throw ex;
                }
            }
        }

        /// <summary>
        /// شیفت دو نفر را در یک روز عوض میکند
        /// </summary>
        /// <param name="personId1"></param>
        /// <param name="personId2"></param>
        /// <param name="date1">تاریخ شیفت شخص 1</param>
        /// <param name="date2">تاریخ شیفت شخص 2</param>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void ExchangePerson(decimal personId1, decimal personId2, string date1, string date2)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            if (personId1 == 0 || personId2 == 0)
            {
                exception.Add(ExceptionResourceKeys.ExceptionShiftPersonIdRequierd, "شناسه پرسنل مشخص نشده است", ExceptionSrc);
            }

            if (exception.Count > 0)
            {
                throw exception;
            }
            using (NHibernateSessionManager.Instance.BeginTransactionOn())
            {
                try
                {
                    DateTime dayDate1, dayDate2;
                    if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                    {
                        dayDate1 = Utility.ToMildiDate(date1);
                        dayDate2 = Utility.ToMildiDate(date2);
                    }
                    else
                    {
                        dayDate1 = Utility.ToMildiDateTime(date1);
                        dayDate2 = Utility.ToMildiDateTime(date2);
                    }

                    objectRep.DeleteExceptionShift(personId1, dayDate1, dayDate1);
                    objectRep.DeleteExceptionShift(personId1, dayDate2, dayDate2);
                    objectRep.DeleteExceptionShift(personId2, dayDate2, dayDate2);
                    objectRep.DeleteExceptionShift(personId2, dayDate1, dayDate1);

                    ShiftRepository shiftRep = new ShiftRepository();
                    decimal? shift1 = shiftRep.GetShiftIdByPersonId(personId1, dayDate1);
                    decimal? shift2 = shiftRep.GetShiftIdByPersonId(personId2, dayDate2);

                    ShiftException exShift = new ShiftException();
                    exShift.RegistrationDate = DateTime.Now.Date;
                    exShift.Date = dayDate1;
                    exShift.Person = new Person() { ID = personId2 };
                    exShift.UserID = BUser.CurrentUser.ID;
                    if (shift1 != null && shift1 > 0)
                    {
                        exShift.Shift = new Shift() { ID = (decimal)shift1 };
                    }
                    else
                    {
                        exShift.Shift = null;
                    }
                    base.SaveChanges(exShift, UIActionType.ADD);

                    exShift = new ShiftException();
                    exShift.RegistrationDate = DateTime.Now.Date;
                    exShift.Date = dayDate2;
                    exShift.Person = new Person() { ID = personId1 };
                    exShift.UserID = BUser.CurrentUser.ID;
                    if (shift2 != null && shift2 > 0)
                    {
                        exShift.Shift = new Shift() { ID = (decimal)shift2 };
                    }
                    else
                    {
                        exShift.Shift = null;
                    }
                    base.SaveChanges(exShift, UIActionType.ADD);

                    NHibernateSessionManager.Instance.CommitTransactionOn();
                }
                catch (Exception ex)
                {
                    NHibernateSessionManager.Instance.RollbackTransactionOn();
                    LogException(ex, "BExceptionShift", "ExchangePerson");
                    throw ex;
                }
            }
        }

        /// <summary>
        /// شیفت دو گروه کاری را باهم عوض میکند
        /// </summary>
        /// <param name="workGroup1"></param>
        /// <param name="workGroup2"></param>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void ExchangeWorkGroup(decimal workGroup1, decimal workGroup2, string date1, string date2)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            if (workGroup1 == 0 || workGroup2 == 0)
            {
                exception.Add(ExceptionResourceKeys.ExceptionShiftWorkGroupIdRequierd, "شناسه گروه کاری مشخص نشده است", ExceptionSrc);
            }

            if (exception.Count > 0)
            {
                throw exception;
            }
            using (NHibernateSessionManager.Instance.BeginTransactionOn())
            {
                try
                {
                    DateTime dayDate1, dayDate2;
                    if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                    {
                        dayDate1 = Utility.ToMildiDate(date1);
                        dayDate2 = Utility.ToMildiDate(date2);
                    }
                    else
                    {
                        dayDate1 = Utility.ToMildiDateTime(date1);
                        dayDate2 = Utility.ToMildiDateTime(date2);
                    }

                    ISearchPerson searchTool = new BPerson();
                    PersonAdvanceSearchProxy proxy = new PersonAdvanceSearchProxy();
                    proxy.WorkGroupId = workGroup1;
                    proxy.WorkGroupFromDate = Utility.ToString(dayDate1);
                    int count = searchTool.GetPersonInAdvanceSearchCount(proxy);
                    IList<Person> persons1 = searchTool.GetPersonInAdvanceSearch(proxy, 0, count);

                    proxy = new PersonAdvanceSearchProxy();
                    proxy.WorkGroupId = workGroup2;
                    proxy.WorkGroupFromDate = Utility.ToString(dayDate2);
                    count = searchTool.GetPersonInAdvanceSearchCount(proxy);
                    IList<Person> persons2 = searchTool.GetPersonInAdvanceSearch(proxy, 0, count);

                    ShiftRepository shiftRep = new ShiftRepository(false);
                    decimal? shift1 = shiftRep.GetShiftIdByWorkGroupId(workGroup1, dayDate1);
                    decimal? shift2 = shiftRep.GetShiftIdByWorkGroupId(workGroup2, dayDate2);

                    foreach (Person prs in persons2)
                    {
                        decimal personId2 = prs.ID;
                        objectRep.DeleteExceptionShift(personId2, dayDate2, dayDate2);
                        objectRep.DeleteExceptionShift(personId2, dayDate1, dayDate1);

                        ShiftException exShift = new ShiftException();
                        exShift.Date = dayDate1;
                        exShift.Person = new Person() { ID = personId2 };
                        exShift.RegistrationDate = DateTime.Now;
                        if (shift1 != null && shift1 > 0)
                        {
                            exShift.Shift = new Shift() { ID = (decimal)shift1 };
                        }
                        else
                        {
                            exShift.Shift = null;
                        }
                        base.SaveChanges(exShift, UIActionType.ADD);
                    }

                    foreach (Person prs in persons1)
                    {
                        decimal personId1 = prs.ID;
                        objectRep.DeleteExceptionShift(personId1, dayDate2, dayDate2);
                        objectRep.DeleteExceptionShift(personId1, dayDate1, dayDate1);

                        ShiftException exShift = new ShiftException();
                        exShift.Date = dayDate2;
                        exShift.Person = new Person() { ID = personId1 };
                        exShift.RegistrationDate = DateTime.Now;
                        if (shift2 != null && shift2 > 0)
                        {
                            exShift.Shift = new Shift() { ID = (decimal)shift2 };
                        }
                        else
                        {
                            exShift.Shift = null;
                        }
                        base.SaveChanges(exShift, UIActionType.ADD);
                    }
                    NHibernateSessionManager.Instance.CommitTransactionOn();
                }
                catch (Exception ex)
                {
                    NHibernateSessionManager.Instance.RollbackTransactionOn();
                    LogException(ex, "BExceptionShift", "ExchangeWorkGroup");
                    throw ex;
                }
            }
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exShiftId"></param>
        public void DeleteExceptionShift(decimal exShiftId)
        {
            try
            {
                ShiftException ex = base.GetByID(exShiftId);
                SaveChanges(ex, UIActionType.DELETE);
            }
            catch (Exception ex)
            {
                LogException(ex, "BExceptionShift", "DeleteExceptionShift");
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IList<Shift> GetShiftList()
        {
            try
            {
                BShift busShift = new BShift();
                return busShift.GetAll();
            }
            catch (Exception ex)
            {
                LogException(ex, "BExceptionShift", "GetShiftList");
                throw ex;
            }
        }

        /// <summary>
        /// گروهای کاری را برمیگرداند
        /// </summary>
        /// <returns></returns>
        public IList<WorkGroup> GetAllWorkGroups()
        {
            BWorkgroup busw = new BWorkgroup();
            return busw.GetAll();
        }

        /// <summary>
        /// شیفت یک روز را برمیگرداند
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public string GetDayShiftByPersonId(decimal personId, string date)
        {
            try
            {
                DateTime dayDate;
                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    dayDate = Utility.ToMildiDate(date);
                }
                else
                {
                    dayDate = Utility.ToMildiDateTime(date);
                }

                Shift shift = new ShiftRepository(false).GetShiftByPerson(personId, dayDate);
                if (shift == null)
                {
                    return "";
                }
                return shift.Name;
            }
            catch (Exception ex)
            {
                LogException(ex, "BExceptionShift", "GetDayShift");
                throw ex;
            }
        }

        /// <summary>
        /// شیفت یک روز را برمیگرداند
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public string GetDayShiftByWorkGroup(decimal workGroupID, string date)
        {
            try
            {
                DateTime dayDate;
                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    dayDate = Utility.ToMildiDate(date);
                }
                else
                {
                    dayDate = Utility.ToMildiDateTime(date);
                }

                decimal? shiftId = new ShiftRepository(false).GetShiftIdByWorkGroupId(workGroupID, dayDate);
                if (shiftId == null || shiftId == 0)
                {
                    return "";
                }
                return new ShiftRepository(false).GetById((decimal)shiftId, false).Name;
            }
            catch (Exception ex)
            {
                LogException(ex, "BExceptionShift", "GetDayShift");
                throw ex;
            }
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckExceptionShiftsLoadAccess()
        {
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void InsertExceptionShift(decimal personnelID, decimal shiftID, string firstDate, string secondDate)
        {
            this.InsertByPerson(personnelID, shiftID, firstDate, secondDate);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void UpdateExceptionShift(decimal personnelID, decimal shiftID, string firstDate, string secondDate)
        {
            this.InsertByPerson(personnelID, shiftID, firstDate, secondDate);
        }

        public IList<MonthlyExceptionShiftProxy> GetMonthlyExceptionShiftsList(int year, int month, int pageIndex, int pageSize, string searchItem)
        {
            try
            {
                //IList<Person> PersonnelList = this.PersonSearchBusiness.QuickSearchByPageApplyCulture(pageIndex, pageSize, string.Empty);
                IList<MonthlyExceptionShiftProxy> MonthlyExceptionShiftProxyList = this.GetMonthlyExceptionShiftProxyList(year, month, searchItem, pageIndex, pageSize);
                return MonthlyExceptionShiftProxyList;
            }
            catch (Exception ex)
            {
                BaseBusiness<ShiftException>.LogException(ex, "BMonthlyExceptionShifts", "GetMonthlyExceptionShiftsList");
                throw ex;
            }
        }

        //public IList<MonthlyExceptionShiftProxy> GetMonthlyExceptionShiftsListByQuickSerch(int year, int month, string searchTerm, int pageIndex, int pageSize)
        //{
        //    try
        //    {
        //        IList<Person> PersonnelList = this.PersonSearchBusiness.QuickSearchByPageApplyCulture(pageIndex, pageSize, searchTerm);
        //        IList<MonthlyExceptionShiftProxy> MonthlyExceptionShiftProxyList = this.GetMonthlyExceptionShiftProxyList(year, month, PersonnelList);
        //        return MonthlyExceptionShiftProxyList;

        //    }
        //    catch (Exception ex)
        //    {
        //        BaseBusiness<ShiftException>.LogException(ex, "BMonthlyExceptionShifts", "GetMonthlyExceptionShiftsListByQuickSerch");
        //        throw ex;
        //    }
        //}
        //public IList<MonthlyExceptionShiftProxy> GetMonthlyExceptionShiftsListBySearch(int year, int month, decimal PersonId, int pageIndex, int pageSize)
        //{
        //    try
        //    {
        //        Person personObj = new BPerson().GetByID(PersonId);
        //        IList<Person> PersonnelList = new List<Person>();
        //        PersonnelList.Add(personObj);
        //        IList<MonthlyExceptionShiftProxy> MonthlyExceptionShiftProxyList = this.GetMonthlyExceptionShiftProxyList(year, month, PersonnelList);
        //        return MonthlyExceptionShiftProxyList;

        //    }
        //    catch (Exception ex)
        //    {
        //        BaseBusiness<ShiftException>.LogException(ex, "BMonthlyExceptionShifts", "GetMonthlyExceptionShiftsListByQuickSerch");
        //        throw ex;
        //    }
        //}
        //public IList<MonthlyExceptionShiftProxy> GetMonthlyExceptionShiftsListByAdvancedSearch(int year, int month, PersonAdvanceSearchProxy personAdvanceSearchProxy, int pageIndex, int pageSize)
        //{
        //    try
        //    {
        //        IList<Person> PersonnelList = this.PersonSearchBusiness.GetPersonInAdvanceSearchApplyCulture(personAdvanceSearchProxy, pageIndex, pageSize);
        //        IList<MonthlyExceptionShiftProxy> MonthlyExceptionShiftProxyList = this.GetMonthlyExceptionShiftProxyList(year, month, PersonnelList);
        //        return MonthlyExceptionShiftProxyList;
        //    }
        //    catch (Exception ex)
        //    {
        //        BaseBusiness<ShiftException>.LogException(ex, "BMonthlyExceptionShifts", "GetMonthlyExceptionShiftsListByAdvancedSearch");
        //        throw ex;
        //    }
        //}

        private IList<MonthlyExceptionShiftProxy> GetMonthlyExceptionShiftProxyList(int year, int month, string searchValue, int pageIndex, int pageSize)
        {
            IList<MonthlyExceptionShiftProxy> MonthlyExceptionShiftProxyList = new List<MonthlyExceptionShiftProxy>();
            IList<DateTime> MonthDatesList = this.GetMonthDatesList(year, month);
            IList<Person> PersonnelList = this.PersonSearchBusiness.QuickSearchByPageApplyCultureMonthlyExceptionShift(pageIndex, pageSize, searchValue, MonthDatesList);
            foreach (Person personItem in PersonnelList)
            {
                MonthlyExceptionShiftProxy monthlyExceptionShiftProxy = new MonthlyExceptionShiftProxy();
                monthlyExceptionShiftProxy.ID = Guid.NewGuid().ToString();
                monthlyExceptionShiftProxy.PersonID = personItem.ID;
                monthlyExceptionShiftProxy.PersonCode = personItem.PersonCode;
                monthlyExceptionShiftProxy.PersonName = personItem.FirstName + " " + personItem.LastName;
                for (int i = 0; i < MonthDatesList.Count(); i++)
                {
                    ShiftException exceptionShift = this.objectRep.GetPersonnelExceptionShiftByDate(personItem.ID, MonthDatesList[i]);
                    PropertyInfo propertyInfo = typeof(MonthlyExceptionShiftProxy).GetProperty("Day" + (i + 1) + "");
                    propertyInfo.SetValue(monthlyExceptionShiftProxy, exceptionShift != null && exceptionShift.Shift != null ? exceptionShift.Shift.CustomCode : string.Empty, null);
                }
                MonthlyExceptionShiftProxyList.Add(monthlyExceptionShiftProxy);
            }
            return MonthlyExceptionShiftProxyList;
        }

        public IList<DateTime> GetMonthDatesList(int year, int month)
        {
            IList<DateTime> MonthDatesList = new List<DateTime>();
            switch (BLanguage.CurrentSystemLanguage)
            {
                case LanguagesName.Parsi:
                    PersianCalendar pCal = new PersianCalendar();
                    for (int i = 1; i <= pCal.GetDaysInMonth(year, month); i++)
                    {
                        MonthDatesList.Add(pCal.ToDateTime(year, month, i, 0, 0, 0, 0));
                    }
                    break;
                case LanguagesName.English:
                    GregorianCalendar gCal = new GregorianCalendar();
                    for (int j = 1; j <= gCal.GetDaysInMonth(year, month); j++)
                    {
                        MonthDatesList.Add(gCal.ToDateTime(year, month, j, 0, 0, 0, 0));
                    }
                    break;
            }
            return MonthDatesList;
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckMonthlyExceptionShiftsLoadAccess()
        {
        }

        public void UpdatePersonnelMonthlyExceptionShifts(decimal personnelID, IList<DateTime> MonthDatesList, IList<string> DaysShiftList, out int ExceptionShiftFailedCount)
        {
            try
            {
                UIValidationExceptions exceptions = new UIValidationExceptions();
                ShiftException shiftException = null;
                ExceptionShiftFailedCount = 0;
                for (int i = 0; i < DaysShiftList.Count(); i++)
                {
                    try
                    {

                        Shift shift = this.objectRep.GetShiftByCustomCode(DaysShiftList[i]);
                        if (shift == null && DaysShiftList[i] != string.Empty)
                        {
                            ValidationException exception = new ValidationException(ExceptionResourceKeys.ShiftWithThisCustomCodeNotExists, "شیفت با این کد موجود نمی باشد", ExceptionSrc);
                            exception.Data.Add("Info", DaysShiftList[i]);
                            exceptions.Add(exception);
                        }
                        else
                        {
                            shiftException = this.objectRep.GetPersonnelExceptionShiftByDate(personnelID, MonthDatesList[i]);
                            //اگر شیفت وارد شده در یک روز با شیفت قبلی مغایرت داشته باشد یا شیفت در یک روز خاص به دلیل جابه جایی شیفت ها در فرم شیفت های استثنا از بین رفته باشد عملیات حذف انجام شود
                            if ((shiftException != null && shiftException.Shift != null && shiftException.Shift.CustomCode != DaysShiftList[i]) || (DaysShiftList[i] != string.Empty && shiftException != null && shiftException.Shift == null))
                                base.SaveChanges(shiftException, UIActionType.DELETE);
                            if (DaysShiftList[i] != string.Empty && ((shiftException != null && shiftException.Shift != null && shiftException.Shift.CustomCode != DaysShiftList[i]) || (shiftException == null) || (shiftException != null && shiftException.Shift == null)))
                            {
                                shiftException = new ShiftException();
                                shiftException.RegistrationDate = DateTime.Now.Date;
                                shiftException.Date = MonthDatesList[i];
                                shiftException.Person = new Person() { ID = personnelID };
                                shiftException.Shift = new Shift() { ID = shift.ID };
                                shiftException.UserID = BUser.CurrentUser.ID;
                                base.SaveChanges(shiftException, UIActionType.ADD);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ExceptionShiftFailedCount++;
                        Exception exception = new Exception(ex.Message.ToString());
                        BaseBusiness<Entity>.LogException(exception);
                    }
                }
                if (exceptions.Count > 0)
                    throw exceptions;
            }
            catch (Exception ex)
            {
                BaseBusiness<ShiftException>.LogException(ex, "BMonthlyExceptionShifts", "UpdatePersonnelMonthlyExceptionShifts");
                throw ex;
            }
        }
        //private string GetErrorTitleForInsertCollectiveRequest(ShiftException shiftException)
        //{
        //    string message = string.Empty;
        //    string precardName = new BPrecard().GetByID(request.Precard.ID).Name;
        //    string registTypeStr = string.Empty;
        //    switch (registType)
        //    {
        //        case OperatorRegistType.Request:
        //            switch (BLanguage.CurrentLocalLanguage)
        //            {
        //                case LanguagesName.Unknown:
        //                    break;
        //                case LanguagesName.Parsi:
        //                    registTypeStr = "درخواست";
        //                    break;
        //                case LanguagesName.English:
        //                    registTypeStr = "Request";
        //                    break;
        //                default:
        //                    break;
        //            }
        //            break;
        //        case OperatorRegistType.Permit:
        //            switch (BLanguage.CurrentLocalLanguage)
        //            {
        //                case LanguagesName.Unknown:
        //                    break;
        //                case LanguagesName.Parsi:
        //                    registTypeStr = "مجوز";
        //                    break;
        //                case LanguagesName.English:
        //                    registTypeStr = "Permit";
        //                    break;
        //                default:
        //                    break;
        //            }
        //            break;
        //        default:
        //            break;
        //    }
        //    switch (BLanguage.CurrentLocalLanguage)
        //    {
        //        case LanguagesName.Unknown:
        //            message = " خطا در ثبت " + registTypeStr + " انبوه - " + "(" + "شماره پرسنلی : " + request.Person.BarCode + " --- " + "نام و نام خانوادگی : " + request.Person.Name + " --- " + "نوع درخواست : " + precardName + " --- " + "تاریخ : " + request.TheFromDate + ") --- ";
        //            break;
        //        case LanguagesName.Parsi:
        //            message = " خطا در ثبت " + registTypeStr + " انبوه - " + "(" + "شماره پرسنلی : " + request.Person.BarCode + " --- " + "نام و نام خانوادگی : " + request.Person.Name + " --- " + "نوع درخواست : " + precardName + " --- " + "تاریخ : " + request.TheFromDate + ") --- ";
        //            break;
        //        case LanguagesName.English:
        //            message = "error in regist " + registTypeStr + " collective - " + "(" + "Barcode : " + request.Person.BarCode + " --- " + "Name : " + request.Person.Name + " --- " + "Precard Name : " + precardName + " --- " + "Date : " + request.TheFromDate + ") --- ";

        //            break;
        //        default:
        //            break;
        //    }

        //    return message;
        //}

        public IList<string> UpdatePersonnelPeriodRepeateMonthlyExceptionShifts(int Year, int Month, int FromDay, int ToDay, int PersonId, IList<decimal> HolidaysList, IList<string> DaysShiftList, IList<DateTime> MonthDatesList, out int ExceptionShiftFailedCount)
        {
            try
            {
                IList<string> DayPeriodRepeatList = new List<string>();
                int Index = 0;
                int j = 0;

                List<CalendarCellInfo> holidays = new List<CalendarCellInfo>();
                if (HolidaysList != null)
                {
                    foreach (decimal id in HolidaysList)
                    {
                        holidays.AddRange(new BCalendarType().GetCalendarList(Year, id));
                    }
                }
                if (FromDay > ToDay)
                {
                    UIValidationExceptions exception = new UIValidationExceptions();
                    exception.Add(ExceptionResourceKeys.FromdayElderToDay, "تاریخ شروع بزرگتر از تاریخ پایان می باشد", ExceptionSrc);
                    throw exception;
                }
                else
                {
                    #region "گرفتن کد شیفتهای از روز تا روز و سپس تکرار شیفتها در روزهای دیگر ماه"
                    for (j = FromDay - 1; j < ToDay; j++)
                    {
                        DayPeriodRepeatList.Add(DaysShiftList[j]);
                        Index++;
                    }
                    if (DayPeriodRepeatList.Count != 0)
                    {
                        int PriodRepeatCount = Convert.ToInt32(Math.Ceiling((double)DaysShiftList.Count() / (double)Index));
                        for (int prCount = 1; prCount <= PriodRepeatCount; prCount++)
                        {
                            for (int i = 0; i < Index; i++)
                            {
                                if (j < DaysShiftList.Count())
                                {
                                    DaysShiftList[j] = DayPeriodRepeatList[i];
                                    j++;
                                }
                            }
                        }
                    }
                    #endregion
                    #region "بیرون کشیدن تعطیلات از لیست شیفت های اختصاص داده شده به یک پرسنل در یک ماه خاص"
                    ExceptionShiftFailedCount = 0;
                    for (int day = 0; day < DaysShiftList.Count(); day++)
                    {
                        try
                        {
                            Shift shift = this.objectRep.GetShiftByCustomCode(DaysShiftList[day]);
                            ShiftException shiftException = this.objectRep.GetPersonnelExceptionShiftByDate(PersonId, MonthDatesList[day]);
                            //اگر شیفت وارد شده در یک روز با شیفت قبلی مغایرت داشته باشد یا حذف تعطیلات انتخاب شده باشد و روز مربوطه تعطیل باشد شرط اجرا شود
                            if ((shiftException != null && shiftException.Shift != null && shiftException.Shift.CustomCode != DaysShiftList[day]) || (shiftException != null && holidays != null && holidays.Where(x => x.Day == (day + 1) && x.Month == Month).Count() != 0) || (DaysShiftList[day] != string.Empty && shiftException != null && shiftException.Shift == null))
                                base.SaveChanges(shiftException, UIActionType.DELETE);
                            if (holidays.Where(x => x.Day == (day + 1) && x.Month == Month).Count() == 0)
                            {
                                if (DaysShiftList[day] != string.Empty && ((shiftException != null && shiftException.Shift != null && shiftException.Shift.CustomCode != DaysShiftList[day]) || (shiftException == null) || (shiftException != null && shiftException.Shift == null)))
                                {
                                    shiftException = new ShiftException();
                                    shiftException.RegistrationDate = DateTime.Now.Date;
                                    shiftException.Date = MonthDatesList[day];
                                    shiftException.Person = new Person() { ID = PersonId };
                                    shiftException.Shift = new Shift() { ID = shift.ID };
                                    shiftException.UserID = BUser.CurrentUser.ID;
                                    base.SaveChanges(shiftException, UIActionType.ADD);
                                }
                            }
                            else
                            {
                                DaysShiftList[day] = string.Empty;
                            }
                        }
                        catch (Exception ex)
                        {
                            ExceptionShiftFailedCount++;
                            Exception exception = new Exception(ex.Message.ToString());
                            BaseBusiness<Entity>.LogException(exception);
                        }

                    }
                    #endregion
                }

                return DaysShiftList;
            }
            catch (Exception ex)
            {
                BaseBusiness<ShiftException>.LogException(ex, "BMonthlyExceptionShifts", "UpdatePersonnelPeriodRepeateMonthlyExceptionShifts");
                throw ex;
            }
        }

        public void DeletePersonnelMonthlyExceptionShifts(decimal personnelID, IList<DateTime> MonthDatesList)
        {
            try
            {
                for (int i = 0; i < MonthDatesList.Count(); i++)
                {
                    ShiftException shiftException = this.objectRep.GetPersonnelExceptionShiftByDate(personnelID, MonthDatesList[i]);
                    if (shiftException != null)
                        base.Delete(shiftException);
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<ShiftException>.LogException(ex, "BMonthlyExceptionShifts", "DeletePersonnelMonthlyExceptionShifts");
                throw ex;
            }
        }
        public IList<ShiftExceptionProxy> GetShiftExceptionProxy(decimal personId, string firstDate, string secondDate)
        {
            DateTime FirstDate, SecondDate;
            ShiftException shiftExceptionAlias = null;
            UIValidationExceptions exception = new UIValidationExceptions();
            IList<ShiftExceptionProxy> shiftExceptionProxyList = new List<ShiftExceptionProxy>();            
            try
            {
                if (personId == 0)
                {
                    exception.Add(ExceptionResourceKeys.ExceptionShiftPersonIdRequierd, "شناسه پرسنل مشخص نشده است", ExceptionSrc);
                }   
                if (firstDate == secondDate)
                {
                    exception.Add(ExceptionResourceKeys.SelectedDateIsSimilar, "تاریخ های انتخاب شده یکی می باشند", ExceptionSrc);
                }
                if (exception.Count > 0)
                {
                    throw exception;
                }
                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    FirstDate = Utility.ToMildiDate(firstDate);
                    SecondDate = Utility.ToMildiDate(secondDate);
                }
                else
                {
                    FirstDate = Utility.ToMildiDateTime(firstDate);
                    SecondDate = Utility.ToMildiDateTime(secondDate);
                }
                IList<ShiftException> ExceptionShiftList = NHibernateSessionManager.Instance.GetSession().QueryOver<ShiftException>(() => shiftExceptionAlias)
                                                                                                         .Where(() => shiftExceptionAlias.Person.ID == personId &&
                                                                                                                      (shiftExceptionAlias.Date == FirstDate || shiftExceptionAlias.Date == SecondDate)
                                                                                                               )
                                                                                                         .List<ShiftException>();
                for (int i = 1; i <= 2; i++)
                {
                    ShiftExceptionProxy shiftExceptionProxy = new ShiftExceptionProxy();
                    if (i == 1)
                    {
                        string shift = this.GetDayShiftByPersonId(personId, firstDate);
                        ShiftException exceptionShift = ExceptionShiftList.Where(x => x.Date == FirstDate).SingleOrDefault();
                        shiftExceptionProxy.ShiftName = shift;
                        shiftExceptionProxy.Date = firstDate;
                        shiftExceptionProxy.First = true;
                        if (exceptionShift != null && exceptionShift.Shift != null)
                            shiftExceptionProxy.ShiftExceptionName = exceptionShift.Shift.Name;
                        else
                            shiftExceptionProxy.ShiftExceptionName = string.Empty;
                        shiftExceptionProxyList.Add(shiftExceptionProxy);
                    }
                    else if (i == 2)
                    {
                        string shift = this.GetDayShiftByPersonId(personId, secondDate);
                        ShiftException exceptionShift = ExceptionShiftList.Where(x => x.Date == SecondDate).SingleOrDefault();
                        shiftExceptionProxy.ShiftName = shift;
                        shiftExceptionProxy.Date = secondDate;
                        shiftExceptionProxy.Second = true;
                        if (exceptionShift != null && exceptionShift.Shift != null)
                            shiftExceptionProxy.ShiftExceptionName = exceptionShift.Shift.Name;
                        else
                            shiftExceptionProxy.ShiftExceptionName = string.Empty;
                        shiftExceptionProxyList.Add(shiftExceptionProxy);
                    }

                }
                return shiftExceptionProxyList;
            }
            catch(Exception ex)
            {
                LogException(ex, "BExceptionShift", "GetShiftExceptionProxy");
                throw ex;
                
            }                       
        }        

        #region BaseBusiness Implementation

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        protected override void InsertValidate(ShiftException obj)
        {
            UIValidationExceptions exception = new UIValidationExceptions();



            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        protected override void UpdateValidate(ShiftException obj)
        {
            UIValidationExceptions exception = new UIValidationExceptions();



            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        protected override void DeleteValidate(ShiftException obj)
        {
            UIValidationExceptions exception = new UIValidationExceptions();



            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        protected override void GetReadyBeforeSave(ShiftException obj, UIActionType action)
        {
            obj.RegistrationDate = DateTime.Now;
            obj.UserID = BUser.CurrentUser.ID;
        }

        protected override void UIValidate(ShiftException obj, UIActionType action)
        {
            UIValidator.DoValidate(obj);
        }

        protected override void Insert(ShiftException obj)
        {
            try
            {
                objectRep.WithoutTransactSave(obj);
            }
            catch (Exception ex)
            {
                LogException(ex, "GTS.Clock.Business-Nhibernate Action");
                throw ex;
            }
        }

        protected override void UpdateCFP(ShiftException obj, UIActionType action)
        {
            base.UpdateCFP(obj.Person.ID, obj.Date);
        }
        #endregion
    }
}
