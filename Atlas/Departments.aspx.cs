using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.Globalization;
using GTS.Clock.Presentaion.Forms.App_Code;
using ComponentArt.Web.UI;
using System.Data;
using System.Collections;
using GTS.Clock.Business.Charts;
using GTS.Clock.Business;
using GTS.Clock.Model.Charts;
using System.IO;
using GTS.Clock.Business.UI;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Business.AppSettings;
using System.Web.Script.Serialization;
using GTS.Clock.Business.Security;
using System.Web.Services;
using GTS.Clock.Business.RequestFlow;
using GTS.Clock.Model.RequestFlow;
using GTS.Clock.Infrastructure;
using GTS.Clock.Infrastructure.Utility;

namespace GTS.Clock.Presentaion.WebForms
{
    public partial class Departments : GTSBasePage
    {
        internal class DepartmentObj
        {

            public string Name { get; set; }
            public string CustomCode { get; set; }
            public string DepartmentTypeID { get; set; }
            public string DepartmentTypeTitle { get; set; }
        }

        public JavaScriptSerializer JsSerializer
        {
            get
            {
                return new JavaScriptSerializer();
            }
        }

        public BDepartment DepartmentBusiness
        {
            get
            {
                return BusinessHelper.GetBusinessInstance<BDepartment>();
            }
        }

        public StringGenerator StringBuilder
        {
            get
            {
                return new StringGenerator();
            }
        }

        public ExceptionHandler exceptionHandler
        {
            get
            {
                return new ExceptionHandler();
            }
        }

        public BLanguage LangProv
        {
            get
            {
                return new BLanguage();
            }
        }

        enum Scripts
        {
            Departments_onPageLoad,
            tbDepartmentsIntroduction_TabStripMenus_Operations,
            Alert_Box,
            HelpForm_Operations,
            DialogWaiting_Operations,
            DropDownDive
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            RefererValidationProvider.CheckReferer();
            if (!CallBack_cmbDepartmentType_DepartmentIntroduction.IsCallback && !this.CallBack_trvDepartmentsIntroduction_DepartmentIntroduction.IsCallback && !this.CallBack_cmbDepartmentSearchResult_Departments.IsCallback)
            {
                Page DepartmentsPage = this;
                Ajax.Utility.GenerateMethodScripts(DepartmentsPage);
                SkinHelper.InitializeSkin(this.Page);
                ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
                this.CheckDepartmentsLoadAccess_Departments();
            }
        }

        private void CheckDepartmentsLoadAccess_Departments()
        {
            string[] retMessage = new string[4];
            try
            {
                this.DepartmentBusiness.CheckDepartmentsLoadAccess();
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                Response.Redirect("WhitePage.aspx?" + typeof(IllegalServiceAccess).Name + "=" + retMessage[1]);
            }
        }

        protected override void InitializeCulture()
        {
            this.SetCurrentCultureResObjs(this.LangProv.GetCurrentLanguage());
            base.InitializeCulture();
        }

        /// <summary>
        /// تنظیم زبان انتخابی کاربر 
        /// </summary>
        /// <param name="LangID"></param>
        private void SetCurrentCultureResObjs(string LangID)
        {
            //Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(LangID);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(LangID);
        }

