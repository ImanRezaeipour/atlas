using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.Log;
using GTS.Clock.Model;
using GTS.Clock.Model.UIValidation;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Business.Security;
using GTS.Clock.Business;
using NHibernate;
using GTS.Clock.Model.Concepts;
using NHibernate.Transaction;
using NHibernate.Criterion;
using GTS.Clock.Model.Temp;
using GTS.Clock.Business.Temp;
using GTS.Clock.Business.RequestFlow;
using GTS.Clock.Business.UIValidation;
using System.Web.Script.Serialization;
using GTS.Clock.Business.Proxy.UiValidation;
using System.Drawing;

namespace GTS.Clock.Business.UIValidation
{
    /// <summary>
    /// created at: 4/4/2012 12:29:23 PM
    /// by        : Farhad Salvati
    /// write your name here
    /// </summary>
    public class BUIValidationGroup : BaseBusiness<UIValidationGroup>
    {
        private const string ExceptionSrc = "GTS.Clock.Business.UIValidation.BUIValidationGroup";
        private EntityRepository<UIValidationGroup> objectRep = new EntityRepository<UIValidationGroup>();
        private EntityRepository<UIValidationRuleGroup> RuleGroupRep = new EntityRepository<UIValidationRuleGroup>();
        private EntityRepository<Precard> RulePrecard = new EntityRepository<Precard>();
        private ISession NHSession = NHibernateSessionManager.Instance.GetSession();
        IDataAccess accessPort = new BUser();
        private BTemp bTemp = new BTemp();
        int operationBatchSizeValue = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings[OperationBatchSize.OperationBatchSizeKey.ToString()]);
        EntityRepository<UIValidationRuleGroupPrecard> rulePrecardRep = new EntityRepository<UIValidationRuleGroupPrecard>(false);
        EntityRepository<UIValidationRuleGroup> ruleGroupRep = new EntityRepository<UIValidationRuleGroup>(false);
        EntityRepository<UIValidationRuleParam> ruleParamRep = new EntityRepository<UIValidationRuleParam>(false);
        private Random random = new Random();
        public JavaScriptSerializer JsSerializer
        {
            get
            {
                return new JavaScriptSerializer();
            }
        }

        public UIValidationRuleGroup GetRuleGroup(decimal ruleId, decimal GroupId)
        {
            try
            {
                UIValidationRuleGroup ruleGroupAlias = null;
                UIValidationRule ruleAlias = null;
                UIValidationGroup GroupAlias = null;
                UIValidationRuleGroup ruleGroup = NHSession.QueryOver(() => ruleGroupAlias)
                                                           .JoinAlias(() => ruleGroupAlias.ValidationRule, () => ruleAlias)
                                                           .JoinAlias(() => ruleGroupAlias.ValidationGroup, () => GroupAlias)
                                                           .Where(() => ruleAlias.ID == ruleId && GroupAlias.ID == GroupId)
                                                           .SingleOrDefault();
                return ruleGroup;
            }
            catch (Exception ex)
            {
                LogException(ex, "BUIValidationGroup", "GetRuleGroup");
                throw ex;
            }
        }
        /// <summary>
        /// به محض ایجاد یک گروه, قوانین را به آن انتساب میدهد با اکتیو فالس
        /// </summary>
        /// <param name="groupId"></param>     
        public void SetUiValidationRulesGroup(decimal groupId)
        {
            try
            {
                using (NHibernateSessionManager.Instance.BeginTransactionOn())
                {
                    IList<UIValidationRule> ruleList = new EntityRepository<UIValidationRule>(false).GetAll();
                    ruleList = ruleList.Where(x => x.SubsystemID == (int)SubSystemIdentifier.TimeAtendance).OrderBy(x => x.Order).ToList();

                    foreach (UIValidationRule rule in ruleList)
                    {
                        UIValidationRuleGroup ruleGroup = new UIValidationRuleGroup();
                        ruleGroup.ValidationGroup = new UIValidationGroup() { ID = groupId };
                        ruleGroup.ValidationRule = new UIValidationRule() { ID = rule.ID };
                        ruleGroup.Active = false;
                        ruleGroup.Warning = false;
                        RuleGroupRep.Save(ruleGroup);
                    }
                    NHibernateSessionManager.Instance.CommitTransactionOn();
                }

            }
            catch (Exception ex)
            {
                NHibernateSessionManager.Instance.RollbackTransactionOn();
                LogException(ex, "BUIValidationGroup", "SetUiValidationRuleGroup");
                throw ex;
            }
        }

        /// <summary>
        /// لیست قوانین یک گروه را برمیگرداند
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>

