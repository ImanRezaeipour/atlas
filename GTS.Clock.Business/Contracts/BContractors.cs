using GTS.Clock.Business.Security;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Model.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;
using NHibernate.Linq;
using NHibernate.Transaction;
using NHibernate.Criterion;
using NHibernate;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure.Repository;

namespace GTS.Clock.Business.Contracts
{
    public class BContractors : BaseBusiness<Contractor>
    {
        private NHibernate.ISession NHSession = NHibernateSessionManager.Instance.GetSession();
        const string ExceptionSrc = "GTS.Clock.Business.Contracts.BContractors";
        private EntityRepository<Contractor> objectRep = new EntityRepository<Contractor>();
        EntityRepository<Contractor> ContractorRep = new EntityRepository<Contractor>(false);
        public BContractors()
        {

        }
        public IList<Contractor> GetContractorByPaging(int pageIndex, int pageSize, string searchValue)
        {
            IList<Contractor> ContractorList = new List<Contractor>();
            try
            {
                Contractor contractorAlias = null;
                ContractorList = NHSession.QueryOver(() => contractorAlias)
                                          .Where(() => contractorAlias.Name.IsInsensitiveLike(searchValue, MatchMode.Anywhere) ||
                                                       contractorAlias.Code.IsInsensitiveLike(searchValue, MatchMode.Anywhere) ||
                                                       contractorAlias.EconomicCode.IsInsensitiveLike(searchValue, MatchMode.Anywhere)
                                                )
                                         .Skip(pageIndex * pageSize)
                                         .Take(pageSize)
                                         .List<Contractor>();
                return ContractorList;

            }
            catch (Exception ex)
            {
                LogException(ex, "BContractors", "GetContractorByPaging");
                throw ex;

            }
        }
        public int GetContractorByPagingCount(string SearchValue)
        {
            try
            {
                Contractor contractorAlias = null;
                int Count = NHSession.QueryOver(() => contractorAlias)
                                     .Where(() => contractorAlias.Name.IsInsensitiveLike(SearchValue, MatchMode.Anywhere) ||
                                                  contractorAlias.Code.IsInsensitiveLike(SearchValue, MatchMode.Anywhere) ||
                                                  contractorAlias.EconomicCode.IsInsensitiveLike(SearchValue, MatchMode.Anywhere)
                                            )
                                     .RowCount();
                return Count;
            }
            catch (Exception ex)
            {
                LogException(ex, "BContract", "GetContractorByPagingCount");
                throw ex;
            }
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertContractor(Contractor contractor, UIActionType UAT)
        {
            using (NHibernateSessionManager.Instance.BeginTransactionOn())
            {
                try
                {
                    this.ChangeContractorIsDefault(contractor);
                    NHibernateSessionManager.Instance.CommitTransactionOn();
                    return base.SaveChanges(contractor, UAT);
                }
                catch (Exception ex)
                {
                    NHibernateSessionManager.Instance.RollbackTransactionOn();
                    LogException(ex, "BContractor", "InsertContractor");
                    throw ex;
                }
            }
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal UpdateContractor(Contractor contractor, UIActionType UAT)
        {

            using (NHibernateSessionManager.Instance.BeginTransactionOn())
            {
                try
                {
                    this.ChangeContractorIsDefault(contractor);
                    NHibernateSessionManager.Instance.CommitTransactionOn();
                    return base.SaveChanges(contractor, UAT);
                }
                catch (Exception ex)
                {
                    NHibernateSessionManager.Instance.RollbackTransactionOn();
                    LogException(ex, "BContractor", "UpdateContractor");
                    throw ex;
                }
            }
        }

        public void ChangeContractorIsDefault(Contractor Obj)
        {
            if (Obj.IsDefault)
            {
                Contractor contractorAlias = null;
                Contractor contractorObj = NHSession.QueryOver(() => contractorAlias)
                                                     .Where(() => contractorAlias.IsDefault == true &&
                                                                  contractorAlias.ID != Obj.ID
                                                           )
                                                     .SingleOrDefault();
                if (contractorObj != null)
                {
                    contractorObj.IsDefault = false;
                    ContractorRep.WithoutTransactSave(contractorObj);
                }                   
            }
        }
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal DeleteContractor(Contractor Contractor, UIActionType UAT)
        {
            return base.SaveChanges(Contractor, UAT);
        }
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckContractorsLoadAccess()
        {
        }
        protected override void InsertValidate(Contractor obj)
        {
            UIValidationExceptions exception = new UIValidationExceptions();
            if (Utility.IsEmpty(obj.Name))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.ContractorNameIsEmpty, "نام پیمانکار نباید خالی باشد", ExceptionSrc));
            }
            if (obj.Code != string.Empty && objectRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new Contractor().Code), obj.Code.ToLower()),
                                               new CriteriaStruct(Utility.GetPropertyName(() => new Contractor().ID), obj.ID, CriteriaOperation.NotEqual)) > 0
               )
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.ContractorCodeRepeated, "کد پیمانکار تکراری می باشد", ExceptionSrc));
            }
            if (obj.EconomicCode != string.Empty && objectRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new Contractor().EconomicCode), obj.EconomicCode.ToLower()),
                                               new CriteriaStruct(Utility.GetPropertyName(() => new Contractor().ID), obj.ID, CriteriaOperation.NotEqual)) > 0
               )
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.ContractorEconomicCodeRepeated, "کد اقتصادی تکراری می باشد", ExceptionSrc));
            }
            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        protected override void UpdateValidate(Contractor obj)
        {
            UIValidationExceptions exception = new UIValidationExceptions();
            Boolean IsDefault = this.GetIsDefault(obj);
            if (Utility.IsEmpty(obj.Name))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.ContractorNameIsEmpty, "نام پیمانکار نباید خالی باشد", ExceptionSrc));
            }
            if (obj.Code != string.Empty && objectRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new Contractor().Code), obj.Code.ToLower()),
                                               new CriteriaStruct(Utility.GetPropertyName(() => new Contractor().ID), obj.ID, CriteriaOperation.NotEqual)) > 0
               )
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.ContractorCodeRepeated, "کد پیمانکار تکراری می باشد", ExceptionSrc));
            }
            if (obj.EconomicCode != string.Empty && objectRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new Contractor().EconomicCode), obj.EconomicCode.ToLower()),
                                               new CriteriaStruct(Utility.GetPropertyName(() => new Contractor().ID), obj.ID, CriteriaOperation.NotEqual)) > 0
               )
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.ContractorEconomicCodeRepeated, "کد اقتصادی تکراری می باشد", ExceptionSrc));
            }
            if (IsDefault == true && obj.IsDefault == false)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.AtLeastOneContractorMustBeSelectForDefault, "حداقل یکی از پیمانکاران به صورت پیش فرض باید انتخاب شود", ExceptionSrc));
            }
            if (exception.Count > 0)
            {
                throw exception;
            }
        }
        public Boolean GetIsDefault(Contractor obj)
        {
            Contractor contractorAlias = null;
            Contractor contractor = NHSession.QueryOver(() => contractorAlias)
                                      .Where(() => contractorAlias.ID == obj.ID)
                                      .SingleOrDefault();
            NHSession.Evict(contractor);
            return contractor.IsDefault;
        }
        public bool CheckIsContractInUse(Contractor obj)
        {
            try
            {
                bool IsExist = false;
                Contract contractAlias = null;
                Contractor contractorAlias = null;
                int count = NHSession.QueryOver(() => contractAlias)
                                     .JoinAlias(() => contractAlias.Contractor, () => contractorAlias)
                                     .Where(() => contractorAlias.ID == obj.ID)
                                     .RowCount();
                if (count > 0)
                    IsExist = true;
                return IsExist;
            }
            catch (Exception ex)
            {
                LogException(ex, "BContractor", "CheckIsContractInUse");
                throw ex;
            }
        }
        protected override void DeleteValidate(Contractor obj)
        {
            UIValidationExceptions exception = new UIValidationExceptions();
            if (CheckIsContractInUse(obj))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.ContractorUseInContract, "بدلیل استفاده در قرارداد نباید حذف شود", ExceptionSrc));
            }
            if(obj.IsDefault)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.AtLeastOneContractorMustBeSelectForDefault, "بدلیل استفاده در قرارداد نباید حذف شود", ExceptionSrc));
            }
            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        public Contractor GetDefaultContractor()
        {
            try
            {
                Contractor contractor = null;
                IList<Contractor> contractorsList = NHSession.QueryOver<Contractor>()
                                                             .Where(x => x.IsDefault)
                                                             .List<Contractor>();
                if (contractorsList != null)
                    contractor = contractorsList.FirstOrDefault();

                return contractor;
            }
            catch (Exception ex)
            {
                LogException(ex, "BContractor", "GetDefaultContractor");
                throw;
            }
        }
    }
}
