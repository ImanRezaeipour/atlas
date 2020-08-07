using GTS.Clock.Infrastructure;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Model.RequestFlow;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Business.Security;
using System.Globalization;
using GTS.Clock.Business.Temp;
using GTS.Clock.Business.Presentaion_Helper.Proxy;
using NHibernate.Criterion;
using GTS.Clock.Infrastructure.Repository;

namespace GTS.Clock.Business.RequestFlow
{
    public class BManagerFlowCondition : BaseBusiness<ManagerFlowCondition>
    {
        ISession NHSession = NHibernateSessionManager.Instance.GetSession();
        BPrecard bPrecard = new BPrecard();
        BFlow bFlow = new BFlow();
        const string ExceptionSrc = "GTS.Clock.Business.RequestFlow.BManagerFlowCondition";

        public IList<PrecardGroups> GetAllPrecardGroups()
        {
            try
            {
                IList<PrecardGroups> precardGroupsList = new List<PrecardGroups>();
                precardGroupsList = this.bPrecard.GetAllPrecardGroups();
                return precardGroupsList;
            }
            catch (Exception ex)
            {
                LogException(ex, "BManagerFlowCondition", "GetAllPrecardGroups");
                throw ex;
            }
        }

        public IList<ManagerFlowConditionProxy> GetAllManagerFlowConditionsByPrecardGroup(decimal flowID, decimal managerFlowID, decimal precardGroupID, Dictionary<ConditionOperators, string> conditionOperatorsDic)
        {
            try
            {
                Precard precardAlias = null;
                PrecardGroups precardGroupsAlias = null;
                PrecardAccessGroupDetail precardAccessGroupDetailAlias = null;
                ManagerFlow managerFlowAlias = null;
                ManagerFlowCondition managerFlowConditionAlias = null;
                IList<ManagerFlowConditionProxy> ManagerFlowConditionProxyList = new List<ManagerFlowConditionProxy>();

                IList<ManagerFlowCondition> existingManagerFlowConditionsList = NHSession.QueryOver<ManagerFlowCondition>(() => managerFlowConditionAlias)
                                                                         .JoinAlias(() => managerFlowConditionAlias.PrecardAccessGroupDetail, () => precardAccessGroupDetailAlias)
                                                                         .JoinAlias(() => precardAccessGroupDetailAlias.Precard, () => precardAlias)
                                                                         .JoinAlias(() => precardAlias.PrecardGroup, () => precardGroupsAlias)
                                                                         .JoinAlias(() => managerFlowConditionAlias.ManagerFlow, () => managerFlowAlias)
                                                                         .Where(() => managerFlowAlias.ID == managerFlowID &&
                                                                                      precardGroupsAlias.ID == precardGroupID &&
                                                                                      precardAlias.Active
                                                                               )
                                                                         .List<ManagerFlowCondition>();


                foreach (ManagerFlowCondition managerFlowConditionItem in existingManagerFlowConditionsList)
                {
                    ManagerFlowConditionProxy managerFlowConditionProxy = new ManagerFlowConditionProxy();
                    managerFlowConditionProxy.State = "View";
                    managerFlowConditionProxy.ID = managerFlowConditionItem.ID;
                    managerFlowConditionProxy.PrecardAccessGroupDetailID = managerFlowConditionItem.PrecardAccessGroupDetail.ID;
                    managerFlowConditionProxy.PrecardID = managerFlowConditionItem.PrecardAccessGroupDetail.Precard.ID;
                    managerFlowConditionProxy.PrecardName = managerFlowConditionItem.PrecardAccessGroupDetail.Precard.Name;
                    managerFlowConditionProxy.PrecardCode = managerFlowConditionItem.PrecardAccessGroupDetail.Precard.Code;
                    managerFlowConditionProxy.EndFlow = managerFlowConditionItem.EndFlow;
                    managerFlowConditionProxy.Access = true;
                    managerFlowConditionProxy.OperatorKey = ((ConditionOperators)managerFlowConditionItem.Operator).ToString();
                    managerFlowConditionProxy.OperatorTitle = conditionOperatorsDic[(ConditionOperators)managerFlowConditionItem.Operator];
                    managerFlowConditionProxy.Value = managerFlowConditionItem.Value;
                    managerFlowConditionProxy.ValueType = this.SetManagerFlowConditionValueType(managerFlowConditionItem.PrecardAccessGroupDetail.Precard);
                    managerFlowConditionProxy.Description = managerFlowConditionItem.Description;
                    ManagerFlowConditionProxyList.Add(managerFlowConditionProxy);
                }

                Flow flow = this.bFlow.GetByID(flowID);
                IList<PrecardAccessGroupDetail> precardAccessGroupDetailList = flow.AccessGroup.PrecardAccessGroupDetailList.Where(x => x.Precard.PrecardGroup.ID == precardGroupID).ToList<PrecardAccessGroupDetail>();
                IList<Precard> flowPrecardGroupPrecards = precardAccessGroupDetailList.Select(x => x.Precard).Where(x => x.Active).ToList<Precard>();
                foreach (Precard precardItem in flowPrecardGroupPrecards.Where(x => !ManagerFlowConditionProxyList.Select(y => y.PrecardID).ToList().Contains(x.ID)))
                {
                    ManagerFlowConditionProxy managerFlowConditionProxy = new ManagerFlowConditionProxy();
                    managerFlowConditionProxy.State = "View";
                    managerFlowConditionProxy.ID = 0;
                    managerFlowConditionProxy.PrecardAccessGroupDetailID = precardAccessGroupDetailList.Where(x => x.Precard.ID == precardItem.ID).First().ID;
                    managerFlowConditionProxy.PrecardID = precardItem.ID;
                    managerFlowConditionProxy.PrecardName = precardItem.Name;
                    managerFlowConditionProxy.PrecardCode = precardItem.Code;
                    managerFlowConditionProxy.EndFlow = false;
                    managerFlowConditionProxy.Access = true;
                    managerFlowConditionProxy.OperatorKey = string.Empty;
                    managerFlowConditionProxy.OperatorTitle = string.Empty;
                    managerFlowConditionProxy.Value = string.Empty;
                    managerFlowConditionProxy.ValueType = this.SetManagerFlowConditionValueType(precardItem);
                    managerFlowConditionProxy.Description = string.Empty;
                    ManagerFlowConditionProxyList.Add(managerFlowConditionProxy);
                }

                return ManagerFlowConditionProxyList;
            }
            catch (Exception ex)
            {
                LogException(ex, "BManagerFlowCondition", "GetAllManagerFlowConditionsByPrecardGroup");
                throw;
            }
        }

