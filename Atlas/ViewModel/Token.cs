using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Atlas.Api.ViewModel
{
    public class Token
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public DateTime LastActivityTime { get; set; }
    }
}