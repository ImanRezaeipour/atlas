using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GTS.Clock.Presentation.WebApi.Models
{
    public class TreeItem
    {
        public int id { get; set; }

        public int parentid { get; set; }
        public string text { get; set; }
    }
}