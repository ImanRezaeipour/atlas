using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure;
using GTS.Clock.Business.AppSettings;

namespace GTS.Clock.Business.Concept
{
    /// <summary>
    /// مفهوم
    /// </summary>
    public class BConcept : BaseBusiness<SecondaryConcept>
    {
        EntityRepository<SecondaryConcept> reportRep = new EntityRepository<SecondaryConcept>(false);

        /// <summary>
        /// کلیه مفایهم را بر اساس نوع آن بر می گرداند 
        /// </summary>
        /// <param name="periodicType">نوع مفهوم</param>
        /// <returns>لیست مفاهیم</returns>
        public IList<SecondaryConcept> GetAllConceptByPeriodicType(ScndCnpPeriodicType periodicType)
        {
            try
            {

                IList<SecondaryConcept> list = reportRep.Find(s => s.PeriodicType == periodicType).ToList<SecondaryConcept>();
                foreach (SecondaryConcept item in list)
                {

                    switch (BLanguage.CurrentLocalLanguage)
                    {
                        case LanguagesName.Parsi:
                            item.Name = item.FnName;
                            break;
                        case LanguagesName.English:
                            item.Name = item.EnName;
                            break;
                        default:
                            item.Name = item.FnName;
                            break;
                    }

                }

                return list;
            }
            catch (Exception ex)
            {
                LogException(ex, "BConcept", "GetAllConceptByPeriodicType");
                throw ex;
            }
        }
        
        /// <summary>
        /// اعتبارسنجی عملیات درج
        /// </summary>
        /// <param name="obj">مفهوم</param>
        protected override void InsertValidate(SecondaryConcept obj)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// اعتبارسنجی عملیات ویرایش
        /// </summary>
        /// <param name="obj">مفهوم</param>
        protected override void UpdateValidate(SecondaryConcept obj)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// اعتبارسنجی عملیات حذف
        /// </summary>
        /// <param name="obj">مفهوم</param>
        protected override void DeleteValidate(SecondaryConcept obj)
        {
            throw new NotImplementedException();
        }
    }
}
