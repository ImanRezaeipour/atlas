using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Model;
using System.Reflection;
using GTS.Clock.Model.AppSetting;
using GTS.Clock.Infrastructure;
using GTS.Clock.Business.Rules;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Business.Security;
using NHibernate;

namespace GTS.Clock.Business.Assignments
{
    /// <summary>
    /// اختصاص محدوده محاسبات به پرسنل
    /// </summary>
    public class BAssignDateRange : BaseBusiness<PersonRangeAssignment>
    {
        const string ExceptionSrc = "GTS.Clock.Business.Assignments.BAssignDateRange";
        private EntityRepository<PersonRangeAssignment> asignRepository = new EntityRepository<PersonRangeAssignment>(false);
        private SysLanguageResource systemLanguage;
        private ISession NHSession = NHibernateSessionManager.Instance.GetSession();

        /// <summary>
        /// زبان سیستم را مشخص می کند
        /// </summary>
        /// <param name="sysLanguage">زبان سیستم</param>
        public BAssignDateRange(SysLanguageResource sysLanguage)
        {
            systemLanguage = sysLanguage;
        }
        public BAssignDateRange()
        {            
        }
        /// <summary>
        /// زبان سیستم را مشخص می کند
        /// </summary>
        /// <param name="sysLanguage">نام زبان سیستم</param>
        public BAssignDateRange(LanguagesName sysLanguage)
        {
            if (sysLanguage == LanguagesName.Parsi)
                systemLanguage = SysLanguageResource.Parsi;
            else
                systemLanguage = SysLanguageResource.English;
        }

