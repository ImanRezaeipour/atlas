using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Atlas.ServiceProvider.Proxy
{
    [DataContract]
    public class TreeItem
    {
        [DataMember]
        public int id { get; set; }

        [DataMember]
        public string parent { get; set; }

        [DataMember]
        public string text { get; set; }
    }
}