        private string SetManagerFlowConditionValueType(Precard precard)
        {
            string managerFlowConditionValueType = string.Empty;
            switch ((PrecardGroupsName)precard.PrecardGroup.IntLookupKey)
            {
                case PrecardGroupsName.leave:
                case PrecardGroupsName.leaveestelajy:
                case PrecardGroupsName.duty:
                    if (precard.IsHourly)
                        managerFlowConditionValueType = ManagerFlowConditionValueType.Minute.ToString();
                    else
                        if (precard.IsDaily)
                            managerFlowConditionValueType = ManagerFlowConditionValueType.Day.ToString();
                    break;
                case PrecardGroupsName.traffic:
                case PrecardGroupsName.overwork:
                case PrecardGroupsName.imperative:
                    managerFlowConditionValueType = ManagerFlowConditionValueType.Minute.ToString();
                    break;
                default:
                    break;
            }
            return managerFlowConditionValueType;
        }

        public IList<ManagerFlowCondition> GetAllManagerFlowConditionsByManagerFlowID(decimal managerFlowID)
        {
            IList<ManagerFlowCondition> managerFlowConditionsList = this.NHSession.QueryOver<ManagerFlowCondition>()
                                                                                  .Where(x => x.ManagerFlow.ID == managerFlowID)
                                                                                  .List<ManagerFlowCondition>();
            return managerFlowConditionsList;
        }

        protected override void InsertValidate(ManagerFlowCondition managerFlowCondition)
        {
        }

        protected override void UpdateValidate(ManagerFlowCondition managerFlowCondition)
        {
        }

