using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Model.Report;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Business.Security;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Infrastructure;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Business.Concept;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Model.PersonInfo;
using GTS.Clock.Business.PersonInfo;
using GTS.Clock.Model.Rules;
using GTS.Clock.Business.Rules;
using NHibernate;
using GTS.Clock.Infrastructure.NHibernateFramework;

namespace GTS.Clock.Business.Reporting
{
    public class BDesignedReportsColumn : BaseBusiness<DesignedReportColumn>
    {

        EntityRepository<DesignedReportColumn> repDesignedReportColumns = new EntityRepository<DesignedReportColumn>(false);
        EntityRepository<DesignedReportStaticColumn> repDesignedReportStaticColumn = new EntityRepository<DesignedReportStaticColumn>(false);
        EntityRepository<DesignedReportPersonInfoColumn> repDesignedReportPersonInfoColumn = new EntityRepository<DesignedReportPersonInfoColumn>(false);
        EntityRepository<DesignedReportTrafficColumn> repDesignedReportTrafficColumn = new EntityRepository<DesignedReportTrafficColumn>(false);
        EntityRepository<DesignedReportType> repDesignedReportType = new EntityRepository<DesignedReportType>(false);
        private ISession NHSession = NHibernateSessionManager.Instance.GetSession();
        const string ExceptionSrc = "GTS.Clock.Business.Reporting.BDesignedReportsColumn";