        /// <summary>
        /// متد اصلی پر کردن درخت بخش ها
        /// </summary>
        /// <param name="IsDepartmentCodeView">شاخص نمایش کد بخش</param>
        private void Fill_trvDepartmentsIntroduction_DepartmentIntroduction(bool IsDepartmentCodeView)
        {
            string[] retMessage = new string[4];
            try
            {
                this.InitializeCulture();

                IList<Department> departmentsList = this.DepartmentBusiness.GetAll();
                Department rootDep = this.DepartmentBusiness.GetDepartmentsTree();
                TreeViewNode rootDepNode = new TreeViewNode();
                rootDepNode.ID = rootDep.ID.ToString();
                string rootDepNodeText = string.Empty;
                if (GetLocalResourceObject("OrgNode_trvDepartmentsIntroduction_DepartmentIntroduction") != null)
                    rootDepNodeText = GetLocalResourceObject("OrgNode_trvDepartmentsIntroduction_DepartmentIntroduction").ToString();
                else
                    rootDepNodeText = rootDep.Name;
                rootDepNode.Text = rootDepNodeText;
                DepartmentObj departmentObj = new DepartmentObj()
                {

                    Name = rootDepNodeText,
                    CustomCode = rootDep.CustomCode,
                    DepartmentTypeID = ((int)rootDep.DepartmentType).ToString(),
                    DepartmentTypeTitle = rootDep.DepartmentType.ToString() != "0" ? GetLocalResourceObject(rootDep.DepartmentType.ToString()).ToString() : ""
                };
                rootDepNode.Value = this.JsSerializer.Serialize(departmentObj);
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + PathHelper.GetModulePath_Nuke() + "\\Images\\TreeView\\folder.gif"))
                    rootDepNode.ImageUrl = PathHelper.GetModuleUrl_Nuke() + "Images/TreeView/folder.gif";
                if (IsDepartmentCodeView)
                    rootDepNode.Text = rootDep.CustomCode + " - " + rootDepNodeText;
                this.trvDepartmentsIntroduction_DepartmentIntroduction.Nodes.Add(rootDepNode);
                rootDepNode.Expanded = true;

                this.GetChildDepartment_trvDepartmentsIntroduction_DepartmentIntroduction(departmentsList, rootDepNode, rootDep, IsDepartmentCodeView);
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_Departments.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_Departments.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_Departments.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }


        /// <summary>
        /// متد بازگشتی پرکردن درخت بخش ها
        /// </summary>
        /// <param name="parentDepartmentNode">گره والد</param>
        /// <param name="parentDepartment">بخش والد</param>
        /// <param name="IsDepartmentCodeView">شاخص نمایش کد بخش</param>
        private void GetChildDepartment_trvDepartmentsIntroduction_DepartmentIntroduction(IList<Department> departmentsList, TreeViewNode parentDepartmentNode, Department parentDepartment, bool IsDepartmentCodeView)
        {
            foreach (Department childDep in this.DepartmentBusiness.GetDepartmentChilds(parentDepartment.ID, departmentsList))
            {
                TreeViewNode childDepNode = new TreeViewNode();
                childDepNode.ID = childDep.ID.ToString();
                childDepNode.Text = childDep.Name;
                DepartmentObj departmentObj = new DepartmentObj()
                {
                    Name = childDep.Name,
                    CustomCode = childDep.CustomCode,
                    DepartmentTypeID = ((int)childDep.DepartmentType).ToString(),
                    DepartmentTypeTitle = childDep.DepartmentType.ToString() != "0" ? GetLocalResourceObject(childDep.DepartmentType.ToString()).ToString() : ""
                };
                childDepNode.Value = this.JsSerializer.Serialize(departmentObj);
                childDepNode.ImageUrl = parentDepartmentNode.ImageUrl;

                if (IsDepartmentCodeView)
                    childDepNode.Text = childDep.CustomCode + " - " + childDep.Name;
                parentDepartmentNode.Nodes.Add(childDepNode);
                try
                {
                    if (parentDepartmentNode.Parent != null)
                        if (parentDepartmentNode.Parent.Parent == null)
                            parentDepartmentNode.Expanded = true;
                }
                catch
                { }
                if (this.DepartmentBusiness.GetDepartmentChilds(childDep.ID, departmentsList).Count > 0)
                    this.GetChildDepartment_trvDepartmentsIntroduction_DepartmentIntroduction(departmentsList, childDepNode, childDep, IsDepartmentCodeView);
            }
        }

