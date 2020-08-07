using GTS.Clock.Infrastructure;
using GTS.Clock.Model.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Model.Report
{
   public class DesignedReportColumn:IEntity 
    {

       public virtual Decimal ID { get ;set; }
       public virtual String Title { get; set; }
       public virtual String Name { get; set; }
       public virtual Boolean Active { get; set; }
       public virtual Report Report { get; set; }
       public virtual Concepts.SecondaryConcept Concept { get; set; }
       public virtual Int16 Order { get; set; }
       public virtual DesignedReportPersonInfoColumn PersonInfo { get; set; }
       public virtual Boolean IsConcept { get; set; }
       public virtual Boolean IsGroupColumn { get; set; }
       public virtual String ColumnName { get; set; }
       public virtual DesignedReportColumnType ColumnType { get; set; }

       public virtual DesignedReportTrafficColumn Traffic { get; set; }
       public virtual PersonParamField PersonParam { get; set; }
       public virtual int TrafficColumnCount { get; set; }
    }
    //public class DesignedReportColumnConceptKeyComparer : IEqualityComparer<DesignedReportColumn>
    //{
    //    public bool Equals(DesignedReportColumn x, DesignedReportColumn y)
    //    {
    //        bool isEqual = false;
    //        if (x.Concept != null && y.Concept != null)
    //        {
    //            if (x.Concept.KeyColumnName == y.Concept.KeyColumnName)
    //                isEqual = true;
    //        }
    //        return isEqual;
    //    }

    //    public int GetHashCode(DesignedReportColumn obj)
    //    {
    //        if (Object.ReferenceEquals(obj, null) || obj.Concept==null)
    //            return 0;
    //        return obj.Concept.KeyColumnName.GetHashCode();
    //    }
    //}
    public class DesignedReportColumnConceptKeyComparer : IEqualityComparer<DesignedReportColumn>
    {
        public bool Equals(DesignedReportColumn x, DesignedReportColumn y)
        {
            bool isEqual = false;
            if (x.Concept != null && y.Concept != null)
            {
                if (x.Concept.ID == y.Concept.ID)
                    isEqual = true;
            }
            return isEqual;
        }

        public int GetHashCode(DesignedReportColumn obj)
        {
            if (Object.ReferenceEquals(obj, null) || obj.Concept == null)
                return 0;
            return obj.Concept.ID.GetHashCode();
        }
    }
}
