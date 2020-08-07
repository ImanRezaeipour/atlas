using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GTS.Clock.Presentation.WebApi.Models
{
    public class SelectPageData
    {
        public IEnumerable<SelectItem> items { get; set; }
        public int total_count { get; set; }
    }
}