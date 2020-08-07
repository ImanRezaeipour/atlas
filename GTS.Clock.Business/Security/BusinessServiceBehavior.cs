using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity.InterceptionExtension;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Model;
using GTS.Clock.Model.Security;
using GTS.Clock.Business.RequestFlow;
using GTS.Clock.Infrastructure.Utility;

namespace GTS.Clock.Business.Security
{
    public class BusinessServiceBehavior : IInterceptionBehavior
    {
        decimal personId = 0, roleId = 0;
        string roleCustomeCode = "";
        public BusinessServiceBehavior()
        {
            personId = BUser.CurrentUser.Person.ID;
            roleId = BUser.CurrentUser.Role.ID;
            roleCustomeCode = BUser.CurrentUser.Role.CustomCode;
        }
        public ResourceRepository resourceRepository
        {
            get
            {
                return new ResourceRepository();
            }
        }

        public IEnumerable<Type> GetRequiredInterfaces()
        {
            return Type.EmptyTypes;
        }

        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            IMethodReturn msg = null;
            if (personId > 0)
            {
                bool IsAuthorizableService = false;
                foreach (var customAttribute in input.MethodBase.GetCustomAttributes(false))
                {
                    if (customAttribute is ServiceAuthorizeBehavior)
                    {
                        IsAuthorizableService = true;
                        ServiceAuthorizeBehavior SAB = (ServiceAuthorizeBehavior)customAttribute;
                        switch (SAB.serviceAuthorizeState)
                        {
                            case ServiceAuthorizeState.Enforce:

                                #region GetAllowedResource
                                BRole busRole = new BRole();
                                List<Resource> accessAllowedResourceList = new List<Resource>();

                                accessAllowedResourceList.AddRange(busRole.GetAlowedResourceList(roleId));

                                #region Apply Other Business Roles
                                IList<RoleCustomCodeType> otherRoles = this.GetCurrentUserBusinessRole();
                                Dictionary<string, object> managementState = (Dictionary<string, object>)SessionHelper.GetSessionValue(SessionHelper.GTSCurrentUserManagmentState);

                            //    if (roleCustomeCode.Equals(((int)RoleCustomCodeType.User).ToString()))
                           //     {
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

                                                //Role role = new BRole().GetRoleByCode(roleCode);

                                            }
                                            if (tmpRoleId > 0)
                                            {
                                                accessAllowedResourceList.AddRange(busRole.GetAlowedResourceList(tmpRoleId));
                                            }
                                        }
                                        accessAllowedResourceList = accessAllowedResourceList.Distinct().ToList();
                           //        }
                                }
                                #endregion

                                #endregion


                                //ServiceAuthorizeType SAT = this.resourceRepository.CheckServiceAuthorize(BUser.CurrentUser.Role.ID, input);
                                ServiceAuthorizeType SAT = accessAllowedResourceList.Where(resource => resource.MethodPath == input.Target.ToString()
                                    && resource.MethodFullName == input.MethodBase.ToString()).Count() > 0 ? ServiceAuthorizeType.Legal : ServiceAuthorizeType.Illegal;
                                switch (SAT)
                                {
                                    case ServiceAuthorizeType.Illegal:
                                        msg = input.CreateExceptionMethodReturn(new IllegalServiceAccess("دسترسی غیر مجاز به سرویس", input.Target.ToString()));
                                        BaseBusiness<Entity>.LogException(new IllegalServiceAccess("دسترسی غیر مجاز به سرویس", input.Target.ToString()), input.Target.GetType().Name, input.MethodBase.Name);
                                        break;
                                    case ServiceAuthorizeType.Legal:
                                        msg = getNext()(input, getNext);
                                        break;
                                }
                                break;
                            case ServiceAuthorizeState.Avoid:
                                msg = getNext()(input, getNext);
                                break;
                        }
                        break;
                    }
                }
                if (!IsAuthorizableService)
                    msg = getNext()(input, getNext);
            }
            return msg;
        }

        public bool WillExecute
        {
            get { return true; }
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
                BaseBusiness<Entity>.LogException(ex);
                throw ex;
            }
        }
    }
}
