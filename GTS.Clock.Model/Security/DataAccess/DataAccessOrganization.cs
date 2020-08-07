﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Model.BaseInformation;

namespace GTS.Clock.Model.Security
{
    public class DACorporation
    {
        #region Properties
        /// <summary>
        /// Gets or sets the ID value.
        /// </summary>
        public virtual Decimal ID { get; set; }

        /// <summary>
        /// Gets or sets the UserID value.
        /// </summary>
        public virtual Decimal UserID { get; set; }

        /// <summary>
        /// Gets or sets the DoctorID value.
        /// </summary>
        public virtual Decimal? CorporationID { get; set; }

        public virtual Corporation Corporation { get; set; }
		public virtual User User { get; set; }
        
        #endregion		

    }
}
