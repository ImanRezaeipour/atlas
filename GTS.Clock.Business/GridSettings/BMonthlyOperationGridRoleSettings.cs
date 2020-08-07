using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Model.UI;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Infrastructure;
using GTS.Clock.Model.AppSetting;
using GTS.Clock.Model.Security;


namespace GTS.Clock.Business.GridSettings
{
    /// <summary>
    /// 
    /// </summary>
    public class BMonthlyOperationGridRoleSettings : BaseBusiness<MonthlyOperationGridRoleSettings>
    {
        private const string ExceptionSrc = "GTS.Clock.Business.GridSettings.BMonthlyOperationGridRoleSettings";
        private EntityRepository<MonthlyOperationGridRoleSettings> rep = new EntityRepository<MonthlyOperationGridRoleSettings>(false);
        private UserRepository userRep = new UserRepository(false);
        private ISession NHSession = NHibernateSessionManager.Instance.GetSession();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public IList<MonthlyOperationGridRoleSettingsProxy> GetMonthlyOperationGridRoleSettings(decimal roleId)
        {
            try
            {
                IList<MonthlyOperationGridRoleSettingsProxy> MonthlyOperationGridRoleSettingsProxyList = new List<MonthlyOperationGridRoleSettingsProxy>();
                MonthlyOperationGridRoleSettings monthlyOperationGridRoleSettings = NHSession.QueryOver<MonthlyOperationGridRoleSettings>()
                                                                                             .Where(x => x.Role.ID == roleId)
                                                                                             .SingleOrDefault();
                if (monthlyOperationGridRoleSettings == null)
                {
                    foreach (GridRoleSettings setting in Enum.GetValues(typeof(GridRoleSettings)))
                    {
                        MonthlyOperationGridRoleSettingsProxy monthlyOperationGridRoleSettingsProxy = new MonthlyOperationGridRoleSettingsProxy();
                        monthlyOperationGridRoleSettingsProxy.ID = 0;
                        monthlyOperationGridRoleSettingsProxy.RoleID = roleId;
                        monthlyOperationGridRoleSettingsProxy.ViewState = true;
                        monthlyOperationGridRoleSettingsProxy.GridColumn = setting.ToString();
                        monthlyOperationGridRoleSettingsProxy.Exist = false;
                        MonthlyOperationGridRoleSettingsProxyList.Add(monthlyOperationGridRoleSettingsProxy);
                    }
                }
                else
                {
                    foreach (PropertyInfo pInfo in typeof(MonthlyOperationGridRoleSettings).GetProperties())
                    {
                        foreach (GridRoleSettings setting in Enum.GetValues(typeof(GridRoleSettings)))
                        {
                            if (pInfo.Name == setting.ToString())
                            {
                                MonthlyOperationGridRoleSettingsProxy monthlyOperationGridRoleSettingsProxy = new MonthlyOperationGridRoleSettingsProxy();
                                monthlyOperationGridRoleSettingsProxy.ID = monthlyOperationGridRoleSettings.ID;
                                monthlyOperationGridRoleSettingsProxy.RoleID = monthlyOperationGridRoleSettings.Role.ID;
                                monthlyOperationGridRoleSettingsProxy.ViewState = (bool)pInfo.GetValue(monthlyOperationGridRoleSettings, null);
                                monthlyOperationGridRoleSettingsProxy.GridColumn = pInfo.Name;
                                monthlyOperationGridRoleSettingsProxy.Exist = true;
                                MonthlyOperationGridRoleSettingsProxyList.Add(monthlyOperationGridRoleSettingsProxy);
                            }
                        }
                    }
                }

                return MonthlyOperationGridRoleSettingsProxyList;
            }
            catch (Exception ex)
            {
                LogException(ex, "BRole", "GetMonthlyOperationGridRoleSettings");
                throw ex;
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="roleId"></param>
        /// <param name="exist"></param>
        /// <param name="SettingsColArray"></param>
        /// <param name="MonthlyOperationGridColumn"></param>
        public void UpdateGridMonthlyOperationGridRoleSettings(decimal id, decimal roleId, bool exist, Dictionary<string, string> SettingsColArray, Dictionary<string, string> MonthlyOperationGridColumn)
        {
            using (NHibernateSessionManager.Instance.BeginTransactionOn())
            {
                try
                {
                    MonthlyOperationGridRoleSettings monthlyOperationGridRoleSettings = null;
                    IList<MonthlyOperationGridClientSettings> monthlyOperationGridClientSettingList = new List<MonthlyOperationGridClientSettings>();
                    switch (exist)
                    {
                        case true:
                            monthlyOperationGridRoleSettings = new MonthlyOperationGridRoleSettings() { ID = id };
                            monthlyOperationGridRoleSettings.Role = new Model.Security.Role() { ID = roleId };
                            foreach (PropertyInfo pInfo in typeof(MonthlyOperationGridRoleSettings).GetProperties())
                            {
                                foreach (GridRoleSettings setting in Enum.GetValues(typeof(GridRoleSettings)))
                                {
                                    if (setting.ToString() == pInfo.Name)
                                        pInfo.SetValue(monthlyOperationGridRoleSettings, bool.Parse(SettingsColArray[MonthlyOperationGridColumn[pInfo.Name]]), null);
                                }
                            }
                            rep.WithoutTransactUpdate(monthlyOperationGridRoleSettings);
                            this.UpdateMonthlyOperationGridClientSettings(roleId, monthlyOperationGridRoleSettings);
                            break;
                        case false:
                            monthlyOperationGridRoleSettings = new MonthlyOperationGridRoleSettings();
                            monthlyOperationGridRoleSettings.Role = new Model.Security.Role() { ID = roleId };
                            foreach (PropertyInfo pInfo in typeof(MonthlyOperationGridRoleSettings).GetProperties())
                            {
                                foreach (GridRoleSettings setting in Enum.GetValues(typeof(GridRoleSettings)))
                                {
                                    if (setting.ToString() == pInfo.Name)
                                        pInfo.SetValue(monthlyOperationGridRoleSettings, bool.Parse(SettingsColArray[MonthlyOperationGridColumn[pInfo.Name]]), null);
                                }

                            }
                            rep.WithoutTransactSave(monthlyOperationGridRoleSettings);
                            this.UpdateMonthlyOperationGridClientSettings(roleId, monthlyOperationGridRoleSettings);
                            break;
                    }
                    NHibernateSessionManager.Instance.CommitTransactionOn();
                }
                catch (Exception ex)
                {
                    NHibernateSessionManager.Instance.RollbackTransactionOn();
                    LogException(ex, "BRole", "UpdateGridMonthlyOperationGridRoleSettings");
                    throw ex;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="monthlyOperationGridRoleSettings"></param>
        public void UpdateMonthlyOperationGridClientSettings(decimal roleId, MonthlyOperationGridRoleSettings monthlyOperationGridRoleSettings)
        {
            try
            {
                UserSettings userSettingAlias = null;
                User userAlias = null;
                Role roleAlias = null;
                IList<UserSettings> userSettingList = NHSession.QueryOver(() => userSettingAlias)
                                                                          .JoinAlias(() => userSettingAlias.User, () => userAlias)
                                                                          .JoinAlias(() => userAlias.Role, () => roleAlias)
                                                                          .Where(() => roleAlias.ID == roleId && roleAlias.Active)
                                                                          .List<UserSettings>();
                BGridMonthlyOperationClientSettings bGridMonthlyOperationClientSettings = new BGridMonthlyOperationClientSettings();
                bGridMonthlyOperationClientSettings.UpdateMonthlyOperationGridClientSettings(userSettingList, monthlyOperationGridRoleSettings);
            }
            catch (Exception ex)
            {
                LogException(ex, "BRole", "UpdateMonthlyOperationGridClientSettings");
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        protected override void InsertValidate(MonthlyOperationGridRoleSettings obj)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        protected override void UpdateValidate(MonthlyOperationGridRoleSettings obj)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        protected override void DeleteValidate(MonthlyOperationGridRoleSettings obj)
        {
            throw new NotImplementedException();
        }
    }
}