        /// <summary>
        /// آیا قبلا انتساب دوره در یک تاریخ خاص داده شده
        /// جهت استفاده در فرم پرسنل برای چک کردن ثبت نشدن دوره تکراری
        /// </summary>
        /// <param name="dateRangeGroupId">کلید اصلی گروه محدوده محاسبات</param>
        /// <param name="personId">کلید اصلی پرسنل</param>
        /// <param name="date">تاریخ</param>
        /// <returns>محدوده محاسبات اختصاص داده شده</returns>
        public PersonRangeAssignment GetPersonRangeAssignmentByGroupAndDate(decimal dateRangeGroupId,decimal personId, DateTime date) 
        {
            IList<PersonRangeAssignment> list = asignRepository.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new PersonRangeAssignment().CalcDateRangeGroup), new CalculationRangeGroup() { ID = dateRangeGroupId }),
                                                                              new CriteriaStruct(Utility.GetPropertyName(() => new PersonRangeAssignment().Person), new Person() {ID=personId }),
                                                                              new CriteriaStruct(Utility.GetPropertyName(() => new PersonRangeAssignment().FromDate), date));
            if (list.Count > 0) 
            {
                return list.Last();
            }
            return null;
        }

        /// <summary>
        /// چک می کند آیا محدوده محاسباتی برای پرسنل اختصاص داده شده است یا خیر ؟
        /// </summary>
        /// <param name="personId">کلید اصلی پرسنل</param>
        /// <returns>بلی/خیر</returns>
        public bool ExsitsForPerson(decimal personId)
        {
            try
            {
                if (asignRepository.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new PersonRangeAssignment().Person), new Person() { ID = personId })) > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                LogException(ex, "BAssignDateRange", "ExsitsForPerson");
                throw ex;
            }
        }

        /// <summary>
        /// لیستی از گروه محدوده محاسبات را برمیگرداند
        /// </summary>
        /// <returns>لیست گروه محدوده محاسبات</returns>
        public IList<CalculationRangeGroup> GetAllCalculationRangeGroup()
        {
            try
            {
                BDateRange busRAnge = new BDateRange();
                return busRAnge.GetAll();
            }
            catch (Exception ex)
            {
                LogException(ex, "BAssignDateRange", "GetAllCalculationRangeGroup");
                throw ex;
            }
        }

        /// <summary>
        /// لیستی از محدوده محاسبات اختصاص داده شده به پرسنل را برمی گرداند 
        /// </summary>
        /// <param name="personId">کلید اصلی پرسنل</param>
        /// <returns>لیست محدوده محاسبات اختصاص داده شده به پرسنل</returns>
        public IList<PersonRangeAssignment> GetAll(decimal personId)
        {
            try
            {
                if (personId > 0)
                {
                    IList<PersonRangeAssignment> list = asignRepository.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new PersonRangeAssignment().Person), new Person() { ID = personId }));
                    list = list.OrderBy(x => x.FromDate).ToList();
                    foreach (PersonRangeAssignment assign in list)
                    {
                        assign.UIFromDate = Utility.ToPersianDate(assign.FromDate);
                    }
                    return list;
                }
                else
                {
                    throw new ItemNotExists("پرسنل مشخص نشده است - خطا در مرورگر", ExceptionSrc);
                }
            }
            catch (Exception ex)
            {
                LogException(ex, "BAssignDateRange", "GetAll");
                throw ex;
            }
        }

        /// <summary>
        /// لیستی از محدوده محاسبات اختصاص داده شده به پرسنل را  بر اساس گروه محدوده محاسبات برمی گرداند 
        /// </summary>
        /// <param name="groupId">کلید اصلی گروه محدوده محاسبات</param>
        /// <returns>لیست محدوده محاسبات اختصاص داده شده به پرسنل</returns>
        public IList<PersonRangeAssignment> GetAllByRangeId(decimal groupId)
        {
            try
            {

                IList<PersonRangeAssignment> list = asignRepository.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new PersonRangeAssignment().CalcDateRangeGroup), new CalculationRangeGroup() { ID = groupId }));
                return list;
            }
            catch (Exception ex)
            {
                LogException(ex, "BAssignDateRange", "GetAllByRangeId");
                throw ex;
            }
        }

        /// <summary>
        /// محدوده محاسبات اختصاص داده شده به پرسنل را بر اساس کلید اصلی آن بر می گرداند
        /// </summary>
        /// <param name="objID">کلید اصلی محدوده محاسبات اختصاص داده شده</param>
        /// <returns>محدوده محاسبات اختصاص داده شده به پرسنل</returns>
        public override PersonRangeAssignment GetByID(decimal objID)
        {
            try
            {
                PersonRangeAssignment assign = base.GetByID(objID);
                assign.UIFromDate = Utility.ToPersianDate(assign.FromDate);
                return assign;
            }
            catch (Exception ex)
            {
                LogException(ex, "BAssignDateRange", "GetByID");
                throw ex;
            }
        }

        /// <summary>
        /// آماده سازی آبجکت قبل از ذخیره در دیتابیس
        /// </summary>
        /// <param name="assignRange">محدوده محاسبات اختصاص داده شده به پرسنل</param>
        /// <param name="action">نوع عملیات</param>
        protected override void GetReadyBeforeSave(PersonRangeAssignment assignRange, UIActionType action)
        {
            if (systemLanguage == SysLanguageResource.Parsi)
            {
                assignRange.FromDate = Utility.ToMildiDate(assignRange.UIFromDate);
            }
            else if (systemLanguage == SysLanguageResource.English)
            {
                if (assignRange.UIFromDate != null)
                {
                    string[] strs = assignRange.UIFromDate.Split('/');
                    if (strs[0].Length == 4)
                    {
                        assignRange.FromDate = new DateTime(Utility.ToInteger(strs[0]), Utility.ToInteger(strs[1]), Utility.ToInteger(strs[2]));
                    }
                    else
                    {
                        assignRange.FromDate = new DateTime(Utility.ToInteger(strs[2]), Utility.ToInteger(strs[1]), Utility.ToInteger(strs[0]));
                    }
                }
            }
            if (action == UIActionType.ADD || action == UIActionType.EDIT)
            {
                //اگر اولین انتساب است اتوماتیک به ابتدای سال برود
                Person prs = new PersonRepository(false).GetById(assignRange.Person.ID, false);
                if (prs.PersonRangeAssignList == null || prs.PersonRangeAssignList.Count == 0)
                {
                    DateTime startYear;
                    if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                    {
                        PersianDateTime pd = Utility.ToPersianDateTime(assignRange.FromDate);
                        startYear = Utility.ToMildiDate(String.Format("{0}/01/01", pd.Year));
                    }
                    else
                    {
                        startYear = new DateTime(assignRange.FromDate.Year, 1, 1);
                    }
                    if (assignRange.FromDate.Date > startYear.Date)
                    {
                        assignRange.FromDate = startYear.Date;
                    }
                }
            }
        }

        /// <summary>    
        /// اعتبار سنجی عملیات درج
        /// </summary>
        /// <param name="assignRange">محدوده محاسبات اختصاص داده شده به پرسنل</param>
        protected override void InsertValidate(PersonRangeAssignment assignRange)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            PersonRepository personRep = new PersonRepository(false);
            EntityRepository<CalculationRangeGroup> groupRep = new EntityRepository<CalculationRangeGroup>(false);
            if (assignRange.Person == null || personRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new Person().ID), assignRange.Person.ID)) == 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.AssignRangePersonIdNotExsits, "پرسنلی با این مشخصات یافت نشد", ExceptionSrc));
            }

            if (assignRange.CalcDateRangeGroup == null || groupRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new CalculationRangeGroup().ID), assignRange.CalcDateRangeGroup.ID)) == 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.AssignRangeGroupIdNotExsits, "گروه دوره محاسبات با این مشخصات یافت نشد", ExceptionSrc));
            }
            if (assignRange.FromDate < Utility.GTSMinStandardDateTime)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.AssignRangeSmallerThanStandardValue, "مقدار تاریخ انتساب گروه محدوده محاسبات از حد مجاز کمتر میباشد", ExceptionSrc));
            }
            if (asignRepository.Find(x =>x.Person.ID==assignRange.Person.ID && x.FromDate.Date == assignRange.FromDate.Date).Count() > 0) 
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.AssignRangeDateIsRepeated, "تاریخ تکراری است", ExceptionSrc));
            }
            if (exception.Count == 0) 
            {
                Person prs = personRep.GetById(assignRange.Person.ID, false);
                if (prs.PersonRangeAssignList == null || prs.PersonRangeAssignList.Count == 0)
                {
                    DateTime startYear;
                    if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                    {
                        PersianDateTime pd = Utility.ToPersianDateTime(assignRange.FromDate);
                        startYear = Utility.ToMildiDate(String.Format("{0}/01/01", pd.Year));
                    }
                    else
                    {
                        startYear = new DateTime(assignRange.FromDate.Year, 1, 1);
                    }
                    if (assignRange.FromDate.Date > startYear.Date)
                    {
                        exception.Add(new ValidationException(ExceptionResourceKeys.AssignRangeFirstMustBeFromStartYear, "ابتدای بازه انتساب در اولین انتساب ، باید ابتدای سال باشد", ExceptionSrc));
                    }
                }
            }
            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        /// <summary>    
        /// اعتبار سنجی عملیات ویرایش
        /// </summary>
        /// <param name="assignRange">محدوده محاسبات اختصاص داده شده به پرسنل</param>
        protected override void UpdateValidate(PersonRangeAssignment assignRange)
        {
            PersonRangeAssignment personRangeAssignmentAlias = null;
            UIValidationExceptions exception = new UIValidationExceptions();
            EntityRepository<CalculationRangeGroup> groupRep = new EntityRepository<CalculationRangeGroup>(false);
            PersonRepository personRep = new PersonRepository(false);
            if (assignRange.Person == null || personRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new Person().ID), assignRange.Person.ID)) == 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.AssignRangePersonIdNotExsits, "پرسنلی با این مشخصات یافت نشد", ExceptionSrc));
            }
            if (assignRange.CalcDateRangeGroup == null || groupRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new CalculationRangeGroup().ID), assignRange.CalcDateRangeGroup.ID)) == 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.AssignRangeGroupIdNotExsits, "گروه دوره محاسبات با این مشخصات یافت نشد", ExceptionSrc));
            }
            if (assignRange.FromDate < Utility.GTSMinStandardDateTime)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.AssignRangeSmallerThanStandardValue, "مقدار تاریخ انتساب گروه محدوده محاسبات از حد مجاز کمتر میباشد", ExceptionSrc));
            }                  
            IList<PersonRangeAssignment> list = NHSession.QueryOver(() => personRangeAssignmentAlias)
                                                         .Where(() => personRangeAssignmentAlias.Person.ID == assignRange.Person.ID &&
                                                                      personRangeAssignmentAlias.ID != assignRange.ID
                                                                   )
                                                         .List();
                                                                                 
            if(list.Count > 0)
            {
                if (list.Where(x => x.FromDate == assignRange.FromDate).Count() > 0)
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.AssignRangeDateIsRepeated, "تاریخ تکراری است", ExceptionSrc));
                }
            }
            if (exception.Count == 0)
            {
                Person prs = personRep.GetById(assignRange.Person.ID, false);
                if (prs.PersonRangeAssignList == null || prs.PersonRangeAssignList.Count == 1)
                {
                    DateTime startYear;
                    if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                    {
                        PersianDateTime pd = Utility.ToPersianDateTime(assignRange.FromDate);
                        startYear = Utility.ToMildiDate(String.Format("{0}/01/01", pd.Year));
                    }
                    else
                    {
                        startYear = new DateTime(assignRange.FromDate.Year, 1, 1);
                    }
                    if (assignRange.FromDate.Date > startYear.Date)
                    {
                        exception.Add(new ValidationException(ExceptionResourceKeys.AssignRangeFirstMustBeFromStartYear, "ابتدای بازه انتساب در اولین انتساب ، باید ابتدای سال باشد", ExceptionSrc));
                    }
                }
            }
            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        /// <summary>
        /// اعتبارسنجی عملیات حذف
        /// </summary>
        /// <param name="obj">محدوده محاسبات اختصاص داده شده به پرسنل</param>
        protected override void DeleteValidate(PersonRangeAssignment obj)
        {
            UIValidationExceptions exception = new UIValidationExceptions();
            if(this.GetPersonRangeAssignment(obj) <= 1)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.PeronMustHaveOneAssignDateRange, "حداقل یک محدوده محاسبات باید به پرسنل تخصیص داده شده باشد", ExceptionSrc));
            }
            if (exception.Count > 0)
            {
                throw exception;
            }
        }
        public int GetPersonRangeAssignment(PersonRangeAssignment obj)
        {
            PersonRangeAssignment personRangeAssignmentAlias = null;
            Person personAlias = null;
            int count = NHSession.QueryOver<PersonRangeAssignment>(() => personRangeAssignmentAlias)
                                 .JoinAlias(() => personRangeAssignmentAlias.Person, () => personAlias)
                                 .Where(() => personAlias.ID == obj.Person.ID)
                                 .RowCount();
            return count;
        }

        /// <summary>
        /// بدون ایجاد ترانزاکشن و آماده سازی عمل درج را انجام میدهد
        /// </summary>
        /// <param name="assignRule">محدوده محاسبات اختصاص داده شده به پرسنل</param>
        /// <returns></returns>
        public decimal InsertWithoutTransaction(PersonRangeAssignment assignRange)
        {
            try
            {
                asignRepository.WithoutTransactSave(assignRange);
                return assignRange.ID;
            }
            catch (Exception ex)
            {
                LogException(ex, "BAssignDateRange", "InsertWithoutTransaction");
                throw ex;
            }
        }

        /// <summary>
        /// به روز رسانی نشانگر محاسبات
        /// </summary>
        /// <param name="obj"> محدوده محاسبات اختصاص داده شده به پرسنل</param>
        /// <param name="action">نوع عملیات</param>
        protected override void UpdateCFP(PersonRangeAssignment obj, UIActionType action)
        {
            if (action == UIActionType.ADD || action == UIActionType.EDIT)
            {
                decimal personId = obj.Person.ID;
               // CFP cfp = base.GetCFP(personId);
                DateTime newCfpDate = obj.FromDate;
                //if (cfp.ID == 0 || cfp.Date > newCfpDate)
                //{
                //    DateTime calculationLockDate = base.UIValidator.GetCalculationLockDate(personId);

                //    //بسته بودن محاسبات 
                //    if (calculationLockDate > Utility.GTSMinStandardDateTime && calculationLockDate > newCfpDate)
                //    {
                //        newCfpDate = calculationLockDate.AddDays(1);
                //    }

                    base.UpdateCFP(personId, newCfpDate);
               // }
            }
        }
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertAssignDateRange_onPersonnelInsert(PersonRangeAssignment rangeAssignment, UIActionType UAT)
        {
            return this.SaveChanges(rangeAssignment, UAT);
        }
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal UpdateAssignDateRange_onPersonnelInsert(PersonRangeAssignment rangeAssignment, UIActionType UAT)
        {
            return this.SaveChanges(rangeAssignment, UAT);
        }
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal DeleteAssignDateRange_onPersonnelInsert(PersonRangeAssignment rangeAssignment, UIActionType UAT)
        {
            return this.SaveChanges(rangeAssignment, UAT);
        }


        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertAssignDateRange_onPersonnelUpdate(PersonRangeAssignment rangeAssignment, UIActionType UAT)
        {
            return this.SaveChanges(rangeAssignment, UAT);
        }
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal UpdateAssignDateRange_onPersonnelUpdate(PersonRangeAssignment rangeAssignment, UIActionType UAT)
        {
            return this.SaveChanges(rangeAssignment, UAT);
        }
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal DeleteAssignDateRange_onPersonnelUpdate(PersonRangeAssignment rangeAssignment, UIActionType UAT)
        {
            return this.SaveChanges(rangeAssignment, UAT);
        }
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckCalculationRangesGroupsLoadAccess_onPersonnelInsert()
        {

        }
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckCalculationRangesGroupsLoadAccess_onPersonnelUpdate()
        {

        }
    }
}