using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Model.BaseInformation;

namespace GTS.Clock.Business.BaseInformation
{
    /// <summary>
    /// داشبورد
    /// </summary>
    public class BDashboards : BaseBusiness<Dashboards>
    {
        /// <summary>
        /// اعتبار سنجی عملیات درج
        /// </summary>
        /// <param name="clCar"></param>
        protected override void InsertValidate(Dashboards clCar)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// اعتبار سنجی عملیات ویرایش
        /// </summary>
        /// <param name="clCar"></param>
        protected override void UpdateValidate(Dashboards clCar)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// اعتبارسنجی عملیات حذف
        /// </summary>
        /// <param name="clCar"></param>
        protected override void DeleteValidate(Dashboards clCar)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// کلیه داشبورد ها را بر می گرداند
        /// </summary>
        /// <returns></returns>
        public override IList<Dashboards> GetAll()
        {
            return base.GetAll().Where(d => d.SubSystemID == 1).ToList();
        }
    }
}
