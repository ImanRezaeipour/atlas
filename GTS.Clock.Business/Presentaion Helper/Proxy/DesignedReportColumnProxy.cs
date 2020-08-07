using GTS.Clock.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Business.Proxy
{
    public class DesignedReportColumnProxy
   {
       public  Decimal ID { get; set; }
       public  String Title { get; set; }
       public  String ColumnName { get; set; }
       public String Name { get; set; }
       public  Boolean Active { get; set; }
       public Decimal ReportID { get; set; }
       public Decimal ConceptID { get; set; }
       public  Int16 Order { get; set; }
       public Decimal PersonInfoID { get; set; }
       public  Boolean IsConcept { get; set; }
       public Boolean IsGroupColumn { get; set; }
       public String ColumnType { get; set; }

       public Decimal TrafficID { get; set; }
       public Decimal PersonParamID { get; set; }
       public String TrafficColumnCount { get; set; }

   }
}
