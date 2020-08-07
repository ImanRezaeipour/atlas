using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Model;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Model.Charts;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Business;
using GTS.Clock.Business.Charts;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Business.Security;
using GTS.Clock.Model.RequestFlow;
using NHibernate;
using NHibernate.Criterion;

namespace GTS.Clock.Business.RequestFlow
{
    public class BPrecardAccessGroup : BaseBusiness<PrecardAccessGroup>
    {
        const string ExceptionSrc = "GTS.Clock.Business.RequestFlow.BPrecardAccessGroup";
        EntityRepository<PrecardAccessGroup> accessGroupRep = new EntityRepository<PrecardAccessGroup>();
        FlowRepository FlowRep = new FlowRepository(false);
        ISession NHSession = NHibernateSessionManager.Instance.GetSession();

        /// <summary>
        /// دلیل استفاده از این سرویس این است که واسط کاربر باید درختی از پیشکارتها را نمایش دهد
        /// و بدلیل محدودیتها جاوا اسکریپت مجبوریم شمای درخت را در سرویس دریافت و آنرا تحلیل کنیم
        /// </summary>
        /// <param name="name">نام گروه دسترسی</param>
        /// <param name="description">توضیح</param>
        /// <param name="accessGroupList">لیست پیکارتها که از درخت استخراج شده است</param>
        public decimal InsertByProxy(string name, string description, IList<AccessGroupProxy> accessGroupList)
        {
            try
            {
                EntityRepository<PrecardGroups> groupRep = new EntityRepository<PrecardGroups>(false);
                IList<Precard> removeList = new List<Precard>();
                PrecardAccessGroup accessGroup = new PrecardAccessGroup();
                accessGroup.Name = name;
                accessGroup.Description = description;
                accessGroup.PrecardList = new List<Precard>();
                foreach (AccessGroupProxy proxy in accessGroupList)
                {
                    if (proxy.IsParent)
                    {
                        PrecardGroups group = groupRep.GetById(proxy.ID, false);
                        foreach (Precard p in group.PrecardList)
                        {
                            accessGroup.PrecardList.Add(p);
                        }
                    }
                    else if (proxy.Checked)
                    {
                        accessGroup.PrecardList.Add(new Precard() { ID = proxy.ID });
                    }
                    else
                    {
                        removeList.Add(new Precard() { ID = proxy.ID });
                    }
                }
                foreach (Precard p in removeList)
                {
                    accessGroup.PrecardList.Remove(p);
                }
                SaveChanges(accessGroup, UIActionType.ADD);
                return accessGroup.ID;
            }
            catch (Exception ex)
            {
                LogException(ex, "BPrecardAccessGroup", "InsertByProxy");
                throw ex;
            }
        }

        public decimal UpdateByProxy(decimal accessGroupId, string name, string description, IList<AccessGroupProxy> accessGroupList, bool updateAccessGroupDetail)
        {
            try
            {
                EntityRepository<PrecardGroups> groupRep = new EntityRepository<PrecardGroups>(false);
                IList<Precard> removeList = new List<Precard>();
                PrecardAccessGroup accessGroup = new PrecardAccessGroup();
                accessGroup = base.GetByID(accessGroupId);
                accessGroup.Name = name;
                accessGroup.Description = description;
                accessGroup.AccessGroupDetailOld = new Dictionary<decimal, decimal>();
                foreach (PrecardAccessGroupDetail accessGroupDetail in accessGroup.PrecardAccessGroupDetailList)
                {
                    accessGroup.AccessGroupDetailOld.Add(accessGroupDetail.Precard.ID, accessGroupDetail.ID);
                }
                if (updateAccessGroupDetail)
                {//اگر این لیست خالی باشد معنایش این است که آیتم های قبلی نباید دست بخورد
                    accessGroup.PrecardList.Clear();
                }

                foreach (AccessGroupProxy proxy in accessGroupList)
                {
                    IList<Precard> precardGroup = new List<Precard>();
                    if (proxy.IsParent)
                    {
                        PrecardGroups group = groupRep.GetById(proxy.ID, false);
                        foreach (Precard p in group.PrecardList)
                        {                            
                            accessGroup.PrecardList.Add(p);
                        }
                    }
                    else if (proxy.Checked)
                    {                        
                        accessGroup.PrecardList.Add(new Precard() { ID = proxy.ID });
                    }
                    else
                    {
                        removeList.Add(new Precard() { ID = proxy.ID });
                    }
                }
                foreach (Precard p in removeList)
                {
                    accessGroup.PrecardList.Remove(p);
                }
                SaveChanges(accessGroup, UIActionType.EDIT);
                return accessGroup.ID;
            }
            catch (Exception ex)
            {
                LogException(ex, "BPrecardAccessGroup", "UpdateByProxy");
                throw ex;
            }
        }

