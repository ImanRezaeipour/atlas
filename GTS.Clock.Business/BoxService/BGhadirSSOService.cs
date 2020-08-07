using GTS.Clock.Business.Security;
using GTS.Clock.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Business.BoxService
{
    /// <summary>
    /// 
    /// </summary>
   public class BGhadirSSOService
    {
       /// <summary>
       /// 
       /// </summary>
       GhadirSSOService.GhadirSSOServiceClient ssoServiceClient;
       
       /// <summary>
       /// 
       /// </summary>
       public BGhadirSSOService()
       {
           try
           {
               ssoServiceClient = new GhadirSSOService.GhadirSSOServiceClient();
           }



           catch (Exception ex)
           {
               
               throw ex;
           }
       }

       /// <summary>
       /// 
       /// </summary>
       /// <param name="barcode"></param>
       /// <returns></returns>
       public string GetKeySSOByBarcode(string barcode)
       {
           try
           {
               string licenceKey=System.Configuration.ConfigurationManager.AppSettings["GhadirSSOServiceLicence"];
               string encryptionKey = System.Configuration.ConfigurationManager.AppSettings["GhadirSSOEncryptionKey"];
               string encryptedLicenceKey = new AtlasEncryption.CryptData(encryptionKey).EncryptData(licenceKey);
               string key = ssoServiceClient.GetEncryptedKey(barcode, encryptedLicenceKey);
               return key;
           }
           catch (Exception ex)
           {
               BaseBusiness<Entity>.LogException(ex, "BGhadirSSOService", "GetKeySSOByBarcode");
               throw ex;
           }
       }

       /// <summary>
       /// 
       /// </summary>
       /// <returns></returns>
       public string GetWebRestAddressURL()
       {
           try
           {
               string webRestURL=System.Configuration.ConfigurationManager.AppSettings["WebRestLoginUrl"];
               return webRestURL;
           }
           catch (Exception ex)
           {
               BaseBusiness<Entity>.LogException(ex, "BGhadirSSOService", "GetWebRestAddressURL");
               throw ex;
           }
       }

       /// <summary>
       /// 
       /// </summary>
       [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
       public void CheckGhadirSSOServiceLoadAccess()
       {
       }
    }
}
