using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure.Exceptions;
using GTS.Clock.Model;
using GTS.Clock.Model.BaseInformation;
using GTS.Clock.Model.Charts;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Model.Concepts.Operations;
using GTS.Clock.Business.Security;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Infrastructure;
using GTS.Clock.Business.Charts;
using GTS.Clock.Business.BaseInformation;
using GTS.Clock.Business.Shifts;
using GTS.Clock.Business.Rules;
using GTS.Clock.Business.RequestFlow;
using GTS.Clock.Model.RequestFlow;
using GTS.Clock.Model.UIValidation;
using GTS.Clock.Business.UIValidation;
using GTS.Clock.Model.PersonInfo;
using GTS.Clock.Business.PersonInfo;
using GTS.Clock.Business.Assignments;
using NHibernate;
using NHibernate.Linq;
using NHibernate.Type;
using NHibernate.Transform;
using NHibernate.Criterion;
using NHibernate.SqlCommand;
using NHibernate.SqlTypes;
using GTS.Clock.Business.Temp;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using GTS.Clock.Business.Presentaion_Helper.Proxy;
using System.Text.RegularExpressions;
using GTS.Clock.Model.Security;
using System.Globalization;
using GTS.Clock.Model.Contracts;
using GTS.Clock.Business.Contracts;

//using GTS.Clock.Model.RequestFlow;



namespace GTS.Clock.Business
{
    public class BPerson : BaseBusiness<Person>, ISearchPerson
    {
        #region variables
        PersonRepository personRepository = new PersonRepository(false);
        const string ExceptionSrc = "GTS.Clock.Business.BPerson";
        private SysLanguageResource sysLanguageResource;
        private LocalLanguageResource localLanguageResource;
        private BTemp bTemp = new BTemp();
        int operationBatchSizeValue = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings[OperationBatchSize.OperationBatchSizeKey.ToString()]);
        private ISession NHSession = NHibernateSessionManager.Instance.GetSession();
        EntityRepository<Person> PersonRep = new EntityRepository<Person>(false);
        private EntityRepository<PersonDetail> PersonDetailRep = new EntityRepository<PersonDetail>();
        #endregion

        #region Constructor

        public BPerson(SysLanguageResource sysLanguage, LocalLanguageResource localLanguage)
        {
            //this.CheckTASpecPerson();
            sysLanguageResource = sysLanguage;
            localLanguageResource = localLanguage;
        }

        public BPerson()
        {
            //this.CheckTASpecPerson();

            if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
            {
                sysLanguageResource = SysLanguageResource.Parsi;
            }
            else
            {
                sysLanguageResource = SysLanguageResource.English;
            }

            if (BLanguage.CurrentLocalLanguage == LanguagesName.Parsi)
            {
                localLanguageResource = LocalLanguageResource.Parsi;
            }
            else
            {
                localLanguageResource = LocalLanguageResource.English;
            }
        }

        /// <summary>
        /// یک شخص را در دیتابیس درج میکند تا بتوان عملیات 
        /// مربوط به پرسنل را انجام داد
        /// </summary>
        /// <returns>شناسه شخص که باید در کلاینت ذخیره شود</returns>
        public decimal CreateWorkingPerson()
        {
            try
            {
                new PersonRepository().DeleteExtraRecordFromDB();

                Person workingPerson = new Person();
                PersonDetail detail = new PersonDetail();
                PersonTASpec prsTASpec = new PersonTASpec();
                PersonCLSpec prsCLSpec = new PersonCLSpec();
                EntityRepository<PersonDetail> detailRepository = new EntityRepository<PersonDetail>(false);
                EntityRepository<PersonTASpec> prsTASpecRepository = new EntityRepository<PersonTASpec>(false);
                EntityRepository<PersonCLSpec> prsCLSpecRepository = new EntityRepository<PersonCLSpec>(false);
                using (NHibernateSessionManager.Instance.BeginTransactionOn())
                {
                    workingPerson.PersonCode = "00000000";
                    workingPerson.CardNum = "00000000";
                    personRepository.WithoutTransactSave(workingPerson);

                    detail.ID = workingPerson.ID;
                    detailRepository.WithoutTransactSave(detail);

                    prsTASpec.ID = workingPerson.ID;
                    prsTASpecRepository.WithoutTransactSave(prsTASpec);

                    prsCLSpec.ID = workingPerson.ID;
                    prsCLSpecRepository.WithoutTransactSave(prsCLSpec);

                    NHibernateSessionManager.Instance.CommitTransactionOn();
                    return workingPerson.ID;
                }
            }
            catch (Exception ex)
            {
                NHibernateSessionManager.Instance.RollbackTransactionOn();
                LogException(ex, "BPerson", "CreateWorkingPerson()");
                throw ex;
            }
        }

        //جهت تست و موقت
        public decimal CreateWorkingPerson2()
        {
            try
            {
                Person workingPerson = new Person();
                base.Insert(workingPerson);
                PersonDetail detail = new PersonDetail();
                detail.ID = workingPerson.ID;
                EntityRepository<PersonDetail> detailRepository = new EntityRepository<PersonDetail>(false);
                detailRepository.Save(detail);
                workingPerson.PersonDetail = detail;
                base.Update(workingPerson);
                return workingPerson.ID;
            }
            catch (Exception ex)
            {
                LogException(ex, "BPerson", "CreateWorkingPerson()");
                throw ex;
            }
        }

        #endregion

        #region Current Assigns