        /// <summary>
        /// همه پیشکارتها را جهت نمایش گروهی برمیگرداند
        /// اگر پارامتر ورودی صفر باشد بدین معنی است که
        /// در مد اینزرت هستیم
        /// </summary>
        /// <param name="accessGroupId"></param>
        /// <returns></returns>
        public IList<PrecardGroups> GetPrecardTree(decimal accessGroupId)
        {
            try
            {
                BPrecard bPrecard = new BPrecard();
                IList<PrecardGroups> groupList = bPrecard.GetAllPrecardGroups();
                if (accessGroupId > 0)
                {
                    PrecardAccessGroup accessGroup = base.GetByID(accessGroupId);

                    foreach (PrecardGroups group in groupList)
                    {
                        int precardCount = group.PrecardList.Count, childCounter = 0;
                        foreach (Precard precard in group.PrecardList)
                        {
                            foreach (Precard p in accessGroup.PrecardList)
                            {
                                if (p.Equals(precard))
                                {
                                    childCounter++;
                                    precard.ContainInPrecardAccessGroup = true;
                                    break;
                                }
                            }
                        }
                        if (precardCount == childCounter && precardCount > 0)
                            group.ContainInPrecardAccessGroup = true;
                    }
                }
                return groupList;
            }
            catch (Exception ex)
            {
                LogException(ex, "BPrecardAccessGroup", "GetPrecardTree");
                throw ex;
            }
        }

        /// <summary>
        /// پیشکارتهای یک گروه را برمیگرداند
        /// </summary>
        /// <param name="accessGroupId"></param>
        /// <returns></returns>
        public IList<Precard> GetPrecardGroupChilds(decimal accessGroupId)
        {
            BPrecard bprecard = new BPrecard();
            return bprecard.GetAllByPrecardGroup(accessGroupId);
        }

