using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Infrastructure.RepositoryFramework;
using GTS.Clock.Model.UIValidation;
using NHibernate.Transform;
using GTS.Clock.Model;
using NHibernate;

namespace GTS.Clock.Infrastructure.Repository
{
    public class UIValidationGroupingRepository : RepositoryBase<UIValidationRuleGroup>
    {
        int operationBatchSizeValue = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings[OperationBatchSize.OperationBatchSizeKey.ToString()]);
        public override string TableName
        {
            get { return "TA_UIValidationRuleGroup"; }
        }

        /// <summary>
        /// بعد از بروزرسانی یگ گروه اعتبارسنجی و تخصیص قوانین جدید لازم است قوانین قبلی حذف و قوانین جدید
        /// جایگزین قبلی شود
        /// </summary>
        /// <param name="validationGroupId"></param>
        /// <param name="newRuleIds"></param>
        public void DeleteAfterUpdate(decimal validationGroupId, IList<decimal> newRuleIds)
        {
            string SQLCommand = @"Delete From TA_UIValidationGrouping where UIGrp_GroupID=:groupId
                                  and UIGrp_RuleID not in (:newRuleIds)";
            base.NHibernateSession.CreateSQLQuery(SQLCommand)
                .SetParameter("groupId", validationGroupId)
                .SetParameterList("newRuleIds", base.CheckListParameter(newRuleIds))
                .ExecuteUpdate();
        }

        //public decimal GetByPersonIdAndRuleCode(decimal personId, int ruleCode)
        //{
        //    IList<decimal> kk = new List<decimal>();
        //    UIValidationRuleGroup grouping = null;
        //    UIValidationGroup group = null;
        //    UIValidationRule rule = null;
        //    PersonTASpec person = null;

        //    IList<UIValidationRuleGroup> list = base.NHibernateSession.QueryOver<UIValidationRuleGroup>(() => grouping)
        //                                  .JoinAlias(() => grouping.ValidationGroup, () => group)
        //                                  .JoinAlias(() => group.PersonTAList, () => person)
        //                                  .JoinAlias(() => grouping.ValidationRule, () => rule)
        //                                  .Where(() => person.ID == personId)
        //                                  .And(() => rule.CustomCode == ruleCode.ToString())
        //                                  .And(() => group.SubSystemID == (int)SubSystemIdentifier.TimeAtendance)
        //                                  .List<UIValidationRuleGroup>();

        //    decimal result = list != null && list.Count > 0 ? list.First().ID : 0;

        //    return result;
        //}
        public decimal GetByPersonIdAndRuleCode(decimal personId, int ruleCode)
        {
            IList<decimal> kk = new List<decimal>();
            UIValidationRuleGroup grouping = null;
            UIValidationGroup group = null;
            UIValidationRule rule = null;
            PersonTASpec person = null;

            IList<UIValidationRuleGroup> list = base.NHibernateSession.QueryOver<UIValidationRuleGroup>(() => grouping)
                                          .JoinAlias(() => grouping.ValidationGroup, () => group)
                                          .JoinAlias(() => group.PersonTAList, () => person)
                                          .JoinAlias(() => grouping.ValidationRule, () => rule)
                                          .Where(() => person.ID == personId)
                                          .And(() => rule.CustomCode == ruleCode.ToString())
                                          .And(() => group.SubSystemID == (int)SubSystemIdentifier.TimeAtendance)
                                          .List<UIValidationRuleGroup>();

            decimal result = list != null && list.Count > 0 ? list.First().ID : 0;

            return result;
        }


        //public IList<UIValidationRuleGroup> GetByPersonId(decimal personId)
        //{

        //    UIValidationRuleGroup grouping = null;
        //    UIValidationGroup group = null;
        //    PersonTASpec person = null;

