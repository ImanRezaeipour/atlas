using GTS.Clock.Infrastructure;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Model;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace GTS.Clock.Business.Temp
{
    public class BTemp : BaseBusiness<GTS.Clock.Model.Temp.Temp>
    {
        TempRepository tempRepository = new TempRepository(false);
        protected override void InsertValidate(Model.Temp.Temp clCar)
        {
        }

        protected override void UpdateValidate(Model.Temp.Temp clCar)
        {
        }

        protected override void DeleteValidate(Model.Temp.Temp clCar)
        {
        }

        public string InsertTempList(IList<decimal> objectIDList)
        {
            try
            {
                return this.tempRepository.InsertTempList(objectIDList);
            }
            catch (Exception ex)
            {                
                BaseBusiness<Entity>.LogException(ex, "BTemp", "DeleteTempList");
                throw ex;
            }
        }

        public void DeleteTempList(string operationGUID)
        {
            try
            {
                this.tempRepository.DeleteTempList(operationGUID);
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BTemp", "DeleteTempList");
                throw ex;
            }
        }

        public void OrganizeTemp()
        {
            try
            {
                this.tempRepository.OrganizeTemp();
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BTemp", "OrganizeTemp");
                throw ex;
            }
        }

        public void ClearChartTempImagesDirectory()
        {
            try
            {
                this.tempRepository.ClearChartTempImagesDirectory();
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BTemp", "ClearChartTempImagesDirectory");                
                throw;
            }
        }

        public string GetApplicationVersion()
        {
            string version = string.Empty;
            return version;
        }

    }
}