        /// <summary>        
        /// نام نباید خالی باشد
        /// نام تکراری نباشد       
        /// لیست جزیات نباید خالی باشد        
        /// </summary>
        /// <param name="precard"></param>
        protected override void InsertValidate(PrecardAccessGroup accessGroup)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            if (Utility.IsEmpty(accessGroup.Name))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.AccessGroupNameRequierd, "نام گروه دسترسی نباید خالی باشد", ExceptionSrc));
            }
            else if (accessGroupRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new Precard().Name), accessGroup.Name)) > 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.AccessGroupNameRepeated, "نام گروه دسترسی نباید تکراری باشد", ExceptionSrc));
            }


            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        /// <summary>
        /// نام نباید خالی باشد
        /// نام تکراری نباشد       
        /// </summary>
        /// <param name="precard"></param>
        protected override void UpdateValidate(PrecardAccessGroup accessGroup)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            if (Utility.IsEmpty(accessGroup.Name))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.AccessGroupNameRequierd, "نام گروه دسترسی نباید خالی باشد", ExceptionSrc));
            }
            else if (accessGroupRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new Precard().Name), accessGroup.Name),
                                                       new CriteriaStruct(Utility.GetPropertyName(() => new Precard().ID), accessGroup.ID, CriteriaOperation.NotEqual)) > 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.AccessGroupNameRepeated, "نام گروه دسترسی نباید تکراری باشد", ExceptionSrc));
            }

            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        /// <summary>
        /// در جریان ها استفاده نشده باشد
        /// </summary>
        /// <param name="obj"></param>
        protected override void DeleteValidate(PrecardAccessGroup accessGroup)
        {
            UIValidationExceptions exception = new UIValidationExceptions();
            FlowRepository flowRep = new FlowRepository(false);
            int flowCount = flowRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new Model.RequestFlow.Flow().AccessGroup), accessGroup));
            if (flowCount > 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.AccessGroupUsedByFlow, "گروه دسترسی در جریان مورد استفاده قرار گرفته است", ExceptionSrc));
            }

            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        /// <summary>
        /// if details list is empty means that list should not be update else 
        /// we should update the list
        /// </summary>
        /// <param name="accessGroup"></param>
        /// <param name="action"></param>
        protected override void GetReadyBeforeSave(PrecardAccessGroup accessGroup, UIActionType action)
        {
            if (action == UIActionType.ADD || action == UIActionType.EDIT)
            {
                if (!Utility.IsEmpty<Precard>(accessGroup.PrecardList))
                {
                    foreach (Precard precard in accessGroup.PrecardList)
                    {
                        precard.AccessGroupList = new List<PrecardAccessGroup>() { accessGroup };
                    }
                }
            }
        }

        protected override void OnSaveChangesSuccess(PrecardAccessGroup obj, UIActionType action)
        {
            if (action == UIActionType.EDIT)
            {
                this.UpdateSubstitutePrecardAccess(obj);
                this.UpdateManagerFlowCondition(obj);
            }


        }
        private void UpdateManagerFlowCondition(PrecardAccessGroup obj)
        {          
            IList<PrecardAccessGroupDetailProxy> AccessGroupDetailProxy = new List<PrecardAccessGroupDetailProxy>();            
            PrecardAccessGroupDetail accessGroupDetailAlias = null;
            ManagerFlowCondition managerFlowConditionAlias = null;            
            IList<PrecardAccessGroupDetail> precardAccessGroupDetailList = NHSession.QueryOver<PrecardAccessGroupDetail>(() => accessGroupDetailAlias)
                                                                                    .Where(() => accessGroupDetailAlias.PrecardAccessGroup.ID == obj.ID)
                                                                                    .List<PrecardAccessGroupDetail>();
            foreach (decimal precardId in obj.AccessGroupDetailOld.Keys)
            {
                PrecardAccessGroupDetailProxy accessGroupDetailProxy = new PrecardAccessGroupDetailProxy();
                accessGroupDetailProxy.PrecardAccessGroupDetailOldId = obj.AccessGroupDetailOld[precardId];               
                accessGroupDetailProxy.PrecardAccessGroupDetailNewId = precardAccessGroupDetailList.Where(x => x.Precard.ID == precardId).Select(x => x.ID).FirstOrDefault();
                AccessGroupDetailProxy.Add(accessGroupDetailProxy);
            }
            IList<decimal> PrecardAccessGroupDetailIds = NHSession.QueryOver<ManagerFlowCondition>(() => managerFlowConditionAlias)
                                                                  .Where(() => managerFlowConditionAlias.PrecardAccessGroupDetail.ID.IsIn(AccessGroupDetailProxy.Select(x => x.PrecardAccessGroupDetailOldId).ToArray()))
                                                                  .Select(y => y.PrecardAccessGroupDetail.ID)
                                                                  .List<decimal>();
            if (PrecardAccessGroupDetailIds.Count != 0)
            {
                AccessGroupDetailProxy = AccessGroupDetailProxy.Where(x => PrecardAccessGroupDetailIds.Contains(x.PrecardAccessGroupDetailOldId)).ToList();
                foreach (PrecardAccessGroupDetailProxy accessGroupDetailProxy in AccessGroupDetailProxy)
               {                 
                  FlowRep.UpdateManagerFlowCondition(accessGroupDetailProxy.PrecardAccessGroupDetailOldId, accessGroupDetailProxy.PrecardAccessGroupDetailNewId);
               }
            }
        }

        public PrecardAccessGroup GetPrecardAccessGroupService()
        {
            PrecardAccessGroup precardAccessGroupObj = null;
            try
            {
                PrecardAccessGroup accessGroupAlias = null;

                precardAccessGroupObj = NHSession.QueryOver<PrecardAccessGroup>(() => accessGroupAlias)
                                                  .Where(() => accessGroupAlias.IsFromService).SingleOrDefault();


            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionSrc, "GetPrecardAccessGroupService");
            }
            return precardAccessGroupObj;
        }
        private void UpdateSubstitutePrecardAccess(PrecardAccessGroup precardAccessGroup)
        {
            try
            {
                Flow flowAlias = null;
                PrecardAccessGroup precardAccessGroupAlias = null;
                IList<decimal> flowIDsList = this.NHSession.QueryOver<Flow>(() => flowAlias)
                                                           .JoinAlias(() => flowAlias.AccessGroup, () => precardAccessGroupAlias)
                                                           .Where(() => precardAccessGroupAlias.ID == precardAccessGroup.ID)
                                                           .Select(x => x.ID)
                                                           .List<decimal>();
                if (flowIDsList.Count() > 0)
                {
                    BSubstitute bSubstitute = new BSubstitute();
                    bSubstitute.UpdateSubstitutePrecardAccessByFlow(flowIDsList);
                }

            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionSrc, "UpdateSubstitutePrecardAccess");
            }
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckAccessGroupsLoadAccess_onOrganizationFlowInsert()
        {
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckAccessGroupsLoadAccess_onOrganizationFlowUpdate()
        {
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertAccessGroup_onOrganizationFlowInsert(string AccessGroupName, string AccessGroupDescription, IList<AccessGroupProxy> AccessLevelsList)
        {
            return this.InsertByProxy(AccessGroupName, AccessGroupDescription, AccessLevelsList);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertAccessGroup_onOrganizationFlowUpdate(string AccessGroupName, string AccessGroupDescription, IList<AccessGroupProxy> AccessLevelsList)
        {
            return this.InsertByProxy(AccessGroupName, AccessGroupDescription, AccessLevelsList);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal UpdateAccessGroup_onOrganizationFlowInsert(decimal selectedAccessGroupID, string AccessGroupName, string AccessGroupDescription, IList<AccessGroupProxy> AccessLevelsList, bool IsUpdateAccessLevelsList)
        {
            return this.UpdateByProxy(selectedAccessGroupID, AccessGroupName, AccessGroupDescription, AccessLevelsList, IsUpdateAccessLevelsList);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal UpdateAccessGroup_onOrganizationFlowUpdate(decimal selectedAccessGroupID, string AccessGroupName, string AccessGroupDescription, IList<AccessGroupProxy> AccessLevelsList, bool IsUpdateAccessLevelsList)
        {
            return this.UpdateByProxy(selectedAccessGroupID, AccessGroupName, AccessGroupDescription, AccessLevelsList, IsUpdateAccessLevelsList);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal DeleteAccessGroup_onOrganizationFlowInsert(PrecardAccessGroup accessGroup, UIActionType UAT)
        {
            return base.SaveChanges(accessGroup, UAT);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal DeleteAccessGroup_onOrganizationFlowUpdate(PrecardAccessGroup accessGroup, UIActionType UAT)
        {
            return base.SaveChanges(accessGroup, UAT);
        }





    }
}
