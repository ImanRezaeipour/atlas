using DotNetNuke.Entities.Modules;
using GTS.BaseBusiness;
using GTS.Business;
using GTS.Clock.Business.RequestFlow;
using GTS.Clock.Business.Security;
using GTS.Clock.Infrastructure;
using GTS.Clock.Model.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace GTS.Clock.Business
{
   public abstract class GTSBasePageNuke : BaseGTSBasePageNuke
    {
       private class AuthorizeService : IAuthorizeServices
        {

            #region IAuthorizeServices Members

            public Role GetRoleByCode(RoleCustomCodeType roleCode)
            {
                return new BRole().GetRoleByCode(roleCode);
            }

            public IList<Resource> GetAccessDeniedList(decimal roleId)
            {
                return new BRole().GetAccessDeniedList(roleId);
            }

            public IList<Resource> GetAlowedResourceList(decimal roleId)
            {
                return new BRole().GetAlowedResourceList(roleId);
            }

            public bool IsManager(decimal personId)
            {
                return new BManager().GetManager(BUser.CurrentUser.Person.ID).ID > 0 ? true : false;
            }

            public bool IsSubstituteManager(decimal personId)
            {
                return new BSubstitute().GetSubstituteManager(BUser.CurrentUser.Person.ID) > 0 ? true : false;
            }

            public bool IsOperator(decimal personId)
            {
                return new BOperator().IsOperator();
            }

            #endregion
        }
       public void SetUserAndAthorize()
       {
           Session.Clear();
           base.CurrentUSer = BUser.CurrentUser;
           base.Authorize(new AuthorizeService(),this);
       }
        public GTSBasePageNuke()
            : base()
        {
           
        }
        //protected override void OnInit(EventArgs e)
        //{
        //    base.CurrentUSer = BUser.CurrentUser;
        //    base.OnInit(e);
        //}

        //protected override void OnPreRender(EventArgs e)
        //{
        //    try
        //    {
        //        if (!base.IsPostBack)
        //        {
        //            base.OnPreRender(e);
        //            base.Authorize(new AuthorizeService());
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //BaseBusiness<Entity>.LogException(ex, "GTSBasePage", "Authorizing...");
        //        throw ex;
        //    }
        //}
    }
}
