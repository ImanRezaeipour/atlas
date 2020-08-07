using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Model;
using GTS.Clock.Model.Charts;
using GTS.Clock.Business.Security;
using NHibernate.Linq;
using NHibernate.Transaction;
using NHibernate.Criterion;
using NHibernate;
namespace GTS.Clock.Business.Rules
{
    public class BRuleTemp : BaseBusiness<RuleTemplate>
    {
        private readonly EntityRepository<RuleTemplate> _ruleRep;//= new EntityRepository<RuleTemplate>();
        const string ExceptionSrc = "GTS.Clock.Business.Rules.BRuleTemp";
        private ISession NHSession = NHibernateSessionManager.Instance.GetSession();
        private BRuleType bRuleType;// = new BRuleType();
        private Dictionary<decimal, string> RuleTypeList;// = new Dictionary<int, string>();
       
        public BRuleTemp()
        {
            _ruleRep = new EntityRepository<RuleTemplate>();

            bRuleType = new BRuleType();
            RuleTypeList = new Dictionary<decimal, string>();
            foreach (var ruleType in bRuleType.GetAll())
                RuleTypeList.Add(ruleType.ID, ruleType.Name);

        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertRule(RuleTemplate ruleRecived)
        {
            try
            {
                return this.SaveChanges(ruleRecived, UIActionType.ADD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal UpdateRule(RuleTemplate ruleRecived)
        {
            try
            {
                return this.SaveChanges(ruleRecived, UIActionType.EDIT);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal DeleteRule(RuleTemplate ruleRecived)
        {
            try
            {
                return this.SaveChanges(ruleRecived, UIActionType.DELETE);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected override void InsertValidate(RuleTemplate obj)
        {
            GeneralValidation(obj);

            UIValidationExceptions exception = new UIValidationExceptions();
            RuleRepository ruleRepository = new RuleRepository();
            //if (ruleRepository.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => obj.Name), obj.Name)) > 0)
                //if (ruleRepository.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new RuleTemplate().Name), obj.Name.ToLower())) > 0)
            IList<RuleTemplate> NamequeryOnRule = NHSession.QueryOver<RuleTemplate>()
                                                                           .Where(x => x.Name == obj.Name)
                                                                           .List<RuleTemplate>();
            if (NamequeryOnRule.Count > 0)
            {
                //if (54ll().Any(x => x.Name.ToUpper().Equals(obj.Name.ToUpper())))
                exception.Add(ExceptionResourceKeys.BRuleCodeRepeated, "نام تكراري است", ExceptionSrc);
            }
            //if (GetAll().Any(x => x.IdentifierCode.Equals(obj.IdentifierCode)))
            //if (ruleRepository.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => obj.IdentifierCode), obj.IdentifierCode)) > 0)
            IList<RuleTemplate> CodequeryOnRule = NHSession.QueryOver<RuleTemplate>().Where(x => x.IdentifierCode == obj.IdentifierCode).List<RuleTemplate>();
            if (CodequeryOnRule.Count > 0)
            {
                exception.Add(ExceptionResourceKeys.BRuleCodeRepeated, "كد تكراري است", ExceptionSrc);
            }
            if (exception.Count > 0)
            {
                throw exception;
            }

        }
        protected override void UpdateValidate(RuleTemplate obj)
        {
            GeneralValidation(obj);
            RuleRepository ruleRepository = new RuleRepository();
            UIValidationExceptions exception = new UIValidationExceptions();

            //if (_ruleRep.GetAll().Any(x => x.ID != obj.ID && x.Name.ToUpper().Equals(obj.Name.ToUpper())))
            if (ruleRepository.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => obj.Name), obj.Name)) > 0)
            {
                exception.Add(ExceptionResourceKeys.BRuleCodeRepeated, "نام تكراري است", ExceptionSrc);
            }
            //if (_ruleRep.GetAll().Any(x => x.ID != obj.ID && x.IdentifierCode.Equals(obj.IdentifierCode)))
            if (ruleRepository.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => obj.IdentifierCode), obj.IdentifierCode)) > 0)
            {
                exception.Add(ExceptionResourceKeys.BRuleCodeRepeated, "كد تكراري است", ExceptionSrc);
            }
            if (exception.Count > 0)
            {
                throw exception;
            }
        }
        protected override void DeleteValidate(RuleTemplate obj)
        {

        }

        private void GeneralValidation(RuleTemplate obj)
        {
            UIValidationExceptions exception = new UIValidationExceptions();
        
            if (string.IsNullOrEmpty(obj.Name))
                exception.Add(ExceptionResourceKeys.BRuleNameRepeated, "نام اجباري است", ExceptionSrc);

            //if (obj.IdentifierCode < 1)
            //    exception.Add(ExceptionResourceKeys.BRuleCodeRequierd, "كد اجباري است", ExceptionSrc);
            if (obj.OperationalArea == 1)
            {
                exception.Add(ExceptionResourceKeys.BRuleCodeRequierd, "حوزه عمل اجباري است", ExceptionSrc);
            }
            if (obj.TypeId == 0)
            {
                exception.Add(ExceptionResourceKeys.BRuleCodeRequierd, "نوع قانون اجباري است", ExceptionSrc);
            }
            if (exception.Count > 0)
            {
                throw exception;
            }

        }

        public int GetAllByPageBySearchCount(string searchTerm)
        {
            var count = 0;

            IEnumerable<RuleTemplate> queryOnRule = null;
           
            try
            {
                if (string.IsNullOrEmpty(searchTerm.Trim()))
                {
                    queryOnRule = NHSession.QueryOver<RuleTemplate>()
                                           .Where(x => x.UserDefined)
                                           .List<RuleTemplate>();
           
                }
                else
                {
                     queryOnRule = NHSession.QueryOver<RuleTemplate>()
                                                                 .Where(rule => rule.UserDefined && rule.Name.Contains(searchTerm) || rule.IdentifierCode.ToString(CultureInfo.InvariantCulture).Contains(searchTerm))
                                                                 .List<RuleTemplate>();
                }

                count = 0;
                if (queryOnRule.FirstOrDefault() != null) count = queryOnRule.Count();
            }
            catch (Exception ex)
            {
                LogException(ex, "GTS.Clock.Business.Rules.BRule", "GetAllByPageBySearchCount(ruleSearchKeys searchKey, string searchTerm)");
                throw ex;
            }
            return count;
        }
      
        public IList<RuleTemplateProxy> GetAllByPageBySearch(int pageIndex, int pageSize, string searchTerm)
        {
            IEnumerable<RuleTemplate> queryOnRule = null;
          
             IList<RuleTemplate> ruleTemplates = new List<RuleTemplate>();
             IList<DesignedRule> designedRules = new BDesignedRules().GetAll();
            try
            {

                if (string.IsNullOrEmpty(searchTerm.Trim()))
                {
                    queryOnRule =
                        _ruleRep.Find(ruleTmp =>
                        ruleTmp.UserDefined);
             
                }
                else
                {
                    queryOnRule = NHSession.QueryOver<RuleTemplate>()
                                                                .Where(rule => rule.UserDefined && rule.Name.Contains(searchTerm) || rule.Name.Contains(searchTerm) || rule.IdentifierCode.ToString(CultureInfo.InvariantCulture).Contains(searchTerm))
                                                                .List<RuleTemplate>();
                }

                queryOnRule =
                    queryOnRule

                    .Skip(pageIndex * pageSize)
                    .Take(pageSize);
                ruleTemplates = queryOnRule.ToList<RuleTemplate>();

            }
            catch (Exception ex)
            {
                LogException(ex, "GTS.Clock.Business.Rules.BRule", "GetAllByPageBySearch");
                throw ex;
            }
            foreach (RuleTemplate item in ruleTemplates)
            {
                DesignedRule desinedRuleObj =designedRules.FirstOrDefault(d => d.RuleTemplate.ID == item.ID);
                    item.DesignedRule = desinedRuleObj;
              
                
            }

            var RuleTemplateProxyList = ruleTemplates.Select(ruleTemplate => new RuleTemplateProxy(ruleTemplate) { Type = RuleTypeList[ruleTemplate.TypeId] }).ToList();

            return RuleTemplateProxyList;
        }
        public decimal GetLastCodeByRuleTypeID(Decimal RuleTypeId)
        {
            decimal LastRuleType = 0;
            IList<RuleTemplate> ruleTemplateObj = new List<RuleTemplate>();

            IList<RuleTemplate> ruleRuleTemplatesList = NHSession.QueryOver<RuleTemplate>()
                                                                 .Where(x => x.UserDefined && x.TypeId == RuleTypeId)
                                                                 .List<RuleTemplate>();
            if (ruleRuleTemplatesList.Count() > 0)
            {
                LastRuleType = ruleRuleTemplatesList.LastOrDefault().IdentifierCode + 1;
            }
            else
            {
                LastRuleType = SetDefualtIdentifierode(RuleTypeId);

            }
               
            return LastRuleType;
        }
        public decimal SetDefualtIdentifierode(Decimal RuleTypeId)
        {
            decimal DefualCode = 0;
            int RuleTypeID = Convert.ToInt32(RuleTypeId);
            switch (RuleTypeID)
            {
                case 552:
                    DefualCode = 6701;
                    break;
                case 553:
                    DefualCode = 4701;
                    break;
                case 554:
                    DefualCode = 2701;
                    break;
                case 555:
                    DefualCode = 5701;
                    break;
                case 556:
                    DefualCode = 3701;
                    break;
                case 564:
                    DefualCode = 1701;
                    break;
            }
            return DefualCode;
        }
        public void Copy(RuleTemplate RuleTemplateFrom, ref RuleTemplate RuleTemplateTo)
        {
            RuleTemplateTo.IdentifierCode = RuleTemplateFrom.IdentifierCode;
            RuleTemplateTo.Name = RuleTemplateFrom.Name;
            RuleTemplateTo.CustomCategoryCode = RuleTemplateFrom.CustomCategoryCode;
            RuleTemplateTo.TypeId = RuleTemplateFrom.TypeId;
            RuleTemplateTo.UserDefined = RuleTemplateFrom.UserDefined;
            RuleTemplateTo.Script = RuleTemplateFrom.Script;
            RuleTemplateTo.CSharpCode = RuleTemplateFrom.CSharpCode;
            RuleTemplateTo.JsonObject = RuleTemplateFrom.JsonObject;
            RuleTemplateTo.OperationalArea = RuleTemplateFrom.OperationalArea;
        }

    }
}
