using System;
using System.Collections.Generic;
using System.Linq;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Model.BaseInformation;
using GTS.Clock.Business.Security;
using NHibernate.Linq;
using NHibernate.Transaction;
using NHibernate.Criterion;
using GTS.Clock.Infrastructure.NHibernateFramework;
namespace GTS.Clock.Business.BaseInformation
{
    /// <summary>
    /// محل ماموریت
    /// </summary>
    public class BDutyPlace : BaseBusiness<DutyPlace>
    {
        const string ExceptionSrc = "GTS.Clock.Business.BaseInformation.BDutyPlace";
        private DutyPlaceRepository dutyPlaceRep = new DutyPlaceRepository(false);

        /// <summary>
        /// ریشه درخت را برمیگرداند که با پیمایش بچه های آن درخت استخراج میشود
        /// </summary>
        /// <returns>محل ماموریت</returns>
        public DutyPlace GetDutyPalcesTree()
        {
            try
            {              

                IList<DutyPlace> departmentsList = dutyPlaceRep.GetDutyPlaceTree();

                if (departmentsList.Count == 1)
                {
                    return departmentsList.First();
                }
                else
                {
                    throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.DutyPlaceRootMoreThanOne, "تعداد ریشه محلهای ماموریت در دیتابیس نامعتبر است", ExceptionSrc);
                }
            }
            catch (Exception ex)
            {
                LogException(ex, "BDutyPlace", "GetDutyPalcesTree");
                throw ex;
            }
        }