        //    IList<UIValidationRuleGroup> list = base.NHibernateSession.QueryOver<UIValidationRuleGroup>(() => grouping)
        //                                  .JoinAlias(() => grouping.ValidationGroup, () => group)
        //                                  .JoinAlias(() => group.PersonTAList, () => person)
        //                                  .Where(() => person.ID == personId)
        //                                  .And(() => group.SubSystemID == (int)SubSystemIdentifier.TimeAtendance)
        //                                  .List<UIValidationRuleGroup>();

        //    IList<UIValidationRuleGroup> result = list != null && list.Count > 0 ? list : new List<UIValidationRuleGroup>();

        //    return result;
        //}
        public IList<UIValidationRuleGroup> GetByPersonId(decimal personId)
        {

            UIValidationRuleGroup grouping = null;
            UIValidationGroup group = null;
            PersonTASpec person = null;

            IList<UIValidationRuleGroup> list = base.NHibernateSession.QueryOver<UIValidationRuleGroup>(() => grouping)
                                          .JoinAlias(() => grouping.ValidationGroup, () => group)
                                          .JoinAlias(() => group.PersonTAList, () => person)
                                          .Where(() => person.ID == personId)
                                          .And(() => group.SubSystemID == (int)SubSystemIdentifier.TimeAtendance)
                                          .List<UIValidationRuleGroup>();

            IList<UIValidationRuleGroup> result = list != null && list.Count > 0 ? list : new List<UIValidationRuleGroup>();

            return result;
        }

        //public IList<UIValidationRuleGroup> GetByGroupId(decimal groupId)
        //{
        //    UIValidationRuleGroup grouping = null;
        //    UIValidationGroup group = null;
        //    IList<UIValidationRuleGroup> list = NHibernateSession.QueryOver<UIValidationRuleGroup>(() => grouping)
        //        //.Where(() => grouping.GroupID == groupId)
        //        .Where (() => grouping.ValidationGroup.ID==groupId)
        //        .List();

        //    IList<UIValidationRuleGroup> result = list != null && list.Count > 0 ? list : new List<UIValidationRuleGroup>();

        //    return result;
        //}
        public IList<UIValidationRuleGroup> GetByGroupId(decimal groupId)
        {
            UIValidationRuleGroup grouping = null;
            IList<UIValidationRuleGroup> list = NHibernateSession.QueryOver<UIValidationRuleGroup>(() => grouping)
                //.Where(() => grouping.GroupID == groupId)
                .Where(() => grouping.ValidationGroup.ID == groupId)
                .List();

            IList<UIValidationRuleGroup> result = list != null && list.Count > 0 ? list : new List<UIValidationRuleGroup>();

            return result;
        }
        /// <summary>
        /// پیشکاتهای متصل به یک قانون را برمیگرداند
        /// </summary>
        /// <param name="ruleId"></param>
        /// <returns></returns>
        //        public IList<Precard> GetPrecard(string uiRuleCustomCode)
        //        {

        //            string SQLCommand = @"select * from TA_Precard where 
        //                                  Precrd_Code in (select UIRlePrecard_PrecardCustomCode from TA_UIValidationRuleGroupPrecard 
        //                                  where UIRlePrecard_RuleCustomCode=:ruleCode)";

        //            IList<Precard> list = base.NHibernateSession.CreateSQLQuery(SQLCommand)
        //                                     .AddEntity(typeof(Precard))
        //                                     .SetParameter("ruleCode", uiRuleCustomCode)
        //                                     .List<Precard>();
        //            IList<Precard> result = list != null && list.Count > 0 ? list : new List<Precard>();
        //            return result;
        //        }
        //        public IList<Precard> GetPrecardRule(Decimal uiRuleID)
        //        {

        ////            string SQLCommand = @"select * from TA_Precard where 
        ////                                  Precrd_Code in (select UIValPre_PrecardCustomCode from TA_UIValidationPrecard
        ////                                  where UIValPre_RuleCustomCode=:ruleCode)";
        //            string SQLCommand = @"select * from TA_Precard where 
        //                                  Precrd_ID in (select UIValPre_PrecardID from TA_UIValidationRuleGroupPrecard
        //                                  where UIValPre_RuleGroupID=:ruleCode)";

