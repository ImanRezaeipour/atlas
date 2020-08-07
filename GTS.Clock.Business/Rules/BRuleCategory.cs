using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Model;
using GTS.Clock.Model.Charts;
using GTS.Clock.Business.Security;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Model.AppSetting;
using GTS.Clock.Infrastructure;
using GTS.Clock.Business.Presentaion_Helper.Proxy;
using System.Collections;
using NHibernate;
using GTS.Clock.Business.Temp;
using System.Web.Configuration;
using NHibernate.Criterion;

namespace GTS.Clock.Business.Rules
{
    public class BRuleCategory : BaseBusiness<RuleCategory>
    {
       
        UIValidationGroupingRepository uivalidationGroupingRepository = new UIValidationGroupingRepository();
        int operationBatchSizeValue = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings[OperationBatchSize.OperationBatchSizeKey.ToString()]);
        IDataAccess accessPort = new BUser();
        private ISession NHSession = NHibernateSessionManager.Instance.GetSession();
        const string ExceptionSrc = "GTS.Clock.Business.Rules.BRuleCategory";
        private RuleCategoryRepository RuleCategoryRep = new RuleCategoryRepository(false);
        private RuleRepository RuleRep = new RuleRepository();
        private AssignRuleParameterRepository AssignRuleParameterRep = new AssignRuleParameterRepository(false);
        private int OperationBatchSizeValue = int.Parse(WebConfigurationManager.AppSettings[OperationBatchSize.OperationBatchSizeKey.ToString()]);
        private BTemp bTemp = new BTemp();


        public BRuleCategory()
        {
            this.EnableInsertValidate = true;
            this.EnableUpdateValidate = true;
        }

        /// <summary>
        /// قانون های ما به ازای "الگوی قانون"های ورودی را ایجاد نموده و به خصوصیت "لیست قانون" های "دسته قوانین" ورودی اضافه می نماید"
        /// همچنین خصوصیت "شناسه دسته"ی قانون های ایجاد شده را برابر "دسته قانون" ورودی قرار می دهد
        /// </summary>
        /// <returns></returns>
        private void SetInsertedMutualRules(RuleCategory ruleCategory)
        {
            try
            {
                if (ruleCategory.RuleList == null)
                    ruleCategory.RuleList = new List<Rule>();
                this.CheckForcibleRulesExisting(ref ruleCategory, UIActionType.ADD);
                foreach (RuleTemplate RuleTmp in this.RuleRep.GetRuleTemplates(ruleCategory.InsertedTemplateIDs))
                {
                    ruleCategory.RuleList.Add(new Rule()
                    {
                        Category = ruleCategory,
                        IdentifierCode = RuleTmp.IdentifierCode,
                        Name = RuleTmp.Name,
                        Order = RuleTmp.Order,
                        Script = RuleTmp.Script,
                        TemplateId = RuleTmp.ID,
                        TypeId = RuleTmp.TypeId,
                        IsForcible = RuleTmp.IsForcible
                    });
                }
            }
            catch (Exception ex)
            {
                LogException(ex, "BRuleCategory", "SetInsertedMutualRules");
                throw ex;
            }
        }