        public virtual string GetCurrentActiveWorkGroup(decimal personId)
        {
            try
            {
                EntityRepository<AssignWorkGroup> assignRep = new EntityRepository<AssignWorkGroup>(false);
                IList<AssignWorkGroup> list = assignRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new AssignWorkGroup().Person), new Person() { ID = personId }),
                                                                      new CriteriaStruct(Utility.GetPropertyName(() => new AssignWorkGroup().FromDate), DateTime.Now.Date, CriteriaOperation.LessEqThan));

                if (list.Count > 0)
                {
                    AssignWorkGroup assign = list.OrderBy(x => x.FromDate).LastOrDefault();
                    if (assign.WorkGroup != null)
                    {
                        return assign.WorkGroup.Name;
                    }
                }

                return String.Empty;
            }
            catch (Exception ex)
            {
                LogException(ex, "BPerson", "GetCurrentActiveWorkGroup");
                throw ex;
            }
        }

        public virtual string GetCurrentActiveDateRange(decimal personId)
        {
            try
            {
                EntityRepository<PersonRangeAssignment> assignRep = new EntityRepository<PersonRangeAssignment>(false);
                IList<PersonRangeAssignment> list = assignRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new PersonRangeAssignment().Person), new Person() { ID = personId }),
                                                                      new CriteriaStruct(Utility.GetPropertyName(() => new PersonRangeAssignment().FromDate), DateTime.Now.Date, CriteriaOperation.LessEqThan));

                if (list.Count > 0)
                {
                    PersonRangeAssignment assign = list.OrderBy(x => x.FromDate).LastOrDefault();
                    if (assign.CalcDateRangeGroup != null)
                    {
                        return assign.CalcDateRangeGroup.Name;
                    }
                }

                return String.Empty;
            }
            catch (Exception ex)
            {
                LogException(ex, "BPerson", "GetCurrentActiveDateRange");
                throw ex;
            }
        }

        public virtual string GetCurrentActiveRuleGroup(decimal personID)
        {
            try
            {
                EntityRepository<PersonRuleCatAssignment> assignRep = new EntityRepository<PersonRuleCatAssignment>(false);
                IList<PersonRuleCatAssignment> list = assignRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new PersonRuleCatAssignment().Person), new Person() { ID = personID }));
                if (list.Count > 0)
                {
                    PersonRuleCatAssignment assign = list.Where(x => Convert.ToDateTime(x.FromDate) <= DateTime.Now.Date && DateTime.Now.Date <= Convert.ToDateTime(x.ToDate))
                                                         .OrderBy(x => x.FromDate).LastOrDefault();
                    if (assign != null && assign.RuleCategory != null)
                    {
                        return assign.RuleCategory.Name;
                    }
                }
                return String.Empty;
            }
            catch (Exception ex)
            {
                LogException(ex, "BPerson", "GetCurrentActiveRuleGroup");
                throw ex;
            }
        }

        public virtual string GetCurrentActiveContract(decimal personID)
        {
            try
            {
                Person personAlias = null;
                PersonContractAssignment personContractAssignmentAlias = null;

                IList<PersonContractAssignment> list = NHSession.QueryOver<PersonContractAssignment>(() => personContractAssignmentAlias)
                                                                .JoinAlias(() => personContractAssignmentAlias.Person, () => personAlias)
                                                                .Where(() => !personContractAssignmentAlias.IsDeleted &&
                                                                              personAlias.ID == personID &&
                                                                              (
                                                                                (personContractAssignmentAlias.FromDate <= DateTime.Now.Date &&
                                                                                 personContractAssignmentAlias.ToDate == Utility.GTSMinStandardDateTime
                                                                                ) ||
                                                                                (personContractAssignmentAlias.FromDate <= DateTime.Now.Date &&
                                                                                 personContractAssignmentAlias.ToDate != Utility.GTSMinStandardDateTime &&
                                                                                 personContractAssignmentAlias.ToDate >= DateTime.Now.Date
                                                                                )
                                                                              )
                                                                      )
                                                                .OrderBy(() => personContractAssignmentAlias.FromDate)
                                                                .Desc
                                                                .List<PersonContractAssignment>();
                if (list != null && list.Count > 0)
                {
                    PersonContractAssignment assign = list.FirstOrDefault();
                    if (assign != null && assign.Contract != null)
                    {
                        return assign.Contract.Title;
                    }
                }
                return String.Empty;
            }
            catch (Exception ex)
            {
                LogException(ex, "BPerson", "GetCurrentActiveContract");
                throw ex;
            }
        }

        //public virtual string GetCurrentActiveEmploymentType(decimal personID)
        //{
        //    try
        //    {
        //        EntityRepository<AssignEmploymentType> assignRep = new EntityRepository<AssignEmploymentType>(false);
        //        IList<AssignEmploymentType> list = assignRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new AssignEmploymentType().Person), new Person() { ID = personID }));
        //        if (list.Count > 0)
        //        {
        //            AssignEmploymentType assign = list.Where(x => Convert.ToDateTime(x.FromDate) <= DateTime.Now.Date && DateTime.Now.Date <= Convert.ToDateTime(x.ToDate))
        //                                                 .OrderBy(x => x.FromDate).LastOrDefault();
        //            if (assign != null && assign.EmploymentType != null)
        //            {
        //                return assign.EmploymentType.Name;
        //            }
        //        }
        //        return String.Empty;
        //    }
        //    catch (Exception ex)
        //    {
        //        LogException(ex, "BPerson", "GetCurrentActiveRuleGroup");
        //        throw ex;
        //    }
        //}
        public virtual PersonRangeAssignment GetCurrentRangeAssignment(decimal personId)
        {
            try
            {
                EntityRepository<PersonRangeAssignment> assignRep = new EntityRepository<PersonRangeAssignment>(false);
                IList<PersonRangeAssignment> list = assignRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new PersonRangeAssignment().Person), new Person() { ID = personId }),
                                                                            new CriteriaStruct(Utility.GetPropertyName(() => new PersonRangeAssignment().FromDate), DateTime.Now.Date, CriteriaOperation.LessEqThan));
                if (list.Count > 0)
                {
                    PersonRangeAssignment assign = list.OrderBy(x => x.FromDate).LastOrDefault();
                    if (assign.CalcDateRangeGroup != null)
                    {
                        return assign;
                    }
                }

                return new PersonRangeAssignment() { CalcDateRangeGroup = new CalculationRangeGroup() };
            }
            catch (Exception ex)
            {
                LogException(ex, "BPerson", "GetCurrentRangeAssignment");
                throw ex;
            }
        }

        #endregion

        #region Get Alls

        public int GetActivePersonCount()
        {
            try
            {
                int count = personRepository.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new Person().Active), true),
                                                                new CriteriaStruct(Utility.GetPropertyName(() => new Person().IsDeleted), false)
                                                               );
                return count;
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw ex;
            }
        }

        /// <summary>
        /// تعداد پرسنل شامل فعال و غیر فعال را برمیگرداند 
        /// </summary>
        /// <returns></returns>
        public int GetPersonCount()
        {
            try
            {
                int count = personRepository.GetPersonCount(BUser.CurrentUser.ID);
                return count;
            }
            catch (Exception ex)
            {
                LogException(ex, "BPerson", "GetPersonCount");
                throw ex;
            }
        }

        /// <summary>
        /// لیست تمامی پرسنل شامل فعال و غیر فعال
        /// </summary>
        /// <param name="pageSize">تعداد در هر صفحه</param>
        /// <param name="pageIndex">شماره صفحه</param>
        /// <returns></returns>
        public IList<Person> GetAllByPage(int pageSize, int pageIndex)
        {
            try
            {
                int count = this.GetPersonCount();
                if (pageSize * pageIndex == 0 || pageSize * pageIndex < count)
                {
                    IList<Person> result = personRepository.GetAllByPage(BUser.CurrentUser.ID, pageIndex, pageSize);
                    return ApplyCultureAndFixCurrentObject(result, this.sysLanguageResource, this.localLanguageResource);
                }
                else
                {
                    throw new OutOfExpectedRangeException("0", Convert.ToString(count - 1), Convert.ToString(pageSize * (pageIndex + 1)), ExceptionSrc + "GetAllByPage");
                }
            }
            catch (Exception ex)
            {
                LogException(ex, "BPerson", "GetAllByPage");
                throw ex;
            }
        }

        /// <summary>
        /// بخشها را برمیگرداند
        /// </summary>
        /// <returns></returns>
        public Department GetAllDepartmentTree()
        {
            try
            {
                Business.Charts.BDepartment departments = new GTS.Clock.Business.Charts.BDepartment();
                return departments.GetDepartmentsTree();
            }
            catch (Exception ex)
            {
                LogException(ex, "BPerson", "GetAllDepartmentTree");
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        public IList<Department> GetAllDepartmentChildTree(decimal departmentId)
        {
            try
            {
                Business.Charts.BDepartment departments = new GTS.Clock.Business.Charts.BDepartment();
                return departments.GetDepartmentChilds(departmentId);
            }
            catch (Exception ex)
            {
                LogException(ex, "BPerson", "GetAllDepartmentChildTree");
                throw ex;
            }
        }

        /// <summary>
        /// لیستی از ایستگاههای کنترل را برکیگرداند
        /// </summary>
        /// <returns></returns>
        public IList<ControlStation> GetAllControlStations()
        {
            try
            {
                Business.BaseInformation.BControlStation bControl = new GTS.Clock.Business.BaseInformation.BControlStation();
                return bControl.GetAll();
            }
            catch (Exception ex)
            {
                LogException(ex, "BPerson", "GetAllControlStations");
                throw ex;
            }
        }

        /// <summary>
        /// درخت چارت سازمانی را برمیگرداند
        /// </summary>
        /// <returns></returns>
        public OrganizationUnit GetAllOrganizationUnitTree()
        {
            try
            {
                Business.Charts.BOrganizationUnit units = new GTS.Clock.Business.Charts.BOrganizationUnit();
                return units.GetOrganizationUnitTree();
            }
            catch (Exception ex)
            {
                LogException(ex, "BPerson", "GetAllOrganizationUnitTree");
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public IList<OrganizationUnit> GetAllOrganizationUnitChildsTree(decimal parentId)
        {
            try
            {
                Business.Charts.BOrganizationUnit units = new GTS.Clock.Business.Charts.BOrganizationUnit();
                return units.GetChilds(parentId);
            }
            catch (Exception ex)
            {
                LogException(ex, "BPerson", "GetAllOrganizationUnitChildsTree");
                throw ex;
            }
        }

        /// <summary>
        /// لیستی از انواع استخدام را برگرداند
        /// </summary>
        /// <returns></returns>
        public IList<EmploymentType> GetAllEmployType()
        {
            try
            {
                Business.BaseInformation.BEmployment bemp = new GTS.Clock.Business.BaseInformation.BEmployment();
                return bemp.GetAll();
            }
            catch (Exception ex)
            {
                LogException(ex, "BPerson", "GetAllEmployType");
                throw ex;
            }
        }
        /// <summary>
        /// لیستی از گروه های واسط کاربری را برگرداند
        /// </summary>
        /// <returns></returns>
        public IList<UIValidationGroup> GetAllUIValidationGroup()
        {
            try
            {
                Business.UIValidation.BUIValidationGroup bemp = new Business.UIValidation.BUIValidationGroup();
                return bemp.GetAllUiValidationGroup();
            }
            catch (Exception ex)
            {
                LogException(ex, "BPerson", "GetAllEmployType");
                throw ex;
            }
        }
        /// <summary>
        /// لیستی از انواع رده را برگرداند
        /// </summary>
        /// <returns></returns>
        public IList<Grade> GetAllGrade()
        {
            try
            {
                Business.BaseInformation.BGrade bGrade = new GTS.Clock.Business.BaseInformation.BGrade();
                return bGrade.GetAll();
            }
            catch (Exception ex)
            {
                LogException(ex, "BPerson", "GetAllGrade");
                throw ex;
            }
        }

        public IList<CostCenter> GetAllCostCenter()
        {
            try
            {
                Business.BaseInformation.BCostCenter bCostCenter = new GTS.Clock.Business.BaseInformation.BCostCenter();
                return bCostCenter.GetAll();
            }
            catch (Exception ex)
            {
                LogException(ex, "BPerson", "GetAllCostCenter");
                throw ex;
            }
        }

        /// <summary>
        /// لیستی از گروه های دوره محاسبات را برمیگرداند
        /// </summary>
        /// <returns></returns>
        public IList<CalculationRangeGroup> GetAllCalculationGroup()
        {
            try
            {
                Business.Rules.BDateRange dateRange = new GTS.Clock.Business.Rules.BDateRange();
                return dateRange.GetAll();
            }
            catch (Exception ex)
            {
                LogException(ex, "BPerson", "GetAllCalculationGroup");
                throw ex;
            }
        }

        /// <summary>
        /// لیستی از گروهای اعتبارسنجی برمیگرداند
        /// </summary>
        /// <returns></returns>
        //public IList<UIValidationGroup> GetAllUIValidationGroup()
        //{
        //    BUIValidationGroup uiVal = new BUIValidationGroup();
        //    return uiVal.GetAll();
        //}
        #endregion

        #region Image

        /// <summary>
        /// تصویر شخص را در دیتابیس ذخیره میکند
        /// </summary>
        /// <param name="personDtlId"></param>
        /// <param name="image"></param>
        /// <returns></returns>
        public decimal UpdatePersonImage(decimal personDtlId, string image)
        {
            try
            {
                return personRepository.UpdatePersonImage(personDtlId, image.Replace("-", string.Empty));
            }
            catch (Exception ex)
            {
                LogException(ex, "BPerson", "UpdatePersonImage");
                throw ex;
            }
        }

        /// <summary>
        /// تصویر شخص را در قالب رشته برمیگرداند
        /// </summary>
        /// <param name="personDtlId"></param>
        /// <returns></returns>
        public string GetPersonImage(decimal personDtlId)
        {
            try
            {
                return personRepository.GetPersonImage(personDtlId);
            }
            catch (Exception ex)
            {
                LogException(ex, "BPerson", "GetPersonImage");
                throw ex;
            }
        }

        #endregion

        #region Insert Update Delete

        /// <summary>
        ///       
        /// </summary>
        /// <param name="person"></param>
        protected override void InsertValidate(Person person)
        {
            throw new IllegalServiceAccess(" عملیات درج برای پرسنل تعریف نشده است و تنها باید بروزرسانی شود زیرا طبق برنامه شخص هنگام تعریف انتسابات درج شده است", ExceptionSrc);
        }

        /// <summary>
        /// نام خالی
        /// نام خانوادگی خالی
        /// جنسیت خالی
        /// تاهل خالی
        /// شماره پرسنلی خالی
        /// بخش خالی
        /// گروه کاری خالی
        /// گروه قوانین خالی
        /// نوع استخدام خالی
        /// محدوده محاسبات خالی
        /// جایگزینی شماره کارت با شماره پرسنلی در صورت خالی بودن شماره کارت
        /// شماره پرسنلی تکرار
        /// شماره کارت تکرار
        /// شماره پرسنلی عددی
        /// کدملی عددی
        /// شماره شناسنامه عددی
        /// شماره کارت عددی
        /// تاریخ شروع استخدام از تاریخ پایان کوچکتر باشد
        /// </summary>
        /// <param name="person"></param>
        protected override void UpdateValidate(Person person)
        {
            UIValidationExceptions exception = new UIValidationExceptions();
            User userAlias = null;
            Person personAlias = null;
            if (person.ID > 0)
            {
                this.CheckUserInterfaceRuleGroup(person);
                if (Utility.IsEmpty(person.FirstName))
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.PersonNameRequied, " نام نباید خالی باشد", ExceptionSrc));
                }
                if (Utility.IsEmpty(person.LastName))
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.PersonLastNameRequierd, " نام خانوادگی نباید خالی باشد", ExceptionSrc));
                }
                if (Utility.IsEmpty(person.Sex))
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.PersonSexRequierd, " جنسیت نباید خالی باشد", ExceptionSrc));
                }
                //if (person.MaritalStatus == 0)
                //{
                //    exception.Add(new ValidationException(ExceptionResourceKeys.PersonMarriedRequierd, " وضیت تاهل نباید خالی باشد", ExceptionSrc));
                //}
                if (Utility.IsEmpty(person.PersonCode))
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.PersonBarcodeRequierd, " بارکد نباید خالی باشد", ExceptionSrc));
                }
                else if (!Utility.IsNumber(person.PersonCode))
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.PersonBarcodeInValid, " بارکد باید تنها شامل عدد باشد", ExceptionSrc));
                }
                else if (personRepository.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => person.IsDeleted), false)
                                                                 , new CriteriaStruct(Utility.GetPropertyName(() => person.BarCode), person.PersonCode.TrimStart('0')),
                                                                  new CriteriaStruct(Utility.GetPropertyName(() => person.ID), person.ID, CriteriaOperation.NotEqual)) > 0)
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.PersonBarcodeRepeated, "بارکد نباید تکراری باشد", ExceptionSrc));
                }
                if (person.Department.ID == 0)
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.PersonDepartmentRequierd, " بخش نباید خالی باشد", ExceptionSrc));
                }
                if (person.CostCenter.ID == 0)
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.PersonCostCenterRequired, " مرکز هزینه نباید خالی باشد", ExceptionSrc));
                }
                if (!new Assignments.BAssignWorkGroup(sysLanguageResource).ExsitsForPerson(person.ID))
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.PersonWorkGroupRequierd, " گروه کاری نباید خالی باشد", ExceptionSrc));
                }
                if (!new Assignments.BAssignDateRange(sysLanguageResource).ExsitsForPerson(person.ID))
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.PersonDateRangeRequierd, " دوره محاسبات نباید خالی باشد", ExceptionSrc));
                }
                if (!new Assignments.BAssignRule(sysLanguageResource).ExsitsForPerson(person.ID))
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.PersonRuleGroupRequierd, " گروه قوانین نباید خالی باشد", ExceptionSrc));
                }
                if (person.EmploymentType.ID == 0)
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.PersonEmploymenttypeRequierd, " نوع استخدام نباید خالی باشد", ExceptionSrc));
                }               
                if (person.PersonTASpec.UIValidationGroup == null || person.PersonTASpec.UIValidationGroup.ID == 0)
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.PersonUIValidationRequierd, " گروه قانون واسط نباید خالی باشد", ExceptionSrc));
                }

                //if (Utility.IsEmpty(person.CardNum) && !Utility.IsEmpty(person.PersonCode))
                //{
                //    person.CardNum = person.PersonCode;
                //}
                //else if (!Utility.IsNumber(person.CardNum))
                //{
                //    exception.Add(new ValidationException(ExceptionResourceKeys.PersonCartNumInValid, "شماره کارت باید تنها شامل عدد باشد", ExceptionSrc));
                //}
                // if (person.CardNum.Length > 8)
                //{
                //    exception.Add(new ValidationException(ExceptionResourceKeys.PersonCardNumberIsTooLong, "شماره کارت باید حداکثر 8 کاراکتر باشد", ExceptionSrc));
                //}
                // else
                // @"[^\W]+$"
                if (!Regex.IsMatch(person.CardNum, @"^[A-Za-z0-9]*$"))
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.PersonCardNumIsNumberOrIsCaracter, "شماره کارت فقط شامل عدد یا کاراکتر می تواند باشد", ExceptionSrc));
                }

                //DNN Note: به درخواست شهرداری حذف شود این قسمت
                //if (personRepository.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => person.CardNum), person.CardNum),
                //                                             new CriteriaStruct(Utility.GetPropertyName(() => person.ID), person.ID, CriteriaOperation.NotEqual),
                //                                             new CriteriaStruct(Utility.GetPropertyName(() => person.IsDeleted), false, CriteriaOperation.Equal)) > 0 && person.CardNum != string.Empty)
                //{
                //    exception.Add(new ValidationException(ExceptionResourceKeys.PersonCartNumberRepeated, "شماره کارت نباید تکراری باشد", ExceptionSrc));
                //}
                if ((!Utility.IsEmpty(person.PersonDetail.MeliCode) && !CustomValidateTargetList.Contains(Utility.GetPropertyName(() => new PersonDetail().MeliCode))))
                {

                    EntityRepository<PersonDetail> dtlRep = new EntityRepository<PersonDetail>(false);
                    if (!Utility.IsNumber(person.PersonDetail.MeliCode))
                    {
                        exception.Add(new ValidationException(ExceptionResourceKeys.PersonMeliCodeInValid, "کد ملی باید تنها شامل عدد باشد", ExceptionSrc));
                    }
                    else if (dtlRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new PersonDetail().MeliCode), person.PersonDetail.MeliCode),
                                                       new CriteriaStruct(Utility.GetPropertyName(() => new PersonDetail().ID), person.ID, CriteriaOperation.NotEqual)) > 0)
                    {
                        exception.Add(new ValidationException(ExceptionResourceKeys.PersonMeliCodeRepeated, "کد ملی نباید تکراری باشد", ExceptionSrc));
                    }
                }
                if (!Utility.IsEmpty(person.EmploymentNum))
                {
                    if (personRepository.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => person.EmploymentNum), person.EmploymentNum),
                                                                 new CriteriaStruct(Utility.GetPropertyName(() => person.ID), person.ID, CriteriaOperation.NotEqual)) > 0)
                    {
                        exception.Add(new ValidationException(ExceptionResourceKeys.PersonEmployeeNumRepeated, "کد استخدامی نباید تکراری باشد", ExceptionSrc));
                    }
                }
                if (!Utility.IsEmpty(person.PersonDetail.ShomareShenasname))
                {
                    if (!Utility.IsNumber(person.PersonDetail.ShomareShenasname))
                    {
                        exception.Add(new ValidationException(ExceptionResourceKeys.PersonShenasnameCodeInValid, " شماره شناسنامه باید تنها شامل عدد باشد", ExceptionSrc));
                    }
                }
                if (person.EmploymentDate == null || person.EmploymentDate == Utility.GTSMinStandardDateTime)
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.PersonEmploymentFromDateRequired, " تاریخ شروع استخدام مقدار نگرفته است", ExceptionSrc));
                }
                if (person.EndEmploymentDate > Utility.GTSMinStandardDateTime && person.EmploymentDate >= person.EndEmploymentDate)
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.PersonEmploymentFromDateGreaterThanToDate, " تاریخ شروع استخدام از انتها بزرگتر است", ExceptionSrc));
                }
                if (person.OrganizationUnit != null && person.OrganizationUnit.ID == 1)
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.AssignedRootAsOrganizationPostNotValid, "تخصیص ریشه به عنوان پست سازمانی مجاز نمی باشد", ExceptionSrc));
                }

                BPersonContractAssignment bPersonContractAssignment = new BPersonContractAssignment();
                IList<PersonContractAssignment> personContractAssignmentList = bPersonContractAssignment.GetPersonContractAssignmentsLessThanEmploymentDate(person.ID, person.EmploymentDate);
                if (personContractAssignmentList != null && personContractAssignmentList.Count > 0)
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.personContractAssignmentFromDateMustNotLessThanEmploymentDate, "تاریخ انتساب ابتدای قرارداد نباید از تاریخ استخدام کوچکتر باشد", ExceptionSrc));
                }
                if (NHibernateSessionManager.Instance.GetSession().QueryOver<User>(() => userAlias)
                                                             .JoinAlias(() => userAlias.Person, () => personAlias)
                                                             .Where(() => userAlias.UserName == person.BarCode &&
                                                                          userAlias.Person.ID != person.ID &&
                                                                          userAlias.Active &&
                                                                         !personAlias.IsDeleted
                                                                   )
                                                             .RowCount() > 0
                   )
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.UsernameReplication, "درج - Username Replication", ExceptionSrc));
                }
            }
            else
            {
                throw new ItemNotExists("شناسه شخص ارسالی معتبر نمیباشد", ExceptionSrc);
            }
            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        #region "Check CustomValidation"

        private void CheckUserInterfaceRuleGroup(Person person)
        {
            try
            {
                if ((person.PersonTASpec.UIValidationGroup != null && person.PersonTASpec.OldUserInterfaceRuleGroupID == 0) || (person.PersonTASpec.UIValidationGroup != null && person.PersonTASpec.OldUserInterfaceRuleGroupID != 0))
                {
                    DateTime calculationLockDate = base.UIValidator.GetCalculationLockDate(person);
                }
            }
            catch (Exception ex)
            {
                LogException(ex, "BPrson", "CheckUserInterfaceRuleGroup");
                throw ex;
            }
        }

        protected override void UpdateCustomValidate(Person person)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            UIValidationExceptions exception = new UIValidationExceptions();
            IList<CustomValidation> customValidationList = new List<CustomValidation>();
            customValidationList = (List<CustomValidation>)SessionHelper.GetSessionValue(SessionHelper.BussinessCustomValidation);
            if (customValidationList == null)
                return;
            CustomValidation customValidationMeliCode = customValidationList.Where(x => x.ClassName == typeof(PersonDetail).FullName && x.FieldName == Utility.GetPropertyName(() => new PersonDetail().MeliCode)).FirstOrDefault();
            List<CustomValidationExpression> CustomValidationExpressionList = js.Deserialize<List<CustomValidationExpression>>(customValidationMeliCode.Expression);
            List<CustomValidationExpressionFormat> CustomValidationExpressionFormatList = js.Deserialize<List<CustomValidationExpressionFormat>>(customValidationMeliCode.Format);


            if (customValidationMeliCode.SubSystem == 1)
            {
                if (!customValidationMeliCode.AllowDuplicate)
                {
                    if (!Utility.IsEmpty(person.PersonDetail.MeliCode))
                    {

                        EntityRepository<PersonDetail> dtlRep = new EntityRepository<PersonDetail>(false);
                        if (dtlRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new PersonDetail().MeliCode), person.PersonDetail.MeliCode),
                                                          new CriteriaStruct(Utility.GetPropertyName(() => new PersonDetail().ID), person.ID, CriteriaOperation.NotEqual)) > 0)
                        {
                            exception.Add(new ValidationException(ExceptionResourceKeys.PersonMeliCodeRepeated, "کد ملی نباید تکراری باشد", ExceptionSrc));
                        }
                    }
                }
                if (!customValidationMeliCode.AllowNull)
                {
                    if (Utility.IsEmpty(person.PersonDetail.MeliCode))
                    {
                        exception.Add(new ValidationException(ExceptionResourceKeys.PersonnelMeliCodeRequierd, " کد ملی نباید خالی باشد", ExceptionSrc));
                    }
                }
                if (person.PersonDetail.MeliCode != string.Empty && person.PersonDetail.MeliCode != null)
                {
                    if (customValidationMeliCode.Length.ToString() != null)
                    {
                        if ((person.PersonDetail.MeliCode.Length > customValidationMeliCode.Length) || (person.PersonDetail.MeliCode.Length < customValidationMeliCode.Length))
                        {
                            exception.Add(new ValidationException(ExceptionResourceKeys.PersonnelMeliCodeLength, " تعداد کاراکتر های کد ملی نامعتبر می باشد", ExceptionSrc));
                        }
                    }
                    if (customValidationMeliCode.Expression != null)
                    {
                        CustomValidationExpression customValidationExpression = new CustomValidationExpression();
                        CustomValidationExpressionFormat customValidationExpressionFormat = new CustomValidationExpressionFormat();
                        switch (BLanguage.CurrentSystemLanguage)
                        {
                            case LanguagesName.Parsi:

                                customValidationExpression = CustomValidationExpressionList.Where(x => x.Clr == "Fa_IR").FirstOrDefault();
                                customValidationExpressionFormat = CustomValidationExpressionFormatList.Where(x => x.Clr == "Fa_IR").FirstOrDefault();
                                if (customValidationExpression != null)
                                {

                                    if (!Regex.IsMatch(person.PersonDetail.MeliCode, customValidationExpression.Exp))
                                    {
                                        exception.Add(new ValidationException(ExceptionResourceKeys.PersonnelMeliCodeExpression, " فرمت کد ملی نامعتبر می باشد", ExceptionSrc));
                                        if (customValidationExpressionFormat != null)
                                        {
                                            ValidationException incorrentNationalCodeException = new ValidationException(ExceptionResourceKeys.PersonnelMeliCodeExpressionCorrectFormat, "فرمت صحیح", ExceptionSrc);
                                            incorrentNationalCodeException.Data.Add("Info", customValidationExpressionFormat.Format);
                                            exception.Add(incorrentNationalCodeException);
                                        }
                                    }
                                }
                                break;
                            case LanguagesName.English:

                                customValidationExpression = CustomValidationExpressionList.Where(x => x.Clr == "En_US").FirstOrDefault();
                                customValidationExpressionFormat = CustomValidationExpressionFormatList.Where(x => x.Clr == "En_US").FirstOrDefault();
                                if (customValidationExpression != null)
                                {
                                    if (!Regex.IsMatch(person.PersonDetail.MeliCode, customValidationExpression.Exp))
                                    {
                                        exception.Add(new ValidationException(ExceptionResourceKeys.PersonnelMeliCodeExpression, " فرمت کلد ملی نامعتبر می باشد", ExceptionSrc));
                                        if (customValidationExpressionFormat != null)
                                        {
                                            ValidationException incorrentNationalCodeException = new ValidationException(ExceptionResourceKeys.PersonnelMeliCodeExpressionCorrectFormat, "فرمت صحیح", ExceptionSrc);
                                            incorrentNationalCodeException.Data.Add("Info", customValidationExpressionFormat.Format);
                                            exception.Add(incorrentNationalCodeException);
                                        }
                                    }
                                }
                                break;
                        }
                    }
                }

                base.CustomValidateTargetList.Add(Utility.GetPropertyName(() => new PersonDetail().MeliCode));
            }
            if (exception.Count > 0)
            {
                throw exception;
            }
        }
        #endregion
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        protected override void DeleteValidate(Person obj)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            BManager bManager = new BManager();
            Person person = this.GetByID(obj.ID);
            if (person.OrganizationUnit != null && person.OrganizationUnit.ID != 0)
                person.OldOrganizationUnitId = person.OrganizationUnit.ID;
            if (bManager.CheckPersonUsedInFlowAsManager(person))
                exception.Add(new ValidationException(ExceptionResourceKeys.PersonUsedInFlowAsManager, "پرسنل یا پست سازمانی منتسب به پرسنل در جریان کاری بعنوان مدیر تعریف شده است", ExceptionSrc));
            if (obj.ID == BUser.CurrentUser.Person.ID)
                exception.Add(new ValidationException(ExceptionResourceKeys.CurrentUserdoseNotAllowedDeleteItsPersonnel, "کاربر جاری مجاز به حذف کردن پرسنل خود نمی باشد.", ExceptionSrc));
            this.NHSession.Evict(person);
            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        /// <summary>
        /// اگر جزئیات شخص تهی باشد آنرا مقداردهی میکنیم
        /// فیلدهایی که نوع زبان در آنها اعمال شده است باید در فیلدهای اصلی اعمال گردد
        /// تاریخ تولد
        /// تاریخ شروع استخدام
        /// تاریخ پایان استخدام
        /// تاریخ شروع اجرای محدوده محاسبات         
        /// </summary>
        /// <param name="person"></param>
        protected override void GetReadyBeforeSave(Person person, UIActionType action)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            //عمل درج نداریم همچنین هنگام عمل حذف کاری نباید انجام گردد
            if (action == UIActionType.EDIT)
            {
                if (person.CardNum != null && person.CardNum != string.Empty)
                    person.CardNum = person.CardNum.PadLeft(8, '0');

                int year = 0;

                if (person.PersonTASpec.LeaveYearMonth <= 0)
                    person.PersonTASpec.LeaveYearMonth = 1;
                if (person.PersonTASpec.LeaveYearDay <= 0)
                    person.PersonTASpec.LeaveYearDay = 1;

                switch (BLanguage.CurrentSystemLanguage)
                {
                    case LanguagesName.Parsi:
                        if (person.PersonDetail.UIBirthDate != null && person.PersonDetail.UIBirthDate != string.Empty)
                            person.PersonDetail.BirthDate = Utility.ToMildiDate(person.PersonDetail.UIBirthDate);
                        if (person.PersonDetail.UIChildBirthDate != null && person.PersonDetail.UIChildBirthDate != string.Empty)
                            person.PersonDetail.ChildBirthDate = Utility.ToMildiDate(person.PersonDetail.UIChildBirthDate);
                        if (person.UIEmploymentDate != null && person.UIEmploymentDate != string.Empty)
                        {
                            person.EmploymentDate = Utility.ToMildiDate(person.UIEmploymentDate);

                            PersianCalendar pCal = new PersianCalendar();
                            year = pCal.GetYear(person.EmploymentDate);
                            if (person.PersonTASpec.LeaveYearDay > pCal.GetDaysInMonth(year, person.PersonTASpec.LeaveYearMonth))
                                person.PersonTASpec.LeaveYearDay = pCal.GetDaysInMonth(year, person.PersonTASpec.LeaveYearMonth);
                            if (pCal.IsLeapYear(year) && person.PersonTASpec.LeaveYearMonth == 12)
                                person.PersonTASpec.LeaveYearDay = person.PersonTASpec.LeaveYearDay - 1;
                        }
                        if (person.UIEndEmploymentDate != null && person.UIEndEmploymentDate != string.Empty)
                            person.EndEmploymentDate = Utility.ToMildiDate(person.UIEndEmploymentDate);
                        break;
                    case LanguagesName.English:
                        if (person.PersonDetail.UIBirthDate != null && person.PersonDetail.UIBirthDate != string.Empty)
                            person.PersonDetail.BirthDate = Utility.ToMildiDateTime(person.PersonDetail.UIBirthDate);

                        if (person.PersonDetail.UIChildBirthDate != null && person.PersonDetail.UIChildBirthDate != string.Empty)
                            person.PersonDetail.ChildBirthDate = Utility.ToMildiDateTime(person.PersonDetail.UIChildBirthDate);
                        if (person.UIEmploymentDate != null && person.UIEmploymentDate != string.Empty)
                            person.EmploymentDate = Utility.ToMildiDateTime(person.UIEmploymentDate);
                        if (person.UIEndEmploymentDate != null && person.UIEndEmploymentDate != string.Empty)
                            person.EndEmploymentDate = Utility.ToMildiDateTime(person.UIEndEmploymentDate);

                        GregorianCalendar gCal = new GregorianCalendar();
                        year = gCal.GetYear(person.EmploymentDate);
                        if (person.PersonTASpec.LeaveYearDay > gCal.GetDaysInMonth(year, person.PersonTASpec.LeaveYearMonth))
                            person.PersonTASpec.LeaveYearDay = gCal.GetDaysInMonth(year, person.PersonTASpec.LeaveYearMonth);
                        if (gCal.IsLeapYear(year) && person.PersonTASpec.LeaveYearMonth == 2)
                            person.PersonTASpec.LeaveYearDay = person.PersonTASpec.LeaveYearDay - 1;
                        break;
                }
                if (person.PersonDetail == null)
                {
                    person.PersonDetail = new PersonDetail() { ID = person.ID };
                }
                else if (person.PersonDetail.ID == 0)
                {
                    person.PersonDetail.ID = person.ID;
                }

                #region Null Properties

                if (person.IsDeleted == null)
                    person.IsDeleted = false;

                if (person.PersonTASpec.ControlStation != null && person.PersonTASpec.ControlStation.ID == 0)
                {
                    person.PersonTASpec.ControlStation = null;
                }

                person.PersonTASpec = this.CheckReservedFieldsForSave(person.PersonTASpec);
                person.PersonCLSpec = this.CheckReservedFieldsForSave(person.PersonCLSpec);

                //person.PersonCLSpec = new PersonCLSpec() { ID = person.ID };
                if (person.PersonCLSpec.ControlStation != null && person.PersonCLSpec.ControlStation.ID == 0)
                {
                    person.PersonCLSpec.ControlStation = null;
                }

                if (person.PersonTASpec.UIValidationGroup != null && person.PersonTASpec.UIValidationGroup.ID == 0)
                {
                    person.PersonTASpec.UIValidationGroup = null;
                }

                if (person.PersonCLSpec.UIValidationGroup != null && person.PersonCLSpec.UIValidationGroup.ID == 0)
                {
                    person.PersonCLSpec.UIValidationGroup = null;
                }

                if (person.PersonCLSpec.DepartmentPosition != null && person.PersonCLSpec.DepartmentPosition.ID == 0)
                {
                    person.PersonCLSpec.DepartmentPosition = null;
                }

                if (person.OrganizationUnit != null && person.OrganizationUnit.ID == 0)
                {
                    person.OrganizationUnit = null;
                }

                if (person.Grade != null && person.Grade.ID == 0)
                {
                    person.Grade = null;
                }
                if (person.CostCenter != null && person.CostCenter.ID == 0)
                {
                    person.CostCenter = null;
                }

                if (person.PersonRangeAssignList != null)
                {
                    List<int> indexes = new List<int>();

                    for (int i = 0; i < person.PersonRangeAssignList.Count; i++)
                    {
                        if (person.PersonRangeAssignList[i].CalcDateRangeGroup != null &&
                                person.PersonRangeAssignList[i].CalcDateRangeGroup.ID == 0)
                        {
                            indexes.Add(i);
                        }
                    }
                    foreach (int index in indexes.OrderByDescending(x => x))
                    {
                        person.PersonRangeAssignList.RemoveAt(index);
                    }
                }
                #endregion

                GTS.Clock.Business.Assignments.BAssignDateRange busRange = new GTS.Clock.Business.Assignments.BAssignDateRange(sysLanguageResource);

                person.BarCode = person.PersonCode.TrimStart('0');

                if (action == UIActionType.EDIT && (!person.Active || person.OldOrganizationUnitId != 0))
                {
                    BManager bManager = new BManager();
                    if (!person.Active)
                    {
                        if (person.OldOrganizationUnitId == 0 && person.OrganizationUnit != null && person.OrganizationUnit.ID != 0)
                            person.OldOrganizationUnitId = person.OrganizationUnit.ID;
                        if (bManager.CheckPersonUsedInFlowAsManager(person))
                            exception.Add(new ValidationException(ExceptionResourceKeys.PersonDeactiveNotAllowedBecauseUsedInFlowAsManager, "غیرفعال کردن پرسنل مجاز نمی باشد بدلیل اینکه پرسنل یا پست سازمانی منتسب به پرسنل در جریان کاری بعنوان مدیر تعریف شده است", ExceptionSrc));
                        if (person.ID == BUser.CurrentUser.Person.ID)
                            exception.Add(new ValidationException(ExceptionResourceKeys.CurrentUserdoseNotAllowedDeactiveItsPersonnel, "کاربر جاری مجاز به غیر فعال کردن پرسنل خود نمی باشد.", ExceptionSrc));
                    }
                    else
                    {
                        if (person.OldOrganizationUnitId != 0 && person.OldOrganizationUnitId != person.OrganizationUnit.ID)
                        {
                            BOrganizationUnit bOrganizationUnit = new BOrganizationUnit();
                            OrganizationUnit org = bOrganizationUnit.GetByID(person.OldOrganizationUnitId);
                            if (bManager.CheckOrganizationUnitUsedInFlowAsManager(org, false))
                                exception.Add(new ValidationException(ExceptionResourceKeys.PersonOrganizationUnitChangeNotAllowedBecauseUsedInFlowAsManager, "تغییر پست سازمانی پرسنل مجاز نمی باشد بدلیل اینکه پرسنل یا پست سازمانی منتسب به پرسنل در جریان کاری بعنوان مدیر تعریف شده است", ExceptionSrc));
                            if (org != null)
                                this.NHSession.Evict(org);
                        }
                    }
                    if (exception.Count > 0)
                    {
                        throw exception;
                    }
                }

                OrganizationUnitRepository organRep = new OrganizationUnitRepository(false);
                organRep.DeleteByPerson(person.ID);

            }
            if (action != UIActionType.DELETE)
            {
                if (person.PersonTASpec == null)
                {
                    person.PersonTASpec = new PersonTASpec() { ID = person.ID };
                }
                else if (person.PersonTASpec.ID == 0)
                {
                    person.PersonTASpec.ID = person.ID;
                }
                if (person.PersonCLSpec == null)
                {
                    person.PersonCLSpec = new PersonCLSpec() { ID = person.ID };
                }
                else if (person.PersonCLSpec.ID == 0)
                {
                    person.PersonCLSpec.ID = person.ID;
                }
            }
            if (action == UIActionType.DELETE)
            {
                Person personAlias = null;
                Person personObj = NHSession.QueryOver(() => personAlias)
                                            .Where(() => personAlias.ID == person.ID)
                                            .List()
                                            .FirstOrDefault();
                person.PersonDetail = new PersonDetail()
                {
                    ID = person.ID,
                    Address = personObj.PersonDetail.Address,
                    BirthCertificate = personObj.PersonDetail.BirthCertificate,
                    BirthDate = personObj.PersonDetail.BirthDate,
                    BirthPlace = personObj.PersonDetail.BirthPlace,
                    ChildBirthDate = personObj.PersonDetail.ChildBirthDate,
                    EmailAddress = personObj.PersonDetail.EmailAddress,
                    FatherName = personObj.PersonDetail.FatherName,
                    Grade = personObj.PersonDetail.Grade,
                    Image = personObj.PersonDetail.Image,
                    MeliCode = personObj.PersonDetail.MeliCode,
                    MilitaryStatus = personObj.PersonDetail.MilitaryStatus,
                    MobileNumber = personObj.PersonDetail.MobileNumber,
                    PlaceIssued = personObj.PersonDetail.PlaceIssued,
                    ShomareShenasname = personObj.PersonDetail.ShomareShenasname,
                    Tel = personObj.PersonDetail.Tel,
                    UIBirthDate = personObj.PersonDetail.UIBirthDate,
                    UIChildBirthDate = personObj.PersonDetail.UIChildBirthDate,
                    RoleID = personObj.User.Role.ID
                };
                this.NHSession.Evict(personObj);
                PersonDetailRep.Update(person.PersonDetail);
            }
        }

        /// <summary>
        /// بعد از حذف شخص باید جزئیات آن هم حذف شود
        /// </summary>
        /// <param name="person"></param>
        /// <param name="action"></param>
        protected override void OnSaveChangesSuccess(Person person, UIActionType action)
        {
            /* if (action == UIActionType.DELETE)
             {
                 EntityRepository<PersonDetail> detailRep = new EntityRepository<PersonDetail>(false);
                 detailRep.Delete(new PersonDetail() { ID = person.ID });
             }*/
            if (action == UIActionType.EDIT)
            {
                if (!person.IsDeleted)
                    this.CreateBasePersonUser(person);
                this.UpdateManagerFlowOnConflistRow(person);
                this.UpdateUnderManagment(person);
                //BManager bManager = new BManager();
                //bManager.UpdateManager_OnAfterPersonUpdate(person);
            }
            this.UpdateUnderManagmentPersons(person, action);
            this.UpdateRequestSubstitute(person, action);
            this.CreateBasePersonContractAssignment(person, action);
        }

        private void CreateBasePersonContractAssignment(Person person, UIActionType action)
        {
            try
            {
                if (action == UIActionType.EDIT && person.EmploymentDate != null && person.EmploymentDate > Utility.GTSMinStandardDateTime)
                {
                    BPersonContractAssignment bPersonContractAssignment = new BPersonContractAssignment();
                    if (!bPersonContractAssignment.ExsitsForPerson(person.ID))
                    {
                        BContract bContract = new BContract();
                        Contract contract = bContract.GetDefaultContract();
                        if (contract != null)
                        {
                            PersonContractAssignment personContractAssignment = new PersonContractAssignment()
                            {
                                IsDeleted = false,
                                Contract = contract,
                                Person = person,
                                UIFromDate = person.UIEmploymentDate,
                                UIToDate = string.Empty
                            };
                            bPersonContractAssignment.SaveChanges(personContractAssignment, UIActionType.ADD);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogException(ex, "BPerson", "CreateBasePersonContractAssignment");
                throw ex;
            }
        }
        private void UpdateUnderManagmentPersons(Person person, UIActionType action)
        {
            UnderManagmentRepository underMngRep = new UnderManagmentRepository(false);
            ActionType actionType = (ActionType)Enum.Parse(typeof(ActionType), action.ToString());
            if (action == UIActionType.EDIT)
            {
                if (person.OldDepartmentId != 0)
                {
                    if (person.OldDepartmentId != person.Department.ID)
                    {
                        underMngRep.UpdateUnderManagementPersons(person.ID, person.Department.ID, person.OldDepartmentId, actionType);
                    }
                }
                else
                {
                    underMngRep.UpdateUnderManagementPersons(person.ID, person.Department.ID, 0, actionType);
                }
            }
            if (action == UIActionType.DELETE)
            {
                underMngRep.UpdateUnderManagementPersons(person.ID, person.Department.ID, 0, actionType);
            }

        }
        private void UpdateRequestSubstitute(Person person, UIActionType action)
        {
            try
            {
                BRequestSubstitute rSubstitute = new BRequestSubstitute();
                if (action == UIActionType.EDIT)
                {
                    if (person.OldActive == true && person.Active == false)
                    {
                        rSubstitute.UpdateRequestSubstituteAccordingToInActiveOrIsDeletedPerson(person.ID);
                    }
                    else
                    {
                        if ((person.OldDepartmentId != 0 && person.OldDepartmentId != person.Department.ID) || (person.OldGradId != 0 && person.Grade.ID != person.OldGradId) || (person.OldCostCenterId != 0 && person.CostCenter.ID != person.OldCostCenterId))
                        {
                            rSubstitute.UpdateRequestSubstituteAccordingToDepartmentOrGradChange(person);
                        }
                    }
                    NHSession.Evict(person);
                }
                if (action == UIActionType.DELETE)
                {
                    rSubstitute.UpdateRequestSubstituteAccordingToInActiveOrIsDeletedPerson(person.ID);
                }
            }
            catch (Exception ex)
            {
                LogException(ex, "BPerson", "UpdateRequestSubstitute");
                throw ex;
            }
        }
        private void UpdateUnderManagment(Person person)
        {
            try
            {
                if (person.Department != null && person.OldDepartmentId != 0 && person.Department.ID != person.OldDepartmentId)
                {
                    string SQLCommand = @"UPDATE dbo.TA_UnderManagment
                                          SET underMng_DepartmentID =:DepartmentId  WHERE  underMng_PersonID =:PersonId AND  underMng_DepartmentID =:OldDepartmentId";
                    IQuery query = NHibernateSessionManager.Instance.GetSession().CreateSQLQuery(SQLCommand);
                    query.SetParameter("DepartmentId", person.Department.ID);
                    query.SetParameter("PersonId", person.ID);
                    query.SetParameter("OldDepartmentId", person.OldDepartmentId);
                    query.ExecuteUpdate();
                }
            }
            catch (Exception ex)
            {
                LogException(ex, "BPerson", "UpdateUnderManagment");
                throw ex;
            }
        }
        private void UpdateManagerFlowOnConflistRow(Person person)
        {
            try
            {
                if ((person.OldOrganizationUnitId == 0 && person.OrganizationUnit != null) || (person.OrganizationUnit != null && person.OldOrganizationUnitId != 0 && person.OrganizationUnit.ID != person.OldOrganizationUnitId))
                {
                    Manager ManagerOrganization = NHSession.QueryOver<Manager>()
                                                           .Where(x => x.OrganizationUnit.ID == person.OrganizationUnit.ID &&
                                                                       x.Active
                                                                 )
                                                           .SingleOrDefault();
                    if (ManagerOrganization != null)
                    {
                        Manager ManagerPerson = NHSession.QueryOver<Manager>()
                                                            .Where(x => x.Active && x.Person.ID == person.ID)
                                                            .SingleOrDefault();
                        if (ManagerPerson != null)
                        {
                            string SQLCommand = @"UPDATE dbo.TA_ManagerFlow
                                            SET mngrFlow_ManagerID =:ManagerOrganizationID  WHERE mngrFlow_ManagerID =:ManagerPersonID";
                            IQuery query = NHibernateSessionManager.Instance.GetSession().CreateSQLQuery(SQLCommand);
                            query.SetParameter("ManagerOrganizationID", ManagerOrganization.ID);
                            query.SetParameter("ManagerPersonID", ManagerPerson.ID);
                            query.ExecuteUpdate();

                            ManagerPerson.Active = false;
                            BManager bManager = new BManager();
                            bManager.SaveChanges(ManagerPerson, UIActionType.EDIT);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogException(ex, "Bperson", "UpdateManagerOnConflistRow");
                throw ex;
            }
        }

        private void CreateBasePersonUser(Person person)
        {
            BUser userBusiness = new BUser();
            userBusiness.CreateBasePersonUser(person);
        }

        /// <summary>
        /// یک شخص را برمیگرداند و زبان را برروی آن اعمال میکند
        /// </summary>
        /// <param name="objID"></param>
        /// <returns></returns>
        public override Person GetByID(decimal personId)
        {
            try
            {
                Person person = base.GetByID(personId);
                
                IList<Person> list = new List<Person>();
                list.Add(person);
                list = ApplyCultureAndFixCurrentObject(list, sysLanguageResource, localLanguageResource);
                return list[0];
            }
            catch (Exception ex)
            {
                LogException(ex, "BPerson", "GetByID");
                throw ex;
            }
        }

        protected override void UpdateCFP(Person obj, UIActionType action)
        {
            if (action == UIActionType.ADD)
            {
                base.UpdateCFP(obj.ID, DateTime.Now);
            }
            else if (action == UIActionType.EDIT)
            {
                DateTime assgnDate = new DateTime();
                BAssignRule assgnRleBus = new BAssignRule(LanguagesName.English);
                IList<PersonRuleCatAssignment> list = assgnRleBus.GetAll(obj.ID);
                if (list != null && list.Count > 0)
                {
                    PersonRuleCatAssignment assign = list.Where(x => Convert.ToDateTime(x.FromDate) <= DateTime.Now.Date && DateTime.Now.Date <= Convert.ToDateTime(x.ToDate))
                                                             .OrderBy(x => x.FromDate).LastOrDefault();
                    if (assign != null && assign.RuleCategory != null)
                    {
                        assgnDate = Utility.ToMildiDateTime(assign.FromDate);
                    }
                    else
                    {
                        assgnDate = Utility.ToMildiDateTime(list.OrderBy(x => x.FromDate).Last().FromDate);
                    }
                    decimal personId = obj.ID;
                    DateTime newCfpDate = assgnDate;
                    //CFP cfp = base.GetCFP(personId);
                    //if (cfp.ID == 0 || cfp.Date > newCfpDate)
                    //{
                    //    DateTime calculationLockDate = base.UIValidator.GetCalculationLockDate(personId);

                    //بسته بودن محاسبات 
                    //if (calculationLockDate > Utility.GTSMinStandardDateTime && calculationLockDate > newCfpDate)
                    //{
                    //    newCfpDate = calculationLockDate.AddDays(1);
                    //}

                    base.UpdateCFP(personId, newCfpDate);
                    // }
                }
            }
        }

        /// <summary>
        /// حذف منطقی
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected override bool Delete(Person obj)
        {
            try
            {
                personRepository.DeletePerson(obj.ID);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Other Services
        public Person GetByUsername(string username)
        {
            try
            {
                GTS.Clock.Business.Security.BUser bUser = new BUser();
                GTS.Clock.Model.Security.User user = bUser.GetByUsername(username);
                if (user != null && user.ID != 0)
                {
                    return user.Person;
                }
                throw new ItemNotExists(String.Format("شخصی با شناسه کاربری {0} یافت شند", username), ExceptionSrc);
            }
            catch (Exception ex)
            {
                LogException(ex, "BPerson", "GetByUsername");
                throw ex;
            }
        }
        public Person GetByBarcode(string barcode)
        {
            try
            {
                ISession NHSession = NHibernateSessionManager.Instance.GetSession();
                Person personAlias = null;
                Person personObj = null;
                personObj = NHSession.QueryOver<Person>(() => personAlias)
                                     .Where(() => !personAlias.IsDeleted &&
                                                   personAlias.Active &&
                                                   personAlias.BarCode == barcode
                                           )
                                     .SingleOrDefault();
                if (personObj == null || personObj.ID == 0)
                {
                    IList<Person> personList = NHSession.QueryOver<Person>(() => personAlias).Where(() => !personAlias.IsDeleted &&
                                                   personAlias.Active &&
                                                   personAlias.BarCode.IsInsensitiveLike(barcode, MatchMode.Anywhere)).List();
                    foreach (Person item in personList)
                    {
                        int leftPadPersonBarcodeLength = item.BarCode.Length - barcode.Length;
                        string leftPadPersonBarcode = item.BarCode.Substring(0, leftPadPersonBarcodeLength);
                        bool leftPadIsNumberZero = true;
                        foreach (char chararcter in leftPadPersonBarcode)
                        {
                            if (chararcter != '0')
                                leftPadIsNumberZero = false;
                        }
                        if (leftPadIsNumberZero)
                        {
                            personObj = item;
                            break;
                        }
                    }
                }

                return personObj;
                //throw new ItemNotExists(String.Format("شخصی با بارکد {0} یافت نشد", barcode), ExceptionSrc);
            }
            catch (Exception ex)
            {
                LogException(ex, "BPerson", "GetByBarcode");
                throw ex;
            }
        }

        #endregion

        #region ISearchTool

        /// <summary>
        /// تعداد پرسنل شامل فعال و غیر فعال باتوجه به کلمه کلیدی جستجو را برمیگرداند 
        /// اگر شخص دسترسی عمومی داشته باشد طبق آن به پرسنل دسترسی دارد
        /// در غیر این صورت بسته به این که مدیر - جانشین یا اپراتور باشد جستجو در بین
        /// پرسنل تحت مدیریت انجام میگیرد
        /// </summary>
        /// <returns></returns>
        int ISearchPerson.GetPersonInQuickSearchCount(string searchValue)
        {
            int count = 0;
            //DNN Note:improve performance
            //ISearchPerson searchTool = new BPerson();

            IDataAccess port = new BUser();
            int accessibleCount = port.GetAccessibleDeparments().Count;
            if (accessibleCount > 0)
            {
                count = (this as ISearchPerson).GetPersonInQuickSearchCount(searchValue, PersonCategory.Public);
            }
            else
            {
                accessibleCount = port.GetAccessibleEmploymentTypes().Count;
                if (accessibleCount > 0)
                {
                    count = (this as ISearchPerson).GetPersonInQuickSearchCount(searchValue, PersonCategory.Public);
                }
                else
                {
                    PersonCategory category = this.GetCurrentPersonCategory();
                    if (category != PersonCategory.Public)
                    {
                        count = (this as ISearchPerson).GetPersonInQuickSearchCount(searchValue, category);
                    }
                }
            }
            return count;
        }
        int ISearchPerson.GetPersonInQuickSearchCountByMonthlyExceptionShift(string searchValue, IList<DateTime> MonthDateList)
        {
            try
            {
                int count = 0;
                //DNN Note:improve performance
                //ISearchPerson searchTool = new BPerson();
                IDataAccess port = new BUser();
                int accessibleCount = port.GetAccessibleDeparments().Count;
                IList<DateTime> MonthDatesList = new List<DateTime>();
                if (accessibleCount > 0)
                {
                    count = (this as ISearchPerson).GetPersonInQuickSearchCountByMonthlyExceptionShift(searchValue, PersonCategory.Public, MonthDateList);
                }
                else
                {
                    accessibleCount = port.GetAccessibleEmploymentTypes().Count;
                    if (accessibleCount > 0)
                    {
                        count = (this as ISearchPerson).GetPersonInQuickSearchCountByMonthlyExceptionShift(searchValue, PersonCategory.Public, MonthDateList);
                    }
                    else
                    {
                        PersonCategory category = this.GetCurrentPersonCategory();
                        if (category != PersonCategory.Public)
                        {
                            count = (this as ISearchPerson).GetPersonInQuickSearchCountByMonthlyExceptionShift(searchValue, category, MonthDateList);
                        }
                    }
                }
                return count;
            }
            catch (Exception ex)
            {
                LogException(ex, "BPerson", "GetPersonInQuickSearchCountByMonthlyExceptionShift");
                throw ex;
            }
        }

        /// <summary>
        /// تعداد پرسنل شامل فعال و غیر فعال باتوجه به کلمه کلیدی جستجو را برمیگرداند 
        /// </summary>
        /// <returns></returns>
        int ISearchPerson.GetPersonInQuickSearchCount(string searchValue, PersonCategory searchInCategory)
        {
            try
            {
                BApplicationSettings.CheckGTSLicense();

                IList<Person> personList = new List<Person>();

                searchValue = searchValue == null ? String.Empty : searchValue;
                searchValue = searchValue.Trim();
                decimal managerId = this.GetCurentManagerId(ref searchInCategory);
                int count = personRepository.SearchCount(searchValue, BUser.CurrentUser.ID, managerId, searchInCategory);
                return count;
            }
            catch (Exception ex)
            {
                LogException(ex, "BPerson", "GetPersonInQuickSearchCount");
                throw ex;
            }
        }
        int ISearchPerson.GetPersonInQuickSearchCountBySubstitute(string searchValue, PersonCategory searchInCategory, decimal personId)
        {
            try
            {
                BApplicationSettings.CheckGTSLicense();
                decimal PersonId = 0;
                if (searchInCategory == PersonCategory.Public)
                {
                    PersonId = this.NHSession.QueryOver<User>()
                                             .Select(x => x.Person.ID)
                                             .Where(x => x.ID == BUser.CurrentUser.ID)
                                             .List<decimal>().SingleOrDefault();

                }
                else
                    PersonId = personId;
                IList<Person> personList = new List<Person>();

                searchValue = searchValue == null ? String.Empty : searchValue;
                searchValue = searchValue.Trim();
                decimal managerId = this.GetCurentManagerId(ref searchInCategory);
                int count = personRepository.SearchCountBySubstitute(personId, searchValue, BUser.CurrentUser.ID, managerId, searchInCategory);
                return count;
            }
            catch (Exception ex)
            {
                LogException(ex, "BPerson", "GetPersonInQuickSearchCountBySubstitute");
                throw ex;
            }
        }
        int ISearchPerson.GetPersonInQuickSearchCountByMonthlyExceptionShift(string searchValue, PersonCategory searchInCategory, IList<DateTime> MonthDatesList)
        {
            try
            {
                BApplicationSettings.CheckGTSLicense();

                IList<Person> personList = new List<Person>();

                searchValue = searchValue == null ? String.Empty : searchValue;
                searchValue = searchValue.Trim();
                decimal managerId = this.GetCurentManagerId(ref searchInCategory);
                int count = personRepository.SearchCountByMonthlyExceptionShift(searchValue, BUser.CurrentUser.ID, managerId, searchInCategory, MonthDatesList);
                return count;
            }
            catch (Exception ex)
            {
                LogException(ex, "BPerson", "GetPersonInQuickSearchCountByMonthlyExceptionShift");
                throw ex;
            }
        }

        int ISearchPerson.GetPersonInQuickIntegratedSearchCount(string searchValue, string integratedSearchValue, PersonCategory searchInCategory)
        {
            try
            {
                BApplicationSettings.CheckGTSLicense();

                IList<Person> personList = new List<Person>();

                searchValue = searchValue == null ? String.Empty : searchValue;
                searchValue = searchValue.Trim();
                decimal managerId = this.GetCurentManagerId(ref searchInCategory);
                int count = personRepository.IntegratedSearchCount(searchValue, integratedSearchValue, BUser.CurrentUser.ID, managerId, searchInCategory);
                return count;
            }
            catch (Exception ex)
            {
                LogException(ex, "BPerson", "GetPersonInQuickIntegratedSearchCount");
                throw ex;
            }
        }


        int ISearchPerson.GetPersonInQuickSearchCountByOperator(string searchValue)
        {
            try
            {
                BApplicationSettings.CheckGTSLicense();

                IList<Person> personList = new List<Person>();

                searchValue = searchValue == null ? String.Empty : searchValue;
                searchValue = searchValue.Trim();
                int count = personRepository.SearchCountByOperator(searchValue, BUser.CurrentUser.ID);
                return count;
            }
            catch (Exception ex)
            {
                LogException(ex, "BPerson", "GetPersonInQuickSearchCountByOperator");
                throw ex;
            }
        }


        /// <summary>
        /// اگر شخص دسترسی عمومی داشته باشد طبق آن به پرسنل دسترسی دارد
        /// در غیر این صورت بسته به این که مدیر - جانشین یا اپراتور باشد جستجو در بین
        /// پرسنل تحت مدیریت انجام میگیرد
        /// </summary>
        /// <param name="proxy"></param>
        /// <returns></returns>
        int ISearchPerson.GetPersonInAdvanceSearchCount(PersonAdvanceSearchProxy proxy)
        {
            try
            {
                int count = 0;
                //DNN Note:Improve performance
                //ISearchPerson searchTool = new BPerson();
                IDataAccess port = new BUser();
                int accessibleCount = port.GetAccessibleDeparments().Count;
                if (accessibleCount > 0)
                {
                    count = (this as ISearchPerson).GetPersonInAdvanceSearchCount(proxy, PersonCategory.Public);
                }
                else
                {
                    accessibleCount = port.GetAccessibleEmploymentTypes().Count;
                    if (accessibleCount > 0)
                    {
                        count = (this as ISearchPerson).GetPersonInAdvanceSearchCount(proxy, PersonCategory.Public);
                    }
                    else
                    {
                        PersonCategory category = this.GetCurrentPersonCategory();
                        if (category != PersonCategory.Public)
                        {
                            count = (this as ISearchPerson).GetPersonInAdvanceSearchCount(proxy, category);
                        }
                    }
                }
                return count;
            }
            catch (Exception ex)
            {
                LogException(ex, "BPerson", "GetPersonInAdvanceSearchCount");
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="searchInCategory"></param>
        /// <returns></returns>
        int ISearchPerson.GetPersonInAdvanceSearchCount(PersonAdvanceSearchProxy proxy, PersonCategory searchInCategory)
        {
            try
            {
                BApplicationSettings.CheckGTSLicense();

                if (proxy.PersonIdList != null && proxy.PersonIdList.Count > 0)
                {
                    return proxy.PersonIdList.Count;
                }

                #region Date Convert
                if (!Utility.IsEmpty(proxy.FromBirthDate) && BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    proxy.FromBirthDate = Utility.ToMildiDateString(proxy.FromBirthDate);
                }
                if (!Utility.IsEmpty(proxy.ToBirthDate) && BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    proxy.ToBirthDate = Utility.ToMildiDateString(proxy.ToBirthDate);
                }
                if (!Utility.IsEmpty(proxy.FromEmploymentDate) && BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    proxy.FromEmploymentDate = Utility.ToMildiDateString(proxy.FromEmploymentDate);
                }
                if (!Utility.IsEmpty(proxy.ToEmploymentDate) && BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    proxy.ToEmploymentDate = Utility.ToMildiDateString(proxy.ToEmploymentDate);
                }
                if (!Utility.IsEmpty(proxy.WorkGroupFromDate) && BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    proxy.WorkGroupFromDate = Utility.ToMildiDateString(proxy.WorkGroupFromDate);
                }
                if (!Utility.IsEmpty(proxy.RuleGroupFromDate) && BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    proxy.RuleGroupFromDate = Utility.ToMildiDateString(proxy.RuleGroupFromDate);
                }
                if (!Utility.IsEmpty(proxy.RuleGroupToDate) && BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    proxy.RuleGroupToDate = Utility.ToMildiDateString(proxy.RuleGroupToDate);
                }
                if (!Utility.IsEmpty(proxy.CalculationFromDate) && BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    proxy.CalculationFromDate = Utility.ToMildiDateString(proxy.CalculationFromDate);
                }
                if (!Utility.IsEmpty(proxy.ContractFromDate) && BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    proxy.ContractFromDate = Utility.ToMildiDateString(proxy.ContractFromDate);
                }
                if (!Utility.IsEmpty(proxy.ContractToDate) && BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    proxy.ContractToDate = Utility.ToMildiDateString(proxy.ContractToDate);
                }
                #endregion

                decimal managerId = this.GetCurentManagerId(ref searchInCategory);
                int count = personRepository.SearchCount(proxy, BUser.CurrentUser.ID, managerId, searchInCategory);
                return count;
            }
            catch (Exception ex)
            {
                LogException(ex, "BPerson", "GetPersonInAdvanceSearchCount");
                throw ex;
            }
        }

        int ISearchPerson.GetPersonInAdvanceSearchCountByOperator(PersonAdvanceSearchProxy proxy)
        {
            try
            {
                BApplicationSettings.CheckGTSLicense();

                if (proxy.PersonIdList != null && proxy.PersonIdList.Count > 0)
                {
                    return proxy.PersonIdList.Count;
                }

                #region Date Convert
                if (!Utility.IsEmpty(proxy.FromBirthDate) && BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    proxy.FromBirthDate = Utility.ToMildiDateString(proxy.FromBirthDate);
                }
                if (!Utility.IsEmpty(proxy.ToBirthDate) && BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    proxy.ToBirthDate = Utility.ToMildiDateString(proxy.ToBirthDate);
                }
                if (!Utility.IsEmpty(proxy.FromEmploymentDate) && BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    proxy.FromEmploymentDate = Utility.ToMildiDateString(proxy.FromEmploymentDate);
                }
                if (!Utility.IsEmpty(proxy.ToEmploymentDate) && BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    proxy.ToEmploymentDate = Utility.ToMildiDateString(proxy.ToEmploymentDate);
                }
                if (!Utility.IsEmpty(proxy.WorkGroupFromDate) && BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    proxy.WorkGroupFromDate = Utility.ToMildiDateString(proxy.WorkGroupFromDate);
                }
                if (!Utility.IsEmpty(proxy.RuleGroupFromDate) && BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    proxy.RuleGroupFromDate = Utility.ToMildiDateString(proxy.RuleGroupFromDate);
                }
                if (!Utility.IsEmpty(proxy.RuleGroupToDate) && BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    proxy.RuleGroupToDate = Utility.ToMildiDateString(proxy.RuleGroupToDate);
                }
                if (!Utility.IsEmpty(proxy.CalculationFromDate) && BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    proxy.CalculationFromDate = Utility.ToMildiDateString(proxy.CalculationFromDate);
                }
                #endregion

                int count = personRepository.SearchCountByOperator(proxy, BUser.CurrentUser.ID);
                return count;
            }
            catch (Exception ex)
            {
                LogException(ex, "BPerson", "GetPersonInAdvanceSearchCountByOperator");
                throw ex;
            }
        }
         
        int ISearchPerson.GetPersonCount()
        {
            //DNN Note:Improve Performance
            //ISearchPerson p = new BPerson();
            return (this as ISearchPerson).GetPersonInQuickSearchCount("");
        }
        int ISearchPerson.GetPersonCountByMonthlyExceptionShift(int year, int month, string searchValue)
        {
            //DNN Note:Improve Performance
            //ISearchPerson p = new BPerson();
            BExceptionShift b = new BExceptionShift();
            IList<DateTime> MonthDateList = b.GetMonthDatesList(year, month);
            return (this as ISearchPerson).GetPersonInQuickSearchCountByMonthlyExceptionShift(searchValue, MonthDateList);
        }
        /// <summary>
        /// جهت استفاده در جستجوی گزارشات
        /// </summary>
        /// <param name="proxy"></param>
        /// <returns></returns>
        IList<Person> ISearchPerson.GetPersonInAdvanceSearch(Business.Proxy.PersonAdvanceSearchProxy proxy)
        {
            try
            {
                //DNN Note:Improve Performance
                //ISearchPerson searchTool = new BPerson();
                IList<Person> list = new List<Person>();

                Business.Proxy.PersonAdvanceSearchProxy proxyObj = (Business.Proxy.PersonAdvanceSearchProxy)proxy.Clone();

                int count = 0;
                IDataAccess port = new BUser();
                int accessibleCount = port.GetAccessibleDeparments().Count;
                if (accessibleCount > 0)
                {
                    count = (this as ISearchPerson).GetPersonInAdvanceSearchCount(proxy, PersonCategory.Public);
                    list = (this as ISearchPerson).GetPersonInAdvanceSearch(proxyObj, 0, count, PersonCategory.Public);
                }
                else
                {
                    accessibleCount = port.GetAccessibleEmploymentTypes().Count;
                    if (accessibleCount > 0)
                    {
                        count = (this as ISearchPerson).GetPersonInAdvanceSearchCount(proxy, PersonCategory.Public);
                        list = (this as ISearchPerson).GetPersonInAdvanceSearch(proxyObj, 0, count, PersonCategory.Public);
                    }
                    else
                    {
                        PersonCategory category = this.GetCurrentPersonCategory();
                        if (category != PersonCategory.Public)
                        {
                            count = (this as ISearchPerson).GetPersonInAdvanceSearchCount(proxy, category);
                            list = (this as ISearchPerson).GetPersonInAdvanceSearch(proxyObj, 0, count, category);
                        }
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                LogException(ex, "BPerson", "GetPersonInAdvanceSearch");
                throw ex;
            }
        }

        /// <summary>
        /// جستجوی پیشرفته
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IList<Person> ISearchPerson.GetPersonInAdvanceSearch(Business.Proxy.PersonAdvanceSearchProxy proxy, int pageIndex, int pageSize)
        {
            try
            {
                //DNN Note:Improve Performance
                //ISearchPerson searchTool = new BPerson();
                IList<Person> list = new List<Person>();

                IDataAccess port = new BUser();
                int accessibleCount = port.GetAccessibleDeparments().Count;
                if (accessibleCount > 0)
                {
                    list = (this as ISearchPerson).GetPersonInAdvanceSearch(proxy, pageIndex, pageSize, PersonCategory.Public);
                }
                else
                {
                    accessibleCount = port.GetAccessibleEmploymentTypes().Count;
                    if (accessibleCount > 0)
                    {
                        list = (this as ISearchPerson).GetPersonInAdvanceSearch(proxy, pageIndex, pageSize, PersonCategory.Public);
                    }
                    else
                    {
                        PersonCategory category = this.GetCurrentPersonCategory();
                        if (category != PersonCategory.Public)
                        {
                            list = (this as ISearchPerson).GetPersonInAdvanceSearch(proxy, pageIndex, pageSize, category);
                        }
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                LogException(ex, "BPerson", "GetPersonInAdvanceSearch");
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchInCategory"></param>
        /// <returns></returns>
        IList<Person> ISearchPerson.GetPersonInAdvanceSearch(Business.Proxy.PersonAdvanceSearchProxy proxy, int pageIndex, int pageSize, PersonCategory searchInCategory)
        {

            try
            {
                BApplicationSettings.CheckGTSLicense();

                #region Date Convert
                if (!Utility.IsEmpty(proxy.FromBirthDate) && BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    proxy.FromBirthDate = Utility.ToMildiDateString(proxy.FromBirthDate);
                }
                if (!Utility.IsEmpty(proxy.ToBirthDate) && BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    proxy.ToBirthDate = Utility.ToMildiDateString(proxy.ToBirthDate);
                }
                if (!Utility.IsEmpty(proxy.FromEmploymentDate) && BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    proxy.FromEmploymentDate = Utility.ToMildiDateString(proxy.FromEmploymentDate);
                }
                if (!Utility.IsEmpty(proxy.ToEmploymentDate) && BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    proxy.ToEmploymentDate = Utility.ToMildiDateString(proxy.ToEmploymentDate);
                }
                if (!Utility.IsEmpty(proxy.WorkGroupFromDate) && BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    proxy.WorkGroupFromDate = Utility.ToMildiDateString(proxy.WorkGroupFromDate);
                }
                if (!Utility.IsEmpty(proxy.RuleGroupFromDate) && BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    proxy.RuleGroupFromDate = Utility.ToMildiDateString(proxy.RuleGroupFromDate);
                }
                if (!Utility.IsEmpty(proxy.RuleGroupToDate) && BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    proxy.RuleGroupToDate = Utility.ToMildiDateString(proxy.RuleGroupToDate);
                }
                if (!Utility.IsEmpty(proxy.CalculationFromDate) && BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    proxy.CalculationFromDate = Utility.ToMildiDateString(proxy.CalculationFromDate);
                }
                if (!Utility.IsEmpty(proxy.ContractFromDate) && BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    proxy.ContractFromDate = Utility.ToMildiDateString(proxy.ContractFromDate);
                }
                if (!Utility.IsEmpty(proxy.ContractToDate) && BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    proxy.ContractToDate = Utility.ToMildiDateString(proxy.ContractToDate);
                }
                #endregion

                IList<Person> list;
                if (!Utility.IsEmpty(proxy.PersonId))
                {
                    list = new List<Person>();
                    Person prs = base.GetByID((decimal)proxy.PersonId);
                    list.Add(prs);
                }
                else
                {
                    decimal managerId = this.GetCurentManagerId(ref searchInCategory);
                    list = personRepository.Search(proxy, BUser.CurrentUser.ID, managerId, searchInCategory, pageIndex, pageSize);
                }

                return list;
            }
            catch (Exception ex)
            {
                LogException(ex, "BPerson", "GetPersonInAdvanceSearch");
                throw ex;
            }
        }
        //IList<Person> ISearchPerson.GetPersonInAdvanceSearchBySubstitute(Business.Proxy.PersonAdvanceSearchProxy proxy, int pageIndex, int pageSize, PersonCategory searchInCategory , int PersonId)
        //{

        //    try
        //    {



        //        BApplicationSettings.CheckGTSLicense();

        //        #region Date Convert
        //        if (!Utility.IsEmpty(proxy.FromBirthDate) && BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
        //        {
        //            proxy.FromBirthDate = Utility.ToMildiDateString(proxy.FromBirthDate);
        //        }
        //        if (!Utility.IsEmpty(proxy.ToBirthDate) && BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
        //        {
        //            proxy.ToBirthDate = Utility.ToMildiDateString(proxy.ToBirthDate);
        //        }
        //        if (!Utility.IsEmpty(proxy.FromEmploymentDate) && BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
        //        {
        //            proxy.FromEmploymentDate = Utility.ToMildiDateString(proxy.FromEmploymentDate);
        //        }
        //        if (!Utility.IsEmpty(proxy.ToEmploymentDate) && BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
        //        {
        //            proxy.ToEmploymentDate = Utility.ToMildiDateString(proxy.ToEmploymentDate);
        //        }
        //        if (!Utility.IsEmpty(proxy.WorkGroupFromDate) && BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
        //        {
        //            proxy.WorkGroupFromDate = Utility.ToMildiDateString(proxy.WorkGroupFromDate);
        //        }
        //        if (!Utility.IsEmpty(proxy.RuleGroupFromDate) && BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
        //        {
        //            proxy.RuleGroupFromDate = Utility.ToMildiDateString(proxy.RuleGroupFromDate);
        //        }
        //        if (!Utility.IsEmpty(proxy.RuleGroupToDate) && BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
        //        {
        //            proxy.RuleGroupToDate = Utility.ToMildiDateString(proxy.RuleGroupToDate);
        //        }
        //        if (!Utility.IsEmpty(proxy.CalculationFromDate) && BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
        //        {
        //            proxy.CalculationFromDate = Utility.ToMildiDateString(proxy.CalculationFromDate);
        //        }
        //        #endregion

        //        IList<Person> list;
        //        if (!Utility.IsEmpty(proxy.PersonId))
        //        {
        //            list = new List<Person>();
        //            Person prs = base.GetByID((decimal)proxy.PersonId);
        //            list.Add(prs);
        //        }
        //        else
        //        {
        //            decimal managerId = this.GetCurentManagerId(ref searchInCategory);
        //            list = personRepository.Search(proxy, BUser.CurrentUser.ID, managerId, searchInCategory, pageIndex, pageSize);
        //        }

        //        return list;
        //    }
        //    catch (Exception ex)
        //    {
        //        LogException(ex, "BPerson", "GetPersonInAdvanceSearch");
        //        throw ex;
        //    }
        //}
        IList<Person> ISearchPerson.GetPersonInAdvanceSearchByOperator(Business.Proxy.PersonAdvanceSearchProxy proxy, int pageIndex, int pageSize, out int count)
        {
            try
            {
                BApplicationSettings.CheckGTSLicense();

                count = 0;

                #region Date Convert
                if (!Utility.IsEmpty(proxy.FromBirthDate) && BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    proxy.FromBirthDate = Utility.ToMildiDateString(proxy.FromBirthDate);
                }
                if (!Utility.IsEmpty(proxy.ToBirthDate) && BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    proxy.ToBirthDate = Utility.ToMildiDateString(proxy.ToBirthDate);
                }
                if (!Utility.IsEmpty(proxy.FromEmploymentDate) && BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    proxy.FromEmploymentDate = Utility.ToMildiDateString(proxy.FromEmploymentDate);
                }
                if (!Utility.IsEmpty(proxy.ToEmploymentDate) && BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    proxy.ToEmploymentDate = Utility.ToMildiDateString(proxy.ToEmploymentDate);
                }
                if (!Utility.IsEmpty(proxy.WorkGroupFromDate) && BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    proxy.WorkGroupFromDate = Utility.ToMildiDateString(proxy.WorkGroupFromDate);
                }
                if (!Utility.IsEmpty(proxy.RuleGroupFromDate) && BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    proxy.RuleGroupFromDate = Utility.ToMildiDateString(proxy.RuleGroupFromDate);
                }
                if (!Utility.IsEmpty(proxy.RuleGroupToDate) && BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    proxy.RuleGroupToDate = Utility.ToMildiDateString(proxy.RuleGroupToDate);
                }
                if (!Utility.IsEmpty(proxy.CalculationFromDate) && BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    proxy.CalculationFromDate = Utility.ToMildiDateString(proxy.CalculationFromDate);
                }
                #endregion

                IList<Person> list;
                if (!Utility.IsEmpty(proxy.PersonId))
                {
                    list = new List<Person>();
                    Person prs = base.GetByID((decimal)proxy.PersonId);
                    list.Add(prs);
                }
                else
                {
                    list = personRepository.SearchByOperator(proxy, BUser.CurrentUser.ID, pageIndex, pageSize, out count);
                }
                return list;
            }
            catch (Exception ex)
            {
                LogException(ex, "BPerson", "GetPersonInAdvanceSearchByOperator");
                throw ex;
            }
        }

        /// <summary>
        /// جستجوی پیشرفته
        /// جهت استفاده در فرم پرسنل
        /// در این سرویس دسترسی مدیر یا اپراتور اعمال نمیشود
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IList<Person> ISearchPerson.GetPersonInAdvanceSearchApplyCulture(Business.Proxy.PersonAdvanceSearchProxy proxy, int pageIndex, int pageSize)
        {
            try
            {
                //list = searchTool.GetPersonInAdvanceSearch(proxy, pageIndex, pageSize, PersonCategory.Public);
                //return ApplyCultureAndFixCurrentObject(list, this.sysLanguageResource, this.localLanguageResource);

                //DNN Note:Improve Perfromance
                //ISearchPerson searchTool = new BPerson();
                IList<Person> list = new List<Person>();

                IDataAccess port = new BUser();
                int accessibleCount = port.GetAccessibleDeparments().Count;
                if (accessibleCount > 0)
                {
                    list = (this as ISearchPerson).GetPersonInAdvanceSearch(proxy, pageIndex, pageSize, PersonCategory.Public);
                    list = ApplyCultureAndFixCurrentObject(list, this.sysLanguageResource, this.localLanguageResource);
                }
                else
                {
                    accessibleCount = port.GetAccessibleEmploymentTypes().Count;
                    if (accessibleCount > 0)
                    {
                        list = (this as ISearchPerson).GetPersonInAdvanceSearch(proxy, pageIndex, pageSize, PersonCategory.Public);
                        list = ApplyCultureAndFixCurrentObject(list, this.sysLanguageResource, this.localLanguageResource);
                    }
                    else
                    {
                        PersonCategory category = this.GetCurrentPersonCategory();
                        if (category != PersonCategory.Public)
                        {
                            list = (this as ISearchPerson).GetPersonInAdvanceSearch(proxy, pageIndex, pageSize, category);
                            list = ApplyCultureAndFixCurrentObject(list, this.sysLanguageResource, this.localLanguageResource);
                        }
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                LogException(ex, "BPerson", "GetPersonInAdvanceSearch");
                throw ex;
            }
        }

        /// <summary>
        /// جستجوی سریع بر روی فیلدهای زیر:
        /// شماره پرسنلی
        /// نام
        /// نام خانوادگی
        /// شماره استخدام       
        /// </summary>
        /// <param name="searchValue"></param>
        /// <param name="searchInCategory"></param>
        /// <returns></returns>
        IList<Person> ISearchPerson.QuickSearch(string searchValue, PersonCategory searchInCategory)
        {
            try
            {
                BApplicationSettings.CheckGTSLicense();

                IList<Person> personList = new List<Person>();
                //DNN Note:Improve Perfromance
                //ISearchPerson searchTool = new BPerson();
                searchValue = searchValue == null ? String.Empty : searchValue;
                searchValue = searchValue.Trim();
                //int count = searchTool.GetPersonInQuickSearchCount(searchValue, searchInCategory);

                decimal managerId = this.GetCurentManagerId(ref searchInCategory);
                IList<Person> result = personRepository.Search(searchValue, BUser.CurrentUser.ID, managerId, searchInCategory);
                return result;

            }
            catch (Exception ex)
            {
                LogException(ex, "BPerson", "QuickSearch");
                throw ex;
            }
        }

        /// <summary>
        /// جستجوی سریع بر روی فیلدهای زیر:
        /// شماره پرسنلی
        /// نام
        /// نام خانوادگی
        /// شماره استخدام  
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        IList<Person> ISearchPerson.QuickSearchByPage(int pageIndex, int pageSize, string searchValue)
        {
            IList<Person> list = new List<Person>();

            //DNN Note:Improve Perfromance
            //ISearchPerson searchTool = new BPerson();
            IDataAccess port = new BUser();
            int accessibleCount = port.GetAccessibleDeparments().Count;
            if (accessibleCount > 0)
            {
                list = (this as ISearchPerson).QuickSearchByPage(pageIndex, pageSize, searchValue, PersonCategory.Public);
            }
            else
            {
                accessibleCount = port.GetAccessibleEmploymentTypes().Count();
                if (accessibleCount > 0)
                {
                    list = (this as ISearchPerson).QuickSearchByPage(pageIndex, pageSize, searchValue, PersonCategory.Public);
                }
                else
                {
                    PersonCategory category = this.GetCurrentPersonCategory();
                    if (category != PersonCategory.Public)
                    {
                        list = (this as ISearchPerson).QuickSearchByPage(pageIndex, pageSize, searchValue, category);
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// جستجوی سریع بر روی فیلدهای زیر:
        /// شماره پرسنلی
        /// نام
        /// نام خانوادگی
        /// شماره استخدام       
        /// </summary>
        /// <param name="searchValue"></param>
        /// <param name="pageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="searchInCategory"></param>
        /// <returns></returns>
        IList<Person> ISearchPerson.QuickSearchByPage(int pageIndex, int pageSize, string searchValue, PersonCategory searchInCategory)
        {
            try
            {
                BApplicationSettings.CheckGTSLicense();

                IList<Person> personList = new List<Person>();
                //DNN Note:Improve Perfromance
                //ISearchPerson searchTool = new BPerson();
                searchValue = searchValue == null ? String.Empty : searchValue;
                searchValue = searchValue.Trim();
                int count = (this as ISearchPerson).GetPersonInQuickSearchCount(searchValue, searchInCategory);
                if (pageSize * pageIndex == 0 || pageSize * pageIndex < count)
                {
                    decimal managerId = this.GetCurentManagerId(ref searchInCategory);
                    IList<Person> result = personRepository.Search(searchValue, BUser.CurrentUser.ID, managerId, searchInCategory, pageSize, pageIndex);
                    return result;
                }
                else
                {
                    throw new OutOfExpectedRangeException("0", Convert.ToString(count - 1), Convert.ToString(pageSize * (pageIndex + 1)), ExceptionSrc + "QuickSearchByPage");
                }
            }
            catch (Exception ex)
            {
                LogException(ex, "BPerson", "QuickSearchByPage");
                throw ex;
            }
        }

        IList<Person> ISearchPerson.QuickSearchMethodBaseByPage(int pageIndex, int pageSize, string searchValue, PersonCategory searchInCategory)
        {
            try
            {
                BApplicationSettings.CheckGTSLicense();

                IList<Person> personList = new List<Person>();
                //DNN Note:Improve Perfromance
                //ISearchPerson searchTool = new BPerson();
                searchValue = searchValue == null ? String.Empty : searchValue;
                searchValue = searchValue.Trim();
                int count = (this as ISearchPerson).GetPersonInQuickSearchCount(searchValue, searchInCategory);
                if (pageSize * pageIndex == 0 || pageSize * pageIndex < count)
                {
                    decimal managerId = this.GetCurentManagerId(ref searchInCategory);
                    IList<Person> result = personRepository.SearchMethodBase(searchValue, BUser.CurrentUser.ID, managerId, searchInCategory, pageSize, pageIndex);
                    return result;
                }
                else
                {
                    throw new OutOfExpectedRangeException("0", Convert.ToString(count - 1), Convert.ToString(pageSize * (pageIndex + 1)), ExceptionSrc + "QuickSearchByPage");
                }
            }
            catch (Exception ex)
            {
                LogException(ex, "BPerson", "QuickSearchMethodBaseByPage");
                throw ex;
            }
        }

        IList<Person> ISearchPerson.QuickSearchByPageBySubstitute(int pageIndex, int pageSize, string searchValue, PersonCategory searchInCategory, int personId)
        {
            try
            {
                BApplicationSettings.CheckGTSLicense();
                decimal PersonId = 0;
                if (searchInCategory == PersonCategory.Public)
                {
                    PersonId = this.NHSession.QueryOver<User>()
                                                 .Select(x => x.Person.ID)
                                                 .Where(x => x.ID == BUser.CurrentUser.ID)
                                                 .List<decimal>().SingleOrDefault();

                }
                else
                    PersonId = personId;
                IList<Person> personList = new List<Person>();
                //DNN Note:Improve Perfromance
                //ISearchPerson searchTool = new BPerson();
                searchValue = searchValue == null ? String.Empty : searchValue;
                searchValue = searchValue.Trim();
                int count = (this as ISearchPerson).GetPersonInQuickSearchCountBySubstitute(searchValue, searchInCategory, PersonId);
                if (pageSize * pageIndex == 0 || pageSize * pageIndex < count)
                {
                    decimal managerId = this.GetCurentManagerId(ref searchInCategory);
                    IList<Person> result = personRepository.SearchBySubstitute(PersonId, searchValue, BUser.CurrentUser.ID, managerId, searchInCategory, pageSize, pageIndex);
                    return result;
                }
                else
                {
                    throw new OutOfExpectedRangeException("0", Convert.ToString(count - 1), Convert.ToString(pageSize * (pageIndex + 1)), ExceptionSrc + "QuickSearchByPage");
                }
            }
            catch (Exception ex)
            {
                LogException(ex, "BPerson", "QuickSearchByPageBySubstitute");
                throw ex;
            }
        }
        IList<Person> ISearchPerson.QuickSearchByPageByMonthlyExceptionShift(int pageIndex, int pageSize, string searchValue, PersonCategory searchInCategory, IList<DateTime> MonthDatesList)
        {
            try
            {
                BApplicationSettings.CheckGTSLicense();

                IList<Person> personList = new List<Person>();
                //DNN Note:Improve Perfromance
                //ISearchPerson searchTool = new BPerson();
                searchValue = searchValue == null ? String.Empty : searchValue;
                searchValue = searchValue.Trim();
                int count = (this as ISearchPerson).GetPersonInQuickSearchCountByMonthlyExceptionShift(searchValue, searchInCategory, MonthDatesList);
                if (pageSize * pageIndex == 0 || pageSize * pageIndex < count)
                {
                    decimal managerId = this.GetCurentManagerId(ref searchInCategory);
                    IList<Person> result = personRepository.SearchByMonthlyExceptionShifts(searchValue, BUser.CurrentUser.ID, managerId, searchInCategory, pageSize, pageIndex, MonthDatesList);
                    return result;
                }
                else
                {
                    throw new OutOfExpectedRangeException("0", Convert.ToString(count - 1), Convert.ToString(pageSize * (pageIndex + 1)), ExceptionSrc + "QuickSearchByPage");
                }
            }
            catch (Exception ex)
            {
                LogException(ex, "BPerson", "QuickSearchByPageByMonthlyExceptionShift");
                throw ex;
            }
        }

        IList<Person> ISearchPerson.QuickIntegratedSearchByPage(int pageIndex, int pageSize, string searchValue, string integratedSerachValue, PersonCategory searchInCategory)
        {
            try
            {
                BApplicationSettings.CheckGTSLicense();

                IList<Person> personList = new List<Person>();

                //DNN Note:Improve Perfromance
                //ISearchPerson searchTool = new BPerson();

                searchValue = searchValue == null ? String.Empty : searchValue;
                searchValue = searchValue.Trim();
                int count = (this as ISearchPerson).GetPersonInQuickIntegratedSearchCount(searchValue, integratedSerachValue, searchInCategory);
                if (pageSize * pageIndex == 0 || pageSize * pageIndex < count)
                {
                    decimal managerId = this.GetCurentManagerId(ref searchInCategory);
                    IList<Person> result = personRepository.IntegratedSearch(searchValue, integratedSerachValue, BUser.CurrentUser.ID, managerId, searchInCategory, pageSize, pageIndex);
                    return result;
                }
                else
                {
                    throw new OutOfExpectedRangeException("0", Convert.ToString(count - 1), Convert.ToString(pageSize * (pageIndex + 1)), ExceptionSrc + "QuickIntegratedSearchByPage");
                }
            }
            catch (Exception ex)
            {
                LogException(ex, "BPerson", "QuickSearchByPage");
                throw ex;
            }
        }


        IList<Person> ISearchPerson.QuickSearchByPageByOperator(int pageIndex, int pageSize, string searchValue, out int count)
        {
            try
            {
                BApplicationSettings.CheckGTSLicense();

                IList<Person> personList = new List<Person>();
                //DNN Note:Improve Perfromance
                //ISearchPerson searchTool = new BPerson();
                searchValue = searchValue == null ? String.Empty : searchValue;
                searchValue = searchValue.Trim();
                IList<Person> result = personRepository.SearchByOperator(searchValue, BUser.CurrentUser.ID, pageSize, pageIndex, out count);
                return result;
            }
            catch (Exception ex)
            {
                LogException(ex, "BPerson", "QuickSearchByPageByOperator");
                throw ex;
            }
        }


        IList<Person> ISearchPerson.GetAllPerson(int pageIndex, int pageSize)
        {
            //DNN Note:Improve Perfromance
            //ISearchPerson p = new BPerson();
            return (this as ISearchPerson).QuickSearchByPage(pageIndex, pageSize, "");
        }

        /// <summary>
        /// جستجوی سریع بر روی فیلدهای زیر:
        /// شماره پرسنلی
        /// نام
        /// نام خانوادگی
        /// شماره استخدام  
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        IList<Person> ISearchPerson.QuickSearchByPageApplyCulture
            (int pageIndex, int pageSize, string searchValue)
        {
            //ISearchPerson searchTool = new BPerson();
            //IList<Person> list = searchTool.QuickSearchByPage(pageIndex, pageSize, searchValue, PersonCategory.Public);
            //return ApplyCultureAndFixCurrentObject(list, this.sysLanguageResource, this.localLanguageResource);

            ISearchPerson searchTool = new BPerson();
            IList<Person> list = new List<Person>();

            IDataAccess port = new BUser();
            int accessibleCount = port.GetAccessibleDeparments().Count;
            if (accessibleCount > 0)
            {
                list = searchTool.QuickSearchByPage(pageIndex, pageSize, searchValue, PersonCategory.Public);
                list = ApplyCultureAndFixCurrentObject(list, this.sysLanguageResource, this.localLanguageResource);
            }
            else
            {
                accessibleCount = port.GetAccessibleEmploymentTypes().Count;
                if (accessibleCount > 0)
                {
                    list = searchTool.QuickSearchByPage(pageIndex, pageSize, searchValue, PersonCategory.Public);
                    list = ApplyCultureAndFixCurrentObject(list, this.sysLanguageResource, this.localLanguageResource);
                }
                else
                {
                    PersonCategory category = this.GetCurrentPersonCategory();
                    if (category != PersonCategory.Public)
                    {
                        list = searchTool.QuickSearchByPage(pageIndex, pageSize, searchValue, category);
                        list = ApplyCultureAndFixCurrentObject(list, this.sysLanguageResource, this.localLanguageResource);
                    }
                }
            }
            return list;
        }
        IList<Person> ISearchPerson.QuickSearchByPageApplyCultureMonthlyExceptionShift
            (int pageIndex, int pageSize, string searchValue, IList<DateTime> MonthDatesList)
        {
            ISearchPerson searchTool = new BPerson();
            IList<Person> list = new List<Person>();

            IDataAccess port = new BUser();
            int accessibleCount = port.GetAccessibleDeparments().Count;
            if (accessibleCount > 0)
            {
                list = searchTool.QuickSearchByPageByMonthlyExceptionShift(pageIndex, pageSize, searchValue, PersonCategory.Public, MonthDatesList);
                list = ApplyCultureAndFixCurrentObject(list, this.sysLanguageResource, this.localLanguageResource);
            }
            else
            {
                accessibleCount = port.GetAccessibleEmploymentTypes().Count;
                if (accessibleCount > 0)
                {
                    list = searchTool.QuickSearchByPageByMonthlyExceptionShift(pageIndex, pageSize, searchValue, PersonCategory.Public, MonthDatesList);
                    list = ApplyCultureAndFixCurrentObject(list, this.sysLanguageResource, this.localLanguageResource);
                }
                else
                {
                    PersonCategory category = this.GetCurrentPersonCategory();
                    if (category != PersonCategory.Public)
                    {
                        list = searchTool.QuickSearchByPageByMonthlyExceptionShift(pageIndex, pageSize, searchValue, category, MonthDatesList);
                        list = ApplyCultureAndFixCurrentObject(list, this.sysLanguageResource, this.localLanguageResource);
                    }
                }
            }
            return list;
        }

        //IList<Person> ISearchPerson.QuickSearchByPageApplyCulture
        //    (int pageIndex, int pageSize, string searchValue , string barCode , decimal personId)
        //{
        //    //ISearchPerson searchTool = new BPerson();
        //    //IList<Person> list = searchTool.QuickSearchByPage(pageIndex, pageSize, searchValue, PersonCategory.Public);
        //    //return ApplyCultureAndFixCurrentObject(list, this.sysLanguageResource, this.localLanguageResource);
        //    if (personId != 0)
        //    {

        //    }

        //    ISearchPerson searchTool = new BPerson();
        //    IList<Person> list = new List<Person>();

        //    IDataAccess port = new BUser();
        //    int accessibleCount = port.GetAccessibleDeparments().Count;
        //    if (accessibleCount > 0)
        //    {
        //        list = searchTool.QuickSearchByPage(pageIndex, pageSize, searchValue, PersonCategory.Public);
        //        list = ApplyCultureAndFixCurrentObject(list, this.sysLanguageResource, this.localLanguageResource);
        //    }
        //    else
        //    {
        //        accessibleCount = port.GetAccessibleEmploymentTypes().Count;
        //        if (accessibleCount > 0)
        //        {
        //            list = searchTool.QuickSearchByPage(pageIndex, pageSize, searchValue, PersonCategory.Public);
        //            list = ApplyCultureAndFixCurrentObject(list, this.sysLanguageResource, this.localLanguageResource);
        //        }
        //        else
        //        {
        //            PersonCategory category = this.GetCurrentPersonCategory();
        //            if (category != PersonCategory.Public)
        //            {
        //                list = searchTool.QuickSearchByPage(pageIndex, pageSize, searchValue, category);
        //                list = ApplyCultureAndFixCurrentObject(list, this.sysLanguageResource, this.localLanguageResource);
        //            }
        //        }
        //    }
        //    return list;
        //}
        ///بررسی مدیر بودن یا جانشین بودن
        ///در غیر این صورت صفر برمیگردد
        ///
        public decimal GetCurentManagerId(ref PersonCategory personCategory)
        {
            decimal managerId = 0;
            if (personCategory == PersonCategory.Manager_UnderManagment)
            {
                Manager manager = new BManager().GetManagerByUsername(BUser.CurrentUser.UserName);
                managerId = manager.ID;
            }
            else if (personCategory == PersonCategory.Substitute_UnderManagment)
            {
                personCategory = PersonCategory.Manager_UnderManagment;
                IList<Substitute> subList = new SubstituteRepository(false).GetSubstitute(BUser.CurrentUser.Person.ID);
                subList = subList.Where(x => x.Active && x.Manager != null && x.Manager.Active).ToList();
                if (subList != null && subList.Count > 0)
                {
                    managerId = subList.OrderBy(x => x.ID).First().Manager.ID;
                }
            }
            return managerId;
        }

        #endregion

        #region ISearchPerson Controls

        Department ISearchPerson.GetDepartmentRoot()
        {
            return new BDepartment().GetDepartmentsTree();
        }

        /// <summary>
        /// بارگزاری بر اساس سطح دسترسی ,مدیر ,جانشین و اپراتور
        /// </summary>
        /// <returns></returns>
        IList<Department> ISearchPerson.GetAllDepartments()
        {
            IList<Department> depList = new List<Department>();
            //DNN Note:Improve Perfromance
            //ISearchPerson searchTool = new BPerson();
            IDataAccess port = new BUser();
            int accessibleCount = port.GetAccessibleDeparments().Count;
            if (accessibleCount > 0)
            {
                depList = new BDepartment().GetAll();
            }
            else
            {
                decimal objectId = 0;
                PersonCategory category = this.GetCurrentPersonCategory(ref objectId);
                if (category != PersonCategory.Public)
                {
                    BUnderManagment busUnder = new BUnderManagment();
                    switch (category)
                    {
                        case PersonCategory.Manager_UnderManagment:
                        case PersonCategory.Substitute_UnderManagment:
                            depList = new BDepartment().GetAllManagerDepartmentTree(objectId);
                            break;
                        case PersonCategory.Operator_UnderManagment:
                            depList = new BDepartment().GetAllOperatorDepartmentTree(objectId);
                            break;
                    }
                }
            }
            return depList;
        }

        IList<Department> ISearchPerson.GetDepartmentChild(decimal parentId, IList<Department> allDepartments)
        {
            IList<Department> depList = new List<Department>();
            depList = new BDepartment().GetDepartmentChilds(parentId, allDepartments);
            return depList;
        }

        /// <summary>
        /// تنها بر اساس سطح دسترسی بارگزاری میشود
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        IList<Department> ISearchPerson.GetDepartmentChild(decimal parentId)
        {
            //return new BDepartment().GetDepartmentChilds(parentId);
            IList<Department> depList = new List<Department>();
            //DNN Note:Improve Perfromance
            //ISearchPerson searchTool = new BPerson();

            IDataAccess port = new BUser();
            int accessibleCount = port.GetAccessibleDeparments().Count;
            if (accessibleCount > 0)
            {
                depList = new BDepartment().GetDepartmentChilds(parentId);
            }
            else
            {
                decimal objectId = 0;
                PersonCategory category = this.GetCurrentPersonCategory(ref objectId);
                if (category != PersonCategory.Public)
                {
                    BUnderManagment busUnder = new BUnderManagment();
                    switch (category)
                    {
                        case PersonCategory.Manager_UnderManagment:
                        case PersonCategory.Substitute_UnderManagment:
                            depList = new BDepartment().GetManagerDepartmentTree(objectId, parentId);
                            break;
                        case PersonCategory.Operator_UnderManagment:
                            depList = new BDepartment().GetOperatorDepartmentTree(objectId, parentId);
                            break;
                    }
                }
            }
            return depList;
        }

        OrganizationUnit ISearchPerson.GetOrganizationRoot()
        {
            return new BOrganizationUnit().GetOrganizationUnitTree();
        }

        IList<OrganizationUnit> ISearchPerson.GetOrganizationChild(decimal parentId)
        {
            IList<OrganizationUnit> organList = new List<OrganizationUnit>();
            return new BOrganizationUnit().GetChilds(parentId);
        }

        IList<ControlStation> ISearchPerson.GetAllControlStation()
        {
            return new BControlStation().GetAll();
        }

        IList<WorkGroup> ISearchPerson.GetAllWorkGroup()
        {
            return new BWorkgroup().GetAll();
        }

        IList<RuleCategory> ISearchPerson.GetAllRuleGroup()
        {
            return new BRuleCategory().GetAll();
        }
        IList<Contract> ISearchPerson.GetAllContract()
        {
            return new BContract().GetAll();
        }
        IList<CalculationRangeGroup> ISearchPerson.GetAllDateRanges()
        {
            return new BDateRange().GetAll();
        }

        IList<EmploymentType> ISearchPerson.GetAllEmploymentTypes()
        {
            return new BEmployment().GetAll();
        }

        IList<EmploymentType> ISearchPerson.GetAllEmploymentTypesWithoutAccessible()
        {
            return new BEmployment().GetAllWithoutAccessible();
        }


        #endregion

        /// <summary>
        /// شخص وارد شونده در یکی از دسته های زیر میباشد
        /// اپراتور
        /// مدیر
        /// مدیر سیستم
        /// جانشین
        /// </summary>
        private PersonCategory GetCurrentPersonCategory()
        {
            if (BUser.CurrentUser.UserName.ToLower().Equals("nunituser"))
            {
                return PersonCategory.Public;
            }
            decimal personId = BUser.CurrentUser.Person.ID;
            //آیا مدیر است؟
            BManager bmanager = new BManager();
            Manager m = bmanager.GetManager(personId);
            if (m.ID > 0)
                return PersonCategory.Manager_UnderManagment;
            //آیا جانشین است؟
            IList<Substitute> subList = new SubstituteRepository(false).GetSubstitute(personId);
            subList = subList.Where(x => x.Active && x.Manager != null && x.Manager.Active).ToList();
            if (subList != null && subList.Count > 0)
            {
                return PersonCategory.Substitute_UnderManagment;
            }
            //آیا اپراتور است؟
            IList<Operator> opList = new BOperator().GetOperator(personId);
            if (opList.Count > 0)
                return PersonCategory.Operator_UnderManagment;
            //در غیر این صورت مدیر است
            return PersonCategory.Public;
        }

        /// <summary>
        /// شخص وارد شونده در یکی از دسته های زیر میباشد
        /// اپراتور
        /// مدیر
        /// مدیر سیستم
        /// جانشین
        /// </summary>
        private PersonCategory GetCurrentPersonCategory(ref decimal objectId)
        {
            if (BUser.CurrentUser.UserName.ToLower().Equals("nunituser"))
            {
                return PersonCategory.Public;
            }
            decimal personId = BUser.CurrentUser.Person.ID;
            //آیا مدیر است؟
            BManager bmanager = new BManager();
            Manager m = bmanager.GetManager(personId);
            if (m.ID > 0)
            {
                objectId = m.ID;
                return PersonCategory.Manager_UnderManagment;
            }
            //آیا جانشین است؟
            IList<Substitute> subList = new SubstituteRepository(false).GetSubstitute(personId);
            subList = subList.Where(x => x.Active && x.Manager != null && x.Manager.Active).ToList();
            if (subList != null && subList.Count > 0)
            {
                objectId = subList.First().Manager.ID;
                return PersonCategory.Substitute_UnderManagment;
            }
            //آیا اپراتور است؟
            IList<Operator> opList = new BOperator().GetOperator(personId);
            if (opList.Count > 0)
            {
                objectId = personId;
                return PersonCategory.Operator_UnderManagment;
            }
            //در غیر این صورت مدیر است
            return PersonCategory.Public;
        }

        /// <summary>
        /// پرسنلی که باید در آنها جستجو شود را برای هر گروه برمیگرداند
        /// </summary>
        /// <param name="searchInCategory"></param>
        /// <returns></returns>
        private decimal[] GetAccessiblePersons_Excoud(PersonCategory searchInCategory)
        {
            List<Person> personList = new List<Person>();
            IList<decimal> personIds = new List<decimal>();
            IDataAccess dataAccessPort = new BUser();
            switch (searchInCategory)
            {
                case PersonCategory.Manager:
                    IList<decimal> managersId = dataAccessPort.GetAccessibleManagers();
                    personIds = new BManager().GetManagerPersons(managersId);
                    break;
                /*case PersonCategory.Operator:
                    personList.AddRange(new OperatorRepository(false).GetAllOperator());
                    var ids = from person in personList
                          select person.ID;

                    personIds = ids.ToList<decimal>();
                    break;*/
                case PersonCategory.Manager_UnderManagment:
                    Flow flow = new Flow();
                    IList<Operator> opList = new BOperator().GetOperator(BUser.CurrentUser.Person.ID);
                    if (opList.Count > 0)
                    {
                        foreach (Operator op in opList)
                        {
                            personList.AddRange(new BUnderManagment().GetUnderManagmentPersonsByFlow(op.Flow));
                        }
                    }
                    else
                    {
                        BManager bmanager = new BManager();
                        Manager m = bmanager.GetManager(BUser.CurrentUser.Person.ID);
                        if (m.ID > 0)
                        {
                            personList.AddRange(new BUnderManagment().GetUnderManagmentPersonsByManager(m.ID));
                        }
                    }
                    var ids = from person in personList
                              select person.ID;
                    personIds = ids.ToList<decimal>();
                    break;
                case PersonCategory.Substitute_UnderManagment:
                    decimal managerId = 0;
                    IList<Substitute> subList = new SubstituteRepository(false).GetSubstitute(BUser.CurrentUser.Person.ID);
                    subList = subList.Where(x => x.Active).ToList();
                    if (subList != null && subList.Count > 0)
                    {
                        managerId = subList.OrderBy(x => x.ID).First().Manager.ID;
                    }
                    if (managerId > 0)
                    {
                        personList.AddRange(new BUnderManagment().GetUnderManagmentPersonsByManager(managerId));
                    }

                    var ids1 = from person in personList
                               select person.ID;
                    personIds = ids1.ToList<decimal>();
                    break;
                case PersonCategory.Sentry_UnderManagment:
                    personIds = dataAccessPort.GetAccessiblePersonByDepartmentAndControlSatation();
                    break;
                case PersonCategory.Public:
                    personIds = dataAccessPort.GetAccessiblePersonByDepartment();
                    break;
            }
            return personIds.ToArray();
        }

        /// <summary>
        /// پرسنلی که باید در آنها جستجو شود را برای هر گروه برمیگرداند
        /// </summary>
        /// <param name="searchInCategory"></param>
        /// <returns></returns>
        private string[] GetAccessiblePersons2(PersonCategory searchInCategory)
        {
            List<Person> personList = new List<Person>();
            IList<decimal> personIds = new List<decimal>();
            IDataAccess dataAccessPort = new BUser();
            switch (searchInCategory)
            {
                case PersonCategory.Manager:
                    IList<decimal> managersId = dataAccessPort.GetAccessibleManagers();
                    personIds = new BManager().GetManagerPersons(managersId);
                    break;
                /* case PersonCategory.Operator:
                     personList.AddRange(new OperatorRepository(false).GetAllOperator());
                     var ids = from person in personList
                               select person.ID;

                     personIds = ids.ToList<decimal>();
                     break;*/
                case PersonCategory.Manager_UnderManagment:
                    Flow flow = new Flow();
                    IList<Operator> opList = new BOperator().GetOperator(BUser.CurrentUser.Person.ID);
                    if (opList.Count > 0)
                    {
                        foreach (Operator op in opList)
                        {
                            personList.AddRange(new BUnderManagment().GetUnderManagmentPersonsByFlow(op.Flow));
                        }
                    }
                    else
                    {
                        BManager bmanager = new BManager();
                        Manager m = bmanager.GetManager(BUser.CurrentUser.Person.ID);
                        if (m.ID > 0)
                        {
                            personList.AddRange(new BUnderManagment().GetUnderManagmentPersonsByManager(m.ID));
                        }
                    }
                    var ids = from person in personList
                              select person.ID;
                    personIds = ids.ToList<decimal>();
                    break;
                case PersonCategory.Sentry_UnderManagment:
                    personIds = dataAccessPort.GetAccessiblePersonByDepartmentAndControlSatation();
                    break;
                case PersonCategory.Public:
                    personIds = dataAccessPort.GetAccessiblePersonByDepartment();
                    break;
            }
            return personIds.Select(x => x.ToString()).ToArray();
        }

        /// <summary>
        /// فیلدهای رزرو
        /// تاریخ تولد
        /// تاریخ شروع استخدام
        /// تاریخ پایان استخدام
        /// تاریخ شروع اجرای محدوده محاسبات         
        /// دسته قانون جاری
        /// دسته کار! جاری
        /// نام دسته محدوده محاسبات جاری
        /// شناسه دسته محدوده محاسبات جاری
        /// </summary>
        /// <param name="list"></param>
        /// <param name="sysLanguage"></param>
        /// <param name="localLanguage"></param>
        /// <returns></returns>
        private IList<Person> ApplyCultureAndFixCurrentObject(IList<Person> list, SysLanguageResource sysLanguage, LocalLanguageResource localLanguage)
        {
            try
            {
                foreach (Person p in list)
                {
                    if (p.PersonDetail == null)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.PersonDetailNotExistsInDatabase, String.Format("جزئیات شخض {0} در دیتابیس موجود نیست", p.PersonCode), ExceptionSrc + " -ApplyCulture");
                    }
                    if (p.PersonTASpec == null)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.PersonDetailNotExistsInDatabase, String.Format("مشخصات اختصاصی حضور و غیاب شخض {0} در دیتابیس موجود نیست", p.PersonCode), ExceptionSrc + " -ApplyCulture");
                    }
                    p.PersonTASpec = TranslateReservedFields(p.PersonTASpec);

                    if (p.PersonTASpec.ControlStation == null)
                    {
                        p.PersonTASpec.ControlStation = new ControlStation();
                    }

                    if (p.PersonTASpec.UIValidationGroup == null)
                    {
                        p.PersonTASpec.UIValidationGroup = new UIValidationGroup();
                    }

                    if (sysLanguage == SysLanguageResource.Parsi)
                    {
                        if (p.PersonDetail.BirthDate == Utility.GTSMinStandardDateTime)
                        {
                            p.PersonDetail.UIBirthDate = "";
                        }
                        else
                        {
                            p.PersonDetail.UIBirthDate = Utility.ToPersianDate(p.PersonDetail.BirthDate);
                        }
                        if (p.EmploymentDate == Utility.GTSMinStandardDateTime)
                        {
                            p.UIEmploymentDate = "";
                        }
                        else
                        {
                            p.UIEmploymentDate = Utility.ToPersianDate(p.EmploymentDate);
                        }
                        if (p.EndEmploymentDate == Utility.GTSMinStandardDateTime)
                        {
                            p.UIEndEmploymentDate = "";
                        }
                        else
                        {
                            p.UIEndEmploymentDate = Utility.ToPersianDate(p.EndEmploymentDate);
                        }
                        if (p.PersonDetail.ChildBirthDate == Utility.GTSMinStandardDateTime)
                        {
                            p.PersonDetail.UIChildBirthDate = "";
                        }
                        else
                        {
                            p.PersonDetail.UIChildBirthDate = Utility.ToPersianDate(p.PersonDetail.ChildBirthDate);
                        }
                    }
                    else if (sysLanguage == SysLanguageResource.English)
                    {
                        if (p.PersonDetail.BirthDate == Utility.GTSMinStandardDateTime)
                        {
                            p.PersonDetail.UIBirthDate = "";
                        }
                        else
                        {
                            p.PersonDetail.UIBirthDate = Utility.ToString(p.PersonDetail.BirthDate);
                        }
                        if (p.EmploymentDate == Utility.GTSMinStandardDateTime)
                        {
                            p.UIEmploymentDate = "";
                        }
                        else
                        {
                            p.UIEmploymentDate = Utility.ToString(p.EmploymentDate);
                        }
                        if (p.EndEmploymentDate == Utility.GTSMinStandardDateTime)
                        {
                            p.UIEndEmploymentDate = "";
                        }
                        else
                        {
                            p.UIEndEmploymentDate = Utility.ToString(p.EndEmploymentDate);
                        }
                        if (p.PersonDetail.ChildBirthDate == Utility.GTSMinStandardDateTime)
                        {
                            p.PersonDetail.UIChildBirthDate = "";
                        }
                        else
                        {
                            p.PersonDetail.UIChildBirthDate = Utility.ToString(p.PersonDetail.ChildBirthDate);
                        }
                    }
                    p.CurrentRangeAssignment = this.GetCurrentRangeAssignment(p.ID);
                    if (p.CurrentRangeAssignment.ID > 0)
                    {
                        if (p.CurrentRangeAssignment.FromDate == Utility.GTSMinStandardDateTime)
                        {
                            p.CurrentRangeAssignment.UIFromDate = "";
                        }
                        else
                        {
                            p.CurrentRangeAssignment.UIFromDate = Utility.ToPersianDate(p.CurrentRangeAssignment.FromDate);
                        }
                    }
                    p.CurrentActiveRuleGroup = this.GetCurrentActiveRuleGroup(p.ID);
                    p.CurrentActiveWorkGroup = this.GetCurrentActiveWorkGroup(p.ID);
                    p.CurrentActiveDateRangeGroup = this.GetCurrentActiveDateRange(p.ID);
                    p.CurrentActiveContract = this.GetCurrentActiveContract(p.ID);
                    //p.CurrentActiveEmploymentType = this.GetCurrentActiveEmploymentType(p.ID);
                }
                return list;
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw ex;
            }
        }

        /// <summary>
        /// نام فیلدهای رزرو شده را برمیگرداند
        /// </summary>
        /// <param name="detail"></param>
        /// <returns></returns>
        private PersonTASpec TranslateReservedFields(PersonTASpec personSpec)
        {
            try
            {
                BPersonReservedField busPrsRsvFields = new BPersonReservedField();
                IList<PersonReserveField> list = busPrsRsvFields.GetAllReservedFields();
                EntityRepository<PersonReserveFieldComboValue> comboValuesRep = new EntityRepository<PersonReserveFieldComboValue>(false);
                /*
                detail.R1Lable = list.Where(x => x.OrginalName.Trim().Equals("R1")).First().Lable;
                detail.R2Lable = list.Where(x => x.OrginalName.Trim().Equals("R2")).First().Lable;
                detail.R3Lable = list.Where(x => x.OrginalName.Trim().Equals("R3")).First().Lable;
                detail.R4Lable = list.Where(x => x.OrginalName.Trim().Equals("R4")).First().Lable;
                detail.R5Lable = list.Where(x => x.OrginalName.Trim().Equals("R5")).First().Lable;
                detail.R6Lable = list.Where(x => x.OrginalName.Trim().Equals("R6")).First().Lable;
                detail.R7Lable = list.Where(x => x.OrginalName.Trim().Equals("R7")).First().Lable;
                detail.R8Lable = list.Where(x => x.OrginalName.Trim().Equals("R8")).First().Lable;
                detail.R9Lable = list.Where(x => x.OrginalName.Trim().Equals("R9")).First().Lable;
                detail.R10Lable = list.Where(x => x.OrginalName.Trim().Equals("R10")).First().Lable;
                detail.R11Lable = list.Where(x => x.OrginalName.Trim().Equals("R11")).First().Lable;
                detail.R12Lable = list.Where(x => x.OrginalName.Trim().Equals("R12")).First().Lable;
                detail.R13Lable = list.Where(x => x.OrginalName.Trim().Equals("R13")).First().Lable;
                detail.R14Lable = list.Where(x => x.OrginalName.Trim().Equals("R14")).First().Lable;
                detail.R15Lable = list.Where(x => x.OrginalName.Trim().Equals("R15")).First().Lable;
                detail.R16Lable = list.Where(x => x.OrginalName.Trim().Equals("R16")).First().Lable;
                detail.R17Lable = list.Where(x => x.OrginalName.Trim().Equals("R17")).First().Lable;
                detail.R18Lable = list.Where(x => x.OrginalName.Trim().Equals("R18")).First().Lable;
                detail.R19Lable = list.Where(x => x.OrginalName.Trim().Equals("R19")).First().Lable;
                detail.R20Lable = list.Where(x => x.OrginalName.Trim().Equals("R20")).First().Lable;
                 * */

                if (personSpec.R16 > 0)
                {
                    PersonReserveFieldComboValue combo = comboValuesRep.GetById(Utility.ToDecimal(personSpec.R16), false);
                    personSpec.R16Text = combo != null ? combo.ComboText : "";
                }
                if (personSpec.R17 > 0)
                {
                    PersonReserveFieldComboValue combo = comboValuesRep.GetById(Utility.ToDecimal(personSpec.R17), false);
                    personSpec.R17Text = combo != null ? combo.ComboText : "";
                }
                if (personSpec.R18 > 0)
                {
                    PersonReserveFieldComboValue combo = comboValuesRep.GetById(Utility.ToDecimal(personSpec.R18), false);
                    personSpec.R18Text = combo != null ? combo.ComboText : "";
                }
                if (personSpec.R19 > 0)
                {
                    PersonReserveFieldComboValue combo = comboValuesRep.GetById(Utility.ToDecimal(personSpec.R19), false);
                    personSpec.R19Text = combo != null ? combo.ComboText : "";
                }
                if (personSpec.R20 > 0)
                {
                    PersonReserveFieldComboValue combo = comboValuesRep.GetById(Utility.ToDecimal(personSpec.R20), false);
                    personSpec.R20Text = combo != null ? combo.ComboText : "";
                }
                return personSpec;
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw ex;
            }
        }

        private PersonTASpec CheckReservedFieldsForSave(PersonTASpec personSpec)
        {
            try
            {
                if (personSpec.R16 <= 0)
                {
                    //personSpec.R16 = null;
                    personSpec.R16Value = null;
                }
                if (personSpec.R17 <= 0)
                {
                    //personSpec.R17 = null;
                    personSpec.R17Value = null;
                }
                if (personSpec.R18 <= 0)
                {
                    //personSpec.R18 = null;
                    personSpec.R18Value = null;
                }
                if (personSpec.R19 <= 0)
                {
                    //personSpec.R19 = null;
                    personSpec.R19Value = null;
                }
                if (personSpec.R20 <= 0)
                {
                    //personSpec.R20 = null;
                    personSpec.R20Value = null;
                }
                return personSpec;
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw ex;
            }
        }

        private PersonCLSpec CheckReservedFieldsForSave(PersonCLSpec personSpec)
        {
            try
            {
                if (personSpec.R16 <= 0)
                {
                    //personSpec.R16 = null;
                    personSpec.R16Value = null;
                }
                if (personSpec.R17 <= 0)
                {
                    //personSpec.R17 = null;
                    personSpec.R17Value = null;
                }
                if (personSpec.R18 <= 0)
                {
                    //personSpec.R18 = null;
                    personSpec.R18Value = null;
                }
                if (personSpec.R19 <= 0)
                {
                    //personSpec.R19 = null;
                    personSpec.R19Value = null;
                }
                if (personSpec.R20 <= 0)
                {
                    //personSpec.R20 = null;
                    personSpec.R20Value = null;
                }
                return personSpec;
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw ex;
            }
        }

        /// <summary>
        /// همگام سازی جداول پرسنل حضور و غیاب با سیستم یکپارچه
        /// </summary>
        private void CheckTASpecPerson()
        {
            personRepository.SyncPersonTASpec();
        }

        public IList<Department> GetAllPersonnelDepartmentParents(decimal departmentID)
        {
            try
            {
                return this.personRepository.GetAllPersonnelDepartmentParents(departmentID);
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw ex;
            }
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckPersonnelLoadAccess()
        {
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertPerson(Person person, UIActionType UAT)
        {
            return base.SaveChanges(person, UAT);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal UpdatePerson(Person person, UIActionType UAT)
        {
            return base.SaveChanges(person, UAT);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal DeletePerson(Person person, UIActionType UAT)
        {
            return base.SaveChanges(person, UAT);
        }

        public void DeletePersonnelImage(string path)
        {
            try
            {
                this.personRepository.DeletePersonnelImage(path);
            }
            catch (Exception ex)
            {
                LogException(ex, "BPerson", "DeletePersonnelImage");
                throw ex;
            }
        }

        IList<Person> ISearchPerson.GetPersonByPersonIdList(IList<decimal> personIdList)
        {
            try
            {
                return this.personRepository.GetPersonByPersonIdList(personIdList);
            }
            catch (Exception ex)
            {
                LogException(ex, "BPerson", "GetPersonByPersonIdList");
                throw ex;
            }
        }

        public void CheckPersonnelExistanceValidation(decimal personID)
        {
            try
            {
                Person person = this.GetByID(personID);
                if (person.FirstName == null && person.LastName == null)
                    this.personRepository.Delete(person);
            }
            catch (Exception ex)
            {
                LogException(ex, "BPerson", "CheckPersonnelExistanceValidation");
            }
        }

        /// <summary>
        /// اطلاعات اضافی یک پرسنل را بر می گرداند
        /// </summary>
        /// <param name="personID">کلید پرسنل</param>
        /// <returns>آبجکت اطلاعات اضافی</returns>
        public PersonTASpec GetPersonTASpecByID(decimal personID)
        {
            try
            {
                return this.personRepository.GetPersonTASpecByID(personID);
            }
            catch (Exception ex)
            {
                LogException(ex, "BPerson", "GetPersonTASpecByID");
                throw ex;
            }
        }

        /// <summary>
        /// اطلاعات اضافی یک پرسنل را بر می گرداند
        /// </summary>
        /// <param name="personID">کلید پرسنل</param>
        /// <returns>آبجکت اطلاعات اضافی</returns>
        public IList<PersonTASpec> GetTASpecList()
        {
            try
            {
                return this.personRepository.GetTASpecList();
            }
            catch (Exception ex)
            {
                LogException(ex, "BPerson", "GetAllTASpec");
                throw ex;
            }
        }

        /// <summary>
        /// اطلاعات اضافی پرسنل را بر می گرداند
        /// </summary>
        /// <param name="personID">لیست کلید پرسنل </param>
        /// <returns>آبجکت اطلاعات اضافی</returns>
        public IList<PersonTASpec> GetTASpecByPersonIdList(IList<decimal> personIdList)
        {
            try
            {
                return this.personRepository.GetTASpecByPersonIdList(personIdList);
            }
            catch (Exception ex)
            {
                LogException(ex, "BPerson", "GetTASpecByPersonIdList");
                throw ex;
            }
        }
         
        public PersonControlStationExistanceState CheckPersonnelExistanceInControlStation(decimal userID, decimal machineID, Person person)
        {
            try
            {
                PersonControlStationExistanceState pcses = PersonControlStationExistanceState.Undefined;
                ISession NHSession = NHibernateSessionManager.Instance.GetSession();
                IDataAccess bDataAccess = new BUser();
                int personCount = 0;
                int controlStationCount = 0;
                Person personAlias = null;
                PersonTASpec personTASpecAlias = null;
                ControlStation controlStationAlias = null;
                GTS.Clock.Model.BaseInformation.Clock machineAlias = new Model.BaseInformation.Clock();
                GTS.Clock.Model.Temp.Temp tempAlias = null;

                IList<decimal> accessableIDs = bDataAccess.GetAccessibleControlStations(userID);

                if (accessableIDs.Count < this.operationBatchSizeValue && this.operationBatchSizeValue < 2100)
                {
                    IList<decimal> accessibleControlStationsIDList = bDataAccess.GetAccessibleControlStations(userID).ToList<decimal>();
                    personCount = NHSession.QueryOver(() => personAlias)
                                           .JoinAlias(() => personAlias.PersonTASpecList, () => personTASpecAlias)
                                           .JoinAlias(() => personTASpecAlias.ControlStation, () => controlStationAlias)
                                           .Where(() => controlStationAlias.ID.IsIn(accessibleControlStationsIDList.ToArray()) &&
                                                        personAlias.ID == person.ID
                                                 )
                                           .RowCount();
                    if (personCount > 0)
                        pcses = PersonControlStationExistanceState.Exist;
                    else
                    {
                        controlStationCount = NHSession.QueryOver(() => controlStationAlias)
                                                       .JoinAlias(() => controlStationAlias.ClockList, () => machineAlias)
                                                       .Where(() => controlStationAlias.ID.IsIn(accessibleControlStationsIDList.ToArray()) &&
                                                                    machineAlias.ID == machineID
                                                             )
                                                       .RowCount();
                        if (controlStationCount > 0)
                            pcses = PersonControlStationExistanceState.Exist;
                        else
                            pcses = PersonControlStationExistanceState.NotExist;
                    }
                }
                else
                {
                    string operationGUID = this.bTemp.InsertTempList(accessableIDs);
                    personCount = NHSession.QueryOver(() => personAlias)
                                           .JoinAlias(() => personAlias.PersonTASpecList, () => personTASpecAlias)
                                           .JoinAlias(() => personTASpecAlias.ControlStation, () => controlStationAlias)
                                           .JoinAlias(() => controlStationAlias.TempList, () => tempAlias)
                                           .Where(() => tempAlias.OperationGUID == operationGUID &&
                                                        personAlias.ID == person.ID
                                                 )
                                           .RowCount();
                    if (personCount > 0)
                        pcses = PersonControlStationExistanceState.Exist;
                    else
                    {
                        controlStationCount = NHSession.QueryOver(() => controlStationAlias)
                                                       .JoinAlias(() => controlStationAlias.ClockList, () => machineAlias)
                                                       .JoinAlias(() => controlStationAlias.TempList, () => tempAlias)
                                                       .Where(() => tempAlias.OperationGUID == operationGUID &&
                                                                    machineAlias.ID == machineID
                                                             )
                                                       .RowCount();
                        if (controlStationCount > 0)
                            pcses = PersonControlStationExistanceState.Exist;
                        else
                            pcses = PersonControlStationExistanceState.NotExist;
                    }
                    this.bTemp.DeleteTempList(operationGUID);
                }

                return pcses;
            }
            catch (Exception ex)
            {
                LogException(ex, "BPerson", "CheckPersonnelExistanceInControlStation");
                throw ex;
            }
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public Person RetrievelDeletedPersonnel(decimal personId, string barCode, string userPassword)
        {
            try
            {
                Person personAlias = null;
                User userAlias = null;
                Person person = NHSession.QueryOver<Person>(() => personAlias)
                                          .JoinAlias(() => personAlias.UserList, () => userAlias)
                                          .Where(x => (x.BarCode == barCode && x.IsDeleted == false) || userAlias.UserName == barCode)
                                          .List<Person>()
                                         .SingleOrDefault();
                if (person == null)
                {
                    person = this.GetByID(personId);
                    person.IsDeleted = false;
                    person.BarCode = barCode;
                    person.UserPassword = userPassword;
                    this.SaveChanges(person, UIActionType.EDIT);
                    this.NHSession.Evict(person);
                }
                return person;
            }
            catch (Exception ex)
            {
                LogException(ex, "BPerson", "RetrievelDeletedPersonnel");
                throw ex;
            }
        }

        /// <summary>
        /// لیست پرسنل یکه شاخه از چارت سازمانی را بر اساس نوع اضافه کار تشویقی بر می گرداند 
        /// </summary>
        /// <param name="departmenParentID">کلید شاخه چارت سازمانی</param>
        /// <param name="overTimePersuasiveType">نوع اضافه کار تشویقی</param>
        /// <returns>لیست پرسنل</returns>
        public IList<Person> GetPersonsByDepartmentParentID(decimal departmenParentID, OverTimePersuasiveType overTimePersuasiveType)
        {
            IList<Person> list = new List<Person>();
            list = personRepository.GetPersonsByDepartmentParentID(departmenParentID, overTimePersuasiveType).List<Person>();
            return list;
        }

        public IList<Person> GetPersonsByDepartmentId(decimal departmentId)
        {
            IList<Person> list = new List<Person>();
            list = personRepository.GetPersonByDepartmentId(departmentId).List<Person>();
            return list;
        }

        public IList<Person> GetPersonsByDirecetDepartmentId(decimal departmentId)
        {
            IList<Person> list = new List<Person>();
            list = personRepository.GetPersonByDirectDepartmentId(departmentId).List<Person>();
            return list;
        }

        /// <summary>
        /// تعداد پرسنل یکه شاخه از چارت سازمانی را بر اساس نوع اضافه کار تشویقی بر می گرداند  
        /// </summary>
        /// <param name="departmenParentID">کلید شاخه چارت سازمانی</param>
        /// <param name="overTimePersuasiveType">نوع اضافه کار تشویقی</param>
        /// <returns>تعداد پرسنل</returns>
        public int GetAllPersonsCountByDepartmentParentID(decimal departmenParentID, OverTimePersuasiveType overTimePersuasiveType)
        {
            int Count = 0;
            Count = personRepository.GetPersonsByDepartmentParentID(departmenParentID, overTimePersuasiveType).RowCount();
            return Count;
        }

        /// <summary>
        /// لیست پرسنل برای استفاده در اضافه کار تشویقی بر می گرداند 
        /// </summary>
        /// <returns>لیست پرسنل</returns>
        public IList<Person> GetPersonsInfoForOverTime()
        {
            IList<Person> list = new List<Person>();
            list = personRepository.GetPersonsInfoForOverTime().List<Person>();
            return list;
        }
    }
}