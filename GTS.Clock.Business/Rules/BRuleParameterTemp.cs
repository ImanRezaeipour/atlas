using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Model;
using GTS.Clock.Business.Security;

namespace GTS.Clock.Business.Rules
{
    public class BRuleParameterTemp : BaseBusiness<RuleTemplateParameter>
    {
        private readonly EntityRepository<RuleTemplateParameter> ruleTemParameRepo;
        private readonly EntityRepository<RuleTemplate> ruleRepo;
        const string ExceptionSrc = "GTS.Clock.Business.Rules.BRuleParameterTemp";

        private const string TwoColumnValue = "RuleDesigner";

        public BRuleParameterTemp()
        {
            ruleTemParameRepo = new EntityRepository<RuleTemplateParameter>(false);
            ruleRepo = new EntityRepository<RuleTemplate>(false);
        }

        #region Validation Implementation
        protected override void InsertValidate(RuleTemplateParameter clCar)
        {
            GeneralValidation(clCar);

            UIValidationExceptions exception = new UIValidationExceptions();

            if(ruleTemParameRepo.GetAll().Any(x=>
                x.RuleTemplateId.Equals(clCar.RuleTemplateId)
                && x.Name.ToUpper().Equals(clCar.Name.ToUpper())
                ))
                exception.Add(ExceptionResourceKeys.BRuleTempNameRepeated, "نام تكراري است", ExceptionSrc);

                if (exception.Count > 0)
                {
                    throw exception;
                }
        }
        protected override void UpdateValidate(RuleTemplateParameter clCar)
        {
            GeneralValidation(clCar);
        }
        protected override void DeleteValidate(RuleTemplateParameter clCar)
        {
        }

        private void GeneralValidation(RuleTemplateParameter obj)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            if (string.IsNullOrEmpty(obj.Name))
                exception.Add(ExceptionResourceKeys.BRuleTempParameterNameRequied, "نام اجباري است", ExceptionSrc);

            if (ruleRepo.GetById(obj.RuleTemplateId, false) == null)
            {
                exception.Add(ExceptionResourceKeys.BRuleTempParameterRuleRequied, "قانون اجباري است", ExceptionSrc);
            }
            else if (!ruleRepo.GetById(obj.RuleTemplateId, false).UserDefined)
            {
                exception.Add(ExceptionResourceKeys.BRuleTempShouldBeUserDefined, "قانون باید از قوانین ساخته شده توسط کاربر باشد", ExceptionSrc);
            }
            
            if (exception.Count > 0)
            {
                throw exception;
            }

        }

        #endregion

        
        #region CURD

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertRuleTempParam(RuleTemplateParameter ruleTempRecived)
        {
            try
            {
                return this.SaveChanges(ruleTempRecived, UIActionType.ADD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal UpdateRuleTempParam(RuleTemplateParameter ruleTempRecived)
        {
            try
            {
                return this.SaveChanges(ruleTempRecived, UIActionType.EDIT);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal DeleteRuleTempParam(RuleTemplateParameter ruleTempRecived)
        {
            try
            {
                return this.SaveChanges(ruleTempRecived, UIActionType.DELETE);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        public List<RuleTemplateParameter> GeRuleTempParams(decimal ruleTempId)
        {
            return ruleTemParameRepo.GetAll().Where(x => x.RuleTemplateId.Equals(ruleTempId)).ToList();
        }

    }
}