        /// <summary>در اینجا با واکشی دسته قانون ورودی از پایگاه داده 2 کار انجام می شود:
        /// 1- قوانین ما به ازای الگوی قوانین جدیدی که به دسته قانون اضافه شده اند را ایجاد و به دسته قانون اضافه می نماید 
        /// 2- قوانین که از دسته قانون حذف شده اند را حذف می نماید
        /// </summary>
        /// <returns></returns>
        private RuleCategory SetUpdatedMutualRules(RuleCategory ruleCategory)
        {
            try
            {
                this.CheckForcibleRulesExisting(ref ruleCategory, UIActionType.EDIT);

                RuleCategory persistedRuleCategory = null;
                persistedRuleCategory = this.RuleCategoryRep.GetById(ruleCategory.ID, false);
                persistedRuleCategory.Name = ruleCategory.Name;
                persistedRuleCategory.Discription = ruleCategory.Discription;
                persistedRuleCategory.IsRoot = ruleCategory.IsRoot;

                IList<Rule> DeletedRules = persistedRuleCategory
                                            .RuleList
                                            .Where(x => ruleCategory.DeletedTemplateIDs.Contains(x.TemplateId))
                                            .ToList<Rule>();

                Decimal[] InsertedRules = ruleCategory
                                          .InsertedTemplateIDs
                                          .Where(x => !persistedRuleCategory.RuleList
                                                                            .Select(y => y.TemplateId)
                                                                            .Contains(x))
                                                                            .ToArray<decimal>();
                foreach (Rule rule in DeletedRules)
                {
                    persistedRuleCategory.RuleList.Remove(rule);
                }

                foreach (RuleTemplate bRule in this.RuleRep.GetRuleTemplates(InsertedRules))
                {
                    persistedRuleCategory.RuleList.Add(new Rule()
                    {
                        Category = ruleCategory,
                        IdentifierCode = bRule.IdentifierCode,
                        Name = bRule.Name,
                        Order = bRule.Order,
                        Script = bRule.Script,
                        TemplateId = bRule.ID,
                        TypeId = bRule.TypeId,
                        IsForcible = bRule.IsForcible
                    });
                }
                return persistedRuleCategory;
            }
            catch (Exception ex)
            {
                LogException(ex, "BRuleCategory", "SetInsertedMutualRules");
                throw ex;
            }
        }

        /// <summary>
        /// قوانین اجباری را اضافه میکند
        /// </summary>
        /// <param name="ruleCategory"></param>
        /// <param name="action"></param>
        private void CheckForcibleRulesExisting(ref RuleCategory ruleCategory, UIActionType action)
        {
            try
            {
                IList<Rule> ruleCategoryAssignedRules = this.RuleCategoryRep.GetRulesByRuleCatID(ruleCategory.ID);
                IList<RuleTemplate> ForcibleRuleTemplatesList = this.GetForcibleRuleTemplates();

                decimal[] InsertRuleTemplatesTempList = new decimal[ForcibleRuleTemplatesList.Count];
                InsertRuleTemplatesTempList = ruleCategory.InsertedTemplateIDs;

                foreach (RuleTemplate ForcibleRuleTemplateItem in ForcibleRuleTemplatesList)
                {
                    var InsertTemplateCondition = false;
                    switch (action)
                    {
                        case UIActionType.ADD:
                            InsertTemplateCondition = ruleCategory.InsertedTemplateIDs != null && !ruleCategory.InsertedTemplateIDs.Contains(ForcibleRuleTemplateItem.ID);
                            break;
                        case UIActionType.EDIT:
                            InsertTemplateCondition = ruleCategory.InsertedTemplateIDs != null && !ruleCategory.InsertedTemplateIDs.Contains(ForcibleRuleTemplateItem.ID) && this.RuleCategoryRep.GetRulesByRuleCatID(ruleCategory.ID).Where(x => x.TemplateId == ForcibleRuleTemplateItem.ID).Count() == 0;

                            if (ruleCategory.DeletedTemplateIDs != null && ruleCategory.DeletedTemplateIDs.Contains(ForcibleRuleTemplateItem.ID))
                                ruleCategory.DeletedTemplateIDs = ruleCategory.DeletedTemplateIDs.Where(x => x != ForcibleRuleTemplateItem.ID).ToArray();
                            break;
                    }
                    if (InsertTemplateCondition)
                    {
                        Array.Resize(ref InsertRuleTemplatesTempList, InsertRuleTemplatesTempList.Length + 1);
                        InsertRuleTemplatesTempList[InsertRuleTemplatesTempList.Length - 1] = ForcibleRuleTemplateItem.ID;
                        ruleCategory.InsertedTemplateIDs = InsertRuleTemplatesTempList;
                    }
                }
            }
            catch (Exception ex)
            {
                LogException(ex, "BRuleCategory", "CheckForcibleRulesExisting");
                throw ex;
            }
        }

