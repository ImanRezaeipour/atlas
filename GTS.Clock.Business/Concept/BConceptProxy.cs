using GTS.Clock.Business.Proxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Infrastructure.Repository;

namespace GTS.Clock.Business.Concept
{
   public class BConceptProxy
    {

       public BConceptProxy()
       { }
      
       public IList<RuleGeneratorConceptProxy> RuleGeneratorConcept()
       {
           EntityRepository<SecondaryConcept> reportRep = new EntityRepository<SecondaryConcept>(false);
           SecondaryConcept SecConcept=new SecondaryConcept();
           IList<SecondaryConcept> SecondaryConceptlist = reportRep.GetAll();
           IList<RuleGeneratorConceptProxy> RuleGeneratorConceptlist = new List<RuleGeneratorConceptProxy>();
           foreach (SecondaryConcept item in SecondaryConceptlist)
           {
               RuleGeneratorConceptProxy RuleGeneratorConceptObj = new RuleGeneratorConceptProxy();
               DataValue DataValueList = new DataValue();
               DataValueList.ConceptCode = item.IdentifierCode;
               DataValueList.ID = item.ID;
               RuleGeneratorConceptObj.ConceptID = item.ID;
               RuleGeneratorConceptObj.ConceptName = item.Name;
               RuleGeneratorConceptObj.ConceptENName = item.EnName;
               RuleGeneratorConceptObj.ConceptType = Convert.ToInt32(item.PeriodicType);
               RuleGeneratorConceptObj.DataValueObj = DataValueList;
               RuleGeneratorConceptlist.Add(RuleGeneratorConceptObj);
           }
           return RuleGeneratorConceptlist;
       }
    }
}
