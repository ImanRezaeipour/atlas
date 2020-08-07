using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GTS.Clock.Presentation.WebApi.Models
{
    public class Token
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public DateTime LastActivityTime { get; set; }
    }
}