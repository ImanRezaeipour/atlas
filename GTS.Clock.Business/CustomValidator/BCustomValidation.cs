using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Model.UIValidation;
using NHibernate;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Infrastructure;

namespace GTS.Clock.Business.CustomValidator
{
    /// <summary>
    /// اعتبارسنجی سفارشی
    /// </summary>
   public class BCustomValidation:BaseBusiness<CustomValidation>
    {
       private ISession NHSession = NHibernateSessionManager.Instance.GetSession();

       /// <summary>
       /// سازنده کلاس
       /// </summary>
       public BCustomValidation()
       {

       }

       /// <summary>
       /// اعتبارسنجی های سفارشی را در سشن نگهداری می کند
       /// </summary>
       public void GetCustomValidation()
       {
           try
           {
               IList<CustomValidation> CustomValidationList = new List<CustomValidation>();
               CustomValidationList = NHSession.QueryOver<CustomValidation>()
                   .Where(current => current.SubSystem == 1)
                   .List<CustomValidation>();
               if(CustomValidationList.Count > 0)
               {
                    SessionHelper.SaveSessionValue(SessionHelper.BussinessCustomValidation, CustomValidationList);
               }
              
           }
           catch(Exception ex)
           {
               LogException(ex, "BCustomValidation", "GetCustomValidation");
               throw ex;
           }
            
       }

       /// <summary>
       /// اعتبارسنجی عملیات درج
       /// </summary>
       /// <param name="clCar"></param>
       protected override void InsertValidate(CustomValidation clCar)
       {
          
       }

       /// <summary>
       /// اعتبارسنجی عملیات ویرایش
       /// </summary>
       /// <param name="clCar"></param>
       protected override void UpdateValidate(CustomValidation clCar)
       {
          
       }

       /// <summary>
       /// اعتبارسنجی عملیات حذف
       /// </summary>
       /// <param name="clCar">اعتبارسنجی سفارشی</param>
       protected override void DeleteValidate(CustomValidation clCar)
       {
           
       }

       /// <summary>
       /// عملیات ویرایش اعتبارسنجی سفارشی در دیتابیس
       /// </summary>
       /// <param name="clCar">اعتبارسنجی سفارشی</param>
       protected override void UpdateCustomValidate(CustomValidation clCar)
       {
           
       }

       /// <summary>
       /// عملیات درج اعتبارسنجی سفارشی در دیتابیس
       /// </summary>
       /// <param name="clCar">اعتبارسنجی سفارشی</param>
       protected override void InsertCustomValidate(CustomValidation clCar)
       {
          
       }
    }
}
