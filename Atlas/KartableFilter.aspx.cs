﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GTS.Clock.Presentaion.Forms.App_Code;
using System.Threading;
using System.Globalization;
using ComponentArt.Web.UI;
using System.Collections;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Business.UI;

public partial class KartableFilter : GTSBasePage
{
    public class Filter
    {
        private int id;
        public int ID
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
            }
        }

        private string filterCondition;
        public string FilterCondition
        {
            get
            {
                return this.filterCondition;
            }
            set
            {
                this.filterCondition = value;
            }
        }

        private string conditionOperator;
        public string ConditionOperator
        {
            get
            {
                return this.conditionOperator;
            }
            set
            {
                this.conditionOperator = value;
            }
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
        KartableFilter_onPageLoad,
        DialogKartableFilter_Operations,
        DialogWaiting_Operations
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        RefererValidationProvider.CheckReferer();
        if (!this.CallBack_cmbOperator_KartableFilter.IsCallback && !this.CallBack_GridCombinationalConditions_KartableFilter.IsCallback && !this.Page.IsPostBack)
        {
            Page KartableFilterPage = this;
            Ajax.Utility.GenerateMethodScripts(KartableFilterPage);

            this.ViewCurrentLangCalendars_KartableFilter();
            this.Fill_cmbFilterField_KartableFilter();
            this.Fill_cmbOperator_KartableFilter("Date");
            this.InitializeSkin();
            ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
        }
    }

    private void ViewCurrentLangCalendars_KartableFilter()
    {
        switch (this.LangProv.GetCurrentSysLanguage())
        {
            case "fa-IR":
                this.Container_pdpDate_KartableFilter.Visible = true;
                break;
            case "en-US":
                this.Container_gdpDate_KartableFilter.Visible = true;
                break;
        }
    }

    private void InitializeSkin()
    {
        SkinHelper.InitializeSkin(this.Page);
        SkinHelper.SetRelativeTabStripImageBaseUrl(this.Page, this.TabStripFilterTerms);
    }

    protected override void InitializeCulture()
    {
        this.SetCurrentCultureResObjs(this.LangProv.GetCurrentLanguage());
        base.InitializeCulture();
    }

    private void SetCurrentCultureResObjs(string LangID)
    {
        //Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(LangID);
        Thread.CurrentThread.CurrentUICulture = new CultureInfo(LangID);
    }

    [Ajax.AjaxMethod("GetBoxesHeaders_KartableFilterPage", "GetBoxesHeaders_KartableFilterPage_onCallBack", null, null)]
    public string[] GetBoxesHeaders_KartableFilterPage()
    {
        this.InitializeCulture();
        AttackDefender.CSRFDefender(this.Page);
        string[] retMessage = new string[5];
        retMessage[0] = GetLocalResourceObject("Title_DialogKartableFilter").ToString();
        retMessage[1] = GetLocalResourceObject("header_CombinationalConditions_KartableFilter").ToString();
        retMessage[2] = GetLocalResourceObject("FilterValueIsNull").ToString();
        retMessage[3] = "And:" + GetLocalResourceObject("And").ToString();
        retMessage[4] = "Or:" + GetLocalResourceObject("Or").ToString();
        return retMessage;
    }

    private void Fill_cmbFilterField_KartableFilter()
    {
        ComboBoxItem cmbItemDate = new ComboBoxItem(GetLocalResourceObject("Date").ToString());
        cmbItemDate.Value = "Date";
        ComboBoxItem cmbItemSelective = new ComboBoxItem(GetLocalResourceObject("Selective").ToString());
        cmbItemSelective.Value = "Selective";
        ComboBoxItem cmbItemString = new ComboBoxItem(GetLocalResourceObject("String").ToString());
        cmbItemString.Value = "String";
        ComboBoxItem cmbItemTime = new ComboBoxItem(GetLocalResourceObject("Time").ToString());
        cmbItemTime.Value = "Time";
        this.cmbFilterField_KartableFilter.Items.Add(cmbItemDate);
        this.cmbFilterField_KartableFilter.Items.Add(cmbItemSelective);
        this.cmbFilterField_KartableFilter.Items.Add(cmbItemString);
        this.cmbFilterField_KartableFilter.Items.Add(cmbItemTime);
        this.cmbFilterField_KartableFilter.SelectedIndex = 0;
    }



    protected void CallBack_cmbOperator_KartableFilter_onCallBack(object sender, ComponentArt.Web.UI.CallBackEventArgs e)
    {
        AttackDefender.CSRFDefender(this.Page);
        this.cmbOperator_KartableFilter.Dispose();
        this.Fill_cmbOperator_KartableFilter(e.Parameter);
        this.cmbOperator_KartableFilter.RenderControl(e.Output);
    }

    private void Fill_cmbOperator_KartableFilter(string Key)
    {
        this.InitializeCulture();
        Dictionary<string, string> DicOperators = new Dictionary<string, string>();
        switch (Key)
        {
            case "Date":
                DicOperators.Add("Equal", GetLocalResourceObject("Equal").ToString());
                DicOperators.Add("LessThan", GetLocalResourceObject("LessThan").ToString());
                DicOperators.Add("GreatherThan", GetLocalResourceObject("GreatherThan").ToString());
                break;
            case "Selective":
                DicOperators.Add("Equal", GetLocalResourceObject("Equal").ToString());
                break;
            case "Time":
                DicOperators.Add("Equal", GetLocalResourceObject("Equal").ToString());
                DicOperators.Add("LessThan", GetLocalResourceObject("LessThan").ToString());
                DicOperators.Add("GreatherThan", GetLocalResourceObject("GreatherThan").ToString());
                break;
            case "String":
                DicOperators.Add("Equal", GetLocalResourceObject("Equal").ToString());
                DicOperators.Add("StartsWith", GetLocalResourceObject("StartsWith").ToString());
                DicOperators.Add("EndsWith", GetLocalResourceObject("EndsWith").ToString());
                DicOperators.Add("Contains", GetLocalResourceObject("Contains").ToString());
                break;
        }
        foreach (string key in DicOperators.Keys)
        {
            ComboBoxItem cmbItem = new ComboBoxItem(DicOperators[key]);
            cmbItem.Value = key;
            this.cmbOperator_KartableFilter.Items.Add(cmbItem);
        }
        this.cmbOperator_KartableFilter.SelectedIndex = 0;
    }

    [Ajax.AjaxMethod("GetCurrentDateTime_KartableFilterPage", "GetCurrentDateTime_KartableFilterPage_onCallBack", null, null)]
    public string GetCurrentDateTime_KartableFilterPage()
    {
        AttackDefender.CSRFDefender(this.Page);
        string CurrentDateTime = string.Empty;
        string CurrentCulture = this.LangProv.GetCurrentLanguage();
        switch (CurrentCulture)
        {
            case "fa-IR":
                PersianCalendar pCal = new PersianCalendar();
                CurrentDateTime = pCal.GetYear(DateTime.Now).ToString() + "/" + pCal.GetMonth(DateTime.Now).ToString() + "/" + pCal.GetDayOfMonth(DateTime.Now).ToString();
                break;
            case "en-US":
                CurrentDateTime = DateTime.Now.Year.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Day.ToString();
                break;
        }
        return CurrentDateTime;
    }

    protected void CallBack_GridCombinationalConditions_KartableFilter_onCallBack(object sender, ComponentArt.Web.UI.CallBackEventArgs e)
    {
        this.InitializeCulture();
        AttackDefender.CSRFDefender(this.Page);
        ArrayList arFilters = this.InsertFilterConditions_KartableFilter(e.Parameter);
        this.GridCombinationalConditions_KartableFilter.DataSource = arFilters;
        this.GridCombinationalConditions_KartableFilter.DataBind();
        this.GridCombinationalConditions_KartableFilter.RenderControl(e.Output);
    }

    private ArrayList InsertFilterConditions_KartableFilter(string FilterConditions)
    {
        ArrayList arFilters = new ArrayList();
        if (FilterConditions != string.Empty)
        {
            string[] Conditions = FilterConditions.Split(new char[] { '%' });
            if (Conditions.Length > 0)
            {
                for (int i = 0; i < Conditions.Length; i++)
                {
                    string[] ConditionProps = Conditions[i].Split(new char[] { '@' });
                    Filter filter = new Filter()
                    {
                        ID = int.Parse(ConditionProps[0], CultureInfo.InvariantCulture),
                        FilterCondition = GetLocalResourceObject(ConditionProps[1]).ToString() + " " + GetLocalResourceObject(ConditionProps[2]).ToString() + " " + ConditionProps[3],
                        ConditionOperator = GetLocalResourceObject(ConditionProps[4]).ToString()
                    };
                    arFilters.Add(filter);
                }
            }
        }
        return arFilters;
    }



}