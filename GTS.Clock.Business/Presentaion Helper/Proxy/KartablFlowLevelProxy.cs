﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure;

namespace GTS.Clock.Business.Proxy
{
    public class KartablFlowLevelProxy
    {
        public string ManagerName { get; set; }

        public string TheDate { get; set; }

        public string TheTime { get; set; }

        public RequestState RequestStatus { get; set; }

        public string Description { get; set; }

        public decimal RequestStatusID { get; set; }

        public string Applicator { get; set; }
    }

}