        /// <summary>
        /// CallBack درخت بخش ها
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CallBack_trvDepartmentsIntroduction_DepartmentIntroduction_onCallBack(object sender, CallBackEventArgs e)
        {
            AttackDefender.CSRFDefender(this.Page);
            switch (e.Parameters[0])
            {
                case "DepartmentCodeView":
                    this.Fill_trvDepartmentsIntroduction_DepartmentIntroduction(bool.Parse(e.Parameters[1]));
                    break;
                default:
                    break;
            }
            this.trvDepartmentsIntroduction_DepartmentIntroduction.RenderControl(e.Output);
            this.ErrorHiddenField_Departments.RenderControl(e.Output);
        }


        /// <summary>
        /// درج و ویرایش و حذف بخش
        /// </summary>
        /// <param name="state">عملیات جاری</param>
        /// <param name="SelectedDepartmentID">در وضعیت درج شناسه بخش والد و در وضعیت ویرایش شناسه بخش انتخاب شده می باشد</param>
        /// <param name="DepartmentCode">کد بخش</param>
        /// <param name="DepartmentName">نام بخش</param>
        /// <returns>آرایه ای از پیغام و شناسه</returns>
        [Ajax.AjaxMethod("UpdateDepartment_DepartmentsPage", "UpdateDepartment_DepartmentsPage_onCallBack", null, null)]
        public string[] UpdateDepartment_DepartmentsPage(string state, string SelectedDepartmentID, string DepartmentCode, string DepartmentName, string DepartmentTypeID)
        {
            this.InitializeCulture();

            string[] retMessage = new string[4];

            try
            {
                AttackDefender.CSRFDefender(this.Page);
                decimal DepartmentID = 0;
                decimal selectedDepartmentID = decimal.Parse(this.StringBuilder.CreateString(SelectedDepartmentID), CultureInfo.InvariantCulture);
                DepartmentCode = this.StringBuilder.CreateString(DepartmentCode);
                DepartmentName = this.StringBuilder.CreateString(DepartmentName);
                int departmentTypeID = 0;
                if (!Utility.IsEmpty(DepartmentTypeID))
                    departmentTypeID = int.Parse(this.StringBuilder.CreateString(DepartmentTypeID), CultureInfo.InvariantCulture);
                UIActionType uam = (UIActionType)Enum.Parse(typeof(UIActionType), this.StringBuilder.CreateString(state).ToUpper());

                Department department = new Department();
                if (uam != UIActionType.DELETE)
                {
                    department.CustomCode = DepartmentCode;
                    department.Name = DepartmentName;
                    if (!Utility.IsEmpty(DepartmentTypeID))
                        department.DepartmentType = (DepartmentType)Enum.ToObject(typeof(DepartmentType), departmentTypeID);
                }

                switch (uam)
                {
                    case UIActionType.ADD:
                        department.ParentID = selectedDepartmentID;
                        department.ID = 0;
                        DepartmentID = this.DepartmentBusiness.InsertDepartment(department, uam);
                        break;
                    case UIActionType.EDIT:
                        if (selectedDepartmentID == 0)
                        {
                            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, new Exception(GetLocalResourceObject("NoDepartmentSelectedforEdit").ToString()), retMessage);
                            return retMessage;
                        }
                        else
                            department.ID = selectedDepartmentID;
                        DepartmentID = this.DepartmentBusiness.UpdateDepartment(department, uam);
                        break;
                    case UIActionType.DELETE:
                        if (selectedDepartmentID == 0)
                        {
                            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, new Exception(GetLocalResourceObject("NoDepartmentSelectedforDelete").ToString()), retMessage);
                            return retMessage;
                        }
                        else
                            department.ID = selectedDepartmentID;
                        DepartmentID = this.DepartmentBusiness.DeleteDepartment(department, uam);
                        break;
                }

