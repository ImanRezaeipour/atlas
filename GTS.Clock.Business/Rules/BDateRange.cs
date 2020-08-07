using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Model;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Model.AppSetting;
using GTS.Clock.Business.Security;
using NHibernate;
using NHibernate.Criterion;

namespace GTS.Clock.Business.Rules
{
    public class BDateRange : BaseBusiness<CalculationRangeGroup>
    {
        UIValidationGroupingRepository uivalidationGroupingRepository = new UIValidationGroupingRepository();
        CalculationDateRangeRepository dateRangeRepository = new CalculationDateRangeRepository(false);
        const string ExceptionSrc = "GTS.Clock.Business.Rules.BDateRange";
        private ISession NHSession = NHibernateSessionManager.Instance.GetSession();

        /// <summary>
        /// لیستی از مفاهیم که باید به آنها دوره اختصاص داده شود را برمیگرداند
        /// بر اساس نوع مرتب میشود تا در واسط کاربر بهتر نمایش داده شود
        /// همچنین برای نمایش چک باکس ها باید مشخص شود که کدام مفهوم دارای گروه دوره میباشد
        /// </summary>
        /// <returns></returns>
        public IList<SecondaryConcept> GetAllRanglyConcepts()
        {
            try
            {
                EntityRepository<SecondaryConcept> repository = new EntityRepository<SecondaryConcept>(false);
                IList<SecondaryConcept> list = repository.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new SecondaryConcept().PeriodicType), ScndCnpPeriodicType.NoPeriodic, CriteriaOperation.NotEqual)).OrderBy(x => x.Type).ToList();

