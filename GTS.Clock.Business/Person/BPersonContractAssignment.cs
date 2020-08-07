using GTS.Clock.Business.Contracts;
using GTS.Clock.Business.Security;
using GTS.Clock.Infrastructure;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Model;
using GTS.Clock.Model.Contracts;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Business.Assignments
{
    public class BPersonContractAssignment : BaseBusiness<PersonContractAssignment>
    {
        const string ExceptionSrc = "GTS.Clock.Business.Assignments.BPersonContractAssignment";
        private EntityRepository<PersonContractAssignment> personContractAssignmentRepository = new EntityRepository<PersonContractAssignment>(false);
        private SysLanguageResource systemLanguage;
        private ISession NHSession = NHibernateSessionManager.Instance.GetSession();

        public BPersonContractAssignment()
        {
            systemLanguage = SysLanguageResource.Parsi;
        }

        public BPersonContractAssignment(SysLanguageResource sysLanguageResource)
        {
            systemLanguage = sysLanguageResource;
        }

        public BPersonContractAssignment(LanguagesName sysLanguage)
        {
            if (sysLanguage == LanguagesName.Parsi)
                systemLanguage = SysLanguageResource.Parsi;
            else
                systemLanguage = SysLanguageResource.English;
        }

        public override IList<PersonContractAssignment> GetAll()
        {
            try
            {
                throw new IllegalServiceAccess("استفاده از این متد بی معنا میباشد", ExceptionSrc);
            }
            catch (Exception ex)
            {
                LogException(ex, "BPersonContractAssignment", "GetAll");
                throw ex;
            }
        }

        public IList<PersonContractAssignment> GetAll(decimal personId)
        {
            try
            {
                if (personId > 0)
                {
                    IList<PersonContractAssignment> list = personContractAssignmentRepository.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new PersonContractAssignment().Person), new Person() { ID = personId }),
                                                                                                            new CriteriaStruct(Utility.GetPropertyName(() => new PersonContractAssignment().IsDeleted), false)
                                                                                                           );
                    list = list.OrderBy(x => x.FromDate).ToList();
                    foreach (PersonContractAssignment assign in list)
                    {
                        assign.UIFromDate = Utility.ToPersianDate(assign.FromDate);
                        assign.UIToDate = assign.ToDate > Utility.GTSMinStandardDateTime ? Utility.ToPersianDate(assign.ToDate) : string.Empty;
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
                LogException(ex, "BPersonContractAssignment", "GetAll");
                throw ex;
            }
        }

        public IList<PersonContractAssignment> GetAllByContractId(decimal contractId)
        {
            try
            {
                IList<PersonContractAssignment> list = personContractAssignmentRepository.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new PersonContractAssignment().Contract), new Contract() { ID = contractId }),
                                                                                                        new CriteriaStruct(Utility.GetPropertyName(() => new PersonContractAssignment().IsDeleted), false)
                                                                                                       );
                return list;
            }
            catch (Exception ex)
            {
                LogException(ex, "BPersonContractAssignment", "GetAllByContractId");
                throw ex;
            }
        }

        public override PersonContractAssignment GetByID(decimal objID)
        {
            try
            {
                if (objID > 0)
                {
                    PersonContractAssignment assign = base.GetByID(objID);
                    assign.UIFromDate = Utility.ToPersianDate(assign.FromDate);
                    assign.UIToDate = assign.ToDate > Utility.GTSMinStandardDateTime ? Utility.ToPersianDate(assign.ToDate) : string.Empty;
                    return assign;
                }
                else
                {
                    throw new ItemNotExists("پرسنل مشخص نشده است - خطا در مرورگر", ExceptionSrc);
                }
            }
            catch (Exception ex)
            {
                LogException(ex, "BPersonContractAssignment", "GetByID");
                throw ex;
            }
        }

        public bool ExsitsForPerson(decimal personId)
        {
            if (personContractAssignmentRepository.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new PersonContractAssignment().Person), new Person() { ID = personId }),
                                                                      new CriteriaStruct(Utility.GetPropertyName(() => new PersonContractAssignment().IsDeleted), false)) > 0)
            {
                return true;
            }
            return false;
        }

        public IList<Contract> GetAllContracts()
        {
            try
            {
                BContract bContract = new BContract();
                IList<Contract> list = bContract.GetAll();
                if (list != null && list.Count > 0)
                {
                    list = list.OrderBy(x => x.Title).ToList();
                }
                return list;
            }
            catch (Exception ex)
            {
                LogException(ex, "BPersonContractAssignment", "GetAllContracts");
                throw ex;
            }
        }

        protected override void GetReadyBeforeSave(PersonContractAssignment obj, UIActionType action)
        {
            if (systemLanguage == SysLanguageResource.Parsi)
            {
                obj.FromDate = Utility.ToMildiDate(obj.UIFromDate);
                obj.ToDate = Utility.ToMildiDate(obj.UIToDate);
            }
            else if (systemLanguage == SysLanguageResource.English)
            {
                obj.FromDate = Utility.ToMildiDateTime(obj.UIFromDate);
                obj.ToDate = Utility.ToMildiDateTime(obj.UIToDate);
            }
        }

        private void CheckUserInterfaceRuleGroup(Person person)
        {
            try
            {
                if ((person.PersonTASpec.UIValidationGroup != null && person.PersonTASpec.OldUserInterfaceRuleGroupID == 0) || (person.PersonTASpec.UIValidationGroup != null && person.PersonTASpec.OldUserInterfaceRuleGroupID != 0 && person.PersonTASpec.OldUserInterfaceRuleGroupID != person.PersonTASpec.UIValidationGroup.ID))
                {
                    DateTime calculationLockDate = base.UIValidator.GetCalculationLockDate(person);
                }
            }
            catch (Exception ex)
            {
                LogException(ex, "BPersonContractAssignment", "CheckUserInterfaceRuleGroup");
                throw ex;
            }
        }



        protected override void InsertValidate(PersonContractAssignment personContractAssignment)
        {
            UIValidationExceptions exception = new UIValidationExceptions();
            EntityRepository<Contract> contractRep = new EntityRepository<Contract>(false);
            PersonRepository personRep = new PersonRepository(false);
            PersonContractAssignment PersonContractAssignmentAlias = null;
            Person PersonAlias = null;

            this.CheckUserInterfaceRuleGroup(personContractAssignment.Person);

            if (personContractAssignment.Person == null || personRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new Person().ID), personContractAssignment.Person.ID)) == 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.PersonContractAssignmentPersonIdNotExsits, "پرسنلی با این مشخصات یافت نشد", ExceptionSrc));
            }
            if (personContractAssignment.Contract == null || contractRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new Contract().ID), personContractAssignment.Contract.ID)) == 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.PersonContractAssignmentContractIdNotExsits, "قرارداد با این مشخصات یافت نشد", ExceptionSrc));
            }
            if (personContractAssignment.FromDate < Utility.GTSMinStandardDateTime || personContractAssignment.ToDate < Utility.GTSMinStandardDateTime)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.PersonContractAssignmentDateSmallerThanStandardValue, "مقدار تاریخ انتساب قرارداد از حد مجاز کمتر میباشد", ExceptionSrc));
            }
            if (personContractAssignment.ToDate != Utility.GTSMinStandardDateTime && personContractAssignment.FromDate > personContractAssignment.ToDate)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.PersonContractAssignmentFromDateGreaterThanToDate, "تاریخ انتساب ابتدای قرارداد از انتها بزرگتر است", ExceptionSrc));
            }
            else if (exception.Count == 0)
            {
                IList<PersonContractAssignment> personContractAssignmentList = NHSession.QueryOver<PersonContractAssignment>(() => PersonContractAssignmentAlias)
                                                                                        .JoinAlias(() => PersonContractAssignmentAlias.Person, () => PersonAlias)
                                                                                        .Where(() => !PersonContractAssignmentAlias.IsDeleted &&
                                                                                                      PersonAlias.ID == personContractAssignment.Person.ID
                                                                                              )
                                                                                        .List<PersonContractAssignment>();
                if (personContractAssignmentList != null && personContractAssignmentList.Count > 0)
                {
                    personContractAssignmentList.ToList()
                                                .ForEach(x =>
                                                              {
                                                                if (x.ToDate == Utility.GTSMinStandardDateTime)
                                                                x.ToDate = DateTime.MaxValue.Date;
                                                              }
                                                        );
                }

                if (personContractAssignment.ToDate == Utility.GTSMinStandardDateTime)
                    personContractAssignment.ToDate = DateTime.MaxValue.Date;

                if (this.CheckPersonContractAssignmentConfilct(personContractAssignmentList.ToList(), personContractAssignment))
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.PersonContractAssignmentDateHasConfilict, "تاریخ انتساب قرارداد با تاریخ های قبلی همپوشانی دارد", ExceptionSrc));
                }

                if (personContractAssignment.ToDate == DateTime.MaxValue.Date)
                    personContractAssignment.ToDate = Utility.GTSMinStandardDateTime;
            }
            if (exception.Count == 0 && personContractAssignment.Person != null && personContractAssignment.Person.ID != 0)
            {
                BPerson bPerson = new BPerson();
                Person person = bPerson.GetByID(personContractAssignment.Person.ID);

                if (person != null && person.ID != 0 && personContractAssignment.FromDate < person.EmploymentDate)
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.personContractAssignmentFromDateMustNotLessThanEmploymentDate, "تاریخ انتساب ابتدای قرارداد نباید از تاریخ استخدام کوچکتر باشد", ExceptionSrc));
                }

                if (person.PersonContractAssignmentList != null && person.PersonContractAssignmentList.Count > 0)
                    person.PersonContractAssignmentList.ToList().ForEach(x => { this.NHSession.Evict(x); });
                this.NHSession.Evict(person);
            }
            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        protected override void UpdateValidate(PersonContractAssignment personContractAssignment)
        {
            UIValidationExceptions exception = new UIValidationExceptions();
            EntityRepository<Contract> contractRep = new EntityRepository<Contract>(false);
            PersonRepository personRep = new PersonRepository(false);
            PersonContractAssignment PersonContractAssignmentAlias = null;
            Person PersonAlias = null;

            this.CheckUserInterfaceRuleGroup(personContractAssignment.Person);

            if (personContractAssignment.Person == null || personRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new Person().ID), personContractAssignment.Person.ID)) == 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.PersonContractAssignmentPersonIdNotExsits, "پرسنلی با این مشخصات یافت نشد", ExceptionSrc));
            }
            if (personContractAssignment.Contract == null || contractRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new Contract().ID), personContractAssignment.Contract.ID)) == 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.PersonContractAssignmentContractIdNotExsits, "قرارداد با این مشخصات یافت نشد", ExceptionSrc));
            }
            if (personContractAssignment.FromDate < Utility.GTSMinStandardDateTime || personContractAssignment.ToDate < Utility.GTSMinStandardDateTime)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.PersonContractAssignmentDateSmallerThanStandardValue, "مقدار تاریخ انتساب قرارداد از حد مجاز کمتر میباشد", ExceptionSrc));
            }
            if (personContractAssignment.ToDate != Utility.GTSMinStandardDateTime && personContractAssignment.FromDate > personContractAssignment.ToDate)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.PersonContractAssignmentFromDateGreaterThanToDate, "تاریخ انتساب ابتدای قرارداد از انتها بزرگتر است", ExceptionSrc));
            }
            if (exception.Count == 0)
            {
                IList<PersonContractAssignment> personContractAssignmentList = NHSession.QueryOver<PersonContractAssignment>(() => PersonContractAssignmentAlias)
                                                                                        .JoinAlias(() => PersonContractAssignmentAlias.Person, () => PersonAlias)
                                                                                        .Where(() => !PersonContractAssignmentAlias.IsDeleted &&
                                                                                                      PersonAlias.ID == personContractAssignment.Person.ID &&
                                                                                                      PersonContractAssignmentAlias.ID != personContractAssignment.ID
                                                                                              )
                                                                                        .List<PersonContractAssignment>();
                if (personContractAssignmentList != null && personContractAssignmentList.Count > 0)
                {
                    personContractAssignmentList.ToList()
                                                .ForEach(x =>
                                                              {
                                                                if (x.ToDate == Utility.GTSMinStandardDateTime)
                                                                    x.ToDate = DateTime.MaxValue.Date;
                                                              }
                                                        );
                }

                if (personContractAssignment.ToDate == Utility.GTSMinStandardDateTime)
                    personContractAssignment.ToDate = DateTime.MaxValue.Date;

                if (this.CheckPersonContractAssignmentConfilct(personContractAssignmentList.ToList(), personContractAssignment))
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.PersonContractAssignmentDateHasConfilict, "تاریخ انتساب قرارداد با تاریخ های قبلی همپوشانی دارد", ExceptionSrc));
                }

                if (personContractAssignment.ToDate == DateTime.MaxValue.Date)
                    personContractAssignment.ToDate = Utility.GTSMinStandardDateTime;
            }
            if (exception.Count == 0 && personContractAssignment.Person != null && personContractAssignment.Person.ID != 0)
            {
                BPerson bPerson = new BPerson();
                Person person = bPerson.GetByID(personContractAssignment.Person.ID);

                if (person != null && person.ID != 0 && personContractAssignment.FromDate < person.EmploymentDate)
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.personContractAssignmentFromDateMustNotLessThanEmploymentDate, "تاریخ انتساب ابتدای قرارداد نباید از تاریخ استخدام کوچکتر باشد", ExceptionSrc)); 
                }

                if (person.PersonContractAssignmentList != null && person.PersonContractAssignmentList.Count > 0)
                    person.PersonContractAssignmentList.ToList().ForEach(x => { this.NHSession.Evict(x); });
                this.NHSession.Evict(person);
            }
            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        public bool CheckPersonContractAssignmentConfilct(List<PersonContractAssignment> personContractAssignmentList, PersonContractAssignment personContractAssignment)
        {
            try
            {
                bool isAssignmentsConflictOccured = personContractAssignmentList.Where(x =>
                                                                         (
                                                                           x.FromDate <= personContractAssignment.FromDate &&
                                                                           x.FromDate <= personContractAssignment.ToDate &&
                                                                           x.ToDate >= personContractAssignment.FromDate &&
                                                                           x.ToDate >= personContractAssignment.ToDate
                                                                         ) ||
                                                                         (
                                                                           x.FromDate >= personContractAssignment.FromDate &&
                                                                           x.FromDate <= personContractAssignment.ToDate &&
                                                                           x.ToDate >= personContractAssignment.FromDate &&
                                                                           x.ToDate >= personContractAssignment.ToDate
                                                                         ) ||
                                                                         (
                                                                           x.FromDate <= personContractAssignment.FromDate &&
                                                                           x.FromDate <= personContractAssignment.ToDate &&
                                                                           x.ToDate >= personContractAssignment.FromDate &&
                                                                           x.ToDate <= personContractAssignment.ToDate
                                                                         ) ||
                                                                         (
                                                                           x.FromDate >= personContractAssignment.FromDate &&
                                                                           x.FromDate <= personContractAssignment.ToDate &&
                                                                           x.ToDate >= personContractAssignment.FromDate &&
                                                                           x.ToDate <= personContractAssignment.ToDate
                                                                         )
                                                                 )
                                                            .Any();
                personContractAssignmentList.ToList()
                            .ForEach(x =>
                            {
                                if (x.ToDate == DateTime.MaxValue.Date)
                                    x.ToDate = Utility.GTSMinStandardDateTime;
                            }
                                    );

                return isAssignmentsConflictOccured;
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionSrc, "CheckPersonContractAssignmentConfilct");
                throw ex;
            }
        }

        protected override void DeleteValidate(PersonContractAssignment personContractAssignment)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            if (this.GetPersonContractAssignmentCount(personContractAssignment) <= 1)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.PersonMustHaveOneContractAssignment, "حداقل یک قرارداد باید به پرسنل تخصیص داده شده باشد", ExceptionSrc));                
            }

            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        private int GetPersonContractAssignmentCount(PersonContractAssignment personContractAssignment)
        {
            try
            {
                Person personAlias = null;
                PersonContractAssignment personContractAssignmentAlias = null;
                int count = NHSession.QueryOver<PersonContractAssignment>(() => personContractAssignmentAlias)
                                     .JoinAlias(() => personContractAssignmentAlias.Person, () => personAlias)
                                     .Where(() => !personContractAssignmentAlias.IsDeleted &&
                                                   personAlias.ID == personContractAssignment.Person.ID
                                           )
                                     .RowCount();
                return count;
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionSrc, "GetPersonContractAssignmentCount");
                throw;
            }
        }

        protected override void UpdateCFP(PersonContractAssignment obj, UIActionType action)
        {
            if (action == UIActionType.ADD || action == UIActionType.EDIT)
            {
                decimal personId = obj.Person.ID;
                DateTime newCfpDate = obj.FromDate;
                base.UpdateCFP(personId, newCfpDate);
            }
        }

        /// <summary>
        /// بدون ایجاد ترانزاکشن و آماده سازی عمل درج را انجام میدهد
        /// </summary>
        /// <param name="assignRule"></param>
        /// <returns></returns>
        public decimal InsertWithoutTransaction(PersonContractAssignment personContractAssignment)
        {
            try
            {
                personContractAssignmentRepository.WithoutTransactSave(personContractAssignment);
                return personContractAssignment.ID;
            }
            catch (Exception ex)
            {
                LogException(ex, "BPersonContractAssignment", "InsertWithoutTransaction");
                throw ex;
            }
        }

        protected override bool Delete(PersonContractAssignment obj)
        {
            if (obj != null && obj.ID > 0)
            {
                NHSession.Evict(obj);
                PersonContractAssignment personContractAssignment = this.GetByID(obj.ID);
                personContractAssignment.IsDeleted = true;
                base.Update(personContractAssignment);
                return true;
            }
            return false;
        }

        public IList<PersonContractAssignment> GetPersonContractAssignmentsLessThanEmploymentDate(decimal personID, DateTime date)
        {
            try
            {
                Person personAlias = null;
                PersonContractAssignment PersonContractAssignmentAlias = null;

                IList<PersonContractAssignment> personContractAssignmentList = this.NHSession.QueryOver<PersonContractAssignment>(() => PersonContractAssignmentAlias)
                                                                                             .JoinAlias(() => PersonContractAssignmentAlias.Person, () => personAlias)
                                                                                             .Where(() => !personAlias.IsDeleted &&
                                                                                                          !PersonContractAssignmentAlias.IsDeleted &&
                                                                                                           PersonContractAssignmentAlias.FromDate < date.Date &&
                                                                                                           personAlias.ID == personID
                                                                                                   )
                                                                                             .List<PersonContractAssignment>();
                return personContractAssignmentList;
            }
            catch (Exception ex)
            {
                LogException(ex, "BPersonContractAssignment", "GetPersonContractAssignmentsByDate");
                throw ex;
            }
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertContract_OnPersonnelInsert(PersonContractAssignment contractAssignment, UIActionType UAT)
        {
            return base.SaveChanges(contractAssignment, UAT);
        }
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal UpdateContract_OnPersonnelInsert(PersonContractAssignment contractAssignment, UIActionType UAT)
        {
            return base.SaveChanges(contractAssignment, UAT);
        }
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal DeleteContract_OnPersonnelInsert(PersonContractAssignment contractAssignment, UIActionType UAT)
        {
            return base.SaveChanges(contractAssignment, UAT);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertContract_OnPersonnelUpdate(PersonContractAssignment contractAssignment, UIActionType UAT)
        {
            return base.SaveChanges(contractAssignment, UAT);
        }
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal UpdateContract_OnPersonnelUpdate(PersonContractAssignment contractAssignment, UIActionType UAT)
        {
            return base.SaveChanges(contractAssignment, UAT);
        }
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal DeleteContract_OnPersonnelUpdate(PersonContractAssignment contractAssignment, UIActionType UAT)
        {
            return base.SaveChanges(contractAssignment, UAT);
        }
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckContractsLoadAccess_OnPersonnelInsert()
        {

        }
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckContractsLoadAccess_OnPersonnelUpdate()
        {

        }


    }
}
