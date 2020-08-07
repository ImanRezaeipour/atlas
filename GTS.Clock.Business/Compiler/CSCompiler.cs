using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using ComponentArt.Web.UI;
using GTS.Clock.Business.Presentaion_Helper.Proxy;
using GTS.Clock.Business.RuleDesigner;
using GTS.Clock.Infrastructure.CompilerFramework;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Model;
using GTS.Clock.Model.Concepts;
using Microsoft.CSharp;
using GTS.Clock.Business.Security;
using GTS.Clock.Infrastructure.NHibernateFramework;

namespace GTS.Clock.Business.Compiler
{
    /// <summary>
    /// کامپایل قوانین ذخیره شده در دیتابیس
    /// </summary>
    public class CsCompiler:MarshalByRefObject
    {
        private NHibernate.ISession NHSession = NHibernateSessionManager.Instance.GetSession();
        public string ReferenceDirectory { get; set; }

        /// <summary>
        /// مسیر کامل خروجی dll
        /// </summary>
        public string OutputDLLFullPath { get; set; }

        /// <summary>
        /// مسیر کامل خرجی کد سی شارپ
        /// </summary>
        public string OutputCSharpCodeFullPath { get; set; }
         
        /// <summary>
        /// 
        /// </summary>
        public bool GenerateExecutable { get; set; }
        
        /// <summary>
        /// سازنده کلاس
        /// </summary>
        public CsCompiler()
        {
            var cConfig = new CompilerConfig();
            ReferenceDirectory = cConfig.ReadRuleGeneratorPath();
            OutputDLLFullPath = cConfig.ReadRuleGeneratorPath() + "\\GTS.Clock.Business.UserCalculator.DLL";
            OutputCSharpCodeFullPath = cConfig.ReadRuleGeneratorPath() + "\\GTS.Clock.Business.UserCalculator.CS";
        }


