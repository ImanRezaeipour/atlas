using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using GTS.Clock.Model;
using GTS.Clock.Model.Charts;
using GTS.Clock.Infrastructure.RepositoryFramework;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure.NHibernateFramework;

namespace GTS.Clock.Infrastructure.Repository
{
    public class OrganizationUnitRepository : RepositoryBase<GTS.Clock.Model.Charts.OrganizationUnit>, IOrganizationUnitRepository
    {
        public override string TableName
        {
            get { return "TA_OrganizationUnit"; }
        }
		private ISession NHSession = NHibernateSessionManager.Instance.GetSession();
		int operationBatchSizeValue = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings[OperationBatchSize.OperationBatchSizeKey.ToString()]);
        public OrganizationUnitRepository(bool Disconnectedly)
            : base(Disconnectedly)
        { }
    
        #region Model Interface

        public IList<OrganizationUnit> GetOrganizationUnitTree()
        {
            ICriteria crit = this.NHibernateSession.CreateCriteria(typeof(OrganizationUnit));
            crit.Add(Expression.Or(
                Expression.IsNull("Parent"),
                Expression.Eq("Parent.ID", Convert.ToDecimal(0))));

            IList<OrganizationUnit> parents = crit.List<OrganizationUnit>();
            return parents;
        }

        public decimal GetParentID(decimal organID)
        {
            string SQLCommand = String.Format("SELECT organ_ParentID FROM TA_OrganizationUnit " +
                                                "WHERE organ_ID = {0} ", organID);

            IQuery query = base.NHibernateSession.CreateSQLQuery(SQLCommand);

            object parentID = query.List<object>().FirstOrDefault();
            if (parentID != null)
                return (decimal)parentID;
            return 0;
        }

        public bool IsRoot(decimal organID) 
        {
            if (GetParentID(organID) == 0)
                return true;
            return false;
        }

        public bool HasPerson(decimal organID)
        {
            string SQLCommand = String.Format("SELECT organ_PersonID FROM TA_OrganizationUnit " +
                                               "WHERE organ_ID = {0} ", organID);

            IQuery query = base.NHibernateSession.CreateSQLQuery(SQLCommand);

            object personID = query.List<object>().FirstOrDefault();
            if (personID == null || (decimal)personID == 0)
                return false;
            return true;
        }

        public virtual IList<OrganizationUnit> GetChildsPage(decimal parentId, int pageIndex, int pageSize)
        {      
            return this.NHibernateSession.QueryOver<OrganizationUnit>()
                                            .Where(x => x.ID == parentId)
                                            .Skip(pageIndex * pageSize)
                                            .Take(pageSize)
                                            .List();
        }

        public OrganizationUnit GetByPersonId(decimal personID) 
        {
            OrganizationUnit organ = base.GetByCriteria(new CriteriaStruct(Utility.Utility.GetPropertyName(() => new OrganizationUnit().Person), new Person() { ID = personID })).FirstOrDefault();
            return organ;

        }

        /// <summary>
        /// پست سازمانی دسته ای از پرسنل را برمیگرداند
        /// </summary>
        /// <param name="personIds"></param>
        /// <returns></returns>
        public IList<OrganizationUnit> GetOrganizationUsnitByPersonIds(IList<decimal> personIds) 
        {
			IList<decimal> accessableIDs = personIds;
			IList<OrganizationUnit> organizationUnitList = new List<OrganizationUnit>();
			
			if (accessableIDs.Count < this.operationBatchSizeValue && this.operationBatchSizeValue < 2100)
			{
				organizationUnitList= GetByCriteria(new CriteriaStruct(Utility.Utility.GetPropertyName(() => new OrganizationUnit().Person.ID), personIds.ToArray(), CriteriaOperation.IN));
			}
			else
			{
				OrganizationUnit organizationUnitAlias=null;
				Person personAlias=null;
				GTS.Clock.Model.Temp.Temp tempAlias = null;
				TempRepository tempRep = new TempRepository(false);
				string operationGUID = tempRep.InsertTempList(accessableIDs);
				organizationUnitList = NHSession.QueryOver<OrganizationUnit>(() => organizationUnitAlias)
					                          .JoinAlias(()=>organizationUnitAlias.Person,()=>personAlias)
											  .JoinAlias(() => personAlias.TempList, () => tempAlias)
											  .Where(()=> tempAlias.OperationGUID==operationGUID)
											  .List<OrganizationUnit>();
				tempRep.DeleteTempList(operationGUID);
			}
			return organizationUnitList;
        }

        public void DeleteByPerson(decimal prsId) 
        {
            string SQLCommand = @"update TA_OrganizationUnit
                           set organ_personid=null
                           where organ_personid = :personId";

            base.NHibernateSession.CreateSQLQuery(SQLCommand)
                .SetParameter("personId", prsId)
                .ExecuteUpdate();
        }

        public void DeleteHierarchicalByParentId(decimal parentId)
        {
            string SQLCommand = String.Format("DELETE FROM TA_OrganizationUnit where organ_ParentPath like('%,{0},%')", parentId);
            base.NHibernateSession.CreateSQLQuery(SQLCommand)
                                  .ExecuteUpdate();
        }
        public IList<OrganizationUnit> GetSearchOrganizationOfUser(decimal userId, string SearchItem)
        {
            string SqlCommand = @"select * from TA_OrganizationUnit
                                  inner join TA_DataAccessOrganizationUnit
                                  on organ_ID = DataAccessOrgUnit_OrgUnitID or 
                                  organ_ParentPath like '%,' + CONVERT(varchar(10),DataAccessOrgUnit_OrgUnitID) + ',%'
                                  where DataAccessOrgUnit_UserID =:userId AND (organ_Name LIKE :SearchItem or organ_CustomCode LIKE :SearchItem) AND
                                        organ_ParentID is not null";

            IList<OrganizationUnit> OrgList = NHibernateSessionManager.Instance.GetSession().CreateSQLQuery(SqlCommand)
                                                                                .AddEntity(typeof(OrganizationUnit))
                                                                                .SetParameter("SearchItem", String.Format("%{0}%", SearchItem))
                                                                                .SetParameter("userId", userId)
                                                                                .List<OrganizationUnit>();
            return OrgList;
        }

        #endregion      
        
    }
}
