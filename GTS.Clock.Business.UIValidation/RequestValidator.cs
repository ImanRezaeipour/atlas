using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Model.UIValidation;
using GTS.Clock.Model.RequestFlow;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Business.Security;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Business.ArchiveCalculations;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Model;
using GTS.Clock.Business.Rules;
using NHibernate.Criterion;
using GTS.Clock.Business.RequestFlow;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Business.Leave;
using GTS.Clock.Model.AppSetting;
using GTS.Clock.Infrastructure;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Infrastructure.NHibernateFramework;
using NHibernate;
using NHibernate.Linq;
using NHibernate.Transform;
using GTS.Clock.Business.WorkFlow;
using GTS.Clock.Infrastructure.RepositoryFramework;
using GTS.Clock.Business.Engine;
using GTS.Clock.Business.OverTimeFlow;

namespace GTS.Clock.Business.UIValidation
{
    public class RequestValidator : IRequestUIValidation, ILockCalculationUIValidation, IArchiveCalculationUIValidation
    {
        const string ExceptionSrc = "GTS.Clock.Business.UIValidation.RequestValidator";
        UIValidationGroupingRepository validateRep = new UIValidationGroupingRepository();
        RequestRepository requestRepository = new RequestRepository(false);
        UIValidationExceptions exception = new UIValidationExceptions();
        BApprovalAttendanceSchedule ApprovalAttendanceScheduleBusiness = new BApprovalAttendanceSchedule();
        BApprovalAttendanceScheduleException ApprovalAttendanceScheduleExceptionBusiness = new BApprovalAttendanceScheduleException();

        CFPRepository cfpRepository = new CFPRepository(false);
        private ISession NHSession = NHibernateSessionManager.Instance.GetSession();
        public bool IsCurrentUserManager
        {
            get
            {
                if (SessionHelper.HasSessionValue(SessionHelper.GTSCurrentUserManagmentState))
                {
                    Dictionary<string, object> managementState = (Dictionary<string, object>)SessionHelper.GetSessionValue(SessionHelper.GTSCurrentUserManagmentState);
                    if (Utility.ToBoolean(managementState["IsManager"]))
                        return true;
                    else return false;
                }
                else
                {
                    return false;
                }
                //BOperator op = new BOperator();
                //IList<Operator> opList = op.GetOperator(this.GetCurentPersonId());
                //return opList.Count > 0 ? true : false;
            }
        }
        public IUserRegisteredRequests RegisteredRequestsBusiness
        {
            get
            {
                return new BUnderManagment();
            }
        }
        /// <summary>
        /// تاریخ بسته شدن محاسبات را برای یک شخص برمیگرداند
        /// بستن محاسبات تنها از طریق قوانین 4 و 5 صورت میگیرد
        /// دقت شود که همیشه حداکثر تنها یکی از این دو فعال هستند
        /// لازم به ذکر است اگر 4 و 5 فعال نباشد بصورت پیشفرض قانون 6 درنظر گرفته میشود
        /// پس همیشه باید قانون 6 پارامتر داشته باشد
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        public DateTime GetCalculationLockDate(decimal personId)
        {

            DateTime lockDate = Utility.GTSMinStandardDateTime;
            //  IList<UIValidationRuleGroup> RulegroupList = new List<UIValidationRuleGroup>();          
            IList<UIValidationRuleGroup> groupingList = new List<UIValidationRuleGroup>();

            groupingList = validateRep.GetByPersonId(personId);
            //IList<UIValidationRule> ruleList = new EntityRepository<UIValidationRule>().Find(x => x.SubsystemID == 1).ToList();

            UIValidationRuleGroup grouping = groupingList.Where(x => x.ValidationRule != null && x.ValidationRule.CustomCode == 1.ToString()).ToList().FirstOrDefault();
            //در حال تعریف پرسنل هستیم و هنوز گروه واسط کاربر انتساب ندادیم
            if (grouping == null)
            {
                return DateTime.Now;
            }



            #region R3
            //else //r3
            //{
            grouping = groupingList.Where(x => x.ValidationRule != null && x.ValidationRule.CustomCode == 3.ToString()).ToList().FirstOrDefault();
            if (grouping != null && grouping.Active)
            {

                //IList<UIValidationRuleParam> parameters = validateRep.GetParameterRule(grouping.ValidationRule.CustomCode);
                //        UIValidationRuleGroup RuleGroup = new UIValidationRuleGroup();
                groupingList = validateRep.GetByPersonId(personId);
                UIValidationRuleGroup RuleGroup = groupingList.Where(x => x.ValidationRule != null && x.ValidationRule.CustomCode == 3.ToString()).ToList().FirstOrDefault();
                IList<UIValidationRuleParam> parameters = validateRep.GetParameterRule(RuleGroup.ID);

                #region cheking
                if (Utility.IsEmpty(parameters) || parameters.Count != 2)
                {
                    throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterCount, "تعداد پارامتر های قانون 3 با مقدار مورد انتظار نابرابر است ", ExceptionSrc);
                }
                UIValidationRuleParam param = parameters.Where(x => !Utility.IsEmpty(x.UIValidationRuleTempParam.KeyName) && x.UIValidationRuleTempParam.KeyName.Equals("LockCalculationFromDate")).FirstOrDefault();
                if (param == null)
                {
                    throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, String.Format("پارامتر با کلید LockCalculationFromDate    برای قانون 3 یافت نشد، "), ExceptionSrc);
                }
                UIValidationRuleParam SecParam = parameters.Where(x => !Utility.IsEmpty(x.UIValidationRuleTempParam.KeyName) && x.UIValidationRuleTempParam.KeyName.Equals("NumberOfDaysAfterLockDate")).FirstOrDefault();
                if (SecParam == null)
                {
                    throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, "پارامتر با کلید  - زمان مجاز تایید درخواست بعد از بستن محاسبات NumberOfDaysAfterLockDate  نظر برای قانون 3 یافت نشد ", ExceptionSrc);
                }
                #endregion