        /// <summary>
        /// بچه های یک گره را برمیگرداند
        /// </summary>
        /// <param name="parentId">کلید اصلی گره والد</param>
        /// <returns>لیست محل ماموریت</returns>
        public IList<DutyPlace> GetDutyPlaceChilds(decimal parentId)
        {
            try
            {
                IList<DutyPlace> dutyplcList = dutyPlaceRep
                    .GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DutyPlace().ParentID), parentId));
                return dutyplcList;
            }
            catch (Exception ex)
            {
                LogException(ex, "BDutyPlace", "GetDutyPlaceChilds");
                throw ex;
            }
        }

        /// <summary>
        /// عملیات درج محل ماموریت در دیتابیس
        /// </summary>
        /// <param name="dutyPlace">محل ماموریت</param>
        protected override void InsertValidate(DutyPlace dutyPlace)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            if (dutyPlace.ParentID==0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.DutyPlaceParentRequest, "درج - والد باید مشخص شد", ExceptionSrc));
            }
            if (Utility.IsEmpty(dutyPlace.Name))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.DutyPlaceNameRequierd, "درج - نام نباید خالی باشد", ExceptionSrc));
            }
            else if (dutyPlaceRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => dutyPlace.Name), dutyPlace.Name)) > 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.DutyPlaceNameRepeated, "درج - نام نباید تکراری باشد", ExceptionSrc));
            }

            if (!Utility.IsEmpty(dutyPlace.CustomCode))
            {
                if (dutyPlaceRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => dutyPlace.CustomCode), dutyPlace.CustomCode)) > 0)
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.DutyPlaceCustomCodeRepeated, "درج - کد نباید تکراری باشد", ExceptionSrc));
                }
            }
            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        /// <summary>
        /// جستجوی محل های ماموریت
        /// </summary>
        /// <param name="field">فیلد جستجو</param>
        /// <param name="searchVal">عبارت جستجو</param>
        /// <returns>لیست محل ماموریت</returns>
        public IList<DutyPlace> SearchMissionLocations(DutyPlaceSearchFields field, string searchVal)
        {
            try
            {
                IList<DutyPlace> list = new List<DutyPlace>();
                //IList<decimal> ids = accessPort.GetAccessibleDeparments();
                switch (field)
                {
                    case DutyPlaceSearchFields.DutyPlaceName:
                    case DutyPlaceSearchFields.NotSpec:
                        list =
                            NHibernateSessionManager.Instance.GetSession().QueryOver<DutyPlace>()
                                                                          .Where(x => (x.CustomCode.IsInsensitiveLike(searchVal, MatchMode.Anywhere) ||
                                                                                       x.Name.IsInsensitiveLike(searchVal, MatchMode.Anywhere)) && x.CustomCode != "0-0"
                                                                                 )
                                                                          .List<DutyPlace>();
                        //list =
                        //    dutyPlaceRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DutyPlace().Name), searchVal, CriteriaOperation.Like),
                        //                                 new CriteriaStruct(Utility.GetPropertyName(() => new DutyPlace().CustomCode), "0-0", CriteriaOperation.NotEqual));
                        break;


                }
                return list;
            }
            catch (Exception ex)
            {

                LogException(ex, "BDepartment", "SearchDepartment");
                throw ex;
            }
        }
          
        /// <summary>
        /// ویرایش محل ماموریت در دیتابیس
        /// </summary>
        /// <param name="dutyPlace">محل ماموریت</param>
        protected override void UpdateValidate(DutyPlace dutyPlace)
        {
            UIValidationExceptions exception = new UIValidationExceptions();
           
            if (Utility.IsEmpty(dutyPlace.Name))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.DutyPlaceNameRequierd, "نام نباید خالی باشد", ExceptionSrc));
            }
            else
            {
                if (dutyPlaceRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => dutyPlace.Name), dutyPlace.Name),
                                                         new CriteriaStruct(Utility.GetPropertyName(() => dutyPlace.ID), dutyPlace.ID, CriteriaOperation.NotEqual)) > 0)
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.DutyPlaceNameRepeated, "نام نباید تکراری باشد", ExceptionSrc));
                }
            }

            if (!Utility.IsEmpty(dutyPlace.CustomCode))
            {
                if (dutyPlaceRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => dutyPlace.CustomCode), dutyPlace.CustomCode),
                                                         new CriteriaStruct(Utility.GetPropertyName(() => dutyPlace.ID), dutyPlace.ID, CriteriaOperation.NotEqual)) > 0)
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.DutyPlaceCustomCodeRepeated, "بروزرسانی - کد نباید تکراری باشد", ExceptionSrc));
                }
            }

            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        /// <summary>
        /// حذف محل ماموریت از دیتابیس
        /// </summary>
        /// <param name="dutyPlace">محل ماموریت</param>
        protected override void DeleteValidate(DutyPlace dutyPlace)
        {

            UIValidationExceptions exception = new UIValidationExceptions();

            RequestRepository requestRep = new RequestRepository(false);

            if (dutyPlaceRep.IsRoot(dutyPlace.ID))
            {
                exception.Add(ExceptionResourceKeys.DutyPlaceRootDeleteIllegal, "ریشه قابل حذف نیست", ExceptionSrc);
            }
            else
            {
                if (requestRep.IsDutyPlaceUsed(dutyPlace.ID))
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.DutyPlaceUsedByRequest, "بدلیل استفاده در درخواست ها نباید حذف شود", ExceptionSrc));
                }
            }
            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        /// <summary>
        /// بررسی دسترسی به محل ماموریت
        /// </summary>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckMissionLocationsLoadAccess()
        { 
        }

        /// <summary>
        /// درج محل ماموریت در دیتابیس
        /// </summary>
        /// <param name="missionLocation">محل ماموریت</param>
        /// <param name="UAT">نوع عملیات</param>
        /// <returns>کلید اصلی محل ماموریت</returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertMissionLocation(DutyPlace missionLocation , UIActionType UAT)
        {
            return base.SaveChanges(missionLocation, UAT);
        }

        /// <summary>
        /// ویرایش محل ماموریت در دیتبایس
        /// </summary>
        /// <param name="missionLocation">محل ماموریت</param>
        /// <param name="UAT">نوع عملیات</param>
        /// <returns>کلید اصلی محل ماموریت</returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal UpdateMissionLocation(DutyPlace missionLocation, UIActionType UAT)
        {
            return base.SaveChanges(missionLocation, UAT);
        }

        /// <summary>
        /// حذف محل ماموریت در دیتابیس
        /// </summary>
        /// <param name="missionLocation">محل ماموریت</param>
        /// <param name="UAT">نوع عملیات</param>
        /// <returns>کلید اصلی محل ماموریت</returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal DeleteMissionLocation(DutyPlace missionLocation, UIActionType UAT)
        {
            return base.SaveChanges(missionLocation, UAT);
        }

    }
}