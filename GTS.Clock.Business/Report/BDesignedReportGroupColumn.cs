using GTS.Clock.Business.AppSettings;
using GTS.Clock.Infrastructure;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Model.Report;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Business.Reporting
{
   public class BDesignedReportGroupColumn : BaseBusiness<DesignedReportGroupColumn>
    {
       EntityRepository<DesignedReportGroupColumn> designedReportGroupColumnRep = new EntityRepository<DesignedReportGroupColumn>(false);
       const string ExceptionSrc = "GTS.Clock.Business.Reporting.BDesignedReportGroupColumn";
       private ISession NHSession = NHibernateSessionManager.Instance.GetSession();
       public IList<DesignedReportGroupColumn> GetDesignedReportGroupColumns(decimal reportID, decimal personId)
       {
           try
           {

               IList<DesignedReportGroupColumn> designedReportGroupList = designedReportGroupColumnRep.Find(r => r.Report.ID == reportID && r.Person.ID == personId).OrderBy(o=>o.Order).ToList<DesignedReportGroupColumn>();
               foreach (DesignedReportGroupColumn item in designedReportGroupList)
               {
                  
                   if (item.Column.PersonInfo != null)
                   {
                       switch (BLanguage.CurrentLocalLanguage)
                       {
                           case LanguagesName.Parsi:
                               item.Column.PersonInfo.Name = item.Column.PersonInfo.FnName;
                               break;
                           case LanguagesName.English:
                               item.Column.PersonInfo.Name = item.Column.PersonInfo.EnName;
                               break;
                           default:
                               item.Column.PersonInfo.Name = item.Column.PersonInfo.FnName;
                               break;
                       }
                       item.Column.ColumnName = new BReport().GetDesignedReportColumnFieldNameForDesigned(item.Column);
                   }
                   if (item.Column.IsConcept)
                       item.Column.Name = item.Column.Concept.Name;
                   else
                       item.Column.Name = item.Column.PersonInfo.Name;


               }
               return designedReportGroupList;
           }
           catch (Exception ex)
           {
               LogException(ex, "BDesignedReportCondition", "GetDesignedReportCondition");
               throw ex;
           }
       }
       protected override void InsertValidate(DesignedReportGroupColumn obj)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            if (obj.Column == null || obj.Column.ID == 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.DesignedReportColumnIsRequired, "ستونی جهت ثبت انتخاب نشده است.", ExceptionSrc));
            }
            else if (GetDesignedReportGroupColumns(obj.Report.ID, obj.Person.ID).Count > 4)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.DesignedReportColumnIsMax, "حداکثر تعداد ستون های گره بندی 5 می باشد.", ExceptionSrc));
            }
            else 
            {
                DesignedReportGroupColumn groupColumnAilas=null;
                int groupColumnCount = NHSession.QueryOver<DesignedReportGroupColumn>(() => groupColumnAilas)
                                                           .Where(() => groupColumnAilas.Column.ID == obj.Column.ID && groupColumnAilas.Person.ID == obj.Person.ID && groupColumnAilas.Report.ID == obj.Report.ID).RowCount();

                if (groupColumnCount > 0)
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.DesignedReportColumnIsRepeated, "ستون انتخابی تکراری می باشد.", ExceptionSrc));
                }                                         
            }

            if (exception.Count > 0)
            {
                throw exception;
            }
        }

       protected override void UpdateValidate(DesignedReportGroupColumn obj)
        {
   
        }

       protected override void DeleteValidate(DesignedReportGroupColumn obj)
        {
            UIValidationExceptions exception = new UIValidationExceptions();
            if (obj == null || obj.ID == 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.DesignedReportColumnIsRequired, "ستونی جهت حذف انتخاب نشده است.", ExceptionSrc));
            }
            if (exception.Count > 0)
            {
                throw exception;
            }
        }

       public decimal InsertGroupColumn(DesignedReportGroupColumn groupColumn)
       {
           try
           {

               decimal id = this.SaveChanges(groupColumn, UIActionType.ADD);
               return id;
           }
           catch (Exception ex)
           {
               LogException(ex, "BDesignedReportGroupColumn", "InsertGroupColumn");
               throw ex;
           }
       }
       public decimal DeleteGroupColumn(DesignedReportGroupColumn groupColumn)
       {
           try
           {

               decimal id = this.SaveChanges(groupColumn, UIActionType.DELETE);
               return id;
           }
           catch (Exception ex)
           {
               LogException(ex, "BDesignedReportGroupColumn", "DeleteGroupColumn");
               throw ex;
           }
       }
       public decimal UpdateGroupColumn(DesignedReportGroupColumn groupColumn)
       {
           try
           {

               decimal id = this.SaveChanges(groupColumn, UIActionType.EDIT);
               return id;
           }
           catch (Exception ex)
           {
               LogException(ex, "BDesignedReportGroupColumn", "UpdateGroupColumn");
               throw ex;
           }
       }
    }
}
