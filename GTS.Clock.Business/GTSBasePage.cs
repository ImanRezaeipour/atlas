using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using WebControls = System.Web.UI.WebControls;
using ComponentArt.Web.UI;
using GTS.Clock.Model.Security;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Business.Security;
using GTS.Clock.Infrastructure;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Log;
using GTS.Clock.Model;
using System.Web.UI.HtmlControls;
using CpontArt = ComponentArt.Web.UI;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Business.RequestFlow;
using GTS.Clock.Model.RequestFlow;
using GTS.Business;
using System.Globalization;

namespace GTS.Clock.Business.UI
{
    public abstract class GTSBasePage : BaseGTSBasePage
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

        public GTSBasePage()
            : base()
        {

        }
        protected override void OnInit(EventArgs e)
        {
            base.CurrentUSer = BUser.CurrentUser;
            base.OnInit(e);
        }

        protected override void OnPreLoad(EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    base.OnPreLoad(e);
                    base.Authorize(new AuthorizeService());
                }
            }
            catch (Exception ex)
            {
                //BaseBusiness<Entity>.LogException(ex, "GTSBasePage", "Authorizing...");
                throw ex;
            }
        }

    }
}
