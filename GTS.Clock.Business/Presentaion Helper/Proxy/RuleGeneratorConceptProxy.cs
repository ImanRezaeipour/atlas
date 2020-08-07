using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Business.Proxy
{
    public class RuleGeneratorConceptProxy
    {
        public decimal ConceptID { get; set; }
        public string ConceptName { get; set; }
        public string ConceptENName { get; set; }
        public int ConceptType { get; set; }
        public DataValue DataValueObj { get; set; }
    }
    public class DataValue
    {
        public decimal ID { get; set; }
        public decimal ConceptCode { get; set; }
    }
}
