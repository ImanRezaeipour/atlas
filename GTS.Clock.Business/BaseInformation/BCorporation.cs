using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Model.BaseInformation;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Business.Security;
using NHibernate;
using NHibernate.Criterion;
using GTS.Clock.Infrastructure.NHibernateFramework;

namespace GTS.Clock.Business.BaseInformation
{
    /// <summary>
    /// شرکت ها
    /// </summary>
    public class BCorporation : BaseBusiness<Corporation>
    {
        const string ExceptionSrc = "GTS.Clock.Business.BaseInformation.BCorporation";
        private EntityRepository<Corporation> CorporationRepository = new EntityRepository<Corporation>(false);

        public IList<Corporation> GetAll(string SearchItem)
        {
            Corporation corporationAlias = null;
            IList<Corporation> CorporationList = NHibernateSessionManager.Instance.GetSession().QueryOver<Corporation>(() => corporationAlias)
                                                                                               .Where(() => corporationAlias.Name.IsInsensitiveLike(SearchItem , MatchMode.Anywhere) ||
                                                                                                            corporationAlias.Code.IsInsensitiveLike(SearchItem , MatchMode.Anywhere)
                                                                                                     )
                                                                                               .List<Corporation>();
            return CorporationList;
        }

        /// <summary>
        /// اعتبار سنجی عملیات درج شرکت
        /// </summary>
        /// <param name="Corporation">شرکت</param>
        protected override void InsertValidate(Corporation Corporation)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            if (Utility.IsEmpty(Corporation.Name))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.CorporationNameRequierd, "درج - نام نباید خالی باشد", ExceptionSrc));
            }
            else if (CorporationRepository.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => Corporation.Name), Corporation.Name)) > 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.CorporationNameRepeated, "درج - نام نباید تکراری باشد", ExceptionSrc));
            }
            else if (Utility.IsEmpty(Corporation.Code))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.CorporationCodeRequierd, "درج - کد نباید خالی باشد", ExceptionSrc));
            }
            else if (CorporationRepository.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => Corporation.Code), Corporation.Code)) > 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.CorporationCodeRepeated, "درج - کد نباید تکراری باشد", ExceptionSrc));
            }

            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        /// <summary>
        /// اعتبار سنجی عملیات ویرایش شرکت
        /// </summary>
        /// <param name="Corporation">شرکت</param>
        protected override void UpdateValidate(Corporation Corporation)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            if (Utility.IsEmpty(Corporation.Name))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.CorporationNameRequierd, "نام نباید خالی باشد", ExceptionSrc));
            }
            else if (CorporationRepository.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => Corporation.Name), Corporation.Name),
                                                              new CriteriaStruct(Utility.GetPropertyName(() => Corporation.ID), Corporation.ID, CriteriaOperation.NotEqual)) > 0)
            {
                    exception.Add(new ValidationException(ExceptionResourceKeys.CorporationNameRepeated, "نام نباید تکراری باشد", ExceptionSrc));
            }
            else if (Utility.IsEmpty(Corporation.Code))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.CorporationCodeRequierd, "کد نباید خالی باشد", ExceptionSrc));
            }
            else if (CorporationRepository.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => Corporation.Code), Corporation.Code),
                                                              new CriteriaStruct(Utility.GetPropertyName(() => Corporation.ID), Corporation.ID, CriteriaOperation.NotEqual)) > 0) 
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.CorporationCodeRepeated, "کد نباید تکراری باشد", ExceptionSrc));
            }


            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        /// <summary>
        /// اعتبار سنجی عملیات حذف شرکت
        /// </summary>
        /// <param name="Corporation">شرکت</param>
        protected override void DeleteValidate(Corporation Corporation)
        {
        }

        /// <summary>
        /// اعمال دسترسی به کاربر جاری بعد از عملیات درج شرکت در دیتابیس
        /// </summary>
        /// <param name="obj">شرکت</param>
        /// <param name="action">نوع عملیات</param>
        protected override void OnSaveChangesSuccess(Corporation obj, UIActionType action)
        {
            if (action == UIActionType.ADD)
            {
                new BDataAccess().InsertDataAccess(Infrastructure.DataAccessLevelOperationType.Single, Infrastructure.DataAccessParts.Corporation, obj.ID, BUser.CurrentUser.ID, null, "");
            }
        }

        /// <summary>
        /// بررسی دسترسی به شرکت
        /// </summary>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckCorporationsLoadAccess()
        { 
        }

        /// <summary>
        /// عملیات درج شرکت در دیتابیس
        /// </summary>
        /// <param name="corporation">شرکت</param>
        /// <param name="UAT">نوع عملیات</param>
        /// <returns>کلید اصلی</returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertCorporation(Corporation corporation, UIActionType UAT)
        {
            return base.SaveChanges(corporation, UAT);
        }

        /// <summary>
        /// عملیات ویرایش شرکت در دیتابیس
        /// </summary>
        /// <param name="corporation">شرکت</param>
        /// <param name="UAT">نوع عملیات</param>
        /// <returns>کلید اصلی</returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal UpdateCorporation(Corporation corporation, UIActionType UAT)
        {
            return base.SaveChanges(corporation, UAT);
        }

        /// <summary>
        /// عملیات حذف  شرکت از دیتابیس
        /// </summary>
        /// <param name="corporation">شرکت</param>
        /// <param name="UAT">نوع عملیات</param>
        /// <returns>کلید اصلی</returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal DeleteCorporation(Corporation corporation, UIActionType UAT)
        {
            return base.SaveChanges(corporation, UAT);
        }

    }
}