                return list;
            }
            catch (Exception ex)
            {
                LogException(ex, "BDateRange", "GetAllRanglyConcepts");
                throw ex;
            }
        }

        /// <summary>
        /// با توجه به مفهوم مشخص شده وگروه دوره انتخابی لیستی از دوره محاسبات را بر میگرداند
        /// </summary>
        /// <param name="conceptTmpId"></param>
        /// <param name="calculationRangeGroupId"></param>
        /// <returns></returns>
        public IList<CalculationDateRange> GetAllDateRange(decimal conceptTmpId, decimal calculationRangeGroupId)
        {
            try
            {
                IList<CalculationDateRange> list = dateRangeRepository.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new CalculationDateRange().Concept), new SecondaryConcept() { ID = conceptTmpId }),
                                                                          new CriteriaStruct(Utility.GetPropertyName(() => new CalculationDateRange().RangeGroup), new CalculationRangeGroup() { ID = calculationRangeGroupId }));
                return list;
            }
            catch (Exception ex)
            {
                LogException(ex, "BDateRange", "GetAllDateRange");
                throw ex;
            }
        }

        /// <summary>
        /// درج گروه دوره و دورها به ازای چند مفهوم و درج دورهای پیشفرض برای مفاهیم مشخص نشده         
        /// </summary>
        /// <param name="calcDateRangeGroup"></param>
        /// <param name="defaultDateRanges"></param>
        /// <param name="dateRanges"></param>
        /// <param name="conceptTmpIds"></param>
        /// <returns></returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertDateRange(CalculationRangeGroup calcDateRangeGroup, IList<CalculationDateRange> defaultDateRanges, IList<CalculationDateRange> dateRanges, IList<decimal> conceptTmpIds)
        {
            try
            {
                #region validation
                UIValidationExceptions exception = new UIValidationExceptions();

                if (dateRanges == null || dateRanges.Count != 12)
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.DateRangesCountNotEqualToTwelve, "تعداد ماههای ارسالی برای ذخیره دوره محاسبات باید برابر 12 باشد", ExceptionSrc));
                }
                if (Utility.IsEmpty(conceptTmpIds))
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.DateRangesMustHaveConcept, "بمنظور بروزرسانی حتما باید یک یا چند مفهوم انتخاب شود", ExceptionSrc));
                }
                if (exception.Count > 0) { throw exception; }
                #endregion
                LanguagesName sysLanguage = BLanguage.CurrentSystemLanguage;
                if (sysLanguage != null)
                {
                    calcDateRangeGroup.Culture = sysLanguage;
                }
                calcDateRangeGroup.DateRangeList = new List<CalculationDateRange>();

                foreach (decimal conceptID in conceptTmpIds)
                {
                    IList<CalculationDateRange> rangeList = dateRanges.Clone<CalculationDateRange>();

                    for (int i = 0; i < rangeList.Count; i++)
                    {
                        rangeList[i].Concept = new SecondaryConcept() { ID = conceptID };
                        rangeList[i].RangeGroup = calcDateRangeGroup;
                        SetDateRangeIndex(rangeList[i]);
                    }
                    ((List<CalculationDateRange>)calcDateRangeGroup.DateRangeList).AddRange(rangeList);
                }
                //درج دورهای پیشفرض
                IList<SecondaryConcept> orginConcepts = this.GetAllRanglyConcepts();
                foreach (SecondaryConcept cnp in orginConcepts)
                {
                    if (conceptTmpIds.Where(x => x == cnp.ID).Count() == 0)
                    {
                        IList<CalculationDateRange> rangeList = defaultDateRanges.Clone<CalculationDateRange>();
                        for (int i = 0; i < rangeList.Count; i++)
                        {
                            rangeList[i].Concept = new SecondaryConcept() { ID = cnp.ID };
                            rangeList[i].RangeGroup = calcDateRangeGroup;
                            SetDateRangeIndex(rangeList[i]);
                        }
                        ((List<CalculationDateRange>)calcDateRangeGroup.DateRangeList).AddRange(rangeList);
                    }
                }
                base.SaveChanges(calcDateRangeGroup, UIActionType.ADD);

                return calcDateRangeGroup.ID;
            }
            catch (Exception ex)
            {
                LogException(ex, "BDateRange", "InsertDateRange");
                throw ex;
            }
        }

        public DateRange GetSpesificMonthDateRange(Person person, int IdentifierCode, DateTime CalculateDate, int Order)
        {
            if (IdentifierCode == 0)
                IdentifierCode = 9;
            SecondaryConcept ScndCnp = GetConcept(IdentifierCode);
            person.InitializeForDateRange(CalculateDate, CalculateDate, Utility.GetDateOfBeginYear(CalculateDate, BLanguage.CurrentSystemLanguage), Utility.GetDateOfEndYear(CalculateDate, BLanguage.CurrentSystemLanguage));

            DateRange daterange = person.GetPeriodicScndCnpSpesificRange(ScndCnp, CalculateDate, Order);
            return daterange;

        }
        /// <summary>
        /// ویرایش و بروزرسانی
        /// </summary>
        /// <param name="calcDateRangeGroup"></param>
        /// <param name="dateRanges"></param>
        /// <param name="conceptTmpIds"></param>
        /// <returns></returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal UpdateDateRange(CalculationRangeGroup calcDateRangeGroup, IList<CalculationDateRange> dateRanges, IList<decimal> conceptTmpIds)
        {
            try
            {
                #region validation
                UIValidationExceptions exception = new UIValidationExceptions();

                if (dateRanges == null || dateRanges.Count != 12)
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.DateRangesCountNotEqualToTwelve, "تعداد ماههای ارسالی برای ذخیره دوره محاسبات باید برابر 12 باشد", ExceptionSrc));
                }
                if (Utility.IsEmpty(conceptTmpIds))
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.DateRangesMustHaveConcept, "بمنظور بروزرسانی حتما باید یک یا چند مفهوم انتخاب شود", ExceptionSrc));
                }
                if (exception.Count > 0) { throw exception; }
                #endregion
                ApplicationLanguageSettings sysLanguage = new BLanguage().CurrentApplicationSetting;
                if (sysLanguage != null)
                {
                    calcDateRangeGroup.Culture = sysLanguage.Language.Name;
                }

                //بارگذاری دوره مجاسبات ثبت شده در دیتابیس                
                var result = from o in conceptTmpIds
                             select new SecondaryConcept() { ID = o };
                IList<SecondaryConcept> conceptsList = result.ToList<SecondaryConcept>();

                IList<CalculationDateRange> dateRangeListInDatabase = dateRangeRepository.GetCalculationDateRanges(calcDateRangeGroup, conceptsList);
                //استخراج مفاهیمی که در حال حاضر در دیتابیس دوره محاسبات ندارند
                IList<SecondaryConcept> extraCnps = conceptsList.Where(x => dateRangeListInDatabase.Select(y => y.Concept.ID).Contains(x.ID) == false).ToList();
                IList<CalculationDateRange> newOrderList = new List<CalculationDateRange>();
                List<CalculationDateRange> resultDateRangeList = new List<CalculationDateRange>();

                foreach (CalculationDateRange dateRange in dateRanges.OrderBy(x => x.Order).ToList())
                {
                    //بروزرساتی دورهای محاسباتی
                    IList<CalculationDateRange> sameOrderList = dateRangeListInDatabase.Where(x => x.Order == dateRange.Order).ToList();
                    for (int i = 0; i < sameOrderList.Count; i++)
                    {
                        sameOrderList[i].FromDay = dateRange.FromDay;
                        sameOrderList[i].FromMonth = dateRange.FromMonth;
                        sameOrderList[i].ToDay = dateRange.ToDay;
                        sameOrderList[i].ToMonth = dateRange.ToMonth;
                        SetDateRangeIndex(sameOrderList[i]);
                    }
                    //درج دوره محاسبات برای مفاهیم جدید
                    foreach (SecondaryConcept cnp in extraCnps)
                    {
                        CalculationDateRange dr = new CalculationDateRange();
                        dr.Concept = cnp;
                        dr.FromDay = dateRange.FromDay;
                        dr.FromMonth = dateRange.FromMonth;
                        dr.ToDay = dateRange.ToDay;
                        dr.ToMonth = dateRange.ToMonth;
                        dr.Order = dateRange.Order;
                        dr.RangeGroup = calcDateRangeGroup;
                        SetDateRangeIndex(dr);
                        newOrderList.Add(dr);
                    }
                }

                resultDateRangeList.AddRange(dateRangeListInDatabase);
                resultDateRangeList.AddRange(newOrderList);
                calcDateRangeGroup.DateRangeList = resultDateRangeList;

                base.SaveChanges(calcDateRangeGroup, UIActionType.EDIT);
                return calcDateRangeGroup.ID;
            }
            catch (Exception ex)
            {
                LogException(ex, "BDateRange", "UpdateDateRange");
                throw ex;
            }
        }

        /// <summary>
        /// حذف یک آیتم
        /// </summary>
        /// <param name="calcDateRangeGroup"></param>
        /// <returns></returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal DeleteDateRange(CalculationRangeGroup calcDateRangeGroup)
        {
            try
            {
                base.SaveChanges(calcDateRangeGroup, UIActionType.DELETE);
                return calcDateRangeGroup.ID;
            }
            catch (Exception ex)
            {
                LogException(ex, "BDateRange", "DeleteDateRange");
                throw ex;
            }
        }

        /// <summary>
        /// یک کپی از یک گروه محدوده محاسبات میگیرد
        /// </summary>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal CopyDateRangeGroup(decimal groupDateRangeId)
        {
            try
            {
                EntityRepository<CalculationRangeGroup> groupRep = new EntityRepository<CalculationRangeGroup>(false);
                IList<CalculationRangeGroup> list = groupRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new CalculationRangeGroup().ID), groupDateRangeId));
                if (list.Count == 1)
                {
                    CalculationRangeGroup group = list[0];
                    CalculationRangeGroup copyGroup = new CalculationRangeGroup();
                    copyGroup = (CalculationRangeGroup)group.Clone();
                    copyGroup.DateRangeList = group.DateRangeList.Clone();
                    copyGroup.Name = " Copy Of " + group.Name;
                    foreach (CalculationDateRange range in copyGroup.DateRangeList)
                    {
                        range.RangeGroup = copyGroup;
                    }
                    SaveChanges(copyGroup, UIActionType.ADD);
                    return copyGroup.ID;
                }
                else
                {
                    UIValidationExceptions exception = new UIValidationExceptions();
                    exception.Add(new ValidationException(ExceptionResourceKeys.DateRangesCopyIdIsNotValid, "آیتم انتخاب شده جهت گرفتن کپی موجود نیست", ExceptionSrc));
                    throw exception;
                }
            }
            catch (Exception ex)
            {
                LogException(ex, "BDateRange", "CopyDateRangeGroup");
                throw ex;
            }
        }

        /// <summary>
        /// نام تکراری
        /// نام خالی
        /// </summary>
        /// <param name="dateRangeGroup"></param>
        protected override void InsertValidate(CalculationRangeGroup dateRangeGroup)
        {
            UIValidationExceptions exception = new UIValidationExceptions();
            EntityRepository<CalculationRangeGroup> groupRepository = new EntityRepository<CalculationRangeGroup>(false);
           // this.CheckUserInterfaceRuleGroup(dateRangeGroup);
            if (Utility.IsEmpty(dateRangeGroup.Name))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.DateRangesGroupNameRequierd, "نام گروه دوره مجاسبات باید وارد شود", ExceptionSrc));
            }
            else if (groupRepository.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => dateRangeGroup.Name), dateRangeGroup.Name)) > 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.DateRangesGroupNameRepeated, "نام گروه دوره مجاسبات نباید تکراری باشد", ExceptionSrc));
            }

            if (exception.Count > 0)
            {
                throw exception;
            }
        }
        private void CheckUserInterfaceRuleGroup(CalculationRangeGroup dateRangeGroup)
        {
            try
            {
                CalculationRangeGroup DateRangeGroup = base.GetByID(dateRangeGroup.ID);                
                    //foreach (PersonRangeAssignment assign in DateRangeGroup.PersonRangeAssignmentList)
                    //{
                    //    if (assign.Person.PersonTASpec.UIValidationGroup != null)
                    //    {
                    //        base.UIValidator.GetCalculationLockDateByGroup(assign.Person.PersonTASpec.UIValidationGroup.ID);
                    //    }
                    //}      
                IList<decimal> UiValidationGroupIdList = uivalidationGroupingRepository.GetUivalidationIdListByRuleCategory(dateRangeGroup.ID);
                foreach (decimal uiValidateionGrpId in UiValidationGroupIdList)
                {
                    DateTime calculationLockDate = base.UIValidator.GetCalculationLockDateByGroup(uiValidateionGrpId);
                }            
                this.NHSession.Evict(DateRangeGroup);
            }
            catch (Exception ex)
            {
                LogException(ex, "BDateRange", "CheckUserInterfaceRuleGroup");
                throw ex;
            }
        }
        /// <summary>
        /// نام تکراری
        /// نام خالی 
        /// </summary>
        /// <param name="dateRangeGroup"></param>
        protected override void UpdateValidate(CalculationRangeGroup dateRangeGroup)
        {
            UIValidationExceptions exception = new UIValidationExceptions();
            EntityRepository<CalculationRangeGroup> groupRepository = new EntityRepository<CalculationRangeGroup>(false);
            this.CheckUserInterfaceRuleGroup(dateRangeGroup);           
            if (Utility.IsEmpty(dateRangeGroup.Name))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.DateRangesGroupNameRequierd, "نام گروه دوره مجاسبات باید وارد شود", ExceptionSrc));
            }
            else if (groupRepository.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => dateRangeGroup.Name), dateRangeGroup.Name)
                , new CriteriaStruct(Utility.GetPropertyName(() => dateRangeGroup.ID), dateRangeGroup.ID, CriteriaOperation.NotEqual)) > 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.DateRangesGroupNameRepeated, "نام گروه دوره مجاسبات نباید تکراری باشد", ExceptionSrc));
            }

            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        /// <summary>
        /// توسط پرسنل استفاده نشده باشد
        /// </summary>
        /// <param name="dateRangeGroup"></param>
        protected override void DeleteValidate(CalculationRangeGroup dateRangeGroup)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            EntityRepository<PersonRangeAssignment> assgnRep = new EntityRepository<PersonRangeAssignment>();
            if (assgnRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new PersonRangeAssignment().CalcDateRangeGroup), dateRangeGroup)) > 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.DateRangesUsedByPerson, "این دوره محاسبات توسط اشخاص استفاده شده است", ExceptionSrc));
            }

            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        /// <summary>
        /// ایندکس عددی ابتدا و انتها را میسازد        
        /// </summary>
        /// <param name="dateRange"></param>
        /// <param name="langName"></param>
        private void SetDateRangeIndex(CalculationDateRange dateRange)
        {
            //تداخل با سال قبل
            if (dateRange.Order == CalculationDateRangeOrder.Month1 && dateRange.FromMonth == 12)
            {
                dateRange.FromIndex = Utility.ToDateRangeIndex(dateRange.FromMonth, dateRange.FromDay, 1);
            }
            else
            {
                dateRange.FromIndex = Utility.ToDateRangeIndex(dateRange.FromMonth, dateRange.FromDay, 2);
            }
            //تداخل با سال بعد
            if (dateRange.Order == CalculationDateRangeOrder.Month12 && dateRange.ToMonth == 1)
            {
                dateRange.ToIndex = Utility.ToDateRangeIndex(dateRange.ToMonth, dateRange.ToDay, 3);
            }
            else
            {
                dateRange.ToIndex = Utility.ToDateRangeIndex(dateRange.ToMonth, dateRange.ToDay, 2);
            }
        }

        protected void UpdateCFP1(CalculationRangeGroup obj, UIActionType action)
        {
            if (action == UIActionType.EDIT)
            {
                NHibernateSessionManager.Instance.ClearSession();
                CalculationRangeGroup dateRangeGroup = base.GetByID(obj.ID);
                if (dateRangeGroup.PersonRangeAssignmentList != null)
                {
                    foreach (PersonRangeAssignment assign in dateRangeGroup.PersonRangeAssignmentList)
                    {
                        decimal personId = assign.Person.ID;
                        CFP cfp = base.GetCFP(personId);
                        DateTime newCfpDate = assign.FromDate;

                        if (cfp.ID == 0 || cfp.Date > newCfpDate)
                        {
                            DateTime calculationLockDate = base.UIValidator.GetCalculationLockDate(personId);

                            //بسته بودن محاسبات 
                            if (calculationLockDate > Utility.GTSMinStandardDateTime && calculationLockDate > newCfpDate)
                            {
                                newCfpDate = calculationLockDate.AddDays(1);
                            }

                            base.UpdateCFP(personId, newCfpDate);
                        }
                    }
                }
            }
        }

        protected override void UpdateCFP(CalculationRangeGroup obj, UIActionType action)
        {
            if (action == UIActionType.EDIT)
            {
                IList<CFP> cfpList = new List<CFP>();
                Dictionary<decimal, DateTime> lockDates = new Dictionary<decimal, DateTime>();
                NHibernateSessionManager.Instance.ClearSession();
                CalculationRangeGroup dateRangeGroup = base.GetByID(obj.ID);


                Dictionary<decimal, DateTime> uivalidationGroupIdDic = new Dictionary<decimal, DateTime>();
                IList<PersonRangeAssignment> assignPersonDateRangeList = dateRangeGroup.PersonRangeAssignmentList;
                IList<CFP> cfpPersonList = new List<CFP>();
                if (assignPersonDateRangeList.Count > 0)
                    cfpPersonList = base.GetCFPPersons(assignPersonDateRangeList.Select(a => a.Person.ID).ToList<decimal>());
                IList<decimal> UiValidationGroupIdList = uivalidationGroupingRepository.GetUivalidationIdListByCalculationRangeGroup(dateRangeGroup.ID);
                IList<decimal> cfpPersonIdInsertList = new List<decimal>();
                foreach (decimal uiValidateionGrpId in UiValidationGroupIdList)
                {

                    if (!uivalidationGroupIdDic.ContainsKey(uiValidateionGrpId))
                    {
                        DateTime calculationLockDate = base.UIValidator.GetCalculationLockDateByGroup(uiValidateionGrpId);
                        uivalidationGroupIdDic.Add(uiValidateionGrpId, calculationLockDate);
                    }

                }
                base.UpdateCfpByDateRangeGroup(dateRangeGroup.ID, uivalidationGroupIdDic);
                cfpPersonIdInsertList = assignPersonDateRangeList.Where(p => cfpPersonList != null && !cfpPersonList.Select(c => c.PrsId).ToList().Contains(p.Person.ID)).Select(p => p.Person.ID).Distinct().ToList<decimal>();
                if (cfpPersonIdInsertList.Count > 0)
                    base.InsertCfpByDateRangeGroup(cfpPersonIdInsertList, dateRangeGroup.ID, uivalidationGroupIdDic);

            }
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckCalculationRangeLoadAccess()
        {
        }

        public DateRange GetDateRangePerson(Person person, decimal IdentifierCode, DateTime CalculateDate)
        {
            if (IdentifierCode == 0)
                IdentifierCode = 9;
            SecondaryConcept ScndCnp = GetConcept(IdentifierCode);
            person.InitializeForDateRange(CalculateDate, CalculateDate, Utility.GetDateOfBeginYear(CalculateDate, BLanguage.CurrentSystemLanguage), Utility.GetDateOfEndYear(CalculateDate, BLanguage.CurrentSystemLanguage));

            DateRange daterange = person.GetPeriodicScndCnpRange(ScndCnp, CalculateDate);
            return daterange;

        }
        public virtual SecondaryConcept GetConcept(decimal IdentifierCode)
        {
            IDictionary<decimal, SecondaryConcept> ConceptList;
            ConceptList = new Dictionary<decimal, SecondaryConcept>();
            SecondaryConcept concept = null;
            concept = SecondaryConcept.GetRepository(false).Find(c => c.IdentifierCode == IdentifierCode).FirstOrDefault();

            return concept;
        }
    }
}
