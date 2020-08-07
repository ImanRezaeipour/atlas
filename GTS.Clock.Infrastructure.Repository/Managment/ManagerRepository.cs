using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using System.Linq;
using System.Collections;
using GTS.Clock.Model.RequestFlow;
using GTS.Clock.Model.Charts;
using GTS.Clock.Infrastructure.RepositoryFramework;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Model.MonthlyReport;
using GTS.Clock.Model;
using NHibernate.Transform;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Model.Security;

namespace GTS.Clock.Infrastructure.Repository
{
    public class ManagerRepository : RepositoryBase<Manager>, IManagerRepository
    {
        public override string TableName
        {
            get { return "TA_Manager"; }
        }

        public ManagerRepository(bool disconnectly)
            : base(disconnectly)
        {
        }
        private ISession NHSession = NHibernateSessionManager.Instance.GetSession();
        int operationBatchSizeValue = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings[OperationBatchSize.OperationBatchSizeKey.ToString()]);
        #region IManagerRepository Members

        public IList<Manager> GetAllByPage(int pageIndex, int pageSize, decimal[] restirictionIds)
        {
            IList<Manager> entities = new List<Manager>();
            try
            {
                decimal[] accessableIDs = restirictionIds;
                IList<ManagerFlow> list = new List<ManagerFlow>();
                if (accessableIDs.Count() < this.operationBatchSizeValue && this.operationBatchSizeValue < 2100)
                {

                    entities = base.GetByCriteriaByPage(pageIndex, pageSize,
                        new CriteriaStruct(Utility.Utility.GetPropertyName(() => new Manager().Active), true),
                        new CriteriaStruct(Utility.Utility.GetPropertyName(() => new Manager().ID), restirictionIds, CriteriaOperation.IN));
                }
                else
                {
                    TempRepository tempRep = new TempRepository(false);
                    string operationGUID = tempRep.InsertTempList(accessableIDs);

                    Manager managerAlias = null;
                    GTS.Clock.Model.Temp.Temp tempAlias = null;

                    entities = NHSession.QueryOver(() => managerAlias)
                                                      .JoinAlias(() => managerAlias.TempList, () => tempAlias)
                                                      .Where(() => tempAlias.OperationGUID == operationGUID && managerAlias.Active == true)
                                                      .Skip(pageIndex * pageSize).Take(pageSize).List<Manager>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return entities;
        }

        public Manager IsManager(string username)
        {
            string HQLCommand = @"from Manager where Active=1 AND Person.ID in
                        (select Person from User where UserName=:username)";
            IList<Manager> list = base.NHibernateSession.CreateQuery(HQLCommand)
                .SetParameter("username", username)
                .List<Manager>();
            if (list != null && list.Count > 0)
                return list[0];

            HQLCommand = @"from Manager where Active=1 AND OrganizationUnit.ID in (
                        select ID from OrganizationUnit where Person.ID in (
                        select Person from User where UserName=:username))";
            list = base.NHibernateSession.CreateQuery(HQLCommand)
                .SetParameter("username", username)
                .List<Manager>();
            if (list != null && list.Count > 0)
                return list[0];
            return new Manager();
        }

        public IList<UnderManagment> GetAllUnderManagments(decimal managerId)
        {
            string HQLCommand = @"select unmngt  from UnderManagment as unmngt inner join
                                  unmngt.Flow as flw 
                                  inner join flw.ManagerFlowList as mngList
                                  where mngList=:managerId and flw.IsDeleted = 0";
            IList<UnderManagment> list = base.NHibernateSession.CreateQuery(HQLCommand)
                .SetParameter("managerId", managerId)
                .List<UnderManagment>();
            return list;
        }

        public int GetQuickSearchCount(string searchKey, DataAccessLevelsType Dalt, bool isSystemTechnicalAdmin, decimal currentUserID)
        {
            QueryOver<DAManager, DAManager> dataAccessSubQuery = null;
            Manager managerAlias = null;
            DAManager daManagerAlias = null;
            string strManagerIDList = "(";
            string managerSeparator = ", ";

            string HQLCommand = @"select count(mngr) from Manager mngr
                                 left outer join mngr.Person prs 
                                 left outer join mngr.OrganizationUnit.Person organPrs 
                                 left outer join mngr.Person.OrganizationUnitList prsOrgan 
                                 where mngr.Active=1 AND 
                                 (prs.FirstName + ' ' + prs.LastName like :key OR
                                 organPrs.FirstName + ' ' + organPrs.LastName like :key OR
                                 prs.BarCode like :key OR
                                 prsOrgan.Name like :key)";

            if (Dalt == DataAccessLevelsType.Source && !isSystemTechnicalAdmin)
            {
                dataAccessSubQuery = QueryOver.Of<DAManager>(() => daManagerAlias)
                                              .Where(() => daManagerAlias.ManagerID == managerAlias.ID || daManagerAlias.All)
                                              .And(() => daManagerAlias.UserID == currentUserID)
                                              .Select(x => x.ID);
                IList<decimal> managerIDList = this.NHSession.QueryOver<Manager>(() => managerAlias)
                                                             .WithSubquery
                                                             .WhereExists(dataAccessSubQuery)
                                                             .Select(x => x.ID)
                                                             .List<decimal>();
                if (managerIDList.Count > 0)
                {
                    for (int i = 0; i < managerIDList.Count; i++)
                    {
                        if (i == managerIDList.Count - 1)
                            managerSeparator = ")";
                        strManagerIDList += managerIDList[i].ToString() + managerSeparator;
                    }
                    HQLCommand += " AND mngr.ID IN " + strManagerIDList;
                }
            }

            object count = base.NHibernateSession.CreateQuery(HQLCommand)
                .SetParameter("key", "%" + searchKey + "%")
                .List<object>().First();
            return Utility.Utility.ToInteger(count.ToString());
        }

        public int GetSearchCountByPersonName(string personName, decimal[] restrictIds)
        {
            if (restrictIds == null || restrictIds.Length == 0)
                return 0;
            decimal[] accessableIDs = restrictIds;
            object count = new object();
            if (accessableIDs.Count() < this.operationBatchSizeValue && this.operationBatchSizeValue < 2100)
            {
                string HQLCommand = @"select count(mngr)  from Manager mngr
                                left outer join mngr.Person prs 
                                left outer join mngr.OrganizationUnit.Person organPrs 
                                where (prs.FirstName + ' ' + prs.LastName like :personName OR
                                        organPrs.FirstName + ' ' + organPrs.LastName like :personName ) 
                                AND mngr.ID in (:ids)
                                AND mngr.Active =1 ";
                count = base.NHibernateSession.CreateQuery(HQLCommand)
                   .SetParameter("personName", "%" + personName + "%")
                   .SetParameterList("ids", base.CheckListParameter(restrictIds))
                   .List<object>().First();
            }
            else
            {
                TempRepository tempRep = new TempRepository(false);
                string operationGUID = tempRep.InsertTempList(accessableIDs);
                string HQLCommand = @"select count(mngr)  from Manager mngr
                                left outer join mngr.Person prs 
                                left outer join mngr.OrganizationUnit.Person organPrs 
                                INNER JOIN mngr.TempList tmp
                                where tmp.OperationGUID = :operationGUID and (prs.FirstName + ' ' + prs.LastName like :personName OR
                                        organPrs.FirstName + ' ' + organPrs.LastName like :personName ) 
                                AND mngr.Active =1 ";
                count = base.NHibernateSession.CreateQuery(HQLCommand)
                   .SetParameter("personName", "%" + personName + "%")
                   .SetParameter("operationGUID", operationGUID)
                   .List<object>().First();
                tempRep.DeleteTempList(operationGUID);
            }
            return Utility.Utility.ToInteger(count.ToString());
        }

        public int GetSearchCountByPersonCode(string personCode, decimal[] restrictIds)
        {
            if (restrictIds == null || restrictIds.Length == 0)
                return 0;
            decimal[] accessableIDs = restrictIds;
            object count = new object();
            if (accessableIDs.Count() < this.operationBatchSizeValue && this.operationBatchSizeValue < 2100)
            {

                string HQLCommand = @"select count(mngr)  from Manager mngr
                                    left outer join mngr.Person prs 
                                    left outer join mngr.OrganizationUnit.Person organPrs 
                                    where (prs.BarCode like :personCode or organPrs.BarCode like :personCode )
                                    AND mngr.ID IN (:ids) 
                                    AND mngr.Active =1 ";
                count = base.NHibernateSession.CreateQuery(HQLCommand)
                   .SetParameter("personCode", "%" + personCode + "%")
                   .SetParameterList("ids", base.CheckListParameter(restrictIds))
                   .List<object>().First();
            }
            else
            {
                TempRepository tempRep = new TempRepository(false);
                string operationGUID = tempRep.InsertTempList(accessableIDs);

                string HQLCommand = @"select count(mngr)  from Manager mngr
                                    left outer join mngr.Person prs 
                                    left outer join mngr.OrganizationUnit.Person organPrs 
                                    INNER JOIN mngr.TempList tmp
                                    where tmp.OperationGUID = :operationGUID and (prs.BarCode like :personCode or organPrs.BarCode like :personCode )
                                    AND mngr.Active =1 ";
                count = base.NHibernateSession.CreateQuery(HQLCommand)
                   .SetParameter("personCode", "%" + personCode + "%")
                   .SetParameter("operationGUID", operationGUID)
                   .List<object>().First();

                tempRep.DeleteTempList(operationGUID);
            }
            return Utility.Utility.ToInteger(count.ToString());
        }

        public int GetSearchCountByOrganName(string organName, decimal[] restrictIds)
        {
            if (restrictIds == null || restrictIds.Length == 0)
                return 0;
            decimal[] accessableIDs = restrictIds;
            object count = new object();
            if (accessableIDs.Count() < this.operationBatchSizeValue && this.operationBatchSizeValue < 2100)
            {

                string HQLCommand = @"select count(mngr)  from Manager mngr
                                      left outer join mngr.Person.OrganizationUnitList prsOrgan 
                                      left outer join mngr.OrganizationUnit organ1 
                                      where (organ1.Name like :organName OR prsOrgan.Name like :organName )
                                      AND mngr.ID IN (:ids) 
                                      AND mngr.Active =1 ";
                count = base.NHibernateSession.CreateQuery(HQLCommand)
                                              .SetParameter("organName", "%" + organName + "%")
                                              .SetParameterList("ids", base.CheckListParameter(restrictIds))
                                              .List<object>().First();
            }
            else
            {
                TempRepository tempRep = new TempRepository(false);
                string operationGUID = tempRep.InsertTempList(accessableIDs);

                string HQLCommand = @"select count(mngr)  from Manager mngr
                                      left outer join mngr.Person.OrganizationUnitList prsOrgan 
                                      left outer join mngr.OrganizationUnit organ1 
                                      INNER JOIN mngr.TempList tmp 
		                              where tmp.OperationGUID = :operationGUID and  (organ1.Name like :organName OR prsOrgan.Name like :organName) 
                                      AND mngr.Active =1 ";
                count = base.NHibernateSession.CreateQuery(HQLCommand)
                                              .SetParameter("organName", "%" + organName + "%")
                                              .SetParameter("operationGUID", operationGUID)
                                              .List<object>().First();
                tempRep.DeleteTempList(operationGUID);
            }
            return Utility.Utility.ToInteger(count.ToString());
        }

        public int GetSearchCountByQuickSearch(string searchKey, decimal[] restrictIds)
        {
            if (restrictIds == null || restrictIds.Length == 0)
                return 0;
            decimal[] accessableIDs = restrictIds;
            object count = new object();
            if (accessableIDs.Count() < this.operationBatchSizeValue && this.operationBatchSizeValue < 2100)
            {

                string HQLCommand = @"select count(mngr)  from Manager mngr
            left outer join mngr.Person.OrganizationUnitList prsOrgan 
            left outer join mngr.OrganizationUnit organ1 
            left outer join mngr.Person prs 
            left outer join mngr.OrganizationUnit.Person organPrs 
            where (organ1.Name like :organName OR prsOrgan.Name like :organName 
                   OR 
                   prs.FirstName + ' ' + prs.LastName like :organName OR
                                        organPrs.FirstName + ' ' + organPrs.LastName like :organName 
                   OR
                   prs.BarCode =:organName or organPrs.BarCode =:organName )
            AND mngr.ID IN (:ids)  
            AND mngr.Active =1 ";
                count = base.NHibernateSession.CreateQuery(HQLCommand)
                   .SetParameter("organName", "%" + searchKey + "%")
                   .SetParameterList("ids", base.CheckListParameter(restrictIds))
                   .List<object>().First();
            }
            else
            {
                TempRepository tempRep = new TempRepository(false);
                string operationGUID = tempRep.InsertTempList(accessableIDs);
                string HQLCommand = @"select count(mngr)  from Manager mngr
            left outer join mngr.Person.OrganizationUnitList prsOrgan 
            left outer join mngr.OrganizationUnit organ1 
            left outer join mngr.Person prs 
            left outer join mngr.OrganizationUnit.Person organPrs 
            INNER JOIN mngr.TempList tmp
            where tmp.OperationGUID = :operationGUID and (organ1.Name like :organName OR prsOrgan.Name like :organName 
                   OR 
                   prs.FirstName + ' ' + prs.LastName like :organName OR
                                        organPrs.FirstName + ' ' + organPrs.LastName like :organName 
                   OR
                   prs.BarCode =:organName or organPrs.BarCode =:organName )
            AND mngr.Active =1 ";
                count = base.NHibernateSession.CreateQuery(HQLCommand)
                   .SetParameter("organName", "%" + searchKey + "%")
                   .SetParameter("operationGUID", operationGUID)
                   .List<object>().First();
                tempRep.DeleteTempList(operationGUID);
            }
            return Utility.Utility.ToInteger(count.ToString());
        }


        public int GetSearchCountByAccessGroupID(decimal accessGroupID, decimal[] restrictIds)
        {
            if (restrictIds == null || restrictIds.Length == 0)
                return 0;
            decimal[] accessableIDs = restrictIds;
            IList<ManagerFlow> list = new List<ManagerFlow>();
            if (accessableIDs.Count() < this.operationBatchSizeValue && this.operationBatchSizeValue < 2100)
            {
                string HQLCommand = @"SELECT mngrList from Flow flw
                                      JOIN flw.ManagerFlowList mngrList
                                      WHERE flw.AccessGroup =:accessGroup 
                                      AND flw.IsDeleted = 0
                                      AND mngrList.Manager.ID in (:ids)
                                      AND mngrList.Manager.Active = 1";
                list = base.NHibernateSession.CreateQuery(HQLCommand)
                                             .SetParameter("accessGroup", new PrecardAccessGroup() { ID = accessGroupID })
                                             .SetParameterList("ids", base.CheckListParameter(restrictIds))
                                             .List<ManagerFlow>();
            }
            else
            {
                TempRepository tempRep = new TempRepository(false);
                string operationGUID = tempRep.InsertTempList(accessableIDs);
                string HQLCommand = @"SELECT mngrList from Flow flw
                                      JOIN flw.ManagerFlowList mngrList
                                      INNER JOIN mngrList.Manager.TempList tmp
                                      WHERE tmp.OperationGUID= :operationGUID and flw.AccessGroup =:accessGroup 
                                      AND flw.IsDeleted = 0
                                      AND mngrList.Manager.Active = 1";
                list = base.NHibernateSession.CreateQuery(HQLCommand)
                                             .SetParameter("accessGroup", new PrecardAccessGroup() { ID = accessGroupID })
                                             .SetParameter("operationGUID", operationGUID)
                                             .List<ManagerFlow>();
                tempRep.DeleteTempList(operationGUID);
            }

            int count = 0;
            var managers = from n in list
                           group n by n.Manager into g
                           select g;
            count = managers.Count();
            return count;

        }

        /// <summary>
        /// جست و جو برروی نام مدیر و نام پست سازمانی و شماره پرسنلی
        /// </summary>
        /// <param name="searchKey"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public IList<Manager> GetQuickSearch(string searchKey, int pageSize, int pageIndex, bool isSystemTechnicalAdmin, decimal currentUserID)
        {
            QueryOver<DAManager, DAManager> dataAccessSubQuery = null;
            Manager managerAlias = null;
            DAManager daManagerAlias = null;
            string strManagerIDList = "(";
            string managerSeparator = ", ";
            string HQLCommand = @"select mngr  from Manager mngr
                                 left outer join mngr.Person prs 
                                 left outer join mngr.OrganizationUnit.Person organPrs 
                                 left outer join mngr.Person.OrganizationUnitList prsOrgan 
                                 where mngr.Active=1 AND (
                                 prs.FirstName + ' ' + prs.LastName like :key OR
                                 organPrs.FirstName + ' ' + organPrs.LastName like :key OR
                                 prs.BarCode like :key  OR 
                                 prsOrgan.Name like :key )  ";

            if (!isSystemTechnicalAdmin)
            {
                dataAccessSubQuery = QueryOver.Of<DAManager>(() => daManagerAlias)
                                              .Where(() => daManagerAlias.ManagerID == managerAlias.ID || daManagerAlias.All)
                                              .And(() => daManagerAlias.UserID == currentUserID)
                                              .Select(x => x.ID);
                IList<decimal> managerIDList = this.NHSession.QueryOver<Manager>(() => managerAlias)
                                                             .WithSubquery
                                                             .WhereExists(dataAccessSubQuery)
                                                             .Select(x => x.ID)
                                                             .List<decimal>();
                if (managerIDList.Count > 0)
                {
                    for (int i = 0; i < managerIDList.Count; i++)
                    {
                        if (i == managerIDList.Count - 1)
                            managerSeparator = ")";
                        strManagerIDList += managerIDList[i].ToString() + managerSeparator;
                    }
                    HQLCommand += " AND mngr.ID IN " + strManagerIDList;
                }
            }

            IList<Manager> list = base.NHibernateSession.CreateQuery(HQLCommand)
                .SetParameter("key", "%" + searchKey + "%")
                .SetFirstResult(pageIndex * pageSize).SetMaxResults(pageSize)
                .List<Manager>();
            return list;
        }

        public IList<Manager> GetSearchByPersonName(string personName, int pageSize, int pageIndex, decimal[] restrictIds)
        {
            if (restrictIds == null || restrictIds.Length == 0)
                return new List<Manager>();
            decimal[] accessableIDs = restrictIds;
            IList<Manager> list = new List<Manager>();
            if (accessableIDs.Count() < this.operationBatchSizeValue && this.operationBatchSizeValue < 2100)
            {

                string HQLCommand = @"select mngr  from Manager mngr
                                      left outer join mngr.Person prs 
                                      left outer join mngr.OrganizationUnit.Person organPrs 
                                      where (prs.FirstName + ' ' + prs.LastName like :personName OR
                                             organPrs.FirstName + ' ' + organPrs.LastName like :personName  )
                                             AND mngr.ID in (:ids)
                                             AND mngr.Active =1 ";
                list = base.NHibernateSession.CreateQuery(HQLCommand)
                                             .SetParameter("personName", "%" + personName + "%")
                                             .SetParameterList("ids", base.CheckListParameter(restrictIds))
                                             .SetFirstResult(pageIndex * pageSize).SetMaxResults(pageSize)
                                             .List<Manager>();
            }
            else
            {
                TempRepository tempRep = new TempRepository(false);
                string operationGUID = tempRep.InsertTempList(accessableIDs);
                string HQLCommand = @"select mngr  from Manager mngr
                                      left outer join mngr.Person prs 
                                      left outer join mngr.OrganizationUnit.Person organPrs 
                                      INNER JOIN mngr.TempList tmp
                                      where tmp.OperationGUID= :operationGUID and (prs.FirstName + ' ' + prs.LastName like :personName OR
                                            organPrs.FirstName + ' ' + organPrs.LastName like :personName)          
                                            AND mngr.Active =1 ";
                list = base.NHibernateSession.CreateQuery(HQLCommand)
                .SetParameter("personName", "%" + personName + "%")
                .SetParameter("operationGUID", operationGUID)
                .SetFirstResult(pageIndex * pageSize).SetMaxResults(pageSize)
                .List<Manager>();
                tempRep.DeleteTempList(operationGUID);
            }
            return list;
        }

        public IList<Manager> GetSearchByPersonCode(string personCode, int pageSize, int pageIndex, decimal[] restrictIds)
        {
            if (restrictIds == null || restrictIds.Length == 0)
                return new List<Manager>();
            decimal[] accessableIDs = restrictIds;
            IList<Manager> list = new List<Manager>();
            if (accessableIDs.Count() < this.operationBatchSizeValue && this.operationBatchSizeValue < 2100)
            {
                TempRepository tempRep = new TempRepository(false);
                string operationGUID = tempRep.InsertTempList(accessableIDs);

                string HQLCommand = @"select mngr  from Manager mngr
                                      left outer join mngr.Person prs 
                                      left outer join mngr.OrganizationUnit.Person organPrs 
                                      where (prs.BarCode like :personCode or organPrs.BarCode like :personCode )
                                      AND mngr.ID in (:ids)
                                      AND mngr.Active =1 ";

                list = base.NHibernateSession.CreateQuery(HQLCommand)
                                             .SetParameter("personCode", "%" + personCode + "%")
                                             .SetParameterList("ids", base.CheckListParameter(restrictIds))
                                             .SetFirstResult(pageIndex * pageSize).SetMaxResults(pageSize)
                                             .List<Manager>();
                tempRep.DeleteTempList(operationGUID);
            }
            else
            {
                TempRepository tempRep = new TempRepository(false);
                string operationGUID = tempRep.InsertTempList(accessableIDs);
                string HQLCommand = @"select mngr  from Manager mngr
                                      left outer join mngr.Person prs 
                                      left outer join mngr.OrganizationUnit.Person organPrs 
                                      INNER JOIN mngr.TempList tmp
                                      where tmp.OperationGUID= :operationGUID and (prs.BarCode like :personCode or organPrs.BarCode like :personCode )
                                      AND mngr.Active =1 ";
                list = base.NHibernateSession.CreateQuery(HQLCommand)
                                             .SetParameter("personCode", "%" + personCode + "%")
                                             .SetParameter("operationGUID", operationGUID)
                                             .SetFirstResult(pageIndex * pageSize).SetMaxResults(pageSize)
                                             .List<Manager>();
                tempRep.DeleteTempList(operationGUID);
            }
            return list;
        }

        public IList<Manager> GetSearchByOrganName(string organName, int pageSize, int pageIndex, decimal[] restrictIds)
        {
            if (restrictIds == null || restrictIds.Length == 0)
                return new List<Manager>();
            decimal[] accessableIDs = restrictIds;
            IList<Manager> list = new List<Manager>();
            if (accessableIDs.Count() < this.operationBatchSizeValue && this.operationBatchSizeValue < 2100)
            {

                string HQLCommand = @"select mngr from Manager mngr
                                      left outer join mngr.Person.OrganizationUnitList prsOrgan 
                                      left outer join mngr.OrganizationUnit organ1 
                                      where (organ1.Name like :organName OR prsOrgan.Name like :organName )
                                      AND mngr.ID in (:ids)
                                      AND mngr.Active =1 ";
                list = base.NHibernateSession.CreateQuery(HQLCommand)
                                             .SetParameter("organName", "%" + organName + "%")
                                             .SetParameterList("ids", base.CheckListParameter(restrictIds))
                                             .SetFirstResult(pageIndex * pageSize).SetMaxResults(pageSize)
                                             .List<Manager>();
            }
            else
            {
                TempRepository tempRep = new TempRepository(false);
                string operationGUID = tempRep.InsertTempList(accessableIDs);

                string HQLCommand = @"select mngr from Manager mngr
                                      left outer join mngr.Person.OrganizationUnitList prsOrgan 
                                      left outer join mngr.OrganizationUnit organ1 
                                      INNER JOIN mngr.TempList tmp
                                      where tmp.OperationGUID= :operationGUID and (organ1.Name like :organName OR prsOrgan.Name like :organName )
                                      AND mngr.Active =1 ";
                list = base.NHibernateSession.CreateQuery(HQLCommand)
                                             .SetParameter("organName", "%" + organName + "%")
                                             .SetParameter("operationGUID", operationGUID)
                                             .SetFirstResult(pageIndex * pageSize).SetMaxResults(pageSize)
                                             .List<Manager>();
                tempRep.DeleteTempList(operationGUID);
            }
            return list;
        }

        public IList<Manager> GetSearchByQucikSearch(string searchKey, int pageSize, int pageIndex, decimal[] restrictIds)
        {
            if (restrictIds == null || restrictIds.Length == 0)
                return new List<Manager>();
            decimal[] accessableIDs = restrictIds;
            IList<Manager> list = new List<Manager>();
            if (accessableIDs.Count() < this.operationBatchSizeValue && this.operationBatchSizeValue < 2100)
            {

                string HQLCommand = @"select mngr from Manager mngr
            left outer join mngr.Person.OrganizationUnitList prsOrgan 
            left outer join mngr.OrganizationUnit organ1 
            left outer join mngr.Person prs 
            left outer join mngr.OrganizationUnit.Person organPrs 
            where (organ1.Name like :organName OR prsOrgan.Name like :organName 
                   OR 
                   prs.FirstName + ' ' + prs.LastName like :organName OR
                                        organPrs.FirstName + ' ' + organPrs.LastName like :organName 
                   OR
                   prs.BarCode =:organName or organPrs.BarCode =:organName )
            AND mngr.ID IN (:ids) 
            AND mngr.Active =1 ";
                list = base.NHibernateSession.CreateQuery(HQLCommand)
                    .SetParameter("organName", "%" + searchKey + "%")
                    .SetParameterList("ids", base.CheckListParameter(restrictIds))
                    .SetFirstResult(pageIndex * pageSize).SetMaxResults(pageSize)
                    .List<Manager>();
            }
            else
            {
                TempRepository tempRep = new TempRepository(false);
                string operationGUID = tempRep.InsertTempList(accessableIDs);
                string HQLCommand = @"select mngr from Manager mngr
            left outer join mngr.Person.OrganizationUnitList prsOrgan 
            left outer join mngr.OrganizationUnit organ1 
            left outer join mngr.Person prs 
            left outer join mngr.OrganizationUnit.Person organPrs 
            INNER JOIN mngr.TempList tmp
            where tmp.OperationGUID= :operationGUID and (organ1.Name like :organName OR prsOrgan.Name like :organName 
                   OR 
                   prs.FirstName + ' ' + prs.LastName like :organName OR
                                        organPrs.FirstName + ' ' + organPrs.LastName like :organName 
                   OR
                   prs.BarCode =:organName or organPrs.BarCode =:organName )
            AND mngr.Active =1 ";
                list = base.NHibernateSession.CreateQuery(HQLCommand)
                    .SetParameter("organName", "%" + searchKey + "%")
                    .SetParameter("operationGUID", operationGUID)
                    .SetFirstResult(pageIndex * pageSize).SetMaxResults(pageSize)
                    .List<Manager>();
                tempRep.DeleteTempList(operationGUID);
            }
            return list;
        }

        public IList<Manager> GetSearchByAccessGroupID(decimal accessGroupID, int pageSize, int pageIndex, decimal[] restrictIds, out int managerCount)
        {
            if (restrictIds == null || restrictIds.Length == 0)
            {
                managerCount = 0;
                return new List<Manager>();
            }

            decimal[] accessableIDs = restrictIds;
            Manager managerAlias = null;
            ManagerFlow managerFlowAlias = null;
            Flow flowAlias = null;
            PrecardAccessGroup precardAccessGroupAlias = null;
            GTS.Clock.Model.Temp.Temp tempAlias = null;
            IList<Manager> managersList = new List<Manager>();

            if (accessableIDs.Count() < this.operationBatchSizeValue && this.operationBatchSizeValue < 2100)
            {
                if (accessGroupID != 0)
                {
                    managersList = this.NHSession.QueryOver<Manager>(() => managerAlias)
                                                 .JoinAlias(() => managerAlias.ManagerFlowList, () => managerFlowAlias)
                                                 .JoinAlias(() => managerFlowAlias.Flow, () => flowAlias)
                                                 .JoinAlias(() => flowAlias.AccessGroup, () => precardAccessGroupAlias)
                                                 .Where(() => !flowAlias.IsDeleted &&
                                                               managerAlias.Active &&
                                                               managerAlias.ID.IsIn(accessableIDs) &&
                                                               precardAccessGroupAlias.ID == accessGroupID
                                                       )
                                                 .List<Manager>()
                                                 .Distinct(new ManagerComparer())
                                                 .ToList<Manager>();
                }
                else
                {
                    managersList = this.NHSession.QueryOver<Manager>(() => managerAlias)
                                                 .JoinAlias(() => managerAlias.ManagerFlowList, () => managerFlowAlias)
                                                 .JoinAlias(() => managerFlowAlias.Flow, () => flowAlias)
                                                 .Where(() => !flowAlias.IsDeleted &&
                                                               managerAlias.Active &&
                                                               managerAlias.ID.IsIn(accessableIDs)
                                                       )
                                                 .List<Manager>()
                                                 .Distinct(new ManagerComparer())
                                                 .ToList<Manager>();

                }
            }
            else
            {
                TempRepository tempRep = new TempRepository(false);
                string operationGUID = tempRep.InsertTempList(accessableIDs);
                if (accessGroupID != 0)
                {
                    managersList = this.NHSession.QueryOver<Manager>(() => managerAlias)
                                         .JoinAlias(() => managerAlias.TempList, () => tempAlias)
                                         .JoinAlias(() => managerAlias.ManagerFlowList, () => managerFlowAlias)
                                         .JoinAlias(() => managerFlowAlias.Flow, () => flowAlias)
                                         .JoinAlias(() => flowAlias.AccessGroup, () => precardAccessGroupAlias)
                                         .Where(() => !flowAlias.IsDeleted &&
                                                       managerAlias.Active &&
                                                       managerAlias.ID.IsIn(accessableIDs) &&
                                                       precardAccessGroupAlias.ID == accessGroupID &&
                                                       tempAlias.OperationGUID == operationGUID
                                               )
                                         .List<Manager>()
                                         .Distinct(new ManagerComparer())
                                         .ToList<Manager>();
                }
                else
                {
                    managersList = this.NHSession.QueryOver<Manager>(() => managerAlias)
                                        .JoinAlias(() => managerAlias.TempList, () => tempAlias)
                                        .JoinAlias(() => managerAlias.ManagerFlowList, () => managerFlowAlias)
                                        .JoinAlias(() => managerFlowAlias.Flow, () => flowAlias)
                                        .Where(() => !flowAlias.IsDeleted &&
                                                      managerAlias.Active &&
                                                      managerAlias.ID.IsIn(accessableIDs) &&
                                                      tempAlias.OperationGUID == operationGUID
                                              )
                                        .List<Manager>()
                                        .Distinct(new ManagerComparer())
                                        .ToList<Manager>();
                }
                tempRep.DeleteTempList(operationGUID);
            }
            managerCount = managersList.Count();
            managersList = managersList.Skip(pageIndex * pageSize)
                                       .Take(pageSize)
                                       .ToList<Manager>();
            return managersList;
        }

        public IList<Manager> GetFlowManagers(decimal flowId)
        {
            //if (restrictIds == null || restrictIds.Length == 0)
            //    return new List<Manager>();
            string HQLCommand = @"select mngr  from ManagerFlow mngrFlow
            join mngrFlow.Manager mngr
            where mngrFlow.Flow=:flow
            and mngrFlow.Flow.IsDeleted = 0           
            and mngr.Active=1
            and mngrFlow.Active=1
            order by mngrFlow.Level";
            IList<Manager> list = base.NHibernateSession.CreateQuery(HQLCommand)
                .SetParameter("flow", new Flow() { ID = flowId })
                //.SetParameterList("ids", base.CheckListParameter(restrictIds))
                .List<Manager>();
            return list;
        }

        /// <summary>
        /// یک مدیر زا با توجه به پست سازمانی بین مدیران و پرسنل جستجو میکند
        /// اگر مدیر با شناسه پرسنل ذخیره شده بود آنرا با پست سازمانی ذخیره میکند
        /// </summary>
        /// <param name="organID"></param>
        /// <returns></returns>
        public Manager GetManagerByOrganID(decimal organID)
        {
            string HQLCommand = @"select mngr from Manager mngr
            left outer join mngr.Person.OrganizationUnitList prsOrgan 
            left outer join mngr.OrganizationUnit organ1 
            where mngr.Active=1 AND (organ1.ID = :organID OR prsOrgan.ID = :organID )";
            IList<Manager> list = base.NHibernateSession.CreateQuery(HQLCommand)
                .SetParameter("organID", organID)
                .List<Manager>();
            Manager manager = list.FirstOrDefault();
            if (manager != null && manager.ID > 0 && manager.OrganizationUnit == null)
            {
                OrganizationUnitRepository organRep = new OrganizationUnitRepository(false);
                OrganizationUnit unit = organRep.GetByPersonId(manager.Person.ID);
                if (unit != null && unit.ID > 0)
                {
                    manager.OrganizationUnit = unit;
                    manager.Person = null;
                    base.Update(manager);
                }
            }
            return manager;
        }


        public Manager GetManagerByPersonID(decimal personID, params object[] flowActive)
        {
            string HQLCommand = string.Empty;

            if (flowActive.Count() > 0 && (bool)flowActive[0] == false)
            {
                HQLCommand = @"select mngr  from Manager mngr
                                  left outer join mngr.Person prs 
                                  left outer join mngr.OrganizationUnit.Person organPrs    
                                  inner join mngr.ManagerFlowList mngFlow 
                                  inner join mngFlow.Flow flow
                                  where  mngr.Active=1 AND (prs.ID = :personID or organPrs.ID = :personID ) AND  flow.IsDeleted = 0";
            }


            else
            {
                HQLCommand = @"select mngr  from Manager mngr
                                              left outer join mngr.Person prs 
                                              left outer join mngr.OrganizationUnit.Person organPrs    
                                              inner join mngr.ManagerFlowList mngFlow 
                                              inner join mngFlow.Flow flow
                                              where  mngr.Active=1 AND (prs.ID = :personID or organPrs.ID = :personID ) AND flow.ActiveFlow = 1 and flow.IsDeleted = 0";
            }
            IList<Manager> list = base.NHibernateSession.CreateQuery(HQLCommand)
                                      .SetParameter("personID", personID)
                                      .List<Manager>();
            return list.FirstOrDefault();
        }      
        public IList<UnderManagementPerson> GetUnderManagmentByDepartment(GridSearchFields SearchField, decimal personId, decimal departmentID, string personName, string personbarCode, int dateRangeOrder, int dateRangeOrderIndex, string CurrentDateTime, GridOrderFields order, GridOrderFieldType orderType, int pageIndex, int pageSize)
        {
            string orderingType = orderType.ToString();
            string orderingField = order.ToString();
            return NHibernateSession.GetNamedQuery("GetUnderManagementPersonList")
                                    .SetParameter("personId", personId)
                                    .SetParameter("departmentId", departmentID)
                                    .SetParameter("searchField", (int)SearchField)
                                    .SetParameter("barcodeParam", personbarCode)
                                    .SetParameter("personNameParam", personName)
                                    .SetParameter("orderByField", orderingField)
                                    .SetParameter("orderType", orderingType)
                                    .SetParameter("dateRangeOrder", dateRangeOrder)
                                    .SetParameter("dateRangeOrderIndex", dateRangeOrderIndex)
                                    .SetParameter("curentDate", CurrentDateTime)
                                    .SetParameter("pageSize", pageSize)
                                    .SetParameter("pageIndex", pageIndex)
                                    .List<UnderManagementPerson>();
        }

        public IList<UnderManagementPerson> GetUnderManagmentOperatorByDepartment(GridSearchFields SearchField, decimal oprPersonId, decimal departmentID, string personName, string personbarCode, int dateRangeOrder, int dateRangeOrderIndex, string CurrentDateTime, GridOrderFields order, GridOrderFieldType orderType, int pageIndex, int pageSize)
        {
            string orderingType = orderType.ToString();
            string orderingField = order.ToString();
            return NHibernateSession.GetNamedQuery("GetUnderManagementOperatorPersonList")
                                    .SetParameter("oprPersonId", oprPersonId)
                                    .SetParameter("departmentId", departmentID)
                                    .SetParameter("searchField", (int)SearchField)
                                    .SetParameter("barcodeParam", personbarCode)
                                    .SetParameter("personNameParam", personName)
                                    .SetParameter("orderByField", orderingField)
                                    .SetParameter("orderType", orderingType)
                                    .SetParameter("dateRangeOrder", dateRangeOrder)
                                    .SetParameter("dateRangeOrderIndex", dateRangeOrderIndex)
                                    .SetParameter("curentDate", CurrentDateTime)
                                    .SetParameter("pageSize", pageSize)
                                    .SetParameter("pageIndex", pageIndex)
                                    .List<UnderManagementPerson>();
        }

        /// <summary>
        /// پرسنل تحت مدیریت قبلا استخراج شده اند
        /// </summary>
        /// <param name="dateRangeOrder"></param>
        /// <param name="dateRangeOrderIndex"></param>
        /// <param name="CurrentDateTime"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IList<UnderManagementPerson> GetUnderManagment(int dateRangeOrder, int dateRangeOrderIndex, string CurrentDateTime, IList<decimal> underMngList, int pageIndex, int pageSize)
        {
            if (underMngList != null && underMngList.Count > 0)
            {
                return NHibernateSession.GetNamedQuery("GetUnderManagementPersonListByList")
                                        .SetParameter("dateRangeOrder", dateRangeOrder)
                                        .SetParameter("dateRangeOrderIndex", dateRangeOrderIndex)
                                        .SetParameter("curentDate", CurrentDateTime)
                                        .SetParameter("pageSize", pageSize)
                                        .SetParameter("pageIndex", pageIndex)
                                        .SetParameterList("underMngList", underMngList.ToArray())
                                        .List<UnderManagementPerson>();
            }
            return new List<UnderManagementPerson>();
        }

        /// <summary>
        /// پرسنل تحت مدیریت را برمیگرداند
        /// فقط مدیر و شامل جانشین نمیشود
        /// </summary>
        /// <param name="SearchField"></param>
        /// <param name="personId"></param>
        /// <param name="departmentID"></param>
        /// <param name="personName"></param>
        /// <param name="personbarCode"></param>
        /// <param name="dateRangeOrder"></param>
        /// <param name="dateRangeOrderIndex"></param>
        /// <param name="CurrentDateTime"></param>
        /// <param name="order"></param>
        /// <param name="orderType"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IList<UnderManagementPerson> GetUnderManagmentByDepartment_JustMainManagers(GridSearchFields SearchField, decimal personId, decimal departmentID, string personName, string personbarCode, int dateRangeOrder, int dateRangeOrderIndex, string CurrentDateTime, GridOrderFields order, GridOrderFieldType orderType, int pageIndex, int pageSize)
        {
            string orderingType = orderType.ToString();
            string orderingField = order.ToString();
            return NHibernateSession.GetNamedQuery("GetJustUnderManagmentMainFlowByDepartmentList")
                                    .SetParameter("personId", personId)
                                    .SetParameter("departmentId", departmentID)
                                    .SetParameter("searchField", (int)SearchField)
                                    .SetParameter("barcodeParam", personbarCode)
                                    .SetParameter("personNameParam", personName)
                                    .SetParameter("orderByField", orderingField)
                                    .SetParameter("orderType", orderingType)
                                    .SetParameter("dateRangeOrder", dateRangeOrder)
                                    .SetParameter("dateRangeOrderIndex", dateRangeOrderIndex)
                                    .SetParameter("curentDate", CurrentDateTime)
                                    .SetParameter("pageSize", pageSize)
                                    .SetParameter("pageIndex", pageIndex)
                                    .List<UnderManagementPerson>();
        }


        public int GetUnderManagmentByDepartmentCount(GridSearchFields SearchField, decimal personId, decimal departmentID, string personName, string personbarCode)
        {
            return NHibernateSession.GetNamedQuery("GetUnderManagementPersonCount")
                                    .SetParameter("personId", personId)
                                    .SetParameter("departmentId", departmentID)
                                    .SetParameter("searchField", SearchField)
                                    .SetParameter("barcodeParam", personbarCode)
                                    .SetParameter("personNameParam", personName)
                                    .UniqueResult<int>();
        }

        /// <summary>
        /// پرسنل تحت مدیرت را برمیگرداند
        /// فقط جریان اصلی
        /// فقط مدیر و شامل جانشین نیست
        /// </summary>
        /// <param name="SearchField"></param>
        /// <param name="personId"></param>
        /// <param name="departmentID"></param>
        /// <param name="personName"></param>
        /// <param name="personbarCode"></param>
        /// <returns></returns>
        public int GetUnderManagmentByDepartment_JustMainManagers_Count(GridSearchFields SearchField, decimal personId, decimal departmentID, string personName, string personbarCode)
        {
            return NHibernateSession.GetNamedQuery("GetJustUnderManagmentMainFlowByDepartmentCount")
                                    .SetParameter("personId", personId)
                                    .SetParameter("departmentId", departmentID)
                                    .SetParameter("searchField", SearchField)
                                    .SetParameter("barcodeParam", personbarCode)
                                    .SetParameter("personNameParam", personName)
                                    .UniqueResult<int>();
        }


        public int GetUnderManagmentOperatorByDepartmentCount(GridSearchFields SearchField, decimal oprPersonId, decimal departmentID, string personName, string personbarCode)
        {
            return NHibernateSession.GetNamedQuery("GetUnderManagementOperatorPersonCount")
                                    .SetParameter("oprPersonId", oprPersonId)
                                    .SetParameter("departmentId", departmentID)
                                    .SetParameter("searchField", SearchField)
                                    .SetParameter("barcodeParam", personbarCode)
                                    .SetParameter("personNameParam", personName)
                                    .UniqueResult<int>();
        }

        /// <summary>
        /// لیست افرادی که مدیر هستند را برمیگرداند
        /// </summary>
        /// <returns></returns>
        public IList<Person> GetAllManager()
        {
            IList<Manager> list = this.GetAll().Where(x => x.Active == true).ToList();
            var persons = from mng in list
                          select mng.ThePerson;
            IList<Person> result = persons.ToList<Person>();
            return result;
        }

        /// <summary>
        /// تمام پیشکارتهایی که مدیر به آنها دسترسی دارد را برمیگرداند
        /// </summary>
        /// <param name="managerPersonID">شناسه پرسنلی مدیر</param>
        /// <returns></returns>
        public IList<Precard> GetAllAccessGroup(decimal managerID)
        {

            string SQLCommand = @"select distinct p.*
                                    from TA_ManagerFlow
                                    join TA_Flow flow on Flow_ID =mngrFlow_FlowID
                                    join TA_PrecardAccessGroup on accessGrp_ID= Flow_AccessGroupID
                                    join TA_PrecardAccessGroupDetail on accessGrpDtl_AccessGrpId = accessGrp_ID
                                    join TA_Precard as p on  Precrd_ID=accessGrpDtl_PrecardId 
                                    where mngrFlow_ManagerID=:managerId and flow_Deleted = 0";

            IList<Precard> list = base.NHibernateSession.CreateSQLQuery(SQLCommand)
                .AddEntity(typeof(Precard))
                .SetParameter("managerId", managerID)
                .List<Precard>();
            return list;
        }

        #endregion

        public IList<decimal> GetAllManagerPersons(IList<decimal> condidatePersonsId)
        {
            string SQLCommand = @"select case when isnull(MasterMng_PersonID,0) > isnull(MasterMng_OrganizationUnitID,0)
                                then isnull(MasterMng_PersonID,0)
                                else isnull(MasterMng_OrganizationUnitID,0)
                                end
                                from TA_Manager
                                left outer join TA_OrganizationUnit on organ_ID=MasterMng_OrganizationUnitID
                                where MasterMng_Active=1 AND (
                                MasterMng_PersonID in (:condidatePersonsId)  OR 
                                organ_PersonID in (:condidatePersonsId))";
            IList<decimal> list = base.NHibernateSession.CreateSQLQuery(SQLCommand)
               .SetParameterList("condidatePersonsId", base.CheckListParameter(condidatePersonsId))
               .List<decimal>();
            return list;
        }

        public void SetManagerFlowActivation(decimal flowID)
        {
            string SQLCommand = @"UPDATE TA_ManagerFlow
                                  SET mngrFlow_Active = 0
                                  WHERE mngrFlow_FlowID = :flowID";
            base.NHibernateSession.CreateSQLQuery(SQLCommand)
                                  .SetParameter("flowID", flowID)
                                  .ExecuteUpdate();
        }

        /// <summary>
        /// اگر مدیری در جریانی استفاده نشده باشد آنرا غیر فعال میکند
        /// </summary>
        public void SetManagerActivation()
        {
            string SQLCommand = @"update TA_Manager
                                set MasterMng_Active=0
                                where MasterMng_ID not in (select mngrFlow_ManagerID from TA_ManagerFlow where mngrFlow_Active=1)";
            base.NHibernateSession.CreateSQLQuery(SQLCommand)
               .ExecuteUpdate();

            SQLCommand = @"update TA_Manager
                                set MasterMng_Active=1
                                where MasterMng_ID in (select mngrFlow_ManagerID from TA_ManagerFlow where mngrFlow_Active=1)";
            base.NHibernateSession.CreateSQLQuery(SQLCommand)
               .ExecuteUpdate();
        }
    }

}
