using Atlas.Api.ViewModel;
using DotNetNuke.Web.Api;
using GTS.Clock.Business;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Business.RequestFlow;
using GTS.Clock.Business.Security;
using GTS.Clock.Infrastructure;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Model.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Atlas.Api.Controllers
{
    [DnnAuthorize]
    public class UserController : BaseController
    {
        #region Properties

        decimal personId = 0, roleId = 0;
        string roleCustomeCode = "";

        #endregion


        public UserController()
        {

        }

        [HttpGet]
        public HttpResponseMessage GetCurrentUsername()
        {
            try
            {
                var childlist = getAccess();
                var obj = new CurrentUserViewModel()
                {
                    FullName = BUser.CurrentUser.Person.Name,
                    Access = childlist.Select(c => new ResourceViewModel() { ID = c.ID, MethodFullName = c.MethodFullName, Description = c.Description }).ToList()
                };
                return Request.CreateResponse(HttpStatusCode.OK, obj);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        private List<Resource> getAccess()
        {
            personId = BUser.CurrentUser.Person.ID;
            roleId = BUser.CurrentUser.Role.ID;
            roleCustomeCode = BUser.CurrentUser.Role.CustomCode;

            #region GetAllowedResource
            BRole busRole = new BRole();
            List<Resource> accessAllowedResourceList = new List<Resource>();

            accessAllowedResourceList.AddRange(busRole.GetAlowedResourceList(roleId));

            #region Apply Other Business Roles
            IList<RoleCustomCodeType> otherRoles = this.GetCurrentUserBusinessRole();
            Dictionary<string, object> managementState = (Dictionary<string, object>)SessionHelper.GetSessionValue(SessionHelper.GTSCurrentUserManagmentState);

            if (otherRoles.Count > 0)
            {
                foreach (RoleCustomCodeType roleCode in otherRoles)
                {
                    decimal tmpRoleId = 0;
                    switch (roleCode)
                    {
                        case RoleCustomCodeType.Manager:
                            if (managementState.ContainsKey("ManagerRoleId"))
                            {
                                tmpRoleId = Utility.ToDecimal(managementState["ManagerRoleId"]);
                            }
                            break;
                        case RoleCustomCodeType.Substitute:
                            if (managementState.ContainsKey("SubstituteRoleId"))
                            {
                                tmpRoleId = Utility.ToDecimal(managementState["SubstituteRoleId"]);
                            }
                            break;
                        case RoleCustomCodeType.Operator:
                            if (managementState.ContainsKey("OperatorRoleId"))
                            {
                                tmpRoleId = Utility.ToDecimal(managementState["OperatorRoleId"]);
                            }
                            break;
                    }
                    if (tmpRoleId > 0)
                    {
                        accessAllowedResourceList.AddRange(busRole.GetAlowedResourceList(tmpRoleId));
                    }
                }
                accessAllowedResourceList = accessAllowedResourceList.Distinct().OrderBy(c => c.ID).ToList();

            }
            #endregion

            #endregion

            return accessAllowedResourceList;
        }
        private IList<RoleCustomCodeType> GetCurrentUserBusinessRole()
        {
            try
            {
                IList<RoleCustomCodeType> roles = new List<RoleCustomCodeType>();

                if (!SessionHelper.HasSessionValue(SessionHelper.GTSCurrentUserManagmentState))
                {
                    bool isManager = new BManager().GetManager(personId).ID > 0 ? true : false;

                    bool isSubstitute = new BSubstitute().GetSubstituteManager(personId) > 0 ? true : false;

                    bool isOperator = new BOperator().IsOperator();

                    Dictionary<string, object> ManagementState = new Dictionary<string, object>();


                    if (isManager)
                    {
                        Role role = new BRole().GetRoleByCode(RoleCustomCodeType.Manager);
                        if (role != null)
                        {
                            ManagementState.Add("ManagerRoleId", role.ID);
                        }
                    }
                    if (isOperator)
                    {
                        Role role = new BRole().GetRoleByCode(RoleCustomCodeType.Operator);
                        if (role != null)
                        {
                            ManagementState.Add("OperatorRoleId", role.ID);
                        }
                    }
                    if (isSubstitute)
                    {
                        Role role = new BRole().GetRoleByCode(RoleCustomCodeType.Substitute);
                        if (role != null)
                        {
                            ManagementState.Add("SubstituteRoleId", role.ID);
                        }
                    }

                    ManagementState.Add("IsManager", isManager);
                    ManagementState.Add("IsOperator", isOperator);
                    ManagementState.Add("IsSubstitute", isSubstitute);

                    SessionHelper.SaveSessionValue(SessionHelper.GTSCurrentUserManagmentState, ManagementState);
                }

                Dictionary<string, object> managementState = (Dictionary<string, object>)SessionHelper.GetSessionValue(SessionHelper.GTSCurrentUserManagmentState);

                if (Utility.ToBoolean(managementState["IsManager"]))
                    roles.Add(RoleCustomCodeType.Manager);

                if (Utility.ToBoolean(managementState["IsOperator"]))
                    roles.Add(RoleCustomCodeType.Operator);

                if (Utility.ToBoolean(managementState["IsSubstitute"]))
                    roles.Add(RoleCustomCodeType.Substitute);

                return roles;
            }
            catch (Exception ex)
            {
                BaseBusiness<GTS.Clock.Model.Entity>.LogException(ex);
                throw ex;
            }
        }
        public IEnumerable<Resource> GetResource(Resource resource)
        {
            yield return resource;
            foreach (Resource child in resource.ChildList) // check null if you must
                foreach (Resource relative in GetResource(child))
                    yield return relative;
        }
    }

    public class CurrentUserViewModel
    {
        public string FullName { get; set; }
        public List<ResourceViewModel> Access { get; set; }
    }

    public class ResourceViewModel
    {
        public decimal ID { get; set; }
        public string MethodPath { get; set; }
        public string MethodFullName { get; set; }
        public string Description { get; set; }
    }
}