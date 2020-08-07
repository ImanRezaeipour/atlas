using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Model.BaseInformation;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Business.Security;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Model.RequestFlow;

namespace GTS.Clock.Business.RequestFlow
{
   public class BFlowGroup : BaseBusiness<FlowGroup>
    {
       const string ExceptionSrc = "GTS.Clock.Business.RequestFlow.BFlowGroup";
       private EntityRepository<FlowGroup> staionRepository = new EntityRepository<FlowGroup>(false);

       [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
       public decimal InsertFlowGroup(FlowGroup flowGroup, UIActionType UAT)
       {
           return base.SaveChanges(flowGroup, UAT);
       }

       [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
       public decimal UpdateFlowGroup(FlowGroup flowGroup, UIActionType UAT)
       {
           return base.SaveChanges(flowGroup, UAT);
       }

       [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
       public decimal DeleteFlowGroup(FlowGroup flowGroup, UIActionType UAT)
       {
           return base.SaveChanges(flowGroup, UAT);
       }
        
        protected override void InsertValidate(FlowGroup flowGroup)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            if (Utility.IsEmpty(flowGroup.Name))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.FlowGroupNameRequired, "درج - نام نباید خالی باشد", ExceptionSrc));
            }
            else if (staionRepository.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => flowGroup.Name), flowGroup.Name)) > 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.FlowGroupNameRepeated, "درج - نام نباید تکراری باشد", ExceptionSrc));
            }


            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        protected override void UpdateValidate(FlowGroup flowGroup)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            if (Utility.IsEmpty(flowGroup.Name))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.FlowGroupNameRequired, "نام نباید خالی باشد", ExceptionSrc));
            }
            else
            {
                if (staionRepository.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => flowGroup.Name), flowGroup.Name),
                                                         new CriteriaStruct(Utility.GetPropertyName(() => flowGroup.ID), flowGroup.ID, CriteriaOperation.NotEqual)) > 0)
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.FlowGroupNameRepeated, "نام نباید تکراری باشد", ExceptionSrc));
                }
            }

            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        protected override void DeleteValidate(FlowGroup flowGroup)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            FlowRepository flowRep = new FlowRepository(false);
            int count = flowRep.Find(f => f.FlowGroup.ID == flowGroup.ID).Count();
            if (count > 0)
            {
                exception.Add(ExceptionResourceKeys.DepUsedByPersons, "این گروه به جریان انتساب داده شده است", ExceptionSrc);
            }
            if (exception.Count > 0)
            {
                throw exception;
            }
        }
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckFlowGroupLoadAccess()
        {
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckWorkFlowDetailLoadAccess()
        {

        }
    }
}
