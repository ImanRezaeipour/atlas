using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using GTS.Clock.Infrastructure.RepositoryFramework;
using GTS.Clock.Model;
using System.Linq;
using GTS.Clock.Model.Charts;
using System.Text;
using GTS.Clock.Model;
using GTS.Clock.Infrastructure.NHibernateFramework;


namespace GTS.Clock.Infrastructure.Repository
{
    public class RuleCategoryRepository : RepositoryBase<RuleCategory>, IRuleCategoryRepository // IRepository<GTS.Clock.Model.Charts.Category>
    {
        #region Variables

        public override string TableName
        {
            get { return "TA_Category"; }
        }

        #endregion

        #region Constructor

        public RuleCategoryRepository() { }

        public RuleCategoryRepository(bool Disconnectedly)
            : base(Disconnectedly)
        { }

        #endregion

        #region Methods

        public bool IsRoot(decimal id)
        {
            return NHibernateSession.QueryOver<RuleCategory>()
                                    .Where(x => x.ID == id)
                                    .And(x => x.IsRoot == true)
                                    .SingleOrDefault() != null;
        }

        public IList<RuleCategory> GetRoot()
        {
            return Transact<IList<RuleCategory>>(() => NHibernateSession.QueryOver<RuleCategory>()
                                                                    .Where(x => x.IsRoot == true)
                                                                    .List<RuleCategory>());
        }

        public IList<Rule> GetRulesByRuleCatID(decimal id)
        {
            return Transact<IList<Rule>>(() => NHibernateSession.QueryOver<Rule>()
                                                .Where(x => x.Category.ID == id)
                                                .List<Rule>());
        }

        public IList<RuleTemplate> GetForcibleRuleTemplates()
        {
            return Transact<IList<RuleTemplate>>(() => NHibernateSession.QueryOver<RuleTemplate>()
                                    .Where(x => x.IsForcible)
                                    .List<RuleTemplate>());
        }

        public bool HasPerson(decimal id)
        {
            Person Person = null;
            PersonRuleCatAssignment PrsRleCatAsg = null;
            RuleCategory RuleCat = null;
            return NHibernateSession.QueryOver<Person>(() => Person)
                                     .Select(Projections.RowCount())
                                     .JoinAlias(() => Person.PersonRuleCatAssignList, () => PrsRleCatAsg)
                                     .JoinAlias(() => PrsRleCatAsg.RuleCategory, () => RuleCat)
                                     .Where(x => RuleCat.ID == id)
                                     .FutureValue<int>().Value > 0;

        }
        public IList<RuleCategory> GetRuleOfUser(decimal userId, string searchItem)
        {
            string SQLCommand = @"select * from TA_RuleCategory
                                  inner join TA_DataAccessRuleGroup
                                  on RuleCat_ID = DataAccessRuleGrp_RuleGrpID
                                  where RuleCat_Name like :searchItem and DataAccessRuleGrp_UserID  =:userId
                                   ";
            IList<RuleCategory> RuleCategoryList = NHibernateSessionManager.Instance.GetSession().CreateSQLQuery(SQLCommand)
                                                                                                  .AddEntity(typeof(RuleCategory))
                                                                                                  .SetParameter("searchItem", String.Format("%{0}%", searchItem))
                                                                                                  .SetParameter("userId", userId)
                                                                                                  .List<RuleCategory>();
            return RuleCategoryList;
        }




        #endregion


    }
}
