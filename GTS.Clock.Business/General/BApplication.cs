using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Model.General;

namespace GTS.Clock.Business.General
{
    /// <summary>
    /// ورژن دیتابیس
    /// </summary>
    public class BApplication : BaseBusiness<VersionStatus>
    {
        /// <summary>
        /// آخرین نسخه به روز رسانی شده را از دیتابیس بر می گرداند
        /// </summary>
        /// <returns></returns>
        public VersionStatus GetLastDatabaseVersion()
        {
            VersionStatus versionStatusObj=new VersionStatus();
            try
            {
                 versionStatusObj = GetAll().OrderByDescending(o => Convert.ToDecimal(o.Version.Replace(".", ""))).FirstOrDefault();
            }
            catch (Exception)
            {

                versionStatusObj = null;
            }
            
            return versionStatusObj;

        }

        /// <summary>
        /// اعتبار سنجی عملیات درج
        /// </summary>
        /// <param name="obj"></param>
        protected override void InsertValidate(VersionStatus obj)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// اعتبار سنجی عملیات ویرایش
        /// </summary>
        /// <param name="obj"></param>
        protected override void UpdateValidate(VersionStatus obj)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// اعتبار سنجی عملیات حذف
        /// </summary>
        /// <param name="obj"></param>
        protected override void DeleteValidate(VersionStatus obj)
        {
            throw new NotImplementedException();
        }
    }
}