                DateTime LockCalculationFromDate = new DateTime();
                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    LockCalculationFromDate = Utility.ToMildiDateTime(param.Value);
                }
                else
                {
                    LockCalculationFromDate = Utility.ToMildiDateTime(param.Value);
                }
                lockDate = LockCalculationFromDate;
            }
            //}
            #endregion


            if (lockDate == Utility.GTSMinStandardDateTime)
            {
                throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationR3MustBeActive, "قانون <محاسبات تا تاریخ _ بسته شود> باید فعال باشد", ExceptionSrc);
            }
            return lockDate;
        }

        public DateTime GetCalculationLockDate(Person person)
        {
            try
            {
                //groupAlias.ID == personTASpecAlias.UIValidationGroup.ID
                DateTime lockDate = Utility.GTSMinStandardDateTime;
                IList<UIValidationRuleGroup> RuleGroupList = new List<UIValidationRuleGroup>();

                // RuleGroupList = validateRep.GetByPersonId(personId);
                UIValidationRuleGroup ruleGroupAlias = null;
                // PersonTASpec personTASpecAlias = null;
                // Person personAlias = null;
                UIValidationGroup groupAlias = null;
                RuleGroupList = NHSession.QueryOver(() => ruleGroupAlias)
                                         .JoinAlias(() => ruleGroupAlias.ValidationGroup, () => groupAlias)
                                         .Where(x => groupAlias.ID == person.PersonTASpec.UIValidationGroup.ID)
                                         .List<UIValidationRuleGroup>();
                //IList<UIValidationRule> ruleList = new EntityRepository<UIValidationRule>().Find(x => x.SubsystemID == 1).ToList();

                UIValidationRuleGroup grouping = RuleGroupList.Where(x => x.ValidationRule != null && x.ValidationRule.CustomCode == 1.ToString()).ToList().FirstOrDefault();
                //در حال تعریف پرسنل هستیم و هنوز گروه واسط کاربر انتساب ندادیم
                if (grouping == null)
                {
                    return DateTime.Now;
                }
                #region R3
                //else //r3
                //{
                grouping = RuleGroupList.Where(x => x.ValidationRule != null && x.ValidationRule.CustomCode == 3.ToString()).ToList().FirstOrDefault();
                if (grouping != null && grouping.Active)
                {
                    RuleGroupList = NHSession.QueryOver(() => ruleGroupAlias)
                                        .JoinAlias(() => ruleGroupAlias.ValidationGroup, () => groupAlias)
                                        .Where(x => groupAlias.ID == person.PersonTASpec.UIValidationGroup.ID)
                                        .List<UIValidationRuleGroup>();
                    UIValidationRuleGroup RuleGroup = RuleGroupList.Where(x => x.ValidationRule != null && x.ValidationRule.CustomCode == 3.ToString()).ToList().FirstOrDefault();
                    IList<UIValidationRuleParam> parameters = validateRep.GetParameterRule(RuleGroup.ID);

                    #region cheking
                    if (Utility.IsEmpty(parameters) || parameters.Count != 2)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterCount, "تعداد پارامتر های قانون 3 با مقدار مورد انتظار نابرابر است ", ExceptionSrc);
                    }
                    UIValidationRuleParam param = parameters.Where(x => !Utility.IsEmpty(x.UIValidationRuleTempParam.KeyName) && x.UIValidationRuleTempParam.KeyName.Equals("LockCalculationFromDate")).FirstOrDefault();
                    if (param == null)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, String.Format("پارامتر با کلید LockCalculationFromDate    برای قانون 3 یافت نشد، "), ExceptionSrc);
                    }
                    UIValidationRuleParam SecParam = parameters.Where(x => !Utility.IsEmpty(x.UIValidationRuleTempParam.KeyName) && x.UIValidationRuleTempParam.KeyName.Equals("NumberOfDaysAfterLockDate")).FirstOrDefault();
                    if (SecParam == null)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, "پارامتر با کلید  - زمان مجاز تایید درخواست بعد از بستن محاسبات NumberOfDaysAfterLockDate  نظر برای قانون 3 یافت نشد ", ExceptionSrc);
                    }
                    #endregion

                    DateTime LockCalculationFromDate = new DateTime();
                    if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                    {
                        LockCalculationFromDate = Utility.ToMildiDateTime(param.Value);
                    }
                    else
                    {
                        LockCalculationFromDate = Utility.ToMildiDateTime(param.Value);
                    }
                    lockDate = LockCalculationFromDate;
                }
                //}
                #endregion


                if (lockDate == Utility.GTSMinStandardDateTime)
                {
                    throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationR3MustBeActive, "قانون <محاسبات تا تاریخ _ بسته شود> باید فعال باشد", ExceptionSrc);
                }
                return lockDate;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "RequestValidator", "GetCalculationLockDate");
                throw ex;
            }

        }
        /// <summary>
        /// تاریخ بسته شدن محاسبات را برای یک شخص برمیگرداند
        /// بستن محاسبات تنها از طریق قوانین 4 و 5 صورت میگیرد
        /// دقت شود که همیشه حداکثر تنها یکی از این دو فعال هستند
        /// لازم به ذکر است اگر 4 و 5 فعال نباشد بصورت پیشفرض قانون 6 درنظر گرفته میشود
        /// پس همیشه باید قانون 6 پارامتر داشته باشد
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        public DateTime GetCalculationLockDateByGroup(decimal uiGroupId, bool IsPermit = false)
        {

            DateTime lockDate = Utility.GTSMinStandardDateTime;
            IList<UIValidationRuleGroup> groupingList = new List<UIValidationRuleGroup>();
            groupingList = validateRep.GetByGroupId(uiGroupId);
            //IList<UIValidationRule> ruleList = new EntityRepository<UIValidationRule>().Find(x => x.SubsystemID == 1).ToList();

            UIValidationRuleGroup grouping = groupingList.Where(x => x.ValidationRule != null && x.ValidationRule.CustomCode == 1.ToString()).ToList().FirstOrDefault();
            //در حال تعریف پرسنل هستیم و هنوز گروه واسط کاربر انتساب ندادیم
            if (grouping == null)
            {
                return DateTime.Now;
            }

            //R4
            //#region R1

            //if (grouping != null && grouping.Active)
            //{
            //    object obj=new object();
            //    IList<UIValidationRuleParam> parameters = validateRep.GetParameterRule(grouping.ID);
            //    #region Checking
            //    if (Utility.IsEmpty(parameters) || parameters.Count != 3)
            //    {
            //        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterCount, "تعداد پارامتر های قانون 1 با مقدار مورد انتظار نابرابر است ", ExceptionSrc);
            //    }
            //    UIValidationRuleParam param = parameters.Where(x => !Utility.IsEmpty(x.UIValidationRuleTempParam.KeyName) && x.UIValidationRuleTempParam.KeyName.Equals("LockCalculationFromCurrentMonth")).FirstOrDefault();
            //    UIValidationRuleParam paramMonth = parameters.Where(x => !Utility.IsEmpty(x.UIValidationRuleTempParam.KeyName) && x.UIValidationRuleTempParam.KeyName.Equals("LockCalculationFromMonth")).FirstOrDefault();
            //    if (param == null)
            //    {
            //        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, "پارامتر با کلید LockCalculationFromCurrentMonth  نظر برای قانون 1 یافت نشد ", ExceptionSrc);
            //    }
            //    if (paramMonth == null)
            //    {
            //        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, "پارامتر با کلید LockCalculationFromMonth  نظر برای قانون 1 یافت نشد ", ExceptionSrc);
            //    }
            //    UIValidationRuleParam SecParam = parameters.Where(x => !Utility.IsEmpty(x.UIValidationRuleTempParam.KeyName) && x.UIValidationRuleTempParam.KeyName.Equals("NumberOfDaysAfterLockDate")).FirstOrDefault();
            //    if (SecParam == null)
            //    {
            //        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, "پارامتر با کلید NumberOfDaysAfterLockDate  نظر برای قانون 1 یافت نشد ", ExceptionSrc);
            //    }
            //    #endregion

            //    int lockCalculationFromCurrentMonth = Utility.ToInteger(param.Value);
            //    int lockCalculationFromMonth = Utility.ToInteger(paramMonth.Value);
            //    PersianDateTime pd = null;
            //    DateTime ed = new DateTime();
            //    DateTime NowTime = DateTime.Now;
            //    int Month = 1;
            //    if (lockCalculationFromCurrentMonth == 1)
            //    {
            //        Month = NowTime.Month;
            //    }
            //    else
            //    {
            //        Month = NowTime.Month - 1;
            //    }
            //    if (Month <= 6 && lockCalculationFromMonth > 31 && lockCalculationFromMonth > 31)
            //    {
            //        lockCalculationFromMonth = 31;
            //    }
            //    else if (Month > 6 && Month <= 11 && lockCalculationFromMonth > 30)
            //    {
            //        lockCalculationFromMonth = 30;
            //    }
            //    else if (Month == 12 && lockCalculationFromMonth > 29)
            //    {
            //        lockCalculationFromMonth = 29;
            //    }
            //    DateTime changeDate = Utility.GTSMinStandardDateTime;
            //    Type classtype = obj.GetType();
            //    Person personObj = null;

            //    if (lockCalculationFromCurrentMonth == 1)
            //    {
            //        pd = Utility.ToPersianDateTime(DateTime.Now.Date);
            //        ed = DateTime.Now.Date;
            //        if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
            //        {
            //            int EndDay = Utility.GetEndOfPersianMonth(pd.Year, pd.Month);
            //            if (lockCalculationFromMonth > EndDay)
            //                lockDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", pd.Year, pd.Month, EndDay));
            //            else
            //                lockDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", pd.Year, pd.Month, lockCalculationFromMonth));
            //        }
            //        else
            //        {
            //            int EndDay = Utility.GetEndOfMiladiMonth(ed.Year, ed.Month);
            //            if (lockCalculationFromMonth > EndDay)
            //                lockDate = DateTime.Parse(String.Format("{0}/{1}/{2}", ed.Year, ed.Month, EndDay));
            //            else
            //                lockDate = DateTime.Parse(String.Format("{0}/{1}/{2}", ed.Year, ed.Month, lockCalculationFromMonth));
            //        }
            //    }
            //    else if (lockCalculationFromCurrentMonth == 0)
            //    {
            //        pd = Utility.ToPersianDateTime(DateTime.Now.Date);
            //        ed = DateTime.Now.Date;
            //        if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
            //        {
            //            if (pd.Month > 1)
            //            {
            //                int EndDay = Utility.GetEndOfPersianMonth(pd.Year, pd.Month - 1);
            //                if (lockCalculationFromMonth > EndDay)
            //                    lockDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", pd.Year, pd.Month - 1, EndDay));
            //                lockDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", pd.Year, pd.Month - 1, lockCalculationFromMonth));
            //            }
            //            else
            //            {
            //                int EndDay = Utility.GetEndOfPersianMonth(pd.Year - 1, 12);
            //                if (lockCalculationFromMonth > EndDay)
            //                    lockDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", pd.Year - 1, 12, EndDay));
            //                else
            //                    lockDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", pd.Year - 1, 12, lockCalculationFromMonth));
            //            }
            //        }
            //        else
            //        {
            //            if (ed.Month > 1)
            //            {
            //                int EndDay = Utility.GetEndOfMiladiMonth(ed.Year, ed.Month - 1);
            //                if (lockCalculationFromMonth > EndDay)
            //                    lockDate = DateTime.Parse(String.Format("{0}/{1}/{2}", ed.Year, ed.Month - 1, EndDay));
            //                else
            //                    lockDate = DateTime.Parse(String.Format("{0}/{1}/{2}", ed.Year, ed.Month - 1, lockCalculationFromMonth));
            //            }
            //            else
            //            {
            //                int EndDay = Utility.GetEndOfMiladiMonth(ed.Year - 1, 12);
            //                if (lockCalculationFromMonth > EndDay)
            //                    lockDate = DateTime.Parse(String.Format("{0}/{1}/{2}", ed.Year - 1, 12, EndDay));
            //                else
            //                    lockDate = DateTime.Parse(String.Format("{0}/{1}/{2}", ed.Year - 1, 12, lockCalculationFromMonth));
            //            }
            //        }
            //    }
            //    int NumberOfDaysAfterLockDate = Utility.ToInteger(SecParam.Value);
            //    DateTime PermitLockDate = lockDate.AddDays(NumberOfDaysAfterLockDate);
            //    if (IsPermit)
            //    {
            //        if (NowTime > PermitLockDate)
            //        //       if ((changeDate > Utility.GTSMinStandardDateTime) && (changeDate <= LockCalculationFromDate && PermitDate.Date > PermitLockDate))
            //        {
            //            AddException(ExceptionResourceKeys.UIValidation_R1_LockCalculationFromCurrentMonth, " محاسبات بسته شده است");
            //        }
            //    }
            //    else
            //    {
            //        if (DateTime.Now > lockDate && changeDate > Utility.GTSMinStandardDateTime && changeDate <= DateTime.Now.Date)
            //        {
            //            AddException(ExceptionResourceKeys.UIValidation_R1_LockCalculationFromCurrentMonth, " محاسبات بسته شده است");
            //        }
            //    }
            //}
            //#endregion

           // //R5 اگر روز جاری به پارامتر نرسیده بود باید دو ماه قبل را بدهد ولی اگر بزرگتر از 
            // //پارامتر بود باید ماه قبل را بدهد
            // else
            // {
            //     #region R2
            //     grouping = groupingList.Where(x => x.ValidationRule != null  && x.ValidationRule.CustomCode == 2.ToString()).ToList().FirstOrDefault();
            //     if (grouping != null && grouping.Active)
            //     {

           //         //IList<UIValidationRuleParam> parameters = validateRep.GetParameterRule(grouping.ValidationRule.CustomCode);
            ////         UIValidationRuleGroup RuleGroup = new UIValidationRuleGroup();
            //         UIValidationRuleGroup RuleGroup = groupingList.Where(x => x.ValidationRule != null && x.ValidationRule.CustomCode == 2.ToString()).ToList().FirstOrDefault();

           //         IList<UIValidationRuleParam> parameters = validateRep.GetParameterRule(RuleGroup.ID);
            //         #region cheking
            //         if (Utility.IsEmpty(parameters) || parameters.Count != 1)
            //         {
            //             throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterCount, "تعداد پارامتر های قانون 5 با مقدار مورد انتظار نابرابر است ", ExceptionSrc);
            //         }
            //         UIValidationRuleParam param = parameters.Where(x => !Utility.IsEmpty(x.UIValidationRuleTempParam.KeyName) && x.UIValidationRuleTempParam.KeyName.Equals("LockCalculationFromBeforeMonth")).FirstOrDefault();
            //         if (param == null)
            //         {
            //             throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, "پارامتر با کلید LockCalculationFromBeforeMonth  نظر برای قانون 5 یافت نشد ", ExceptionSrc);
            //         }
            //         #endregion

           //         int LockCalculationFromBeforeMonth = Utility.ToInteger(param.Value);
            //         PersianDateTime pd = Utility.ToPersianDateTime(DateTime.Now);
            //         if (LockCalculationFromBeforeMonth >= pd.Day)
            //         {
            //             if (pd.Month > 2)
            //             {
            //                 lockDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", pd.Year, pd.Month - 2, LockCalculationFromBeforeMonth));
            //             }
            //             else if (pd.Month == 2)
            //             {
            //                 lockDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", pd.Year - 1, 12, LockCalculationFromBeforeMonth));
            //             }
            //             else if (pd.Month == 1)
            //             {
            //                 lockDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", pd.Year - 1, 11, LockCalculationFromBeforeMonth));
            //             }
            //         }
            //         else
            //         {
            //             if (pd.Month > 1)
            //             {
            //                 lockDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", pd.Year, pd.Month - 1, LockCalculationFromBeforeMonth));
            //             }
            //             else
            //             {
            //                 lockDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", pd.Year - 1, 12, LockCalculationFromBeforeMonth));
            //             }
            //         }
            //     }
            //     #endregion

            #region R3
            else
            {
                grouping = groupingList.Where(x => x.ValidationRule != null && x.ValidationRule.CustomCode == 3.ToString()).ToList().FirstOrDefault();
                if (grouping != null && grouping.Active)
                {
                    //IList<UIValidationRuleParam> parameters = validateRep.GetParameterRule(grouping.ValidationRule.CustomCode);

                    //  UIValidationRuleGroup RuleGroup = new UIValidationRuleGroup();
                    UIValidationRuleGroup RuleGroup = groupingList.Where(x => x.ValidationRule != null && x.ValidationRule.CustomCode == 3.ToString()).ToList().FirstOrDefault();
                    IList<UIValidationRuleParam> parameters = validateRep.GetParameterRule(RuleGroup.ID);

                    #region cheking
                    if (Utility.IsEmpty(parameters) || parameters.Count != 2)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterCount, "تعداد پارامتر های قانون 3 با مقدار مورد انتظار نابرابر است ", ExceptionSrc);
                    }
                    UIValidationRuleParam param = parameters.Where(x => !Utility.IsEmpty(x.UIValidationRuleTempParam.KeyName) && x.UIValidationRuleTempParam.KeyName.Equals("LockCalculationFromDate")).FirstOrDefault();
                    if (param == null)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, String.Format("پارامتر با کلید LockCalculationFromDate    برای قانون 3 یافت نشد، "), ExceptionSrc);
                    }
                    UIValidationRuleParam SecParam = parameters.Where(x => !Utility.IsEmpty(x.UIValidationRuleTempParam.KeyName) && x.UIValidationRuleTempParam.KeyName.Equals("NumberOfDaysAfterLockDate")).FirstOrDefault();
                    if (SecParam == null)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, "پارامتر با کلید  - تاریخ بستن محاسبات NumberOfDaysAfterLockDate  نظر برای قانون 3 یافت نشد ", ExceptionSrc);
                    }
                    #endregion

                    DateTime LockCalculationFromDate = new DateTime();
                    if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                    {
                        LockCalculationFromDate = Utility.ToMildiDateTime(param.Value);
                    }
                    else
                    {
                        LockCalculationFromDate = Utility.ToMildiDateTime(param.Value);
                    }
                    int NumDays = Utility.ToInteger(SecParam.Value);
                    DateTime PermitLockDate = LockCalculationFromDate.AddDays(NumDays);
                    if (IsPermit)
                    {
                        lockDate = PermitLockDate;
                    }
                    else
                    {
                        lockDate = LockCalculationFromDate;
                    }
                }
            }
            #endregion

            if (lockDate == Utility.GTSMinStandardDateTime)
            {
                throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationR3MustBeActive, "قانون <محاسبات تا تاریخ _ بسته شود> باید فعال باشد", ExceptionSrc);
            }
            return lockDate;
        }
        public void DoValidate(object obj)
        {
            Request request = new Request();
            try
            {
                decimal personId = 0;

                Type classtype = obj.GetType();
                /*1*/
                if (classtype == typeof(GTS.Clock.Model.Concepts.Permit))
                {
                    personId = ((GTS.Clock.Model.Concepts.Permit)obj).Person.ID;
                }
                /*2*/else if (classtype == typeof(GTS.Clock.Model.Concepts.ShiftException))
                {
                    personId = (((GTS.Clock.Model.Concepts.ShiftException)obj).Person.ID);
                }
                /*3*/else if (classtype == typeof(GTS.Clock.Model.Concepts.Budget))
                {
                    personId = 0;//(((GTS.Clock.Model.Concepts.Budget)obj).RuleCategory.);
                }
                /*4*/else if (classtype == typeof(GTS.Clock.Model.Concepts.LeaveIncDec))
                {
                    personId = (((GTS.Clock.Model.Concepts.LeaveIncDec)obj).Person.ID);
                }
                /*5*/else if (classtype == typeof(GTS.Clock.Model.Concepts.LeaveYearRemain))
                {
                    personId = (((GTS.Clock.Model.Concepts.LeaveYearRemain)obj).Person.ID);
                }
                /*6*/else if (classtype == typeof(GTS.Clock.Model.Concepts.BasicTraffic))
                {
                    personId = (((GTS.Clock.Model.Concepts.BasicTraffic)obj).Person.ID);
                }
                /*7*/else if (classtype == typeof(GTS.Clock.Model.RequestFlow.Request))
                {
                    personId = (((GTS.Clock.Model.RequestFlow.Request)obj).Person.ID);
                }
                /*8*/else if (classtype == typeof(GTS.Clock.Business.Engine.BEngineCalculator))
                {
                    BEngineCalculator Calculator = (BEngineCalculator)obj;
                    personId = (((GTS.Clock.Business.Engine.BEngineCalculator)Calculator).PersonID);
                }
                else
                {
                    personId = BUser.CurrentUser.Person.ID;
                }

                IList<UIValidationRuleGroup> groupingList = new List<UIValidationRuleGroup>();
                groupingList = validateRep.GetByPersonId(personId);
                //IList<UIValidationRule> ruleList = new EntityRepository<UIValidationRule>().Find(x => x.SubsystemID == 1).ToList();
                bool r3MustRun = false;

                //#region R1
                //UIValidationRuleGroup grouping = groupingList.Where(x => x.ValidationRule != null && x.ValidationRule.CustomCode == 1.ToString()).ToList().FirstOrDefault();
                //if (grouping != null && grouping.Active)
                //{
                //    //      if ( personId == BUser.CurrentUser.Person.ID)
                //    if (IsCurrentUserManager)
                //    {
                //        //  r6MustRun = false;
                //        R1_LockCalculationFromCurrentMonth(obj, grouping, request, true);
                //    }
                //}
                //#endregion

                //  #region R2
                //  grouping = groupingList.Where(x => x.ValidationRule != null  && x.ValidationRule.CustomCode == 2.ToString()).ToList().FirstOrDefault();
                //  if (grouping != null && grouping.Active)
                //  {
                //      if (personId == BUser.CurrentUser.Person.ID)
                //      {
                //          r6MustRun = false;
                ////          R5_LockCalculationFromBeforeMonth(obj, grouping);
                //      }
                //  }
                //  #endregion

                #region R3
                //UIValidationRuleGroup grouping = groupingList.Where(x => x.ValidationRule != null && x.ValidationRule.CustomCode == 1.ToString()).ToList().FirstOrDefault();
                UIValidationRuleGroup grouping = groupingList.Where(x => x.ValidationRule != null && x.ValidationRule.CustomCode == 3.ToString()).ToList().FirstOrDefault();
                if (grouping != null && grouping.Active)
                {
                    UIValidationRuleGroupPrecard ruleGroupPrecard = this.GetRuleGroupPrecard(grouping, request);
                    //    if ( personId == BUser.CurrentUser.Person.ID) 
                    Decimal UserRole = BUser.CurrentUser.Role.ID;
                    if ((IsCurrentUserManager && request.IsEdited) || ruleGroupPrecard.Operator)
                    {
                        r3MustRun = true;
                        R3_LockCalculationFromDate(obj, grouping, request, true);
                    }
                }
                else if (!r3MustRun)
                {
                    throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationR3MustBeActive, "قانون <محاسبات تا تاریخ _ بسته شود> باید فعال باشد", ExceptionSrc);
                }
                #endregion

                //DNN Note:
                #region R203

                if (classtype == typeof(GTS.Clock.Model.RequestFlow.Request))
                {
                    personId = BUser.CurrentUser.Person.ID;
                    groupingList = validateRep.GetByPersonId(personId);

                    request = ((GTS.Clock.Model.RequestFlow.Request)obj);

                    grouping = groupingList.Where(x => x.ValidationRule != null && x.ValidationRule.CustomCode == 203.ToString()).ToList().FirstOrDefault();
                    if (grouping != null && grouping.Active)
                    {
                        UIValidationRuleGroupPrecard ruleGroupPrecard = this.GetRuleGroupPrecard(grouping, request);
                        //if ((IsCurrentUserManager && request.IsEdited) || ruleGroupPrecard.Operator)
                        if ((IsCurrentUserManager) || ruleGroupPrecard.Operator)
                        {
                            if (grouping.ValidationRule.Type == UIValidationRuleType.RulePrecard)
                            {
                                R203_RequestConfirmedSchedule(obj, grouping, request, true);
                            }
                        }
                    }
                }
                #endregion

                //END of DNN Note

                if (exception.Count > 0)
                    throw exception;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "RequestValidator", "DoValidate");
                throw ex;
            }
        }

        public void DoValidate(decimal personId)
        {
            IList<UIValidationRuleGroup> groupingList = new List<UIValidationRuleGroup>();
            groupingList = validateRep.GetByPersonId(personId);
            EntityRepository<UIValidationRule> rulerep = new EntityRepository<UIValidationRule>(false);
            //IList<UIValidationRule> ruleList = rulerep.Find(x => x.SubsystemID == 1).ToList();

            #region R39
            UIValidationRuleGroup grouping = groupingList.Where(x => x.ValidationRule != null && x.ValidationRule.CustomCode == 27.ToString()).ToList().FirstOrDefault();
            if (grouping != null)
            {
                R27_ArchiveCalculationKaheshi(grouping.Active);
            }
            if (exception.Count > 0)
                throw exception;
            #endregion
        }
        public UIValidationRuleGroupPrecard GetRuleGroupPrecard(UIValidationRuleGroup grouping, Request request)
        {
            IList<UIValidationRuleGroupPrecard> ruleGroupPrecardList = new List<UIValidationRuleGroupPrecard>();
            ruleGroupPrecardList = NHSession.QueryOver<UIValidationRuleGroupPrecard>().List<UIValidationRuleGroupPrecard>();
            UIValidationRuleGroupPrecard ruleGroupPrecard = new UIValidationRuleGroupPrecard();
            if (grouping.ValidationRule.Type == UIValidationRuleType.RulePrecardParameter || grouping.ValidationRule.Type == UIValidationRuleType.RulePrecard)
                ruleGroupPrecard = ruleGroupPrecardList.Where(x => x.UIValidationRuleGroup.ID == grouping.ID && x.Precard.ID == request.Precard.ID).ToList().FirstOrDefault();
            else
            {
                if (grouping.ValidationRule.Type == UIValidationRuleType.RuleParameter || grouping.ValidationRule.Type == UIValidationRuleType.Rule)
                    ruleGroupPrecard = ruleGroupPrecardList.Where(x => x.UIValidationRuleGroup.ID == grouping.ID && x.Precard == null).ToList().FirstOrDefault();
            }
            return ruleGroupPrecard;
        }
        public void DoValidate(Request request)
        {
            try
            {
                decimal personId = request.Person.ID;
                IList<UIValidationRuleGroup> RulegroupList = new List<UIValidationRuleGroup>();
                RulegroupList = validateRep.GetByPersonId(personId);
                #region R1
                UIValidationRuleGroup grouping = RulegroupList.Where(x => x.ValidationRule != null && x.ValidationRule.CustomCode == 1.ToString()).ToList().FirstOrDefault();
                if (grouping != null && grouping.Active)
                {
                    UIValidationRuleGroupPrecard ruleGroupPrecard = this.GetRuleGroupPrecard(grouping, request);

                    if (ruleGroupPrecard != null && ((ruleGroupPrecard.Operator || personId == BUser.CurrentUser.Person.ID) || (request.IsEdited && IsCurrentUserManager) || request.IsFromService))
                    {
                        if (grouping.ValidationRule.Type == UIValidationRuleType.RuleParameter)
                        {
                            R1_LockCalculationFromCurrentMonth(request, grouping, request, false);
                        }
                    }
                }
                #endregion


                #region R2
                grouping = RulegroupList.Where(x => x.ValidationRule != null && x.ValidationRule.CustomCode == 30.ToString()).ToList().FirstOrDefault();
                if (grouping != null && grouping.Active)
                {
                    UIValidationRuleGroupPrecard ruleGroupPrecard = this.GetRuleGroupPrecard(grouping, request);
                    if (ruleGroupPrecard != null && ((ruleGroupPrecard.Operator || personId == BUser.CurrentUser.Person.ID) || (request.IsEdited && IsCurrentUserManager) || request.IsFromService))
                    {
                        if (grouping.ValidationRule.Type == UIValidationRuleType.RuleParameter)
                        {
                            R2_LockCalculationFromCalculationMonth(request, grouping, request);
                        }
                    }
                }

                #endregion
                #region R3
                grouping = RulegroupList.Where(x => x.ValidationRule != null && x.ValidationRule.CustomCode == 3.ToString()).ToList().FirstOrDefault();
                if (grouping != null && grouping.Active)
                {
                    UIValidationRuleGroupPrecard ruleGroupPrecard = this.GetRuleGroupPrecard(grouping, request);
                    if (ruleGroupPrecard != null && (ruleGroupPrecard.Operator || personId == BUser.CurrentUser.Person.ID || (request.IsEdited && IsCurrentUserManager) || request.IsFromService))
                    {
                        if (grouping.ValidationRule.Type == UIValidationRuleType.RuleParameter)
                        {
                            R3_LockCalculationFromDate(request, grouping, request, false);
                        }
                    }
                }

                #endregion
                #region R4
                grouping = RulegroupList.Where(x => x.ValidationRule != null && x.ValidationRule.CustomCode == 4.ToString()).ToList().FirstOrDefault();
                if (grouping != null && grouping.Active)
                {
                    UIValidationRuleGroupPrecard ruleGroupPrecard = this.GetRuleGroupPrecard(grouping, request);
                    if (ruleGroupPrecard != null && ((ruleGroupPrecard.Operator || personId == BUser.CurrentUser.Person.ID) || (request.IsEdited && IsCurrentUserManager) || request.IsFromService))
                    {
                        if (grouping.ValidationRule.Type == UIValidationRuleType.RulePrecardParameter)
                        {
                            R4_MaxTarfficRequest(request, grouping);
                        }
                    }
                }

                #endregion
                #region R5
                grouping = RulegroupList.Where(x => x.ValidationRule != null && x.ValidationRule.CustomCode == 5.ToString()).ToList().FirstOrDefault();
                if (grouping != null && grouping.Active)
                {
                    UIValidationRuleGroupPrecard ruleGroupPrecard = this.GetRuleGroupPrecard(grouping, request);
                    if (ruleGroupPrecard != null && ((ruleGroupPrecard.Operator || personId == BUser.CurrentUser.Person.ID) || (request.IsEdited && IsCurrentUserManager) || request.IsFromService))
                    {
                        if (grouping.ValidationRule.Type == UIValidationRuleType.RulePrecardParameter)
                        {
                            R5_HourlyTrafficRequest(request, grouping);
                        }
                    }
                }
                #endregion
                #region R6
                grouping = RulegroupList.Where(x => x.ValidationRule != null && x.ValidationRule.CustomCode == 6.ToString()).ToList().FirstOrDefault();
                if (grouping != null && grouping.Active)
                {
                    UIValidationRuleGroupPrecard ruleGroupPrecard = this.GetRuleGroupPrecard(grouping, request);
                    if (ruleGroupPrecard != null && ((ruleGroupPrecard.Operator || personId == BUser.CurrentUser.Person.ID) || (request.IsEdited && IsCurrentUserManager) || request.IsFromService))
                    {
                        if (grouping.ValidationRule.Type == UIValidationRuleType.RulePrecardParameter)
                        {
                            R6_RequestMaxAvvalVaght(request, grouping);
                        }
                    }
                }
                #endregion
                #region R7
                grouping = RulegroupList.Where(x => x.ValidationRule != null && x.ValidationRule.CustomCode == 7.ToString()).ToList().FirstOrDefault();
                if (grouping != null && grouping.Active)
                {
                    UIValidationRuleGroupPrecard ruleGroupPrecard = this.GetRuleGroupPrecard(grouping, request);
                    if (ruleGroupPrecard != null && ((ruleGroupPrecard.Operator || personId == BUser.CurrentUser.Person.ID) || (request.IsEdited && IsCurrentUserManager) || request.IsFromService))
                    {
                        if (grouping.ValidationRule.Type == UIValidationRuleType.RulePrecardParameter)
                        {
                            R7_MaxValueOfRequest(request, grouping);
                        }
                    }
                }
                #endregion
                #region R8
                grouping = RulegroupList.Where(x => x.ValidationRule != null && x.ValidationRule.CustomCode == 8.ToString()).ToList().FirstOrDefault();
                if (grouping != null && grouping.Active)
                {
                    UIValidationRuleGroupPrecard ruleGroupPrecard = this.GetRuleGroupPrecard(grouping, request);
                    if (ruleGroupPrecard != null && ((ruleGroupPrecard.Operator || personId == BUser.CurrentUser.Person.ID) || (request.IsEdited && IsCurrentUserManager) || request.IsFromService))
                    {
                        if (grouping.ValidationRule.Type == UIValidationRuleType.RulePrecardParameter)
                        {
                            R8_OverlyHourlyPrecard(request, grouping);
                        }
                    }
                }
                #endregion
                #region R9
                grouping = RulegroupList.Where(x => x.ValidationRule != null && x.ValidationRule.CustomCode == 9.ToString()).ToList().FirstOrDefault();
                if (grouping != null && grouping.Active)
                {
                    UIValidationRuleGroupPrecard ruleGroupPrecard = this.GetRuleGroupPrecard(grouping, request);
                    if (ruleGroupPrecard != null && ((ruleGroupPrecard.Operator || personId == BUser.CurrentUser.Person.ID) || (request.IsEdited && IsCurrentUserManager) || request.IsFromService))
                    {
                        if (grouping.ValidationRule.Type == UIValidationRuleType.RulePrecardParameter)
                        {
                            R9_OverlyDailyPrecard(request, grouping);
                        }
                    }
                }
                #endregion
                #region R10
                grouping = RulegroupList.Where(x => x.ValidationRule != null && x.ValidationRule.CustomCode == 10.ToString()).ToList().FirstOrDefault();
                if (grouping != null && grouping.Active)
                {
                    UIValidationRuleGroupPrecard ruleGroupPrecard = this.GetRuleGroupPrecard(grouping, request);
                    if (ruleGroupPrecard != null && ((ruleGroupPrecard.Operator || personId == BUser.CurrentUser.Person.ID) || (request.IsEdited && IsCurrentUserManager) || request.IsFromService))
                    {
                        if (grouping.ValidationRule.Type == UIValidationRuleType.RulePrecardParameter)
                        {
                            R10_SaveRequestwithTimeRange(request, grouping);
                        }
                    }
                }
                #endregion
                #region R11
                grouping = RulegroupList.Where(x => x.ValidationRule != null && x.ValidationRule.CustomCode == 11.ToString()).ToList().FirstOrDefault();
                if (grouping != null && grouping.Active)
                {
                    UIValidationRuleGroupPrecard ruleGroupPrecard = this.GetRuleGroupPrecard(grouping, request);
                    if (ruleGroupPrecard != null && ((ruleGroupPrecard.Operator || personId == BUser.CurrentUser.Person.ID) || (request.IsEdited && IsCurrentUserManager) || request.IsFromService))
                    {
                        if (grouping.ValidationRule.Type == UIValidationRuleType.RulePrecardParameter)
                        {
                            R11_RequestMinLength(request, grouping);
                        }
                    }
                }
                #endregion
                #region R12
                grouping = RulegroupList.Where(x => x.ValidationRule != null && x.ValidationRule.CustomCode == 12.ToString()).ToList().FirstOrDefault();
                if (grouping != null && grouping.Active)
                {
                    UIValidationRuleGroupPrecard ruleGroupPrecard = this.GetRuleGroupPrecard(grouping, request);
                    if (ruleGroupPrecard != null && ((ruleGroupPrecard.Operator || personId == BUser.CurrentUser.Person.ID) || (request.IsEdited && IsCurrentUserManager) || request.IsFromService))
                    {
                        if (grouping.ValidationRule.Type == UIValidationRuleType.RulePrecard)
                        {
                            R12_RequestDescriptionRequierd(request, grouping);
                        }
                    }
                }
                #endregion
                #region R13
                grouping = RulegroupList.Where(x => x.ValidationRule != null && x.ValidationRule.CustomCode == 13.ToString()).ToList().FirstOrDefault();
                if (grouping != null && grouping.Active)
                {
                    UIValidationRuleGroupPrecard ruleGroupPrecard = this.GetRuleGroupPrecard(grouping, request);
                    if (ruleGroupPrecard != null && ((ruleGroupPrecard.Operator || personId == BUser.CurrentUser.Person.ID) || (request.IsEdited && IsCurrentUserManager) || request.IsFromService))
                    {
                        if (grouping.ValidationRule.Type == UIValidationRuleType.RulePrecard)
                        {
                            R13_IllenssRequest(request, grouping);
                        }
                    }
                }
                #endregion
                #region R14
                grouping = RulegroupList.Where(x => x.ValidationRule != null && x.ValidationRule.CustomCode == 14.ToString()).ToList().FirstOrDefault();
                if (grouping != null && grouping.Active)
                {
                    UIValidationRuleGroupPrecard ruleGroupPrecard = this.GetRuleGroupPrecard(grouping, request);
                    if (ruleGroupPrecard != null && ((ruleGroupPrecard.Operator || personId == BUser.CurrentUser.Person.ID) || (request.IsEdited && IsCurrentUserManager) || request.IsFromService))
                    {
                        if (grouping.ValidationRule.Type == UIValidationRuleType.RulePrecard)
                        {
                            R14_DoctorRequest(request, grouping);
                        }
                    }
                }
                #endregion
                #region R15
                grouping = RulegroupList.Where(x => x.ValidationRule != null && x.ValidationRule.CustomCode == 15.ToString()).ToList().FirstOrDefault();
                if (grouping != null && grouping.Active)
                {
                    UIValidationRuleGroupPrecard ruleGroupPrecard = this.GetRuleGroupPrecard(grouping, request);
                    if (ruleGroupPrecard != null && ((ruleGroupPrecard.Operator || personId == BUser.CurrentUser.Person.ID) || (request.IsEdited && IsCurrentUserManager) || request.IsFromService))
                    {
                        if (grouping.ValidationRule.Type == UIValidationRuleType.RulePrecard)
                        {
                            R15_DutyPlaceRequest(request, grouping);
                        }
                    }
                }
                #endregion
                #region R16
                grouping = RulegroupList.Where(x => x.ValidationRule != null && x.ValidationRule.CustomCode == 16.ToString()).ToList().FirstOrDefault();
                if (grouping != null && grouping.Active)
                {
                    UIValidationRuleGroupPrecard ruleGroupPrecard = this.GetRuleGroupPrecard(grouping, request);
                    if (ruleGroupPrecard != null && ((ruleGroupPrecard.Operator || personId == BUser.CurrentUser.Person.ID) || (request.IsEdited && IsCurrentUserManager) || request.IsFromService))
                    {
                        if (grouping.ValidationRule.Type == UIValidationRuleType.RulePrecardParameter)
                        {
                            R16_DailyTrafficRequest(request, grouping);
                        }
                    }
                }
                #endregion
                #region R17
                grouping = RulegroupList.Where(x => x.ValidationRule != null && x.ValidationRule.CustomCode == 17.ToString()).ToList().FirstOrDefault();
                if (grouping != null && grouping.Active)
                {
                    UIValidationRuleGroupPrecard ruleGroupPrecard = this.GetRuleGroupPrecard(grouping, request);
                    if (ruleGroupPrecard != null && ((ruleGroupPrecard.Operator || personId == BUser.CurrentUser.Person.ID) || (request.IsEdited && IsCurrentUserManager) || request.IsFromService))
                    {
                        if (grouping.ValidationRule.Type == UIValidationRuleType.RulePrecardParameter)
                        {
                            R17_MaxAmountHourlyRequest(request, grouping);
                        }
                    }
                }
                #endregion
                #region R18
                grouping = RulegroupList.Where(x => x.ValidationRule != null && x.ValidationRule.CustomCode == 18.ToString()).ToList().FirstOrDefault();
                if (grouping != null && grouping.Active)
                {
                    UIValidationRuleGroupPrecard ruleGroupPrecard = this.GetRuleGroupPrecard(grouping, request);
                    if (ruleGroupPrecard != null && ((ruleGroupPrecard.Operator || personId == BUser.CurrentUser.Person.ID) || (request.IsEdited && IsCurrentUserManager) || request.IsFromService))
                    {
                        if (grouping.ValidationRule.Type == UIValidationRuleType.RulePrecardParameter)
                        {
                            R18_MaxDailyRequestInYear(request, grouping);
                        }
                    }
                }
                #endregion
                #region R19
                grouping = RulegroupList.Where(x => x.ValidationRule != null && x.ValidationRule.CustomCode == 19.ToString()).ToList().FirstOrDefault();
                if (grouping != null && grouping.Active)
                {
                    UIValidationRuleGroupPrecard ruleGroupPrecard = this.GetRuleGroupPrecard(grouping, request);
                    if (ruleGroupPrecard != null && ((ruleGroupPrecard.Operator || personId == BUser.CurrentUser.Person.ID) || (request.IsEdited && IsCurrentUserManager) || request.IsFromService))
                    {
                        if (grouping.ValidationRule.Type == UIValidationRuleType.RulePrecardParameter)
                        {
                            R19_MaxDailyRequestCountInMonth(request, grouping);
                        }
                    }
                }
                #endregion
                #region R20
                grouping = RulegroupList.Where(x => x.ValidationRule != null && x.ValidationRule.CustomCode == 20.ToString()).ToList().FirstOrDefault();
                if (grouping != null && grouping.Active)
                {
                    UIValidationRuleGroupPrecard ruleGroupPrecard = this.GetRuleGroupPrecard(grouping, request);
                    if (ruleGroupPrecard != null && ((ruleGroupPrecard.Operator || personId == BUser.CurrentUser.Person.ID) || (request.IsEdited && IsCurrentUserManager) || request.IsFromService))
                    {
                        if (grouping.ValidationRule.Type == UIValidationRuleType.RulePrecardParameter)
                        {
                            R20_MaxHourlyRequestCountInDay(request, grouping);
                        }
                    }
                }
                #endregion
                #region R21
                grouping = RulegroupList.Where(x => x.ValidationRule != null && x.ValidationRule.CustomCode == 21.ToString()).ToList().FirstOrDefault();
                if (grouping != null && grouping.Active)
                {
                    UIValidationRuleGroupPrecard ruleGroupPrecard = this.GetRuleGroupPrecard(grouping, request);
                    //if (ruleGroupPrecard != null && ((ruleGroupPrecard.Operator || personId == BUser.CurrentUser.Person.ID) || (request.IsEdited && IsCurrentUserManager) || request.IsFromService))
                    if (ruleGroupPrecard != null && ((ruleGroupPrecard.Operator && personId != BUser.CurrentUser.Person.ID) || (request.IsEdited && IsCurrentUserManager) || request.IsFromService))
                    {
                        if (grouping.ValidationRule.Type == UIValidationRuleType.RulePrecardParameter)
                        {
                            R21_OperatorRequestMaxCount(request, grouping);
                        }
                    }
                }
                #endregion
                #region R22
                grouping = RulegroupList.Where(x => x.ValidationRule != null && x.ValidationRule.CustomCode == 22.ToString()).ToList().FirstOrDefault();
                if (grouping != null && grouping.Active)
                {
                    UIValidationRuleGroupPrecard ruleGroupPrecard = this.GetRuleGroupPrecard(grouping, request);
                    if (ruleGroupPrecard != null && ((ruleGroupPrecard.Operator || personId == BUser.CurrentUser.Person.ID) || (request.IsEdited && IsCurrentUserManager) || request.IsFromService))
                    {
                        if (grouping.ValidationRule.Type == UIValidationRuleType.Rule)
                        {
                            R22_RequestRemainLeaveToEndMonth(request, grouping);
                        }
                    }
                }
                #endregion
                #region R23
                grouping = RulegroupList.Where(x => x.ValidationRule != null && x.ValidationRule.CustomCode == 23.ToString()).ToList().FirstOrDefault();
                if (grouping != null && grouping.Active)
                {
                    UIValidationRuleGroupPrecard ruleGroupPrecard = this.GetRuleGroupPrecard(grouping, request);
                    if (ruleGroupPrecard != null && ((ruleGroupPrecard.Operator || personId == BUser.CurrentUser.Person.ID) || (request.IsEdited && IsCurrentUserManager) || request.IsFromService))
                    {
                        if (grouping.ValidationRule.Type == UIValidationRuleType.RulePrecardParameter)
                        {
                            R23_SaveDailyRequestwithTimeRange(request, grouping);
                        }
                    }
                }
                #endregion
                #region R24
                grouping = RulegroupList.Where(x => x.ValidationRule != null && x.ValidationRule.CustomCode == 24.ToString()).ToList().FirstOrDefault();
                if (grouping != null && grouping.Active)
                {
                    UIValidationRuleGroupPrecard ruleGroupPrecard = this.GetRuleGroupPrecard(grouping, request);
                    if (ruleGroupPrecard != null && ((ruleGroupPrecard.Operator || personId == BUser.CurrentUser.Person.ID) || (request.IsEdited && IsCurrentUserManager) || request.IsFromService))
                    {
                        if (grouping.ValidationRule.Type == UIValidationRuleType.RulePrecardParameter)
                        {
                            R24_DailyRequestInWeekdays(request, grouping);
                        }
                    }
                }
                #endregion
                #region R25
                grouping = RulegroupList.Where(x => x.ValidationRule != null && x.ValidationRule.CustomCode == 25.ToString()).ToList().FirstOrDefault();
                if (grouping != null && grouping.Active)
                {
                    UIValidationRuleGroupPrecard ruleGroupPrecard = this.GetRuleGroupPrecard(grouping, request);
                    if (ruleGroupPrecard != null && ((ruleGroupPrecard.Operator || personId == BUser.CurrentUser.Person.ID) || (request.IsEdited && IsCurrentUserManager) || request.IsFromService))
                    {
                        if (grouping.ValidationRule.Type == UIValidationRuleType.RulePrecardParameter)
                        {
                            R25_HourlyRequestInWeekdays(request, grouping);
                        }
                    }
                }
                #endregion
                #region R26
                grouping = RulegroupList.Where(x => x.ValidationRule != null && x.ValidationRule.CustomCode == 26.ToString()).ToList().FirstOrDefault();
                if (grouping != null && grouping.Active)
                {
                    UIValidationRuleGroupPrecard ruleGroupPrecard = this.GetRuleGroupPrecard(grouping, request);
                    if (ruleGroupPrecard != null && ((ruleGroupPrecard.Operator || personId == BUser.CurrentUser.Person.ID) || (request.IsEdited && IsCurrentUserManager) || request.IsFromService))
                    {
                        if (grouping.ValidationRule.Type == UIValidationRuleType.RulePrecardParameter)
                        {
                            R26_SumOfPrecads(request, grouping);
                        }
                    }
                }
                #endregion

                #region R28
                grouping = RulegroupList.Where(x => x.ValidationRule != null && x.ValidationRule.CustomCode == 28.ToString()).ToList().FirstOrDefault();
                if (grouping != null && grouping.Active)
                {
                    UIValidationRuleGroupPrecard ruleGroupPrecard = this.GetRuleGroupPrecard(grouping, request);
                    if (ruleGroupPrecard != null && ((ruleGroupPrecard.Operator || personId == BUser.CurrentUser.Person.ID) || (request.IsEdited && IsCurrentUserManager) || request.IsFromService))
                    {
                        if (grouping.ValidationRule.Type == UIValidationRuleType.RulePrecardParameter)
                        {
                            R28_MaxAmountHourlyRequestForMonthlyPrecard(request, grouping);
                        }
                    }
                }
                #endregion
                #region R29
                grouping = RulegroupList.Where(x => x.ValidationRule != null && x.ValidationRule.CustomCode == 29.ToString()).ToList().FirstOrDefault();
                if (grouping != null && grouping.Active)
                {
                    UIValidationRuleGroupPrecard ruleGroupPrecard = this.GetRuleGroupPrecard(grouping, request);
                    if (ruleGroupPrecard != null && ((ruleGroupPrecard.Operator || personId == BUser.CurrentUser.Person.ID) || (request.IsEdited && IsCurrentUserManager) || request.IsFromService))
                    {
                        if (grouping.ValidationRule.Type == UIValidationRuleType.RulePrecard)
                        {
                            R29_RequestMaxArrivalExit(request, grouping);
                        }
                    }
                }
                #endregion
                #region R31
                grouping = RulegroupList.Where(x => x.ValidationRule != null && x.ValidationRule.CustomCode == 31.ToString()).ToList().FirstOrDefault();
                if (grouping != null && grouping.Active)
                {
                    UIValidationRuleGroupPrecard ruleGroupPrecard = this.GetRuleGroupPrecard(grouping, request);
                    if (ruleGroupPrecard != null && ((ruleGroupPrecard.Operator || personId == BUser.CurrentUser.Person.ID) || (request.IsEdited && IsCurrentUserManager) || request.IsFromService))
                    {
                        if (grouping.ValidationRule.Type == UIValidationRuleType.RulePrecardParameter)
                        {
                            R31_OverlapDailyHourlyPrecard(request, grouping);
                        }
                    }
                }
                #endregion
                #region R32
                grouping = RulegroupList.Where(x => x.ValidationRule != null && x.ValidationRule.CustomCode == 32.ToString()).ToList().FirstOrDefault();
                if (grouping != null && grouping.Active)
                {
                    UIValidationRuleGroupPrecard ruleGroupPrecard = this.GetRuleGroupPrecard(grouping, request);
                    if (ruleGroupPrecard != null && ((ruleGroupPrecard.Operator || personId == BUser.CurrentUser.Person.ID) || (request.IsEdited && IsCurrentUserManager) || request.IsFromService))
                    {
                        if (grouping.ValidationRule.Type == UIValidationRuleType.RulePrecardParameter)
                        {
                            R32_AmountInDay(request, grouping);
                        }
                    }
                }
                #endregion
                #region R33
                grouping = RulegroupList.Where(x => x.ValidationRule != null && x.ValidationRule.CustomCode == 33.ToString()).ToList().FirstOrDefault();
                if (grouping != null && grouping.Active)
                {
                    UIValidationRuleGroupPrecard ruleGroupPrecard = this.GetRuleGroupPrecard(grouping, request);
                    if (ruleGroupPrecard != null && ((ruleGroupPrecard.Operator || personId == BUser.CurrentUser.Person.ID) || (request.IsEdited && IsCurrentUserManager) || request.IsFromService))
                    {
                        if (grouping.ValidationRule.Type == UIValidationRuleType.RulePrecard)
                        {
                            R33_SubstituteRequest(request, grouping);
                        }
                    }
                }
                #endregion
                #region R34
                grouping = RulegroupList.Where(x => x.ValidationRule != null && x.ValidationRule.CustomCode == 34.ToString()).ToList().FirstOrDefault();
                if (grouping != null && grouping.Active)
                {
                    UIValidationRuleGroupPrecard ruleGroupPrecard = this.GetRuleGroupPrecard(grouping, request);
                    if (ruleGroupPrecard != null && ((ruleGroupPrecard.Operator || personId == BUser.CurrentUser.Person.ID) || (request.IsEdited && IsCurrentUserManager) || request.IsFromService))
                    {
                        if (grouping.ValidationRule.Type == UIValidationRuleType.RulePrecard)
                        {
                            R34_WithOutPayLeaveInRemainLeave(request, grouping);
                        }
                    }
                }
                #endregion
                #region R35
                grouping = RulegroupList.Where(x => x.ValidationRule != null && x.ValidationRule.CustomCode == 35.ToString()).ToList().FirstOrDefault();
                if (grouping != null && grouping.Active)
                {
                    UIValidationRuleGroupPrecard ruleGroupPrecard = this.GetRuleGroupPrecard(grouping, request);
                    if (ruleGroupPrecard != null && ((ruleGroupPrecard.Operator || personId == BUser.CurrentUser.Person.ID) || (request.IsEdited && IsCurrentUserManager) || request.IsFromService))
                    {
                        if (grouping.ValidationRule.Type == UIValidationRuleType.RulePrecardParameter)
                        {
                            R35_InWayLeave(request, grouping);
                        }
                    }
                }
                #endregion

                #region R36
                grouping = RulegroupList.Where(x => x.ValidationRule != null && x.ValidationRule.CustomCode == 36.ToString()).ToList().FirstOrDefault();
                if (grouping != null && grouping.Active)
                {
                    UIValidationRuleGroupPrecard ruleGroupPrecard = this.GetRuleGroupPrecard(grouping, request);
                    if (ruleGroupPrecard != null && ((ruleGroupPrecard.Operator || personId == BUser.CurrentUser.Person.ID) || (request.IsEdited && IsCurrentUserManager) || request.IsFromService))
                    {
                        if (grouping.ValidationRule.Type == UIValidationRuleType.RulePrecardParameter)
                        {
                            R36_GivingBirthLeave(request, grouping);
                        }
                    }
                }

                #endregion
                #region R37
                grouping = RulegroupList.Where(x => x.ValidationRule != null && x.ValidationRule.CustomCode == 37.ToString()).ToList().FirstOrDefault();
                if (grouping != null && grouping.Active)
                {
                    UIValidationRuleGroupPrecard ruleGroupPrecard = this.GetRuleGroupPrecard(grouping, request);
                    if (ruleGroupPrecard != null && ((ruleGroupPrecard.Operator || personId == BUser.CurrentUser.Person.ID) || (request.IsEdited && IsCurrentUserManager) || request.IsFromService))
                    {
                        if (grouping.ValidationRule.Type == UIValidationRuleType.RulePrecard)
                        {
                            R37_EstelajiRequeredFileAttachement(request, grouping);
                        }
                    }
                }
                #endregion

                #region R38
                grouping = RulegroupList.Where(x => x.ValidationRule != null && x.ValidationRule.CustomCode == 38.ToString()).ToList().FirstOrDefault();
                if (grouping != null && grouping.Active)
                {
                    UIValidationRuleGroupPrecard ruleGroupPrecard = this.GetRuleGroupPrecard(grouping, request);
                    if (ruleGroupPrecard != null && ((ruleGroupPrecard.Operator || personId == BUser.CurrentUser.Person.ID) || (request.IsEdited && IsCurrentUserManager) || request.IsFromService))
                    {
                        if (grouping.ValidationRule.Type == UIValidationRuleType.RulePrecardParameter)
                        {
                            R38_RequestMaxLength(request, grouping);
                        }
                    }
                }

                #endregion
                #region R200
                grouping = RulegroupList.Where(x => x.ValidationRule != null && x.ValidationRule.CustomCode == 200.ToString()).ToList().FirstOrDefault();
                if (grouping != null && grouping.Active)
                {
                    UIValidationRuleGroupPrecard ruleGroupPrecard = this.GetRuleGroupPrecard(grouping, request);
                    if (ruleGroupPrecard != null && ((ruleGroupPrecard.Operator || personId == BUser.CurrentUser.Person.ID) || (request.IsEdited && IsCurrentUserManager) || request.IsFromService))
                    {
                        if (grouping.ValidationRule.Type == UIValidationRuleType.RulePrecardParameter)
                        {
                            R200_RequestLeaveHourlyNotAllowedWithOtherPrecardsInSticking(request, grouping);
                        }
                    }
                }
                #endregion
                #region R201
                grouping = RulegroupList.Where(x => x.ValidationRule != null && x.ValidationRule.CustomCode == 201.ToString()).ToList().FirstOrDefault();
                if (grouping != null && grouping.Active)
                {
                    UIValidationRuleGroupPrecard ruleGroupPrecard = this.GetRuleGroupPrecard(grouping, request);
                    if (ruleGroupPrecard != null && ((ruleGroupPrecard.Operator || personId == BUser.CurrentUser.Person.ID) || (request.IsEdited && IsCurrentUserManager) || request.IsFromService))
                    {
                        if (grouping.ValidationRule.Type == UIValidationRuleType.RulePrecardParameter)
                        {
                            R201_RequestLeaveDailyNotAllowedWithOtherPrecardsInSticking(request, grouping);
                        }
                    }
                }
                #endregion

                //DNN Note:
                #region R202
                grouping = RulegroupList.Where(x => x.ValidationRule != null && x.ValidationRule.CustomCode == 202.ToString()).ToList().FirstOrDefault();
                if (grouping != null && grouping.Active)
                {
                    UIValidationRuleGroupPrecard ruleGroupPrecard = this.GetRuleGroupPrecard(grouping, request);
                    if (ruleGroupPrecard != null && ((ruleGroupPrecard.Operator || personId == BUser.CurrentUser.Person.ID) || (request.IsEdited && IsCurrentUserManager) || request.IsFromService))
                    {
                        if (grouping.ValidationRule.Type == UIValidationRuleType.RulePrecard)
                        {
                            R202_RequestRegisterSchedule(request, grouping);
                        }
                    }
                }
                #endregion

                #region R204
                grouping = RulegroupList.Where(x => x.ValidationRule != null && x.ValidationRule.CustomCode == 204.ToString()).ToList().FirstOrDefault();
                if (grouping != null && grouping.Active)
                {
                    UIValidationRuleGroupPrecard ruleGroupPrecard = this.GetRuleGroupPrecard(grouping, request);
                    if (ruleGroupPrecard != null && ((ruleGroupPrecard.Operator || personId == BUser.CurrentUser.Person.ID) || (request.IsEdited && IsCurrentUserManager) || request.IsFromService))
                    {
                        if (grouping.ValidationRule.Type == UIValidationRuleType.RulePrecard)
                        {
                            R204_RequestIncompleteTraffic(request, grouping);
                        }
                    }
                }
                #endregion


                #region 205
                grouping = RulegroupList.Where(x => x.ValidationRule != null && x.ValidationRule.CustomCode == 205.ToString()).ToList().FirstOrDefault();
                if (grouping != null && grouping.Active)
                {
                    UIValidationRuleGroupPrecard ruleGroupPrecard = this.GetRuleGroupPrecard(grouping, request);
                    if (ruleGroupPrecard != null && ((ruleGroupPrecard.Operator || personId == BUser.CurrentUser.Person.ID) || (request.IsEdited && IsCurrentUserManager) || request.IsFromService))
                    {
                        if (grouping.ValidationRule.Type == UIValidationRuleType.RulePrecard)
                        {
                            R205_RequestInHoliday(request, grouping);
                        }
                    }
                }
                #endregion


                //End Of DNN Note

                if (exception.Count > 0)
                    throw exception;
            }


            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "RequestValidator", "DoValidate");
                throw ex;
            }
        }
        /// <summary>
        /// محاسبات هر ماه در روز ___ ماه ____ بسته شود 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="grouping"></param>
        private void R1_LockCalculationFromCurrentMonth(object obj, UIValidationRuleGroup RuleGroup, Request req, bool IsPermit)
        {
            // Request req=new Request();
            //IList<UIValidationRuleParam> parameters = validateRep.GetParameterRule(RuleGroup.ValidationRule.CustomCode);
            IList<UIValidationRuleParam> parameters = validateRep.GetParameterRule(RuleGroup.ID);
            #region Checking
            if (Utility.IsEmpty(parameters) || parameters.Count != 3)
            {
                throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterCount, "تعداد پارامتر های قانون 1 با مقدار مورد انتظار نابرابر است ", ExceptionSrc);
            }
            UIValidationRuleParam param = parameters.Where(x => !Utility.IsEmpty(x.UIValidationRuleTempParam.KeyName) && x.UIValidationRuleTempParam.KeyName.Equals("LockCalculationFromCurrentMonth")).FirstOrDefault();
            UIValidationRuleParam paramMonth = parameters.Where(x => !Utility.IsEmpty(x.UIValidationRuleTempParam.KeyName) && x.UIValidationRuleTempParam.KeyName.Equals("LockCalculationFromMonth")).FirstOrDefault();
            if (param == null)
            {
                throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, "پارامتر با کلید LockCalculationFromCurrentMonth  نظر برای قانون 1 یافت نشد ", ExceptionSrc);
            }
            if (paramMonth == null)
            {
                throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, "پارامتر با کلید LockCalculationFromMonth  نظر برای قانون 1 یافت نشد ", ExceptionSrc);
            }
            UIValidationRuleParam SecParam = parameters.Where(x => !Utility.IsEmpty(x.UIValidationRuleTempParam.KeyName) && x.UIValidationRuleTempParam.KeyName.Equals("NumberOfDaysAfterLockDate")).FirstOrDefault();
            if (SecParam == null)
            {
                throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, "پارامتر با کلید NumberOfDaysAfterLockDate  نظر برای قانون 1 یافت نشد ", ExceptionSrc);
            }
            #endregion

            int lockCalculationFromCurrentMonth = Utility.ToInteger(param.Value);
            int lockCalculationFromMonth = Utility.ToInteger(paramMonth.Value);
            PersianDateTime pd = null;
            PersianDateTime pdd = null;
            DateTime ed = new DateTime();
            DateTime edd = new DateTime();
            DateTime lockDate = new DateTime();
            DateTime NowTime = DateTime.Now;
            int Month = 1;
            if (lockCalculationFromCurrentMonth == 1)
            {
                Month = NowTime.Month;
            }
            else
            {
                Month = NowTime.Month - 1;
            }
            if (Month <= 6 && lockCalculationFromMonth > 31 && lockCalculationFromMonth > 31)
            {
                lockCalculationFromMonth = 31;
            }
            else if (Month > 6 && Month <= 11 && lockCalculationFromMonth > 30)
            {
                lockCalculationFromMonth = 30;
            }
            else if (Month == 12 && lockCalculationFromMonth > 29)
            {
                lockCalculationFromMonth = 29;
            }
            DateTime changeDate = Utility.GTSMinStandardDateTime;
            Type classtype = obj.GetType();
            Person personObj = null;
            /*if (classtype == typeof(GTS.Clock.Model.Concepts.AssignWorkGroup))
            {
                GTS.Clock.Model.Concepts.AssignWorkGroup assgnWrkGrp = (GTS.Clock.Model.Concepts.AssignWorkGroup)obj;
                changeDate = assgnWrkGrp.FromDate;
            }*/
            if (classtype == typeof(GTS.Clock.Model.RequestFlow.Request))
            {
                GTS.Clock.Model.RequestFlow.Request request = (GTS.Clock.Model.RequestFlow.Request)obj;
                changeDate = request.FromDate;
                personObj = request.Person;


            }
            if (classtype == typeof(GTS.Clock.Model.Concepts.Permit))
            {
                GTS.Clock.Model.Concepts.Permit permit = (GTS.Clock.Model.Concepts.Permit)obj;
                changeDate = permit.FromDate;
                personObj = permit.Person;
            }

            else if (classtype == typeof(GTS.Clock.Model.Concepts.ShiftException))
            {
                GTS.Clock.Model.Concepts.ShiftException shift = (GTS.Clock.Model.Concepts.ShiftException)obj;
                changeDate = shift.Date;
                personObj = shift.Person;

            }
            else if (classtype == typeof(GTS.Clock.Model.Concepts.LeaveIncDec))
            {
                GTS.Clock.Model.Concepts.LeaveIncDec leaveIncDec = (GTS.Clock.Model.Concepts.LeaveIncDec)obj;
                changeDate = leaveIncDec.Date;
                personObj = leaveIncDec.Person;
            }
            else if (classtype == typeof(GTS.Clock.Model.Concepts.LeaveYearRemain))
            {
                GTS.Clock.Model.Concepts.LeaveYearRemain leaveYearRemain = (GTS.Clock.Model.Concepts.LeaveYearRemain)obj;
                changeDate = leaveYearRemain.Date;
                personObj = leaveYearRemain.Person;
            }
            else if (classtype == typeof(GTS.Clock.Model.Concepts.BasicTraffic))
            {
                GTS.Clock.Model.Concepts.BasicTraffic traffic = (GTS.Clock.Model.Concepts.BasicTraffic)obj;

                changeDate = traffic.Date;
                personObj = traffic.Person;
            }
            if (classtype == typeof(GTS.Clock.Business.Engine.BEngineCalculator))
            {
                GTS.Clock.Business.Engine.BEngineCalculator Calculation = (GTS.Clock.Business.Engine.BEngineCalculator)obj;
                changeDate = DateTime.Now;
                //    personObj = Calculation.Person;
            }
            Person person = new PersonRepository().GetById(personObj.ID, false);
            DateRange dateRangePersonInRequestDate = new BDateRange().GetDateRangePerson(person, 0, changeDate);
            DateTime requestDate = req.FromDate;

            if (lockCalculationFromCurrentMonth == 1)
            {
                pd = Utility.ToPersianDateTime(dateRangePersonInRequestDate.ToDate);
                pdd = Utility.ToPersianDateTime(NowTime.Date);
                ed = dateRangePersonInRequestDate.ToDate;
                edd = NowTime.Date;
                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    int EndDay = Utility.GetEndOfPersianMonth(pdd.Year, pdd.Month);
                    if (lockCalculationFromMonth > EndDay)
                        lockDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", pdd.Year, pdd.Month, EndDay));
                    else
                        lockDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", pdd.Year, pdd.Month, lockCalculationFromMonth));
                }
                else
                {
                    int EndDay = Utility.GetEndOfMiladiMonth(edd.Year, edd.Month);
                    if (lockCalculationFromMonth > EndDay)
                        lockDate = DateTime.Parse(String.Format("{0}/{1}/{2}", edd.Year, edd.Month, EndDay));
                    else
                        lockDate = DateTime.Parse(String.Format("{0}/{1}/{2}", edd.Year, edd.Month, lockCalculationFromMonth));
                }
            }
            else if (lockCalculationFromCurrentMonth == 0)
            {
                pd = Utility.ToPersianDateTime(dateRangePersonInRequestDate.ToDate);
                pdd = Utility.ToPersianDateTime(NowTime.Date);
                ed = dateRangePersonInRequestDate.ToDate;
                edd = NowTime.Date;
                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    if (pdd.Month > 1)
                    {
                        int EndDay = Utility.GetEndOfPersianMonth(pdd.Year, pdd.Month - 1);
                        if (lockCalculationFromMonth > EndDay)
                            lockDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", pdd.Year, pdd.Month - 1, EndDay));
                        else
                            lockDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", pdd.Year, pdd.Month - 1, lockCalculationFromMonth));
                    }
                    else
                    {
                        int EndDay = Utility.GetEndOfPersianMonth(pdd.Year - 1, 12);
                        if (lockCalculationFromMonth > EndDay)
                            lockDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", pdd.Year - 1, 12, EndDay));
                        else
                            lockDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", pdd.Year - 1, 12, lockCalculationFromMonth));
                    }
                }
                else
                {
                    if (edd.Month > 1)
                    {
                        int EndDay = Utility.GetEndOfMiladiMonth(edd.Year, edd.Month - 1);
                        if (lockCalculationFromMonth > EndDay)
                            lockDate = DateTime.Parse(String.Format("{0}/{1}/{2}", edd.Year, edd.Month - 1, EndDay));
                        else
                            lockDate = DateTime.Parse(String.Format("{0}/{1}/{2}", edd.Year, edd.Month - 1, lockCalculationFromMonth));
                    }
                    else
                    {
                        int EndDay = Utility.GetEndOfMiladiMonth(edd.Year - 1, 12);
                        if (lockCalculationFromMonth > EndDay)
                            lockDate = DateTime.Parse(String.Format("{0}/{1}/{2}", edd.Year - 1, 12, EndDay));
                        else
                            lockDate = DateTime.Parse(String.Format("{0}/{1}/{2}", edd.Year - 1, 12, lockCalculationFromMonth));
                    }
                }
            }
            int NumberOfDaysAfterLockDate = Utility.ToInteger(SecParam.Value);
            DateTime PermitLockDate = lockDate.AddDays(NumberOfDaysAfterLockDate);
            if (IsPermit)
            {
                if (NowTime > PermitLockDate)
                //       if ((changeDate > Utility.GTSMinStandardDateTime) && (changeDate <= LockCalculationFromDate && PermitDate.Date > PermitLockDate))
                {
                    AddException(ExceptionResourceKeys.UIValidation_R1_LockCalculationFromCurrentMonth, " محاسبات بسته شده است");
                }
            }
            else
            {
                if (lockCalculationFromCurrentMonth == 0)
                {
                    if (requestDate < lockDate && changeDate > Utility.GTSMinStandardDateTime && changeDate <= dateRangePersonInRequestDate.ToDate)
                    {
                        AddException(ExceptionResourceKeys.UIValidation_R1_LockCalculationFromCurrentMonth, " محاسبات بسته شده است");
                    }
                }
                else
                {
                    if ((requestDate < lockDate && NowTime.Date > lockDate) && changeDate > Utility.GTSMinStandardDateTime && changeDate <= dateRangePersonInRequestDate.ToDate)
                    {
                        AddException(ExceptionResourceKeys.UIValidation_R1_LockCalculationFromCurrentMonth, " محاسبات بسته شده است");
                    }
                }
            }
        }
        /// <summary>
        /// محاسبات ___ روز بعد از ____ دوره محاسبات قبل بسته شود 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="grouping"></param>
        private void R2_LockCalculationFromCalculationMonth(object obj, UIValidationRuleGroup RuleGroup, Request req)
        {
            // Request req=new Request();
            //IList<UIValidationRuleParam> parameters = validateRep.GetParameterRule(RuleGroup.ValidationRule.CustomCode);
            DateTime lockDate = new DateTime();
            DateTime ed = new DateTime();
            IList<UIValidationRuleParam> parameters = validateRep.GetParameterRule(RuleGroup.ID);
            #region Checking
            if (Utility.IsEmpty(parameters) || parameters.Count != 2)
            {
                throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterCount, "تعداد پارامتر های قانون 2 با مقدار مورد انتظار نابرابر است ", ExceptionSrc);
            }
            UIValidationRuleParam param = parameters.Where(x => !Utility.IsEmpty(x.UIValidationRuleTempParam.KeyName) && x.UIValidationRuleTempParam.KeyName.Equals("LockCalculationtMonth")).FirstOrDefault();
            UIValidationRuleParam paramMonth = parameters.Where(x => !Utility.IsEmpty(x.UIValidationRuleTempParam.KeyName) && x.UIValidationRuleTempParam.KeyName.Equals("LockCalculationDay")).FirstOrDefault();
            if (param == null)
            {
                throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, "پارامتر با کلید LockCalculationtMonth  نظر برای قانون 2 یافت نشد ", ExceptionSrc);
            }
            if (paramMonth == null)
            {
                throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, "پارامتر با کلید LockCalculationDay  نظر برای قانون 2 یافت نشد ", ExceptionSrc);
            }
            #endregion
            int lockCalculationFromSpesificMonth = Utility.ToInteger(param.Value);
            int lockCalculationDay = Utility.ToInteger(paramMonth.Value);
            DateTime changeDate = Utility.GTSMinStandardDateTime;
            DateTime Date = DateTime.Now.Date;
            Type classtype = obj.GetType();
            Person personObj = null;
            if (classtype == typeof(GTS.Clock.Model.RequestFlow.Request))
            {
                GTS.Clock.Model.RequestFlow.Request request = (GTS.Clock.Model.RequestFlow.Request)obj;
                changeDate = Date;
                personObj = request.Person;
            }
            if (classtype == typeof(GTS.Clock.Model.Concepts.Permit))
            {
                GTS.Clock.Model.Concepts.Permit permit = (GTS.Clock.Model.Concepts.Permit)obj;
                changeDate = permit.FromDate;
                personObj = permit.Person;
            }
            else if (classtype == typeof(GTS.Clock.Model.Concepts.ShiftException))
            {
                GTS.Clock.Model.Concepts.ShiftException shift = (GTS.Clock.Model.Concepts.ShiftException)obj;
                changeDate = shift.Date;
                personObj = shift.Person;

            }
            else if (classtype == typeof(GTS.Clock.Model.Concepts.LeaveIncDec))
            {
                GTS.Clock.Model.Concepts.LeaveIncDec leaveIncDec = (GTS.Clock.Model.Concepts.LeaveIncDec)obj;
                changeDate = leaveIncDec.Date;
                personObj = leaveIncDec.Person;

            }
            else if (classtype == typeof(GTS.Clock.Model.Concepts.LeaveYearRemain))
            {
                GTS.Clock.Model.Concepts.LeaveYearRemain leaveYearRemain = (GTS.Clock.Model.Concepts.LeaveYearRemain)obj;
                changeDate = leaveYearRemain.Date;
                personObj = leaveYearRemain.Person;
            }
            else if (classtype == typeof(GTS.Clock.Model.Concepts.BasicTraffic))
            {
                GTS.Clock.Model.Concepts.BasicTraffic traffic = (GTS.Clock.Model.Concepts.BasicTraffic)obj;
                changeDate = traffic.Date;
                personObj = traffic.Person;
            }
            else if (classtype == typeof(GTS.Clock.Business.Engine.BEngineCalculator))
            {
                GTS.Clock.Business.Engine.BEngineCalculator Calculation = (GTS.Clock.Business.Engine.BEngineCalculator)obj;
                changeDate = DateTime.Now;
                //    personObj = Calculation.Person;
            }
            Person person = new PersonRepository().GetById(personObj.ID, false);
            DateRange dateRangePersonInRequestDate = new BDateRange().GetDateRangePerson(person, 0, changeDate);
            DateTime requestDate = req.FromDate;

            ed = dateRangePersonInRequestDate.ToDate;
            int CurrentDateRangeOrder = dateRangePersonInRequestDate.DateRangeOrder;
            int CustomDateRangeOrder = CurrentDateRangeOrder - lockCalculationFromSpesificMonth;
            // decimal CurrentActiveCalculation = new BDateRange().GetCurrentActiveDateRange(person);
            if (CustomDateRangeOrder == 0)
            {
                CustomDateRangeOrder = 12;
                Date = DateTime.Parse(string.Format("{0}/{1}/{2}", Date.Year - 1, 12, Date.Day));
            }
            else if (CustomDateRangeOrder == -1)
            {
                CustomDateRangeOrder = 11;
                Date = DateTime.Parse(string.Format("{0}/{1}/{2}", Date.Year - 1, 11, Date.Day));
            }
            DateRange SpesificdateRange = new BDateRange().GetSpesificMonthDateRange(person, 0, Date, CustomDateRangeOrder);
            int MaximumDay = (int)(SpesificdateRange.ToDate - SpesificdateRange.FromDate).TotalDays;
            int SpesificdateRangeday = SpesificdateRange.ToDate.Day;
            if (lockCalculationDay > MaximumDay)
            {
                lockCalculationDay = MaximumDay;
            }
            int day = SpesificdateRangeday + lockCalculationDay;

            int month = SpesificdateRange.ToDate.Month;
            if (month < 6 && day > 31)
            {
                month = month + 1;
                day = day - 31;
            }
            else if (month >= 6 && month < 12 && day > 30)
            {
                month = month + 1;
                day = day - 30;
            }
            else if (month == 12)
            {
                month = 1;
                day = day - 31;
            }
            int year = SpesificdateRange.ToDate.Year;
            lockDate = DateTime.Parse(String.Format("{0}/{1}/{2}", year, month, day));
            if (requestDate <= lockDate && changeDate > Utility.GTSMinStandardDateTime)
            {
                AddException(ExceptionResourceKeys.UIValidation_R2_LockCalculationFromCalculationMonth, " محاسبات بسته شده است");
            }

        }
        /// <summary>
        /// محاسبات از تاریخ ______ بسته شود 
        /// اگر قوانین 1و2اجرا نشود این قانون حتما اجرا میشود
        /// پس همیشه باید پارامتر داشته باشد
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="grouping"></param>
        private void R3_LockCalculationFromDate(object obj, UIValidationRuleGroup RuleGroup, Request req, bool IsPermit)
        {

            //IList<UIValidationRuleParam> parameters = validateRep.GetParameterRule(RuleGroup.ValidationRule.CustomCode);

            IList<UIValidationRuleParam> parameters = validateRep.GetParameterRule(RuleGroup.ID);

            #region cheking
            if (Utility.IsEmpty(parameters) || parameters.Count != 2)
            {
                throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterCount, "تعداد پارامتر های قانون 3 با مقدار مورد انتظار نابرابر است - تاریخ بستن محاسبات ", ExceptionSrc);
            }
            UIValidationRuleParam param = parameters.Where(x => !Utility.IsEmpty(x.UIValidationRuleTempParam.KeyName) && x.UIValidationRuleTempParam.KeyName.Equals("LockCalculationFromDate")).FirstOrDefault();
            if (param == null)
            {
                throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, "پارامتر با کلید  - تاریخ بستن محاسبات LockCalculationFromDate  نظر برای قانون 3 یافت نشد ", ExceptionSrc);
            }
            UIValidationRuleParam SecParam = parameters.Where(x => !Utility.IsEmpty(x.UIValidationRuleTempParam.KeyName) && x.UIValidationRuleTempParam.KeyName.Equals("NumberOfDaysAfterLockDate")).FirstOrDefault();
            if (SecParam == null)
            {
                throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, "پارامتر با کلید  - زمان مجاز تایید درخواست بعد از بستن محاسبات NumberOfDaysAfterLockDate  نظر برای قانون 3 یافت نشد ", ExceptionSrc);
            }
            #endregion

            DateTime LockCalculationFromDate = new DateTime();
            if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
            {
                LockCalculationFromDate = Utility.ToMildiDateTime(param.Value);
            }
            else
            {
                LockCalculationFromDate = Utility.ToMildiDateTime(param.Value);
            }
            //    PersianDateTime pd = Utility.ToPersianDateTime(DateTime.Now);
            int NumDays = Utility.ToInteger(SecParam.Value);
            DateTime PermitLockDate = LockCalculationFromDate.AddDays(NumDays);
            DateTime changeDate = Utility.GTSMinStandardDateTime;
            DateTime ReqDate = Utility.GTSMinStandardDateTime;
            Type classtype = obj.GetType();
            DateTime PermitDate = DateTime.Now;
            /*if (classtype == typeof(GTS.Clock.Model.Concepts.AssignWorkGroup))
            {
                GTS.Clock.Model.Concepts.AssignWorkGroup assgnWrkGrp = (GTS.Clock.Model.Concepts.AssignWorkGroup)obj;
                changeDate = assgnWrkGrp.FromDate;
            }*/
            if (classtype == typeof(GTS.Clock.Model.RequestFlow.Request))
            {
                GTS.Clock.Model.RequestFlow.Request request = (GTS.Clock.Model.RequestFlow.Request)obj;
                changeDate = request.FromDate;
            }
            if (classtype == typeof(GTS.Clock.Model.Concepts.Permit))
            {
                GTS.Clock.Model.Concepts.Permit permit = (GTS.Clock.Model.Concepts.Permit)obj;
                changeDate = permit.FromDate;
                //decimal ReqD=permit.Pairs[0].RequestID;
                // requestObj = new BRequest().GetByID(ReqD);

            }
            else if (classtype == typeof(GTS.Clock.Model.Concepts.ShiftException))
            {
                GTS.Clock.Model.Concepts.ShiftException shift = (GTS.Clock.Model.Concepts.ShiftException)obj;
                changeDate = shift.Date;
            }
            else if (classtype == typeof(GTS.Clock.Model.Concepts.LeaveIncDec))
            {
                GTS.Clock.Model.Concepts.LeaveIncDec leaveIncDec = (GTS.Clock.Model.Concepts.LeaveIncDec)obj;
                changeDate = leaveIncDec.Date;
            }
            else if (classtype == typeof(GTS.Clock.Model.Concepts.LeaveYearRemain))
            {
                GTS.Clock.Model.Concepts.LeaveYearRemain leaveYearRemain = (GTS.Clock.Model.Concepts.LeaveYearRemain)obj;
                changeDate = leaveYearRemain.Date;
            }
            else if (classtype == typeof(GTS.Clock.Model.Concepts.BasicTraffic))
            {
                GTS.Clock.Model.Concepts.BasicTraffic traffic = (GTS.Clock.Model.Concepts.BasicTraffic)obj;
                changeDate = traffic.Date;
            }
            else if (classtype == typeof(GTS.Clock.Business.Engine.BEngineCalculator))
            {
                GTS.Clock.Business.Engine.BEngineCalculator Calculation = (GTS.Clock.Business.Engine.BEngineCalculator)obj;
                changeDate = Calculation.FromDate;
                //    personObj = Calculation.Person;
            }
            if (IsPermit)
            {
                if ((changeDate > Utility.GTSMinStandardDateTime) && (changeDate <= LockCalculationFromDate && PermitDate.Date > PermitLockDate) && DateTime.Now > LockCalculationFromDate)
                {
                    AddException(ExceptionResourceKeys.UIValidation_R3_LockCalculationFromDate, " محاسبات بسته شده است");
                }
            }
            else
            {
                //if (changeDate > Utility.GTSMinStandardDateTime && changeDate < LockCalculationFromDate && (changeDate == LockCalculationFromDate) && (DateTime.Now > LockCalculationFromDate))
                if (changeDate > Utility.GTSMinStandardDateTime && changeDate <= LockCalculationFromDate && DateTime.Now > LockCalculationFromDate)
                {
                    AddException(ExceptionResourceKeys.UIValidation_R3_LockCalculationFromDate, " محاسبات بسته شده است");
                }
            }
        }
        /// <summary>
        /// تعداد درخواستهای ساعتی پرسنل در ماه حداکثر ___ عدد باشد
        /// </summary>
        private void R4_MaxTarfficRequest(Request request, UIValidationRuleGroup RuleGroup)
        {
            int RequestCount;
            string Param = "MaxRequestInMonth";
            try
            {
                //    if (validateRep.GetPrecardRule(RuleGroup.ValidationRule.CustomCode).Where(x => x.ID == request.Precard.ID).Count() > 0)
                if (validateRep.GetPrecardRule(RuleGroup.ID).Where(x => x.ID == request.Precard.ID).Count() > 0)
                {
                    //IList<UIValidationRuleParam> parameters = validateRep.GetParameterRule(RuleGroup.ValidationRule.CustomCode);
                    IList<UIValidationRuleParam> parameters = validateRep.GetParameterRule(RuleGroup.ID, request.Precard.ID, Param);


                    if (Utility.IsEmpty(parameters) || parameters.Count != 1)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterCount, "تعداد پارامتر های قانون 4 با مقدار مورد انتظار نابرابر است ", ExceptionSrc);
                    }
                    UIValidationRuleParam param = parameters.Where(x => !Utility.IsEmpty(x.UIValidationRuleTempParam.KeyName) && x.UIValidationRuleTempParam.KeyName.Equals("MaxRequestInMonth")).FirstOrDefault();
                    if (param == null)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, "پارامتر با کلید MaxRequestInMonth  نظر برای قانون 4 یافت نشد ", ExceptionSrc);
                    }
                    int maxCount = Utility.ToInteger(param.Value);
                    DateTime requestDate = request.FromDate.Date;
                    DateTime startMonth, endMonth;
                    Person person = new PersonRepository().GetById(request.Person.ID, false);
                    DateRange dateRangePersonInRequestDate = new BDateRange().GetDateRangePerson(person, 0, requestDate);
                    startMonth = dateRangePersonInRequestDate.FromDate;
                    DateTime date = startMonth;
                    endMonth = dateRangePersonInRequestDate.ToDate;
                    IList<GTS.Clock.Model.RequestFlow.Request> confirmlist = null;
                    GTS.Clock.Model.RequestFlow.Request req = null;
                    RequestStatus reqStatus = null;
                    RequestStatus existsReqStatus = null;
                    List<Request> requestList = new List<Request>();
                    IList<GTS.Clock.Model.RequestFlow.Request> list = null;


                    confirmlist = requestRepository.NHibernateSession.QueryOver<GTS.Clock.Model.RequestFlow.Request>(() => req)
                                         .JoinAlias(() => req.RequestStatusList, () => reqStatus)
                                         .Where(() => req.FromDate >= date && req.FromDate <= endMonth)
                                         .And(() => req.Precard.ID == request.Precard.ID)
                                         .And(() => req.Person.ID == request.Person.ID)
                                         .Where(() => reqStatus.Confirm && reqStatus.EndFlow && !reqStatus.IsDeleted)
                                         .List<GTS.Clock.Model.RequestFlow.Request>();
                    var existing = QueryOver.Of<RequestStatus>(() => existsReqStatus)
                .Where(() => existsReqStatus.EndFlow == true)
                .And(() => existsReqStatus.Request.ID == req.ID)
                .Select(x => x.ID);
                    Permit permit = null;
                    IList<Permit> existingPermitRequestList = requestRepository.NHibernateSession.QueryOver<Permit>(() => permit)
                                                                         .Where(() => permit.Person.ID == request.Person.ID)
                                                                          .And(() => permit.FromDate >= date && permit.FromDate <= endMonth)
                                                                         .List<Permit>();
                    List<decimal> requestHavePermitIdsList = new List<decimal>();
                    foreach (Permit item in existingPermitRequestList)
                    {
                        requestHavePermitIdsList.AddRange(item.Pairs.Select(p => p.RequestID).ToList<decimal>());
                    }
                    requestHavePermitIdsList = requestHavePermitIdsList.Distinct().ToList<decimal>();
                    list = requestRepository.NHibernateSession.QueryOver<GTS.Clock.Model.RequestFlow.Request>(() => req)
                                                  .Where(() => req.FromDate >= date && req.FromDate <= endMonth)
                                                  .And(() => req.Person.ID == request.Person.ID)
                                                  .And(() => req.Precard.ID == request.Precard.ID)
                                                  .And(() => !req.EndFlow)
                                                  .And(() => !req.ID.IsIn(requestHavePermitIdsList))
                                                  .WithSubquery.WhereNotExists(existing).List<Request>();

                    requestList.AddRange(confirmlist);
                    requestList.AddRange(list);
                    RequestCount = requestList.Count();
                    if (request.IsEdited)
                    {
                        var requestlist = (from n in requestList
                                           where (request.ID == n.ID)
                                           select n).ToList();
                        if (requestlist.Count >= 1)
                        {
                            RequestCount = RequestCount - 1;
                        }
                    }
                    if (RequestCount + 1 > maxCount)
                    {
                        AddException(ExceptionResourceKeys.UIValidation_R4_MaxTarfficRequest, " تعداد درخواست های ساعتی از حداکثر مجاز تجاوز کرده است");
                    }
                }

            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "RequestValidator", "R4_MaxTarfficRequest");
                throw ex;
            }
        }
        /// <summary>
        /// درخواست های ساعتی از___ روز قبل تا ___ روز بعد از روز درخواست قابل ثبت باشد
        /// </summary>
        private void R5_HourlyTrafficRequest(Request request, UIValidationRuleGroup RuleGroup)
        {
            string FirstParam = "AllowedRequestAfterTime";
            string SecondParam = "AllowedRequestBeforeTime";
            try
            {
                //    if (validateRep.GetPrecardRule(RuleGroup.ValidationRule.CustomCode).Where(x => x.ID == request.Precard.ID).Count() > 0)
                if (validateRep.GetPrecardRule(RuleGroup.ID).Where(x => x.ID == request.Precard.ID).Count() > 0)
                {
                    //  IList<UIValidationRuleParam> parameters = validateRep.GetParameterRule(RuleGroup.ValidationRule.CustomCode);
                    IList<UIValidationRuleParam> parameters = validateRep.GetParameterRule(RuleGroup.ID, request.Precard.ID, FirstParam, SecondParam);


                    if (Utility.IsEmpty(parameters) || parameters.Count != 2)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterCount, "تعداد پارامتر های قانون 5 با مقدار مورد انتظار نابرابر است ", ExceptionSrc);
                    }
                    UIValidationRuleParam param = parameters.Where(x => !Utility.IsEmpty(x.UIValidationRuleTempParam.KeyName) && x.UIValidationRuleTempParam.KeyName.Equals("AllowedRequestAfterTime")).FirstOrDefault();
                    if (param == null)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, "پارامتر با کلید AllowedRequestAfterTime  نظر برای قانون 5 یافت نشد ", ExceptionSrc);
                    }
                    int afterTelorance = Utility.ToInteger(param.Value);

                    param = parameters.Where(x => !Utility.IsEmpty(x.UIValidationRuleTempParam.KeyName) && x.UIValidationRuleTempParam.KeyName.Equals("AllowedRequestBeforeTime")).FirstOrDefault();
                    if (param == null)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, "پارامتر با کلید AllowedRequestBeforeTime  نظر برای قانون 5 یافت نشد ", ExceptionSrc);
                    }
                    int beforeTelorance = Utility.ToInteger(param.Value);

                    DateTime requestDate = request.FromDate.Date;
                    DateTime registerDate = DateTime.Now.Date;
                    if (request.IsEdited)
                    {
                        requestDate = request.FromDate.Date;
                        registerDate = request.RegisterDate;
                    }
                    if ((registerDate - requestDate).Days > afterTelorance)
                    {
                        AddException(ExceptionResourceKeys.UIValidation_R5_HourlyTrafficRequest, "مهلت ثبت درخواست  به پایان رسیده است");
                    }
                    if ((requestDate - registerDate).Days > beforeTelorance)
                    {
                        AddException(ExceptionResourceKeys.UIValidation_R5_HourlyTrafficRequest, "ثبت درخواست برای تاریخ مورد نظر مجاز نمیباشد");
                    }
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "RequestValidator", "R5_HourlyTrafficRequest");
                throw ex;
            }
        }
        /// <summary>
        /// تعداد درخواست های ساعتی اول وقت حداکثر ____ عدد باشد
        /// </summary>
        /// <param name="request"></param>
        /// <param name="grouping"></param>
        private void R6_RequestMaxAvvalVaght(Request request, UIValidationRuleGroup RuleGroup)
        {
            string FirstParam = "MaxCountInMonth";
            string Secondparam = "PeriodAvalVaght";
            try
            {
                //   if (validateRep.GetPrecard(grouping.ValidationRule.CustomCode).Where(x => x.ID == request.Precard.ID).Count() > 0)
                if (validateRep.GetPrecardRule(RuleGroup.ID).Where(x => x.ID == request.Precard.ID).Count() > 0)
                {
                    //    IList<UIValidationRuleParam> parameters = grouping.RuleParameters;
                    IList<UIValidationRuleParam> parameters = validateRep.GetParameterRule(RuleGroup.ID, request.Precard.ID, FirstParam, Secondparam);

                    if (Utility.IsEmpty(parameters) || parameters.Count != 2)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterCount, "تعداد پارامتر های قانون 6 با مقدار مورد انتظار نابرابر است ", ExceptionSrc);
                    }
                    UIValidationRuleParam maxCount = parameters.Where(x => !Utility.IsEmpty(x.UIValidationRuleTempParam.KeyName) && x.UIValidationRuleTempParam.KeyName.Equals("MaxCountInMonth")).FirstOrDefault();
                    if (maxCount == null)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, "پارامتر با کلید MaxCountInMonth  نظر برای قانون 6 یافت نشد ", ExceptionSrc);
                    }
                    UIValidationRuleParam PeriodTime = parameters.Where(x => !Utility.IsEmpty(x.UIValidationRuleTempParam.KeyName) && x.UIValidationRuleTempParam.KeyName.Equals("PeriodAvalVaght")).FirstOrDefault();
                    if (PeriodTime == null)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, "پارامتر با کلید PeriodAvalVaght  نظر برای قانون 6 یافت نشد ", ExceptionSrc);
                    }
                    int maxCountNum = Utility.ToInteger(maxCount.Value);
                    int PeriodTimeNum = Utility.RealTimeToIntTime(PeriodTime.Value);
                    DateTime requestDate = request.FromDate.Date;
                    DateTime startMonth, endMonth;
                    Person prs = new PersonRepository(false).GetById(request.Person.ID, false);

                    DateRange dateRange = new BDateRange().GetDateRangePerson(prs, 1011, request.FromDate);
                    startMonth = dateRange.FromDate;
                    endMonth = dateRange.ToDate;
                    prs.InitializeForAccessRules(startMonth, endMonth);
                    BaseShift shift = prs.GetShiftByDate(request.FromDate);
                    if (shift != null && shift.PairCount > 0)
                    {
                        if (shift.Pairs.OrderBy(x => x.From).First().From + PeriodTimeNum >= request.FromTime)
                        {
                            for (DateTime date = startMonth; date <= endMonth; date = date.AddDays(1))
                            {
                                shift = prs.GetShiftByDate(date);
                                if (shift != null && shift.PairCount > 0)
                                {
                                    RequestRepository rep = new RequestRepository(false);
                                    int shiftFromTime = shift.Pairs.OrderBy(y => y.From).First().From + PeriodTimeNum;
                                    List<InfoRequest> list = requestRepository.GetActiveRequestDateValues(request.Person.ID, request.Precard.ID, startMonth, endMonth).ToList();
                                    // int list = requestRepository.GetActiveRequestDateValues(request.Person.ID, request.Precard.ID, startMonth, endMonth).Where(i => i.FromTime <= shiftFromTime).Count();
                                    List<InfoRequest> requestlist = list.Where(i => i.FromTime <= shiftFromTime && i.TimeDuration <= PeriodTimeNum).ToList();
                                    int count = requestlist.Count();
                                    if (request.IsEdited)
                                    {
                                        var Editedreq = from c in requestlist
                                                        where (c.ID == request.ID)
                                                        select c.ID;
                                        if (Editedreq.Count() > 0)
                                        {
                                            count = count - 1;
                                        }
                                    }

                                    if (count >= maxCountNum && request.TimeDuration <= PeriodTimeNum)
                                    {
                                        AddException(ExceptionResourceKeys.UIValidation_R6_RequestMaxAvvalVaght, " تعداد درخواست  ساعتی اول وقت از حد مجاز تجاوز کرده است");
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "RequestValidator", "R6_RequestMaxAvvalVaght");
                throw ex;
            }
        }
        /// <summary>
        /// تعداد درخواست های روزانه در هر نوبت حداکثر ___ روز باشد
        /// </summary>
        private void R7_MaxValueOfRequest(Request request, UIValidationRuleGroup RuleGroup)
        {
            string Param = "DayCount";
            try
            {
                if (validateRep.GetPrecardRule(RuleGroup.ID).Where(x => x.ID == request.Precard.ID).Count() > 0)
                {
                    IList<UIValidationRuleParam> parameters = validateRep.GetParameterRule(RuleGroup.ID, request.Precard.ID, Param);
                    if (Utility.IsEmpty(parameters) || parameters.Count != 1)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterCount, "تعداد پارامتر های قانون 7 با مقدار مورد انتظار نابرابر است ", ExceptionSrc);
                    }
                    UIValidationRuleParam param = parameters.Where(x => !Utility.IsEmpty(x.UIValidationRuleTempParam.KeyName) && x.UIValidationRuleTempParam.KeyName.Equals("DayCount")).FirstOrDefault();
                    if (param == null)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, "پارامتر با کلید DayCount  نظر برای قانون 7 یافت نشد ", ExceptionSrc);
                    }
                    int maxDay = Utility.ToInteger(param.Value);

                    if ((request.ToDate - request.FromDate).Days + 1 > maxDay)
                    {
                        AddException(ExceptionResourceKeys.UIValidation_R7_MaxValueOfRequest, " مقدار مجموع روز درخواست از حداکثر مجاز تجاوز کرده است");
                    }
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "RequestValidator", "R7_MaxValueOfRequest");
                throw ex;
            }
        }
        /// <summary>
        /// همپوشانی درخواست های ساعتی مجاز نیست 
        /// </summary>
        private void R8_OverlyHourlyPrecard(Request request, UIValidationRuleGroup RuleGroup)
        {
            try
            {
                string Param = "PriorityPrecard";
                if (validateRep.GetPrecardRule(RuleGroup.ID).Where(x => x.ID == request.Precard.ID).Count() > 0)
                {
                    IList<Precard> precardList = validateRep.GetPrecardRule(RuleGroup.ID);
                    IList<UIValidationRuleParam> parameters = validateRep.GetParameterRule(RuleGroup.ID, request.Precard.ID, Param);
                    if (Utility.IsEmpty(parameters) || parameters.Count != 1)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterCount, "تعداد پارامتر های قانون 8 با مقدار مورد انتظار نابرابر است ", ExceptionSrc);
                    }
                    UIValidationRuleParam param = parameters.Where(x => !Utility.IsEmpty(x.UIValidationRuleTempParam.KeyName) && x.UIValidationRuleTempParam.KeyName.Equals("PriorityPrecard")).FirstOrDefault();
                    if (param == null)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, "پارامتر با کلید PriorityPrecard  نظر برای قانون 8 یافت نشد ", ExceptionSrc);
                    }
                    int priorityPrecardRequest = Utility.ToInteger(param.Value);
                    RequestRepository rep = new RequestRepository(false);
                    DateTime requestDate = request.FromDate.Date;
                    DateTime startMonth, endMonth;
                    Person person = new PersonRepository().GetById(request.Person.ID, false);
                    DateRange dateRangePersonInRequestDate = new BDateRange().GetDateRangePerson(person, 0, requestDate);
                    startMonth = dateRangePersonInRequestDate.FromDate;
                    endMonth = dateRangePersonInRequestDate.ToDate;
                    IList<Request> confirmRequestList = rep.GetAllRequest(request.Person.ID, startMonth, endMonth, RequestState.Confirmed);
                    IList<Request> underReviewRequestList = rep.GetAllRequest(request.Person.ID, startMonth, endMonth, RequestState.UnderReview);
                    List<Request> requestList = new List<Request>();
                    requestList.AddRange(confirmRequestList);
                    requestList.AddRange(underReviewRequestList);
                    List<string> precardValidationsList = validateRep.GetPrecardRule(RuleGroup.ID).Select(p => p.Code).ToList<string>();
                    List<Request> requestFilteredList = requestList.Where(r => precardValidationsList.Contains(r.Precard.Code)).ToList<Request>();
                    if (request.IsEdited)
                    {
                        requestFilteredList = requestFilteredList.Where(i => i.ID != request.ID).ToList();
                    }

                    var requestlist = from n in requestFilteredList
                                      where (n.FromTime >= request.FromTime && n.ToTime > request.ToTime && n.FromTime < request.ToTime || n.FromTime <= request.FromTime && n.ToTime >= request.ToTime || n.FromTime <= request.FromTime && n.ToTime <= request.ToTime && n.ToTime > request.FromTime || n.FromTime >= request.FromTime && n.ToTime <= request.ToTime)
                                      select n;

                    var requestlistI = from o in requestlist
                                       where (o.FromDate == request.FromDate || o.ToDate == request.ToDate)
                                       select o;
                    bool isPreventRequest = false;

                    foreach (Precard item in precardList)
                    {

                        IList<UIValidationRuleParam> paramsList = validateRep.GetParameterRule(RuleGroup.ID, item.ID, Param);
                        if (paramsList.Count > 0)
                        {
                            UIValidationRuleParam paramObj = paramsList.Where(x => !Utility.IsEmpty(x.UIValidationRuleTempParam.KeyName) && x.UIValidationRuleTempParam.KeyName.Equals("PriorityPrecard")).FirstOrDefault();
                            int priorityPrecardItem = Utility.ToInteger(paramObj.Value);
                            IList<Request> requestByPrecardList = requestlistI.Where(r => r.Precard.ID == item.ID).ToList();
                            if (priorityPrecardItem <= priorityPrecardRequest && requestByPrecardList.Count(c => c.Precard.ID == item.ID) > 0)
                                isPreventRequest = true;
                        }
                    }

                    if (isPreventRequest)
                    {
                        AddException(ExceptionResourceKeys.UIValidation_R8_OverlyHourlyPrecard, " تداخل زمانی درخواست ها");
                    }
                }


            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "RequestValidator", "R8_OverlyHourlyPrecard");
                throw ex;
            }
        }
        /// <summary>
        /// همپوشانی درخواست های روزانه مجاز نیست 
        /// </summary>

        private void R9_OverlyDailyPrecard(Request request, UIValidationRuleGroup RuleGroup)
        {

            try
            {
                string Param = "PriorityPrecard";
                if (validateRep.GetPrecardRule(RuleGroup.ID).Where(x => x.ID == request.Precard.ID).Count() > 0)
                {
                    IList<Precard> precardList = validateRep.GetPrecardRule(RuleGroup.ID);
                    IList<UIValidationRuleParam> parameters = validateRep.GetParameterRule(RuleGroup.ID, request.Precard.ID, Param);
                    if (Utility.IsEmpty(parameters) || parameters.Count != 1)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterCount, "تعداد پارامتر های قانون 9 با مقدار مورد انتظار نابرابر است ", ExceptionSrc);
                    }
                    UIValidationRuleParam param = parameters.Where(x => !Utility.IsEmpty(x.UIValidationRuleTempParam.KeyName) && x.UIValidationRuleTempParam.KeyName.Equals("PriorityPrecard")).FirstOrDefault();
                    if (param == null)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, "پارامتر با کلید PriorityPrecard  نظر برای قانون 9 یافت نشد ", ExceptionSrc);
                    }
                    int priorityPrecardRequest = Utility.ToInteger(param.Value);
                    RequestRepository rep = new RequestRepository(false);
                    DateTime requestDate = request.FromDate.Date;
                    DateTime startMonth, endMonth;
                    Person person = new PersonRepository().GetById(request.Person.ID, false);
                    DateRange dateRangePersonInRequestDate = new BDateRange().GetDateRangePerson(person, 0, requestDate);
                    startMonth = dateRangePersonInRequestDate.FromDate;
                    endMonth = dateRangePersonInRequestDate.ToDate;
                    IList<Request> confirmRequestList = rep.GetAllRequest(request.Person.ID, startMonth, endMonth, RequestState.Confirmed);
                    IList<Request> underReviewRequestList = rep.GetAllRequest(request.Person.ID, startMonth, endMonth, RequestState.UnderReview);
                    List<Request> requestList = new List<Request>();
                    requestList.AddRange(confirmRequestList);
                    requestList.AddRange(underReviewRequestList);
                    List<string> precardValidationsList = validateRep.GetPrecardRule(RuleGroup.ID).Select(p => p.Code).ToList<string>();
                    List<Request> requestFilteredList = requestList.Where(r => precardValidationsList.Contains(r.Precard.Code)).ToList<Request>();
                    if (request.IsEdited)
                    {
                        requestFilteredList = requestFilteredList.Where(i => i.ID != request.ID).ToList();
                    }
                    var q = from f in requestFilteredList
                            where (f.FromTime == -1000 && f.ToTime == -1000)
                            select f;

                    var List = from o in q
                               where (o.FromDate >= request.FromDate && o.ToDate > request.ToDate && o.FromDate < request.ToDate || o.FromDate <= request.FromDate && o.ToDate >= request.ToDate || o.FromDate <= request.FromDate && o.ToDate <= request.ToDate && o.ToDate > request.FromDate || o.FromDate >= request.FromDate && o.ToDate <= request.ToDate)
                               select o;
                    bool isPreventRequest = false;
                    foreach (Precard item in precardList)
                    {

                        IList<UIValidationRuleParam> paramsList = validateRep.GetParameterRule(RuleGroup.ID, item.ID, Param);
                        if (paramsList.Count > 0)
                        {
                            UIValidationRuleParam paramObj = paramsList.Where(x => !Utility.IsEmpty(x.UIValidationRuleTempParam.KeyName) && x.UIValidationRuleTempParam.KeyName.Equals("PriorityPrecard")).FirstOrDefault();
                            int priorityPrecardItem = Utility.ToInteger(paramObj.Value);
                            IList<Request> requestByPrecardList = List.Where(r => r.Precard.ID == item.ID).ToList();
                            if (priorityPrecardItem <= priorityPrecardRequest && requestByPrecardList.Count(c => c.Precard.ID == item.ID) > 0)
                                isPreventRequest = true;
                        }
                    }
                    if (isPreventRequest)
                    {
                        AddException(ExceptionResourceKeys.UIValidation_R9_OverlyDailyPrecard, " تداخل زمانی درخواست ها");
                    }
                }


            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "RequestValidator", "R9_OverlyDailyPrecard");
                throw ex;
            }
        }
        /// <summary>
        /// درخواست های ساعتی حداکثر تا ____ روز پس از پایان ماه کاری قابل ثبت باشد 
        /// </summary>

        private void R10_SaveRequestwithTimeRange(Request request, UIValidationRuleGroup RuleGroup)
        {
            string Param = "RequestRespite";
            try
            {
                if (validateRep.GetPrecardRule(RuleGroup.ID).Where(x => x.ID == request.Precard.ID).Count() > 0)
                {
                    IList<UIValidationRuleParam> parameters = validateRep.GetParameterRule(RuleGroup.ID, request.Precard.ID, Param);
                    if (Utility.IsEmpty(parameters) || parameters.Count != 1)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterCount, "تعداد پارامتر های قانون 10 با مقدار مورد انتظار نابرابر است ", ExceptionSrc);
                    }
                    UIValidationRuleParam param = parameters.Where(x => !Utility.IsEmpty(x.UIValidationRuleTempParam.KeyName) && x.UIValidationRuleTempParam.KeyName.Equals("RequestRespite")).FirstOrDefault();

                    if (param == null)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, "پارامتر با کلید RequestRespite نظر برای قانون 10 یافت نشد ", ExceptionSrc);
                    }
                    DateTime changeDate = Utility.GTSMinStandardDateTime;
                    DateTime AllowedTime = DateTime.Now;
                    Person personObj = null;
                    personObj = request.Person;
                    Person person = new PersonRepository().GetById(personObj.ID, false);
                    DateRange dateRangePersonInRequestDate = new BDateRange().GetDateRangePerson(person, 0, request.FromDate);

                    int doubleparam = Convert.ToInt32(param.Value);

                    DateTime lockfromspecificday = dateRangePersonInRequestDate.ToDate.AddDays(doubleparam);
                    if (request.IsEdited)
                    {
                        AllowedTime = request.RegisterDate;
                    }
                    if (lockfromspecificday < AllowedTime)
                    {
                        AddException(ExceptionResourceKeys.UIValidation_R10_SaveRequestwithTimeRange, "ثبت درخواست برای ماه های قبل مجاز نیست");
                    }
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "RequestValidator", "R10_SaveRequestwithTimeRange");
                throw ex;
            }
        }
        /// <summary>
        /// مقدار درخواست های ساعتی اول وقت حداقل ___ ساعت باشد
        /// </summary>
        private void R11_RequestMinLength(Request request, UIValidationRuleGroup RuleGroup)
        {
            string FirstParam = "MinLength";
            //   string SecondParam = "avalVaght";
            try
            {
                if (validateRep.GetPrecardRule(RuleGroup.ID).Where(x => x.ID == request.Precard.ID).Count() > 0)
                {
                    //  IList<UIValidationRuleParam> parameters = validateRep.GetParameterRule(RuleGroup.ID, request.Precard.ID, FirstParam, SecondParam);
                    IList<UIValidationRuleParam> parameters = validateRep.GetParameterRule(RuleGroup.ID, request.Precard.ID, FirstParam);

                    if (Utility.IsEmpty(parameters) || parameters.Count != 1)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterCount, "تعداد پارامتر های قانون 11 با مقدار مورد انتظار نابرابر است ", ExceptionSrc);
                    }
                    UIValidationRuleParam minLen = parameters.Where(x => !Utility.IsEmpty(x.UIValidationRuleTempParam.KeyName) && x.UIValidationRuleTempParam.KeyName.Equals("MinLength")).FirstOrDefault();
                    if (minLen == null)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, "پارامتر با کلید MinLength  نظر برای قانون 11 یافت نشد ", ExceptionSrc);
                    }

                    //UIValidationRuleParam avalVaght = parameters.Where(x => !Utility.IsEmpty(x.UIValidationRuleTempParam.KeyName) && x.UIValidationRuleTempParam.KeyName.Equals("avalVaght")).FirstOrDefault();
                    //if (minLen == null)
                    //{
                    //    throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, "پارامتر با کلید avalVaght  نظر برای قانون 36 یافت نشد ", ExceptionSrc);
                    //}
                    int minLength = Utility.RealTimeToIntTime(minLen.Value);
                    //     	bool isAvvalVaght = Utility.ToBoolean(avalVaght.Value);

                    Person prs = new PersonRepository(false).GetById(request.Person.ID, false);
                    prs.InitializeForAccessRules(request.FromDate.AddDays(-1), request.ToDate.AddDays(1));
                    BaseShift shift = prs.GetShiftByDate(request.FromDate);
                    if (shift != null && shift.PairCount > 0 && request.TimeDuration > 0)
                    {
                        if (shift.Pairs.OrderBy(x => x.From).First().From >= request.FromTime)
                        {
                            if (request.ToTime - request.FromTime < minLength)
                            {
                                AddException(ExceptionResourceKeys.UIValidation_R11_RequestMinLength, " مدت زمان بازه درخواست از حد مجاز کمتر است");
                            }
                        }
                    }

                    else
                    {
                        if (request.ToTime - request.FromTime < minLength && request.TimeDuration > 0)
                        {
                            AddException(ExceptionResourceKeys.UIValidation_R11_RequestMinLength, " مدت زمان بازه درخواست از حد مجاز کمتر است");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "RequestValidator", "R11_RequestMinLength");
                throw ex;
            }
        }
        /// <summary>
        /// توضیح درخواست اجباری است
        /// </summary>
        private void R12_RequestDescriptionRequierd(Request request, UIValidationRuleGroup RuleGroup)
        {
            try
            {
                if ((validateRep.GetPrecardRule(RuleGroup.ID).Where(x => x.ID == request.Precard.ID).Count() > 0) && request.IsEdited == false)
                {
                    int Length = request.Description.Length;
                    int count = 0;
                    bool ISnull = false;
                    for (int i = 0; i < Length; i++)
                    {
                        char chr = request.Description[i];
                        if (chr == ' ')
                        {
                            count++;
                        }
                    }
                    if (count == Length)
                    {
                        ISnull = true;
                    }
                    if (Utility.IsEmpty(request.Description) || ISnull)
                    {
                        AddException(ExceptionResourceKeys.UIValidation_R12_RequestDescriptionRequierd, " توضیح درخواست اجباری است");
                    }
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "RequestValidator", "R12_RequestDescriptionRequierd");
                throw ex;
            }
        }
        /// <summary>
        /// مشخص نمودن نام بیماری اجباری است 
        /// </summary>
        private void R13_IllenssRequest(Request request, UIValidationRuleGroup RuleGroup)
        {
            try
            {
                if ((validateRep.GetPrecardRule(RuleGroup.ID).Where(x => x.ID == request.Precard.ID).Count() > 0) && request.IsEdited == false)
                {
                    if (request.IllnessID == 0)
                    {
                        AddException(ExceptionResourceKeys.UIValidation_R13_IllenssRequest, " نام بیماری مشخص نشده است");
                    }
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "RequestValidator", "R13_IllenssRequest");
                throw ex;
            }
        }
        /// <summary>
        /// مشخص نمودن نام پزشک اجباری است 
        /// </summary>
        private void R14_DoctorRequest(Request request, UIValidationRuleGroup RuleGroup)
        {
            try
            {
                if ((validateRep.GetPrecardRule(RuleGroup.ID).Where(x => x.ID == request.Precard.ID).Count() > 0) && request.IsEdited == false)
                {
                    if (request.DoctorID == 0)
                    {
                        AddException(ExceptionResourceKeys.UIValidation_R14_DoctorRequest, " نام پزشک مشخص نشده است");
                    }
                }

            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "RequestValidator", "R14_DoctorRequest");
                throw ex;
            }
        }
        /// <summary>
        /// مشخص نمودن محل ماموریت اجباری است 
        /// </summary>
        private void R15_DutyPlaceRequest(Request request, UIValidationRuleGroup RuleGroup)
        {
            try
            {
                if ((validateRep.GetPrecardRule(RuleGroup.ID).Where(x => x.ID == request.Precard.ID).Count() > 0) && request.IsEdited == false)
                {
                    if (request.DutyPositionID == 0)
                    {
                        AddException(ExceptionResourceKeys.UIValidation_R15_DutyPlaceRequest, " محل ماموریت مشخص نشده است");
                    }

                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "RequestValidator", "R15_DutyPlaceRequest");
                throw ex;
            }
        }

        /// <summary>
        /// درخواستهای روزانه از___ روز قبل تا ___ روز بعد از روز درخواست قابل ثبت باشد 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="parameters"></param>
        private void R16_DailyTrafficRequest(Request request, UIValidationRuleGroup RuleGroup)
        {
            string FirstParam = "BeforeDayCount";
            string SecondParam = "AfterDayCount";
            try
            {
                if (validateRep.GetPrecardRule(RuleGroup.ID).Where(x => x.ID == request.Precard.ID).Count() > 0)
                {
                    IList<UIValidationRuleParam> parameters = validateRep.GetParameterRule(RuleGroup.ID, request.Precard.ID, FirstParam, SecondParam);
                    if (Utility.IsEmpty(parameters) || parameters.Count != 2)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterCount, "تعداد پارامتر های قانون 16 با مقدار مورد انتظار نابرابر است ", ExceptionSrc);
                    }
                    UIValidationRuleParam param = parameters.Where(x => !Utility.IsEmpty(x.UIValidationRuleTempParam.KeyName) && x.UIValidationRuleTempParam.KeyName.Equals("BeforeDayCount")).FirstOrDefault();
                    if (param == null)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, "پارامتر با کلید BeforeDayCount  نظر برای قانون 16 یافت نشد ", ExceptionSrc);
                    }
                    int before = Utility.ToInteger(param.Value);
                    param = parameters.Where(x => !Utility.IsEmpty(x.UIValidationRuleTempParam.KeyName) && x.UIValidationRuleTempParam.KeyName.Equals("AfterDayCount")).FirstOrDefault();
                    if (param == null)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, "پارامتر با کلید AfterDayCount  نظر برای قانون 16 یافت نشد ", ExceptionSrc);
                    }
                    int after = Utility.ToInteger(param.Value);

                    DateTime requestFromDate = request.FromDate.Date;
                    DateTime requestToDate = request.ToDate.Date;
                    DateTime registerDate = DateTime.Now.Date;
                    if (request.IsEdited)
                    {
                        requestFromDate = request.FromDate.Date;
                        registerDate = request.RegisterDate;
                    }
                    if ((requestFromDate - registerDate).Days > before)
                    {
                        AddException(ExceptionResourceKeys.UIValidation_R16_DailyTrafficRequest, "هنوز زمان ثبت درخواست برای تاریخ انتخابی فرانرسیده است");
                    }
                    if ((registerDate - requestToDate).Days > after)
                    {
                        AddException(ExceptionResourceKeys.UIValidation_R16_DailyTrafficRequest, "مهلت ثبت درخواست به پایان رسیده است");
                    }
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "RequestValidator", "R16_DailyTrafficRequest");
                throw ex;
            }
        }
        /// <summary>
        /// مقدار درخواست های ساعتی حداکثر ____ ساعت در ماه باشد 
        /// </summary>
        private void R17_MaxAmountHourlyRequest(Request request, UIValidationRuleGroup RuleGroup)
        {
            string Param = "MaxHrours";
            try
            {
                if (validateRep.GetPrecardRule(RuleGroup.ID).Where(x => x.ID == request.Precard.ID).Count() > 0)
                {
                    IList<UIValidationRuleParam> parameters = validateRep.GetParameterRule(RuleGroup.ID, request.Precard.ID, Param);
                    if (Utility.IsEmpty(parameters) || parameters.Count != 1)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterCount, "تعداد پارامتر های قانون 17 با مقدار مورد انتظار نابرابر است ", ExceptionSrc);
                    }
                    UIValidationRuleParam param = parameters.Where(x => !Utility.IsEmpty(x.UIValidationRuleTempParam.KeyName) && x.UIValidationRuleTempParam.KeyName.Equals("MaxHrours")).FirstOrDefault();
                    if (param == null)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, "پارامتر با کلید MaxHrours  نظر برای قانون 17 یافت نشد ", ExceptionSrc);
                    }
                    //int maxHour = Utility.ToInteger(param.Value) * 60;
                    int maxHour = Utility.RealTimeToIntTime(param.Value);
                    DateTime requestDate = request.FromDate.Date;
                    DateTime startMonth, endMonth;


                    Person person = new PersonRepository().GetById(request.Person.ID, false);
                    DateRange dateRangePersonInRequestDate = new BDateRange().GetDateRangePerson(person, 0, requestDate);
                    startMonth = dateRangePersonInRequestDate.FromDate;
                    endMonth = dateRangePersonInRequestDate.ToDate;

                    IList<InfoRequest> list = requestRepository.GetActiveRequestDateValues(request.Person.ID, request.Precard.ID, startMonth, endMonth);
                    if (request.IsEdited)
                    {
                        list = list.Where(i => i.ID != request.ID).ToList();
                    }
                    var sum1 = from o in list
                               where o.TimeDuration > 0
                               select o.TimeDuration;

                    //var sum2 = from o in list
                    //           where o.TimeDuration == -1000
                    //           select o.ToTime - o.FromTime;

                    //int newDuration = request.TimeDuration > 0 ? request.TimeDuration : request.ToTime - request.FromTime;
                    int newDuration = request.TimeDuration;

                    if (sum1.Sum() + newDuration > maxHour)
                    {
                        AddException(ExceptionResourceKeys.UIValidation_R17_MaxAmountHourlyRequest, " مقدار ساعت درخواست از حداکثر مجاز تجاوز کرده است");
                    }
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "RequestValidator", "R17_MaxAmountHourlyRequest");
                throw ex;
            }
        }
        /// <summary>
        /// تعداد درخواستهای روزانه در سال حداکثر ___ عدد باشد
        /// </summary>
        private void R18_MaxDailyRequestInYear(Request request, UIValidationRuleGroup RuleGroup)
        {
            string Param = "MaxCount";
            try
            {
                if (validateRep.GetPrecardRule(RuleGroup.ID).Where(x => x.ID == request.Precard.ID).Count() > 0)
                {
                    IList<UIValidationRuleParam> parameters = validateRep.GetParameterRule(RuleGroup.ID, request.Precard.ID, Param);
                    if (Utility.IsEmpty(parameters) || parameters.Count != 1)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterCount, "تعداد پارامتر های قانون 18 با مقدار مورد انتظار نابرابر است ", ExceptionSrc);
                    }
                    UIValidationRuleParam param = parameters.Where(x => !Utility.IsEmpty(x.UIValidationRuleTempParam.KeyName) && x.UIValidationRuleTempParam.KeyName.Equals("MaxCount")).FirstOrDefault();
                    if (param == null)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, "پارامتر با کلید MaxCount  نظر برای قانون 18 یافت نشد ", ExceptionSrc);
                    }
                    int maxCount = Utility.ToInteger(param.Value);
                    DateTime requestDate = request.FromDate.Date;
                    DateTime startYear, endYear;


                    if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                    {
                        startYear = Utility.GetDateOfBeginYear(requestDate, LanguagesName.Parsi);
                        endYear = Utility.GetDateOfEndYear(requestDate, LanguagesName.Parsi);
                    }
                    else
                    {
                        startYear = Utility.GetDateOfBeginYear(requestDate, LanguagesName.English);
                        endYear = Utility.GetDateOfEndYear(requestDate, LanguagesName.English);
                    }
                    IList<GTS.Clock.Model.RequestFlow.Request> confirmlist = null;
                    GTS.Clock.Model.RequestFlow.Request req = null;
                    RequestStatus reqStatus = null;
                    RequestStatus existsReqStatus = null;
                    List<Request> requestList = new List<Request>();
                    IList<GTS.Clock.Model.RequestFlow.Request> list = null;
                    //      int count = requestRepository.GetActiveRequestCount(request.Person.ID, request.Precard.ID, startYear, endYear);
                    confirmlist = requestRepository.NHibernateSession.QueryOver<GTS.Clock.Model.RequestFlow.Request>(() => req)
                                         .JoinAlias(() => req.RequestStatusList, () => reqStatus)
                                         .Where(() => req.FromDate >= startYear && req.FromDate <= endYear)
                                         .And(() => req.Precard.ID == request.Precard.ID)
                                         .And(() => req.Person.ID == request.Person.ID)
                                         .Where(() => reqStatus.Confirm && reqStatus.EndFlow && !reqStatus.IsDeleted)
                                         .List<GTS.Clock.Model.RequestFlow.Request>();
                    var existing = QueryOver.Of<RequestStatus>(() => existsReqStatus)
                .Where(() => existsReqStatus.EndFlow == true)
                .And(() => existsReqStatus.Request.ID == req.ID)
                .Select(x => x.ID);
                    Permit permit = null;
                    IList<Permit> existingPermitRequestList = requestRepository.NHibernateSession.QueryOver<Permit>(() => permit)
                                                                         .Where(() => permit.Person.ID == request.Person.ID)
                                                                          .And(() => permit.FromDate >= startYear && permit.FromDate <= endYear)
                                                                         .List<Permit>();
                    List<decimal> requestHavePermitIdsList = new List<decimal>();
                    foreach (Permit item in existingPermitRequestList)
                    {
                        requestHavePermitIdsList.AddRange(item.Pairs.Select(p => p.RequestID).ToList<decimal>());
                    }
                    requestHavePermitIdsList = requestHavePermitIdsList.Distinct().ToList<decimal>();
                    list = requestRepository.NHibernateSession.QueryOver<GTS.Clock.Model.RequestFlow.Request>(() => req)
                                                  .Where(() => req.FromDate >= startYear && req.FromDate <= endYear)
                                                  .And(() => req.Person.ID == request.Person.ID)
                                                  .And(() => req.Precard.ID == request.Precard.ID)
                                                  .And(() => !req.EndFlow)
                                                  .And(() => !req.ID.IsIn(requestHavePermitIdsList))
                                                  .WithSubquery.WhereNotExists(existing).List<Request>();

                    requestList.AddRange(confirmlist);
                    requestList.AddRange(list);
                    int count = requestList.Count();
                    if (request.IsEdited)
                    {
                        var requestlist = (from n in requestList
                                           where (request.ID == n.ID)
                                           select n).ToList();
                        if (requestlist.Count >= 1)
                        {
                            count = count - 1;
                        }
                    }
                    if (count + (request.ToDate - request.FromDate).Days + 1 > maxCount)
                    {
                        AddException(ExceptionResourceKeys.UIValidation_R18_MaxDailyRequestInYear, " تعداد درخواست از حد مجاز تجاوز کرده است");
                    }
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "RequestValidator", "R18_MaxDailyRequestInYear");
                throw ex;
            }
        }
        /// <summary>
        /// تعداد درخواستهای  روزانه در ماه حداکثر ___ عدد باشد
        /// </summary>
        private void R19_MaxDailyRequestCountInMonth(Request request, UIValidationRuleGroup RuleGroup)
        {
            string Param = "MaxCountInMonth";
            try
            {
                if (validateRep.GetPrecardRule(RuleGroup.ID).Where(x => x.ID == request.Precard.ID).Count() > 0)
                {
                    IList<UIValidationRuleParam> parameters = validateRep.GetParameterRule(RuleGroup.ID, request.Precard.ID, Param);
                    if (Utility.IsEmpty(parameters) || parameters.Count != 1)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterCount, "تعداد پارامتر های قانون 19 با مقدار مورد انتظار نابرابر است ", ExceptionSrc);
                    }
                    UIValidationRuleParam param = parameters.Where(x => !Utility.IsEmpty(x.UIValidationRuleTempParam.KeyName) && x.UIValidationRuleTempParam.KeyName.Equals("MaxCountInMonth")).FirstOrDefault();
                    if (param == null)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, "پارامتر با کلید MaxCountInMonth  نظر برای قانون 19 یافت نشد ", ExceptionSrc);
                    }
                    int maxCount = Utility.ToInteger(param.Value);
                    DateTime requestDate = request.FromDate.Date;
                    DateTime startMonth, endMonth;
                    Person person = new PersonRepository().GetById(request.Person.ID, false);
                    DateRange dateRangePersonInRequestDate = new BDateRange().GetDateRangePerson(person, 0, requestDate);
                    startMonth = dateRangePersonInRequestDate.FromDate;
                    endMonth = dateRangePersonInRequestDate.ToDate;
                    //     int count = requestRepository.GetActiveRequestCount(request.Person.ID, request.Precard.ID, startMonth, endMonth);
                    IList<GTS.Clock.Model.RequestFlow.Request> confirmlist = null;
                    GTS.Clock.Model.RequestFlow.Request req = null;
                    RequestStatus reqStatus = null;
                    RequestStatus existsReqStatus = null;
                    List<Request> requestList = new List<Request>();
                    IList<GTS.Clock.Model.RequestFlow.Request> list = null;
                    confirmlist = requestRepository.NHibernateSession.QueryOver<GTS.Clock.Model.RequestFlow.Request>(() => req)
                                         .JoinAlias(() => req.RequestStatusList, () => reqStatus)
                                         .Where(() => req.FromDate >= startMonth && req.FromDate <= endMonth)
                                         .And(() => req.Precard.ID == request.Precard.ID)
                                         .And(() => req.Person.ID == request.Person.ID)
                                         .Where(() => reqStatus.Confirm && reqStatus.EndFlow && !reqStatus.IsDeleted)
                                         .List<GTS.Clock.Model.RequestFlow.Request>();
                    var existing = QueryOver.Of<RequestStatus>(() => existsReqStatus)
                .Where(() => existsReqStatus.EndFlow == true)
                .And(() => existsReqStatus.Request.ID == req.ID)
                .Select(x => x.ID);
                    Permit permit = null;
                    IList<Permit> existingPermitRequestList = requestRepository.NHibernateSession.QueryOver<Permit>(() => permit)
                                                                         .Where(() => permit.Person.ID == request.Person.ID)
                                                                          .And(() => permit.FromDate >= startMonth && permit.FromDate <= endMonth)
                                                                         .List<Permit>();
                    List<decimal> requestHavePermitIdsList = new List<decimal>();
                    foreach (Permit item in existingPermitRequestList)
                    {
                        requestHavePermitIdsList.AddRange(item.Pairs.Select(p => p.RequestID).ToList<decimal>());
                    }
                    requestHavePermitIdsList = requestHavePermitIdsList.Distinct().ToList<decimal>();
                    list = requestRepository.NHibernateSession.QueryOver<GTS.Clock.Model.RequestFlow.Request>(() => req)
                                                  .Where(() => req.FromDate >= startMonth && req.FromDate <= endMonth)
                                                  .And(() => req.Person.ID == request.Person.ID)
                                                  .And(() => req.Precard.ID == request.Precard.ID)
                                                  .And(() => !req.EndFlow)
                                                  .And(() => !req.ID.IsIn(requestHavePermitIdsList))
                                                  .WithSubquery.WhereNotExists(existing).List<Request>();

                    requestList.AddRange(confirmlist);
                    requestList.AddRange(list);
                    int count = requestList.Count();
                    if (request.IsEdited)
                    {
                        var requestlist = (from n in requestList
                                           where (request.ID == n.ID)
                                           select n).ToList();
                        if (requestlist.Count >= 1)
                        {
                            count = count - 1;
                        }
                    }
                    if (count + 1 > maxCount)
                    {

                        AddException(ExceptionResourceKeys.UIValidation_R19_MaxDailyRequestCountInMonth, " تعداد درخواست از حداکثر مجاز تجاوز کرده است");

                    }
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "RequestValidator", "R19_MaxDailyRequestCountInMonth");
                throw ex;
            }
        }
        /// <summary>
        ///  تعداد درخواست های ساعتی در روز حداکثر ____ عدد باشد
        /// </summary>
        /// <param name="request"></param>
        /// <param name="grouping"></param>
        private void R20_MaxHourlyRequestCountInDay(Request request, UIValidationRuleGroup RuleGroup)
        {
            string Param = "MaxCountInDay";
            try
            {
                if (validateRep.GetPrecardRule(RuleGroup.ID).Where(x => x.ID == request.Precard.ID).Count() > 0)
                {
                    IList<UIValidationRuleParam> parameters = validateRep.GetParameterRule(RuleGroup.ID, request.Precard.ID, Param);
                    if (Utility.IsEmpty(parameters) || parameters.Count != 1)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterCount, "تعداد پارامتر های قانون 20 با مقدار مورد انتظار نابرابر است ", ExceptionSrc);
                    }
                    UIValidationRuleParam param = parameters.Where(x => !Utility.IsEmpty(x.UIValidationRuleTempParam.KeyName) && x.UIValidationRuleTempParam.KeyName.Equals("MaxCountInDay")).FirstOrDefault();
                    if (param == null)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, "پارامتر با کلید MaxCountInDay  نظر برای قانون 20 یافت نشد ", ExceptionSrc);
                    }
                    int maxCount = Utility.ToInteger(param.Value);
                    DateTime requestDate = request.FromDate.Date;

                    // int count = requestRepository.GetActiveRequestCount(request.Person.ID, request.Precard.ID, requestDate, requestDate);
                    IList<GTS.Clock.Model.RequestFlow.Request> confirmlist = null;
                    GTS.Clock.Model.RequestFlow.Request req = null;
                    RequestStatus reqStatus = null;
                    RequestStatus existsReqStatus = null;
                    List<Request> requestList = new List<Request>();
                    IList<GTS.Clock.Model.RequestFlow.Request> list = null;
                    confirmlist = requestRepository.NHibernateSession.QueryOver<GTS.Clock.Model.RequestFlow.Request>(() => req)
                                         .JoinAlias(() => req.RequestStatusList, () => reqStatus)
                                         .Where(() => req.FromDate >= requestDate && req.FromDate <= requestDate)
                                         .And(() => req.Precard.ID == request.Precard.ID)
                                         .And(() => req.Person.ID == request.Person.ID)
                                         .Where(() => reqStatus.Confirm && reqStatus.EndFlow && !reqStatus.IsDeleted)
                                         .List<GTS.Clock.Model.RequestFlow.Request>();
                    var existing = QueryOver.Of<RequestStatus>(() => existsReqStatus)
                .Where(() => existsReqStatus.EndFlow == true)
                .And(() => existsReqStatus.Request.ID == req.ID)
                .Select(x => x.ID);
                    Permit permit = null;
                    IList<Permit> existingPermitRequestList = requestRepository.NHibernateSession.QueryOver<Permit>(() => permit)
                                                                         .Where(() => permit.Person.ID == request.Person.ID)
                                                                          .And(() => permit.FromDate >= requestDate && permit.FromDate <= requestDate)
                                                                         .List<Permit>();
                    List<decimal> requestHavePermitIdsList = new List<decimal>();
                    foreach (Permit item in existingPermitRequestList)
                    {
                        requestHavePermitIdsList.AddRange(item.Pairs.Select(p => p.RequestID).ToList<decimal>());
                    }
                    requestHavePermitIdsList = requestHavePermitIdsList.Distinct().ToList<decimal>();
                    list = requestRepository.NHibernateSession.QueryOver<GTS.Clock.Model.RequestFlow.Request>(() => req)
                                                  .Where(() => req.FromDate >= requestDate && req.FromDate <= requestDate)
                                                  .And(() => req.Person.ID == request.Person.ID)
                                                  .And(() => req.Precard.ID == request.Precard.ID)
                                                  .And(() => !req.EndFlow)
                                                  .And(() => !req.ID.IsIn(requestHavePermitIdsList))
                                                  .WithSubquery.WhereNotExists(existing).List<Request>();

                    requestList.AddRange(confirmlist);
                    requestList.AddRange(list);
                    int count = requestList.Count();
                    if (request.IsEdited)
                    {
                        var requestlist = (from n in requestList
                                           where (request.ID == n.ID)
                                           select n).ToList();
                        if (requestlist.Count >= 1)
                        {
                            count = count - 1;
                        }
                    }
                    if (count + 1 > maxCount)
                    {
                        AddException(ExceptionResourceKeys.UIValidation_R20_MaxHourlyRequestCountInDay, " تعداد درخواست از حداکثر مجاز تجاوز کرده است");
                    }
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "RequestValidator", "R20_MaxHourlyRequestCountInDay");
                throw ex;
            }
        }
        /// <summary>
        /// حداکثر تعداد درخواست توسط اپراتور
        /// </summary>
        private void R21_OperatorRequestMaxCount(Request request, UIValidationRuleGroup RuleGroup)
        {
            string Param = "OperatorRequestMaxCount";
            try
            {
                if (validateRep.GetPrecardRule(RuleGroup.ID).Where(x => x.ID == request.Precard.ID).Count() > 0)
                {
                    IList<UIValidationRuleParam> parameters = validateRep.GetParameterRule(RuleGroup.ID, request.Precard.ID, Param);
                    if (Utility.IsEmpty(parameters) || parameters.Count != 1)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterCount, "تعداد پارامتر های قانون 21 با مقدار مورد انتظار نابرابر است ", ExceptionSrc);
                    }
                    UIValidationRuleParam operatorRequestMaxCount = parameters.Where(x => !Utility.IsEmpty(x.UIValidationRuleTempParam.KeyName) && x.UIValidationRuleTempParam.KeyName.Equals("OperatorRequestMaxCount")).FirstOrDefault();
                    if (operatorRequestMaxCount == null)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, "پارامتر با کلید OperatorRequestMaxCount برای قانون 21 یافت نشد", ExceptionSrc);
                    }
                    int oprRequestMaxCount = Utility.ToInteger(operatorRequestMaxCount.Value);
                    DateTime requestDate = request.FromDate.Date;
                    DateTime startMonth, endMonth;
                    Person person = new PersonRepository().GetById(request.Person.ID, false);
                    DateRange dateRangePersonInRequestDate = new BDateRange().GetDateRangePerson(person, 0, requestDate);
                    startMonth = dateRangePersonInRequestDate.FromDate;
                    endMonth = dateRangePersonInRequestDate.ToDate;
                    int operatorIssuedRequestsCount = this.requestRepository.GetOperatorActiveRequestCount(BUser.CurrentUser.ID, RuleGroup.ValidationGroup, request.Precard.ID, startMonth, endMonth);
                    if (operatorIssuedRequestsCount >= oprRequestMaxCount)
                        AddException(ExceptionResourceKeys.UIValidation_R21_OperatorRequestMaxCount, "تعداد درخواست اپراتور از حد مجاز تجاوز کرده است");
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "RequestValidator", "R21_OperatorRequestMaxCount");
                throw ex;
            }


        }
        /// <summary>
        /// اعمال مانده مرخصی تا پایان ماه در درخواست های مرخصی
        /// </summary>
        /// <param name="request"></param>
        /// <param name="grouping"></param>
        private void R22_RequestRemainLeaveToEndMonth(Request request, UIValidationRuleGroup RuleGroup)
        {
            try
            {

                if (request.IsExecuteWarningUIValidation)
                {
                    DateTime fromDateRequest, endMonth;
                    Person prs = new PersonRepository(false).GetById(request.Person.ID, false);
                    CFP cfpPersonObj = cfpRepository.GetByPersonID(prs.ID);
                    DateTime LockCalculationDate = GetCalculationLockDate(prs.ID);
                    if (LockCalculationDate >= cfpPersonObj.Date)
                        fromDateRequest = LockCalculationDate.AddDays(1);
                    else
                    {
                        fromDateRequest = cfpPersonObj.Date;
                    }
                    IList<LeaveCalcResult> leaveCalcResultList = prs.LeaveCalcResultList;//;.Where(l => l.Date >= fromDateRequest && l.Type.Trim().ToLower() == "uld").ToList();
                    prs.InitializeForAccessRules(request.FromDate, request.ToDate);
                    object parameterRuleObj = GetRuleParameter(request.ToDate, prs, 3017, "First");
                    int leaveMinuteInDay = 0;
                    if (parameterRuleObj != null && Convert.ToInt32(parameterRuleObj) != 0)
                    {
                        leaveMinuteInDay = Convert.ToInt32(parameterRuleObj);
                    }
                    else
                    {
                        //7.30 ساعت به طور پیش فرض گرفته شده
                        leaveMinuteInDay = (int)((7 * 60) + 30);
                    }


                    int yearRequest = 0;
                    int monthRequest = 0;
                    if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                    {
                        yearRequest = Utility.ToPersianDateTime(request.ToDate).Year;
                        monthRequest = Utility.ToPersianDateTime(request.ToDate).Month;
                    }
                    else
                    {
                        yearRequest = request.ToDate.Year;
                        monthRequest = request.ToDate.Month;
                    }


                    endMonth = Utility.GetEndOfPersianMonth(request.ToDate);

                    DateTime endDateYear = Utility.GetDateOfEndYear(DateTime.Now, BLanguage.CurrentSystemLanguage);
                    IList<InfoRequest> requestUnderviewList = RegisteredRequestsBusiness.GetAllRequests(prs.ID, RequestState.UnderReview, LockCalculationDate, endMonth);
                    IList<InfoRequest> requestConfirmedList = RegisteredRequestsBusiness.GetAllRequests(prs.ID, RequestState.Confirmed, fromDateRequest, endMonth);

                    IList<InfoRequest> requestRejectDeletedIsInCalcList = RegisteredRequestsBusiness.GetAllRequests(prs.ID, RequestState.Unconfirmed, fromDateRequest, endMonth);
                    ((List<InfoRequest>)(requestRejectDeletedIsInCalcList)).AddRange(RegisteredRequestsBusiness.GetAllRequests(prs.ID, RequestState.Deleted, fromDateRequest, endMonth));

                    //if (leaveCalcResultList.Count > 0)                 
                    //    requestRejectDeletedIsInCalcList = requestRejectDeletedIsInCalcList.Where(r => r.ToDate <= leaveCalcResultList.Max(m => m.Date)).ToList();
                    //else
                    //    requestRejectDeletedIsInCalcList=new List<InfoRequest>();
                    int registeredRequestDaily = 0;
                    int registeredRequestHourly = 0;

                    foreach (InfoRequest item in requestUnderviewList)
                    {
                        if (item.PrecardID == 41 && item.FromDate > LockCalculationDate)
                        {
                            registeredRequestDaily += (item.ToDate - item.FromDate).Days + 1;
                        }
                        else if (item.PrecardID == 21 && item.FromDate > LockCalculationDate)
                        {
                            registeredRequestHourly += (item.ToTime - item.FromTime);
                        }

                    }

                    List<decimal> permitList = new List<decimal>();
                    Permit permit = null;
                    UsedLeaveDetail usedLeaveDetail = null;
                    IList<UsedLeaveDetail> usedLeaveDetailList = NHSession.QueryOver(() => usedLeaveDetail).JoinAlias(() => usedLeaveDetail.Permit, () => permit).Where(() => usedLeaveDetail.Person.ID == prs.ID).List<UsedLeaveDetail>();
                    //List<decimal> usedLeavePermitIdList = new List<decimal>();
                    //usedLeavePermitIdList.AddRange(usedLeaveDetailList.Select(x => x.Permit.ID));




                    foreach (InfoRequest item in requestConfirmedList)
                    {
                        Permit permitObj = new BPermit().GetExistingPermit(item.ID);
                        if (permitObj != null)
                        {
                            for (var day = item.FromDate; day <= item.ToDate; day = day.AddDays(1))
                            {
                                if (usedLeaveDetailList.Count(c => c.Permit != null && c.Permit.ID == permitObj.ID && c.Date == day) == 0)
                                {
                                    if (item.PrecardID == 41)
                                    {
                                        registeredRequestDaily += 1;
                                    }
                                    else if (item.PrecardID == 21)
                                    {
                                        registeredRequestHourly += (item.ToTime - item.FromTime);
                                    }
                                }

                            }
                        }


                    }
                    foreach (InfoRequest item in requestRejectDeletedIsInCalcList)
                    {
                        Permit permitObj = new BPermit().GetExistingPermit(item.ID);
                        if (permitObj != null)
                        {
                            for (var day = item.FromDate; day <= item.ToDate; day = day.AddDays(1))
                            {
                                if (usedLeaveDetailList.Count(c => c.Permit != null && c.Permit.ID == permitObj.ID && c.Date == day) >= 0)
                                    if (item.PrecardID == 41 && day > LockCalculationDate)
                                    {
                                        registeredRequestDaily -= 1;
                                    }
                                    else if (item.PrecardID == 21 && day > LockCalculationDate)
                                    {
                                        registeredRequestHourly -= (item.ToTime - item.FromTime);
                                    }
                            }
                        }

                    }
                    int allRegisteredRequestIsNotInRemainLeave = (registeredRequestDaily * leaveMinuteInDay) + registeredRequestHourly;
                    int minuteRemainLeaveToEndMonth = 0;
                    new BRemainLeave().GetRemainLeaveToEndOfMonth(request.Person.ID, yearRequest, monthRequest, out minuteRemainLeaveToEndMonth);
                    int requestDays = 0;
                    if (request.Precard.ID == 41)
                    {
                        for (var day = request.FromDate; day <= request.ToDate; day = day.AddDays(1))
                        {
                            requestDays += 1;
                        }
                        if ((requestDays * leaveMinuteInDay) > (minuteRemainLeaveToEndMonth - allRegisteredRequestIsNotInRemainLeave))
                        {

                            if (RuleGroup.Warning)
                            {
                                AddWarning(ExceptionResourceKeys.UIValidation_R22_RequestRemainLeaveToEndMonth, "  مجموع مقدار درخواستی و درخواست های درحال بررسی از مانده مرخصی بیشتر است");
                            }
                            else
                            {
                                AddException(ExceptionResourceKeys.UIValidation_R22_RequestRemainLeaveToEndMonth, "  مجموع مقدار درخواستی و درخواست های درحال بررسی از مانده مرخصی بیشتر است");
                            }
                        }
                    }
                    else if (request.Precard.ID == 21)
                    {
                        if ((request.ToTime - request.FromTime) > (minuteRemainLeaveToEndMonth - allRegisteredRequestIsNotInRemainLeave))
                        {
                            if (RuleGroup.Warning)
                            {
                                AddWarning(ExceptionResourceKeys.UIValidation_R22_RequestRemainLeaveToEndMonth, "  مجموع مقدار درخواستی و درخواست های درحال بررسی از مانده مرخصی بیشتر است");
                            }
                            else
                            {
                                AddException(ExceptionResourceKeys.UIValidation_R22_RequestRemainLeaveToEndMonth, "  مجموع مقدار درخواستی و درخواست های درحال بررسی از مانده مرخصی بیشتر است");
                            }

                        }
                    }

                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "RequestValidator", "R22_RequestRemainLeaveToEndMonth");
                throw ex;

            }
        }
        /// <summary>
        /// درخواست های روزانه حداکثر تا ____ روز پس از پایان ماه کاری قابل ثبت باشد 
        /// </summary>

        private void R23_SaveDailyRequestwithTimeRange(Request request, UIValidationRuleGroup RuleGroup)
        {
            string Param = "RequestRespiteDaily";
            try
            {
                if (validateRep.GetPrecardRule(RuleGroup.ID).Where(x => x.ID == request.Precard.ID).Count() > 0)
                {
                    IList<UIValidationRuleParam> parameters = validateRep.GetParameterRule(RuleGroup.ID, request.Precard.ID, Param);
                    if (Utility.IsEmpty(parameters) || parameters.Count != 1)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterCount, "تعداد پارامتر های قانون 23 با مقدار مورد انتظار نابرابر است ", ExceptionSrc);
                    }
                    UIValidationRuleParam param = parameters.Where(x => !Utility.IsEmpty(x.UIValidationRuleTempParam.KeyName) && x.UIValidationRuleTempParam.KeyName.Equals("RequestRespiteDaily")).FirstOrDefault();

                    if (param == null)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, "پارامتر با کلید RequestRespiteDaily نظر برای قانون 23 یافت نشد ", ExceptionSrc);
                    }
                    DateTime changeDate = Utility.GTSMinStandardDateTime;

                    Person personObj = null;
                    personObj = request.Person;
                    Person person = new PersonRepository().GetById(personObj.ID, false);
                    DateRange dateRangePersonInRequestDate = new BDateRange().GetDateRangePerson(person, 0, request.FromDate);

                    int doubleparam = Convert.ToInt32(param.Value);

                    DateTime lockfromspecificday = dateRangePersonInRequestDate.ToDate.AddDays(doubleparam);
                    DateTime DateTime = DateTime.Now;
                    if (request.IsEdited)
                    {
                        DateTime = request.RegisterDate;
                    }
                    if (lockfromspecificday < DateTime.Date)
                    {
                        AddException(ExceptionResourceKeys.UIValidation_R23_SaveDailyRequestwithTimeRange, "ثبت درخواست برای ماه های قبل مجاز نیست");
                    }
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "RequestValidator", "R23_SaveDailyRequestwithTimeRange");
                throw ex;
            }
        }
        /// <summary>
        /// ثبت درخواست های روزانه برای روزهای هفته مجاز نیست
        /// </summary>
        private void R24_DailyRequestInWeekdays(Request request, UIValidationRuleGroup RuleGroup)
        {
            string FirstParam = "Thursday";
            string SecondParam = "satyrday";
            string ThirdParam = "Monday";
            string ForthParam = "Thuesday";
            string FifthParam = "Wednsday";
            string SixthParam = "Sunday";
            string SeventhParam = "Friday";

            try
            {
                if (validateRep.GetPrecardRule(RuleGroup.ID).Where(x => x.ID == request.Precard.ID).Count() > 0)
                {
                    IList<UIValidationRuleParam> parameters = validateRep.GetParameterRule(RuleGroup.ID, request.Precard.ID, FirstParam, SecondParam, ThirdParam, ForthParam, FifthParam, SixthParam, SeventhParam);
                    if (Utility.IsEmpty(parameters) || parameters.Count != 7)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterCount, "تعداد پارامتر های قانون 24 با مقدار مورد انتظار نابرابر است ", ExceptionSrc);
                    }
                    UIValidationRuleParam param = parameters.Where(x => !Utility.IsEmpty(x.UIValidationRuleTempParam.KeyName) && x.UIValidationRuleTempParam.KeyName.Equals("Thursday")).FirstOrDefault();
                    if (param == null)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, "پارامتر با کلید Thursday  نظر برای قانون 24 یافت نشد ", ExceptionSrc);
                    }
                    int AllowThursday = Utility.ToInteger(param.Value);

                    param = parameters.Where(x => !Utility.IsEmpty(x.UIValidationRuleTempParam.KeyName) && x.UIValidationRuleTempParam.KeyName.Equals("satyrday")).FirstOrDefault();
                    if (param == null)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, "پارامتر با کلید satyrday  نظر برای قانون 24 یافت نشد ", ExceptionSrc);
                    }
                    int AllowSaturday = Utility.ToInteger(param.Value);

                    param = parameters.Where(x => !Utility.IsEmpty(x.UIValidationRuleTempParam.KeyName) && x.UIValidationRuleTempParam.KeyName.Equals("Monday")).FirstOrDefault();
                    if (param == null)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, "پارامتر با کلید Monday  نظر برای قانون 24 یافت نشد ", ExceptionSrc);
                    }
                    int AllowMonday = Utility.ToInteger(param.Value);

                    param = parameters.Where(x => !Utility.IsEmpty(x.UIValidationRuleTempParam.KeyName) && x.UIValidationRuleTempParam.KeyName.Equals("Thuesday")).FirstOrDefault();
                    if (param == null)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, "پارامتر با کلید Thuesday  نظر برای قانون 24 یافت نشد ", ExceptionSrc);
                    }
                    int AllowThuesday = Utility.ToInteger(param.Value);

                    param = parameters.Where(x => !Utility.IsEmpty(x.UIValidationRuleTempParam.KeyName) && x.UIValidationRuleTempParam.KeyName.Equals("Wednsday")).FirstOrDefault();
                    if (param == null)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, "پارامتر با کلید Wednsday  نظر برای قانون 24 یافت نشد ", ExceptionSrc);
                    }
                    int AllowWednsday = Utility.ToInteger(param.Value);


                    param = parameters.Where(x => !Utility.IsEmpty(x.UIValidationRuleTempParam.KeyName) && x.UIValidationRuleTempParam.KeyName.Equals("Sunday")).FirstOrDefault();
                    if (param == null)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, "پارامتر با کلید Sunday  نظر برای قانون 24 یافت نشد ", ExceptionSrc);
                    }
                    int AllowSunday = Utility.ToInteger(param.Value);


                    param = parameters.Where(x => !Utility.IsEmpty(x.UIValidationRuleTempParam.KeyName) && x.UIValidationRuleTempParam.KeyName.Equals("Friday")).FirstOrDefault();
                    if (param == null)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, "پارامتر با کلید Friday  نظر برای قانون 24 یافت نشد ", ExceptionSrc);
                    }
                    int AllowFriday = Utility.ToInteger(param.Value);

                    if (AllowThursday == 1)
                    {
                        for (var day = request.FromDate; day.Date <= request.ToDate; day = day.AddDays(1))
                        {
                            string DayOfRequest = day.DayOfWeek.ToString();
                            if (DayOfRequest == "Thursday")
                            {
                                AddException(ExceptionResourceKeys.UIValidation_R24_DailyRequestInWeekdays, " ثبت درخواست برای روز پنجشنبه مجاز نیست");
                            }
                        }
                    }

                    if (AllowSaturday == 1)
                    {
                        for (var day = request.FromDate; day.Date <= request.ToDate; day = day.AddDays(1))
                        {
                            string DayOfRequest = day.DayOfWeek.ToString();
                            if (DayOfRequest == "Saturday")
                            {
                                AddException(ExceptionResourceKeys.UIValidation_R24_DailyRequestInWeekdays, " ثبت درخواست  برای روز شنبه مجاز نیست");
                            }
                        }
                    }

                    if (AllowSunday == 1)
                    {
                        for (var day = request.FromDate; day.Date <= request.ToDate; day = day.AddDays(1))
                        {
                            string DayOfRequest = day.DayOfWeek.ToString();
                            if (DayOfRequest == "Sunday")
                            {
                                AddException(ExceptionResourceKeys.UIValidation_R24_DailyRequestInWeekdays, " ثبت درخواست برای روز یکشنبه مجاز نیست");
                            }
                        }
                    }

                    if (AllowMonday == 1)
                    {
                        for (var day = request.FromDate; day.Date <= request.ToDate; day = day.AddDays(1))
                        {
                            string DayOfRequest = day.DayOfWeek.ToString();
                            if (DayOfRequest == "Monday")
                            {
                                AddException(ExceptionResourceKeys.UIValidation_R24_DailyRequestInWeekdays, " ثبت درخواست برای روز دوشنبه مجاز نیست");
                            }
                        }
                    }

                    if (AllowThuesday == 1)
                    {
                        for (var day = request.FromDate; day.Date <= request.ToDate; day = day.AddDays(1))
                        {
                            string DayOfRequest = day.DayOfWeek.ToString();
                            if (DayOfRequest == "Tuesday")
                            {
                                AddException(ExceptionResourceKeys.UIValidation_R24_DailyRequestInWeekdays, " ثبت درخواست برای روز سه شنبه مجاز نیست");
                            }
                        }
                    }


                    if (AllowWednsday == 1)
                    {
                        for (var day = request.FromDate; day.Date <= request.ToDate; day = day.AddDays(1))
                        {
                            string DayOfRequest = day.DayOfWeek.ToString();
                            if (DayOfRequest == "Wednesday")
                            {
                                AddException(ExceptionResourceKeys.UIValidation_R24_DailyRequestInWeekdays, " ثبت درخواست برای روز چهار شنبه مجاز نیست");
                            }
                        }
                    }

                    if (AllowFriday == 1)
                    {
                        for (var day = request.FromDate; day.Date <= request.ToDate; day = day.AddDays(1))
                        {
                            string DayOfRequest = day.DayOfWeek.ToString();
                            if (DayOfRequest == "Friday")
                            {
                                AddException(ExceptionResourceKeys.UIValidation_R24_DailyRequestInWeekdays, " ثبت درخواست برای روز جمعه مجاز نیست");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "RequestValidator", "R24_DailyRequestInWeekdays");
                throw ex;
            }
        }
        /// <summary>
        /// ثبت درخواست های ساعتی برای روزهای هفته مجاز نیست
        /// </summary>
        private void R25_HourlyRequestInWeekdays(Request request, UIValidationRuleGroup RuleGroup)
        {
            string FirstParam = "Thursday";
            string SecondParam = "satyrday";
            string ThirdParam = "Monday";
            string ForthParam = "Thuesday";
            string FifthParam = "Wednsday";
            string SixthParam = "Sunday";
            string SeventhParam = "Friday";

            try
            {
                if (validateRep.GetPrecardRule(RuleGroup.ID).Where(x => x.ID == request.Precard.ID).Count() > 0)
                {
                    IList<UIValidationRuleParam> parameters = validateRep.GetParameterRule(RuleGroup.ID, request.Precard.ID, FirstParam, SecondParam, ThirdParam, ForthParam, FifthParam, SixthParam, SeventhParam);
                    if (Utility.IsEmpty(parameters) || parameters.Count != 7)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterCount, "تعداد پارامتر های قانون 25 با مقدار مورد انتظار نابرابر است ", ExceptionSrc);
                    }
                    UIValidationRuleParam param = parameters.Where(x => !Utility.IsEmpty(x.UIValidationRuleTempParam.KeyName) && x.UIValidationRuleTempParam.KeyName.Equals("Thursday")).FirstOrDefault();
                    if (param == null)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, "پارامتر با کلید Thursday  نظر برای قانون 25 یافت نشد ", ExceptionSrc);
                    }
                    int AllowThursday = Utility.ToInteger(param.Value);

                    param = parameters.Where(x => !Utility.IsEmpty(x.UIValidationRuleTempParam.KeyName) && x.UIValidationRuleTempParam.KeyName.Equals("satyrday")).FirstOrDefault();
                    if (param == null)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, "پارامتر با کلید satyrday  نظر برای قانون 25 یافت نشد ", ExceptionSrc);
                    }
                    int AllowSaturday = Utility.ToInteger(param.Value);

                    param = parameters.Where(x => !Utility.IsEmpty(x.UIValidationRuleTempParam.KeyName) && x.UIValidationRuleTempParam.KeyName.Equals("Monday")).FirstOrDefault();
                    if (param == null)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, "پارامتر با کلید Monday  نظر برای قانون 25 یافت نشد ", ExceptionSrc);
                    }
                    int AllowMonday = Utility.ToInteger(param.Value);

                    param = parameters.Where(x => !Utility.IsEmpty(x.UIValidationRuleTempParam.KeyName) && x.UIValidationRuleTempParam.KeyName.Equals("Thuesday")).FirstOrDefault();
                    if (param == null)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, "پارامتر با کلید Thuesday  نظر برای قانون 25 یافت نشد ", ExceptionSrc);
                    }
                    int AllowThuesday = Utility.ToInteger(param.Value);

                    param = parameters.Where(x => !Utility.IsEmpty(x.UIValidationRuleTempParam.KeyName) && x.UIValidationRuleTempParam.KeyName.Equals("Wednsday")).FirstOrDefault();
                    if (param == null)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, "پارامتر با کلید Wednsday  نظر برای قانون 25 یافت نشد ", ExceptionSrc);
                    }
                    int AllowWednsday = Utility.ToInteger(param.Value);


                    param = parameters.Where(x => !Utility.IsEmpty(x.UIValidationRuleTempParam.KeyName) && x.UIValidationRuleTempParam.KeyName.Equals("Sunday")).FirstOrDefault();
                    if (param == null)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, "پارامتر با کلید Sunday  نظر برای قانون 25 یافت نشد ", ExceptionSrc);
                    }
                    int AllowSunday = Utility.ToInteger(param.Value);


                    param = parameters.Where(x => !Utility.IsEmpty(x.UIValidationRuleTempParam.KeyName) && x.UIValidationRuleTempParam.KeyName.Equals("Friday")).FirstOrDefault();
                    if (param == null)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, "پارامتر با کلید Friday  نظر برای قانون 25 یافت نشد ", ExceptionSrc);
                    }
                    int AllowFriday = Utility.ToInteger(param.Value);

                    if (AllowThursday == 1)
                    {
                        for (var day = request.FromDate; day.Date <= request.ToDate; day = day.AddDays(1))
                        {
                            string DayOfRequest = day.DayOfWeek.ToString();
                            if (DayOfRequest == "Thursday")
                            {
                                AddException(ExceptionResourceKeys.UIValidation_R25_HourlyRequestInWeekdays, " ثبت درخواست برای روز پنجشنبه مجاز نیست");
                            }
                        }
                    }

                    if (AllowSaturday == 1)
                    {
                        for (var day = request.FromDate; day.Date <= request.ToDate; day = day.AddDays(1))
                        {
                            string DayOfRequest = day.DayOfWeek.ToString();
                            if (DayOfRequest == "Saturday")
                            {
                                AddException(ExceptionResourceKeys.UIValidation_R25_HourlyRequestInWeekdays, " ثبت درخواست برای روز شنبه مجاز نیست");
                            }
                        }
                    }

                    if (AllowSunday == 1)
                    {
                        for (var day = request.FromDate; day.Date <= request.ToDate; day = day.AddDays(1))
                        {
                            string DayOfRequest = day.DayOfWeek.ToString();
                            if (DayOfRequest == "Sunday")
                            {
                                AddException(ExceptionResourceKeys.UIValidation_R25_HourlyRequestInWeekdays, " ثبت درخواست برای روز یکشنبه مجاز نیست");
                            }
                        }
                    }

                    if (AllowMonday == 1)
                    {
                        for (var day = request.FromDate; day.Date <= request.ToDate; day = day.AddDays(1))
                        {
                            string DayOfRequest = day.DayOfWeek.ToString();
                            if (DayOfRequest == "Monday")
                            {
                                AddException(ExceptionResourceKeys.UIValidation_R25_HourlyRequestInWeekdays, " ثبت درخواست برای روز دوشنبه مجاز نیست");
                            }
                        }
                    }

                    if (AllowThuesday == 1)
                    {
                        for (var day = request.FromDate; day.Date <= request.ToDate; day = day.AddDays(1))
                        {
                            string DayOfRequest = day.DayOfWeek.ToString();
                            if (DayOfRequest == "Tuesday")
                            {
                                AddException(ExceptionResourceKeys.UIValidation_R25_HourlyRequestInWeekdays, " ثبت درخواست برای روز سه شنبه مجاز نیست");
                            }
                        }
                    }


                    if (AllowWednsday == 1)
                    {
                        for (var day = request.FromDate; day.Date <= request.ToDate; day = day.AddDays(1))
                        {
                            string DayOfRequest = day.DayOfWeek.ToString();
                            if (DayOfRequest == "Wednesday")
                            {
                                AddException(ExceptionResourceKeys.UIValidation_R25_HourlyRequestInWeekdays, " ثبت درخواست برای روز چهار شنبه مجاز نیست");
                            }
                        }
                    }

                    if (AllowFriday == 1)
                    {
                        for (var day = request.FromDate; day.Date <= request.ToDate; day = day.AddDays(1))
                        {
                            string DayOfRequest = day.DayOfWeek.ToString();
                            if (DayOfRequest == "Friday")
                            {
                                AddException(ExceptionResourceKeys.UIValidation_R25_HourlyRequestInWeekdays, " ثبت درخواست برای روز جمعه مجاز نیست");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "RequestValidator", "R25_HourlyRequestInWeekdays");
                throw ex;
            }
        }
        /// <summary>
        /// مجموع دو پیشکارت حداکثر _____ عدد در سال باشد
        /// </summary>
        private void R26_SumOfPrecads(Request request, UIValidationRuleGroup RuleGroup)
        {
            string Param = "SumPrecard";
            int CountAll = 0;
            List<Request> requestList = new List<Request>();
            try
            {
                if (validateRep.GetPrecardRule(RuleGroup.ID).Where(x => x.ID == request.Precard.ID).Count() > 0)
                {
                    IList<UIValidationRuleParam> parameters = validateRep.GetParameterRule(RuleGroup.ID, request.Precard.ID, Param);
                    if (Utility.IsEmpty(parameters) || parameters.Count != 1)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterCount, "تعداد پارامتر های قانون 26 با مقدار مورد انتظار نابرابر است ", ExceptionSrc);
                    }
                    UIValidationRuleParam param = parameters.Where(x => !Utility.IsEmpty(x.UIValidationRuleTempParam.KeyName) && x.UIValidationRuleTempParam.KeyName.Equals("SumPrecard")).FirstOrDefault();
                    if (param == null)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, "پارامتر با کلید SumPrecard  نظر برای قانون 26 یافت نشد ", ExceptionSrc);
                    }
                    int maxCount = Utility.ToInteger(param.Value);
                    DateTime requestDate = request.FromDate.Date;
                    DateTime startYear, endYear;


                    if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                    {
                        startYear = Utility.GetDateOfBeginYear(requestDate, LanguagesName.Parsi);
                        endYear = Utility.GetDateOfEndYear(requestDate, LanguagesName.Parsi);
                    }
                    else
                    {
                        startYear = Utility.GetDateOfBeginYear(requestDate, LanguagesName.English);
                        endYear = Utility.GetDateOfEndYear(requestDate, LanguagesName.English);
                    }
                    IList<Precard> CountOfRequest = validateRep.GetPrecardRule(RuleGroup.ID).ToList();
                    for (var i = 0; i < CountOfRequest.Count; i++)
                    {
                        //     int count = requestRepository.GetActiveRequestCount(request.Person.ID, CountOfRequest[i].ID, startYear, endYear);
                        IList<InfoRequest> list = requestRepository.GetActiveRequestDateValues(request.Person.ID, CountOfRequest[i].ID, startYear, endYear);
                        var sum1 = from o in list
                                   where o.TimeDuration > 0
                                   select o.TimeDuration;
                        CountAll = CountAll + sum1.Sum();
                        if (request.IsEdited)
                        {
                            var Editedreq = from s in list
                                            where s.ID == request.ID
                                            select s.ID;
                            if (Editedreq.Count() > 0)
                            {
                                CountAll = CountAll - 1;
                            }
                        }
                    }
                    if (CountAll + (request.ToDate - request.FromDate).Days + 1 > maxCount)
                    {
                        AddException(ExceptionResourceKeys.UIValidation_R26_SumOfPrecads, " مقدار مجموع دو پیشکارت از حد مجاز تجاوز کرده است");
                    }
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "RequestValidator", "R26_SumOfPrecads");
                throw ex;
            }
        }
        // آرشیو نتایج محاسبات (کاهشی- سپه)
        private void R27_ArchiveCalculationKaheshi(bool active)
        {
            try
            {
                if (active == false)
                {

                    AddException(ExceptionResourceKeys.UIValidation_R27_ArchiveCalculationKaheshi, "قانون آرشیو نتایج محاسبات - کاهشی سپه فعال نیست");

                }
            }
            catch (Exception ex)
            {

                BaseBusiness<Entity>.LogException(ex, "RequestValidator", "R26_SumOfPrecads");
                throw ex;
            }

        }
        /// <summary>
        ///   پیش کارت های ماهانه) مقدار درخواست های ساعتی حداکثر ____ ساعت در ماه باشد )
        /// </summary>
        private void R28_MaxAmountHourlyRequestForMonthlyPrecard(Request request, UIValidationRuleGroup RuleGroup)
        {
            string Param = "MonthlyMaxHrours";
            try
            {
                if (validateRep.GetPrecardRule(RuleGroup.ID).Where(x => x.ID == request.Precard.ID).Count() > 0)
                {
                    IList<UIValidationRuleParam> parameters = validateRep.GetParameterRule(RuleGroup.ID, request.Precard.ID, Param);
                    if (Utility.IsEmpty(parameters) || parameters.Count != 1)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterCount, "تعداد پارامتر های قانون 28 با مقدار مورد انتظار نابرابر است ", ExceptionSrc);
                    }
                    UIValidationRuleParam param = parameters.Where(x => !Utility.IsEmpty(x.UIValidationRuleTempParam.KeyName) && x.UIValidationRuleTempParam.KeyName.Equals("MonthlyMaxHrours")).FirstOrDefault();
                    if (param == null)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, "پارامتر با کلید MonthlyMaxHrours  نظر برای قانون 28 یافت نشد ", ExceptionSrc);
                    }
                    int maxHour = Utility.RealTimeToIntTime(param.Value);
                    DateTime requestDate = request.FromDate.Date;
                    DateTime startMonth, endMonth;


                    Person person = new PersonRepository().GetById(request.Person.ID, false);
                    DateRange dateRangePersonInRequestDate = new BDateRange().GetDateRangePerson(person, 0, requestDate);
                    startMonth = dateRangePersonInRequestDate.FromDate;
                    endMonth = dateRangePersonInRequestDate.ToDate;

                    IList<InfoRequest> list = requestRepository.GetActiveRequestDateValues(request.Person.ID, request.Precard.ID, startMonth, endMonth);

                    if (request.IsEdited)
                    {
                        list = list.Where(i => i.ID != request.ID).ToList();
                    }
                    var sum1 = from o in list
                               where o.TimeDuration > 0
                               select o.TimeDuration;

                    var sum2 = from o in list
                               where o.TimeDuration == -1000
                               select o.ToTime - o.FromTime;
                    int newDuration = request.TimeDuration > 0 ? request.TimeDuration : request.ToTime - request.FromTime;
                    string newDurationStr = Utility.IntTimeToRealTime(newDuration * 60);
                    int newDurationInt = Utility.RealTimeToIntTime(newDurationStr);
                    if (sum1.Sum() + sum2.Sum() + newDurationInt > maxHour)
                    {
                        AddException(ExceptionResourceKeys.UIValidation_R28_MaxAmountHourlyRequestForMonthlyPrecard, " مقدار ساعت درخواست  از حداکثر مجاز تجاوز کرده است");
                    }
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "RequestValidator", "R28_MaxAmountHourlyRequestForMonthlyPrecard");
                throw ex;
            }
        }
        /// <summary>
        /// ثبت درخواست تردد بصورت ورود و خروج همزمان امکان پذیر نیست
        /// </summary>
        /// <param name="request"></param>
        /// <param name="grouping"></param>
        private void R29_RequestMaxArrivalExit(Request request, UIValidationRuleGroup RuleGroup)
        {
            try
            {
                if (validateRep.GetPrecardRule(RuleGroup.ID).Where(x => x.ID == request.Precard.ID).Count() > 0)
                {
                    if (request.FromTime != -1000 && request.ToTime != -1000)
                    {
                        AddException(ExceptionResourceKeys.UIValidation_R29_RequestMaxArrivalExit, " امکان ثبت همزمان ورود و خروج وجود ندارد");
                    }
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "RequestValidator", "R29_RequestMaxArrivalExit");
                throw ex;
            }
        }
        /// <summary>
        /// همپوشانی درخواست ها مجاز نیست
        /// </summary>
        /// <param name="request"></param>
        /// <param name="grouping"></param>
        private void R31_OverlapDailyHourlyPrecard(Request request, UIValidationRuleGroup RuleGroup)
        {

            string Param = "PriorityPrecard";
            try
            {
                if (validateRep.GetPrecardRule(RuleGroup.ID).Where(x => x.ID == request.Precard.ID).Count() > 0)
                {
                    IList<Precard> precardList = validateRep.GetPrecardRule(RuleGroup.ID);
                    IList<UIValidationRuleParam> parameters = validateRep.GetParameterRule(RuleGroup.ID, request.Precard.ID, Param);
                    if (Utility.IsEmpty(parameters) || parameters.Count != 1)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterCount, "تعداد پارامتر های قانون 31 با مقدار مورد انتظار نابرابر است ", ExceptionSrc);
                    }
                    UIValidationRuleParam param = parameters.Where(x => !Utility.IsEmpty(x.UIValidationRuleTempParam.KeyName) && x.UIValidationRuleTempParam.KeyName.Equals("PriorityPrecard")).FirstOrDefault();
                    if (param == null)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, "پارامتر با کلید PriorityPrecard  نظر برای قانون 31 یافت نشد ", ExceptionSrc);
                    }
                    int priorityPrecardRequest = Utility.ToInteger(param.Value);
                    RequestRepository rep = new RequestRepository(false);
                    DateTime requestDate = request.FromDate.Date;
                    DateTime startMonth, endMonth;
                    Person person = new PersonRepository().GetById(request.Person.ID, false);
                    DateRange dateRangePersonInRequestDate = new BDateRange().GetDateRangePerson(person, 0, requestDate);
                    startMonth = dateRangePersonInRequestDate.FromDate;
                    endMonth = dateRangePersonInRequestDate.ToDate;
                    IList<Request> confirmRequestList = rep.GetAllRequest(request.Person.ID, startMonth, endMonth, RequestState.Confirmed);
                    IList<Request> underReviewRequestList = rep.GetAllRequest(request.Person.ID, startMonth, endMonth, RequestState.UnderReview);
                    List<Request> requestList = new List<Request>();
                    requestList.AddRange(confirmRequestList);
                    requestList.AddRange(underReviewRequestList);
                    //   List<string> precardValidationsList = validateRep.GetPrecard(grouping.ValidationRule.CustomCode, PrecardType).Select(p => p.Code).ToList<string>();
                    List<string> precardValidationsList = validateRep.GetPrecardRule(RuleGroup.ID).Select(p => p.Code).ToList<string>();
                    List<Request> requestFilteredList = requestList.Where(r => precardValidationsList.Contains(r.Precard.Code)).ToList<Request>();
                    if (request.IsEdited)
                    {
                        requestFilteredList = requestFilteredList.Where(i => i.ID != request.ID).ToList();
                    }
                    var List = from o in requestFilteredList
                               where ((o.FromDate >= request.FromDate && o.ToDate > request.ToDate && o.FromDate < request.ToDate && (request.Precard.IsDaily || o.Precard.IsDaily) || o.FromDate <= request.FromDate
                               && o.ToDate >= request.ToDate && (request.Precard.IsDaily || o.Precard.IsDaily) || o.FromDate <= request.FromDate && o.ToDate <= request.ToDate && o.ToDate > request.FromDate && (request.Precard.IsDaily || o.Precard.IsDaily)
                               || o.FromDate >= request.FromDate && o.ToDate <= request.ToDate && (request.Precard.IsDaily || o.Precard.IsDaily)) || (o.FromTime >= request.FromTime && o.ToTime > request.ToTime
                               && o.FromTime < request.ToTime || o.FromTime <= request.FromTime && o.ToTime >= request.ToTime || o.FromTime <= request.FromTime
                               && o.ToTime <= request.ToTime && o.ToTime > request.FromTime || o.FromTime >= request.FromTime && o.ToTime <= request.ToTime))
                               select o;
                    var requestlistI = from o in List
                                       where (o.FromDate == request.FromDate || o.ToDate == request.ToDate)
                                       select o;
                    bool isPreventRequest = false;
                    foreach (Precard item in precardList)
                    {
                        IList<UIValidationRuleParam> paramsList = validateRep.GetParameterRule(RuleGroup.ID, item.ID, Param);
                        if (paramsList.Count > 0)
                        {
                            UIValidationRuleParam paramObj = paramsList.Where(x => !Utility.IsEmpty(x.UIValidationRuleTempParam.KeyName) && x.UIValidationRuleTempParam.KeyName.Equals("PriorityPrecard")).FirstOrDefault();
                            int priorityPrecardItem = Utility.ToInteger(paramObj.Value);
                            IList<Request> requestByPrecardList = requestlistI.Where(r => r.Precard.ID == item.ID).ToList();
                            if (priorityPrecardItem <= priorityPrecardRequest && requestByPrecardList.Count(c => c.Precard.ID == item.ID) > 0)
                                isPreventRequest = true;
                        }
                    }
                    if (isPreventRequest)
                    {
                        AddException(ExceptionResourceKeys.UIValidation_R31_OverlapDailyHourlyPrecard, " تداخل زمانی درخواست ها");
                    }


                }


            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "RequestValidator", "R31_OverlapDailyHourlyPrecard");
                throw ex;
            }
        }
        /// <summary>
        /// مقدار درخواست های ساعتی در روز
        /// </summary>
        /// <param name="request"></param>
        /// <param name="grouping"></param>
        public void R32_AmountInDay(Request request, UIValidationRuleGroup RuleGroup)
        {
            try
            {
                string Param = "MaxHroursInDay";

                if (validateRep.GetPrecardRule(RuleGroup.ID).Where(x => x.ID == request.Precard.ID).Count() > 0)
                {
                    IList<UIValidationRuleParam> parameters = validateRep.GetParameterRule(RuleGroup.ID, request.Precard.ID, Param);
                    if (Utility.IsEmpty(parameters) || parameters.Count != 1)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterCount, "تعداد پارامتر های قانون 32 با مقدار مورد انتظار نابرابر است ", ExceptionSrc);
                    }
                    UIValidationRuleParam param = parameters.Where(x => !Utility.IsEmpty(x.UIValidationRuleTempParam.KeyName) && x.UIValidationRuleTempParam.KeyName.Equals("MaxHroursInDay")).FirstOrDefault();
                    if (param == null)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, "پارامتر با کلید MaxHroursInDay  نظر برای قانون 32 یافت نشد ", ExceptionSrc);
                    }
                    int maxHour = Utility.RealTimeToIntTime(param.Value);
                    DateTime requestFromDate = request.FromDate.Date;
                    DateTime requestToTime = request.ToDate.Date;
                    Person person = new PersonRepository().GetById(request.Person.ID, false);
                    DateRange dateRangePersonInRequestDate = new BDateRange().GetDateRangePerson(person, 0, requestFromDate);
                    IList<InfoRequest> list = requestRepository.GetActiveRequestDateValues(request.Person.ID, request.Precard.ID, requestFromDate, requestToTime);

                    var sum1 = from o in list
                               where o.TimeDuration > 0
                               select o.TimeDuration;

                    var sum2 = from o in list
                               where o.TimeDuration == -1000
                               select o.ToTime - o.FromTime;
                    TimeSpan DifferenceDates = request.ToDate - request.FromDate;


                    int newDuration = request.TimeDuration > 0 ? request.TimeDuration : request.ToTime - request.FromTime;

                    newDuration = newDuration * (DifferenceDates.Days + 1);
                    if (sum1.Sum() + sum2.Sum() + newDuration > maxHour)
                    {
                        AddException(ExceptionResourceKeys.UIValidation_R32_AmountInDay, " مقدار ساعت درخواست  از حداکثر مجاز تجاوز کرده است");
                    }
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "RequestValidator", "R32_AmountInDay");
                throw ex;
            }
        }
        /// <summary>
        /// مشخص نمودن جانشین درخواست الزامی است
        /// </summary>
        /// <param name="request"></param>
        /// <param name="RuleGroup"></param>
        private void R33_SubstituteRequest(Request request, UIValidationRuleGroup RuleGroup)
        {
            try
            {
                if ((validateRep.GetPrecardRule(RuleGroup.ID).Where(x => x.ID == request.Precard.ID).Count() > 0) && request.IsEdited == false)
                {
                    if (request.SubstitutePerson == null)
                    {
                        AddException(ExceptionResourceKeys.UIValidation_R33_SubstituteRequest, " جانشین مشخص نشده است");
                    }
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "RequestValidator", "R33_SubstituteRequest");
                throw ex;
            }
        }
        /// <summary>
        /// ثبت درخواست مرخصی بدون حقوق درصورت داشتن مانده مرخصی امکان پذیر نیست
        /// </summary>
        /// <param name="request"></param>
        /// <param name="grouping"></param>
        private void R34_WithOutPayLeaveInRemainLeave(Request request, UIValidationRuleGroup RuleGroup)
        {
            try
            {

                if (request.IsExecuteWarningUIValidation)
                {
                    DateTime fromDateRequest, endMonth;
                    Person prs = new PersonRepository(false).GetById(request.Person.ID, false);
                    CFP cfpPersonObj = cfpRepository.GetByPersonID(prs.ID);
                    DateTime LockCalculationDate = GetCalculationLockDate(prs.ID);
                    if (LockCalculationDate >= cfpPersonObj.Date)
                        fromDateRequest = LockCalculationDate.AddDays(1);
                    else
                    {
                        fromDateRequest = cfpPersonObj.Date;
                    }
                    IList<LeaveCalcResult> leaveCalcResultList = prs.LeaveCalcResultList;//;.Where(l => l.Date >= fromDateRequest && l.Type.Trim().ToLower() == "uld").ToList();
                    prs.InitializeForAccessRules(request.FromDate, request.ToDate);
                    object parameterRuleObj = GetRuleParameter(request.ToDate, prs, 3017, "First");
                    int leaveMinuteInDay = 0;
                    if (parameterRuleObj != null && Convert.ToInt32(parameterRuleObj) != 0)
                    {
                        leaveMinuteInDay = Convert.ToInt32(parameterRuleObj);
                    }
                    else
                    {
                        //7.30 ساعت به طور پیش فرض گرفته شده
                        leaveMinuteInDay = (int)((7 * 60) + 30);
                    }


                    int yearRequest = 0;
                    int monthRequest = 0;
                    if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                    {
                        yearRequest = Utility.ToPersianDateTime(request.ToDate).Year;
                        monthRequest = Utility.ToPersianDateTime(request.ToDate).Month;
                    }
                    else
                    {
                        yearRequest = request.ToDate.Year;
                        monthRequest = request.ToDate.Month;
                    }


                    endMonth = Utility.GetEndOfPersianMonth(request.ToDate);

                    DateTime endDateYear = Utility.GetDateOfEndYear(DateTime.Now, BLanguage.CurrentSystemLanguage);
                    IList<InfoRequest> requestUnderviewList = RegisteredRequestsBusiness.GetAllRequests(prs.ID, RequestState.UnderReview, LockCalculationDate, endMonth);
                    IList<InfoRequest> requestConfirmedList = RegisteredRequestsBusiness.GetAllRequests(prs.ID, RequestState.Confirmed, fromDateRequest, endMonth);

                    IList<InfoRequest> requestRejectDeletedIsInCalcList = RegisteredRequestsBusiness.GetAllRequests(prs.ID, RequestState.Unconfirmed, fromDateRequest, endMonth);
                    ((List<InfoRequest>)(requestRejectDeletedIsInCalcList)).AddRange(RegisteredRequestsBusiness.GetAllRequests(prs.ID, RequestState.Deleted, fromDateRequest, endMonth));

                    //if (leaveCalcResultList.Count > 0)                 
                    //    requestRejectDeletedIsInCalcList = requestRejectDeletedIsInCalcList.Where(r => r.ToDate <= leaveCalcResultList.Max(m => m.Date)).ToList();
                    //else
                    //    requestRejectDeletedIsInCalcList=new List<InfoRequest>();
                    int registeredRequestDaily = 0;
                    int registeredRequestHourly = 0;

                    foreach (InfoRequest item in requestUnderviewList)
                    {
                        if (item.PrecardID == 41 && item.FromDate > LockCalculationDate)
                        {
                            registeredRequestDaily += (item.ToDate - item.FromDate).Days + 1;
                        }
                        else if (item.PrecardID == 21 && item.FromDate > LockCalculationDate)
                        {
                            registeredRequestHourly += (item.ToTime - item.FromTime);
                        }

                    }

                    List<decimal> permitList = new List<decimal>();
                    Permit permit = null;
                    UsedLeaveDetail usedLeaveDetail = null;
                    IList<UsedLeaveDetail> usedLeaveDetailList = NHSession.QueryOver(() => usedLeaveDetail).JoinAlias(() => usedLeaveDetail.Permit, () => permit).Where(() => usedLeaveDetail.Person.ID == prs.ID).List<UsedLeaveDetail>();
                    //List<decimal> usedLeavePermitIdList = new List<decimal>();
                    //usedLeavePermitIdList.AddRange(usedLeaveDetailList.Select(x => x.Permit.ID));


                    foreach (InfoRequest item in requestConfirmedList)
                    {
                        Permit permitObj = new BPermit().GetExistingPermit(item.ID);
                        if (permitObj != null)
                        {
                            for (var day = item.FromDate; day <= item.ToDate; day = day.AddDays(1))
                            {
                                if (usedLeaveDetailList.Count(c => c.Permit != null && c.Permit.ID == permitObj.ID && c.Date == day) == 0)
                                {
                                    if (item.PrecardID == 41)
                                    {
                                        registeredRequestDaily += 1;
                                    }
                                    else if (item.PrecardID == 21)
                                    {
                                        registeredRequestHourly += (item.ToTime - item.FromTime);
                                    }
                                }

                            }
                        }


                    }
                    foreach (InfoRequest item in requestRejectDeletedIsInCalcList)
                    {
                        Permit permitObj = new BPermit().GetExistingPermit(item.ID);
                        if (permitObj != null)
                        {
                            for (var day = item.FromDate; day <= item.ToDate; day = day.AddDays(1))
                            {
                                if (usedLeaveDetailList.Count(c => c.Permit != null && c.Permit.ID == permitObj.ID && c.Date == day) >= 0)
                                    if (item.PrecardID == 41 && day > LockCalculationDate)
                                    {
                                        registeredRequestDaily -= 1;
                                    }
                                    else if (item.PrecardID == 21 && day > LockCalculationDate)
                                    {
                                        registeredRequestHourly -= (item.ToTime - item.FromTime);
                                    }
                            }
                        }

                    }
                    int allRegisteredRequestIsNotInRemainLeave = (registeredRequestDaily * leaveMinuteInDay) + registeredRequestHourly;
                    int minuteRemainLeaveToEndMonth = 0;
                    new BRemainLeave().GetRemainLeaveToEndOfMonth(request.Person.ID, yearRequest, monthRequest, out minuteRemainLeaveToEndMonth);
                    if (minuteRemainLeaveToEndMonth - allRegisteredRequestIsNotInRemainLeave > 0 || allRegisteredRequestIsNotInRemainLeave > minuteRemainLeaveToEndMonth)
                    {
                        AddException(ExceptionResourceKeys.UIValidation_R34_WithOutPayLeaveInRemainLeave, " امکان ثبت مرخصی بدون حقوق وجود ندارد");
                    }

                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "RequestValidator", "R34_WithOutPayLeaveInRemainLeave");
                throw ex;

            }
        }
        /// <summary>
        /// مرخصی با حداکثر _____ روز به ماموریت با حداکثر ____ روز متصل باشد
        /// </summary>
        /// <param name="request"></param>
        /// <param name="grouping"></param>
        private void R35_InWayLeave(Request request, UIValidationRuleGroup RuleGroup)
        {
            string FirstParam = "InWayLeaveCount";
            List<Decimal> precardList = validateRep.GetPrecardRule(RuleGroup.ValidationRule.ID).Select(i => i.ID).ToList<Decimal>();
            List<Precard> SpesificRulePrecard = new List<Precard>();
            //List<UIValidationRuleParam> MissionRuleParam = new List<UIValidationRuleParam>();
            int MissionRequestCount = 0;
            int MaxCount;
            try
            {
                IList<UIValidationRuleParam> parameters = validateRep.GetParameterRule(RuleGroup.ID, request.Precard.ID, FirstParam);
                if (Utility.IsEmpty(parameters) || parameters.Count != 1)
                {
                    throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterCount, "تعداد پارامتر های قانون 35 با مقدار مورد انتظار نابرابر است ", ExceptionSrc);
                }
                UIValidationRuleParam param = parameters.Where(x => !Utility.IsEmpty(x.UIValidationRuleTempParam.KeyName) && x.UIValidationRuleTempParam.KeyName.Equals("InWayLeaveCount")).FirstOrDefault();
                if (param == null)
                {
                    throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, "پارامتر با کلید InWayLeaveCount  نظر برای قانون 35 یافت نشد ", ExceptionSrc);
                }
                IList<Precard> Missionprecardlist = new BPrecard().GetAllByPrecardGroup(new BPrecard().GetAllPrecardGroups().FirstOrDefault(p => p.LookupKey == Infrastructure.PrecardGroupsName.duty.ToString()).ID).ToList<Precard>();
                IList<Precard> Leaveprecardlist = new BPrecard().GetAllByPrecardGroup(new BPrecard().GetAllPrecardGroups().FirstOrDefault(p => p.LookupKey == Infrastructure.PrecardGroupsName.leave.ToString()).ID).ToList<Precard>();

                MaxCount = Utility.ToInteger(param.Value);
                if (Leaveprecardlist.Select(p => p.ID).Contains(request.Precard.ID))
                {
                    int days = request.ToDate.Subtract(request.FromDate).Days + 1;
                    int InWayLeaveCount = Utility.ToInteger(param.Value);
                    DateTime YesterdayRequestDate = request.FromDate.AddDays(-1);
                    DateTime TommorowRequestDate = request.ToDate.AddDays(1);
                    IList<InfoRequest> RequestList = requestRepository.GetActiveRequestDateValuesForUiValidationRule(request.Person.ID, YesterdayRequestDate, TommorowRequestDate);
                    InfoRequest MissionRequest = RequestList.Where(i => Missionprecardlist.Select(p => p.ID).Contains(i.PrecardID)).ToList<InfoRequest>().FirstOrDefault();
                    if (MissionRequest != null)
                    {
                        IList<UIValidationRuleParam> MissionParameterList = validateRep.GetParameterRule(RuleGroup.ID, MissionRequest.PrecardID, FirstParam);
                        UIValidationRuleParam MissionParam = MissionParameterList.Where(x => !Utility.IsEmpty(x.UIValidationRuleTempParam.KeyName) && x.UIValidationRuleTempParam.KeyName.Equals("InWayLeaveCount")).FirstOrDefault();
                        int MissionParamValue = Utility.ToInteger(MissionParam.Value);
                        int FilteredMissionRequestList = MissionRequest.ToDate.Subtract(MissionRequest.FromDate).Days + 1;
                        if (FilteredMissionRequestList < MissionParamValue)
                        {
                            MissionRequestCount = 0;
                        }
                        else
                        {
                            MissionRequestCount = 1;
                        }
                    }
                    if ((MissionRequestCount == 0 && MissionRequest != null) || days > MaxCount || MissionRequest == null)
                    {
                        AddException(ExceptionResourceKeys.UIValidation_R35_InWayLeave, "مرخصی می بایست به ماموریت متصل باشد");
                    }
                }

            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "RequestValidator", "R35_InWayLeave");
                throw ex;
            }
        }
        /// <summary>
        /// مرخصی والدین از تولد فرزند تا ____ ماه ، ______ روز باشد
        /// </summary>
        /// <param name="request"></param>
        /// <param name="RuleGroup"></param>
        private void R36_GivingBirthLeave(Request request, UIValidationRuleGroup RuleGroup)
        {

            string FirstParam = "Discontinuous";
            string SecondParam = "DayCount";
            string ThirdParam = "GivingBirthLeaveRespite";

            IList<UIValidationRuleParam> parameters = validateRep.GetParameterRule(RuleGroup.ID, request.Precard.ID, FirstParam, SecondParam, ThirdParam);
            if (Utility.IsEmpty(parameters) || parameters.Count != 3)
            {
                throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterCount, "تعداد پارامتر های قانون 36 با مقدار مورد انتظار نابرابر است ", ExceptionSrc);
            }
            UIValidationRuleParam Discontinuousparam = parameters.Where(x => !Utility.IsEmpty(x.UIValidationRuleTempParam.KeyName) && x.UIValidationRuleTempParam.KeyName.Equals("Discontinuous")).FirstOrDefault();
            if (Discontinuousparam == null)
            {
                throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, "پارامتر با کلید Discontinuous  نظر برای قانون 36 یافت نشد ", ExceptionSrc);
            }
            UIValidationRuleParam DayCountparam = parameters.Where(x => !Utility.IsEmpty(x.UIValidationRuleTempParam.KeyName) && x.UIValidationRuleTempParam.KeyName.Equals("DayCount")).FirstOrDefault();
            if (DayCountparam == null)
            {
                throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, "پارامتر با کلید DayCount  نظر برای قانون 36 یافت نشد ", ExceptionSrc);
            }
            UIValidationRuleParam GivingBirthLeaveRespiteparam = parameters.Where(x => !Utility.IsEmpty(x.UIValidationRuleTempParam.KeyName) && x.UIValidationRuleTempParam.KeyName.Equals("GivingBirthLeaveRespite")).FirstOrDefault();
            if (GivingBirthLeaveRespiteparam == null)
            {
                throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, "پارامتر با کلید GivingBirthLeaveRespite  نظر برای قانون 36 یافت نشد ", ExceptionSrc);
            }
            bool Continuous = Utility.ToBoolean(Discontinuousparam.Value);
            int DayCount = Utility.ToInteger(DayCountparam.Value);
            int GivingBirthLeaveRespite = Utility.ToInteger(GivingBirthLeaveRespiteparam.Value);
            DateTime ReuestFromDate = request.FromDate;
            DateTime RequestToDate = request.ToDate;
            // Change UIEmploymentDate To SonBirthDate
            Person pd = NHSession.QueryOver<Person>()
                                  .Where(x => x.ID == request.Person.ID)
                                  .SingleOrDefault();
            DateTime SonBirthDate = pd.PersonDetail.ChildBirthDate;
            int days = request.ToDate.Subtract(request.FromDate).Days + 1;
            DateTime EndOfGivingBirthLeaveRespite = SonBirthDate.AddMonths(GivingBirthLeaveRespite);
            if (!Continuous)
            {
                //   IList<InfoRequest> list = requestRepository.GetActiveRequestDateValues(request.Person.ID, request.Precard.ID, request.FromDate, request.ToDate);
                IList<GTS.Clock.Model.RequestFlow.Request> list = null;
                IList<GTS.Clock.Model.RequestFlow.Request> confirmlist = null;
                GTS.Clock.Model.RequestFlow.Request req = null;
                RequestStatus reqStatus = null;
                RequestStatus existsReqStatus = null;
                List<Request> requestList = new List<Request>();
                confirmlist = requestRepository.NHibernateSession.QueryOver<GTS.Clock.Model.RequestFlow.Request>(() => req)
                                     .JoinAlias(() => req.RequestStatusList, () => reqStatus)
                    //  .Where(() => req.FromDate >= startYear && req.FromDate <= endYear)
                                     .Where(() => req.Precard.ID == request.Precard.ID)
                                     .And(() => req.Person.ID == request.Person.ID)
                                     .Where(() => reqStatus.Confirm && reqStatus.EndFlow && !reqStatus.IsDeleted)
                                     .List<GTS.Clock.Model.RequestFlow.Request>();
                var existing = QueryOver.Of<RequestStatus>(() => existsReqStatus)
            .Where(() => existsReqStatus.EndFlow == true)
            .And(() => existsReqStatus.Request.ID == req.ID)
            .Select(x => x.ID);
                Permit permit = null;
                IList<Permit> existingPermitRequestList = requestRepository.NHibernateSession.QueryOver<Permit>(() => permit)
                                                                     .Where(() => permit.Person.ID == request.Person.ID)
                    //  .And(() => permit.FromDate >= startYear && permit.FromDate <= endYear)
                                                                     .List<Permit>();
                List<decimal> requestHavePermitIdsList = new List<decimal>();
                foreach (Permit item in existingPermitRequestList)
                {
                    requestHavePermitIdsList.AddRange(item.Pairs.Select(p => p.RequestID).ToList<decimal>());
                }
                requestHavePermitIdsList = requestHavePermitIdsList.Distinct().ToList<decimal>();
                list = requestRepository.NHibernateSession.QueryOver<GTS.Clock.Model.RequestFlow.Request>(() => req)
                    //      .Where(() => req.FromDate >= startYear && req.FromDate <= endYear)
                                              .Where(() => req.Person.ID == request.Person.ID)
                                              .And(() => req.Precard.ID == request.Precard.ID)
                                              .And(() => !req.EndFlow)
                                              .And(() => !req.ID.IsIn(requestHavePermitIdsList))
                                              .WithSubquery.WhereNotExists(existing).List<Request>();

                requestList.AddRange(confirmlist);
                requestList.AddRange(list);
                int RegisteredRequestCount = requestList.Count();
                int RequestDayCount = 0;
                for (int i = 0; i < requestList.Count; i++)
                {
                    RequestDayCount = RequestDayCount + (requestList[i].ToDate.Subtract(requestList[i].FromDate)).Days + 1;
                }
                //  int count = RegisteredRequestCount + days;
                int count = RequestDayCount + days;
                if (count > DayCount)
                {
                    AddException(ExceptionResourceKeys.UIValidation_R36_GivingBirthLeave, " تعداد روزهای درخواست بیش از حد مجاز است");
                }
            }
            else
            {
                IList<InfoRequest> list = requestRepository.GetActiveRequestDateValues(request.Person.ID, request.Precard.ID, SonBirthDate, EndOfGivingBirthLeaveRespite);
                if (list.Count > 0)
                {
                    AddException(ExceptionResourceKeys.UIValidation_R36_GivingBirthLeave, " امکان ثبت مرخصی  وجود ندارد");
                }
                if (days > DayCount)
                {
                    AddException(ExceptionResourceKeys.UIValidation_R36_GivingBirthLeave, " تعداد روزهای درخواست بیش از حد مجاز است");
                }
            }

            if (EndOfGivingBirthLeaveRespite < ReuestFromDate)
            {
                AddException(ExceptionResourceKeys.UIValidation_R36_GivingBirthLeave, " مهلت استفاده از مرخصی پدر به پایان رسیده است یا تاریخ تولد فرزند مقدار نگرفته است");
            }

        }
        /// <summary>
        /// انتخاب فایل پیوست اجباری است
        /// </summary>
        private void R37_EstelajiRequeredFileAttachement(Request request, UIValidationRuleGroup RuleGroup)
        {
            try
            {
                if ((validateRep.GetPrecardRule(RuleGroup.ID).Where(x => x.ID == request.Precard.ID).Count() > 0) && request.IsEdited == false)
                {
                    if (Utility.IsEmpty(request.AttachmentFile))
                    {
                        AddException(ExceptionResourceKeys.UIValidation_R37_EstelajiRequeredFileAttachement, " فایلی پیوست نشده است");
                    }
                }

            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "RequestValidator", "R37_EstelajiRequeredFileAttachement");
                throw ex;
            }
        }
        /// <summary>
        /// مقدار درخواست های ساعتی اول وقت حداکثر ___ ساعت باشد
        /// </summary>
        private void R38_RequestMaxLength(Request request, UIValidationRuleGroup RuleGroup)
        {
            string FirstParam = "MaxLength";
            //   string SecondParam = "avalVaght";
            try
            {
                if (validateRep.GetPrecardRule(RuleGroup.ID).Where(x => x.ID == request.Precard.ID).Count() > 0)
                {
                    //  IList<UIValidationRuleParam> parameters = validateRep.GetParameterRule(RuleGroup.ID, request.Precard.ID, FirstParam, SecondParam);
                    IList<UIValidationRuleParam> parameters = validateRep.GetParameterRule(RuleGroup.ID, request.Precard.ID, FirstParam);

                    if (Utility.IsEmpty(parameters) || parameters.Count != 1)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterCount, "تعداد پارامتر های قانون 38 با مقدار مورد انتظار نابرابر است ", ExceptionSrc);
                    }
                    UIValidationRuleParam maxLen = parameters.Where(x => !Utility.IsEmpty(x.UIValidationRuleTempParam.KeyName) && x.UIValidationRuleTempParam.KeyName.Equals("MaxLength")).FirstOrDefault();
                    if (maxLen == null)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, "پارامتر با کلید MaxLength  نظر برای قانون 38 یافت نشد ", ExceptionSrc);
                    }
                    int maxLength = Utility.RealTimeToIntTime(maxLen.Value);
                    //     	bool isAvvalVaght = Utility.ToBoolean(avalVaght.Value);

                    Person prs = new PersonRepository(false).GetById(request.Person.ID, false);
                    prs.InitializeForAccessRules(request.FromDate.AddDays(-1), request.ToDate.AddDays(1));
                    BaseShift shift = prs.GetShiftByDate(request.FromDate);
                    if (shift != null && shift.PairCount > 0 && request.TimeDuration > 0)
                    {
                        if (shift.Pairs.OrderBy(x => x.From).First().From >= request.FromTime)
                        {
                            if (request.ToTime - request.FromTime > maxLength)
                            {
                                AddException(ExceptionResourceKeys.UIValidation_R38_RequestMaxLength, " مدت زمان بازه درخواست از حد مجاز بیشتر است");
                            }
                        }
                    }

                    else
                    {
                        if (request.ToTime - request.FromTime > maxLength && request.TimeDuration > 0)
                        {
                            AddException(ExceptionResourceKeys.UIValidation_R38_RequestMaxLength, " مدت زمان بازه درخواست از حد مجاز بیشتر است");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "RequestValidator", "R38_RequestMaxLength");
                throw ex;
            }
        }
        private void R200_RequestLeaveHourlyNotAllowedWithOtherPrecardsInSticking(Request request, UIValidationRuleGroup RuleGroup)
        {
            try
            {
                string firstParam = "PeriodTime";
                string secondParam = "Sticking";
                if (validateRep.GetPrecardRule(RuleGroup.ID).Where(x => x.ID == request.Precard.ID).Count() > 0 || request.Precard.Code == "21")
                {
                    IList<Precard> precardList = validateRep.GetPrecardRule(RuleGroup.ID);
                    IList<UIValidationRuleParam> parameters = validateRep.GetParameterRule(RuleGroup.ID, request.Precard.ID, firstParam, secondParam);
                    if (Utility.IsEmpty(parameters) || parameters.Count != 2)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterCount, "تعداد پارامتر های قانون 200 با مقدار مورد انتظار نابرابر است ", ExceptionSrc);
                    }
                    UIValidationRuleParam paramPeriodTime = parameters.Where(x => !Utility.IsEmpty(x.UIValidationRuleTempParam.KeyName) && x.UIValidationRuleTempParam.KeyName.Equals("PeriodTime")).FirstOrDefault();
                    if (paramPeriodTime == null)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, "پارامتر با کلید PeriodTime  نظر برای قانون 200 یافت نشد ", ExceptionSrc);
                    }
                    UIValidationRuleParam paramSticking = parameters.Where(x => !Utility.IsEmpty(x.UIValidationRuleTempParam.KeyName) && x.UIValidationRuleTempParam.KeyName.Equals("Sticking")).FirstOrDefault();
                    if (paramSticking == null)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, "پارامتر با کلید Sticking  نظر برای قانون 200 یافت نشد ", ExceptionSrc);
                    }
                    int periodTime = Utility.ToInteger(paramPeriodTime.Value);
                    int StickingStatus = Utility.ToInteger(paramSticking.Value);
                    List<Request> requestList = new List<Request>();

                    IList<Request> requestConfirmedList = requestRepository.GetAllRequest(request.Person.ID, request.FromDate, request.ToDate, RequestState.Confirmed).Where(r => !(request.IsEdited && r.ID == request.ID)).ToList();
                    requestList.AddRange(requestConfirmedList);
                    IList<Request> requestUnderViewList = requestRepository.GetAllRequest(request.Person.ID, request.FromDate, request.ToDate, RequestState.UnderReview).Where(r => !(request.IsEdited && r.ID == request.ID)).ToList();
                    requestList.AddRange(requestUnderViewList);
                    bool requestIsFailed = false;
                    Precard precardLeaveHourlyObj = new BPrecard().GetPrecardByCode("21");
                    if (precardLeaveHourlyObj != null)
                        precardList.Add(precardLeaveHourlyObj);
                    request.Precard = new BPrecard().GetByID(request.Precard.ID);
                    if (request.Precard.Code == "21")
                    {
                        requestList = requestList.Where(p => p.Precard.Code != "21").ToList();
                    }
                    else
                    {
                        requestList = requestList.Where(p => p.Precard.Code == "21").ToList();
                    }
                    if (StickingStatus == 0)
                    {
                        if (requestList.Count(c => precardList.Select(p => p.Code).Contains(c.Precard.Code) && ((c.FromTime >= request.ToTime && c.FromTime - periodTime <= request.ToTime)
                            || (c.ToTime <= request.FromTime && c.ToTime + periodTime >= request.FromTime) || (request.FromTime >= c.ToTime && request.FromTime - periodTime <= c.ToTime) || (request.ToTime <= c.FromTime && request.ToTime + periodTime >= c.FromTime))) > 0)
                        {
                            requestIsFailed = true;
                        }
                    }
                    else if (StickingStatus == 1)
                    {
                        if (request.Precard.Code != "21" && requestList.Count(c => precardList.Select(p => p.Code).Contains(c.Precard.Code) && ((request.FromTime >= c.ToTime && request.FromTime - periodTime <= c.ToTime) ||
                             (request.ToTime <= c.FromTime && request.ToTime + periodTime >= c.FromTime))) == 0)
                        {
                            requestIsFailed = true;
                        }
                    }




                    if (requestIsFailed)
                    {
                        if (StickingStatus == 0)
                        {
                            AddException(ExceptionResourceKeys.UIValidation_R200_RequestLeaveHourlyNotAllowedWithOtherPrecardsInSticking, " امکان ثبت درخواست متصل به درخواست مرخصی استحقاقی وجود ندارد");
                        }
                        else if (StickingStatus == 1)
                        {
                            AddException(ExceptionResourceKeys.UIValidation_R200_RequestLeaveHourlyStickingInOtherPrecards, " درخواست می بایست به درخواست مرخصی ساعتی متصل باشد");
                        }
                    }


                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "RequestValidator", "R200_RequestLeaveHourlyNotAllowedWithOtherPrecardsInSticking");
                throw ex;
            }
        }
        private void R201_RequestLeaveDailyNotAllowedWithOtherPrecardsInSticking(Request request, UIValidationRuleGroup RuleGroup)
        {
            try
            {
                string firstParam = "Sticking";
                string secondParam = "RequestCount";
                string thirthParam = "MaxDays";
                Precard dailyLeavePrecard = new BPrecard().GetPrecardByCode("41");
                if (validateRep.GetPrecardRule(RuleGroup.ID).Where(x => x.ID == request.Precard.ID).Count() > 0 || request.Precard.Code == dailyLeavePrecard.Code)
                {
                    IList<Precard> precardList = validateRep.GetPrecardRule(RuleGroup.ID);
                    IList<UIValidationRuleParam> parameters = validateRep.GetParameterRule(RuleGroup.ID, request.Precard.ID, firstParam, secondParam, thirthParam);
                    if (Utility.IsEmpty(parameters) || parameters.Count != 3)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterCount, "تعداد پارامتر های قانون 201 با مقدار مورد انتظار نابرابر است ", ExceptionSrc);
                    }
                    UIValidationRuleParam paramSticking = parameters.Where(x => !Utility.IsEmpty(x.UIValidationRuleTempParam.KeyName) && x.UIValidationRuleTempParam.KeyName.Equals("Sticking")).FirstOrDefault();
                    if (paramSticking == null)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, "پارامتر با کلید Sticking  نظر برای قانون 201 یافت نشد ", ExceptionSrc);
                    }
                    UIValidationRuleParam paramRequestCount = parameters.Where(x => !Utility.IsEmpty(x.UIValidationRuleTempParam.KeyName) && x.UIValidationRuleTempParam.KeyName.Equals("RequestCount")).FirstOrDefault();
                    if (paramRequestCount == null)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, "پارامتر با کلید RequestCount  نظر برای قانون 201 یافت نشد ", ExceptionSrc);
                    }
                    UIValidationRuleParam paramMaxDays = parameters.Where(x => !Utility.IsEmpty(x.UIValidationRuleTempParam.KeyName) && x.UIValidationRuleTempParam.KeyName.Equals("MaxDays")).FirstOrDefault();
                    if (paramMaxDays == null)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UIValidationParameterNotfound, "پارامتر با کلید MaxDays  نظر برای قانون 201 یافت نشد ", ExceptionSrc);
                    }
                    RequestRepository rep = new RequestRepository(false);
                    int StickingStatusParam = Utility.ToInteger(paramSticking.Value);
                    int requestCountParam = Utility.ToInteger(paramRequestCount.Value);
                    int maxDaysRequestParam = Utility.ToInteger(paramMaxDays.Value);
                    DateTime firstDayYearDate = Utility.GetDateOfBeginYear(DateTime.Now, BLanguage.CurrentSystemLanguage);
                    DateTime endDayYearDate = Utility.GetDateOfEndYear(DateTime.Now, BLanguage.CurrentSystemLanguage);
                    DateTime startDayRequestMonth = new DateTime();
                    DateTime endDayRequestMonth = new DateTime();

                    switch (BLanguage.CurrentSystemLanguage)
                    {
                        case LanguagesName.Unknown:
                            break;
                        case LanguagesName.Parsi:
                            startDayRequestMonth = Utility.GetStartOfPersianMonth(request.FromDate.AddDays(-1));
                            endDayRequestMonth = Utility.GetEndOfPersianMonth(request.ToDate.AddDays(1));
                            break;
                        case LanguagesName.English:
                            startDayRequestMonth = Utility.GetStartOfPersianMonth(request.FromDate.AddDays(-1));
                            endDayRequestMonth = Utility.GetEndOfPersianMonth(request.ToDate.AddDays(1));
                            break;
                        default:
                            break;
                    }


                    bool requestIsFailed = false;
                    Person personObj = new PersonRepository(false).GetById(request.Person.ID, false);
                    request.Precard = new BPrecard().GetByID(request.Precard.ID);
                    personObj.InitializeForAccessRules(startDayRequestMonth, endDayRequestMonth);
                    int countRequestIsInShift = 0;
                    for (var day = request.FromDate; day.Date <= request.ToDate; day = day.AddDays(1))
                    {
                        BaseShift shiftFromDate = personObj.GetShiftByDate(day);
                        if (shiftFromDate != null && shiftFromDate.PairCount > 0)
                        {
                            countRequestIsInShift++;
                        }
                    }

                    if (dailyLeavePrecard != null)
                        precardList.Add(dailyLeavePrecard);
                    IList<InfoRequest> requestList = requestRepository.GetActiveRequestDateValuesForUiValidationRule(request.Person.ID, request.FromDate.AddDays(-1), request.ToDate.AddDays(+1)).Where(r => !(request.IsEdited && r.ID == request.ID)).ToList();
                    if ((countRequestIsInShift > maxDaysRequestParam) && request.Precard.Code != dailyLeavePrecard.Code)
                    {
                        AddException(ExceptionResourceKeys.UIValidation_R201_RequestLeaveDailyNotAllowedWithOtherPrecardsInSticking, string.Format(" حداکثر مقدار درخواست {0} روز می باشد", maxDaysRequestParam));
                        throw exception;
                    }
                    int requestCount = 0;
                    if (request.Precard.Code != dailyLeavePrecard.Code)
                        requestCount = rep.GetActiveRequestCount(request.Person.ID, request.Precard.ID, firstDayYearDate, endDayYearDate) + 1;

                    if (requestCount > requestCountParam && request.Precard.Code != dailyLeavePrecard.Code)
                    {
                        AddException(ExceptionResourceKeys.UIValidation_R201_RequestLeaveDailyNotAllowedWithOtherPrecardsInSticking, string.Format(" حداکثر تعداد درخواست {0} روز در سال می باشد", requestCountParam));
                        throw exception;
                    }
                    if (request.Precard.Code == dailyLeavePrecard.Code)
                    {

                        requestList = requestList.Where(p => p.PrecardID != dailyLeavePrecard.ID).ToList();
                    }
                    else
                    {


                        requestList = requestList.Where(p => p.PrecardID == dailyLeavePrecard.ID).ToList();
                    }
                    if (StickingStatusParam == 0)
                    {

                        if (requestList.Count(c => precardList.Select(p => p.Code).Contains(c.PrecardID.ToString()) && (request.ToDate.AddDays(1) >= c.FromDate || request.FromDate.AddDays(-1) <= c.ToDate)) > 0)
                        {
                            requestIsFailed = true;
                        }


                    }
                    else if (StickingStatusParam == 1)
                    {
                        if (request.Precard.Code != dailyLeavePrecard.Code && requestList.Count(c => precardList.Select(p => p.ID).Contains(c.PrecardID) && (request.ToDate.AddDays(1) >= c.FromDate || request.FromDate.AddDays(-1) <= c.ToDate)) == 0)
                        {
                            requestIsFailed = true;
                        }
                    }
                    if (requestIsFailed)
                    {
                        if (StickingStatusParam == 0)
                        {
                            AddException(ExceptionResourceKeys.UIValidation_R201_RequestLeaveDailyNotAllowedWithOtherPrecardsInSticking, " امکان ثبت درخواست متصل به درخواست مرخصی روزانه استحقاقی وجود ندارد");
                        }
                        else if (StickingStatusParam == 1)
                        {
                            AddException(ExceptionResourceKeys.UIValidation_R201_RequestLeaveDailyStickingInOtherPrecards, " درخواست می بایست به درخواست مرخصی روزانه استحقاقی متصل باشد");
                        }
                    }


                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "RequestValidator", "R201_RequestLeaveDailyNotAllowedWithOtherPrecardsInSticking");
                throw ex;
            }
        }

        /// <summary>
        /// کنترل مهلت ثبت درخواست با توجه به جدول زمانبندی
        /// </summary>
        /// <param name="request">درخواست</param>
        /// <param name="RuleGroup"></param>
        private void R202_RequestRegisterSchedule(Request request, UIValidationRuleGroup RuleGroup)
        {
            bool IsValid = true;
            try
            {
                if ((validateRep.GetPrecardRule(RuleGroup.ID).Where(x => x.ID == request.Precard.ID).Count() > 0) && request.IsEdited == false)
                {
                    Person person = new PersonRepository().GetById(request.Person.ID, false);
                    if (person.CostCenter == null)
                    {
                        AddException(ExceptionResourceKeys.UIValidation_R202_RequestRegisterSchedule, string.Format("مرکز هزینه به پرسنل {0} اختصاص داده نشده است", person.Name));
                        throw exception;
                    }

                    var approvalSchedule = ApprovalAttendanceScheduleBusiness.GetByApprovalScheduleTypeAndCostCenter(ApprovalScheduleType.Personal, person.ID);
                    if (approvalSchedule == null)
                    {
                        AddException(ExceptionResourceKeys.UIValidation_R202_RequestRegisterSchedule, string.Format("مرکز هزینه پرسنل {0} در فرم زمان بندی مشخص نشده است", person.Name));
                        throw exception;
                    }
                    DateTime requestDate = request.FromDate.Date;

                    DateRange dateRangePersonInRequestDate = new BDateRange().GetDateRangePerson(person, 0, requestDate);

                    //چک کند اگر دوره درخواست با دوره زمانبندی یکسان باشد
                    if (dateRangePersonInRequestDate.DateRangeOrder == approvalSchedule.DateRangeOrder)
                    {
                        var currentDate = DateTime.Now.Date;
                        //چک کن این پرسنل در لیست استثناء زمانبندی واکشی شده است یا خیر ؟
                        var exceptionList = ApprovalAttendanceScheduleExceptionBusiness.GetListProxyByApprovalAttendanceScheduleID(approvalSchedule.ID, person.ID);
                        if (exceptionList != null || exceptionList.Count > 0)
                        {
                            if (exceptionList.Where(c => c.DateFrom.Date <= currentDate && c.DateTo.Date >= currentDate).Any())
                            {
                                IsValid = true;
                            }
                            else
                            {
                                //چک میکند اگر زمان ثبت درخواست در بازه زمانبندی باشد
                                if (currentDate > approvalSchedule.DateTo.Date)
                                    IsValid = false;
                                else
                                    IsValid = true;
                            }
                        }
                        else
                        {
                            //چک میکند اگر زمان ثبت درخواست در بازه زمانبندی باشد
                            if (currentDate > approvalSchedule.DateTo.Date)
                                IsValid = false;
                            else
                                IsValid = true;
                        }
                    }
                    else if (dateRangePersonInRequestDate.DateRangeOrder < approvalSchedule.DateRangeOrder)
                    {
                        //درخواست مربوط به ماه های قبل بوده و مجاز به ثبت نمی باشد
                        IsValid = false;
                    }
                    else if (dateRangePersonInRequestDate.DateRangeOrder > approvalSchedule.DateRangeOrder)
                    {
                        //درخواست مربوط به ماه های آتی می باشد و مجاز به ثبت می باشد
                        IsValid = true;
                    }

                    //----------------------------------------------------------------------------------------------------------------------------
                    if (!IsValid)
                    {
                        AddException(ExceptionResourceKeys.UIValidation_R202_RequestRegisterSchedule, "مهلت ثبت درخواست پایان رسیده است");
                    }
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "RequestValidator", "R202_RequestRegisterSchedule");
                throw ex;
            }
        }

        /// <summary>
        /// کنترل تایید درخواست با توجه به جدول زمانبندی
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="RuleGroup"></param>
        /// <param name="req"></param>
        /// <param name="IsPermit"></param>
        private void R203_RequestConfirmedSchedule(object obj, UIValidationRuleGroup RuleGroup, Request req, bool IsPermit)
        {
            bool IsValid = true;
            try
            {
                Type classtype = obj.GetType();
                if (classtype == typeof(GTS.Clock.Model.RequestFlow.Request))
                {
                    GTS.Clock.Model.RequestFlow.Request request = (GTS.Clock.Model.RequestFlow.Request)obj;

                    if ((validateRep.GetPrecardRule(RuleGroup.ID).Where(x => x.ID == request.Precard.ID).Count() > 0))
                    {
                        Person person = new PersonRepository().GetById(BUser.CurrentUser.Person.ID, false);
                        if (person.CostCenter == null)
                        {
                            AddException(ExceptionResourceKeys.UIValidation_R203_RequestConfirmedSchedule, string.Format("مرکز هزینه به پرسنل {0} اختصاص داده نشده است", person.Name));
                            throw exception;
                        }

                        //تشخیص اینکه طرف معاون هست یا خیر - جهت اعمال محدودیت زمان بندی تایید به تفکیک مدیران و معاونین
                        ApprovalScheduleType approvalType = ApprovalScheduleType.Manager;


                        var personParamAssistance = person.PersonTASpec.GetParamValue(person.ID, "IsAssistance", DateTime.Now);
                        bool stateAssistance = personParamAssistance != null ? Utility.ToInteger(personParamAssistance.Value) > 0 : false;
                        if (stateAssistance)
                            approvalType = ApprovalScheduleType.Assistance;
                        //-----------------------------------------------------------------------------------------------
                        var approvalSchedule = ApprovalAttendanceScheduleBusiness.GetByApprovalScheduleTypeAndCostCenter(approvalType, person.ID);
                        if (approvalSchedule == null)
                        {
                            AddException(ExceptionResourceKeys.UIValidation_R202_RequestRegisterSchedule, string.Format("مرکز هزینه پرسنل {0} در فرم زمان بندی مشخص نشده است", person.Name));
                            throw exception;
                        }

                        DateTime requestDate = request.FromDate.Date;

                        person = new PersonRepository().GetById(request.Person.ID, false);
                        DateRange dateRangePersonInRequestDate = new BDateRange().GetDateRangePerson(person, 0, requestDate);

                        //چک کند اگر دوره درخواست با دوره زمانبندی یکسان باشد
                        if (dateRangePersonInRequestDate.DateRangeOrder == approvalSchedule.DateRangeOrder)
                        {
                            var currentDate = DateTime.Now.Date;
                            //چک کن این پرسنل در لیست استثناء زمانبندی واکشی شده است یا خیر ؟
                            var exceptionList = ApprovalAttendanceScheduleExceptionBusiness.GetListProxyByApprovalAttendanceScheduleID(approvalSchedule.ID, person.ID);
                            if (exceptionList != null || exceptionList.Count > 0)
                            {
                                if (exceptionList.Where(c => c.DateFrom.Date <= currentDate && c.DateTo.Date >= currentDate).Any())
                                {
                                    IsValid = true;
                                }
                                else
                                {
                                    //چک میکند اگر زمان تایید درخواست از زمان انتهای زمانبندی کوچکتر باشد
                                    if (currentDate <= approvalSchedule.DateTo.Date)
                                        IsValid = true;
                                    else
                                        IsValid = false;
                                }
                            }
                            else
                            {
                                //چک میکند اگر زمان تایید درخواست از زمان انتهای زمانبندی کوچکتر باشد
                                if (currentDate <= approvalSchedule.DateTo.Date)
                                    IsValid = true;
                                else
                                    IsValid = false;
                            }
                        }
                        else if (dateRangePersonInRequestDate.DateRangeOrder < approvalSchedule.DateRangeOrder)
                        {
                            //درخواست مربوط به ماه های قبل بوده و مجاز به تایید نمی باشد
                            IsValid = false;
                        }
                        else if (dateRangePersonInRequestDate.DateRangeOrder > approvalSchedule.DateRangeOrder)
                        {
                            //درخواست مربوط به ماه های آتی می باشد و مجاز به تایید می باشد
                            IsValid = true;
                        }

                        //----------------------------------------------------------------------------------------------------------------------------
                        if (!IsValid)
                        {
                            AddException(ExceptionResourceKeys.UIValidation_R203_RequestConfirmedSchedule, "مهلت تایید درخواست پایان رسیده است");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "RequestValidator", "R203_RequestConfirmedSchedule");
                throw ex;
            }
        }

        /// <summary>
        /// کنترل ثبت درخواست در صورت تردد ناقص
        /// </summary>
        /// <param name="request"></param>
        /// <param name="RuleGroup"></param>
        private void R204_RequestIncompleteTraffic(Request request, UIValidationRuleGroup RuleGroup)
        {
            bool IsValid = true;
            try
            {
                //اگر درخواست در روز جاری باشد ادامه ندهد
                if (request.FromDate.Date != DateTime.Now.Date)
                {
                    //اگر نوع درخواست ثبت کارت باشد ادامه ندهد
                    if (request.Precard.ID != 8832)
                    {
                        ////اگر درخواست کارت قبلا ثبت کرده بود ادامه ندهد
                        //var currentRequest = requestRepository.GetAllRequest(request.Person.ID, request.FromDate, request.FromDate, RequestState.UnderReview).ToList();
                        //if (!currentRequest.Where(c => c.Precard.Code == 0.ToString()).Any())
                        //{
                        BPerson personBussiness = new BPerson();
                        var trafficList = personBussiness.GetByID(request.Person.ID).ProceedTrafficList.Where(x => x.FromDate == request.FromDate).ToList();
                        foreach (ProceedTraffic traffic in trafficList)
                        {
                            if (traffic.Pairs.Where(c => c.IsFilled == false).Any())
                                IsValid = false;
                        }
                        //}
                    }
                }
                //----------------------------------------------------------------------------------------------------------------------------
                if (!IsValid)
                {
                    AddException(ExceptionResourceKeys.UIValidation_R202_RequestRegisterSchedule, "تردد روز انتخابی ناقص می باشد لطفا ابتدا درخواست ثبت کارت ثبت نمایید");
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "RequestValidator", "R204_RequestIncompleteTraffic");
                throw ex;
            }

        }

        /// <summary>
        /// کنترل درخواست در روز بدون شیفت
        /// </summary>
        /// <param name="request"></param>
        /// <param name="RuleGroup"></param>
        private void R205_RequestInHoliday(Request request, UIValidationRuleGroup RuleGroup)
        {
            bool IsValid = true;
            try
            {
                if ((validateRep.GetPrecardRule(RuleGroup.ID).Where(x => x.ID == request.Precard.ID).Count() > 0) && request.IsEdited == false)
                {

                    Person person = new PersonRepository().GetById(request.Person.ID, false);
                    DateRange dateRange = new BDateRange().GetDateRangePerson(person, 1011, request.FromDate);
                    DateTime startMonth, endMonth;
                    startMonth = dateRange.FromDate;
                    endMonth = dateRange.ToDate;
                    person.InitializeForAccessRules(startMonth, endMonth);

                    TimeSpan ts = request.ToDate.Date - request.FromDate.Date;
                    int differenceInDays = ts.Days;
                    for (int i = 0; i <= differenceInDays; i++)
                    {
                        BaseShift shift = person.GetShiftByDate(request.FromDate.AddDays(i).Date);
                        if (shift == null || shift.PairCount == 0)
                        {
                            IsValid = false;
                        }
                    }

                    if (!IsValid)
                    {
                        AddException(ExceptionResourceKeys.UIValidation_R205_RequestInHoliday, "امکان ثبت درخواست در روز بدون شیفت میسر نمی باشد");
                    }
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "RequestValidator", "R205_RequestInHoliday");
                throw ex;
            }
        }

        private object GetRuleParameter(DateTime currentDate, Person person, int ruleIdentifier, string parameterName)
        {
            if (person.AssignedRuleList != null)
            {
                AssignedRule ar = person.AssignedRuleList.Where(x => x.FromDate <= currentDate && x.ToDate >= currentDate && x.IdentifierCode == ruleIdentifier).FirstOrDefault();
                if (ar != null)
                {
                    EntityRepository<AssignRuleParameter> paramRep = new EntityRepository<AssignRuleParameter>();
                    IList<AssignRuleParameter> paramList = paramRep.Find(x => x.Rule.ID == ar.RuleId).ToList();

                    AssignRuleParameter asp = paramList.Where(x => x.FromDate <= currentDate && x.ToDate >= currentDate).FirstOrDefault();
                    if (asp != null)
                    {
                        RuleParameter parameter = asp.RuleParameterList.Where(x => x.Name.ToLower().Equals(parameterName.ToLower())).FirstOrDefault();

                        if (parameter != null)
                            return parameter.Value;
                    }
                }
            }
            return null;
        }
        private void AddException(ExceptionResourceKeys key, string msg)
        {
            exception.Add(new ValidationException(key, msg, ExceptionSrc));
        }
        private void AddWarning(ExceptionResourceKeys key, string msg)
        {
            exception.Add(new ValidationWarning(key, msg, ExceptionSrc));
        }
    }
}
