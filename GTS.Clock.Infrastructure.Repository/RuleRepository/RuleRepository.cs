using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Model.Clientele;
using NHibernate;
using NHibernate.Criterion;
using GTS.Clock.Infrastructure.RepositoryFramework;
using GTS.Clock.Model;
using GTS.Clock.Infrastructure.Utility;


namespace GTS.Clock.Infrastructure.Repository
{
    public class RuleRepository : RepositoryBase<Rule>, IRuleRepository
    {
        public override string TableName
        {
            get { return "TA_DefinedRule"; }
        }
		        private ISession NHSession = NHibernateSessionManager.Instance.GetSession();
		int operationBatchSizeValue = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings[OperationBatchSize.OperationBatchSizeKey.ToString()]);
        public RuleRepository()
            : base()
        { }

        public RuleRepository(bool Disconnectedly)
            : base(Disconnectedly)
        { }

        public IList<RuleTemplate> GetRuleTemplates(decimal[] ruleTemplateIds)
        {
            if (ruleTemplateIds == null)
                return new List<RuleTemplate>();
			RuleTemplate ruleTemplateAlias = null;
			IList<decimal> accessableIDs = ruleTemplateIds;
			IList<RuleTemplate> result = new List<RuleTemplate>();
			IList<RuleTemplate> ruleTemplateList = new List<RuleTemplate>();
			if (accessableIDs.Count < this.operationBatchSizeValue && this.operationBatchSizeValue < 2100)
			{
				ruleTemplateList= NHibernateSession.QueryOver<RuleTemplate>()
										.WhereRestrictionOn(x => x.ID)
										.IsIn(ruleTemplateIds)
										.List<RuleTemplate>();
			}
			else
			{
				GTS.Clock.Model.Temp.Temp tempAlias = null;
				TempRepository tempRep = new TempRepository(false);
				string operationGUID = tempRep.InsertTempList(accessableIDs);
				ruleTemplateList= NHibernateSession.QueryOver<RuleTemplate>(()=>ruleTemplateAlias)
					                    .JoinAlias(()=>ruleTemplateAlias.TempList,()=>tempAlias)
                                        .Where(() => tempAlias.OperationGUID == operationGUID)
										.List<RuleTemplate>();
				tempRep.DeleteTempList(operationGUID);
			}
			return ruleTemplateList;
        }

        public int GetTemplateParameterCount(decimal ruleTmpId) 
        {
            EntityRepository<RuleTemplateParameter> rep = new EntityRepository<RuleTemplateParameter>(false);
            int count = rep.GetCountByCriteria(new CriteriaStruct(Utility.Utility.GetPropertyName(() => new RuleTemplateParameter().RuleTemplateId), ruleTmpId));
            return count;
        }

        /// <summary>
        /// کل پارامترهای قوانین را واکشی میکند
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public IList<AssignedRuleParameter> GetAssginedRuleParamList(DateTime fromDate, DateTime toDate)
        {

            return NHibernateSession.GetNamedQuery("GetAssginedRuleParamList")
                                    .SetParameter("fromDate", GTS.Clock.Infrastructure.Utility.Utility.ToString(fromDate))
                                    .SetParameter("toDate", GTS.Clock.Infrastructure.Utility.Utility.ToString(toDate))
                                    .List<AssignedRuleParameter>();
        }

        public List<RuleTemplate> GetRuleUserDefined()
        {
            return new EntityRepository<RuleTemplate>().Find(x => x.UserDefined).ToList();
        }
    }
}
