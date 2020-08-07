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
using GTS.Clock.Infrastructure;
using GTS.Clock.Model.RequestFlow;
using GTS.Clock.Infrastructure.Validation.Configuration;
using GTS.Clock.Business.Shifts;
using GTS.Clock.Business.Security;
using NHibernate;

namespace GTS.Clock.Business.Assignments
{
    /// <summary>
    /// گروه کاری تخصیص داده شده به پرسنل
    /// </summary>
    public class BAssignWorkGroup : BaseBusiness<AssignWorkGroup>
    {
        const string ExceptionSrc = "GTS.Clock.Business.Assignments.BAssignWorkGroup";
        private EntityRepository<AssignWorkGroup> asignRepository = new EntityRepository<AssignWorkGroup>(false);
        private SysLanguageResource systemLanguage;
        private ISession NHSession = NHibernateSessionManager.Instance.GetSession();

        /// <summary>
        /// سازنده کلاس
        /// </summary>
        /// <param name="sysLanguage">زبان سیستم</param>
        public BAssignWorkGroup(SysLanguageResource sysLanguage)
        {
            systemLanguage = sysLanguage;
        }
        public BAssignWorkGroup()
        {           
        }
        /// <summary>
        /// سازنده کلاس
        /// </summary>
        /// <param name="sysLanguage">نام زبان سیستم</param>
        public BAssignWorkGroup(LanguagesName sysLanguage)
        {
            if (sysLanguage == LanguagesName.Parsi)
                systemLanguage = SysLanguageResource.Parsi;
            else
                systemLanguage = SysLanguageResource.English;
        }

        /// <summary>
        /// بررسی می کند آیا گروه کاری به پرسنل تخصیص داده شده است یا خیر ؟
        /// </summary>
        /// <param name="personId">کلید اصلی پرسنل</param>
        /// <returns>بلی/خیر</returns>
        public bool ExsitsForPerson(decimal personId)
        {
            if (asignRepository.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new AssignWorkGroup().Person), new Person() { ID = personId })) > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// لیستی از گروههای کاری را برمیگرداند
        /// </summary>
        /// <returns>لیست گروه های کاری</returns>
        public IList<WorkGroup> GetAllWorkGroup()
        {
            try
            {
                BWorkgroup busWorkGroup = new BWorkgroup();
                return busWorkGroup.GetAll();
            }
            catch (Exception ex)
            {
                LogException(ex, "BAssignWorkGroup", "GetAllWorkGroup");
                throw ex;
            }
        }

        /// <summary>
        /// لیست گروه کاری تخصیص داده شده به پرسنل را بر می گرداند
        /// </summary>
        /// <returns>لیست گروه کاری تخصیص داده شده به پرسنل</returns>
        public override IList<AssignWorkGroup> GetAll()
        {
            try
            {
                throw new IllegalServiceAccess("استفاده از این متد بی معنا میباشد", ExceptionSrc);
            }
            catch (Exception ex)
            {
                LogException(ex, "BAssignWorkGroup", "GetAll");
                throw ex;
            }
        }

