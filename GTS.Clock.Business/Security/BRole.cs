using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Model.Concepts.Operations;
using GTS.Clock.Model.Security;
using GTS.Clock.Infrastructure.Exceptions;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Model;
using GTS.Clock.Infrastructure;
using GTS.Clock.Business.AppSettings;
using NHibernate.Criterion;

namespace GTS.Clock.Business.Security
{
    public class BRole : BaseBusiness<Role>
    {
        private const string ExceptionSrc = "GTS.Clock.Business.Security.BRole";
        private RoleRepository roleRep = new RoleRepository(false);
        private AuthorizeRepository authorizeRep = new AuthorizeRepository();
        private UserRepository userRepository = new UserRepository();
        private NHibernate.ISession NHSession = NHibernateSessionManager.Instance.GetSession();
        private IDataAccess bUser = (IDataAccess)new BUser();
        private bool IsSystemTechnicalAdmin
        {
            get
            {
                bool isSystemTechnicalAdmin = false;
                if (BUser.CurrentUser.Role.CustomCode != string.Empty && (RoleCustomCodeType)Enum.Parse(typeof(RoleCustomCodeType), BUser.CurrentUser.Role.CustomCode) == RoleCustomCodeType.SystemTechnicalAdmin)
                    isSystemTechnicalAdmin = true;
                return isSystemTechnicalAdmin;
            }
        }

        public override IList<Role> GetAll()
        {
            IList<Role> list = base.GetAll();
            return list.Where(x => x.Active).ToList<Role>();
        }

        public IList<Role> GetAllAccessibleRoles()
        {
            try
            {
                IList<Role> rolesList = this.GetAll();
                IList<decimal> accessibleRoleIds = bUser.GetAccessibleRoles();
                rolesList = rolesList.Where(x => x.ID.IsIn(accessibleRoleIds.ToArray())).ToList<Role>();
                return rolesList;
            }
            catch (Exception ex)
            {
                LogException(ex, "BRole", "GetAllAccessibleRoles");
                throw ex;
            }
        }

        protected override void GetReadyBeforeSave(Role obj, UIActionType action)
        {
            string separator = ",";
            if (action == UIActionType.ADD && obj.ParentId > 0)
            {
                Role parent = base.GetByID(obj.ParentId);
                if (parent.ParentPath != null && parent.ParentPath.EndsWith(","))
                    separator = string.Empty;
                obj.ParentPath = parent.ParentPath + String.Format("" + separator + "{0},", obj.ParentId);
            }
            else if (action == UIActionType.EDIT)
            {
                Role node = base.GetByID(obj.ID);
                obj.ParentPath = node.ParentPath;
                NHibernateSessionManager.Instance.ClearSession();
            }
        }

        public Role GetRoleTree()
        {
            try
            {
                IList<Role> list = NHibernateSessionManager.Instance.GetSession().QueryOver<Role>()
                                                                                 .Where(x => x.ParentId == null || x.ParentId == 0)
                                                                                 .List<Role>();
                if (list.Count == 1)
                {
                    return list.First();
                }
                else
                {
                    throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.RoleRootMoreThanOne, "تعداد ریشه نقشها در دیتابیس نامعتبر است", ExceptionSrc);
                }
            }
            catch (Exception ex)
            {
                LogException(ex, "BRole", "GetRoleTree");
                throw ex;
            }
        }

        public Role GetByCustomCode(string customCode)
        {
            return roleRep.Find(x => x.CustomCode == customCode).FirstOrDefault();
        }

        /// <summary>
        /// بچه های یک گره را برمیگرداند
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        public IList<Role> GetRoleChilds(decimal roleId, IList<Role> allRoles)
        {
            try
            {
                IList<decimal> accessibleRoleIds = this.bUser.GetAccessibleRoles();
                IList<Role> rolesList = new List<Role>();
                if (allRoles != null && allRoles.Count > 0)
                {
                    rolesList = allRoles.Where(x => x.Active &&
                                                    x.ParentId == roleId &&
                                                    accessibleRoleIds.Contains(x.ID)).ToList();
                }
                return rolesList;
            }
            catch (Exception ex)
            {
                LogException(ex, "BRole", "GetRoleChilds");
                throw ex;
            }
        }

