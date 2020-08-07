using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Atlas.Api.ViewModel
{
    public class DataTablePageData<T>
         where T : new()
    {
        public IEnumerable<T> data { get; set; }
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public string error { get; set; }
    }
}