        /// <summary>
        /// لیست گروه کاری تخصیص داده شده به یک پرسنل بر می گرداند
        /// </summary>
        /// <param name="personId"></param>
        /// <returns>لیست گروه کاری تخصیص داده شده به پرسنل</returns>
        public IList<AssignWorkGroup> GetAll(decimal personId)
        {
            try
            {
                if (personId > 0)
                {
                    IList<AssignWorkGroup> list = asignRepository.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new AssignWorkGroup().Person), new Person() { ID = personId }));
                    list = list.OrderBy(x => x.FromDate).ToList();
                    foreach (AssignWorkGroup assign in list)
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
                LogException(ex, "BAssignWorkGroup", "GetAll");
                throw ex;
            }
        }

        /// <summary>
        /// لیست گروه کاری تخصیص داده شده به پرسنل
        /// </summary>
        /// <param name="workGroupId">کلید اصلی گروه کاری</param>
        /// <returns>لیست گروه کاری تخصیص داده شده به پرسنل</returns>
        public IList<AssignWorkGroup> GetAllByWorkGroupId(decimal workGroupId)
        {
            try
            {
                IList<AssignWorkGroup> list = asignRepository.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new AssignWorkGroup().WorkGroup), new WorkGroup() { ID = workGroupId }));
                return list;
            }
            catch (Exception ex)
            {
                LogException(ex, "BAssignWorkGroup", "GetAllByWorkGroupId");
                throw ex;
            }
        }

        /// <summary>
        /// بدون ایجاد ترانزاکشن و آماده سازی عمل درج را انجام میدهد
        /// </summary>
        /// <param name="assignWorkGroup">گروه کاری تخصیص داده شده به پرسنل</param>
        /// <returns></returns>
        public decimal InsertWithoutTransaction(AssignWorkGroup assignWorkGroup)
        {
            try
            {
                asignRepository.WithoutTransactSave(assignWorkGroup);
                return assignWorkGroup.ID;
            }
            catch (Exception ex)
            {
                LogException(ex, "BAssignWorkGroup", "InsertWithoutTransaction");
                throw ex;
            }
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertAssignWorkGroup_onPersonnelInsert(AssignWorkGroup assignWorkGroup, UIActionType UAT)
        {
            return this.SaveChanges(assignWorkGroup, UAT);
        }
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal UpdateAssignWorkGroup_onPersonnelInsert(AssignWorkGroup assignWorkGroup, UIActionType UAT)
        {
            return this.SaveChanges(assignWorkGroup, UAT);
        }
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal DeleteAssignWorkGroup_onPersonnelInsert(AssignWorkGroup assignWorkGroup, UIActionType UAT)
        {
            return this.SaveChanges(assignWorkGroup, UAT);
        }


        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertAssignWorkGroup_onPersonnelUpdate(AssignWorkGroup assignWorkGroup, UIActionType UAT)
        {
            return this.SaveChanges(assignWorkGroup, UAT);
        }
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal UpdateAssignWorkGroup_onPersonnelUpdate(AssignWorkGroup assignWorkGroup, UIActionType UAT)
        {
            return this.SaveChanges(assignWorkGroup, UAT);
        }
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal DeleteAssignWorkGroup_onPersonnelUpdate(AssignWorkGroup assignWorkGroup, UIActionType UAT)
        {
            return this.SaveChanges(assignWorkGroup, UAT);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckWorkGroupLoadAccess_onPersonnelInsert()
        {
        }
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckWorkGroupLoadAccess_onPersonnelUpdate()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objID">کلید اصلی گروه کاری تخصیص داده شده به پرسنل</param>
        /// <returns>گروه کاری تخصیص داده شده به پرسنل</returns>
        public override AssignWorkGroup GetByID(decimal objID)
        {
            try
            {
                AssignWorkGroup assign = base.GetByID(objID);
                assign.UIFromDate = Utility.ToPersianDate(assign.FromDate);
                return assign;
            }
            catch (Exception ex)
            {
                LogException(ex, "BAssignWorkGroup", "GetByID");
                throw ex;
            }
        }

        #region override methods

        /// <summary>
        /// آماده سازی گروه کاری تخصیص داده شده به پرسنل قبل از ذخیره در دیتابیس
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="action"></param>
        protected override void GetReadyBeforeSave(AssignWorkGroup obj, UIActionType action)
        {
            if (systemLanguage == SysLanguageResource.Parsi)
            {
                obj.FromDate = Utility.ToMildiDate(obj.UIFromDate);
            }
            else if (systemLanguage == SysLanguageResource.English)
            {
                obj.FromDate = Utility.ToMildiDateTime(obj.UIFromDate);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="assignWorkGroup">گروه کاری تخصیص داده شده به پرسنل</param>
        protected override void InsertValidate(AssignWorkGroup assignWorkGroup)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            PersonRepository personRep = new PersonRepository(false);
            WorkGroupRepository workRep = new WorkGroupRepository(false);

            if (assignWorkGroup.Person == null || personRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new Person().ID), assignWorkGroup.Person.ID)) == 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.AssignWorkGroupPersonIdNotExsits, "پرسنلی با این مشخصات یافت نشد", ExceptionSrc));
            }

            if (assignWorkGroup.WorkGroup == null || workRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new WorkGroup().ID), assignWorkGroup.WorkGroup.ID)) == 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.AssignWorkGroupIdNotExsits, "گروه کاری با این مشخصات یافت نشد", ExceptionSrc));
            }
            if (assignWorkGroup.FromDate < Utility.GTSMinStandardDateTime)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.AssignWorkGroupSmallerThanStandardValue, "مقدار تاریخ انتساب گروه محدوده محاسبات از حد مجاز کمتر میباشد", ExceptionSrc));
            }
            if (asignRepository.Find(x => x.Person.ID == assignWorkGroup.Person.ID && x.FromDate.Date == assignWorkGroup.FromDate.Date).Count() > 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.AssignWorkGroupIsRepeated, "تاریخ تکراری است", ExceptionSrc));
            }
            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="assignWorkGroup">گروه کاری تخصیص داده شده به پرسنل</param>
        protected override void UpdateValidate(AssignWorkGroup assignWorkGroup)
        {
            UIValidationExceptions exception = new UIValidationExceptions();
            WorkGroupRepository workRep = new WorkGroupRepository(false);
            AssignWorkGroup assignWorkGroupAlias = null;

            PersonRepository personRep = new PersonRepository(false);
            if (assignWorkGroup.Person == null || personRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new Person().ID), assignWorkGroup.Person.ID)) == 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.AssignWorkGroupPersonIdNotExsits, "پرسنلی با این مشخصات یافت نشد", ExceptionSrc));
            }
            if (assignWorkGroup.WorkGroup == null || workRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new WorkGroup().ID), assignWorkGroup.WorkGroup.ID)) == 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.AssignWorkGroupIdNotExsits, "گروه کاری با این مشخصات یافت نشد", ExceptionSrc));
            }
            if (assignWorkGroup.FromDate < Utility.GTSMinStandardDateTime)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.AssignWorkGroupSmallerThanStandardValue, "مقدار تاریخ انتساب گروه محدوده محاسبات از حد مجاز کمتر میباشد", ExceptionSrc));
            }
            IList<AssignWorkGroup> list = NHSession.QueryOver(() => assignWorkGroupAlias)
                                                        .Where(() => assignWorkGroupAlias.Person.ID == assignWorkGroup.Person.ID &&
                                                                     assignWorkGroupAlias.ID != assignWorkGroup.ID
                                                                  )
                                                        .List();

            if (list.Count > 0)
            {
                if (list.Where(x => x.FromDate == assignWorkGroup.FromDate).Count() > 0)
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.AssignWorkGroupIsRepeated, "تاریخ تکراری است", ExceptionSrc));
                }
            }           
            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj">گروه کاری تخصیص داده شده به پرسنل</param>
        protected override void DeleteValidate(AssignWorkGroup obj)
        {
            UIValidationExceptions exception = new UIValidationExceptions();
            if(this.GetPersonAssignWorkGroup(obj) <= 1)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.PersonMustHaveOneAssignWorkGroup, "حداقل یک گروه کاری باید به پرسنل تخصیص داده شده باشد", ExceptionSrc));
            }
            if (exception.Count > 0)
            {
                throw exception;
            }
        }
        public int GetPersonAssignWorkGroup(AssignWorkGroup obj)
        {
             AssignWorkGroup assignWorkGroupAlias = null;
            Person personAlias = null;
            int count = NHSession.QueryOver<AssignWorkGroup>(() => assignWorkGroupAlias)
                                 .JoinAlias(() => assignWorkGroupAlias.Person, () => personAlias)
                                 .Where(() => personAlias.ID == obj.Person.ID)
                                 .RowCount();
            return count;                                 
        }

        /// <summary>
        /// به روز رسانی نشانگر محاسبات
        /// </summary>
        /// <param name="assignWorkGroup">گروه کاری تخصیص داده شده به پرسنل</param>
        /// <param name="action"></param>
        protected override void UpdateCFP(AssignWorkGroup assignWorkGroup, UIActionType action)
        {
            if (action == UIActionType.ADD || action == UIActionType.EDIT)
            {
                decimal personId = assignWorkGroup.Person.ID;
                //CFP cfp = base.GetCFP(personId);
                DateTime newCfpDate = assignWorkGroup.FromDate;
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

        #endregion

    }
}