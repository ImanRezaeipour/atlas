using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Infrastructure;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Model;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Business.Security;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Model.Concepts;
using System.Reflection;
using GTS.Clock.Infrastructure.Validation.Configuration;
using GTS.Clock.Business.Rules;
using GTS.Clock.Business.Assignments;
using GTS.Clock.Infrastructure.NHibernateFramework;

namespace GTS.Clock.Business.ArchiveCalculations
{
    /// <summary>
    /// نتایج محاسبات
    /// </summary>
    public class BArchiveCalculator : BaseBusiness<ArchiveConceptValue>
    {
        const string ExceptionSrc = "GTS.Clock.Business.ArchiveCalculations.BArchiveCalculator";
        const string Empty = "-1000";
        ISearchPerson searchTool = new BPerson();
        ArchiveConceptsRepository archiveRep = new ArchiveConceptsRepository();
        EntityRepository<ArchiveFieldMap> mapRep = new EntityRepository<ArchiveFieldMap>(false);
        CFPRepository cfpRe = new CFPRepository();


        #region UI Meta Data

        /// <summary>
        /// تتظیمات جدول نتایج محاسبات را بر می گرداند
        /// </summary>
        /// <returns>لیست فیلد ها</returns>
        public IList<ArchiveFieldMap> GetArchiveGridSettings()
        {
            try
            {
                IList<ArchiveFieldMap> mapList = mapRep.GetAll();
                foreach (ArchiveFieldMap map in mapList)
                {
                    if (Utility.IsEmpty(map.PId))
                    {
                        map.Visible = false;
                    }
                    else if (!map.PId.ToLower().StartsWith("p"))
                    {
                        map.Visible = false;
                    }

                    if (BLanguage.CurrentLocalLanguage == LanguagesName.Parsi)
                    {
                        map.Title = map.FnTitle;
                        map.ColumnSize = map.FnColumnSize;
                    }
                    else
                    {
                        map.Title = map.EnTitle;
                        map.ColumnSize = map.EnColumnSize;
                    }
                }

                return mapList;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex);
                throw ex;
            }
        }

        /// <summary>
        /// تنظمیات جدول نتایج محاسبات را دخیره سازی می کند
        /// </summary>
        /// <param name="mapList">لیست فیلد ها</param>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void SetArchiveGridSettings(IList<ArchiveFieldMap> mapList)
        {
            try
            {
                IList<ArchiveFieldMap> orginalMapList = mapRep.GetAll();
                foreach (ArchiveFieldMap changedMap in mapList)
                {
                    ArchiveFieldMap map = orginalMapList.Where(x => x.PId == changedMap.PId).FirstOrDefault();
                    if (map == null)
                        continue;
                    if (map.Visible != changedMap.Visible)
                    {
                        map.Visible = changedMap.Visible;
                        mapRep.Update(map);
                    }
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex);
                throw ex;
            }
        }

        #endregion

        #region Count & Paging

