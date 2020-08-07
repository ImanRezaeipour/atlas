using AtlasEncryption;
using GTS.Clock.Business.BoxService;
using GTS.Clock.Business.Security;
using GTS.Clock.Infrastructure;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Model.BoxService;
using GTS.Clock.Model.Security;
using System;
using System.Collections.Generic;
//using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "AtlasUIWebService" in code, svc and config file together.
[ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerSession)]
[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
public class AtlasUIWebService : IAtlasUIWebService
{
    private const string splitter = "*#*#*";
    private const string UsernameIsNotValued = "خطا : نام کاربری مقدار نگرفته است";
    private const string PasswordIsNotValued = "خطا : کلمه عبور مقدار نگرفته است";
    private const string InvalidUserNameOrPassword = "خطا : نام کاربری یا کلمه عبور صحیح نمی باشد";
    private const string IllegalAccess = "خطا : دسترسی غیرمجاز";
    public BMainPageBox bMainPageBox
    {
        get
        {
            return new BMainPageBox();
        }
    }

    public BUser bUser
    {
        get
        {
            return new BUser();
        }
    }

    public string Authenticate(string username, string password)
    {
        try
        {
            string token = string.Empty;

            if (username == string.Empty)
                throw new FaultException(new FaultReason(UsernameIsNotValued), new FaultCode(FaultKey.UsernameIsNotValued.ToString()));
            if (password == string.Empty)
                throw new FaultException(new FaultReason(PasswordIsNotValued), new FaultCode(FaultKey.PasswordIsNotValued.ToString()));

            User user = this.bUser.GetByUsername(username);
            if(user == null || user.ID == 0 || user.Password == null)
                throw new FaultException(new FaultReason(InvalidUserNameOrPassword), new FaultCode(FaultKey.InvalidUserNameOrPassword.ToString()));

            CryptData cryptData = new CryptData(username);
            password = cryptData.DecryptData(password);
            if(!Utility.VerifyHashCode(password, user.Password))
                throw new FaultException(new FaultReason(InvalidUserNameOrPassword), new FaultCode(FaultKey.InvalidUserNameOrPassword.ToString()));

            token = this.CreateToken(username, user.Password);

            return token;
        }
        catch (FaultException ex)
        {
            throw ex;
        }
        catch (CommunicationException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private string CreateToken(string username, string password)
    {
        string token = string.Empty;
        token = SimpleHash.ComputeHash(username + splitter + password + splitter + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString(), HashStandards.MD5, Encoding.UTF8.GetBytes(splitter + password + username + splitter));
        return token;
    }

    private bool ValidateToken(string username, string token, out User user)
    {
        bool isValid = false;

        user = this.bUser.GetByUsername(username);
        if (user == null || user.ID == 0 || user.Password == null)
            return isValid;

        string validToken = SimpleHash.ComputeHash(user.UserName + splitter + user.Password + splitter + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString(), HashStandards.MD5, Encoding.UTF8.GetBytes(splitter + user.Password + user.UserName + splitter));
        if (string.Equals(token, validToken))
            isValid = true;

        return isValid;
    }

    public IList<KartablSummary> GetKartablSummary(string username, string token)
    {
        try
        {
            User user = new User();
            if (!this.ValidateToken(username, token, out user))
                throw new FaultException(new FaultReason(IllegalAccess), new FaultCode(FaultKey.IllegalAccess.ToString()));

            return this.bMainPageBox.GetKartablSummary(user.ID);
        }
        catch (FaultException ex)
        {
            throw ex;
        }
        catch (CommunicationException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


}
