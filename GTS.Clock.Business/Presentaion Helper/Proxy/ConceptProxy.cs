using GTS.Clock.Infrastructure;
using GTS.Clock.Model.Concepts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Business.Proxy
{
    public class ConceptProxy
    {
        public ConceptProxy()
        {

        }
        public ConceptProxy(SecondaryConcept concept)
        {
            ID = concept.ID;
            FnName = concept.FnName;
            EnName = concept.EnName;
            Name = concept.Name;

        }
        public decimal ID { get; set; }
        public string FnName { get; set; }
        public string EnName { get; set; }
        public string Name { get; set; }
        public DesignedReportColumnType ColumnType { get; set; }

        public string KeyColumn { get; set; }
    }
}
