using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using AtlasEncryption;
using SKGL;
using GTS.Clock.Infrastructure.NHibernateFramework;
using System.Configuration;
using GTS.Clock.Infrastructure;

namespace GTS.Clock.Infrastructure
{
    public class ConfigurationHelper
    {
        public static void SetNHibernateSessionFactoryProps()
        {
            if (NHibernateSessionManager.Instance.SessionFactoryPropsDic.Count == 0)
            {
                string configPath = AppDomain.CurrentDomain.BaseDirectory + "\\Web.config";
                XmlDocument XmlDocumentConfig = new XmlDocument();
                XmlDocumentConfig.Load(configPath);

                XmlNamespaceManager namespaceManagerAtlas = new XmlNamespaceManager(XmlDocumentConfig.NameTable);
                namespaceManagerAtlas.AddNamespace("nh", "urn:nhibernate-configuration-2.2");
                XmlNode property = XmlDocumentConfig.SelectSingleNode("configuration/nh:hibernate-configuration/nh:session-factory", namespaceManagerAtlas);
                foreach (XmlNode item in XmlDocumentConfig.SelectSingleNode("configuration/nh:hibernate-configuration/nh:session-factory", namespaceManagerAtlas))
                {
                    if (item != null && item.Name == "property" && item.Attributes["name"].Value != null)
                    {
                        string itemInnerText = item.InnerText;
                        if (item.Attributes["name"].Value == "connection.connection_string")
                            itemInnerText = new CryptData(GetKeyFromMachinCode()).DecryptData(itemInnerText.Trim());
                        if (!NHibernateSessionManager.Instance.SessionFactoryPropsDic.Keys.Contains(item.Attributes["name"].Value))
                            NHibernateSessionManager.Instance.SessionFactoryPropsDic.Add(item.Attributes["name"].Value, itemInnerText.Trim());
                    }
                }
            }
        }

        public static string GetKeyFromMachinCode()
        {
            Generate skgl = new Generate();
            string machineCode = skgl.MachineCode.ToString();
            skgl.secretPhase = SimpleHash.ComputeHash(machineCode, HashStandards.MD5, Encoding.UTF8.GetBytes(machineCode));
            string key = skgl.doKey(0, Utility.Utility.GTSMinStandardDateTime, int.Parse(machineCode)).ToString();
            return key;
        }
        public static string GetKeyFromMachinCode(string machineCode)
        {
            Generate skgl = new Generate();
            //string machineCode = skgl.MachineCode.ToString();
            skgl.secretPhase = SimpleHash.ComputeHash(machineCode, HashStandards.MD5, Encoding.UTF8.GetBytes(machineCode));
            string key = skgl.doKey(0, Utility.Utility.GTSMinStandardDateTime, int.Parse(machineCode)).ToString();
            return key;
        }
        public static string GetLogDBConnectionString()
        {
            //DNN Note
            return ConfigurationManager.ConnectionStrings["log4netConnectionStr"].ConnectionString;
            //return new CryptData(GetKeyFromMachinCode()).DecryptData(ConfigurationManager.ConnectionStrings["log4netConnectionStr"].ConnectionString);
        }
    }

    

}