        public IList<RuleTemplate> GetForcibleRuleTemplates()
        {
            return this.RuleCategoryRep.GetForcibleRuleTemplates();
        }

        /// <summary>
        /// ریشه درخت دسته قوانین را بر می گرداند در اینجا اگر ریشه وجود نداشت آن را ایجاد می نماید
        /// </summary>
        /// <returns></returns>
        public RuleCategory GetRoot()
        {
            try
            {
                IList<RuleCategory> roots = RuleCategoryRep.GetRoot();
                if (roots.Count == 0)
                {
                    roots.Add(RuleCategoryRep.Save(new RuleCategory() { Name = "دسته قوانین", Discription = "", IsRoot = true }));
                    if (RuleCategoryRep.GetRoot().Count > 1)
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.RuleCategoryRootMoreThanOne, "تعداد ریشه چارت قوانین در دیتابیس نامعتبر است", ExceptionSrc);
                    }
                }
                RuleCategory root = roots.First();
                IList<decimal> ids = accessPort.GetAccessibleRuleGroups();
                IList<RuleCategory> childs = new List<RuleCategory>();
                if (root.ChildList != null)
                {
                    foreach (RuleCategory rc in root.ChildList)
                    {
                        if (ids.Contains(rc.ID) && childs.Where(ruleCategory => ruleCategory.ID == rc.ID).Count() == 0)
                        {
                            childs.Add(rc);
                        }
                    }
                }
                root.ChildList = childs;
                NHibernateSessionManager.Instance.ClearSession();
                return root;
            }
            catch (Exception ex)
            {
                LogException(ex, "BRuleCategory", "GetRoot");
                throw ex;
            }
        }

        public override IList<RuleCategory> GetAll()
        {
            IList<RuleCategory> list = null;
            IList<decimal> accessableIDs = accessPort.GetAccessibleRuleGroups();
            if (accessableIDs.Count < this.OperationBatchSizeValue && this.OperationBatchSizeValue < 2100)
            {
                list = NHSession.QueryOver<RuleCategory>()
                                .Where(x => x.ID.IsIn(accessableIDs.ToArray()))
                                .List<RuleCategory>();
            }
            else
            {
                RuleCategory ruleCategoryAlias = null;
                GTS.Clock.Model.Temp.Temp tempAlias = null;
                string operationGUID = this.bTemp.InsertTempList(accessableIDs);
                list = this.NHSession.QueryOver(() => ruleCategoryAlias)
                                      .JoinAlias(() => ruleCategoryAlias.TempList, () => tempAlias)
                                      .Where(() => tempAlias.OperationGUID == operationGUID)
                                      .List<RuleCategory>();
                this.bTemp.DeleteTempList(operationGUID);
            }
            return list;
        }

        /// <summary>
        /// قوانین "دسته"ای که شناسه آن داده شده را برمی گرداند
        /// </summary>
        /// <param name="RuleCatID"></param>
        /// <returns></returns>
        public IList<Rule> GetRulesByRuleCatID(decimal RuleCatID)
        {
            try
            {
                return RuleCategoryRep.GetRulesByRuleCatID(RuleCatID);
            }
            catch (Exception ex)
            {
                LogException(ex, "BRuleCategory", "GetRulesByRuleCatID");
                throw ex;
            }
        }

        public decimal SaveChanges(RuleCategory ruleCategory, UIActionType action)
        {
            switch (action)
            {
                case UIActionType.ADD:
                    {
                        this.InsertValidate(ruleCategory);
                        this.EnableInsertValidate = false;
                        this.SetInsertedMutualRules(ruleCategory);
                        ruleCategory.Parent = this.GetRoot();
                        return base.SaveChanges(ruleCategory, action);
                    }
                case UIActionType.EDIT:
                    {
                        this.UpdateValidate(ruleCategory);
                        this.EnableUpdateValidate = false;

                        // Rezaei Note
                        if (ruleCategory.IsGroup)
                        {
                            this.CheckUserInterfaceRuleGroup(ruleCategory);
                            return base.SaveChanges(ruleCategory, action);
                        }
                           

                        ruleCategory = this.SetUpdatedMutualRules(ruleCategory);
                        this.CheckUserInterfaceRuleGroup(ruleCategory);
                        return base.SaveChanges(ruleCategory, action);
                    }
                case UIActionType.DELETE:
                    {
                        return base.SaveChanges(ruleCategory, action);
                    }
            }
            return 0;
        }



        public bool EnableInsertValidate
        {
            get;
            set;
        }

        public bool EnableUpdateValidate
        {
            get;
            set;
        }

        /// <summary>
        /// کپی دسته قانون
        /// </summary>
        /// <param name="ruleCatId"></param>
        /// <returns></returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public RuleCategory CopyRuleCategory(decimal ruleCatId)
        {
            RuleCategory ruleCat = this.GetByID(ruleCatId);
            RuleCategory newRuleCat = new RuleCategory();
            if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
            {
                newRuleCat.Name = String.Format("کپی {0}", ruleCat.Name);
            }
            else
            {
                newRuleCat.Name = String.Format("Copy Of {0}", ruleCat.Name);
            }
            newRuleCat.IsRoot = false;
            newRuleCat.Discription = ruleCat.Discription;
            newRuleCat.CustomCode = ruleCat.CustomCode;

            #region Parent
            newRuleCat.Parent = this.GetRoot();

            #endregion

            #region Child
            ruleCat.ChildList = new List<RuleCategory>();
            foreach (RuleCategory child in ruleCat.ChildList)
            {
                RuleCategory ch = new RuleCategory();
                ch.ID = child.ID;
                ruleCat.ChildList.Add(ch);
            }
            #endregion

            #region Rule
            newRuleCat.RuleList = new List<Rule>();

            foreach (Rule rule in ruleCat.RuleList)
            {
                Rule r = new Rule();
                r.Category = newRuleCat;
                r.IdentifierCode = rule.IdentifierCode;
                r.IsPeriodic = rule.IsPeriodic;
                r.Name = rule.Name;
                r.Order = rule.Order;
                r.Script = rule.Script;
                r.TemplateId = rule.TemplateId;
                r.TypeId = rule.TypeId;
                r.IsForcible = rule.IsForcible;

                #region Assign Rule Param

                r.AssignRuleParamList = new List<AssignRuleParameter>();
                if (rule.HasParameter)
                {
                    IList<AssignRuleParameter> AssignRuleParamList = this.AssignRuleParameterRep.GetAssigneRuleParametersListByRuleID(rule.ID);
                    foreach (AssignRuleParameter assginParam in AssignRuleParamList)
                    {
                        AssignRuleParameter ass = new AssignRuleParameter();
                        ass.FromDate = assginParam.FromDate;
                        ass.ToDate = assginParam.ToDate;
                        ass.Rule = r;

                        #region Rule Parameter
                        ass.RuleParameterList = new List<RuleParameter>();

                        foreach (RuleParameter param in assginParam.RuleParameterList)
                        {
                            RuleParameter rp = new RuleParameter();
                            rp.Name = param.Name;
                            rp.Title = param.Title;
                            rp.Type = param.Type;
                            rp.Value = param.Value;
                            rp.AssignRuleParameter = ass;
                            ass.RuleParameterList.Add(rp);
                        }
                        #endregion

                        r.AssignRuleParamList.Add(ass);

                    }
                }
                #endregion

                newRuleCat.RuleList.Add(r);
            }


            #endregion

            //NHibernateSessionManager.Instance.ClearSession();
            NHibernateSessionManager.Instance.GetSession().Evict(ruleCat);
            this.SaveChanges(newRuleCat, UIActionType.ADD);
            return new RuleCategory() { ID = newRuleCat.ID, Name = newRuleCat.Name };
        }

        protected override void OnSaveChangesSuccess(RuleCategory ruleCategory, UIActionType action)
        {

            if (action == UIActionType.ADD)
            {
                new BDataAccess().InsertDataAccess(Infrastructure.DataAccessLevelOperationType.Single, Infrastructure.DataAccessParts.RuleGroup, ruleCategory.ID, BUser.CurrentUser.ID, null, "");
            }

            if (action == UIActionType.ADD || action == UIActionType.EDIT)
            {
                DateTime fromDate, toDate;
                RuleCategory ruleCat = base.GetByID(ruleCategory.ID);

                if (ruleCat.PersonRuleCatAssignList != null && ruleCat.PersonRuleCatAssignList.Count > 0)
                {
                    fromDate = ruleCat.PersonRuleCatAssignList.Min(x => Utility.ToMildiDateTime(x.FromDate));
                    toDate = ruleCat.PersonRuleCatAssignList.Max(x => Utility.ToMildiDateTime(x.ToDate));
                }
                else
                {
                    fromDate = DateTime.Now.AddYears(-2);
                    toDate = DateTime.Now.AddYears(5);
                }


                //دوره تاریخ پیشفرض پارامتر
                foreach (Rule rule in ruleCat.RuleList)
                {
                    if (rule.HasParameter && (rule.AssignRuleParamList == null || rule.AssignRuleParamList.Count == 0))
                    {
                        BRuleParameter busRuleParam = new BRuleParameter(rule.TemplateId, ruleCat.ID);
                        busRuleParam.InsertParameter(new List<RuleTemplateParameter>(), fromDate, toDate , UIActionType.ADD);
                    }
                    else if (rule.HasParameter && (rule.AssignRuleParamList != null || rule.AssignRuleParamList.Count != 0) && ((int)rule.IdentifierCode == (int)RuleParameterType.MissionParameterOne || 
                                                                                                                                (int)rule.IdentifierCode == (int)RuleParameterType.MissionParameterOne ||
                                                                                                                                (int)rule.IdentifierCode == (int)RuleParameterType.Work ||
                                                                                                                                (int)rule.IdentifierCode == (int)RuleParameterType.Absence ||
                                                                                                                                (int)rule.IdentifierCode == (int)RuleParameterType.LeaveParameter3012 ||
                                                                                                                                (int)rule.IdentifierCode == (int)RuleParameterType.LeaveParemeter3017 ||
                                                                                                                                (int)rule.IdentifierCode == (int)RuleParameterType.Miscellaneous
                                                                                                                               ))
                    {
                        BRuleParameter busRuleParam = new BRuleParameter(rule.TemplateId, ruleCat.ID);
                        busRuleParam.InsertParameter(new List<RuleTemplateParameter>(), fromDate, toDate , UIActionType.EDIT);
                    }
                }
            }
        }

        protected override void InsertValidate(RuleCategory ruleCategory)
        {
            if (!this.EnableInsertValidate)
                return;

            UIValidationExceptions exception = new UIValidationExceptions();
            if (Utility.IsEmpty(ruleCategory.Name))
            {
                exception.Add(ExceptionResourceKeys.RuleCategoryNameRequierd, "درج - نام دسته قانون نباید خالی باشد", ExceptionSrc);
            }
            else
            {
                if (RuleCategoryRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => ruleCategory.Name), ruleCategory.Name)) > 0)
                {
                    exception.Add(ExceptionResourceKeys.RuleCategoryNameRepeated, "درج - نام دسته قانون نباید تکراری باشد", ExceptionSrc);
                }
                //TODO: درصورتیکه نیاز شد عملیات به دلیل وارد نکردن قانون برای دسته قانون متوقف شود این کد اصلاح گردد
                //else if (ruleCategory.InsertedTemplateIDs == null 
                //            || 
                //         ruleCategory.InsertedTemplateIDs.Count() == 0)
                //{
                //    return ;
                //}

            }

            if (exception.Count > 0)
            {
                throw exception;
            }

        }

        protected override void UpdateValidate(RuleCategory ruleCategory)
        {
            if (!this.EnableUpdateValidate)
                return;
            UIValidationExceptions exception = new UIValidationExceptions();
            if (Utility.IsEmpty(ruleCategory.Name))
            {
                exception.Add(ExceptionResourceKeys.RuleCategoryNameRequierd, "بروزرسانی - نام دسته قانون نباید خالی باشد", ExceptionSrc);
            }
            else
            {
                if (RuleCategoryRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => ruleCategory.Name), ruleCategory.Name),
                                                            new CriteriaStruct(Utility.GetPropertyName(() => ruleCategory.ID), ruleCategory.ID, CriteriaOperation.NotEqual)) > 0)
                {
                    exception.Add(ExceptionResourceKeys.RuleCategoryNameRepeated, "بروزرسانی - نام دسته قانون نباید تکراری باشد", ExceptionSrc);
                }
                //TODO: کد اصلاح گردد
                //else if ((ruleCategory.InsertedTemplateIDs == null ||
                //              ruleCategory.InsertedTemplateIDs.Count() == 0)
                //            &&
                //            (ruleCategory.DeletedTemplateIDs == null ||
                //              ruleCategory.DeletedTemplateIDs.Count() == 0))
                //{
                //    return ;
                //}

            }

            if (exception.Count > 0)
            {
                throw exception;
            }

        }

        protected override void DeleteValidate(RuleCategory ruleCategory)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            if (RuleCategoryRep.IsRoot(ruleCategory.ID))
            {
                exception.Add(ExceptionResourceKeys.RuleCategoryRootDeleteIllegal, "حذف - ریشه دسته قوانین نباید حذف شود", ExceptionSrc);
            }
            else
            {
                if (RuleCategoryRep.HasPerson(ruleCategory.ID))
                {
                    exception.Add(ExceptionResourceKeys.RuleCategoryUsedByPerson, "حذف - این دسته قانون به شخص انتساب داده شده است", ExceptionSrc);
                }
            }

            if (exception.Count > 0)
            {
                throw exception;
            }

        }
        private void CheckUserInterfaceRuleGroup(RuleCategory ruleCategory)
        {
            try
            {
                //foreach (PersonRuleCatAssignment assgn in ruleCategory.PersonRuleCatAssignList)
                //{
                //    if (assgn.Person.PersonTASpec.UIValidationGroup != null)
                //        base.UIValidator.GetCalculationLockDateByGroup(assgn.Person.PersonTASpec.UIValidationGroup.ID);
                //}                                                                                                                                     
                IList<decimal> UiValidationGroupIdList = uivalidationGroupingRepository.GetUivalidationIdListByRuleCategory(ruleCategory.ID);
                foreach (decimal uiValidateionGrpId in UiValidationGroupIdList)
                {
                    DateTime calculationLockDate = base.UIValidator.GetCalculationLockDateByGroup(uiValidateionGrpId); 
                }
            }
            catch (Exception ex)
            {
                LogException(ex, "BRuleCategory", "CheckUserInterfaceRuleGroup");
                throw ex;
            }
        }
        protected override void UpdateCFP(RuleCategory obj, UIActionType action)
        {
            if (action == UIActionType.EDIT)
            {
                Dictionary<decimal, DateTime> uivalidationGroupIdDic = new Dictionary<decimal, DateTime>();
                IList<PersonRuleCatAssignment> assignPersonRulCateList = obj.PersonRuleCatAssignList;
                IList<CFP> cfpPersonList = new List<CFP>();
                if (assignPersonRulCateList.Count > 0)
                    cfpPersonList = base.GetCFPPersons(assignPersonRulCateList.Select(a => a.Person.ID).ToList<decimal>());
                IList<decimal> UiValidationGroupIdList = uivalidationGroupingRepository.GetUivalidationIdListByRuleCategory(obj.ID);
                IList<decimal> cfpPersonIdInsertList = new List<decimal>();
                foreach (decimal uiValidateionGrpId in UiValidationGroupIdList)
                {

                    if (!uivalidationGroupIdDic.ContainsKey(uiValidateionGrpId))
                    {
                        DateTime calculationLockDate = base.UIValidator.GetCalculationLockDateByGroup(uiValidateionGrpId);
                        uivalidationGroupIdDic.Add(uiValidateionGrpId, calculationLockDate);
                    }

                }
                base.UpdateCfpByRuleCategory(obj.ID, uivalidationGroupIdDic);
                cfpPersonIdInsertList = assignPersonRulCateList.Where(p => cfpPersonList != null && !cfpPersonList.Select(c => c.PrsId).ToList().Contains(p.Person.ID)).Select(p => p.Person.ID).Distinct().ToList<decimal>();
                if (cfpPersonIdInsertList.Count > 0)
                    base.InsertCfpByRuleCategory(cfpPersonIdInsertList, obj.ID, uivalidationGroupIdDic);
            }
        }

        protected override void GetReadyBeforeSave(RuleCategory obj, UIActionType action)
        {
            base.GetReadyBeforeSave(obj, action);
        }

        public IList<RuleParametersValidationFeaturesProxy> ValidateRuleGroupRulesParameters(decimal ruleCategoryID)
        {
            try
            {
                IList<RuleParametersValidationFeaturesProxy> ValidateRuleParametersFeaturesProxyList = new List<RuleParametersValidationFeaturesProxy>();
                RuleParametersValidationFeaturesProxy ruleParametersValidationFeaturesProxy = null;

                RuleCategory RuleCategoryProxy = this.GetByID(ruleCategoryID);

                foreach (Rule ruleItem in RuleCategoryProxy.RuleList)
                {
                    if (ruleItem.HasParameter)
                    {
                        if (ruleItem.AssignRuleParamList == null || ruleItem.AssignRuleParamList.Count == 0)
                        {
                            ruleParametersValidationFeaturesProxy = new RuleParametersValidationFeaturesProxy();
                            ruleParametersValidationFeaturesProxy.ValidationType = RuleParametersValidationType.RuleParametersNoRegulation;
                            ruleParametersValidationFeaturesProxy.RelativeRule = ruleItem;
                            ValidateRuleParametersFeaturesProxyList.Add(ruleParametersValidationFeaturesProxy);
                        }

                        if (ruleItem.AssignRuleParamList != null && ruleItem.AssignRuleParamList.Count > 0)
                        {
                            DateTime MinAssignRuleParamDateRange = ruleItem.AssignRuleParamList.Min(ruleParam => ruleParam.FromDate);
                            DateTime MaxAssignRuleParamDateRange = ruleItem.AssignRuleParamList.Max(ruleParam => ruleParam.ToDate);

                            foreach (PersonRuleCatAssignment personRuleCatAssignmentItem in RuleCategoryProxy.PersonRuleCatAssignList)
                            {
                                if (MinAssignRuleParamDateRange.CompareTo(Utility.ToMildiDateTime(personRuleCatAssignmentItem.FromDate)) > 0 || MaxAssignRuleParamDateRange.CompareTo(Utility.ToMildiDateTime(personRuleCatAssignmentItem.ToDate)) < 0)
                                {
                                    ruleParametersValidationFeaturesProxy = new RuleParametersValidationFeaturesProxy();
                                    ruleParametersValidationFeaturesProxy.ValidationType = RuleParametersValidationType.RuleParametersDateRangesNoCover;
                                    ruleParametersValidationFeaturesProxy.RelativeRule = ruleItem;
                                    ruleParametersValidationFeaturesProxy.RelativePersonRuleCatAssignment = personRuleCatAssignmentItem;
                                    ValidateRuleParametersFeaturesProxyList.Add(ruleParametersValidationFeaturesProxy);
                                }
                            }
                        }
                    }
                }

                return ValidateRuleParametersFeaturesProxyList;

            }
            catch (Exception ex)
            {
                LogException(ex, "BRuleCategory", "ValidateRuleGroupRulesParameters");
                throw ex;
            }
        }
        public decimal [] GetRuleTemplatIds(decimal[] IdentifierCode)
        {
            try
            {
                decimal[] ruleTemplateIds = new decimal[]{};
                RuleTemplate ruleTemplateAlias = null;
                IList<decimal> RuleTemplateIds = NHSession.QueryOver(() => ruleTemplateAlias)
                                                           .Where(() => ruleTemplateAlias.IdentifierCode.IsIn(IdentifierCode.ToArray()))
                                                           .Select(x => x.ID)
                                                           .List<decimal>();                               
                return RuleTemplateIds.ToArray();

            }
            catch (Exception ex)
            {
                LogException(ex, "BRuleCategory", "GetRuleTemplatIds");
                throw ex;
            }
        }
        public IList<RuleCategory> GetRuleCategory(string searchItem)
          {
              RuleCategory ruleCategoryAlias = null;
               IList<RuleCategory> RuleCategoryList = NHSession.QueryOver(() => ruleCategoryAlias)
                                                                 .Where(() => ruleCategoryAlias.Name.IsInsensitiveLike(searchItem, MatchMode.Anywhere))
                                                                 .List<RuleCategory>();
                 return RuleCategoryList;
          }
        
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckRulesGroupLoadAccess()
        {
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertRuleGroup(RuleCategory ruleCategory, UIActionType UAT)
        {
            return this.SaveChanges(ruleCategory, UAT);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal UpdateRuleGroup(RuleCategory ruleCategory, UIActionType UAT)
        {
            return this.SaveChanges(ruleCategory, UAT);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal DeleteRuleGroup(RuleCategory ruleCategory, UIActionType UAT)
        {
            return this.SaveChanges(ruleCategory, UAT);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public IList<RuleParametersValidationFeaturesProxy> ValidateRuleGroupRulesParameters_onRuleGroupInsert(decimal ruleCategoryID)
        {
            return this.ValidateRuleGroupRulesParameters(ruleCategoryID);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public IList<RuleParametersValidationFeaturesProxy> ValidateRuleGroupRulesParameters_onRuleGroupUpdate(decimal ruleCategoryID)
        {
            return this.ValidateRuleGroupRulesParameters(ruleCategoryID);
        }




        public IList<RuleCategory> GetRuleCategoryChilds(decimal parentId)
        {
            try
            {
                IList<decimal> accessableIDs = accessPort.GetAccessibleRuleGroups();
                IList<RuleCategory> list = new List<RuleCategory>();
                RuleCategory ruleCatAlias = null;

                GTS.Clock.Model.Temp.Temp tempAlias = null;

                if (accessableIDs.Count < this.operationBatchSizeValue && this.operationBatchSizeValue < 2100)
                {
                    list = RuleCategoryRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new RuleCategory().ParentId), parentId),
                                                                 new CriteriaStruct(Utility.GetPropertyName(() => new RuleCategory().ID), accessableIDs.ToArray(), CriteriaOperation.IN),
                                                                 new CriteriaStruct(Utility.GetPropertyName(() => new RuleCategory().SubSystemId), SubSystemIdentifier.TimeAtendance));

                }
                else
                {

                    string operationGUID = this.bTemp.InsertTempList(accessableIDs);
                    list = NHSession.QueryOver(() => ruleCatAlias)
                                                      .JoinAlias(() => ruleCatAlias.TempList, () => tempAlias)
                                                      .Where(() => tempAlias.OperationGUID == operationGUID && ruleCatAlias.ParentId == parentId && ruleCatAlias.SubSystemId == SubSystemIdentifier.TimeAtendance)
                                                      .List<RuleCategory>();
                    this.bTemp.DeleteTempList(operationGUID);




                }

              //  IList<ReportParameter> parameters = reportParameterRep.GetAll();

                //foreach (RuleCategory report in list)
                //{
                //    if (report.IsReport && !report.IsDesignedReport)
                //    {
                //        report.HasParameter = this.HasReportParameter(report.ReportFile.ID, parameters);
                //    }
                //    if (report.IsDesignedReport)
                //        report.HasParameter = true;
                //}

                List<RuleCategory> result = new List<RuleCategory>();
                result.AddRange(list.Where(x => x.IsGroup).OrderBy(x=>x.IsGroup));
                result.AddRange(list.Where(x => !x.IsGroup).OrderBy(x=>x.IsGroup));
                return result;
            }
            catch (Exception ex)
            {
                LogException(ex, "BRulesGroup", "GetRulesGroupChilds");
                throw ex;
            }
        }

    }
}