        /// <summary>
        /// تعداد مقادیر نتایج محاسبات جستجو شده را بر می گرداند
        /// </summary>
        /// <param name="searchKey">کلید جستجو</param>
        /// <param name="year">سال</param>
        /// <param name="month">ماه</param>
        /// <returns>تعداد یافت شده</returns>
        public int GetSearchCount(string searchKey, int year, int month)
        {
            try
            {
                int rangeOrder = month;
                ISearchPerson searchTool = new BPerson();
                int totalCount = searchTool.GetPersonInQuickSearchCount(searchKey);
                IList<Person> personList = searchTool.QuickSearchByPage(0, totalCount, searchKey);
                //personList = this.CheckMaxArrayParamCount(personList);
                var ids = from o in personList
                          select o.ID;
                IList<decimal> personIds = archiveRep.GetExsitsArchivePersons(ids.ToList(), year, rangeOrder);
                return personIds.Count;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex);
                throw ex;
            }
        }

        /// <summary>
        /// تعداد مقادیر نتایج محاسبات جستجو شده را بر می گرداند
        /// </summary>
        /// <param name="proxy">پروکسی جستجوی پیشرفته پرسنل</param>
        /// <param name="year">سال</param>
        /// <param name="month">ماه</param>
        /// <returns>تعداد یافت شده</returns>
        public int GetSearchCount(PersonAdvanceSearchProxy proxy, int year, int month)
        {
            try
            {
                int rangeOrder = month;
                ISearchPerson searchTool = new BPerson();
                IList<Person> personList = searchTool.GetPersonInAdvanceSearch(proxy);
                //personList = this.CheckMaxArrayParamCount(personList);
                var ids = from o in personList
                          select o.ID;
                IList<decimal> personIds = archiveRep.GetExsitsArchivePersons(ids.ToList(), year, rangeOrder);
                return personIds.Count;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex);
                throw ex;
            }
        }
        /*  
             public IList<Person> GetSearchResult(string searchKey, int year, int month, int pageIndex, int pageSize)
             {
                 try
                 {
                     int rangeOrder = month;
                     ISearchPerson searchTool = new BPerson();
                     int totalCount = searchTool.GetPersonInQuickSearchCount(searchKey);
                     IList<Person> personList = searchTool.QuickSearchByPage(0, totalCount, searchKey);
                     var ids = from o in personList
                               select o.ID;
                     IList<decimal> existsPersonArchiveList = archiveRep.GetExsitsArchivePersons(ids.ToList(), year, rangeOrder);

                     personList = personList.Where(x => existsPersonArchiveList.Any(y => y == x.ID))
                         .Skip(pageIndex * pageSize)
                         .Take(pageSize)
                         .ToList();

                     return personList;
                 }
                 catch (Exception ex)
                 {
                     BaseBusiness<Entity>.LogException(ex);
                     throw ex;
                 }
             }

             public IList<Person> GetSearchResult(PersonAdvanceSearchProxy proxy, int year, int month, int pageIndex, int pageSize)
             {
                 try
                 {
                     int rangeOrder = month;
                     ISearchPerson searchTool = new BPerson();
                     IList<Person> personList = searchTool.GetPersonInAdvanceSearch(proxy);
                     var ids = from o in personList
                               select o.ID;
                     IList<decimal> existsPersonArchiveList = archiveRep.GetExsitsArchivePersons(ids.ToList(), year, rangeOrder);

                     personList = personList.Where(x => existsPersonArchiveList.Any(y => y == x.ID))
                         .Skip(pageIndex * pageSize)
                         .Take(pageSize)
                         .ToList();

                     return personList;
                 }
                 catch (Exception ex)
                 {
                     BaseBusiness<Entity>.LogException(ex);
                     throw ex;
                 }
             }
          */
        #endregion

        #region Check Archive & Archive

        /// <summary>
        /// آیا داده های اشخاص مشخص شده قبلا نتایج محاسبات شده اند
        /// </summary>
        /// <param name="year">سال</param>
        /// <param name="month">ماه</param>
        /// <param name="personId">کد پرسنلی</param>
        /// <returns>وضعیت نتایج محاسبات</returns>
        public ArchiveExistsConditions IsArchiveExsits(int year, int month, decimal personId)
        {
            Person prs = new BPerson().GetByID(personId);
            IList<Person> list = new List<Person>();
            list.Add(prs);
            NHibernateSessionManager.Instance.GetSession().Evict(prs);
            return this.IsArchiveExsits(year, month, list);
        }

        /// <summary>
        /// آیا داده های اشخاص مشخص شده قبلا نتایج محاسبات شده اند
        /// </summary>
        /// <param name="year">سال</param>
        /// <param name="month">ماه</param>
        /// <param name="searchKey">عبارت جستجو</param>
        /// <returns>وضعیت نتایج محاسبات</returns>
        public ArchiveExistsConditions IsArchiveExsits(int year, int month, string searchKey)
        {
            int count = searchTool.GetPersonInQuickSearchCount(searchKey);
            IList<Person> list = searchTool.QuickSearchByPage(0, count, searchKey);
            return this.IsArchiveExsits(year, month, list);
        }

        /// <summary>
        /// آیا داده های اشخاص مشخص شده قبلا نتایج محاسبات شده اند
        /// </summary>
        /// <param name="year">سال</param>
        /// <param name="month">ماه</param>
        /// <param name="proxy"></param>
        /// <returns>وضعیت نتایج محاسبات</returns>
        public ArchiveExistsConditions IsArchiveExsits(int year, int month, PersonAdvanceSearchProxy proxy)
        {
            int count = searchTool.GetPersonInAdvanceSearchCount(proxy);
            IList<Person> list = searchTool.GetPersonInAdvanceSearch(proxy, 0, count);
            return this.IsArchiveExsits(year, month, list);
        }

        /// <summary>
        /// پس از حذف نتایج محاسبات قبلی , دادههای جدید را کپی میکند
        /// </summary>
        /// <param name="year">سال</param>
        /// <param name="month">ماه</param>
        /// <param name="personId">کد پرسنلی</param>
        /// <param name="overwrite">بازنویسی</param>
        /// <returns>انجام شد/انجام نشد</returns>
        public bool ArchiveData(int year, int month, decimal personId, bool overwrite)
        {
            Person prs = new BPerson().GetByID(personId);
            IList<Person> list = new List<Person>();
            list.Add(prs);
            NHibernateSessionManager.Instance.GetSession().Evict(prs);
            bool result = this.ArchiveData(year, month, list, overwrite);
            return result;
        }

        /// <summary>
        /// پس از حذف نتایج محاسبات قبلی , دادههای جدید را کپی میکند
        /// </summary>
        /// <param name="year">سال</param>
        /// <param name="month">ماه</param>
        /// <param name="searchKey">کلید جستجو</param>
        /// <param name="overwrite">بازنویسی</param>
        /// <returns>انجام شد/انجام نشد</returns>
        public bool ArchiveData(int year, int month, string searchKey, bool overwrite)
        {
            int count = searchTool.GetPersonInQuickSearchCount(searchKey);
            IList<Person> list = searchTool.QuickSearchByPage(0, count, searchKey);
            bool result = this.ArchiveData(year, month, list, overwrite);
            return result;
        }

        /// <summary>
        /// پس از حذف نتایج محاسبات قبلی , دادههای جدید را کپی میکند
        /// </summary>
        /// <param name="year">سال</param>
        /// <param name="month">ماه</param>
        /// <param name="proxy"></param>
        ///<param name="overwrite">بازنویسی</param>
        /// <returns>انجام شد/انجام نشد</returns>
        public bool ArchiveData(int year, int month, PersonAdvanceSearchProxy proxy, bool overwrite)
        {
            int count = searchTool.GetPersonInAdvanceSearchCount(proxy);
            IList<Person> list = searchTool.GetPersonInAdvanceSearch(proxy, 0, count);
            bool result = this.ArchiveData(year, month, list, overwrite);
            return result;
        }

        #endregion

        #region Load Archive Values

        /// <summary>
        /// لیست نتایج محاسبات محاسبات یک پرسنل را بر می گرداند
        /// </summary>
        /// <param name="year">سال</param>
        /// <param name="month">ماه</param>
        /// <param name="personId">کد پرسنلی</param>
        /// <returns>لیست نتایج محاسبات محاسبات</returns>
        public IList<ArchiveCalcValuesProxy> GetArchiveValues(int year, int month, decimal personId)
        {
            Person prs = new BPerson().GetByID(personId);
            IList<Person> list = new List<Person>();
            list.Add(prs);

            return this.GetArchiveValues(year, month, list);
        }

        /// <summary>
        /// لیست نتایج محاسبات محاسبات  را به صورت صفحه بندی شده بر می گرداند 
        /// </summary>
        /// <param name="year">سال</param>
        /// <param name="month">ماه</param>
        /// <param name="searchKey">کلید جستجو</param>
        /// <param name="pageIndex">شماره صفحه</param>
        /// <param name="pageSize">اندازه هر صفحه</param>
        /// <returns>لیست نتایج محاسبات محاسبات</returns>
        public IList<ArchiveCalcValuesProxy> GetArchiveValues(int year, int month, string searchKey, int pageIndex, int pageSize)
        {
            int rangeOrder = month;
            ISearchPerson searchTool = new BPerson();
            int totalCount = searchTool.GetPersonInQuickSearchCount(searchKey);
            IList<Person> personList = searchTool.QuickSearchByPage(0, totalCount, searchKey);
            //personList = this.CheckMaxArrayParamCount(personList);
            var ids = from o in personList
                      select o.ID;
            IList<decimal> existsPersonArchiveList = archiveRep.GetExsitsArchivePersons(ids.ToList(), year, rangeOrder);

            personList = personList.Where(x => existsPersonArchiveList.Any(y => y == x.ID)).OrderBy(o => o.BarCode)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToList();

            return this.GetArchiveValues(year, month, personList);
        }

        /// <summary>
        /// لیست نتایج محاسبات محاسبات  را به صورت صفحه بندی شده بر می گرداند  
        /// </summary>
        /// <param name="year">سال</param>
        /// <param name="month">ماه</param>
        /// <param name="proxy"></param>
        /// <param name="pageIndex">شماره صفحه</param>
        /// <param name="pageSize">اندازه هر صفحه</param>
        /// <returns>لیست نتایج محاسبات محاسبات</returns>
        public IList<ArchiveCalcValuesProxy> GetArchiveValues(int year, int month, PersonAdvanceSearchProxy proxy, int pageIndex, int pageSize)
        {
            //int count = searchTool.GetPersonInAdvanceSearchCount(proxy);
            //IList<Person> list = searchTool.GetPersonInAdvanceSearch(proxy, 0, count);
            int rangeOrder = month;
            ISearchPerson searchTool = new BPerson();
            IList<Person> personList = searchTool.GetPersonInAdvanceSearch(proxy);
            //personList = this.CheckMaxArrayParamCount(personList);
            var ids = from o in personList
                      select o.ID;
            IList<decimal> existsPersonArchiveList = archiveRep.GetExsitsArchivePersons(ids.ToList(), year, rangeOrder);

            personList = personList.Where(x => existsPersonArchiveList.Any(y => y == x.ID)).OrderBy(o => o.BarCode)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToList();

            return this.GetArchiveValues(year, month, personList);
        }

        #endregion

        #region Edit Data

        /// <summary>
        /// مقادیر نتایج محاسبات محاسبات را ذخیره می کند
        /// </summary>
        /// <param name="year">سال</param>
        /// <param name="month">ماه</param>
        /// <param name="personId">کد پرسنلی</param>
        /// <param name="proxy">پروکسی مقادیر نتایج محاسبات محاسبات</param>
        /// <returns>پروکسی مقادیر نتایج محاسبات محاسبات</returns>

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public ArchiveCalcValuesProxy SetArchiveValues(int year, int month, decimal personId, ArchiveCalcValuesProxy proxy)
        {
            IList<ArchiveCalcValuesProxy> resultList = new List<ArchiveCalcValuesProxy>();
            int rangeOrder = month;
            try
            {
                UIValidate(personId);
                proxy = ValidateKaheshiKosuratBankSepah(proxy, year, month, personId);
            }
            catch (Exception)
            {


            }



            IList<ArchiveConceptValue> archiveValues = archiveRep.LoadArchiveValueList(new List<decimal>() { personId }, year, rangeOrder);

            IList<ArchiveFieldMap> fildsMapList = mapRep.GetAll();

            if (archiveValues.Any(x => x.PersonId == personId))
            {
                ArchiveFieldMap map = null;
                MemberInfo[] members = proxy.GetType().GetMembers();

                #region Data Type Validatin
                UIValidationExceptions exceptions = new UIValidationExceptions();
                foreach (MemberInfo member in members.Where(a => a.MemberType == MemberTypes.Property && !a.Name.ToLower().Contains("person")))
                {
                    PropertyInfo prop = typeof(ArchiveCalcValuesProxy).GetProperty(member.Name);

                    map = fildsMapList.Where(x => x.PId.ToLower().Equals(member.Name.ToLower())).FirstOrDefault();
                    if (map != null && map.Visible)
                    {
                        ArchiveConceptValue cnpValue = archiveValues.Where(x => x.PersonId == personId && x.Concept.KeyColumnName.ToLower().Equals(map.ConceptKeyColumn.ToLower())).FirstOrDefault();

                        if (cnpValue != null)
                        {
                            bool isValid = false;
                            string pValue = Utility.ToString(prop.GetValue(proxy, null)).Trim();
                            if (Utility.IsEmpty(pValue))
                                pValue = "0";
                            int value = 0;
                            string cnpName = "";
                            if (BLanguage.CurrentLocalLanguage == LanguagesName.Parsi)
                            {
                                cnpName = map.FnTitle;
                            }
                            else
                            {
                                cnpName = map.EnTitle;
                            }
                            switch (cnpValue.Concept.DataType)
                            {
                                case ConceptDataType.Int:
                                    if (!Utility.IsIntiger(pValue))
                                    {
                                        ValidationException exception = new ValidationException(ExceptionResourceKeys.ArchiveDataTypeIsNotValid, cnpName, ExceptionSrc);
                                        exception.Data.Add("Info", cnpName);
                                        exceptions.Add(exception);
                                    }
                                    break;
                                case ConceptDataType.Hour:
                                    if (!Utility.IsTime(pValue) && !Utility.IsIntiger(pValue))
                                    {
                                        ValidationException exception = new ValidationException(ExceptionResourceKeys.ArchiveDataTypeIsNotValid, cnpName, ExceptionSrc);
                                        exception.Data.Add("Info", cnpName);
                                        exceptions.Add(exception);
                                    }
                                    break;
                            }
                            if (isValid)
                            {
                                cnpValue.ChangedValue = value;
                                archiveRep.SaveOrUpdate(cnpValue);
                            }
                        }
                    }
                }
                if (exceptions.Count > 0)
                    throw exceptions;
                #endregion

                #region Set Value
                foreach (MemberInfo member in members.Where(a => a.MemberType == MemberTypes.Property && !a.Name.ToLower().Contains("person")))
                {
                    PropertyInfo prop = typeof(ArchiveCalcValuesProxy).GetProperty(member.Name);

                    map = fildsMapList.Where(x => x.PId.ToLower().Equals(member.Name.ToLower())).FirstOrDefault();
                    if (map != null && map.Visible)
                    {
                        ArchiveConceptValue cnpValue = archiveValues.Where(x => x.PersonId == personId && x.Concept.KeyColumnName.ToLower().Equals(map.ConceptKeyColumn.ToLower())).FirstOrDefault();

                        if (cnpValue != null)
                        {
                            bool isValid = false;
                            string pValue = Utility.ToString(prop.GetValue(proxy, null)).Trim();
                            if (Utility.IsEmpty(pValue))
                                pValue = "0";
                            int value = 0;
                            switch (cnpValue.Concept.DataType)
                            {
                                case ConceptDataType.Int:
                                    if (Utility.IsIntiger(pValue))
                                    {
                                        value = Utility.ToInteger(pValue);
                                        isValid = true;
                                    }
                                    break;
                                case ConceptDataType.Hour:
                                    if (Utility.IsIntiger(pValue))
                                    {
                                        pValue = pValue += ":00";
                                    }
                                    if (Utility.IsTime(pValue))
                                    {
                                        value = Utility.RealTimeToIntTime(pValue);
                                        isValid = true;
                                    }
                                    break;
                            }
                            if (isValid)
                            {
                                cnpValue.ModifiedDate = DateTime.Now;
                                cnpValue.ModifiedPersonId = BUser.CurrentUser.Person.ID;
                                cnpValue.ChangedValue = value;
                                archiveRep.SaveOrUpdate(cnpValue);
                                BArchiveCalculator.LogUserAction(cnpValue, string.Format("Change Archive for person {0} , Date: {2}/{1}", cnpValue.PersonId.ToString(), month, year));
                            }
                        }
                    }
                }
                #endregion
            }

            return proxy;
        }

        #endregion

        #region Private Services

        /// <summary>
        /// پس از حذف نتایج محاسبات قبلی , دادههای جدید را کپی میکند
        /// بعلت محدودیت در تعداد پارامتر , دسته دسته نتایج محاسبات میشود
        /// </summary>
        /// <param name="year">سال</param>
        /// <param name="month">ماه</param>
        /// <param name="personList">لیست پرسنل</param>
        /// <param name="overwrite">بازنویسی</param>
        /// <returns>انجام  شد/انجام نشد</returns>
        private bool ArchiveData(int year, int month, IList<Person> personList, bool overwrite)
        {
            //DNN Note
            // ابتدا بررسی می کند که برای اشخاص محاسبات  در این روز انجام شده است یا خیر
            var CFPList = cfpRe.GetByPersonIDList(personList.Select(c => c.ID).ToList()).ToList();
            var validCFPCount = CFPList.Where(c => c.CalculationIsValid == true && c.Date.Date >= DateTime.Now.Date).Count();
            if (validCFPCount < personList.Count)
            {

                UIValidationExceptions exception = new UIValidationExceptions();
                exception.Add(new ValidationException(ExceptionResourceKeys.PersonCalculationRequied, string.Format("نشانگر محاسبات پرسنل برای {0} نفر بروز نمی باشد, ابتدا انجام محاسبات اجرا شود", personList.Count - validCFPCount), ExceptionSrc));
                throw exception;
            }
            //----------------------------------------------------
            try
            {
                DateTime date = new DateTime(year, month, Utility.GetEndOfMiladiMonth(year, month));
                int rangeOrder = month;

                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    date = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, month, Utility.GetEndOfPersianMonth(year, month)));
                }
                int ofset = 2000;
                for (int i = 0; i < personList.Count; i += ofset)
                {


                    var ids = from o in personList
                              .Skip(i)
                              .Take(ofset)
                              select o.ID;

                    if (overwrite)
                    {
                        archiveRep.DeleteArchiveValues(ids.ToList(), year, month);
                    }
                    foreach (decimal id in ids)
                    {
                        if (!overwrite && this.IsArchiveExsits(year, month, id) != ArchiveExistsConditions.NotExists)
                        {
                            continue;
                        }
                        archiveRep.ArchiveConceptValues(id, year, rangeOrder, date, BUser.CurrentUser.Person.ID);
                        //DNN Note  
                        var person = personList.Where(c => c.ID == id).First();
                        string info = string.Format("نتایج محاسبات برای پرسنل {0} با کد {1} مربوط به ماه {2} سال {3} آرشیو گردید", person.Name, person.BarCode, month, year);
                        base.LogUserAction(info, "Archive", BUser.CurrentUser.UserName, true);
                    }

                }
                return true;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex);
                throw ex;
            }
        }

        /// <summary>
        /// آیا داده های اشخاص مشخص شده قبلا نتایج محاسبات شده اند
        /// </summary>
        /// <param name="year">سال</param>
        /// <param name="month">ماه</param>
        /// <param name="personList">لیست پرسنل</param>
        /// <returns>وضعیت نتایج محاسبات</returns>
        private ArchiveExistsConditions IsArchiveExsits(int year, int month, IList<Person> personList)
        {
            try
            {
                //personList = this.CheckMaxArrayParamCount(personList);
                int rangeOrder = month;
                var ids = from o in personList
                          select o.ID;
                int count = archiveRep.ExsitsArchiveCount(ids.ToList(), year, rangeOrder);
                if (count == 0)
                    return ArchiveExistsConditions.NotExists;
                else if (count >= ids.Count())
                {
                    return ArchiveExistsConditions.AllExists;
                }
                else return ArchiveExistsConditions.SomeExists;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex);
                throw ex;
            }
        }

        /// <summary>
        /// نتایج محاسبات را بارگزاری میکند
        /// </summary>
        /// <param name="year">سال</param>
        /// <param name="month">ماه</param>
        /// <param name="personList">لیست پرسنل</param>
        /// <returns>لیست پروکسی مقادیر نتایج محاسبات محاسبات</returns>
        private IList<ArchiveCalcValuesProxy> GetArchiveValues(int year, int month, IList<Person> personList)
        {
            try
            {
                //personList = this.CheckMaxArrayParamCount(personList);
                IList<ArchiveCalcValuesProxy> resultList = new List<ArchiveCalcValuesProxy>();
                DateTime date = new DateTime(year, month, Utility.GetEndOfMiladiMonth(year, month));
                int rangeOrder = month;

                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    date = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, month, Utility.GetEndOfPersianMonth(year, month)));
                }

                var ids = from o in personList
                          select o.ID;
                IList<ArchiveConceptValue> archiveValues = archiveRep.LoadArchiveValueList(ids.ToList(), year, rangeOrder);

                IList<ArchiveFieldMap> fildsMapList = mapRep.GetAll();

                foreach (Person prs in personList)
                {
                    if (archiveValues.Any(x => x.PersonId == prs.ID))
                    {
                        ArchiveCalcValuesProxy proxy = new ArchiveCalcValuesProxy() { PersonId = prs.ID, PersonCode = prs.PersonCode, PersonName = prs.Name };
                        proxy = SetMapValue(proxy, prs.ID, archiveValues, fildsMapList);
                        resultList.Add(proxy);
                    }
                }

                return resultList;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex);
                throw ex;
            }
        }

        /// <summary>
        /// فیلدهایی که باید مقدار بگیرند را مقدار دهی میکند
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="personId">کد پرسنلی</param>
        /// <param name="archiveValues">لیست مقادیر مفاعیم نتایج محاسبات شده</param>
        /// <param name="fildsMapList">فیلد های نتایج محاسبات محسبات</param>
        /// <returns></returns>
        private ArchiveCalcValuesProxy SetMapValue(ArchiveCalcValuesProxy proxy, decimal personId, IList<ArchiveConceptValue> archiveValues, IList<ArchiveFieldMap> fildsMapList)
        {
            try
            {
                ArchiveFieldMap map = null;
                MemberInfo[] members = proxy.GetType().GetMembers();

                foreach (MemberInfo member in members.Where(a => a.MemberType == MemberTypes.Property && !a.Name.ToLower().Contains("person")))
                {
                    PropertyInfo prop = typeof(ArchiveCalcValuesProxy).GetProperty(member.Name);
                    map = fildsMapList.Where(x => x.PId.ToLower().Equals(member.Name.ToLower())).FirstOrDefault();
                    prop.SetValue(proxy, Empty, null);

                    if (map != null)
                    {
                        ArchiveConceptValue cnpValue = archiveValues.Where(x => x.PersonId == personId && x.Concept.KeyColumnName.ToLower().Equals(map.ConceptKeyColumn.ToLower())).FirstOrDefault();

                        if (cnpValue != null)
                        {
                            string value = Utility.ToString(cnpValue.ChangedValue);
                            switch (cnpValue.Concept.DataType)
                            {
                                case ConceptDataType.Int:
                                    value = Utility.ToInteger(value).ToString();
                                    break;
                                case ConceptDataType.Hour:
                                    value = Utility.IntTimeToTime(Utility.ToInteger(value));
                                    break;
                            }
                            prop.SetValue(proxy, value, null);
                        }
                    }
                }
                proxy.ID = Guid.NewGuid().ToString();
                return proxy;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex);
                throw ex;
            }
        }

        /// <summary>
        /// اگر تعداد پارامتر خیلی زیاد باشد منجر به خطا میشود
        /// </summary>
        /// <param name="personList"></param>
        /// <returns></returns>
        //private IList<Person> CheckMaxArrayParamCount(IList<Person> personList) 
        //{
        //	if (personList == null)
        //		return new List<Person>();
        //	if (personList.Count > 2000) 
        //	{
        //		personList = personList.Take(2000).ToList();
        //	}
        //	return personList;
        //}
        #endregion

        /// <summary>
        /// جهت پیش بینی نیازهای آتی
        /// پیاده سازی نشده است
        /// </summary>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckUpdateCalculationResultLoadAccess()
        {
        }

        /// <summary>
        /// جهت پیش بینی نیازهای آتی
        /// پیاده سازی نشده است
        /// </summary>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckArchiveDataAccess()
        {
        }

        private ArchiveCalcValuesProxy ValidateKaheshiKosuratBankSepah(ArchiveCalcValuesProxy archiveCalcProxyObj, int year, int month, decimal personId)
        {
            try
            {
                archiveCalcProxyObj.PersonId = personId;
                PersonRepository personRep = new PersonRepository(false);
                IRuleRepository ruleRep = Rule.GetRuleRepository(false);
                Person personObj = personRep.GetById(personId, false);
                PersonRangeAssignment personRangeAssigmentObj = new BPerson().GetCurrentRangeAssignment(personId);
                IList<CalculationDateRange> calculationDateRangeList = personRangeAssigmentObj.CalcDateRangeGroup.DateRangeList;
                CalculationDateRange calculationDateRangeObj = calculationDateRangeList.FirstOrDefault(f => f.Concept.IdentifierCode == 4005 && f.ToMonth == month);
                int dayRule = calculationDateRangeObj.ToDay;
                DateTime ruledate = Utility.ToMildiDate(year.ToString() + "/" + (month < 10 ? "0" + month.ToString() : month.ToString()).ToString() + "/" + (dayRule < 10 ? "0" + dayRule.ToString() : dayRule.ToString()).ToString());
                personObj.InitializeForAccessRules(ruledate, ruledate);
                int maxOverTime = Convert.ToInt32(personObj.PersonTASpec.R10 == string.Empty ? "0" : personObj.PersonTASpec.R10) * 60;
                AssignedRule ar = personObj.AssignedRuleList.Where(x => x.FromDate <= ruledate && x.ToDate >= ruledate && x.IdentifierCode == 6029).FirstOrDefault();
                int zaribBaje = 1;
                if (ar != null)
                {
                    IList<AssignedRuleParameter> ruleParameterList = ruleRep.GetAssginedRuleParamList(ruledate, ruledate);
                    IList<AssignedRuleParameter> asp = ruleParameterList.Where(x => x.RuleId == ar.RuleId && x.FromDate <= ruledate && x.ToDate >= ruledate).ToList();
                    zaribBaje = 1 + (Utility.ToInteger(asp.SingleOrDefault(a => a.Name == "Third").Value) / 100);

                }

                if (archiveCalcProxyObj.P2 == null || archiveCalcProxyObj.P2.Trim() == string.Empty) archiveCalcProxyObj.P2 = "00:00";
                else if (!archiveCalcProxyObj.P2.Contains(":")) archiveCalcProxyObj.P2 = archiveCalcProxyObj.P2.Trim() + ":00";

                if (archiveCalcProxyObj.P3 == null || archiveCalcProxyObj.P3.Trim() == string.Empty) archiveCalcProxyObj.P3 = "00:00";
                else if (!archiveCalcProxyObj.P3.Contains(":")) archiveCalcProxyObj.P3 = archiveCalcProxyObj.P3.Trim() + ":00";

                if (archiveCalcProxyObj.P4 == null || archiveCalcProxyObj.P4.Trim() == string.Empty) archiveCalcProxyObj.P4 = "00:00";
                else if (!archiveCalcProxyObj.P4.Contains(":")) archiveCalcProxyObj.P4 = archiveCalcProxyObj.P4.Trim() + ":00";

                if (archiveCalcProxyObj.P5 == null || archiveCalcProxyObj.P5.Trim() == string.Empty) archiveCalcProxyObj.P5 = "00:00";
                else if (!archiveCalcProxyObj.P5.Contains(":")) archiveCalcProxyObj.P5 = archiveCalcProxyObj.P5.Trim() + ":00";

                if (archiveCalcProxyObj.P6 == null || archiveCalcProxyObj.P6.Trim() == string.Empty) archiveCalcProxyObj.P6 = "00:00";
                else if (!archiveCalcProxyObj.P6.Contains(":")) archiveCalcProxyObj.P6 = archiveCalcProxyObj.P6.Trim() + ":00";

                if (archiveCalcProxyObj.P7 == null || archiveCalcProxyObj.P7.Trim() == string.Empty) archiveCalcProxyObj.P7 = "00:00";
                else if (!archiveCalcProxyObj.P7.Contains(":")) archiveCalcProxyObj.P7 = archiveCalcProxyObj.P7.Trim() + ":00";

                if (archiveCalcProxyObj.P8 == null || archiveCalcProxyObj.P8.Trim() == string.Empty) archiveCalcProxyObj.P8 = "00:00";
                else if (!archiveCalcProxyObj.P8.Contains(":")) archiveCalcProxyObj.P8 = archiveCalcProxyObj.P8.Trim() + ":00";

                if (archiveCalcProxyObj.P11 == null || archiveCalcProxyObj.P11.Trim() == string.Empty) archiveCalcProxyObj.P11 = "00:00";
                else if (!archiveCalcProxyObj.P11.Contains(":")) archiveCalcProxyObj.P11 = archiveCalcProxyObj.P11.Trim() + ":00";

                if (archiveCalcProxyObj.P12 == null || archiveCalcProxyObj.P12.Trim() == string.Empty) archiveCalcProxyObj.P12 = "00:00";
                else if (!archiveCalcProxyObj.P12.Contains(":")) archiveCalcProxyObj.P12 = archiveCalcProxyObj.P12.Trim() + ":00";



                int jameSotunhayeEzafeKar = (Utility.ToInteger(Utility.RealTimeToIntTime(archiveCalcProxyObj.P2)) * zaribBaje) + Utility.ToInteger(Utility.RealTimeToIntTime(archiveCalcProxyObj.P3)) + Utility.ToInteger(Utility.RealTimeToIntTime(archiveCalcProxyObj.P4)) + Utility.ToInteger(Utility.RealTimeToIntTime(archiveCalcProxyObj.P5)) + Utility.ToInteger(Utility.RealTimeToIntTime(archiveCalcProxyObj.P6)) + Utility.ToInteger(Utility.RealTimeToIntTime(archiveCalcProxyObj.P7) + Utility.ToInteger(Utility.RealTimeToIntTime(archiveCalcProxyObj.P8)));

                if (jameSotunhayeEzafeKar > maxOverTime)
                {
                    int tafavotemaxOverTimeSotunha = jameSotunhayeEzafeKar - maxOverTime;
                    archiveCalcProxyObj.P11 = (Utility.IntTimeToTime(tafavotemaxOverTimeSotunha + Utility.RealTimeToIntTime(archiveCalcProxyObj.P11)));
                    if (Utility.RealTimeToIntTime(archiveCalcProxyObj.P3) > tafavotemaxOverTimeSotunha)
                    {
                        archiveCalcProxyObj.P3 = Utility.IntTimeToTime((Utility.ToInteger(Utility.RealTimeToIntTime(archiveCalcProxyObj.P3)) - tafavotemaxOverTimeSotunha));

                    }
                    else
                    {
                        tafavotemaxOverTimeSotunha = tafavotemaxOverTimeSotunha - (Utility.RealTimeToIntTime(archiveCalcProxyObj.P3) == -1000 ? 0 : Utility.RealTimeToIntTime(archiveCalcProxyObj.P3));
                        archiveCalcProxyObj.P3 = Utility.IntTimeToTime(0);
                        if (Utility.RealTimeToIntTime(archiveCalcProxyObj.P4) > tafavotemaxOverTimeSotunha)
                        {
                            archiveCalcProxyObj.P4 = Utility.IntTimeToTime((Utility.ToInteger(Utility.RealTimeToIntTime(archiveCalcProxyObj.P4)) - tafavotemaxOverTimeSotunha));

                        }
                        else
                        {

                            tafavotemaxOverTimeSotunha = tafavotemaxOverTimeSotunha - (Utility.RealTimeToIntTime(archiveCalcProxyObj.P4) == -1000 ? 0 : Utility.RealTimeToIntTime(archiveCalcProxyObj.P4));
                            archiveCalcProxyObj.P4 = Utility.IntTimeToTime(0);
                            if ((Utility.RealTimeToIntTime(archiveCalcProxyObj.P2) * zaribBaje) > tafavotemaxOverTimeSotunha)
                            {
                                archiveCalcProxyObj.P2 = Utility.IntTimeToTime(Utility.ToInteger((Utility.ToInteger(Utility.RealTimeToIntTime(archiveCalcProxyObj.P2) * zaribBaje) - tafavotemaxOverTimeSotunha) / zaribBaje));

                            }
                            else
                            {

                                tafavotemaxOverTimeSotunha = tafavotemaxOverTimeSotunha - (Utility.RealTimeToIntTime(archiveCalcProxyObj.P2) == -1000 ? 0 : Utility.RealTimeToIntTime(archiveCalcProxyObj.P2) * zaribBaje);
                                archiveCalcProxyObj.P2 = Utility.IntTimeToTime(0);
                                if (Utility.RealTimeToIntTime(archiveCalcProxyObj.P5) > tafavotemaxOverTimeSotunha)
                                {
                                    archiveCalcProxyObj.P5 = Utility.IntTimeToTime((Utility.ToInteger(Utility.RealTimeToIntTime(archiveCalcProxyObj.P5)) - tafavotemaxOverTimeSotunha));

                                }
                                else
                                {

                                    tafavotemaxOverTimeSotunha = tafavotemaxOverTimeSotunha - (Utility.RealTimeToIntTime(archiveCalcProxyObj.P5) == -1000 ? 0 : Utility.RealTimeToIntTime(archiveCalcProxyObj.P5));
                                    archiveCalcProxyObj.P5 = Utility.IntTimeToTime(0);
                                }
                            }
                        }
                    }



                }
                //else

                //    archiveCalcProxyObj.P11 = Utility.IntTimeToTime(0);


                int kosurat = Utility.RealTimeToIntTime(archiveCalcProxyObj.P12);
                if (kosurat > 0)
                {


                    if (Utility.RealTimeToIntTime(archiveCalcProxyObj.P3) > kosurat)
                    {
                        archiveCalcProxyObj.P3 = Utility.IntTimeToTime((Utility.ToInteger(Utility.RealTimeToIntTime(archiveCalcProxyObj.P3)) - kosurat));

                    }
                    else
                    {

                        kosurat = kosurat - (Utility.RealTimeToIntTime(archiveCalcProxyObj.P3) == -1000 ? 0 : Utility.RealTimeToIntTime(archiveCalcProxyObj.P3));
                        archiveCalcProxyObj.P3 = Utility.IntTimeToTime(0);
                        if (Utility.RealTimeToIntTime(archiveCalcProxyObj.P4) > kosurat)
                        {
                            archiveCalcProxyObj.P4 = Utility.IntTimeToTime((Utility.ToInteger(Utility.RealTimeToIntTime(archiveCalcProxyObj.P4)) - kosurat));

                        }
                        else
                        {

                            kosurat = kosurat - (Utility.RealTimeToIntTime(archiveCalcProxyObj.P4) == -1000 ? 0 : Utility.RealTimeToIntTime(archiveCalcProxyObj.P4));
                            archiveCalcProxyObj.P4 = Utility.IntTimeToTime(0);
                            if ((Utility.RealTimeToIntTime(archiveCalcProxyObj.P2) * zaribBaje) > kosurat)
                            {
                                archiveCalcProxyObj.P2 = Utility.IntTimeToTime(Utility.ToInteger((Utility.ToInteger(Utility.RealTimeToIntTime(archiveCalcProxyObj.P2) * zaribBaje) - kosurat) / zaribBaje));

                            }
                            else
                            {

                                kosurat = kosurat - (Utility.RealTimeToIntTime(archiveCalcProxyObj.P2) == -1000 ? 0 : Utility.RealTimeToIntTime(archiveCalcProxyObj.P2) * zaribBaje);
                                archiveCalcProxyObj.P2 = Utility.IntTimeToTime(0);
                                if (Utility.RealTimeToIntTime(archiveCalcProxyObj.P5) > kosurat)
                                {
                                    archiveCalcProxyObj.P5 = Utility.IntTimeToTime((Utility.ToInteger(Utility.RealTimeToIntTime(archiveCalcProxyObj.P5)) - kosurat));

                                }
                                else
                                {

                                    kosurat = kosurat - (Utility.RealTimeToIntTime(archiveCalcProxyObj.P5) == -1000 ? 0 : Utility.RealTimeToIntTime(archiveCalcProxyObj.P5));
                                    archiveCalcProxyObj.P5 = Utility.IntTimeToTime(0);
                                }
                            }
                        }
                    }



                }


                return archiveCalcProxyObj;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex);
                throw ex;
            }
        }

        /// <summary>
        /// جهت پیش بینی نیازهای آتی
        /// پیاده سازی نشده است
        /// </summary>
        /// <param name="clCar"></param>
        protected override void InsertValidate(ArchiveConceptValue clCar)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// جهت پیش بینی نیازهای آتی
        /// پیاده سازی نشده است
        /// </summary>>
        /// <param name="clCar"></param>
        protected override void UpdateValidate(ArchiveConceptValue clCar)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// جهت پیش بینی نیازهای آتی
        /// پیاده سازی نشده است
        /// </summary>
        /// <param name="clCar"></param>
        protected override void DeleteValidate(ArchiveConceptValue clCar)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// اعتبار سنجی با توجه به قوانین واسط کاربری 
        /// </summary>
        /// <param name="personId">کد پرسنلی</param>
        protected void UIValidate(decimal personId)
        {
            CallUIValidator(personId);
        }

        /// <summary>
        /// اعتبار سنجی با توجه به قوانین واسط کاربری
        /// </summary>
        /// <param name="personId">کد پرسنلی</param>
        private void CallUIValidator(decimal personId)
        {
            IArchiveCalculationUIValidation validator = UIValidationFactory.GetRepository<IArchiveCalculationUIValidation>();
            if (validator != null)
            {
                validator.DoValidate(personId);
            }
        }
    }
}
