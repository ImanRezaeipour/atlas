using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Model.UIValidation
{
    public class CustomValidation : IEntity
    {
      public virtual decimal ID { get; set; }

      public virtual string AssemblyName { get; set; }

      public virtual string ClassName { get; set; }

      public virtual string FieldName { get; set; }

      public virtual int DataType { get; set; }

      public virtual bool AllowNull { get; set; }

      public virtual bool AllowDuplicate { get; set; }

      public virtual int Length { get; set; }

      public virtual string Expression { get; set; }

      public virtual string Format { get; set; }
      public virtual int SubSystem { get; set; }
      public virtual string Description { get; set; }
    }
}