        public IList<DesignedReportColumnProxy> GetDesignedReportsColumnsProxyByReportID(decimal reportID)
        {
            try
            {

                IList<DesignedReportColumn> designedReportColumnList = repDesignedReportColumns.Find(d => d.Report.ID == reportID).ToList<DesignedReportColumn>().Distinct(new DesignedReportColumnConceptKeyComparer()).OrderBy(o => o.Order).ToList<DesignedReportColumn>();

                IList<DesignedReportColumnProxy> designedReportColumnProxyList = new List<DesignedReportColumnProxy>();
                foreach (DesignedReportColumn item in designedReportColumnList)
                {

                    DesignedReportColumnProxy designedReportColumnProxy = ConvertDesignedReportColumnToProxy(item);
                    designedReportColumnProxyList.Add(designedReportColumnProxy);


                }
                return designedReportColumnProxyList;
            }
            catch (Exception ex)
            {
                LogException(ex, "BDesignedReportsColumn", "GetDesignedReportsColumnsByReportID");
                throw ex;
            }
        }
        public IList<DesignedReportColumn> GetDesignedReportsColumnsByReportID(decimal reportID)
        {
            try
            {

                IList<DesignedReportColumn> designedReportColumnList = repDesignedReportColumns.Find(d => d.Report.ID == reportID).ToList<DesignedReportColumn>().Distinct(new DesignedReportColumnConceptKeyComparer()).OrderBy(o => o.Order).ToList<DesignedReportColumn>();
                foreach (DesignedReportColumn item in designedReportColumnList)
                {
                    if (item.Concept != null)
                    {
                        switch (BLanguage.CurrentLocalLanguage)
                        {
                            case LanguagesName.Parsi:
                                item.Concept.Name = item.Concept.FnName;
                                break;
                            case LanguagesName.English:
                                item.Concept.Name = item.Concept.EnName;
                                break;
                            default:
                                item.Concept.Name = item.Concept.FnName;
                                break;
                        }
                        item.ColumnName = item.Concept.KeyColumnName;
                        item.Name = item.Concept.Name;
                        item.ColumnType = DesignedReportColumnType.Concept;

                    }
                    else if (item.PersonInfo != null)
                    {
                        switch (BLanguage.CurrentLocalLanguage)
                        {
                            case LanguagesName.Parsi:
                                item.PersonInfo.Name = item.PersonInfo.FnName;
                                break;
                            case LanguagesName.English:
                                item.PersonInfo.Name = item.PersonInfo.EnName;
                                break;
                            default:
                                item.PersonInfo.Name = item.PersonInfo.FnName;
                                break;
                        }
                        item.ColumnName = new BReport().GetDesignedReportColumnFieldNameForDesigned(item);
                        item.Name = item.PersonInfo.Name;
                        item.ColumnType = DesignedReportColumnType.PersonInfo;
                    }
                    else if (item.Traffic != null)
                    {

                        switch (BLanguage.CurrentLocalLanguage)
                        {
                            case LanguagesName.Parsi:
                                item.Traffic.Name = item.Traffic.FnName;
                                break;
                            case LanguagesName.English:
                                item.Traffic.Name = item.Traffic.EnName;
                                break;
                            default:
                                item.Traffic.Name = item.Traffic.FnName;
                                break;
                        }
                        item.ColumnName = new BReport().GetDesignedReportColumnFieldNameForDesigned(item);
                        item.Name = item.Traffic.Name;
                        item.ColumnType = DesignedReportColumnType.Traffic;
                    }
                    else if(item.PersonParam!=null)
                    {
                        switch (BLanguage.CurrentLocalLanguage)
                        {
                            case LanguagesName.Parsi:
                                item.PersonParam.Name = item.PersonParam.FnTitle;
                                break;
                            case LanguagesName.English:
                                item.PersonParam.Name = item.PersonParam.EnTitle;
                                break;
                            default:
                                item.PersonParam.Name = item.PersonParam.FnTitle;
                                break;
                        }
                        item.ColumnName = new BReport().GetDesignedReportColumnFieldNameForDesigned(item);
                        item.Name = item.PersonParam.Name;
                        item.ColumnType = DesignedReportColumnType.PersonParam;
                    }







                }
                return designedReportColumnList;
            }
            catch (Exception ex)
            {
                LogException(ex, "BDesignedReportsColumn", "GetDesignedReportsColumnsByReportID");
                throw ex;
            }
        }


        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Avoid)]
        public decimal InsertColumn(DesignedReportColumn column)
        {
            try
            {


                decimal id = this.SaveChanges(column, UIActionType.ADD);
                return id;
            }
            catch (Exception ex)
            {
                LogException(ex, "BDesignedReportsColumn", "InsertColumn");
                throw ex;
            }
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Avoid)]
        public decimal UpdateColumn(DesignedReportColumn column)
        {
            try
            {

                decimal id = this.SaveChanges(column, UIActionType.EDIT);
                return id;
            }
            catch (Exception ex)
            {
                LogException(ex, "BDesignedReportsColumn", "UpdateColumn");
                throw ex;
            }
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Avoid)]
        public decimal DeleteColumn(DesignedReportColumn column)
        {

            decimal id = base.SaveChanges(column, UIActionType.DELETE);
            return id;
        }

        protected override void InsertValidate(DesignedReportColumn obj)
        {
            UIValidationExceptions exception = new UIValidationExceptions();
            Report reportObj = new BReport().GetByID(obj.Report.ID);
            DesignedReportTypeEnum designedReportType = reportObj.DesignedType.CustomCode;
            IList<DesignedReportColumn> designedReportColumnList = GetDesignedReportsColumnsByReportID(obj.Report.ID);
            if (obj.Concept == null && obj.PersonInfo == null && obj.Traffic==null && obj.PersonParam==null)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.DesignedReportColumnIsRequired, "ستونی جهت ثبت انتخاب نشده است.", ExceptionSrc));
            }
            else if (designedReportType == DesignedReportTypeEnum.Person && (obj.Concept!=null || obj.Traffic!=null) )
                     exception.Add(new ValidationException(ExceptionResourceKeys.DesignedReportTypeIsNotSelected, "نوع گزارش پرسنلی است .از ستون مفاهیم نمی توانید انتخاب کنید.", ExceptionSrc));

            else if (obj.Traffic != null && obj.ColumnType == DesignedReportColumnType.Traffic && obj.Traffic.Key == DesignedReportTrafficKeyColumn.AllTraffic && designedReportColumnList.Where(d => d.Traffic != null).Count(d => d.Traffic.Key == DesignedReportTrafficKeyColumn.FirstTraffic || d.Traffic.Key == DesignedReportTrafficKeyColumn.LastTraffic)>0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.DesignedReportTrafficTypeIsConflicted, "ستون انتخابی مجاز نمی باشد.", ExceptionSrc));
            }
            else if (obj.Traffic != null && obj.ColumnType == DesignedReportColumnType.Traffic && (obj.Traffic.Key == DesignedReportTrafficKeyColumn.FirstTraffic || obj.Traffic.Key == DesignedReportTrafficKeyColumn.LastTraffic) && designedReportColumnList.Where(d => d.Traffic != null).Count(d => d.Traffic.Key == DesignedReportTrafficKeyColumn.AllTraffic) > 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.DesignedReportTrafficTypeIsConflicted, "ستون انتخابی مجاز نمی باشد.", ExceptionSrc));
            }
            else
            {
                IList<DesignedReportColumn> columnList = GetDesignedReportsColumnsByReportID(obj.Report.ID);
                foreach (DesignedReportColumn item in columnList)
                {

                    if (item.Concept != null)
                    {
                        if (obj.Concept!=null && item.Concept.ID == obj.Concept.ID)
                        {
                            exception.Add(new ValidationException(ExceptionResourceKeys.DesignedReportColumnIsRepeated, "ستون انتخابی تکراری می باشد.", ExceptionSrc));
                        }
                    }
                    else if (item.PersonInfo != null)
                    {
                        if (obj.PersonInfo != null && item.PersonInfo.ID == obj.PersonInfo.ID)
                        {
                            exception.Add(new ValidationException(ExceptionResourceKeys.DesignedReportColumnIsRepeated, "ستون انتخابی تکراری می باشد.", ExceptionSrc));
                        }
                    }
                    else if (obj.Traffic != null && item.Traffic != null)
                    {
                        if (item.Traffic.ID == obj.Traffic.ID)
                        {
                            exception.Add(new ValidationException(ExceptionResourceKeys.DesignedReportColumnIsRepeated, "ستون انتخابی تکراری می باشد.", ExceptionSrc));
                        }
                    }
                }


            }

            if (exception.Count > 0)
            {
                throw exception;
            }

        }

        protected override void UpdateValidate(DesignedReportColumn obj)
        {

        }

        protected override void DeleteValidate(DesignedReportColumn obj)
        {
            UIValidationExceptions exception = new UIValidationExceptions();
            IList<DesignedReportCondition> reportConditionsList = new BDesignedReportCondition().GetDesignedReportConditions(obj.Report.ID);
            DesignedReportColumn selectedColumn = GetDesignedReportsColumnsByReportID(obj.Report.ID).FirstOrDefault(c => c.ID == obj.ID);
            if(selectedColumn!=null)
            {
                foreach (DesignedReportCondition item in reportConditionsList)
                {
                    
                    if(item.ConditionValue.Contains(selectedColumn.ColumnName))
                    {
                        int indexColumn = item.ConditionValue.IndexOf(selectedColumn.ColumnName);
                        if(item.ConditionValue[indexColumn -1] != '\'')
                            exception.Add(new ValidationException(ExceptionResourceKeys.ReportColumnInUseConditions, "ستون در شروط پویا توسط کاربران استفاده شده است.", ExceptionSrc));
                    }
                }
            }

            NHSession.Evict(selectedColumn);
            if (exception.Count > 0)
            {
                throw exception;
            }
          

        }

        public DesignedReportStaticColumn GetDesignedReportsStaticColumnByKeyName(string fieldName)
        {
            try
            {

                DesignedReportStaticColumn designedReportStaticColumnObj = repDesignedReportStaticColumn.Find(d => d.KeyName.ToLower() == fieldName.ToLower()).SingleOrDefault();
                switch (BLanguage.CurrentLocalLanguage)
                {
                    case LanguagesName.Parsi:
                        designedReportStaticColumnObj.Name = designedReportStaticColumnObj.FnName;
                        break;
                    case LanguagesName.English:
                        designedReportStaticColumnObj.Name = designedReportStaticColumnObj.EnName;
                        break;
                    default:
                        designedReportStaticColumnObj.Name = designedReportStaticColumnObj.FnName;
                        break;
                }
                return designedReportStaticColumnObj;
            }
            catch (Exception ex)
            {
                LogException(ex, "BDesignedReportsStaticColumn", "GetDesignedReportsStaticColumnByFieldName");
                throw ex;
            }
        }
        public IList<DesignedReportStaticColumn> GetAllDesignedReportsStaticColumn()
        {
            try
            {
                IList<DesignedReportStaticColumn> designedReportStaticColumnList = new List<DesignedReportStaticColumn>();
                designedReportStaticColumnList = repDesignedReportStaticColumn.GetAll().ToList<DesignedReportStaticColumn>();

                foreach (DesignedReportStaticColumn item in designedReportStaticColumnList)
                {
                    switch (BLanguage.CurrentLocalLanguage)
                    {
                        case LanguagesName.Parsi:
                            item.Name = item.FnName;
                            break;
                        case LanguagesName.English:
                            item.Name = item.EnName;
                            break;
                        default:
                            item.Name = item.FnName;
                            break;
                    }
                }

                return designedReportStaticColumnList;
            }
            catch (Exception ex)
            {
                LogException(ex, "BDesignedReportsColumn", "GetAllDesignedReportsStaticColumn");
                throw ex;
            }
        }
        public IList<DesignedReportPersonInfoColumn> GetAllDesignedReportsPersonInfoColumns()
        {
            try
            {
                IList<DesignedReportPersonInfoColumn> designedReportPersonInfoColumnList = new List<DesignedReportPersonInfoColumn>();
                designedReportPersonInfoColumnList = repDesignedReportPersonInfoColumn.GetAll().ToList<DesignedReportPersonInfoColumn>();
                IList<PersonReserveField> personReserveFieldList = new List<PersonReserveField>();
                if (designedReportPersonInfoColumnList.Count(d => d != null && d.IsReserveField) > 0)
                    personReserveFieldList = new BPersonReservedField().GetAll();
                foreach (DesignedReportPersonInfoColumn item in designedReportPersonInfoColumnList)
                {
                    if (item.IsReserveField)
                    {
                        string itemKey = item.Key.ToString().Replace("prsTA_", "");
                        PersonReserveField personReserveFieldObj = personReserveFieldList.FirstOrDefault(r => r.OrginalName.ToLower().Trim() == itemKey.ToLower().Trim());
                        if (personReserveFieldObj != null)
                            item.Name = personReserveFieldObj.Lable;
                        else
                        {
                            switch (BLanguage.CurrentLocalLanguage)
                            {
                                case LanguagesName.Parsi:
                                    item.Name = item.FnName;
                                    break;
                                case LanguagesName.English:
                                    item.Name = item.EnName;
                                    break;
                                default:
                                    item.Name = item.FnName;
                                    break;
                            }
                        }
                            
                    }
                    else
                    {
                        switch (BLanguage.CurrentLocalLanguage)
                        {
                            case LanguagesName.Parsi:
                                item.Name = item.FnName;
                                break;
                            case LanguagesName.English:
                                item.Name = item.EnName;
                                break;
                            default:
                                item.Name = item.FnName;
                                break;
                        }
                    }
                   
                }
                IList<PersonParamField> personParamFieldList = new BPersonParamFields().GetAll().Where(p => p.Active && p.SubSystemId == SubSystemIdentifier.TimeAtendance).ToList();
                foreach (PersonParamField item in personParamFieldList)
                {
                    DesignedReportPersonInfoColumn designedReportPersonInfoColumnObj = new DesignedReportPersonInfoColumn();
                    designedReportPersonInfoColumnObj.EnName = item.EnTitle;
                    designedReportPersonInfoColumnObj.FnName = item.FnTitle;
         
                    //switch (BLanguage.CurrentLocalLanguage)
                    //{
                    //    case LanguagesName.Unknown:
                    //        conceptProxy.Name = item.FnTitle;
                    //        break;
                    //    case LanguagesName.Parsi:
                    //        conceptProxy.Name = item.FnTitle;
                    //        break;
                    //    case LanguagesName.English:
                    //        conceptProxy.Name = item.EnTitle;
                    //        break;
                    //    default:
                    //        conceptProxy.Name = item.FnTitle;
                    //        break;
                    //}
                    //conceptProxyList.Add(conceptProxy);

                }
                designedReportPersonInfoColumnList = designedReportPersonInfoColumnList.OrderBy(o => o.Name).ToList<DesignedReportPersonInfoColumn>();
                return designedReportPersonInfoColumnList;
            }
            catch (Exception ex)
            {
                LogException(ex, "BDesignedReportsColumn", "GetAllDesignedReportsPersonInfoColumns");
                throw ex;
            }
        }
        public IList<DesignedColumnProxy> GetAllDesignedReportsPersonInfoProxyColumns(decimal reportId)
        {
            try
            {
                IList<DesignedColumnProxy> designedColumnProxyList = new List<DesignedColumnProxy>();
                IList<DesignedReportPersonInfoColumn> designedReportPersonInfoColumnList = repDesignedReportPersonInfoColumn.GetAll().ToList<DesignedReportPersonInfoColumn>();
                IList<PersonReserveField> personReserveFieldList = new List<PersonReserveField>();
                if (designedReportPersonInfoColumnList.Count(d => d != null && d.IsReserveField) > 0)
                    personReserveFieldList = new BPersonReservedField().GetAll();

                foreach (DesignedReportPersonInfoColumn item in designedReportPersonInfoColumnList)
                {
                    DesignedColumnProxy columnProxy = new DesignedColumnProxy();
                    if (item.IsReserveField)
                    {
                        string itemKey = item.Key.ToString().Replace("prsTA_", "");
                        PersonReserveField personReserveFieldObj = personReserveFieldList.FirstOrDefault(r => r.OrginalName.ToLower().Trim() == itemKey.ToLower().Trim());
                        if (personReserveFieldObj != null)
                            columnProxy.Name = personReserveFieldObj.Lable;
                        else
                        {
                            switch (BLanguage.CurrentLocalLanguage)
                            {
                                case LanguagesName.Parsi:
                                    columnProxy.Name = item.FnName;
                                    break;
                                case LanguagesName.English:
                                    columnProxy.Name = item.EnName;
                                    break;
                                default:
                                    columnProxy.Name = item.FnName;
                                    break;
                            }
                        }

                    }
                    else
                    {
                        switch (BLanguage.CurrentLocalLanguage)
                        {
                            case LanguagesName.Parsi:
                                columnProxy.Name = item.FnName;
                                break;
                            case LanguagesName.English:
                                columnProxy.Name = item.EnName;
                                break;
                            default:
                                columnProxy.Name = item.FnName;
                                break;
                        }
                    }
                    columnProxy.ColumnType = DesignedReportColumnType.PersonInfo;
                    columnProxy.ID = item.ID;
                    columnProxy.KeyColumn = item.Key.ToString();
                    designedColumnProxyList.Add(columnProxy);

                }
                designedColumnProxyList = designedColumnProxyList.OrderBy(o => o.Name).ToList<DesignedColumnProxy>();
                Report reportObj = new BReport().GetByID(reportId);
                DesignedReportType reportTypeObj = GetAllDesignedReportsTypes().SingleOrDefault(d => d.CustomCode == DesignedReportTypeEnum.Daily);
                if (reportObj.DesignedType == reportTypeObj)
                {
                    IList<PersonParamField> personParamFieldList = new BPersonParamFields().GetAll().Where(p => p.Active && p.SubSystemId == SubSystemIdentifier.TimeAtendance).ToList();
                    foreach (PersonParamField item in personParamFieldList)
                    {
                        DesignedColumnProxy columnProxy = new DesignedColumnProxy(); DesignedReportPersonInfoColumn designedReportPersonInfoColumnObj = new DesignedReportPersonInfoColumn();
                        columnProxy.EnName = item.EnTitle;
                        columnProxy.FnName = item.FnTitle;
                        switch (BLanguage.CurrentLocalLanguage)
                        {
                            case LanguagesName.Parsi:
                                columnProxy.Name = item.FnTitle;
                                break;
                            case LanguagesName.English:
                                columnProxy.Name = item.EnTitle;
                                break;
                            default:
                                columnProxy.Name = item.FnTitle;
                                break;
                        }
                        columnProxy.ColumnType = DesignedReportColumnType.PersonParam;
                        columnProxy.ID = item.ID;
                        columnProxy.KeyColumn = item.Key;
                        designedColumnProxyList.Add(columnProxy);
                    }
                }

                return designedColumnProxyList;
            }
            catch (Exception ex)
            {
                LogException(ex, "BDesignedReportsColumn", "GetAllDesignedReportsPersonInfoColumns");
                throw ex;
            }
        }
        public IList<DesignedReportType> GetAllDesignedReportsTypes()
        {
            try
            {
                IList<DesignedReportType> designedReportTypeList = new List<DesignedReportType>();
                designedReportTypeList = repDesignedReportType.GetAll().ToList<DesignedReportType>();



                return designedReportTypeList;
            }
            catch (Exception ex)
            {
                LogException(ex, "BDesignedReportsColumn", "GetAllDesignedReportsTypes");
                throw ex;
            }
        }
        private DesignedReportColumnProxy ConvertDesignedReportColumnToProxy(DesignedReportColumn designedReportColumn)
        {
            try
            {
                DesignedReportColumnProxy designedReportColumnProxy = new DesignedReportColumnProxy();
                designedReportColumnProxy.Active = designedReportColumn.Active;

                if (designedReportColumn.Concept != null)
                {
                    switch (BLanguage.CurrentLocalLanguage)
                    {
                        case LanguagesName.Parsi:
                            designedReportColumnProxy.Name = designedReportColumn.Concept.FnName;
                            break;
                        case LanguagesName.English:
                            designedReportColumnProxy.Name = designedReportColumn.Concept.EnName;
                            break;
                        default:
                            designedReportColumnProxy.Name = designedReportColumn.Concept.FnName;
                            break;
                    }
                    designedReportColumnProxy.ConceptID = designedReportColumn.Concept.ID;
                    designedReportColumnProxy.ColumnName = designedReportColumn.Concept.KeyColumnName;
                    designedReportColumnProxy.ColumnType = DesignedReportColumnType.Concept.ToString();
                }
                if (designedReportColumn.PersonInfo != null)
                {
                    IList<PersonReserveField> personReserveFieldList = new List<PersonReserveField>();
                    if (designedReportColumn.PersonInfo.IsReserveField)
                         personReserveFieldList = new BPersonReservedField().GetAll();
                    if (designedReportColumn.PersonInfo.IsReserveField)
                    {
                        string itemKey = designedReportColumn.PersonInfo.Key.ToString().Replace("prsTA_", "");
                        PersonReserveField personReserveFieldObj = personReserveFieldList.FirstOrDefault(r => r.OrginalName.ToLower().Trim() == itemKey.ToLower().Trim());
                        if (personReserveFieldObj != null)
                            designedReportColumnProxy.Name = personReserveFieldObj.Lable;
                        else
                        {
                            switch (BLanguage.CurrentLocalLanguage)
                            {
                                case LanguagesName.Parsi:
                                    designedReportColumnProxy.Name = designedReportColumn.PersonInfo.FnName;
                                    break;
                                case LanguagesName.English:
                                    designedReportColumnProxy.Name = designedReportColumn.PersonInfo.EnName;
                                    break;
                                default:
                                    designedReportColumnProxy.Name = designedReportColumn.PersonInfo.FnName;
                                    break;
                            }
                        }

                    }
                    else
                    {
                        switch (BLanguage.CurrentLocalLanguage)
                        {
                            case LanguagesName.Parsi:
                                designedReportColumnProxy.Name = designedReportColumn.PersonInfo.FnName;
                                break;
                            case LanguagesName.English:
                                designedReportColumnProxy.Name = designedReportColumn.PersonInfo.EnName;
                                break;
                            default:
                                designedReportColumnProxy.Name = designedReportColumn.PersonInfo.FnName;
                                break;
                        }
                    }

                    designedReportColumnProxy.PersonInfoID = designedReportColumn.PersonInfo.ID;
                    designedReportColumnProxy.ColumnName = new BReport().GetDesignedReportColumnFieldNameForDesigned(designedReportColumn);
                    designedReportColumnProxy.ColumnType = DesignedReportColumnType.PersonInfo.ToString();
                }
                if (designedReportColumn.Traffic != null)
                {
                    switch (BLanguage.CurrentLocalLanguage)
                    {
                        case LanguagesName.Parsi:
                            designedReportColumnProxy.Name = designedReportColumn.Traffic.FnName;
                            break;
                        case LanguagesName.English:
                            designedReportColumnProxy.Name = designedReportColumn.Traffic.EnName;
                            break;
                        default:
                            designedReportColumnProxy.Name = designedReportColumn.Traffic.FnName;
                            break;
                    }

                    designedReportColumnProxy.TrafficID = designedReportColumn.Traffic.ID;
                    designedReportColumnProxy.ColumnName = designedReportColumn.Traffic.Key.ToString();
                    designedReportColumnProxy.ColumnType = DesignedReportColumnType.Traffic.ToString();
                    designedReportColumnProxy.TrafficColumnCount = designedReportColumn.TrafficColumnCount.ToString();
                }
                if(designedReportColumn.PersonParam !=null)
                {
                    
                        switch (BLanguage.CurrentLocalLanguage)
                        {
                            case LanguagesName.Parsi:
                                designedReportColumnProxy.Name = designedReportColumn.PersonParam.FnTitle;
                                break;
                            case LanguagesName.English:
                                designedReportColumnProxy.Name = designedReportColumn.PersonParam.EnTitle;
                                break;
                            default:
                                designedReportColumnProxy.Name = designedReportColumn.PersonParam.FnTitle;
                                break;
                        }
                        designedReportColumnProxy.PersonParamID = designedReportColumn.PersonParam.ID;
                        designedReportColumnProxy.ColumnName = new BReport().GetDesignedReportColumnFieldNameForDesigned(designedReportColumn);
                        designedReportColumnProxy.ColumnType = DesignedReportColumnType.PersonParam.ToString();
                    
                }
                designedReportColumnProxy.ID = designedReportColumn.ID;
                designedReportColumnProxy.IsConcept = designedReportColumn.IsConcept;
                designedReportColumnProxy.Order = designedReportColumn.Order;
                designedReportColumnProxy.ReportID = designedReportColumn.Report.ID;
                designedReportColumnProxy.Title = designedReportColumn.Title;
                designedReportColumnProxy.IsGroupColumn = designedReportColumn.IsGroupColumn;
              
                return designedReportColumnProxy;
            }
            catch (Exception ex)
            {

                LogException(ex, "BDesignedReportsColumn", "ConvertDesignedReportColumnToProxy");
                throw ex;
            }
        }

        public IList<DesignedColumnProxy> GetAllConceptAndTrafficColumnsByPeriodicType(ScndCnpPeriodicType priodicType, decimal reportId)
        {
            try
            {
                IList<SecondaryConcept> conceptList = new GTS.Clock.Business.Concept.BConcept().GetAllConceptByPeriodicType(priodicType).Where(c=>c.KeyColumnName!=null && c.KeyColumnName.Trim()!=string.Empty && c.ShowInReport).ToList();
                List<DesignedColumnProxy> conceptProxyList = new List<DesignedColumnProxy>();
                foreach (SecondaryConcept item in conceptList)
                {
                    DesignedColumnProxy conceptProxy = new DesignedColumnProxy(item);
                    conceptProxy.ColumnType = DesignedReportColumnType.Concept;
                    conceptProxy.KeyColumn = item.KeyColumnName;
                    conceptProxyList.Add(conceptProxy);

                }
                Report reportObj = new BReport().GetByID(reportId);
                DesignedReportType reportTypeObj = GetAllDesignedReportsTypes().SingleOrDefault(d => d.CustomCode == DesignedReportTypeEnum.Daily);
                if (reportObj.DesignedType == reportTypeObj)
                {


                    IList<DesignedReportTrafficColumn> designedReportTrafficColumnList = new BDesignedReportsColumn().GetAllDesignedReportsTrafficColumns();
                    int i = 0;
                    foreach (DesignedReportTrafficColumn item in designedReportTrafficColumnList)
                    {
                        DesignedColumnProxy conceptProxy = new DesignedColumnProxy();
                        conceptProxy.FnName = item.FnName;
                        conceptProxy.EnName = item.EnName;
                        conceptProxy.Name = item.Name;
                        conceptProxy.ID = item.ID;
                        conceptProxy.ColumnType = DesignedReportColumnType.Traffic;
                        
                        conceptProxy.KeyColumn = item.Key.ToString();
                        conceptProxyList.Insert(i, conceptProxy);
                        i++;
                    }

                }

                
                return conceptProxyList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public IList<DesignedReportTrafficColumn> GetAllDesignedReportsTrafficColumns()
        {
            try
            {
                IList<DesignedReportTrafficColumn> designedReportTrafficColumnList = new List<DesignedReportTrafficColumn>();
                designedReportTrafficColumnList = repDesignedReportTrafficColumn.GetAll().ToList<DesignedReportTrafficColumn>();

                foreach (DesignedReportTrafficColumn item in designedReportTrafficColumnList)
                {
                    switch (BLanguage.CurrentLocalLanguage)
                    {
                        case LanguagesName.Parsi:
                            item.Name = item.FnName;
                            break;
                        case LanguagesName.English:
                            item.Name = item.EnName;
                            break;
                        default:
                            item.Name = item.FnName;
                            break;
                    }
                }
                designedReportTrafficColumnList = designedReportTrafficColumnList.OrderBy(o => o.Name).ToList<DesignedReportTrafficColumn>();
                return designedReportTrafficColumnList;
            }
            catch (Exception ex)
            {
                LogException(ex, "BDesignedReportsColumn", "GetAllDesignedReportsTrafficColumns");
                throw ex;
            }
        }
    }

}