        /// <summary>
        /// کلیه قوانین ساخته شده را کامپایل و تبدیل به یک دی ال ال می کند
        /// </summary>
        /// <returns></returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public string GenerateAndCompile()
        {
            
            #region Source Main Text

            string sourceTextMain =
@"
using System;
using System.Collections.Generic;
using System.Linq;
using GTS.Clock.ModelEngine;
using GTS.Clock.ModelEngine.ELE;
using GTS.Clock.ModelEngine.Concepts;
using GTS.Clock.ModelEngine.Concepts.Operations;
using GTS.Clock.Infrastructure.Utility;

namespace GTS.Clock.Business.DesignedCalculator
{
    public static class DesignedRuleCalculator
    {
        //#region Rules
        ###RULES###
        //#endregion
    }

    public static class DesignedConceptCalculator
    {
       #region Concepts
       ###CONCEPTS###
       #endregion

    }

}";
            #endregion

            #region CONCEPTS

            string sourceTextConcepts =
            @"
        /// <summary>
        /// ###SUMMARY###
        /// </summary>
        /// <param name=""MyRule""></param>
        public static void ###IDENTIFIER###(this GTS.Clock.Business.Calculator.ConceptCalculator calculator, BaseScndCnpValue Result, SecondaryConcept MyConcept)
        {
            ###CSHARPSOURCE###
        }
";
            var Conceptsource = new EntityRepository<SecondaryConcept>().GetAll().Where(x => x.UserDefined)
    .Select(cnp => new RuleConceptCompileProxy()
    {
        Identifier = cnp.IdentifierCode.ToString(CultureInfo.InvariantCulture),
        CSharpCode = cnp.CSharpCode,
        Summery = cnp.FnName,
    })
    .ToList();

            string strConcepts = string.Empty;
            foreach (var ruleConceptCompileProxy in Conceptsource)
            {
                var tt = sourceTextConcepts.Replace("###SUMMARY###", " C" + ruleConceptCompileProxy.Summery + "Summary");
                tt = tt.Replace("###IDENTIFIER###", "C" + ruleConceptCompileProxy.Identifier);
                tt = tt.Replace("###CSHARPSOURCE###", ruleConceptCompileProxy.CSharpCode);
                strConcepts += tt;
            }
            sourceTextMain = sourceTextMain.Replace("###CONCEPTS###", strConcepts);
            #endregion

            #region RULES
            string sourceTextMethods =
            @"
        /// <summary>
        /// ##SUMMARY###
        /// </summary>
        /// <param name=""MyRule""></param>
        public static void ###IDENTIFIER###(this GTS.Clock.Business.Calculator.RuleCalculator calculator, AssignedRule MyRule)
        {
            ###CSHARPSOURCE###
        }
            ";

            //var Rulesource = new EntityRepository<DesignedRule>()
            //    //var Rulesource = new EntityRepository<RuleTemplate>()
            //    //.GetAll().Where(x => x.UserDefined)
            //    .GetAll().Select(cnp => new RuleConceptCompileProxy()
            //    {
            //        //Identifier = cnp.RuleTemplateID.IdentifierCode.ToString(CultureInfo.InvariantCulture),
            //        //CSharpCode = cnp.CSharpCode,
            //        //Summery = cnp.RuleTemplateID.Name
            //          Identifier = cnp.RuleTemplate.IdentifierCode.ToString(CultureInfo.InvariantCulture),
            //            CSharpCode = cnp.CSharpCode,
            //            Summery = cnp.RuleTemplate.Name
            //    })
            //    .ToList();


            IList<RuleConceptCompileProxy> Rulesource = new List<RuleConceptCompileProxy>();
            DesignedRule designedRuleAlias = null;
            RuleTemplate RuleTemplateAlias = null;
            IList<DesignedRule> designedRuleList = this.NHSession.QueryOver<DesignedRule>(() => designedRuleAlias)
                                                                 .JoinAlias(() => designedRuleAlias.RuleTemplate, () => RuleTemplateAlias)
                                                                 .List<DesignedRule>();
            if (designedRuleList != null && designedRuleList.Count > 0)
            {
                designedRuleList.ToList<DesignedRule>().ForEach(x =>
                {
                    Rulesource.Add(new RuleConceptCompileProxy()
                    {
                        Identifier = x.RuleTemplate.IdentifierCode.ToString(CultureInfo.InvariantCulture),
                        CSharpCode = x.CSharpCode,
                        Summery = x.RuleTemplate.Name
                    });
                });
            } 




            string strMethods = string.Empty;
            foreach (var ruleConceptCompileProxy in Rulesource)
            {
                var tt = sourceTextMethods.Replace("###SUMMARY###", "R" + ruleConceptCompileProxy.Summery + "Summary");
                tt = tt.Replace("###IDENTIFIER###", "R" + ruleConceptCompileProxy.Identifier);
                tt = tt.Replace("###CSHARPSOURCE###", ruleConceptCompileProxy.CSharpCode);

                strMethods += tt;
            }
            sourceTextMain = sourceTextMain.Replace("###RULES###", strMethods);
            #endregion

            var providerOptions = new Dictionary<string, string>
            {
                {"CompilerVersion", "v4.0"}
            };
            CSharpCodeProvider provider = new CSharpCodeProvider(providerOptions);

            CompilerParameters compilerParams = new CompilerParameters
            {
                OutputAssembly = OutputDLLFullPath,
                GenerateInMemory = false,
                GenerateExecutable = false
            };

            #region ReferencedAssemblies

            compilerParams.ReferencedAssemblies.Add("mscorlib.dll");
            compilerParams.ReferencedAssemblies.Add("System.dll");
            compilerParams.ReferencedAssemblies.Add("System.Core.dll");
            compilerParams.ReferencedAssemblies.Add("System.Data.dll");
            compilerParams.ReferencedAssemblies.Add("System.Data.DataSetExtensions.dll");
            compilerParams.ReferencedAssemblies.Add("System.Xml.dll");
            compilerParams.ReferencedAssemblies.Add("System.Xml.Linq.dll");

            var refDlls = new List<string>()
            {
                "GTS.Clock.Business.dll",
                "GTS.Clock.Infrastructure.dll",
                "GTS.Clock.Infrastructure.RepositoryEngine.dll",
                "GTS.Clock.ModelEngine.dll",
                "log4net.dll",
                "GTS.Clock.Business.Calculator.dll",
                "GTS.Clock.Business.GeneralCalculator.dll"
            };

            foreach (var refDll in refDlls)
            {
                var dllPath = string.Format("{0}\\{1}", AppDomain.CurrentDomain.BaseDirectory + "RuleGenerator\\bin", refDll);
                if (System.IO.File.Exists(dllPath)) compilerParams.ReferencedAssemblies.Add(dllPath);
            }

            #endregion

            CompilerResults results = provider.CompileAssemblyFromSource(compilerParams, sourceTextMain);

            var message = string.Empty;
            if (results.Errors.Count == 0)
            {
                using (StreamWriter streamWriter = new StreamWriter(OutputCSharpCodeFullPath))
                {
                    streamWriter.Write(sourceTextMain);
                }
            }
            else
            {
                using (StreamWriter streamWriter = new StreamWriter(OutputCSharpCodeFullPath + ".LOG"))
                {
                    for (int index = 0; index < results.Errors.Count; index++)
                    {
                        CompilerError err = results.Errors[index];
                        streamWriter.Write(err.ErrorText);
                        message += "\r\n" + err.ErrorText;
                    }
                }
            }

            return message;
        }

    }
}
