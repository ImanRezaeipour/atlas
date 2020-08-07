using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.IO;

namespace GTS.Clock.Infrastructure.CompilerFramework
{
    public class CompilerConfig
    {
        public string ReadRuleGeneratorPath()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + AppFolders.RuleGenerator;
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            return path;
        }

    }
}
