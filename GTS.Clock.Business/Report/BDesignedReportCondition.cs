using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Model.Report;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Business.Security;
namespace GTS.Clock.Business.Reporting
{
   public class BDesignedReportCondition : BaseBusiness<DesignedReportCondition>
    {
       EntityRepository<DesignedReportCondition> designedReportConditionRep = new EntityRepository<DesignedReportCondition>(false);

       public DesignedReportCondition GetDesignedReportCondition(decimal reportID, decimal personId)
       {
           try
           {

               DesignedReportCondition designedReportConditionObj = designedReportConditionRep.Find(r => r.Report.ID == reportID && r.Person.ID==personId).SingleOrDefault();

               return designedReportConditionObj;
           }
           catch (Exception ex)
           {
               LogException(ex, "BDesignedReportCondition", "GetDesignedReportCondition");
               throw ex;
           }
       }
       public IList<DesignedReportCondition> GetDesignedReportConditions(decimal reportID)
       {
           try
           {

               IList<DesignedReportCondition> designedReportConditionList = designedReportConditionRep.Find(r => r.Report.ID == reportID).ToList();

               return designedReportConditionList;
           }
           catch (Exception ex)
           {
               LogException(ex, "BDesignedReportCondition", "GetDesignedReportConditions");
               throw ex;
           }
       }
       protected override void GetReadyBeforeSave(DesignedReportCondition condition, UIActionType action)
       {
           if(action==UIActionType.ADD || action ==UIActionType.EDIT)
           {
               string[] removeEndItems = {" and " ," and"," or"," or "};
               string[] removeStartItems = { " and ", "and ", "or ", " or "};
               foreach (string item in removeEndItems)
               {
                   if (condition.ConditionValue.ToLower().EndsWith(item))
                   {
                       condition.ConditionValue = condition.ConditionValue.Substring(0, condition.ConditionValue.ToLower().LastIndexOf(item));
                       
                   }
                   if (condition.TrafficConditionValue.ToLower().EndsWith(item))
                   {
                       condition.TrafficConditionValue = condition.TrafficConditionValue.Substring(0, condition.TrafficConditionValue.ToLower().LastIndexOf(item));

                   }
                   
               }
               foreach (string item in removeStartItems)
               {
                   if (condition.ConditionValue.ToLower().StartsWith(item))
                   {
                       condition.ConditionValue = condition.ConditionValue.Remove(condition.ConditionValue.ToLower().IndexOf(item), item.Count());

                   }
                   if (condition.TrafficConditionValue.ToLower().StartsWith(item))
                   {
                       condition.TrafficConditionValue = condition.TrafficConditionValue.Remove(condition.TrafficConditionValue.ToLower().IndexOf(item), item.Count());

                   }
               }

           }
       }
       [ServiceAuthorizeBehavior(ServiceAuthorizeState.Avoid)]
       public decimal InsertCondition(DesignedReportCondition condition)
       {
           try
           {
               
               decimal id = this.SaveChanges(condition, UIActionType.ADD);
               return id;
           }
           catch (Exception ex)
           {
               LogException(ex, "BDesignedReportCondition", "InsertCondition");
               throw ex;
           }
       }
       [ServiceAuthorizeBehavior(ServiceAuthorizeState.Avoid)]
       public decimal DeleteCondition(DesignedReportCondition condition)
       {
           try
           {
               
               decimal id = this.SaveChanges(condition, UIActionType.DELETE);
               return id;
           }
           catch (Exception ex)
           {
               LogException(ex, "BDesignedReportCondition", "DeleteCondition");
               throw ex;
           }
       }
       [ServiceAuthorizeBehavior(ServiceAuthorizeState.Avoid)]
       public decimal UpdateCondition(DesignedReportCondition condition)
       {
           try
           {

               decimal id = this.SaveChanges(condition, UIActionType.EDIT);
               return id;
           }
           catch (Exception ex)
           {
               LogException(ex, "BDesignedReportCondition", "UpdateCondition");
               throw ex;
           }
       }
       protected override void InsertValidate(DesignedReportCondition obj)
       {
           
       }

       protected override void UpdateValidate(DesignedReportCondition obj)
       {
           
       }

       protected override void DeleteValidate(DesignedReportCondition obj)
       {
           throw new NotImplementedException();
       }
    }
}