                retMessage[0] = GetLocalResourceObject("RetSuccessType").ToString();
                string SuccessMessageBody = string.Empty;
                switch (uam)
                {
                    case UIActionType.ADD:
                        SuccessMessageBody = GetLocalResourceObject("AddComplete").ToString();
                        break;
                    case UIActionType.EDIT:
                        SuccessMessageBody = GetLocalResourceObject("EditComplete").ToString();
                        break;
                    case UIActionType.DELETE:
                        SuccessMessageBody = GetLocalResourceObject("DeleteComplete").ToString();
                        break;
                    default:
                        break;
                }
                retMessage[1] = SuccessMessageBody;
                retMessage[2] = "success";
                retMessage[3] = DepartmentID.ToString();
                return retMessage;
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                return retMessage;
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                return retMessage;
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                return retMessage;
            }
        }
        protected void CallBack_cmbDepartmentSearchResult_Departments_onCallBack(object sender, CallBackEventArgs e)
        {
            AttackDefender.CSRFDefender(this.Page);
            this.cmbDepartmentSearchResult_Departments.Dispose();
            this.Fill_cmbDepartmentSearchResult_Departments(this.StringBuilder.CreateString(e.Parameter));
            this.ErrorHiddenField_DepartmentSearchResult_Departments.RenderControl(e.Output);
            this.cmbDepartmentSearchResult_Departments.RenderControl(e.Output);
        }

        private void Fill_cmbDepartmentSearchResult_Departments(string SearchTerm)
        {
            string[] retMessage = new string[4];
            try
            {
                IList<Department> departmentList = this.DepartmentBusiness.SearchDepartment(DepartmentSearchFields.DepartmentName, SearchTerm);

                foreach (Department departItem in departmentList)
                {
                    ComboBoxItem departCmbItem = new ComboBoxItem(departItem.Name);
                    departCmbItem.Id = departItem.ID.ToString();

                    DepartmentObj departmentObj = new DepartmentObj()
                    {
                        Name = departItem.Name,
                        CustomCode = departItem.CustomCode,
                        DepartmentTypeID = ((int)departItem.DepartmentType).ToString(),
                        DepartmentTypeTitle = departItem.DepartmentType.ToString() != "0" ? GetLocalResourceObject(departItem.DepartmentType.ToString()).ToString() : ""
                    };
                    departCmbItem.Value = this.JsSerializer.Serialize(departmentObj);
                    this.cmbDepartmentSearchResult_Departments.Items.Add(departCmbItem);
                }
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_DepartmentSearchResult_Departments.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_DepartmentSearchResult_Departments.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_DepartmentSearchResult_Departments.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        //DNN Note:--------------------------------------------------
        protected void CallBack_cmbDepartmentType_DepartmentIntroduction_onCallBack(object sender, CallBackEventArgs e)
        {
            AttackDefender.CSRFDefender(this.Page);
            this.cmbDepartmentType_DepartmentIntroduction.Dispose();
            this.Fill_cmbDepartmentType_DepartmentIntroduction();
            this.ErrorHiddenField_DepartmentType.RenderControl(e.Output);
            this.cmbDepartmentType_DepartmentIntroduction.Enabled = true;
            this.cmbDepartmentType_DepartmentIntroduction.RenderControl(e.Output);
        }

        private void Fill_cmbDepartmentType_DepartmentIntroduction()
        {
            string[] retMessage = new string[4];
            try
            {
                foreach (DepartmentType departmentTypeItem in Enum.GetValues(typeof(DepartmentType)))
                {
                    ComboBoxItem cmbItemDepartmentType = new ComboBoxItem(GetLocalResourceObject(departmentTypeItem.ToString()).ToString());
                    cmbItemDepartmentType.Value = ((int)departmentTypeItem).ToString();
                    cmbItemDepartmentType.Id = ((int)departmentTypeItem).ToString();
                    this.cmbDepartmentType_DepartmentIntroduction.Items.Add(cmbItemDepartmentType);
                }
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_DepartmentType.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_DepartmentType.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_DepartmentType.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }
        //-----------------------------------------------------------

    }


}