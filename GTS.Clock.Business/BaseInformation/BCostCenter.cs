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
using GTS.Clock.Infrastructure;
using GTS.Clock.Business.Temp;
using NHibernate;
namespace GTS.Clock.Business.BaseInformation
{
    /// <summary>
    /// مرکز هزینه پرسنل
    /// </summary>
    public class BCostCenter : BaseBusiness<CostCenter>
    {
        const string ExceptionSrc = "GTS.Clock.Business.BaseInformation.BCostCenter";
        private ISession NHSession = NHibernateSessionManager.Instance.GetSession();
        private EntityRepository<CostCenter> CostCenterRepository = new EntityRepository<CostCenter>(false);
        IDataAccess accessPort = new BUser();
        int operationBatchSizeValue = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings[OperationBatchSize.OperationBatchSizeKey.ToString()]);
        private BTemp bTemp = new BTemp();

        /// <summary>
        /// لیست مراکز هزینه را بر اساس دسترسی کاربر به آن بر می گرداند
        /// </summary>
        /// <returns>لیست مراکز هزینه</returns>
        public override IList<CostCenter> GetAll()
        {
            IList<CostCenter> costCenters = new List<CostCenter>();
            try
            {
                IList<decimal> accessableIDs = accessPort.GetAccessibleCostCenters();

                if (accessableIDs.Count < this.operationBatchSizeValue && this.operationBatchSizeValue < 2100)
                {
                    costCenters = CostCenterRepository.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new CostCenter().ID), accessableIDs.ToArray(), CriteriaOperation.IN));

                }
                else
                {
                    CostCenter costCenterAlias = null;
                    GTS.Clock.Model.Temp.Temp tempAlias = null;
                    string operationGUID = this.bTemp.InsertTempList(accessableIDs);
                    costCenters = NHSession.QueryOver(() => costCenterAlias)
                                                      .JoinAlias(() => costCenterAlias.TempList, () => tempAlias)
                                                      .Where(() => tempAlias.OperationGUID == operationGUID)
                                                      .List<CostCenter>();
                    this.bTemp.DeleteTempList(operationGUID);

                }
                 
                return costCenters;
            }
            catch (Exception ex)
            {
                LogException(ex, "BCostCenter", "GetAll");
                throw ex;
            }
            finally { }
        }

        /// <summary>
        /// لیست مراکز هزینه را بدون اعمال دسترسی بر می گرداند
        /// </summary>
        /// <returns>لیست مراکز هزینه</returns>
        public IList<CostCenter> GetAllWithOutCheckAccess()
        {
            return base.GetAll();
        }

        /// <summary>
        /// عملیات درج مرکز هزینه پرسنل در دیتابیس
        /// </summary>
        /// <param name="CostCenter">مرکز هزینه پرسنل</param>
        /// <param name="UAT">عملیات درج</param>
        /// <returns>کلید اصلی مرکز هزینه پرسنل</returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertCostCenter(CostCenter CostCenter, UIActionType UAT)
        {
            return base.SaveChanges(CostCenter, UAT);
        }

        /// <summary>
        /// عملیات ویرایش مرکز هزینه پرسنل در دیتابیس
        /// </summary>
        /// <param name="CostCenter">مرکز هزینه پرسنل</param>
        /// <param name="UAT">عملیات درج</param>
        /// <returns>کلید اصلی مرکز هزینه پرسنل</returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal UpdateCostCenter(CostCenter CostCenter, UIActionType UAT)
        {
            return base.SaveChanges(CostCenter, UAT);
        }

        /// <summary>
        /// عملیات حذف مرکز هزینه پرسنل در دیتابیس
        /// </summary>
        /// <param name="CostCenter">مرکز هزینه پرسنل</param>
        /// <param name="UAT">عملیات درج</param>
        /// <returns>کلید اصلی مرکز هزینه پرسنل</returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal DeleteCostCenter(CostCenter CostCenter, UIActionType UAT)
        {
            return base.SaveChanges(CostCenter, UAT);
        }

        /// <summary>
        /// اعتبار سنجی عملیات درج مرکز هزینه پرسنل
        /// </summary>
        /// <param name="CostCenter">مرکز هزینه پرسنل</param>
        protected override void InsertValidate(CostCenter CostCenter)
        {

            UIValidationExceptions exception = new UIValidationExceptions();

            if (Utility.IsEmpty(CostCenter.Name))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.CostCenterNameRequierd, "درج - نام نباید خالی باشد", ExceptionSrc));
            }
            else if (CostCenterRepository.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => CostCenter.Name), CostCenter.Name)) > 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.CostCenterNameRepeated, "درج - نام نباید تکراری باشد", ExceptionSrc));
            }
            else if (!Utility.IsEmpty(CostCenter.Name) && CostCenterRepository.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => CostCenter.Code), CostCenter.Code)) > 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.CostCenterCodeRepeated, "درج - کد نباید تکراری باشد", ExceptionSrc));
            }

            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        /// <summary>
        /// اعتبار سنجی عملیات ویرایش مرکز هزینه پرسنل
        /// </summary>
        /// <param name="CostCenter">مرکز هزینه پرسنل</param>
        protected override void UpdateValidate(CostCenter CostCenter)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            if (Utility.IsEmpty(CostCenter.Name))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.CostCenterNameRequierd, "نام نباید خالی باشد", ExceptionSrc));
            }
            else
            {
                if (CostCenterRepository.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => CostCenter.Name), CostCenter.Name),
                                                         new CriteriaStruct(Utility.GetPropertyName(() => CostCenter.ID), CostCenter.ID, CriteriaOperation.NotEqual)) > 0)
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.CostCenterNameRepeated, "نام نباید تکراری باشد", ExceptionSrc));
                }
            }
            if (!Utility.IsEmpty(CostCenter.Code))
            {
                if (CostCenterRepository.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => CostCenter.Code), CostCenter.Code),
                                                        new CriteriaStruct(Utility.GetPropertyName(() => CostCenter.ID), CostCenter.ID, CriteriaOperation.NotEqual)) > 0)
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.CostCenterCodeRepeated, "کد نباید تکراری باشد", ExceptionSrc));
                }
            }


            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        /// <summary>
        /// اعتبار سنجی عملیات حذف مرکز هزینه پرسنل
        /// </summary>
        /// <param name="CostCenter">مرکز هزینه پرسنل</param>
        protected override void DeleteValidate(CostCenter CostCenter)
        {
            UIValidationExceptions exception = new UIValidationExceptions();
            PersonRepository personRep = new PersonRepository(false);
            int count = personRep.GetCountByCriteria(new CriteriaStruct("costCenter", CostCenter));

            if (count > 0)
            {
                exception.Add(ExceptionResourceKeys.CostCenterUsedByPersons, "این مرکز هزینه به اشخاص انتساب داده شده است", ExceptionSrc);
            }
            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        /// <summary>
        /// بررسی دسترسی به مرکز هزینه پرسنل
        /// </summary>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckCostCentersLoadAccess()
        {
        }

    }
}