        /// <summary>
        /// کاربران یک نقش را برمیگرداند
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public IList<User> GetUsersInRole(decimal roleId)
        {
            try
            {
                Role role = base.GetByID(roleId);
                return role.UserList;
            }
            catch (Exception ex)
            {
                LogException(ex, "BRole", "GetUsersInRole");
                throw ex;
            }
        }

        /// <summary>
        /// کاربران نقش ادمین سیستمی را برمیگرداند
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public IList<User> GetUsersInSysAdminRole()
        {
            try
            {
                Role role = roleRep.GetByRoleCode("1");
                if (role != null && role.UserList != null)
                {
                    return role.UserList;
                }
                return new List<User>();
            }
            catch (Exception ex)
            {
                LogException(ex, "BRole", "GetUsersInRole");
                throw ex;
            }
        }

        /// <summary>
        /// با توجه به ساختار سلسله مراتبی منابع شناسه والد را میگیرد و فرزندان را برمیگرداند
        /// </summary>
        /// <param name="roleId">شناسه والد.اگر صفر باشد ریشه را برمیگرداند</param>      
        public ResourceProxy GetResources(decimal roleId)
        {
            EntityRepository<Authorize> athorizeRep = new EntityRepository<Authorize>();
            ResourceProxy result;
            Resource resource = this.GetResourceRoot();
            result = new ResourceProxy(resource, roleId);
            int count = athorizeRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new Authorize().Role), new Role() { ID = roleId }),
                new CriteriaStruct(Utility.GetPropertyName(() => new Authorize().Resource), resource),
                new CriteriaStruct(Utility.GetPropertyName(() => new Authorize().Allow), true));
            result.IsAllowed = count > 0 ? true : false;
            result.ChildList = new List<ResourceProxy>();
            this.GetResources(result.ChildList, resource.ChildList, roleId);

            return result;
        }

        /// <summary>
        /// اطالعات مربوط به سطوح دسترسی را در ذخیره میکند
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="resourceIdList">منابعی که دسترسی به آنها مجاز است</param>
        /// <returns></returns>
        public bool UpdateAthorize(decimal roleId, IList<decimal> resourceIdList)
        {

            EntityRepository<Resource> resourceRep = new EntityRepository<Resource>();
            using (NHibernateSessionManager.Instance.BeginTransactionOn())
            {
                try
                {
                    Role role = base.GetByID(roleId);
                    if (role.AuthorizeList != null)
                    {
                        List<Authorize> OtherSystemAuthorizeList = role.AuthorizeList.Where(authorize => authorize.Resource.SubSystemId != SubSystemIdentifier.TimeAtendance).ToList();
                        List<decimal> tmpResourceIds = new List<decimal>();
                        tmpResourceIds.AddRange(resourceIdList);
                        tmpResourceIds.AddRange((from o in OtherSystemAuthorizeList
                                                 select o.Resource.ID).ToList());

                        resourceIdList = tmpResourceIds.ToList();
                        role.AuthorizeList.Clear();
                    }
                    else
                    {
                        role.AuthorizeList = new List<Authorize>();
                    }
                    foreach (decimal resourceId in resourceIdList)
                    {
                        Authorize athorize = new Authorize();
                        Resource resource = resourceRep.GetById(resourceId, false);
                        athorize.Allow = true;
                        athorize.Resource = resource;
                        athorize.Role = role;
                        if (role.AuthorizeList.Where(auth => auth.Resource.ID == athorize.Resource.ID && auth.Role.ID == athorize.Role.ID).Count() == 0)
                            role.AuthorizeList.Add(athorize);
                        foreach (decimal parentId in resource.ParentPathList)
                        {//add parents
                            athorize = new Authorize();
                            athorize.Allow = true;
                            athorize.Resource = new Resource() { ID = parentId };
                            athorize.Role = role;
                            if (role.AuthorizeList.Where(auth => auth.Resource.ID == athorize.Resource.ID && auth.Role.ID == athorize.Role.ID).Count() == 0)
                            {
                                role.AuthorizeList.Add(athorize);
                            }
                        }
                    }
                    this.SaveChanges(role, UIActionType.EDIT);

                    NHibernateSessionManager.Instance.CommitTransactionOn();
                    return true;
                }
                catch (Exception ex)
                {
                    NHibernateSessionManager.Instance.RollbackTransactionOn();
                    LogException(ex, "BRole", "UpdateAthorize");
                    throw ex;
                }
            }
        }

        public bool HasAccessToResource(string username, string resourceKey)
        {
            try
            {

                BUser busUser = new BUser();
                User user = busUser.GetByUsername(username);
                if (user == null || user.Role == null || user.Role.ID == 0)
                {
                    return false;
                }
                IList<Resource> list = authorizeRep.GetAccessAllowed(user.Role.ID);
                return list.Where(x => x.ResourceID.ToLower().Equals(resourceKey.ToLower())).Count() > 0;
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw ex;
            }
        }

        /// <summary>
        /// تغییر نقش کاربر جاری
        /// </summary>
        /// <param name="roleCode"></param>
        public void SetUserRole(Person prs, RoleCustomCodeType roleCode)
        {
            try
            {
                Role role = this.GetByCustomCode(Utility.ToString((int)roleCode));
                prs = new BPerson().GetByID(prs.ID);
                if (role != null && role.ID > 0 && prs.UserList != null)
                {
                    switch (roleCode)
                    {
                        case RoleCustomCodeType.Manager:
                            if (Utility.ToInteger(prs.UserList.First().Role.CustomCode) == (int)RoleCustomCodeType.User
                                 ||
                                 Utility.ToInteger(prs.UserList.First().Role.CustomCode) == (int)RoleCustomCodeType.Substitute)
                            {
                                if (role.ID > 0)
                                {
                                    new BUser().EditUser(prs.UserList.First().ID, role.ID);
                                }
                            }
                            break;
                        case RoleCustomCodeType.Substitute:
                        case RoleCustomCodeType.Operator:
                            if (Utility.ToInteger(prs.UserList.First().Role.CustomCode) == (int)RoleCustomCodeType.User)
                            {
                                if (role.ID > 0)
                                {
                                    new BUser().EditUser(prs.UserList.First().ID, role.ID);
                                }
                            }
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw ex;
            }
        }

        #region Insert Update Delete

        protected override void InsertValidate(Role role)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            if (Utility.IsEmpty(role.Name))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.RoleNameRequierd, "درج - نام نقش نباید خالی باشد", ExceptionSrc));
            }
            else if (roleRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => role.Name), role.Name)) > 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.RoleNameReplication, "درج - نام نقش نباید تکراری باشد", ExceptionSrc));
            }

            if (!Utility.IsEmpty(role.CustomCode))
            {

                if (roleRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => role.CustomCode), role.CustomCode)) > 0)
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.RoleCodeReplication, "درج - کد نقش  نباید تکراری باشد", ExceptionSrc));
                }
            }
            if (role.ParentId == 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.RoleParentNotSpecified, "درج - والد انتخاب نشده است", ExceptionSrc));
            }
            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        protected override void UpdateValidate(Role role)
        {
            UIValidationExceptions exception = new UIValidationExceptions();
            role.ParentId = roleRep.GetParentId(role.ID);
            Role roleAlias = null;
            int customCodeRole = 0;
            Role roleObj = (Role)NHibernateSessionManager.Instance.GetSession().QueryOver<Role>(() => roleAlias)
                .Where(() => roleAlias.ID == role.ID).SingleOrDefault();
            int.TryParse(roleObj.CustomCode, out customCodeRole);
            NHibernateSessionManager.Instance.GetSession().Evict(roleObj);
            if (customCodeRole != 0 && Enum.IsDefined(typeof(RoleCustomCodeType), customCodeRole) && (role.Name != roleObj.Name || role.CustomCode != roleObj.CustomCode))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.RoleSystemNotAllowedUpdate, "نقش های سیستمی قابل ویرایش نمی باشد", ExceptionSrc));
            }
            else if (Utility.IsEmpty(role.Name))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.RoleNameRequierd, "بروزرسانی - نام نقش نباید خالی باشد", ExceptionSrc));
            }
            else if (roleRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => role.Name), role.Name),
                                                new CriteriaStruct(Utility.GetPropertyName(() => role.ID), role.ID, CriteriaOperation.NotEqual)) > 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.RoleNameReplication, "بروزرسانی - نام نقش نباید تکراری باشد", ExceptionSrc));
            }

            if (!Utility.IsEmpty(role.CustomCode))
            {

                if (roleRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => role.CustomCode), role.CustomCode),
                                               new CriteriaStruct(Utility.GetPropertyName(() => role.ID), role.ID, CriteriaOperation.NotEqual)) > 0)
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.RoleCodeReplication, "بروزرسانی - کد نقض نباید تکراری باشد", ExceptionSrc));
                }
            }
            if (role.ParentId == 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.RoleParentNotSpecified, "درج - والد انتخاب نشده است", ExceptionSrc));
            }
            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        protected override void DeleteValidate(Role role)
        {
            UIValidationExceptions exception = new UIValidationExceptions();
            int count = userRepository.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new User().Role), role));
            Role roleAlias = null;
            int customCodeRole = 0;
            Role roleObj = (Role)NHibernateSessionManager.Instance.GetSession().QueryOver<Role>(() => roleAlias)
                .Where(() => roleAlias.ID == role.ID).SingleOrDefault();
            int.TryParse(roleObj.CustomCode, out customCodeRole);
            NHibernateSessionManager.Instance.GetSession().Evict(roleObj);
            if (customCodeRole != 0 && Enum.IsDefined(typeof(RoleCustomCodeType), customCodeRole))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.RoleSystemNotAllowedDelete, "نقش های سیستمی قابل حذف نمی باشد", ExceptionSrc));
            }
            else if (count > 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.RoleUSedByUser, "حذف - این نقش در تعریف کاربران استفاده شده است", ExceptionSrc));
            }
            if (roleRep.IsRoot(role.ID))
            {
                exception.Add(ExceptionResourceKeys.RoleRootDeleteIllegal, "ریشه نقش نباید حذف شود", ExceptionSrc);
                throw exception;
            }
            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        /// <summary>
        /// *حذف نقشهایی که والد آنها حذف شده اند
        /// </summary>
        /// <param name="role"></param>
        /// <param name="action"></param>
        protected override void OnSaveChangesSuccess(Role role, UIActionType action)
        {
            if (action == UIActionType.DELETE)
            {
                DeleteHierarchicalRoles(role.ID);
            }
        }

        #endregion

        /// <summary>
        /// اگر یک گره حذف شود باید تمام بچههای آن نیز حذف شوند
        /// </summary>
        /// <param name="parentRoleId"></param>
        private void DeleteHierarchicalRoles(decimal parentRoleId)
        {
            if (parentRoleId > 0)
            {
                IList<Role> list = roleRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new Role().ParentId), parentRoleId));
                foreach (Role role in list)
                {
                    this.DeleteHierarchicalRoles(role.ID);
                    base.SaveChanges(role, UIActionType.DELETE);
                }
            }
        }

        /// <summary>
        /// ریشه درخت نقشها را برمیگرداند
        /// </summary>
        /// <returns></returns>
        private Role GetRoleRoot()
        {
            IList<Role> roles = roleRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new Role().ParentId), 0));
            if (roles.Count == 0 || roles.Count > 1)
            {
                throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.RoleRootMoreThanOne, "تعداد ریشه نقشها در دیتابیس نامعتبر است", ExceptionSrc);
            }
            return roles.First();
        }

        /// <summary>
        /// ریشه منابع را برمیگرداند
        /// </summary>
        /// <returns></returns>
        private Resource GetResourceRoot()
        {
            EntityRepository<Resource> resourceRep = new EntityRepository<Resource>();
            IList<Resource> roles = resourceRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new Resource().ParentId), null, CriteriaOperation.IS), new CriteriaStruct(Utility.GetPropertyName(() => new Resource().SubSystemId), SubSystemIdentifier.TimeAtendance, CriteriaOperation.Equal));
            if (roles.Count == 0 || roles.Count > 1)
            {
                throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.ResourceRootMoreThanOne, "تعداد ریشه منابع فرمها در دیتابیس نامعتبر است", ExceptionSrc);
            }
            return roles.First();
        }

        public IList<Resource> GetAccessDeniedList(decimal roleId)
        {
            AuthorizeRepository athorizeRep = new AuthorizeRepository();
            string extension = roleId.ToString();
            IList<Resource> list = new List<Resource>();
            if (!SessionHelper.HasSessionValue(SessionHelper.BussinessSecurityResourceList + extension))
            {
                list = athorizeRep.GetAccessDenied(roleId);

                if (list != null && list.Count > 0)
                {
                    SessionHelper.SaveSessionValue(SessionHelper.BussinessSecurityResourceList + extension, list);
                }
                else
                {
                    SessionHelper.ClearSessionValue(SessionHelper.BussinessSecurityResourceList + extension);
                }
            }
            object obj = SessionHelper.GetSessionValue(SessionHelper.BussinessSecurityResourceList + extension);
            if (obj != null)
            {
                list = (IList<Resource>)obj;
            }
            return list;
        }

        public IList<Resource> GetAlowedResourceList(decimal roleId)
        {
            AuthorizeRepository athorizeRep = new AuthorizeRepository();
            string extension = roleId.ToString();
            IList<Resource> list = new List<Resource>();
            if (!SessionHelper.HasSessionValue(SessionHelper.BussinessSecurityAllResourceList + extension))
            {
                list = athorizeRep.GetAccessAllowed(roleId);

                if (list != null && list.Count > 0)
                {
                    SessionHelper.SaveSessionValue(SessionHelper.BussinessSecurityAllResourceList + extension, list);
                }
                else
                {
                    SessionHelper.ClearSessionValue(SessionHelper.BussinessSecurityAllResourceList + extension);
                }
            }
            object obj = SessionHelper.GetSessionValue(SessionHelper.BussinessSecurityAllResourceList + extension);
            if (obj != null)
            {
                list = (IList<Resource>)obj;
            }
            return list;
        }

        /// <summary>
        /// در صورتی که نقش با کد تعریف شده وجود داشته باشد , آنرا بر میگرداند
        /// </summary>
        /// <param name="roleCode"></param>
        /// <returns></returns>
        public Role GetRoleByCode(RoleCustomCodeType roleCode)
        {
            try
            {
                Role role = roleRep.Find(x => x.CustomCode == ((int)roleCode).ToString()).ToList().FirstOrDefault();
                if (role == null)
                    return new Role();
                return role;
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw ex;
            }
        }

        /// <summary>
        /// بصورت بازگشتی منابع را پیمایش میکند
        /// </summary>
        /// <param name="resultList"></param>
        /// <param name="resourceList"></param>
        /// <param name="roleId"></param>
        private void GetResources(IList<ResourceProxy> resultList, IList<Resource> resourceList, decimal roleId)
        {
            EntityRepository<Authorize> athorizeRep = new EntityRepository<Authorize>();
            if (resourceList != null && resourceList.Count > 0)
            {
                foreach (Resource resource in resourceList)
                {
                    ResourceProxy item = new ResourceProxy(resource, roleId);
                    int count = athorizeRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new Authorize().Role), new Role() { ID = roleId }),
                    new CriteriaStruct(Utility.GetPropertyName(() => new Authorize().Resource), resource),
                    new CriteriaStruct(Utility.GetPropertyName(() => new Authorize().Allow), true));
                    item.IsAllowed = count > 0 ? true : false;
                    item.ChildList = new List<ResourceProxy>();
                    GetResources(item.ChildList, resource.ChildList, roleId);
                    resultList.Add(item);
                }
            }
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckRolesLoadAccess()
        {
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertRole(Role role, UIActionType UAT)
        {
            return base.SaveChanges(role, UAT);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal UpdateRole(Role role, UIActionType UAT)
        {
            return base.SaveChanges(role, UAT);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal DeleteRole(Role role, UIActionType UAT)
        {
            return base.SaveChanges(role, UAT);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckAccessLevelsAsignLoadAccess()
        {
        }

        public IList<Role> GetRoleChildsWithoutDA(decimal roleId)
        {
            try
            {
                IList<Role> roleList = new List<Role>();
                roleList = roleRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new Role().ParentId), roleId));
                return roleList;
            }
            catch (Exception ex)
            {
                LogException(ex, "BRole", "GetRoleChildsWithoutDA");
                throw ex;
            }
        }

        public IList<Role> GetRoleChildsByParentPath(decimal parentId)
        {
            IList<Role> roleList = roleRep.GetByCriteria(
                new CriteriaStruct(
                    Utility.GetPropertyName(() => new Role().ParentPath)
                    , String.Format(",{0},", parentId)
                    , CriteriaOperation.Like));
            if (roleList != null && roleList.Count > 0)
            {
                roleList = roleList.OrderBy(x => x.CustomCode).ToList();
            }
            return roleList;
        }
       public IList<Role> GetSearchedRole(string searchItem)
        {
           Role roleAlias = null;
           IList<Role> RoleList = NHSession.QueryOver(() => roleAlias)
                                            .Where(() => roleAlias.Name.IsInsensitiveLike(searchItem, MatchMode.Anywhere))
                                            .List<Role>();
           return RoleList;
        }
    }
}