        public IList<UiValidationRuleGroupProxy> GetGroupRules(decimal groupId)
        {
            try
            {
                using (NHibernateSessionManager.Instance.BeginTransactionOn())
                {
                    IList<UIValidationRuleGroup> ruleGroupList = null;
                    IList<UiValidationRuleGroupProxy> result = new List<UiValidationRuleGroupProxy>();
                    ruleGroupList = NHSession.QueryOver<UIValidationRuleGroup>()
                                             .Where(x => x.ValidationGroup.ID == groupId)
                                             .OrderBy(x => x.ValidationRule.ID)
                                             .Asc
                                             .List();
                    foreach (UIValidationRuleGroup rulegroup in ruleGroupList)
                    {
                        UiValidationRuleGroupProxy ruleGroupProxy = new UiValidationRuleGroupProxy();
                        ruleGroupProxy.ID = rulegroup.ID;
                        ruleGroupProxy.CustomCode = int.Parse(rulegroup.ValidationRule.CustomCode);
                        ruleGroupProxy.GroupID = rulegroup.ValidationGroup.ID;
                        ruleGroupProxy.RuleID = rulegroup.ValidationRule.ID;
                        ruleGroupProxy.RuleName = rulegroup.ValidationRule.Name;
                        ruleGroupProxy.Active = rulegroup.Active;
                        ruleGroupProxy.Warning = rulegroup.Warning;
                        ruleGroupProxy.RuleType = (int)rulegroup.ValidationRule.Type;
                        ruleGroupProxy.RuleGroupType = rulegroup.ValidationRule.RuleConcept;
                        ruleGroupProxy.RuleGroupID = rulegroup.ID;
                        ruleGroupProxy.ExistRuleTag = rulegroup.ValidationRule.ExistTag;
                        UIValidationIsWarning uiValWarning = UIValidationIsWarning.Nothing;
                        Enum.TryParse<UIValidationIsWarning>(rulegroup.ValidationRule.CustomCode, out uiValWarning);
                        if (Enum.GetValues(typeof(UIValidationIsWarning)).Cast<int>().Contains((int)uiValWarning))
                        {
                            ruleGroupProxy.IsWarningAllowed = true;
                            ruleGroupProxy.RuleColor = "#EEB422";
                        }
                        else
                            ruleGroupProxy.RuleColor = "#FFFFFF";
                        result.Add(ruleGroupProxy);
                    }
                    IList<UIValidationRule> ruleList = NHSession.QueryOver<UIValidationRule>()
                                                                 .Where(x => x.SubsystemID == (int)SubSystemIdentifier.TimeAtendance &&
                                                                            !x.ID.IsIn(ruleGroupList.Select(y => y.ValidationRule.ID).ToArray())
                                                                       )
                                                                 .List<UIValidationRule>();
                    if (ruleList != null)
                    {
                        foreach (UIValidationRule rule in ruleList)
                        {
                            UIValidationRuleGroup ruleGroup = new UIValidationRuleGroup()
                            {
                                ValidationGroup = new UIValidationGroup() { ID = groupId },
                                ValidationRule = rule,
                                Active = false,
                                Warning = false
                            };
                            RuleGroupRep.WithoutTransactSave(ruleGroup);
                        }
                        NHibernateSessionManager.Instance.CommitTransactionOn();
                        ruleGroupList = NHSession.QueryOver<UIValidationRuleGroup>()
                                            .Where(x => x.ValidationGroup.ID == groupId &&
                                                        x.ValidationRule.ID.IsIn(ruleList.Select(y => y.ID).ToArray()))
                                             .OrderBy(x => x.ValidationRule.ID)
                                             .Asc
                                             .List<UIValidationRuleGroup>();

                        foreach (UIValidationRuleGroup rulegroup in ruleGroupList)
                        {
                            UiValidationRuleGroupProxy ruleGroupProxy = new UiValidationRuleGroupProxy();
                            ruleGroupProxy.ID = rulegroup.ID;
                            ruleGroupProxy.CustomCode = int.Parse(rulegroup.ValidationRule.CustomCode);
                            ruleGroupProxy.GroupID = rulegroup.ValidationGroup.ID;
                            ruleGroupProxy.RuleID = rulegroup.ValidationRule.ID;
                            ruleGroupProxy.RuleName = rulegroup.ValidationRule.Name;
                            ruleGroupProxy.Active = rulegroup.Active;
                            ruleGroupProxy.Warning = rulegroup.Warning;
                            ruleGroupProxy.RuleType = (int)rulegroup.ValidationRule.Type;
                            ruleGroupProxy.RuleGroupType = rulegroup.ValidationRule.RuleConcept;
                            ruleGroupProxy.RuleGroupID = rulegroup.ID;
                            ruleGroupProxy.ExistRuleTag = rulegroup.ValidationRule.ExistTag;
                            UIValidationIsWarning uiValWarning = UIValidationIsWarning.Nothing;
                            Enum.TryParse<UIValidationIsWarning>(rulegroup.ValidationRule.CustomCode, out uiValWarning);
                            if (Enum.GetValues(typeof(UIValidationIsWarning)).Cast<int>().Contains((int)uiValWarning))
                            {
                                ruleGroupProxy.IsWarningAllowed = true;
                                ruleGroupProxy.RuleColor = "#EEB422";
                            }
                            else
                                ruleGroupProxy.RuleColor = "#FFFFFF";
                            result.Add(ruleGroupProxy);
                        }
                    }
                    return result.OrderBy(x => x.RuleID).ToList();
                }

            }
            catch (Exception ex)
            {
                NHibernateSessionManager.Instance.RollbackTransactionOn();
                LogException(ex, "BUIValidationGroup", "GetGroupRules");
                throw ex;
            }
        }
        public IList<Precard> GetPrecards(int ruleGroupType, string searchValue, decimal ruleId)
        {
            try
            {
                NHibernate.IQueryOver<Precard, Precard> PrecardQueryExpression = null;
                IList<decimal> accessableIDs = accessPort.GetAccessiblePrecards();
                Precard precardAlias = null;
                PrecardGroups precardGroupsAlias = null;
                UIValidationAllowedRulePrecard allowedRulePrecardAlias = null;
                // UIValidationRule ruleAlias = null;
                IList<Precard> PrecardList = null;
                IList<UIValidationAllowedRulePrecard> AllowedRulePrecardList = null;

                //if (accessableIDs.Count < this.operationBatchSizeValue && this.operationBatchSizeValue < 2100)
                //{


                AllowedRulePrecardList = NHSession.QueryOver(() => allowedRulePrecardAlias)                                                  
                                                   .Where(() => allowedRulePrecardAlias.Rule.ID == ruleId)
                                                   .List<UIValidationAllowedRulePrecard>();

                if (AllowedRulePrecardList.Count != 0)
                {
                    PrecardList = NHSession.QueryOver(() => precardAlias)
                                          .JoinAlias(() => precardAlias.AllowedRulePrecardList, () => allowedRulePrecardAlias)
                                          .Where(() => allowedRulePrecardAlias.Rule.ID == ruleId && precardAlias.Active &&
                                                       (precardAlias.Name.IsInsensitiveLike(searchValue, MatchMode.Anywhere) ||
                                                        precardAlias.Code.IsInsensitiveLike(searchValue, MatchMode.Anywhere)
                                                       )
                                                )
                                          .List<Precard>();                   
                }
                else
                {
                    // if (PrecardList.Count == 0)
                    //{
                    PrecardQueryExpression = NHSession.QueryOver(() => precardAlias)
                                                      .JoinAlias(() => precardAlias.PrecardGroup, () => precardGroupsAlias)
                                                      .Where(() => precardAlias.Active &&
                                                                   precardGroupsAlias.IntLookupKey != (int)PrecardGroupsName.terminate &&
                                                                   (precardAlias.Name.IsInsensitiveLike(searchValue, MatchMode.Anywhere) ||
                                                                   precardAlias.Code.IsInsensitiveLike(searchValue, MatchMode.Anywhere)
                                                                   )
                                                             );
                    switch (ruleGroupType)
                    {
                        case (int)UIValidationRuleGroupType.Hourly:
                            PrecardQueryExpression = PrecardQueryExpression.Where(() => precardAlias.IsHourly);
                            break;
                        case (int)UIValidationRuleGroupType.Daily:
                            PrecardQueryExpression = PrecardQueryExpression.Where(() => precardAlias.IsDaily);
                            break;
                        case (int)UIValidationRuleGroupType.Monthly:
                            PrecardQueryExpression = PrecardQueryExpression.Where(() => precardAlias.IsMonthly);
                            break;
                        case (int)UIValidationRuleGroupType.Etc:
                            break;
                    }
                    PrecardList = PrecardQueryExpression.List<Precard>().Where(x => accessableIDs.ToList<decimal>().Contains(x.ID)).ToList<Precard>();
                    //}
                }
              

                // }
                //else
                //{
                //    GTS.Clock.Model.Temp.Temp tempAlias = null;
                //    string operationGUID = this.bTemp.InsertTempList(accessableIDs);

                //    PrecardList = NHSession.QueryOver(() => precardAlias)
                //                              .JoinAlias(() => precardAlias.AllowedRulePrecardList, () => allowedRulePrecardAlias)
                //                              .JoinAlias(() => precardAlias.TempList, () => tempAlias)
                //                              .Where(() => allowedRulePrecardAlias.Rule.ID == ruleId &&
                //                                           precardAlias.Active &&
                //                                           tempAlias.OperationGUID == operationGUID
                //                                    )
                //                              .List<Precard>();
                //    if (PrecardList.Count == 0)
                //    {
                //        PrecardQueryExpression = NHSession.QueryOver(() => precardAlias)
                //                                          .JoinAlias(() => precardAlias.PrecardGroup, () => precardGroupsAlias)
                //                                         .JoinAlias(() => precardAlias.TempList, () => tempAlias)
                //                                         .Where(() => precardAlias.Active && 
                //                                                  precardGroupsAlias.IntLookupKey !=(int)PrecardGroupsName.terminate&&
                //                                                     (precardAlias.Name.IsInsensitiveLike(searchValue, MatchMode.Anywhere) ||
                //                                                      precardAlias.Code.IsInsensitiveLike(searchValue, MatchMode.Anywhere)
                //                                                     ) &&
                //                                                      tempAlias.OperationGUID == operationGUID
                //                                                );
                //        switch (ruleGroupType)
                //        {
                //            case (int)UIValidationRuleGroupType.Hourly:
                //                PrecardQueryExpression = PrecardQueryExpression.Where(() => precardAlias.IsHourly);
                //                break;
                //            case (int)UIValidationRuleGroupType.Daily:
                //                PrecardQueryExpression = PrecardQueryExpression.Where(() => precardAlias.IsDaily);
                //                break;
                //            case (int)UIValidationRuleGroupType.Monthly:
                //                PrecardQueryExpression = PrecardQueryExpression.Where(() => precardAlias.IsMonthly);
                //                break;
                //            case (int)UIValidationRuleGroupType.Etc:
                //                break;
                //        }
                //       // PrecardList = PrecardQueryExpression.List<Precard>();
                //        PrecardList = PrecardQueryExpression.List<Precard>().Where(x => accessableIDs.ToList<decimal>().Contains(x.ID)).ToList<Precard>();
                //    }
                //    this.bTemp.DeleteTempList(operationGUID);
                // }

                return PrecardList;
            }
            catch (Exception ex)
            {
                LogException(ex, "BUIValidationGroup", "GetPrecards");
                throw ex;
            }
        }