        //            IList<Precard> list = base.NHibernateSession.CreateSQLQuery(SQLCommand)
        //                                     .AddEntity(typeof(Precard))
        //                                     .SetParameter("ruleCode", uiRuleID)
        //                                     .List<Precard>();
        //            IList<Precard> result = list != null && list.Count > 0 ? list : new List<Precard>();
        //            return result;
        //        }
        public IList<Precard> GetPrecardRule(Decimal uiRuleID)
        {
            string SQLCommand = @"select * from TA_Precard where 
                                  Precrd_ID in (select UIValPre_PrecardID from TA_UIValidationRuleGroupPrecard
                                  where UIValPre_RuleGroupID=:ruleCode and UIValPre_Active=1)";

            IList<Precard> list = base.NHibernateSession.CreateSQLQuery(SQLCommand)
                                     .AddEntity(typeof(Precard))
                                     .SetParameter("ruleCode", uiRuleID)
                                     .List<Precard>();
            IList<Precard> result = list != null && list.Count > 0 ? list : new List<Precard>();
            return result;
        }
        public IList<UIValidationRuleParam> GetParameterRule(Decimal uiRuleID)
        {

            string SQLCommand = @"select * from TA_UIValidationRuleParam where 
                                  UIValParam_RuleGroupPrecardID in (select UIValPre_ID from TA_UIValidationRuleGroupPrecard
                                where UIValPre_RuleGroupID=:ruleCode And UIValPre_Active=1)";

            IList<UIValidationRuleParam> list = base.NHibernateSession.CreateSQLQuery(SQLCommand)
                                     .AddEntity(typeof(UIValidationRuleParam))
                                     .SetParameter("ruleCode", uiRuleID)
                                     .List<UIValidationRuleParam>();
            IList<UIValidationRuleParam> result = list != null && list.Count > 0 ? list : new List<UIValidationRuleParam>();
            return result;
        }

