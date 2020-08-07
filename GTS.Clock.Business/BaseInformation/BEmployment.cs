using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Model.BaseInformation;
using GTS.Clock.Model;
using System.Reflection;
using GTS.Clock.Business.Security;
using NHibernate;
using GTS.Clock.Infrastructure;
using GTS.Clock.Business.Temp;

namespace GTS.Clock.Business.BaseInformation
{
    /// <summary>
    /// نوع استخدام
    /// </summary>
    public class BEmployment : BaseBusiness<EmploymentType>
    {
        public BEmployment() { }
        const string ExceptionSrc = "GTS.Clock.Business.BaseInformation.BEmployment";
        private EntityRepository<EmploymentType> emplRepository = new EntityRepository<EmploymentType>(false);
        IDataAccess accessPort = new BUser();
        private ISession NHSession = NHibernateSessionManager.Instance.GetSession();
        private BTemp bTemp = new BTemp();
        int operationBatchSizeValue = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings[OperationBatchSize.OperationBatchSizeKey.ToString()]);


        public override IList<EmploymentType> GetAll()
        {

            IList<decimal> accessableIDs = accessPort.GetAccessibleEmploymentTypes();


            IList<EmploymentType> list = new List<EmploymentType>();
            if (accessableIDs.Count < this.operationBatchSizeValue && this.operationBatchSizeValue < 2100)
            {
                list = emplRepository.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new EmploymentType().ID), accessableIDs.ToArray(), CriteriaOperation.IN));
            }
            else
            {
                EmploymentType employmentTypeAlias = null;
                GTS.Clock.Model.Temp.Temp tempAlias = null;
                string operationGUID = this.bTemp.InsertTempList(accessableIDs);
                list = NHSession.QueryOver(() => employmentTypeAlias)
                                .JoinAlias(() => employmentTypeAlias.TempList, () => tempAlias)
                                .Where(() => tempAlias.OperationGUID == operationGUID)
                                .List<EmploymentType>();
                this.bTemp.DeleteTempList(operationGUID);
            }

            return list;
        }
        public IList<EmploymentType> GetAllWithoutAccessible()
        {


            EmploymentType employmentTypeAlias = null;

            IList<EmploymentType> list = new List<EmploymentType>();
            list = NHSession.QueryOver<EmploymentType>(() => employmentTypeAlias).List();
            

            return list;
        }
        public IList<EmploymentType> GetEmploymentTypeList (string SearchTerm)
        {
            string SqlCommand = @"select * from TA_EmploymentType where emply_Name like :SearchTerm or emply_CustomCode like :SearchTerm";
            IList<EmploymentType> EmploymentTypeList = NHSession.CreateSQLQuery(SqlCommand)
                                                                .AddEntity(typeof(EmploymentType))
                                                                .SetParameter("SearchTerm", string.Format("%{0}%", SearchTerm))
                                                                .List<EmploymentType>();
            return EmploymentTypeList;
        }
        /// <summary>
        /// «اعتبارسنجی
        /// «نام نباید خالی باشد
        /// «نام نوع استخدام تکراری نباشد
        /// کد تعریف شده نباید تکراری باشد
        /// </summary>
        /// <param name="empl"></param>
        protected override void InsertValidate(EmploymentType empl)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            if (Utility.IsEmpty(empl.Name))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.EmploymentTypeNameRequierd, "درج - نام نباید خالی باشد", ExceptionSrc));
            }
            else if (emplRepository.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => empl.Name), empl.Name)) > 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.EmploymentTypeNameRepeated, "درج - نام نباید تکراری باشد", ExceptionSrc));
            }

            if (!Utility.IsEmpty(empl.CustomCode))
            {
                if (emplRepository.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => empl.CustomCode), empl.CustomCode)) > 0)
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.EmploymentTypeCustomCodeRepeated, "درج - کد نوع استخدام نباید تکراری باشد", ExceptionSrc));
                }
            }
            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        /// <summary>
        /// «اعتبارسنجی
        /// «نام نباید خالی باشد
        /// «نام نوع استخدام تکراری نباشد
        /// کد تعریف شده نباید تکراری باشد
        /// </summary>
        /// <param name="empl"></param>
        protected override void UpdateValidate(EmploymentType empl)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            if (Utility.IsEmpty(empl.Name))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.EmploymentTypeNameRequierd, "نام نباید خالی باشد", ExceptionSrc));
            }
            else
            {
                if (emplRepository.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => empl.Name), empl.Name),
                                                         new CriteriaStruct(Utility.GetPropertyName(() => empl.ID), empl.ID, CriteriaOperation.NotEqual)) > 0)
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.EmploymentTypeNameRepeated, "نام نباید تکراری باشد", ExceptionSrc));
                }
            }

            if (!Utility.IsEmpty(empl.CustomCode))
            {
                if (emplRepository.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => empl.CustomCode), empl.CustomCode),
                                                         new CriteriaStruct(Utility.GetPropertyName(() => empl.ID), empl.ID, CriteriaOperation.NotEqual)) > 0)
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.EmploymentTypeCustomCodeRepeated, "بروزرسانی - کد نوع استخدام نباید تکراری باشد", ExceptionSrc));
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
        /// <param name="obj"></param>
        protected override void DeleteValidate(EmploymentType empl)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            PersonRepository rep = new PersonRepository(false);

            if (rep.GetCountByCriteria(new CriteriaStruct(/*Utility.GetPropertyName(() => new Person().EmploymentType)*/"employmentType", empl)) > 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.EmploymentTypeUsedByPerson, "بدلیل استفاده در پرسنل نباید حذف شود", ExceptionSrc));
            }

            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        /// <summary>
        /// بررسی دسترسی به نوع استخدام
        /// </summary>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckEmployTypesLoadAccess()
        { 
        }

        /// <summary>
        /// عملیات درج نوع استخدام در دیتابیس
        /// </summary>
        /// <param name="employmentType">نوع استخدام</param>
        /// <param name="UAT">عملیات درج</param>
        /// <returns>کلید اصلی نوع استخدام</returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertEmploymentType(EmploymentType employmentType, UIActionType UAT)
        {
            return base.SaveChanges(employmentType, UAT);
        }

        /// <summary>
        /// عملیات ویرایش نوع استخدام در دیتابیس
        /// </summary>
        /// <param name="employmentType">نوع استخدام</param>
        /// <param name="UAT">عملیات درج</param>
        /// <returns>کلید اصلی نوع استخدام</returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal UpdateEmploymentType(EmploymentType employmentType, UIActionType UAT)
        {
            return base.SaveChanges(employmentType, UAT);
        }

        /// <summary>
        /// عملیات حذف نوع استخدام در دیتابیس
        /// </summary>
        /// <param name="employmentType">نوع استخدام</param>
        /// <param name="UAT">عملیات درج</param>
        /// <returns>کلید اصلی نوع استخدام</returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal DeleteEmploymentType(EmploymentType employmentType, UIActionType UAT)
        {
            return base.SaveChanges(employmentType, UAT);
        }

    }
}