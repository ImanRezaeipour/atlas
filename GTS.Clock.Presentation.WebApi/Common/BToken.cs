using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GTS.Clock.Presentation.WebApi.Common
{
    public class BToken
    {
        private string CryptKey = "@tlasWEB@PI";
        public BToken()
        {

        }

        public string Generate(string username)
        {
            CryptData crypto = new CryptData(CryptKey);
            return crypto.EncryptData(Guid.NewGuid().ToString()) + ";" + crypto.EncryptData(username) + ";" + crypto.EncryptData(DateTime.Now.ToString()) + ";ToKEn";
        }

        public bool Validate(string token)
        {
            CryptData crypto = new CryptData(CryptKey);
            var tokenArray = token.Split(';');

            var guid = crypto.DecryptData(tokenArray[0]);
            var username = crypto.DecryptData(tokenArray[1]);
            var lastActivityTime = crypto.DecryptData(tokenArray[2]).Replace(" ?.?", "");

            Guid guidOutput;
            DateTime dateOutput;
            //-----------------------------------------------------------------------------------------------------
            if (!Guid.TryParse(guid, out guidOutput))
                return false;
            //-----------------------------------------------------------------------------------------------------
            if (!DateTime.TryParse(lastActivityTime, out dateOutput))
                return false;
            //-----------------------------------------------------------------------------------------------------
            int tokenLifeTime = int.Parse(System.Configuration.ConfigurationManager.AppSettings["TokenLifTime"].ToString());
            if ((DateTime.Now - dateOutput).Minutes > tokenLifeTime)
                return false;
            //-----------------------------------------------------------------------------------------------------
            return true;
        }

        public string GetUsername(string token)
        {
            CryptData crypto = new CryptData(CryptKey);
            var tokenArray = token.Split(';');

            var guid = crypto.DecryptData(tokenArray[0]);
            var username = crypto.DecryptData(tokenArray[1]);
            var lastActivityTime = crypto.DecryptData(tokenArray[2]).Replace(" ?.?", "");

            return username;
        }

        public string Renew(string token)
        {
            CryptData crypto = new CryptData(CryptKey);
            var tokenArray = token.Split(';');

            Guid guid = Guid.Parse(crypto.DecryptData(tokenArray[0]));
            var username = crypto.DecryptData(tokenArray[1]);
            DateTime lastActivityTime = DateTime.Parse(crypto.DecryptData(tokenArray[2]).Replace(" ?.?", ""));

            int tokenLifeTime = int.Parse(System.Configuration.ConfigurationManager.AppSettings["TokenLifTime"].ToString());

            lastActivityTime = lastActivityTime.AddMinutes(tokenLifeTime);

            return crypto.EncryptData(Guid.NewGuid().ToString()) + ";" + crypto.EncryptData(username) + ";" + crypto.EncryptData(lastActivityTime.ToString()) + ";ToKEn";
        }
    }
}