        protected override void DeleteValidate(ManagerFlowCondition managerFlowCondition)
        {
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckFlowConditionsLoadAccess()
        {
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void UpdateFlowConditions(decimal flowID, decimal managerFlowID, IList<ManagerFlowConditionProxy> managerFlowConditionProxyList)
        {
            try
            {
                //ManagerFlowConditionPrecardProxy managerFlowConditionPrecardProxy = null;
                //IList<ManagerFlowConditionPrecardProxy> managerFlowConditionPreacardProxyList = new List<ManagerFlowConditionPrecardProxy>();
                ManagerFlowCondition managerFlowCondition = null;
                using (NHibernateSessionManager.Instance.BeginTransactionOn())
                {
                    foreach (ManagerFlowConditionProxy managerFlowConditionItem in managerFlowConditionProxyList)
                    {
                        if (managerFlowConditionItem.State != null && managerFlowConditionItem.State != string.Empty)
                        {
                            UIActionType operationState = (UIActionType)Enum.Parse(typeof(UIActionType), managerFlowConditionItem.State.ToUpper());
                            switch (operationState)
                            {
                                case UIActionType.EDIT:
                                    if (managerFlowConditionItem.ID == 0)
                                    {
                                        managerFlowCondition = new ManagerFlowCondition()
                                        {
                                            ID = 0,
                                            ManagerFlow = new ManagerFlow() { ID = managerFlowID },
                                            PrecardAccessGroupDetail = new PrecardAccessGroupDetail() { ID = managerFlowConditionItem.PrecardAccessGroupDetailID },
                                            Operator = (int)(ConditionOperators)Enum.Parse(typeof(ConditionOperators), managerFlowConditionItem.OperatorKey),
                                            Value = managerFlowConditionItem.Value,
                                            EndFlow = managerFlowConditionItem.EndFlow,
                                            Access = managerFlowConditionItem.Access,
                                        };
                                        this.SaveChanges(managerFlowCondition, UIActionType.ADD);
                                    }
                                    else
                                        if (managerFlowConditionItem.ID > 0)
                                        {
                                            managerFlowCondition = this.GetByID(managerFlowConditionItem.ID);
                                            managerFlowCondition.Operator = (int)(ConditionOperators)Enum.Parse(typeof(ConditionOperators), managerFlowConditionItem.OperatorKey);
                                            managerFlowCondition.Value = managerFlowConditionItem.Value;
                                            managerFlowCondition.EndFlow = managerFlowConditionItem.EndFlow;
                                            managerFlowCondition.Access = managerFlowConditionItem.Access;
                                            this.SaveChanges(managerFlowCondition, UIActionType.EDIT);
                                        }
                                    break;
                                case UIActionType.DELETE:
                                    if (managerFlowConditionItem.ID > 0)
                                    {
                                        managerFlowCondition = this.GetByID(managerFlowConditionItem.ID);
                                        this.SaveChanges(managerFlowCondition, UIActionType.DELETE);
                                    }
                                    break;
                            }

                            //switch (operationState)
                            //{
                            //    case UIActionType.ADD:
                            //    case UIActionType.EDIT:
                            //        managerFlowConditionPrecardProxy = new ManagerFlowConditionPrecardProxy()
                            //        {
                            //            ManagerFlowCondition = managerFlowCondition,
                            //            PrecardID = managerFlowConditionItem.PrecardID
                            //        };
                            //        managerFlowConditionPreacardProxyList.Add(managerFlowConditionPrecardProxy);
                            //        break;
                            //    case UIActionType.DELETE:
                            //        break;
                            //}
                        }
                    }
                    
                    //if(managerFlowConditionPreacardProxyList.Count > 0)
                    //   this.DeleteSuspendRequestStatesByFlowCondition(flowID, managerFlowConditionPreacardProxyList);

                    new RequestStatusRepositiory(false).DeleteSuspendRequestStates(flowID);                    
                }
            }
            catch (Exception ex)
            {
                NHibernateSessionManager.Instance.RollbackTransactionOn();
                LogException(ex, "BManagerFlowCondition", "UpdateFlowConditions");
                throw;
            }
        }

        private void DeleteSuspendRequestStatesByFlowCondition(decimal flowID, IList<ManagerFlowConditionPrecardProxy> managerFlowConditionPreacardProxyList)
        {
            try
            {
                Request requestAlias = null;
                RequestStatus requestStatusAlias = null;
                ManagerFlow managerFlowAlias = null;
                Flow flowAlias = null;
                GTS.Clock.Model.Temp.Temp precardAccessGroupDetailTempAlias = null;
                PrecardAccessGroup precardAccessGroupAlias = null;
                PrecardAccessGroupDetail precardAccessGroupDetailAlias = null;
                string precardAccessGroupDetailOperationGUID = string.Empty;
                string requestStatusOperationGUID = string.Empty;
                List<decimal> requestStatusIdsList = new List<decimal>();
                BTemp tempBusiness = new BTemp();

                precardAccessGroupDetailOperationGUID = tempBusiness.InsertTempList(managerFlowConditionPreacardProxyList.Select(x => x.ManagerFlowCondition.PrecardAccessGroupDetail.ID).ToList<decimal>());

                var EndFlowAndDeletedRequestStatusSubQuery = QueryOver.Of<RequestStatus>(() => requestStatusAlias)
                                                            .Where(() => requestStatusAlias.EndFlow || !requestStatusAlias.IsDeleted)
                                                            .And(() => requestStatusAlias.Request.ID == requestAlias.ID)
                                                            .Select(x => x.ID);

                IList<Request> requestsList = this.NHSession.QueryOver<Request>(() => requestAlias)
                                                            .JoinAlias(() => requestAlias.RequestStatusList, () => requestStatusAlias)
                                                            .JoinAlias(() => requestStatusAlias.ManagerFlow, () => managerFlowAlias)
                                                            .JoinAlias(() => managerFlowAlias.Flow, () => flowAlias)
                                                            .JoinAlias(() => flowAlias.AccessGroup, () => precardAccessGroupAlias)
                                                            .JoinAlias(() => precardAccessGroupAlias.PrecardAccessGroupDetailList, () => precardAccessGroupDetailAlias)
                                                            .JoinAlias(() => precardAccessGroupDetailAlias.TempList, () => precardAccessGroupDetailTempAlias)
                                                            .Where(() => !flowAlias.IsDeleted &&
                                                                         !requestAlias.EndFlow &&
                                                                         flowAlias.ID == flowID &&
                                                                         precardAccessGroupDetailTempAlias.OperationGUID == precardAccessGroupDetailOperationGUID
                                                            )
                                                            .WithSubquery
                                                            .WhereNotExists(EndFlowAndDeletedRequestStatusSubQuery)
                                                            .List<Request>();

                tempBusiness.DeleteTempList(precardAccessGroupDetailOperationGUID);

                foreach (Request requestItem in requestsList)
                {
                    ManagerFlowConditionPrecardProxy managerFlowConditionPrecardProxy = managerFlowConditionPreacardProxyList.Where(x => x.PrecardID == requestItem.ID).FirstOrDefault();
                    if (this.CheckRequestValueAppliedFlowConditionValue(managerFlowConditionPrecardProxy.ManagerFlowCondition, requestItem))
                        requestStatusIdsList.AddRange(requestItem.RequestStatusList.Select(x => x.ID).ToList<decimal>());
                }

                if (requestStatusIdsList.Count > 0)
                {
                    requestStatusOperationGUID = tempBusiness.InsertTempList(requestStatusIdsList);

                    string SQLCommand = @"delete from TA_RequestStatus 
                                                 inner join TA_Temp
                                                 on reqStat_ID = temp_OperationGUID
                                                 where temp_OperationGUID = :operationGUID";
                    NHSession.CreateSQLQuery(SQLCommand)
                   .SetParameter("operationGUID", requestStatusOperationGUID)
                   .ExecuteUpdate();

                    tempBusiness.DeleteTempList(requestStatusOperationGUID);
                }
            }
            catch (Exception ex)
            {
                LogException(ex, "BManagerFlowCondition", "DeleteSuspendRequestStatesByFlowCondition");
                throw;
            }
        }

        private bool CheckRequestValueAppliedFlowConditionValue(ManagerFlowCondition managerFlowCondition, Request request)
        {
            bool isAppliedFlowConditionValue = false;
            if (managerFlowCondition != null)
            {
                ConditionOperators conditionOperator = (ConditionOperators)managerFlowCondition.Operator;
                Precard precard = managerFlowCondition.PrecardAccessGroupDetail.Precard;
                PrecardGroupsName precardGroupsName = (PrecardGroupsName)managerFlowCondition.PrecardAccessGroupDetail.Precard.PrecardGroup.IntLookupKey;
                int requestValue = 0;
                switch (precardGroupsName)
                {
                    case PrecardGroupsName.leave:
                    case PrecardGroupsName.leaveestelajy:
                    case PrecardGroupsName.duty:
                        if (precard.IsHourly)
                            requestValue = request.TimeDuration;
                        else
                            if (precard.IsDaily)
                                requestValue = (int)(request.ToDate - request.FromDate).TotalDays + 1;
                        if (this.CheckRequestValueIsInRangeConditionValue(conditionOperator, managerFlowCondition.Value, requestValue))
                            isAppliedFlowConditionValue = true;
                        break;
                    case PrecardGroupsName.overwork:
                        requestValue = ((int)(request.ToDate - request.FromDate).TotalDays + 1) * request.TimeDuration;
                        if (this.CheckRequestValueIsInRangeConditionValue(conditionOperator, managerFlowCondition.Value, requestValue))
                            isAppliedFlowConditionValue = true;
                        break;
                    case PrecardGroupsName.traffic:
                        if (request.FromTime != -1000 && request.ToTime != 1000 && request.TimeDuration != 1000)
                            if (this.CheckRequestValueIsInRangeConditionValue(conditionOperator, managerFlowCondition.Value, request.TimeDuration))
                                isAppliedFlowConditionValue = true;
                        break;
                    case PrecardGroupsName.imperative:
                        if (this.CheckRequestValueIsInRangeConditionValue(conditionOperator, managerFlowCondition.Value, request.TimeDuration))
                            isAppliedFlowConditionValue = true;
                        break;
                    default:
                        break;
                }
            }
            return isAppliedFlowConditionValue;
        }


        public bool CheckRequestValueIsInRangeConditionValue(ConditionOperators conditionOperators, string conditionValue, int requestValue)
        {
            bool isInRange = false;
            int conditionVal = 0;
            switch (conditionOperators)
            {
                case ConditionOperators.Equal:
                    conditionVal = int.Parse(conditionValue, CultureInfo.InvariantCulture);
                    if (requestValue == conditionVal || conditionVal == 0)
                        isInRange = true;
                    break;
                case ConditionOperators.NotEqual:
                    conditionVal = int.Parse(conditionValue, CultureInfo.InvariantCulture);
                    if (requestValue != conditionVal || conditionVal == 0)
                        isInRange = true;
                    break;
                case ConditionOperators.GreaterThan:
                    conditionVal = int.Parse(conditionValue, CultureInfo.InvariantCulture);
                    if (requestValue > conditionVal || conditionVal == 0)
                        isInRange = true;
                    break;
                case ConditionOperators.GreaterThanOrEqual:
                    conditionVal = int.Parse(conditionValue, CultureInfo.InvariantCulture);
                    if (requestValue >= conditionVal || conditionVal == 0)
                        isInRange = true;
                    break;
                case ConditionOperators.LessThan:
                    conditionVal = int.Parse(conditionValue, CultureInfo.InvariantCulture);
                    if (requestValue < conditionVal || conditionVal == 0)
                        isInRange = true;
                    break;
                case ConditionOperators.LessThanOrEqual:
                    conditionVal = int.Parse(conditionValue, CultureInfo.InvariantCulture);
                    if (requestValue <= conditionVal || conditionVal == 0)
                        isInRange = true;
                    break;
                case ConditionOperators.Between:
                    string[] conditionValueParts = conditionValue.Split(new char[] { ',' });
                    if (conditionValueParts.Length == 2 && (requestValue >= int.Parse(conditionValueParts[0], CultureInfo.InvariantCulture) && requestValue <= int.Parse(conditionValueParts[1], CultureInfo.InvariantCulture)) || (int.Parse(conditionValueParts[0], CultureInfo.InvariantCulture) == 0 && (int.Parse(conditionValueParts[1], CultureInfo.InvariantCulture) == 0)))
                        isInRange = true;
                    break;
                default:
                    break;
            }

            return isInRange;
        }


        public void DeleteManagerFlowConditionsByManagerFlow(decimal managerFlowID)
        {
            try
            {
                IList<ManagerFlowCondition> managerFlowConditionList = this.NHSession.QueryOver<ManagerFlowCondition>()
                                                                             .Where(x => x.ManagerFlow.ID == managerFlowID)
                                                                             .List<ManagerFlowCondition>();
                foreach (ManagerFlowCondition managerFlowConditionItem in managerFlowConditionList)
                {
                    this.Delete(managerFlowConditionItem);
                }

            }
            catch (Exception ex)
            {
                LogException(ex, "BManagerFlowCondition", "DeleteManagerFlowConditionsByManagerFlow");
                throw;
            }
        }
    }
}