        public IList<UIValidationRuleParam> GetParameterRule(Decimal uiRuleID, Decimal uiPrecardID, string Parameter)
        {
            string SQLCommand = @"select * from TA_UIValidationRuleParam where UIValParam_RuleGroupPrecardID in 
                                (select UIValPre_ID from TA_UIValidationRuleGroupPrecard
                                where UIValPre_RuleGroupID=:ruleCode and UIValPre_PrecardID=:precardCode and UIValPre_Active=1)
                                and UIValParam_RuleTempParamID in (select UIValTemp_ID from TA_UIValidationRuleTempParameter
                                 where UIValTemp_KeyName='" + Parameter + "')";
            IList<UIValidationRuleParam> list = base.NHibernateSession.CreateSQLQuery(SQLCommand)
                                     .AddEntity(typeof(UIValidationRuleParam))
                                     .SetParameter("ruleCode", uiRuleID)
                                     .SetParameter("precardCode", uiPrecardID)
                                     .List<UIValidationRuleParam>();
            IList<UIValidationRuleParam> result = list != null && list.Count > 0 ? list : new List<UIValidationRuleParam>();
            return result;
        }
        public IList<UIValidationRuleParam> GetParameterRule(Decimal uiRuleID, Decimal uiPrecardID, string FirstParam, string SecondParam)
        {
            string SQLCommand = @"select * from TA_UIValidationRuleParam where UIValParam_RuleGroupPrecardID in 
                                (select UIValPre_ID from TA_UIValidationRuleGroupPrecard
                                where UIValPre_RuleGroupID=:ruleCode and UIValPre_PrecardID=:precardCode and UIValPre_Active=1)
                                and UIValParam_RuleTempParamID in (select UIValTemp_ID from TA_UIValidationRuleTempParameter
                                 where UIValTemp_KeyName='" + FirstParam + "'" + @"or UIValTemp_KeyName='" + SecondParam + "')";
            IList<UIValidationRuleParam> list = base.NHibernateSession.CreateSQLQuery(SQLCommand)
                                     .AddEntity(typeof(UIValidationRuleParam))
                                     .SetParameter("ruleCode", uiRuleID)
                                     .SetParameter("precardCode", uiPrecardID)
                                     .List<UIValidationRuleParam>();
            IList<UIValidationRuleParam> result = list != null && list.Count > 0 ? list : new List<UIValidationRuleParam>();
            return result;
        }
        public IList<UIValidationRuleParam> GetParameterRule(Decimal uiRuleID, Decimal uiPrecardID, string FirstParam, string SecondParam, string ThirthParam)
        {
            string SQLCommand = @"select * from TA_UIValidationRuleParam where UIValParam_RuleGroupPrecardID in 
                                (select UIValPre_ID from TA_UIValidationRuleGroupPrecard
                                where UIValPre_RuleGroupID=:ruleCode and UIValPre_PrecardID=:precardCode and UIValPre_Active=1)
                                and UIValParam_RuleTempParamID in (select UIValTemp_ID from TA_UIValidationRuleTempParameter
                                 where UIValTemp_KeyName='" + FirstParam + "'" + @" or UIValTemp_KeyName='" + SecondParam + @"' or UIValTemp_KeyName='" + ThirthParam + "')";
            IList<UIValidationRuleParam> list = base.NHibernateSession.CreateSQLQuery(SQLCommand)
                                     .AddEntity(typeof(UIValidationRuleParam))
                                     .SetParameter("ruleCode", uiRuleID)
                                     .SetParameter("precardCode", uiPrecardID)
                                     .List<UIValidationRuleParam>();
            IList<UIValidationRuleParam> result = list != null && list.Count > 0 ? list : new List<UIValidationRuleParam>();
            return result;
        }
        public IList<UIValidationRuleParam> GetParameterRule(Decimal uiRuleID, Decimal uiPrecardID, string FirstParam, string SecondParam, string ThirthParam, string ForthParam, string FifthParam, string SixthParam, string SeventhParam)
        {

            string SQLCommand = @"select * from TA_UIValidationRuleParam where UIValParam_RuleGroupPrecardID in 
                                (select UIValPre_ID from TA_UIValidationRuleGroupPrecard
                                where UIValPre_RuleGroupID=:ruleCode and UIValPre_PrecardID=:precardCode and UIValPre_Active=1)
                                and UIValParam_RuleTempParamID in (select UIValTemp_ID from TA_UIValidationRuleTempParameter
                                 where UIValTemp_KeyName='" + FirstParam + "'" + @" or UIValTemp_KeyName='" + SecondParam + "'" + @" or UIValTemp_KeyName='" + ThirthParam + "'" + @" or UIValTemp_KeyName='" + ForthParam + "'" + @" or UIValTemp_KeyName='" + FifthParam + "'" + @" or UIValTemp_KeyName='" + SixthParam + "'" + @" or UIValTemp_KeyName='" + SeventhParam + "')";
            IList<UIValidationRuleParam> list = base.NHibernateSession.CreateSQLQuery(SQLCommand)
                                     .AddEntity(typeof(UIValidationRuleParam))
                                     .SetParameter("ruleCode", uiRuleID)
                                     .SetParameter("precardCode", uiPrecardID)
                                     .List<UIValidationRuleParam>();
            IList<UIValidationRuleParam> result = list != null && list.Count > 0 ? list : new List<UIValidationRuleParam>();
            return result;
        }
        //  
        public IList<decimal> GetUivalidationIdListByWorkGroup(decimal workGroupId)
        {
            string SQLCommand = @"select prsTA_UIValidationGroupID from  TA_PersonTASpec as pts
                             inner join TA_Person prs
                             on pts.prsTA_ID = prs.Prs_ID
                             inner join TA_AssignWorkGroup as awg
                             on awg.AsgWorkGroup_PersonId = prs.Prs_ID
                             where awg.AsgWorkGroup_WorkGroupId =:workGroupId and prs.Prs_Active =1 and prs.prs_IsDeleted =0
                             and prsTA_UIValidationGroupID is  not null
                             group by prsTA_UIValidationGroupID
                            ";
            IQuery query = NHibernateSession.CreateSQLQuery(SQLCommand)
                          .SetParameter("workGroupId", workGroupId);
            IList<decimal> UiValidationGroupIdList = query.List<decimal>();
            return UiValidationGroupIdList;
        }
        public IList<decimal> GetUivalidationIdListByRuleCategory(decimal RuleCategoryId)
        {
            string SQLCommand = @"select prsTA_UIValidationGroupID from  TA_PersonTASpec as pts
                             inner join TA_Person prs
                             on pts.prsTA_ID = prs.Prs_ID
                             inner join TA_PersonRuleCategoryAssignment as awg
                             on awg.PrsRulCatAsg_PersonId = prs.Prs_ID
                             where  awg.PrsRulCatAsg_RuleCategoryId =:RuleCategoryId and prs.Prs_Active =1 and prs.prs_IsDeleted =0
                             and prsTA_UIValidationGroupID is  not null
                             group by prsTA_UIValidationGroupID
                            ";
            IQuery query = NHibernateSession.CreateSQLQuery(SQLCommand)
                          .SetParameter("RuleCategoryId", RuleCategoryId);
            IList<decimal> UiValidationGroupIdList = query.List<decimal>();
            return UiValidationGroupIdList;
        }
        public IList<decimal> GetUivalidationIdListByCalculationRangeGroup(decimal calculationRangeGroupId)
        {
            string SQLCommand = @"select prsTA_UIValidationGroupID from  TA_PersonTASpec as pts
                             inner join TA_Person prs
                             on pts.prsTA_ID = prs.Prs_ID
                             inner join TA_PersonRangeAssignment as awg
                             on awg.PrsRangeAsg_PersonId = prs.Prs_ID
                             where awg.PrsRangeAsg_CalcRangeGrpId =:CalculationRangeGroupId and prs.Prs_Active =1 and prs.prs_IsDeleted =0
                             and prsTA_UIValidationGroupID is  not null
                             group by prsTA_UIValidationGroupID
                            ";
            IQuery query = NHibernateSession.CreateSQLQuery(SQLCommand)
                          .SetParameter("CalculationRangeGroupId", calculationRangeGroupId);
            IList<decimal> UiValidationGroupIdList = query.List<decimal>();
            return UiValidationGroupIdList;
        }
        public IList<decimal> GetUivalidationIdList(IList<decimal> personIdList)
        {
            IList<decimal> UiValidationGroupIdList = new List<decimal>();
            string SQLCommand = string.Empty;
            if(personIdList.Count < this.operationBatchSizeValue && this.operationBatchSizeValue < 2100 )
            {
                    SQLCommand = @" select prsTA_UIValidationGroupID from  TA_PersonTASpec as pts
                                   inner join TA_Person prs
                                   on pts.prsTA_ID = prs.Prs_ID
                                   inner join TA_UIValidationGroup as uvg
								   on  pts.prsTA_UIValidationGroupID = uvg.UIValGrp_ID 
                                   where  pts.prsTA_ID in (:personlist) and prs.Prs_Active =1 and prs.prs_IsDeleted =0
                                   and prsTA_UIValidationGroupID is  not null
                                   group by prsTA_UIValidationGroupID
                                 ";              
                UiValidationGroupIdList = NHibernateSession.CreateSQLQuery(SQLCommand)
                                                .SetParameterList("personlist", personIdList.ToArray()).List<decimal>();
            }   
            else
            {
                TempRepository tempRep = new TempRepository(false);                
                string operationGUID = tempRep.InsertTempList(personIdList);
                SQLCommand = @"select prsTA_UIValidationGroupID from  TA_PersonTASpec as pts
                                   inner join TA_Person prs
                                   on pts.prsTA_ID = prs.Prs_ID
                                   Inner Join TA_Temp temp
                                   on prs.Prs_ID=temp.temp_ObjectID 
                                   inner join TA_UIValidationGroup as uvg
								   on   pts.prsTA_UIValidationGroupID = uvg.UIValGrp_ID 
                                   where   prs.Prs_Active =1 and prs.prs_IsDeleted =0 and temp_OperationGUID =:operationGUID
                                   and prsTA_UIValidationGroupID is  not null
                                   group by prsTA_UIValidationGroupID
                              ";
                UiValidationGroupIdList = NHibernateSession.CreateSQLQuery(SQLCommand)
                                         .SetParameter("operationGUID", operationGUID)
                                         .List<decimal>();
                tempRep.DeleteTempList(operationGUID);

            }

            return UiValidationGroupIdList;
        }

