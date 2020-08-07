using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Model.Concepts;

namespace GTS.Clock.Business.Presentaion_Helper.Proxy
{
    /// <summary>
    /// 
    /// </summary>
    public class SecondaryConceptProxy
    {
        public virtual Decimal ID { get; set; }
        public virtual Decimal? Parent_ID { get; set; }
        public virtual String ScriptBeginFa { get; set; }
        public virtual String ScriptEndFa { get; set; }
        public virtual String ScriptBeginEn { get; set; }
        public virtual String ScriptEndEn { get; set; }
        public virtual Boolean AddOnParentCreation { get; set; }
        public virtual Boolean CanAddToFinal { get; set; }
        public virtual Boolean CanEditInFinal { get; set; }
        public virtual Boolean Visible { get; set; }
        public virtual int SortOrder { get; set; }

        public virtual decimal DailyConcept { get; set; }


        //public static SecondaryConcept Export(SecondaryConceptProxy secondaryConceptProxy)
        //{
        //    return new SecondaryConcept()
        //        {
        //            ID = secondaryConceptProxy.ID,
        //            p = secondaryConceptProxy.Parent_ID,
        //            ScriptBeginFa = secondaryConceptProxy.ScriptBeginFa,
        //            ScriptEndFa = secondaryConceptProxy.ScriptEndFa,
        //            ScriptBeginEn = secondaryConceptProxy.ScriptBeginEn,
        //            ScriptEndEn = secondaryConceptProxy.ScriptEndEn,
        //            AddOnParentCreation = secondaryConceptProxy.AddOnParentCreation,
        //            CanAddToFinal = secondaryConceptProxy.CanAddToFinal,
        //            CanEditInFinal = secondaryConceptProxy.CanEditInFinal,
        //            Visible = secondaryConceptProxy.Visible,
        //            SortOrder = secondaryConceptProxy.SortOrder,
        //        };
        //}
    }
}
