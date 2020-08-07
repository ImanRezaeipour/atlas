using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Atlas.Api.ViewModel
{
    public class TreeItem
    {
        public int id { get; set; }

        public int parentid { get; set; }
        public string text { get; set; }
    }
}