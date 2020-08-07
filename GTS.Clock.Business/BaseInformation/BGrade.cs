using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Model.BaseInformation;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Business.Security;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.Utility;
using NHibernate.Linq;
using NHibernate.Transaction;
using NHibernate.Criterion;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Model;
namespace GTS.Clock.Business.BaseInformation
{
    /// <summary>
    /// رتبه پرسنل
    /// </summary>
    public class BGrade : BaseBusiness<Grade>
    {
        const string ExceptionSrc = "GTS.Clock.Business.BaseInformation.BGrade";
        private EntityRepository<Grade> gradeRepository = new EntityRepository<Grade>(false);
        private EntityRepository<Grade> staionRepository = new EntityRepository<Grade>(false);

        /// <summary>
        /// عملیات درج رتبه پرسنل در دیتابیس
        /// </summary>
        /// <param name="grade">رتبه پرسنل</param>
        /// <param name="UAT">عملیات درج</param>
        /// <returns>کلید اصلی رتبه پرسنل</returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertGrade(Grade grade, UIActionType UAT)
        {
            return base.SaveChanges(grade, UAT);
        }

        /// <summary>
        /// عملیات ویرایش رتبه پرسنل در دیتابیس
        /// </summary>
        /// <param name="grade">رتبه پرسنل</param>
        /// <param name="UAT">عملیات درج</param>
        /// <returns>کلید اصلی رتبه پرسنل</returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal UpdateGrade(Grade grade, UIActionType UAT)
        {
            return base.SaveChanges(grade, UAT);
        }

        /// <summary>
        /// عملیات حذف رتبه پرسنل در دیتابیس
        /// </summary>
        /// <param name="grade">رتبه پرسنل</param>
        /// <param name="UAT">عملیات درج</param>
        /// <returns>کلید اصلی رتبه پرسنل</returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal DeleteGrade(Grade grade, UIActionType UAT)
        {
            return base.SaveChanges(grade, UAT);
        }

        /// <summary>
        /// اعتبار سنجی عملیات درج رتبه پرسنل
        /// </summary>
        /// <param name="grade">رتبه پرسنل</param>
        protected override void InsertValidate(Grade grade)
        {

            UIValidationExceptions exception = new UIValidationExceptions();

            if (Utility.IsEmpty(grade.Name))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.GradeNameRequierd, "درج - نام نباید خالی باشد", ExceptionSrc));
            }
            else if (staionRepository.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => grade.Name), grade.Name)) > 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.GradeNameRepeated, "درج - نام نباید تکراری باشد", ExceptionSrc));
            }


            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        /// <summary>
        /// اعتبار سنجی عملیات ویرایش رتبه پرسنل
        /// </summary>
        /// <param name="grade">رتبه پرسنل</param>
        protected override void UpdateValidate(Grade grade)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            if (Utility.IsEmpty(grade.Name))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.GradeNameRequierd, "نام نباید خالی باشد", ExceptionSrc));
            }
            else
            {
                if (staionRepository.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => grade.Name), grade.Name),
                                                         new CriteriaStruct(Utility.GetPropertyName(() => grade.ID), grade.ID, CriteriaOperation.NotEqual)) > 0)
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.GradeNameRepeated, "نام نباید تکراری باشد", ExceptionSrc));
                }
            }

            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        /// <summary>
        /// اعتبار سنجی عملیات حذف رتبه پرسنل
        /// </summary>
        /// <param name="grade">رتبه پرسنل</param>
        protected override void DeleteValidate(Grade grade)
        {
            UIValidationExceptions exception = new UIValidationExceptions();
            PersonRepository personRep = new PersonRepository(false);
            int count = personRep.GetCountByCriteria(new CriteriaStruct("grade", grade));

            if (count > 0)
            {
                exception.Add(ExceptionResourceKeys.GradeUsedByPersons, "این رتبه به اشخاص انتساب داده شده است", ExceptionSrc);
            }
            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        /// <summary>
        /// بررسی دسترسی به رتبه پرسنل
        /// </summary>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckGradesLoadAccess()
        {
        }

    }
}