        public IList<UIValidationRulePrecardProxy> GetRulePrecards(decimal groupId, decimal ruleId, int RuleGroupType, string searchValue, int ruleType)
        {
            try
            {
                UIValidationRuleGroupPrecard ruleGroupPrecardAlias = null;
                UIValidationRuleGroup ruleGroupAlias = null;
                Precard PrecardAlias = null;
                IList<UIValidationRulePrecardProxy> RulePrecardProxyList = new List<UIValidationRulePrecardProxy>();
                IList<UIValidationRulePrecardProxy> rulePrecardProxyList = new List<UIValidationRulePrecardProxy>();
                UIValidationRulePrecardProxy rulePrecardProxy = null;
                UIValidationRule ruleAlias = null;
                int counter = 0;
                if (ruleType == (int)UIValidationRuleType.Rule)
                {
                    IList<UIValidationRuleGroupPrecard> ruleGroupPrecardList = NHSession.QueryOver(() => ruleGroupPrecardAlias)
                                                                                  .JoinAlias(() => ruleGroupPrecardAlias.UIValidationRuleGroup, () => ruleGroupAlias)
                                                                                  .JoinAlias(() => ruleGroupAlias.ValidationRule, () => ruleAlias)
                                                                                  .Where(() => ruleGroupAlias.ValidationRule.ID == ruleId &&
                                                                                               ruleGroupAlias.ValidationGroup.ID == groupId &&
                                                                                               ruleGroupPrecardAlias.Precard == null
                                                                                         )
                                                                                  .List<UIValidationRuleGroupPrecard>();
                    if (ruleGroupPrecardList.Count != 0)
                    {
                        foreach (UIValidationRuleGroupPrecard ruleGroupPrecard in ruleGroupPrecardList)
                        {
                            rulePrecardProxy = new UIValidationRulePrecardProxy();
                            rulePrecardProxy.ID = ruleGroupPrecard.ID;
                            rulePrecardProxy.PrecardID = 0;
                            rulePrecardProxy.PrecardName = "";
                            rulePrecardProxy.Active = ruleGroupPrecard.Active;
                            rulePrecardProxy.ExistPrecard = (int)UIValidationExistance.Exist;
                            rulePrecardProxy.Operator = ruleGroupPrecard.Operator;
                            rulePrecardProxy.PrecardColor = this.GetRandomColor(counter++);                          
                            RulePrecardProxyList.Add(rulePrecardProxy);
                        }
                    }
                }
                else
                {
                    IList<UIValidationRuleGroupPrecard> ruleGroupPrecardList = NHSession.QueryOver(() => ruleGroupPrecardAlias)
                                                                                  .JoinAlias(() => ruleGroupPrecardAlias.UIValidationRuleGroup, () => ruleGroupAlias)
                                                                                  .JoinAlias(() => ruleGroupPrecardAlias.Precard, () => PrecardAlias)
                                                                                  .JoinAlias(() => ruleGroupAlias.ValidationRule, () => ruleAlias)
                                                                                  .Where(() => ruleGroupAlias.ValidationRule.ID == ruleId &&
                                                                                               ruleGroupAlias.ValidationGroup.ID == groupId &&
                                                                                               PrecardAlias.Active &&
                                                                                      //  ruleAlias.RuleConcept == RuleGroupType &&
                                                                                              ( PrecardAlias.Name.IsInsensitiveLike(searchValue, MatchMode.Anywhere) ||
                                                                                                PrecardAlias.Code.IsInsensitiveLike(searchValue, MatchMode.Anywhere)
                                                                                              )
                                                                                         )
                                                                                  .List<UIValidationRuleGroupPrecard>();
                    if (ruleGroupPrecardList.Count != 0)
                    {
                        foreach (UIValidationRuleGroupPrecard ruleGroupPrecard in ruleGroupPrecardList)
                        {
                            rulePrecardProxy = new UIValidationRulePrecardProxy();
                            rulePrecardProxy.ID = ruleGroupPrecard.ID;
                            rulePrecardProxy.PrecardID = ruleGroupPrecard.Precard.ID;
                            rulePrecardProxy.PrecardName = ruleGroupPrecard.Precard.Name;
                            rulePrecardProxy.Active = ruleGroupPrecard.Active;
                            rulePrecardProxy.ExistPrecard = (int)UIValidationExistance.Exist;
                            rulePrecardProxy.Operator = ruleGroupPrecard.Operator;
                            rulePrecardProxy.PrecardColor = this.GetRandomColor(counter++);                            
                            RulePrecardProxyList.Add(rulePrecardProxy);
                        }
                    }

                    IList<Precard> precardList = this.GetPrecards(RuleGroupType, searchValue, ruleId);
                    precardList = precardList.Where(x => !ruleGroupPrecardList.Select(y => y.Precard.ID).ToList<decimal>().Contains(x.ID)).ToList<Precard>();
                    foreach (Precard precard in precardList)
                    {
                        rulePrecardProxy = new UIValidationRulePrecardProxy();
                        rulePrecardProxyList = RulePrecardProxyList.Where(x => x.ID == precard.ID).ToList();
                        if (rulePrecardProxyList.Count != 0)
                        {
                            List<decimal> RulePrecardProxyIdList = RulePrecardProxyList.Select(x => x.ID).ToList<decimal>();
                            decimal RulePrecardProxyId = RulePrecardProxyIdList.Max();
                            rulePrecardProxy.ID = RulePrecardProxyId + 1;
                        }
                        else
                        {
                            rulePrecardProxy.ID = precard.ID;
                        }
                        rulePrecardProxy.PrecardID = precard.ID;
                        rulePrecardProxy.PrecardName = precard.Name;
                        rulePrecardProxy.Active = false;
                        rulePrecardProxy.Operator = false;
                        rulePrecardProxy.ExistPrecard = (int)UIValidationExistance.NotExist;
                        rulePrecardProxy.PrecardColor = this.GetRandomColor(counter++);                       
                        RulePrecardProxyList.Add(rulePrecardProxy);
                    }
                }

                return RulePrecardProxyList;
            }
            catch (Exception ex)
            {
                LogException(ex, "BUIValidationGroup", "GetRulePrecards");
                throw ex;
            }
        }
        public IList<UIValidationRuleParameterProxy> GetRuleParameters(decimal ruleId, decimal groupId, UIValidationRulePrecardProxy rulePrecardProxy)
        {
            try
            {
                IList<UIValidationRuleParameterProxy> ruleParameterProxyList = new List<UIValidationRuleParameterProxy>();
                UIValidationRuleParam ruleParamAlias = null;
                UIValidationRuleGroup ruleGroupAlias = null;
                UIValidationRuleGroupPrecard ruleGroupPrecardAlias = null;
                UIValidationRuleTempParameter ruleTempParameterAlias = null;
                UIValidationRule ruleAlias = null;
                if (rulePrecardProxy != null)
                {

                    IList<UIValidationRuleParam> RuleParamList = NHSession.QueryOver(() => ruleParamAlias)
                                                                          .JoinAlias(() => ruleParamAlias.UIValidationPrecard, () => ruleGroupPrecardAlias)
                                                                          .JoinAlias(() => ruleGroupPrecardAlias.UIValidationRuleGroup, () => ruleGroupAlias)
                                                                          .Where(() => ruleGroupAlias.ValidationRule.ID == ruleId &&
                                                                                       ruleGroupAlias.ValidationGroup.ID == groupId &&
                                                                                       ruleGroupPrecardAlias.Precard.ID == rulePrecardProxy.PrecardID
                                                                                )
                                                                          .List<UIValidationRuleParam>();
                    if (RuleParamList.Count != 0)
                    {
                        foreach (UIValidationRuleParam ruleParam in RuleParamList)
                        {
                            UIValidationRuleParameterProxy ruleParameterProxy = new UIValidationRuleParameterProxy();
                            ruleParameterProxy.ID = ruleParam.ID.ToString();
                            ruleParameterProxy.PrecardID = ruleParam.UIValidationPrecard.Precard.ID;
                            ruleParameterProxy.ParamID = ruleParam.UIValidationRuleTempParam.ID;
                            ruleParameterProxy.PrameterName = ruleParam.UIValidationRuleTempParam.Name;
                            if (ruleParam.UIValidationRuleTempParam.Type == RuleParamType.Date && ruleParam.Value != string.Empty)
                            {
                                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                                    ruleParameterProxy.ParameterValue = Utility.ToPersianDate(ruleParam.Value);
                                else
                                    ruleParameterProxy.ParameterValue = ruleParam.Value;
                            }
                            else
                            {
                                ruleParameterProxy.ParameterValue = ruleParam.Value;
                            }
                            ruleParameterProxy.KeyName = ruleParam.UIValidationRuleTempParam.KeyName;
                            ruleParameterProxy.ExistParam = (int)UIValidationExistance.Exist;
                            ruleParameterProxy.ParamType = (int)ruleParam.UIValidationRuleTempParam.Type;
                            ruleParameterProxy.ContinueOnTomorrow = ruleParam.ContinueOnTomorrow;
                            ruleParameterProxy.ParameterColor = rulePrecardProxy.PrecardColor;                           
                            ruleParameterProxyList.Add(ruleParameterProxy);
                        }
                    }
                    //else
                    //{

                    IList<UIValidationRuleTempParameter> ruleTempParameterList = NHSession.QueryOver(() => ruleTempParameterAlias)
                                                                                          .JoinAlias(() => ruleTempParameterAlias.UIValidationRule, () => ruleAlias)
                                                                                          .Where(() => ruleTempParameterAlias.UIValidationRule.ID == ruleId)
                                                                                          .List<UIValidationRuleTempParameter>();
                    if (ruleTempParameterList.Count != 0)
                    {
                        ruleTempParameterList = ruleTempParameterList.Where(x => !RuleParamList.Select(y => y.UIValidationRuleTempParam.ID).ToList<decimal>().Contains(x.ID)).ToList<UIValidationRuleTempParameter>();
                        foreach (UIValidationRuleTempParameter ruleTempParameter in ruleTempParameterList)
                        {
                            UIValidationRuleParameterProxy ruleParameterProxy = new UIValidationRuleParameterProxy();
                            ruleParameterProxy.ID = Guid.NewGuid().ToString();
                            ruleParameterProxy.ParamID = ruleTempParameter.ID;
                            ruleParameterProxy.PrecardID = rulePrecardProxy.PrecardID;
                            ruleParameterProxy.PrameterName = ruleTempParameter.Name;
                            ruleParameterProxy.ParameterValue = string.Empty;
                            ruleParameterProxy.KeyName = ruleTempParameter.KeyName;
                            ruleParameterProxy.ExistParam = (int)UIValidationExistance.NotExist;
                            ruleParameterProxy.ParamType = (int)ruleTempParameter.Type;
                            ruleParameterProxy.ContinueOnTomorrow = false;
                            ruleParameterProxy.ParameterColor = rulePrecardProxy.PrecardColor;                            
                            ruleParameterProxyList.Add(ruleParameterProxy);
                        }
                    }
                    //}
                }
                else
                {
                    IList<UIValidationRuleParam> RuleParamList = NHSession.QueryOver(() => ruleParamAlias)
                                                                          .JoinAlias(() => ruleParamAlias.UIValidationPrecard, () => ruleGroupPrecardAlias)
                                                                          .JoinAlias(() => ruleGroupPrecardAlias.UIValidationRuleGroup, () => ruleGroupAlias)
                                                                          .Where(() => ruleGroupAlias.ValidationRule.ID == ruleId &&
                                                                                       ruleGroupAlias.ValidationGroup.ID == groupId &&
                                                                                       ruleGroupPrecardAlias.Precard == null
                                                                                )
                                                                          .List<UIValidationRuleParam>();
                    if (RuleParamList.Count != 0)
                    {
                        foreach (UIValidationRuleParam ruleParam in RuleParamList)
                        {
                            UIValidationRuleParameterProxy ruleParameterProxy = new UIValidationRuleParameterProxy();
                            ruleParameterProxy.ID = ruleParam.ID.ToString();
                            ruleParameterProxy.ParamID = ruleParam.UIValidationRuleTempParam.ID;
                            ruleParameterProxy.PrameterName = ruleParam.UIValidationRuleTempParam.Name;
                            if (ruleParam.UIValidationRuleTempParam.Type == RuleParamType.Date && ruleParam.Value != string.Empty)
                            {
                                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                                    ruleParameterProxy.ParameterValue = Utility.ToPersianDate(ruleParam.Value);
                                else
                                    ruleParameterProxy.ParameterValue = ruleParam.Value;
                            }
                            else
                            {
                                ruleParameterProxy.ParameterValue = ruleParam.Value;
                            }
                            ruleParameterProxy.KeyName = ruleParam.UIValidationRuleTempParam.KeyName;
                            ruleParameterProxy.ExistParam = (int)UIValidationExistance.Exist;
                            ruleParameterProxy.ParamType = (int)ruleParam.UIValidationRuleTempParam.Type;
                            ruleParameterProxy.ContinueOnTomorrow = ruleParam.ContinueOnTomorrow;
                            ruleParameterProxy.RuleGroupPrecard = new UIValidationRuleGroupPrecard() { Operator = ruleParam.UIValidationPrecard.Operator };
                            ruleParameterProxyList.Add(ruleParameterProxy);
                        }
                    }

                    //else
                    //{
                    IList<UIValidationRuleTempParameter> ruleTempParameterList = NHSession.QueryOver(() => ruleTempParameterAlias)
                                                                                    .Where(() => ruleTempParameterAlias.UIValidationRule.ID == ruleId)
                                                                                    .JoinAlias(() => ruleTempParameterAlias.UIValidationRule, () => ruleAlias)
                                                                                    .Where(() => ruleAlias.Type == (UIValidationRuleType)UIValidationRuleType.RuleParameter)
                                                                                    .List<UIValidationRuleTempParameter>();
                    if (ruleTempParameterList.Count != 0)
                    {
                        ruleTempParameterList = ruleTempParameterList.Where(x => !RuleParamList.Select(y => y.UIValidationRuleTempParam.ID).ToList<decimal>().Contains(x.ID)).ToList<UIValidationRuleTempParameter>();
                        foreach (UIValidationRuleTempParameter ruleTempParameter in ruleTempParameterList)
                        {
                            UIValidationRuleParameterProxy ruleParameterProxy = new UIValidationRuleParameterProxy();
                            ruleParameterProxy.ID = Guid.NewGuid().ToString();
                            ruleParameterProxy.ParamID = ruleTempParameter.ID;
                            ruleParameterProxy.PrameterName = ruleTempParameter.Name;
                            ruleParameterProxy.ParameterValue = string.Empty;
                            ruleParameterProxy.KeyName = ruleTempParameter.KeyName;
                            ruleParameterProxy.ExistParam = (int)UIValidationExistance.NotExist;
                            ruleParameterProxy.ParamType = (int)ruleTempParameter.Type;
                            ruleParameterProxy.ContinueOnTomorrow = false;
                            ruleParameterProxy.RuleGroupPrecard = new UIValidationRuleGroupPrecard() { Operator = false };
                            ruleParameterProxyList.Add(ruleParameterProxy);
                        }
                    }
                    //}

                }

                return ruleParameterProxyList;
            }

            catch (Exception ex)
            {
                LogException(ex, "BUIValidationGroup", "GetRuleParameters");
                throw ex;
            }

        }
        /// <summary>
        /// مقداردهی پیش فرض به پارامترهای قوانین محاسباتی 
        /// در هنگامی که آن قانون فعال شود
        /// </summary>
        /// <param name="rulesGroupProxyList"></param>
        /// <param name="ruleList"></param>
        public void InsertDefaultValueToCalculationRule(IList<UiValidationRuleGroupProxy> rulesGroupProxyList, IList<UIValidationRule> ruleList)
        {
            using (NHibernateSessionManager.Instance.BeginTransactionOn())
            {
                try
                {
                    int CurrentMonth = 1;
                    int DefaultValue = 0;
                    string ParamValue = string.Empty;
                    UIValidationRuleGroupPrecard ruleGroupPrecardAlias = null;
                    UIValidationRuleGroup ruleGroupAlias = null;
                    decimal GroupId = rulesGroupProxyList.Select(x => x.GroupID).FirstOrDefault();
                    foreach (UIValidationRule rule in ruleList)
                    {
                        UIValidationRuleGroupPrecard ruleGroupPrecard = NHSession.QueryOver(() => ruleGroupPrecardAlias)
                                                                                 .JoinAlias(() => ruleGroupPrecardAlias.UIValidationRuleGroup, () => ruleGroupAlias)
                                                                                 .Where(() => ruleGroupAlias.ValidationGroup.ID == GroupId &&
                                                                                              ruleGroupAlias.ValidationRule.ID == rule.ID &&
                                                                                              ruleGroupPrecardAlias.Precard == null
                                                                                       ).SingleOrDefault();
                        IList<UIValidationRuleTempParameter> ruleTempParameterList = NHSession.QueryOver<UIValidationRuleTempParameter>()
                                                                                          .Where(x => x.UIValidationRule.ID == rule.ID)
                                                                                          .List();
                        if (ruleGroupPrecard == null)
                        {
                            UIValidationRuleGroup ruleGroup = NHSession.QueryOver<UIValidationRuleGroup>()
                                                                                  .Where(x => x.ValidationRule.ID == rule.ID &&
                                                                                               x.ValidationGroup.ID == GroupId
                                                                                         ).SingleOrDefault();
                            UIValidationRuleGroupPrecard RuleGroupPrecard = new UIValidationRuleGroupPrecard()
                            {
                                UIValidationRuleGroup = ruleGroup,
                                Precard = null,
                                Active = true,
                                Operator = false
                            };
                            rulePrecardRep.WithoutTransactSave(RuleGroupPrecard);
                            foreach (UIValidationRuleTempParameter ruleTempParameter in ruleTempParameterList)
                            {
                                UIValidationRuleParam ruleParam = null;
                                switch (Utility.ToInteger(rule.CustomCode))
                                {
                                    case (int)UIValidationCustomCode.R1:
                                        if (ruleTempParameter.KeyName == "LockCalculationFromMonth")
                                        {
                                            switch (BLanguage.CurrentSystemLanguage)
                                            {
                                                case LanguagesName.English:
                                                    ruleParam = new UIValidationRuleParam()
                                                    {
                                                        UIValidationPrecard = RuleGroupPrecard,
                                                        UIValidationRuleTempParam = ruleTempParameter,
                                                        Value = DateTime.Now.Day.ToString(),
                                                        ContinueOnTomorrow = false
                                                    };
                                                    break;
                                                case LanguagesName.Parsi:
                                                    ruleParam = new UIValidationRuleParam()
                                                    {
                                                        UIValidationPrecard = RuleGroupPrecard,
                                                        UIValidationRuleTempParam = ruleTempParameter,
                                                        Value = Utility.ToPersianDateTime(DateTime.Now).Day.ToString(),
                                                        ContinueOnTomorrow = false
                                                    };
                                                    break;
                                            }
                                        }
                                        if (ruleTempParameter.KeyName == "LockCalculationFromCurrentMonth")
                                        {
                                            ruleParam = new UIValidationRuleParam()
                                            {
                                                UIValidationPrecard = RuleGroupPrecard,
                                                UIValidationRuleTempParam = ruleTempParameter,
                                                Value = CurrentMonth.ToString(),
                                                ContinueOnTomorrow = false
                                            };
                                        }
                                        if (ruleTempParameter.KeyName == "NumberOfDaysAfterLockDate")
                                        {
                                            ruleParam = new UIValidationRuleParam()
                                            {
                                                UIValidationPrecard = RuleGroupPrecard,
                                                UIValidationRuleTempParam = ruleTempParameter,
                                                Value = DefaultValue.ToString(),
                                                ContinueOnTomorrow = false
                                            };
                                        }
                                        ruleParamRep.WithoutTransactSave(ruleParam);
                                        break;
                                    case (int)UIValidationCustomCode.R3:
                                        if (ruleTempParameter.KeyName == "LockCalculationFromDate")
                                        {
                                            ruleParam = new UIValidationRuleParam()
                                            {
                                                UIValidationPrecard = RuleGroupPrecard,
                                                UIValidationRuleTempParam = ruleTempParameter,
                                                Value = DateTime.Now.ToString("yyyy/MM/dd"),
                                                ContinueOnTomorrow = false
                                            };
                                            ruleParamRep.WithoutTransactSave(ruleParam);
                                        }
                                        if (ruleTempParameter.KeyName == "NumberOfDaysAfterLockDate")
                                        {
                                            ruleParam = new UIValidationRuleParam()
                                            {
                                                UIValidationPrecard = RuleGroupPrecard,
                                                UIValidationRuleTempParam = ruleTempParameter,
                                                Value = DefaultValue.ToString(),
                                                ContinueOnTomorrow = false
                                            };
                                            ruleParamRep.WithoutTransactSave(ruleParam);
                                        }
                                        break;
                                    case (int)UIValidationCustomCode.R30:
                                        if (ruleTempParameter.KeyName == "LockCalculationtMonth")
                                        {
                                            switch (BLanguage.CurrentSystemLanguage)
                                            {
                                                case LanguagesName.English:
                                                    ruleParam = new UIValidationRuleParam()
                                                    {
                                                        UIValidationPrecard = RuleGroupPrecard,
                                                        UIValidationRuleTempParam = ruleTempParameter,
                                                        Value = DateTime.Now.Month.ToString(),
                                                        ContinueOnTomorrow = false
                                                    };
                                                    break;
                                                case LanguagesName.Parsi:
                                                    ruleParam = new UIValidationRuleParam()
                                                    {
                                                        UIValidationPrecard = RuleGroupPrecard,
                                                        UIValidationRuleTempParam = ruleTempParameter,
                                                        Value = Utility.ToPersianDateTime(DateTime.Now).Month.ToString(),
                                                        ContinueOnTomorrow = false
                                                    };
                                                    break;
                                            }
                                        }
                                        if (ruleTempParameter.KeyName == "LockCalculationDay")
                                        {
                                            switch (BLanguage.CurrentSystemLanguage)
                                            {
                                                case LanguagesName.English:
                                                    ruleParam = new UIValidationRuleParam()
                                                    {
                                                        UIValidationPrecard = RuleGroupPrecard,
                                                        UIValidationRuleTempParam = ruleTempParameter,
                                                        Value = DateTime.Now.Day.ToString(),
                                                        ContinueOnTomorrow = false
                                                    };
                                                    break;
                                                case LanguagesName.Parsi:
                                                    ruleParam = new UIValidationRuleParam()
                                                    {
                                                        UIValidationPrecard = RuleGroupPrecard,
                                                        UIValidationRuleTempParam = ruleTempParameter,
                                                        Value = Utility.ToPersianDateTime(DateTime.Now).Day.ToString(),
                                                        ContinueOnTomorrow = false
                                                    };
                                                    break;
                                            }
                                        }
                                        ruleParamRep.WithoutTransactSave(ruleParam);
                                        break;
                                }
                            }
                        }
                        else
                        {
                            foreach (UIValidationRuleTempParameter ruleTempParameter in ruleTempParameterList)
                            {
                                UIValidationRuleParam ruleParam = NHSession.QueryOver<UIValidationRuleParam>()
                                                                       .Where(x => x.UIValidationPrecard == ruleGroupPrecard &&
                                                                                   x.UIValidationRuleTempParam == ruleTempParameter
                                                                             ).SingleOrDefault();
                                if (ruleParam != null && ruleParam.Value == string.Empty)
                                {
                                    switch (Utility.ToInteger(rule.CustomCode))
                                    {
                                        case (int)UIValidationCustomCode.R1:
                                            if (ruleTempParameter.KeyName == "LockCalculationFromMonth")
                                            {
                                                switch (BLanguage.CurrentSystemLanguage)
                                                {
                                                    case LanguagesName.English:
                                                        ruleParam.Value = DateTime.Now.Day.ToString();
                                                        break;
                                                    case LanguagesName.Parsi:
                                                        ruleParam.Value = Utility.ToPersianDateTime(DateTime.Now).Day.ToString();
                                                        break;
                                                }
                                            }
                                            if (ruleTempParameter.KeyName == "LockCalculationFromCurrentMonth")
                                            {
                                                ruleParam.Value = CurrentMonth.ToString();
                                            }
                                            if (ruleTempParameter.KeyName == "NumberOfDaysAfterLockDate")
                                            {
                                                ruleParam.Value = DefaultValue.ToString();
                                            }
                                            ruleParamRep.WithoutTransactUpdate(ruleParam);
                                            break;
                                        case (int)UIValidationCustomCode.R3:
                                            if (ruleTempParameter.KeyName == "LockCalculationFromDate")
                                            {
                                                ruleParam.Value = DateTime.Now.ToString("yyyy/MM/dd");
                                            }
                                            if (ruleTempParameter.KeyName == "NumberOfDaysAfterLockDate")
                                            {
                                                ruleParam.Value = DefaultValue.ToString();
                                            }
                                            ruleParamRep.WithoutTransactUpdate(ruleParam);
                                            break;
                                        case (int)UIValidationCustomCode.R30:
                                            if (ruleTempParameter.KeyName == "LockCalculationtMonth")
                                            {
                                                switch (BLanguage.CurrentSystemLanguage)
                                                {
                                                    case LanguagesName.English:
                                                        ruleParam.Value = DateTime.Now.Month.ToString();
                                                        break;
                                                    case LanguagesName.Parsi:
                                                        ruleParam.Value = Utility.ToPersianDateTime(DateTime.Now).Month.ToString();
                                                        break;
                                                }
                                            }
                                            if (ruleTempParameter.KeyName == "LockCalculationDay")
                                            {
                                                switch (BLanguage.CurrentSystemLanguage)
                                                {
                                                    case LanguagesName.English:
                                                        ruleParam.Value = DateTime.Now.Day.ToString();
                                                        break;
                                                    case LanguagesName.Parsi:
                                                        ruleParam.Value = Utility.ToPersianDateTime(DateTime.Now).Day.ToString();
                                                        break;
                                                }
                                            }
                                            ruleParamRep.WithoutTransactUpdate(ruleParam);
                                            break;
                                    }
                                }
                            }
                        }
                    }
                    NHibernateSessionManager.Instance.CommitTransactionOn();
                }
                catch (Exception ex)
                {
                    NHibernateSessionManager.Instance.RollbackTransactionOn();
                    LogException(ex, "", "");
                    throw ex;
                }
            }

        }
        public void UpdateRulesList(IList<UiValidationRuleGroupProxy> rulesGroupProxyList)
        {
            using (NHibernateSessionManager.Instance.BeginTransactionOn())
            {
                try
                {
                    UIValidationRule ruleAlias = null;
                    UIValidationGroup groupAlias = null;
                    UIValidationRuleGroup ruleGroupAlias = null;
                    string TagValue = string.Empty;

                    foreach (UiValidationRuleGroupProxy rulegroup in rulesGroupProxyList)
                    {
                        if (rulegroup.ExistRuleTag)
                        {
                            TagValue = NHSession.QueryOver(() => ruleGroupAlias)
                                                     .JoinAlias(() => ruleGroupAlias.ValidationGroup, () => groupAlias)
                                                     .JoinAlias(() => ruleGroupAlias.ValidationRule, () => ruleAlias)
                                                     .Where(() => groupAlias.ID == rulegroup.GroupID && ruleAlias.ID == rulegroup.RuleID)
                                                     .Select(x => x.Tag).List<string>().SingleOrDefault();
                        }
                        else
                            TagValue = null;
                        UIValidationRuleGroup RuleGroup = new UIValidationRuleGroup() { ID = rulegroup.ID, Active = rulegroup.Active, ValidationGroup = new UIValidationGroup() { ID = rulegroup.GroupID }, ValidationRule = new UIValidationRule() { ID = rulegroup.RuleID }, Warning = rulegroup.Warning, Tag = TagValue };

                        RuleGroupRep.WithoutTransactUpdate(RuleGroup);
                    }
                    rulesGroupProxyList = rulesGroupProxyList.Where(y => y.Active).ToList();
                    if (rulesGroupProxyList.Count != 0)
                    {
                        IList<UIValidationRule> ruleList = NHSession.QueryOver(() => ruleAlias)
                                                               .Where(x => x.SubsystemID == (int)SubSystemIdentifier.TimeAtendance &&
                                                                            x.ID.IsIn(rulesGroupProxyList.Select(y => y.RuleID).ToArray())
                                                                            && (x.CustomCode == ((int)UIValidationCustomCode.R1).ToString() ||
                                                                                x.CustomCode == ((int)UIValidationCustomCode.R3).ToString() ||
                                                                                x.CustomCode == ((int)UIValidationCustomCode.R30).ToString())
                                                                     )
                                                               .List();
                        if (ruleList.Count != 0)
                        {
                            this.InsertDefaultValueToCalculationRule(rulesGroupProxyList, ruleList);
                        }
                    }

                    NHibernateSessionManager.Instance.CommitTransactionOn();
                }

                catch (Exception ex)
                {
                    NHibernateSessionManager.Instance.RollbackTransactionOn();
                    LogException(ex, "BUIValidationGroup", "UpdateRuleList");
                    throw ex;
                }
            }
        }
        /// <summary>
        /// ذخیره کردن مقادیر در پایگاه
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="ruleId"></param>
        /// <param name="ruleGroupId"></param>
        /// <param name="ruleType"></param>
        /// <param name="rulePrecardParamProxyList"></param>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void UpdateRulePrecardsParams(decimal groupId, decimal ruleId, decimal ruleGroupId, int ruleType, IList<UiValidationRulePrecardParamProxy> rulePrecardParamProxyList, string ruleDetails, int customCode, bool ruleGroupActive, bool ruleGroupWarning)
        {
            using (NHibernateSessionManager.Instance.BeginTransactionOn())
            {
                try
                {
                    IList<Precard> precardsList = NHSession.QueryOver<Precard>()
                                                           .Where(x => x.Active)
                                                           .List<Precard>();
                    UIValidationRuleTempParameter ruleTempParamAlias = null;
                    IList<UIValidationRuleTempParameter> ruleTempParamList = NHSession.QueryOver(() => ruleTempParamAlias)
                                                                                  .Where(() => ruleTempParamAlias.UIValidationRule.ID == ruleId)
                                                                                  .List<UIValidationRuleTempParameter>();
                    this.Validate(ruleId, ruleGroupId, precardsList, ruleType, ruleTempParamList, rulePrecardParamProxyList);

                    switch (ruleType)
                    {
                        case (int)UIValidationRuleType.RulePrecardParameter:
                            foreach (UiValidationRulePrecardParamProxy rulePrecardParamProxy in rulePrecardParamProxyList)
                            {
                                UIValidationRuleGroupPrecard ruleGroupPrecard = null;
                                if (rulePrecardParamProxy.ExistPrecard == 1)
                                {
                                    ruleGroupPrecard = new UIValidationRuleGroupPrecard() { ID = rulePrecardParamProxy.ID, Active = rulePrecardParamProxy.Active, Operator = rulePrecardParamProxy.Operator };
                                    rulePrecardRep.WithoutTransactUpdate(ruleGroupPrecard);

                                    foreach (UiValidationRuleParamProxy ruleParamProxy in rulePrecardParamProxy.ObjRuleParams)
                                    {
                                        if (ruleParamProxy.ExistParam == 1)
                                        {
                                            string ParamValue = this.ConvertParamValue(ruleParamProxy);
                                            UIValidationRuleParam ruleParam = new UIValidationRuleParam() { ID = decimal.Parse(ruleParamProxy.ID), Value = ParamValue, ContinueOnTomorrow = ruleParamProxy.ContinueOnTomorrow };
                                            ruleParamRep.WithoutTransactUpdate(ruleParam);
                                        }
                                        else
                                        {
                                            string ParamValue = this.ConvertParamValue(ruleParamProxy);
                                            UIValidationRuleParam ruleParam = new UIValidationRuleParam()
                                            {
                                                UIValidationPrecard = ruleGroupPrecard,
                                                UIValidationRuleTempParam = new UIValidationRuleTempParameter() { ID = ruleParamProxy.ParamID, UIValidationRule = new UIValidationRule() { ID = ruleId } },
                                                Value = ParamValue,
                                                ContinueOnTomorrow = ruleParamProxy.ContinueOnTomorrow
                                            };
                                            ruleParamRep.WithoutTransactSave(ruleParam);
                                        }
                                    }
                                }

                                else
                                {
                                    ruleGroupPrecard = new UIValidationRuleGroupPrecard()
                                    {
                                        UIValidationRuleGroup = new UIValidationRuleGroup() { ID = ruleGroupId },
                                        Precard = new Precard() { ID = rulePrecardParamProxy.PrecardID },
                                        Active = rulePrecardParamProxy.Active,
                                        Operator = rulePrecardParamProxy.Operator
                                    };
                                    rulePrecardRep.WithoutTransactSave(ruleGroupPrecard);
                                    foreach (UiValidationRuleParamProxy ruleParamProxy in rulePrecardParamProxy.ObjRuleParams)
                                    {
                                        string ParamValue = this.ConvertParamValue(ruleParamProxy);
                                        UIValidationRuleParam ruleParam = new UIValidationRuleParam()
                                        {
                                            UIValidationPrecard = ruleGroupPrecard,
                                            UIValidationRuleTempParam = new UIValidationRuleTempParameter() { ID = ruleParamProxy.ParamID, UIValidationRule = new UIValidationRule() { ID = ruleId } },
                                            Value = ParamValue,
                                            ContinueOnTomorrow = ruleParamProxy.ContinueOnTomorrow
                                        };
                                        ruleParamRep.WithoutTransactSave(ruleParam);
                                    }
                                }
                            }
                            break;
                        case (int)UIValidationRuleType.RulePrecard:
                            foreach (UiValidationRulePrecardParamProxy rulePrecardParamProxy in rulePrecardParamProxyList)
                            {
                                if (rulePrecardParamProxy.ExistPrecard == 1)
                                {
                                    UIValidationRuleGroupPrecard ruleGroupPrecard = new UIValidationRuleGroupPrecard() { ID = rulePrecardParamProxy.ID, Active = rulePrecardParamProxy.Active, Operator = rulePrecardParamProxy.Operator };
                                    rulePrecardRep.WithoutTransactUpdate(ruleGroupPrecard);
                                }
                                else
                                {
                                    UIValidationRuleGroupPrecard ruleGroupPrecard = new UIValidationRuleGroupPrecard()
                                    {
                                        UIValidationRuleGroup = new UIValidationRuleGroup() { ID = ruleGroupId },
                                        Active = rulePrecardParamProxy.Active,
                                        Precard = new Precard() { ID = rulePrecardParamProxy.PrecardID },
                                        Operator = rulePrecardParamProxy.Operator
                                    };
                                    rulePrecardRep.WithoutTransactSave(ruleGroupPrecard);
                                }
                            }
                            break;
                        case (int)UIValidationRuleType.Rule:
                            foreach (UiValidationRulePrecardParamProxy rulePrecardParamProxy in rulePrecardParamProxyList)
                            {
                                if (rulePrecardParamProxy.ExistPrecard == 1)
                                {
                                    UIValidationRuleGroupPrecard ruleGroupPrecard = new UIValidationRuleGroupPrecard() { ID = rulePrecardParamProxy.ID, Operator = rulePrecardParamProxy.Operator, Active = rulePrecardParamProxy .Active};
                                    rulePrecardRep.WithoutTransactUpdate(ruleGroupPrecard);
                                }
                                else
                                {
                                    UIValidationRuleGroupPrecard ruleGroupPrecard = new UIValidationRuleGroupPrecard()
                                    {
                                        UIValidationRuleGroup = new UIValidationRuleGroup() { ID = ruleGroupId },
                                        Active = rulePrecardParamProxy.Active,
                                        Precard = null,
                                        Operator = rulePrecardParamProxy.Operator
                                    };
                                    rulePrecardRep.WithoutTransactSave(ruleGroupPrecard);
                                }

                            }
                            break;

                        case (int)UIValidationRuleType.RuleParameter:
                            foreach (UiValidationRulePrecardParamProxy rulePrecardParamProxy in rulePrecardParamProxyList)
                            {
                                if (rulePrecardParamProxy.ObjRuleParams != null)
                                {
                                    decimal ExistParam = rulePrecardParamProxy.ObjRuleParams.Select(x => x.ExistParam).ToList<decimal>().FirstOrDefault();
                                    UIValidationRuleGroupPrecard ruleGroupPrecard = NHSession.QueryOver<UIValidationRuleGroupPrecard>()
                                                                                  .Where(x => x.UIValidationRuleGroup.ID == ruleGroupId && x.Precard == null)
                                                                                  .List<UIValidationRuleGroupPrecard>().SingleOrDefault();
                                    if (ExistParam == 1)
                                    {
                                        this.NHSession.Evict(ruleGroupPrecard);
                                        ruleGroupPrecard = new UIValidationRuleGroupPrecard() { ID = ruleGroupPrecard.ID, Operator = rulePrecardParamProxy.Operator, Active = ruleGroupPrecard.Active };
                                        rulePrecardRep.WithoutTransactUpdate(ruleGroupPrecard);
                                        foreach (UiValidationRuleParamProxy ruleParamProxy in rulePrecardParamProxy.ObjRuleParams)
                                        {
                                            if (ruleParamProxy.ExistParam == 1)
                                            {
                                                string ParamValue = this.ConvertParamValue(ruleParamProxy);
                                                UIValidationRuleParam ruleParam = new UIValidationRuleParam() { ID = decimal.Parse(ruleParamProxy.ID), Value = ParamValue, ContinueOnTomorrow = ruleParamProxy.ContinueOnTomorrow };
                                                ruleParamRep.WithoutTransactUpdate(ruleParam);
                                            }
                                            else
                                            {
                                                string ParamValue = this.ConvertParamValue(ruleParamProxy);
                                                UIValidationRuleParam ruleParam = new UIValidationRuleParam()
                                                {
                                                    UIValidationPrecard = ruleGroupPrecard,
                                                    UIValidationRuleTempParam = new UIValidationRuleTempParameter() { ID = ruleParamProxy.ParamID, UIValidationRule = new UIValidationRule() { ID = ruleId } },
                                                    Value = ParamValue,
                                                    ContinueOnTomorrow = ruleParamProxy.ContinueOnTomorrow
                                                };
                                                ruleParamRep.WithoutTransactSave(ruleParam);
                                            }

                                        }
                                    }
                                    else
                                    {
                                        if (ruleGroupPrecard == null)
                                        {
                                            ruleGroupPrecard = new UIValidationRuleGroupPrecard()
                                           {
                                               UIValidationRuleGroup = new UIValidationRuleGroup() { ID = ruleGroupId },
                                               Precard = new Precard() { ID = rulePrecardParamProxy.PrecardID },
                                               Active = rulePrecardParamProxy.Active,
                                               Operator = rulePrecardParamProxy.Operator
                                           };
                                            rulePrecardRep.WithoutTransactSave(ruleGroupPrecard);

                                        }
                                        foreach (UiValidationRuleParamProxy ruleParamProxy in rulePrecardParamProxy.ObjRuleParams)
                                        {
                                            if (ruleParamProxy.ExistParam == 1)
                                            {
                                                string ParamValue = this.ConvertParamValue(ruleParamProxy);
                                                UIValidationRuleParam ruleParam = new UIValidationRuleParam() { ID = decimal.Parse(ruleParamProxy.ID), Value = ParamValue, ContinueOnTomorrow = ruleParamProxy.ContinueOnTomorrow };
                                                ruleParamRep.WithoutTransactUpdate(ruleParam);
                                            }
                                            else
                                            {
                                                string ParamValue = this.ConvertParamValue(ruleParamProxy);
                                                UIValidationRuleParam ruleParam = new UIValidationRuleParam()
                                                {
                                                    UIValidationPrecard = ruleGroupPrecard,
                                                    UIValidationRuleTempParam = new UIValidationRuleTempParameter() { ID = ruleParamProxy.ParamID, UIValidationRule = new UIValidationRule() { ID = ruleId } },
                                                    Value = ParamValue,
                                                    ContinueOnTomorrow = ruleParamProxy.ContinueOnTomorrow
                                                };
                                                ruleParamRep.WithoutTransactSave(ruleParam);
                                            }

                                        }
                                    }
                                }
                            }

                            break;
                    }
                    switch (customCode)
                    {
                        case (int)UIVlidationRulesTagExist.R33:
                            UIValidationRuleGroup RuleGroup = new UIValidationRuleGroup()
                            {
                                ID = ruleGroupId,
                                ValidationRule = new UIValidationRule() { ID = ruleId },
                                ValidationGroup = new UIValidationGroup() { ID = groupId },
                                Active = ruleGroupActive,
                                Warning = ruleGroupWarning,
                                Tag = ruleDetails
                            };
                            ruleGroupRep.WithoutTransactUpdate(RuleGroup);
                            break;
                    }
                    NHibernateSessionManager.Instance.CommitTransactionOn();
                }
                catch (Exception ex)
                {
                    NHibernateSessionManager.Instance.RollbackTransactionOn();
                    LogException(ex, "BUIValidationGroup", "UpdateRulePrecardsParams");
                    throw ex;
                }
            }
        }
        /// <summary>
        /// تبدیل مقادیر پارامترهای قانون در صورتی که 
        /// مقادیر از نوع تاریخ یا زمان باشد در هنگامی که زبان سیستم فارسی باشد
        /// </summary>
        /// <param name="ruleParamProxy"></param>
        /// <returns></returns>
        public string ConvertParamValue(UiValidationRuleParamProxy ruleParamProxy)
        {
            if (ruleParamProxy.ParamType == (decimal)RuleParamType.Date)
            {
                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    ruleParamProxy.ParameterValue = Utility.ToMildiDateString(ruleParamProxy.ParameterValue);
                }
            }
            else if (ruleParamProxy.ParamType == (decimal)RuleParamType.Time && ruleParamProxy.ContinueOnTomorrow)
            {
                ruleParamProxy.ParameterValue = ruleParamProxy.ParameterValue.Replace("+", "");
                ruleParamProxy.ParameterValue = Utility.IntTimeToRealTime(Utility.RealTimeToIntTime(ruleParamProxy.ParameterValue) + 1440);
            }
            return ruleParamProxy.ParameterValue;
        }
        public void Validate(decimal ruleId, decimal ruleGroupId, IList<Precard> precardsList, int ruleType, IList<UIValidationRuleTempParameter> ruleTempParamList, IList<UiValidationRulePrecardParamProxy> rulePrecardParamProxyList)
        {
            UIValidationExceptions exception = new UIValidationExceptions();
            ValidationException validationException = null;
            string CustomCode = string.Empty;
            if (rulePrecardParamProxyList.Count != 0)
            {
                foreach (UiValidationRulePrecardParamProxy rulePrecardParamProxy in rulePrecardParamProxyList)
                {
                    CustomCode = NHSession.QueryOver<UIValidationRule>()
                                                       .Where(x => x.ID == ruleId)
                                                       .Select(x => x.CustomCode)
                                                       .List<string>().SingleOrDefault();
                    if (rulePrecardParamProxy.ObjRuleParams != null)
                        exception = this.ValidateUiValidationRules(rulePrecardParamProxy, CustomCode, exception, rulePrecardParamProxyList);

                    switch (ruleType)
                    {
                        case (int)UIValidationRuleType.RulePrecardParameter:
                            string precardName = string.Empty;
                            Precard precard = precardsList.Where(x => x.ID == rulePrecardParamProxy.PrecardID).SingleOrDefault();
                            if (precard != null)
                                precardName = precard.Name;
                            if (rulePrecardParamProxy.Active && (rulePrecardParamProxy.ObjRuleParams.Count == 0 || rulePrecardParamProxy.ObjRuleParams.Count != ruleTempParamList.Count()))
                            {
                                validationException = new ValidationException(ExceptionResourceKeys.ValidationPrecardParamIsNotValue, " پارامتر پیشکارت مقدار نگرفته است :", ExceptionSrc);
                                validationException.Data.Add("Info", precardName);
                                exception.Add(validationException);
                            }
                            if (!rulePrecardParamProxy.Active && rulePrecardParamProxy.ObjRuleParams.Count != 0 && rulePrecardParamProxy.ObjRuleParams.Count != ruleTempParamList.Count())
                            {
                                validationException = new ValidationException(ExceptionResourceKeys.ValidationPrecardParamIsNotValue, " پارامتر پیشکارت مقدار نگرفته است :", ExceptionSrc);
                                validationException.Data.Add("Info", precardName);
                                exception.Add(validationException);
                            }
                            if (rulePrecardParamProxy.Active && rulePrecardParamProxy.ObjRuleParams.Count != 0 && rulePrecardParamProxy.ObjRuleParams.Count == ruleTempParamList.Count())
                            {
                                foreach (UiValidationRuleParamProxy ruleParamProxy in rulePrecardParamProxy.ObjRuleParams)
                                {
                                    if (ruleParamProxy.ParameterValue == string.Empty)
                                    {
                                        validationException = new ValidationException(ExceptionResourceKeys.ValidationPrecardParamIsNotValue, " پارامتر پیشکارت مقدار نگرفته است :", ExceptionSrc);
                                        validationException.Data.Add("Info", precardName);
                                        exception.Add(validationException);
                                    }
                                }
                            }
                            break;
                        case (int)UIValidationRuleType.RuleParameter:
                            if (rulePrecardParamProxy.ObjRuleParams != null && rulePrecardParamProxy.ObjRuleParams.Count != ruleTempParamList.Count)
                            {
                                ruleTempParamList = ruleTempParamList.Where(x => !rulePrecardParamProxy.ObjRuleParams.Select(y => y.ParamID).ToList<decimal>().Contains(x.ID)).ToList<UIValidationRuleTempParameter>();
                                if (ruleTempParamList.Count != 0)
                                {
                                    foreach (UIValidationRuleTempParameter ruleTempParam in ruleTempParamList)
                                    {
                                        validationException = new ValidationException(ExceptionResourceKeys.ValidationRuleParamIsNotValue, "پارامتر قانون مقدار نگرفته است :", ExceptionSrc);
                                        validationException.Data.Add("Info", ruleTempParam.Name);
                                        exception.Add(validationException);
                                    }
                                }
                            }
                            else
                            {
                                if (rulePrecardParamProxy.ObjRuleParams != null && rulePrecardParamProxy.ObjRuleParams.Count == ruleTempParamList.Count)
                                {
                                    foreach (UiValidationRuleParamProxy ruleParamProxy in rulePrecardParamProxy.ObjRuleParams)
                                    {
                                        if (ruleParamProxy.ParameterValue == string.Empty)
                                        {
                                            validationException = new ValidationException(ExceptionResourceKeys.ValidationRuleParamIsNotValue, "پارامتر قانون مقدار نگرفته است :", ExceptionSrc);
                                            validationException.Data.Add("Info", ruleParamProxy.KeyName);
                                            exception.Add(validationException);
                                        }
                                    }
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            else
            {
                exception.Add(ExceptionResourceKeys.ValidationRuleNotValid, "مقادیر نامعتبر می باشد", ExceptionSrc);
            }
            if (exception.Count > 0)
            {
                throw exception;
            }
        }
        /// <summary>
        /// اعتبارسنجی مقادیر پارامترهای تعدادی از قانون های خاص
        /// </summary>
        /// <param name="rulePrecardParamProxy"></param>
        /// <param name="CustomCode"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        public UIValidationExceptions ValidateUiValidationRules(UiValidationRulePrecardParamProxy rulePrecardParamProxy, string CustomCode, UIValidationExceptions exception, IList<UiValidationRulePrecardParamProxy> rulePrecardParamProxyList)
        {
            try
            {
                Dictionary<string, string> dicKeyNameValue = new Dictionary<string, string>();
                IList<UiValidationRuleParamProxy> RuleParamProxy = new List<UiValidationRuleParamProxy>();
                IList<string> RuleParameterValueList = new List<string>();
                IList<UiValidationRuleParamProxy> ruleparamProxy = new List<UiValidationRuleParamProxy>(); ;
                RuleParamProxy = rulePrecardParamProxy.ObjRuleParams.ToList();
                int ActiverulePrecardParamProxyList = 0;
                foreach (UiValidationRuleParamProxy ruleParamProxy in RuleParamProxy)
                {
                    dicKeyNameValue.Add(ruleParamProxy.KeyName, ruleParamProxy.ParameterValue);
                }
                if (CustomCode == "26")
                {
                    ActiverulePrecardParamProxyList = rulePrecardParamProxyList.Where(i => i.Active == true).Count();

                    foreach (UiValidationRulePrecardParamProxy RulePrecardParamProxy in rulePrecardParamProxyList)
                    {
                        ruleparamProxy = RulePrecardParamProxy.ObjRuleParams.ToList();
                        foreach (UiValidationRuleParamProxy ruleparam in ruleparamProxy)
                        {
                            RuleParameterValueList.Add(ruleparam.ParameterValue);
                        }
                    }
                }
                exception = base.UIValidationValidator.ValidateRulesParametersValue(CustomCode, dicKeyNameValue, exception, ActiverulePrecardParamProxyList, RuleParameterValueList);
                return exception;
            }
            catch (Exception ex)
            {
                LogException(ex, "BUIValidationGroup", "ValidateR1ParametersValue");
                throw ex;
            }
        }


        private string GetRandomColor(int index)
        {            
            IList<string> colorsBatchList = new List<string>() { "#31b0d5", "#449d44" };
            return index % 2 == 0 ? colorsBatchList[0] : colorsBatchList[1];
            //return "#" + Color.FromArgb(0, random.Next(128, 255), random.Next(128, 255), random.Next(128, 255)).Name ;            
        }  
        public IList<UIValidationGroup> GetAllUiValidationGroup()
        {
            IList<UIValidationGroup> UiValidationGroupList = NHSession.QueryOver<UIValidationGroup>().List<UIValidationGroup>();
            return UiValidationGroupList;
        }
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckUIValidationLoadAccess()
        {
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertUIValidationGroup(UIValidationGroup uiValidationGroup, UIActionType UAT)
        {
            return base.SaveChanges(uiValidationGroup, UAT);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal UpdateUIValidationGroup(UIValidationGroup uiValidationGroup, IList<UiValidationRuleGroupProxy> UiValidtionRuleGroupProxyList, UIActionType UAT)
        {
            decimal GroupID = base.SaveChanges(uiValidationGroup, UAT);
            this.UpdateRulesList(UiValidtionRuleGroupProxyList);
            return GroupID;
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal DeleteUIValidationGroup(UIValidationGroup uiValidationGroup, UIActionType UAT)
        {
            return base.SaveChanges(uiValidationGroup, UAT);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckUIValidationRulesLoadAccess()
        {
        }



        #region BaseBusiness Implementation

        public override IList<UIValidationGroup> GetAll()
        {
            IList<UIValidationGroup> list = base.GetAll();
            return list.Where(x => x.SubSystemID == (int)SubSystemIdentifier.TimeAtendance).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        protected override void InsertValidate(UIValidationGroup obj)
        {
            UIValidationExceptions exception = new UIValidationExceptions();
            if (Utility.IsEmpty(obj.CustomCode))
            {
                exception.Add(ExceptionResourceKeys.ValidationGroupCodeIsEmpty, "کد گروه اعتبار سنجی نباید خالی باشد", ExceptionSrc);
            }
            else
            {
                if (objectRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new UIValidationGroup().CustomCode), obj.CustomCode.ToLower())) > 0)
                {
                    exception.Add(ExceptionResourceKeys.ValidationGroupCodeIsRepeated, "کد گروه اعتبارسنجی نباید تکراری باشد", ExceptionSrc);
                }
            }
            if (Utility.IsEmpty(obj.Name))
            {
                exception.Add(ExceptionResourceKeys.ValidationGroupNameIsEmpty, "نام گروه اعتبارسنجی نباید خالی باشد", ExceptionSrc);
            }
            else
            {
                if (objectRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new UIValidationGroup().Name), obj.Name.ToLower())) > 0)
                {
                    exception.Add(ExceptionResourceKeys.ValidationGroupNameIsRepeated, "نام گروه اعتبارسنجی نباید تکراری باشد", ExceptionSrc);
                }
            }

            //if (obj.GroupingList==null || obj.GroupingList.Count==0 || obj.GroupingList.Where(x => x.RuleID == 0).Count() > 0)
            //{
            //    exception.Add(ExceptionResourceKeys.ValidationGroupRulesIsEmpty, "برای گروه اعتبارسنجی باید قانون انتخاب شود", ExceptionSrc);
            //}

            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        protected override void UpdateValidate(UIValidationGroup obj)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            if (Utility.IsEmpty(obj.CustomCode))
            {
                exception.Add(ExceptionResourceKeys.ValidationGroupCodeIsEmpty, "کد گروه اعتبار سنجی نباید خالی باشد", ExceptionSrc);
            }
            else
            {
                if (objectRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new UIValidationGroup().CustomCode), obj.CustomCode.ToLower()),
                                                 new CriteriaStruct(Utility.GetPropertyName(() => new UIValidationGroup().ID), obj.ID, CriteriaOperation.NotEqual)) > 0
                   )
                {
                    exception.Add(ExceptionResourceKeys.ValidationGroupCodeIsRepeated, "کد گروه اعتبارسنجی نباید تکراری باشد", ExceptionSrc);
                }
            }
            if (Utility.IsEmpty(obj.Name))
            {
                exception.Add(ExceptionResourceKeys.ValidationGroupNameIsEmpty, "نام گروه اعتبارسنجی نباید خالی باشد", ExceptionSrc);
            }
            else
            {
                if (objectRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new UIValidationGroup().Name), obj.Name.ToLower()),
                                                 new CriteriaStruct(Utility.GetPropertyName(() => new UIValidationGroup().ID), obj.ID, CriteriaOperation.NotEqual)) > 0
                   )
                {
                    exception.Add(ExceptionResourceKeys.ValidationGroupNameIsRepeated, "نام گروه اعتبارسنجی نباید تکراری باشد", ExceptionSrc);
                }
            }

            //if (obj.GroupingList == null || obj.GroupingList.Count == 0 || obj.GroupingList.Where(x => x.RuleID == 0).Count() > 0)
            //{
            //    exception.Add(ExceptionResourceKeys.ValidationGroupRulesIsEmpty, "برای گروه اعتبارسنجی باید قانون انتخاب شود", ExceptionSrc);
            //}

            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        protected override void DeleteValidate(UIValidationGroup obj)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            PersonRepository rep = new PersonRepository(false);

            if (rep.CheckIsUIValidationGroupInUse(obj))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.UIValidationGroupUsedByPerson, "بدلیل استفاده در پرسنل نباید حذف شود", ExceptionSrc));
            }

            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        protected override void GetReadyBeforeSave(UIValidationGroup obj, UIActionType action)
        {
            if (action == UIActionType.ADD || action == UIActionType.EDIT)
            {
                obj.SubSystemID = (int)SubSystemIdentifier.TimeAtendance;
                if (obj.RuleGroupList != null)
                {
                    foreach (UIValidationRuleGroup g in obj.RuleGroupList)
                    {
                        g.ValidationGroup = obj;
                    }
                }
            }
        }
        /// <summary>
        /// بعد از بروز رسانی گروه اعتبارسنجی و تخصیص قوانین جدید به آن
        /// باید قوانین قبلی منتسب به آن را حذف شوند.این کار اینجا انجام میگیرد
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="action"></param>
        protected override void OnSaveChangesSuccess(UIValidationGroup obj, UIActionType action)
        {
            if (action == UIActionType.ADD)
                this.SetUiValidationRulesGroup(obj.ID);
            //if (action == UIActionType.EDIT) 
            //{
            //    UIValidationRuleGroupRepository rep = new UIValidationGroupingRepository();
            //    var ids = from o in obj.GroupingList
            //              select o.RuleID;
            //    IList<decimal> idList = ids.ToList();
            //    rep.DeleteAfterUpdate(obj.ID, idList);
            // }
        }

        #endregion

    }
}