        public Dictionary<decimal, decimal> GetUivalidationPersonIdList(IList<decimal> personIdList)
        {
            Dictionary<decimal, decimal> UiValidationGroupPersonIdDic = new Dictionary<decimal, decimal>();
            string SQLCommand = string.Empty;

            if (personIdList.Count < this.operationBatchSizeValue && this.operationBatchSizeValue < 2100)
            {
                SQLCommand = @" select prs_Id,prsTA_UIValidationGroupID from  TA_PersonTASpec as pts
                                   inner join TA_Person prs
                                   on pts.prsTA_ID = prs.Prs_ID
                                   inner join TA_UIValidationGroup as uvg
								   on  pts.prsTA_UIValidationGroupID = uvg.UIValGrp_ID 
                                   where  pts.prsTA_ID in (:personlist) and prs.Prs_Active =1 and prs.prs_IsDeleted =0
                                   and prsTA_UIValidationGroupID is  not null
                                   group by prsTA_UIValidationGroupID,prs_Id ";
                var resultList = NHibernateSession.CreateSQLQuery(SQLCommand)
                                                .SetParameterList("personlist", personIdList.ToArray()).List<object>();
                foreach (var item in resultList)
                {

                    UiValidationGroupPersonIdDic.Add(Convert.ToDecimal(((object[])(item))[0]),Convert.ToDecimal(((object[])(item))[1]));
                }
                

            }
            else
            {
                TempRepository tempRep = new TempRepository(false);
                string operationGUID = tempRep.InsertTempList(personIdList);
                SQLCommand = @"select prs_Id,prsTA_UIValidationGroupID from  TA_PersonTASpec as pts
                                   inner join TA_Person prs
                                   on pts.prsTA_ID = prs.Prs_ID
                                   Inner Join TA_Temp temp
                                   on prs.Prs_ID=temp.temp_ObjectID 
                                   inner join TA_UIValidationGroup as uvg
								   on   pts.prsTA_UIValidationGroupID = uvg.UIValGrp_ID 
                                   where   prs.Prs_Active =1 and prs.prs_IsDeleted =0 and temp_OperationGUID =:operationGUID
                                   and prsTA_UIValidationGroupID is  not null
                                   group by prsTA_UIValidationGroupID,prs_Id
                              ";
                var resultList = NHibernateSession.CreateSQLQuery(SQLCommand)
                                         .SetParameter("operationGUID", operationGUID).List<object>();
                tempRep.DeleteTempList(operationGUID);
                foreach (var item in resultList)
                {

                    UiValidationGroupPersonIdDic.Add(Convert.ToDecimal(((object[])(item))[0]), Convert.ToDecimal(((object[])(item))[1]));
                }

            }
            
            return UiValidationGroupPersonIdDic;
        }

    }
}
