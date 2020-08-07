using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Model.Contracts;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Linq;
using NHibernate.Transaction;
using NHibernate.Criterion;
using GTS.Clock.Business.Security;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.Repository;

namespace GTS.Clock.Business.Contracts
{
    public class BContract : BaseBusiness<Contract>
    {
        private ISession NHSession = NHibernateSessionManager.Instance.GetSession();
        const string ExceptionSrc = "GTS.Clock.Business.Contracts.BContract";
        EntityRepository<Contract> contractRep = new EntityRepository<Contract>(false);
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Avoid)]
        public void CheckContractLoadAccess()
        {
        }
        protected override void InsertValidate(Contract obj)
        {
            UIValidationExceptions exception = new UIValidationExceptions();
            Contract contractObj = GetContractByTitle(obj.Title);
            if (Utility.IsEmpty(obj.Title))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.ContractNameRequired, "نام قرارداد نباید خالی باشد", ExceptionSrc));
            } 
            if (contractObj != null && obj.ID != contractObj.ID)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.ContractTitleIsRepeated, "نام قرارداد نباید تکراری باشد", ExceptionSrc));
            }
            contractObj = GetContractByCode(obj.Code);
            if (contractObj != null && obj.ID != contractObj.ID && obj.Code != string.Empty)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.ContractCodeIsRepeated, "کد قرارداد نباید تکراری باشد", ExceptionSrc));
            }

            if (obj.Contractor == null || obj.Contractor.ID == 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.ContractContractorRequired, "پیمانکار قرارداد باید انتخاب شود", ExceptionSrc));
            }
            NHSession.Evict(contractObj);

            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        protected override void UpdateValidate(Contract obj)
        {
            UIValidationExceptions exception = new UIValidationExceptions();
            Contract contractTitleObj = GetContractByTitle(obj.Title);
            if (Utility.IsEmpty(obj.Title))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.ContractNameRequired, "نام قرارداد نباید خالی باشد", ExceptionSrc));
            }
            if (contractTitleObj != null && obj.ID != contractTitleObj.ID)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.ContractTitleIsRepeated, "نام قرارداد نباید تکراری باشد", ExceptionSrc));
            }
            NHSession.Evict(contractTitleObj);
            if (obj.Code != string.Empty)
            {
                Contract contractCodeObj = GetContractByCode(obj.Code);
                if (contractCodeObj != null && obj.ID != contractCodeObj.ID)
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.ContractCodeIsRepeated, "کد قرارداد نباید تکراری باشد", ExceptionSrc));
                }
                NHSession.Evict(contractCodeObj);
            }
            if (obj.Contractor == null || obj.Contractor.ID == 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.ContractContractorRequired, "پیمانکار قرارداد باید انتخاب شود", ExceptionSrc));
            }

            if (obj.IsDefault == false)
            {
                Contract contractDefaultObj = GetDefaultContract();
                if (contractDefaultObj != null && obj.ID == contractDefaultObj.ID)
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.ContractDefaultIsRequired, "حداقل یکی از قرارداد ها می بایست به صورت پیشفرض انتخاب شده باشد", ExceptionSrc));
                }
                NHSession.Evict(contractDefaultObj);
            }
            if (exception.Count > 0)
            {
                throw exception;
            }
        }
        protected override void DeleteValidate(Contract obj)
        {
            UIValidationExceptions exception = new UIValidationExceptions();
            if (obj.IsDefault)
                exception.Add(new ValidationException(ExceptionResourceKeys.ContractDefaultCanNotDeleted, "قرارداد پیش فرض نمی تواند حذف شود", ExceptionSrc));
            EntityRepository<PersonContractAssignment> contractAssignRep = new EntityRepository<PersonContractAssignment>();
            int count = contractAssignRep.Find(p => p.Contract.ID == obj.ID && p.IsDeleted==false).Count();
            if (count > 0)
                exception.Add(new ValidationException(ExceptionResourceKeys.ContractIsAssignedToPerson, "قرارداد به پرسنل اختصاص یافته است.", ExceptionSrc));
            if (exception.Count > 0)
            {
                throw exception;
            }
        }
        public Contract GetContractByCode(string code)
        {
            try
            {
                Contract contractObj = NHSession.QueryOver<Contract>().Where(c => c.Code == code).List().FirstOrDefault();
                return contractObj;
            }
            catch (Exception ex)
            {
                LogException(ex, "BContract", "GetContractByCode");
                throw ex;
            }
        }
        public Contract GetContractByTitle(string title)
        {
            try
            {
                Contract contractObj = NHSession.QueryOver<Contract>().Where(c => c.Title == title).List().FirstOrDefault();
                return contractObj;
            }
            catch (Exception ex)
            {
                LogException(ex, "BContract", "GetContractByTitle");
                throw ex;
            }
        }
        public Contract GetDefaultContract()
        {
            try
            {
                Contract contractObj = NHSession.QueryOver<Contract>().Where(c => c.IsDefault == true).List().FirstOrDefault();
                return contractObj;
            }
            catch (Exception ex)
            {

                LogException(ex, "BContract", "GetContractByCode");
                throw ex;
            }
        }


        public IList<Contract> GetContratsByPaging(string searchTerm, int pageIndex, int pageSize)
        {
            IList<Contract> contractList = new List<Contract>();
            try
            {
                Contract contractAlias = null;
                Contractor contractorAlias = null;
                contractList = NHSession.QueryOver(() => contractAlias)
                                        .JoinAlias(() => contractAlias.Contractor, () => contractorAlias)
                                        .Where(() => contractAlias.Title.IsInsensitiveLike(searchTerm, MatchMode.Anywhere) || contractorAlias.Name.IsInsensitiveLike(searchTerm, MatchMode.Anywhere) || contractAlias.Code.IsInsensitiveLike(searchTerm, MatchMode.Anywhere))
                                        .Skip(pageIndex * pageSize)
                                        .Take(pageSize)
                                        .List<Contract>();
                return contractList;
            }
            catch (Exception ex)
            {
                LogException(ex, "BContract", "GetContratsByPaging");
                throw ex;
            }
        }
        public int GetContratsByPagingCount(string searchTerm)
        {
            IList<Contract> contractList = new List<Contract>();
            try
            {
                Contract contractAlias = null;
                Contractor contractorAlias = null;
                int count = NHSession.QueryOver(() => contractAlias)
                                        .JoinAlias(() => contractAlias.Contractor, () => contractorAlias)
                                        .Where(() => contractAlias.Title.IsInsensitiveLike(searchTerm, MatchMode.Anywhere) || contractorAlias.Name.IsInsensitiveLike(searchTerm, MatchMode.Anywhere) || contractAlias.Code.IsInsensitiveLike(searchTerm, MatchMode.Anywhere))
                                        .RowCount();
                return count;
            }
            catch (Exception ex)
            {
                LogException(ex, "BContract", "GetContratsByPaging");
                throw ex;
            }
        }
        protected override void GetReadyBeforeSave(Contract contract, UIActionType action)
        {

        }
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertContract(Contract contract, UIActionType UAT, out decimal contractIdChangedIsDefault)
        {
            decimal contractId = 0;
            contractIdChangedIsDefault = 0;
            try
            {
                using (NHibernateSessionManager.Instance.BeginTransactionOn())
                {
                    Contract contractObj = GetDefaultContract();
                    if (contractObj != null && contract.IsDefault)
                    {
                        contractObj.IsDefault = false;
                        contractRep.WithoutTransactSave(contractObj);
                        contractIdChangedIsDefault = contractObj.ID;

                    }
                    contractId = base.SaveChanges(contract, UAT);
                    NHibernateSessionManager.Instance.CommitTransactionOn();
                }

                return contractId;
            }
            catch (Exception ex)
            {
                NHibernateSessionManager.Instance.RollbackTransactionOn();
                LogException(ex, "BContract", "InsertContract");
                throw ex;
            }
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal UpdateContract(Contract contract, UIActionType UAT, out decimal contractIdChangedIsDefault)
        {
            decimal contractId = 0;
            contractIdChangedIsDefault = 0;
            try
            {
                using (NHibernateSessionManager.Instance.BeginTransactionOn())
                {
                    Contract contractObj = GetDefaultContract();
                    if (contractObj != null && contract.IsDefault)
                    {
                        contractObj.IsDefault = false;
                        contractRep.WithoutTransactSave(contractObj);
                        contractIdChangedIsDefault = contractObj.ID;
                    }
                    contractId = base.SaveChanges(contract, UAT);
                    NHibernateSessionManager.Instance.CommitTransactionOn();
                }

                return contractId;

            }
            catch (Exception ex)
            {
                NHibernateSessionManager.Instance.RollbackTransactionOn();
                LogException(ex, "BContract", "UpdateContract");
                throw ex;
            }
        }


        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal DeleteContract(Contract contract, UIActionType UAT)
        {
            try
            {
                return base.SaveChanges(contract, UAT);
            }
            catch (Exception ex)
            {
                LogException(ex, "BContract", "DeleteContract");
                throw ex;
            }
        }




    }
}
