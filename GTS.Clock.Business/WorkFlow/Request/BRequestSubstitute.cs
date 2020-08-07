using GTS.Clock.Business.AppSettings;
using GTS.Clock.Business.Proxy.UiValidation;
using GTS.Clock.Business.UIValidation;
using GTS.Clock.Infrastructure;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Model;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Model.RequestFlow;
using GTS.Clock.Model.UIValidation;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using NHibernate.Criterion;
using GTS.Clock.Business.Temp;

namespace GTS.Clock.Business.RequestFlow
{
    public class BRequestSubstitute : BaseBusiness<RequestSubstitute>
    {
        ISession NHSession = NHibernateSessionManager.Instance.GetSession();
        int operationBatchSizeValue = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings[OperationBatchSize.OperationBatchSizeKey.ToString()]);
        protected override void InsertValidate(RequestSubstitute obj)
        {
        }

        protected override void UpdateValidate(RequestSubstitute obj)
        {
        }

        protected override void DeleteValidate(RequestSubstitute obj)
        {
        }

        public void InsertRequestSubstitute(Request request)
        {
            try
            {
                RequestSubstitute requestSubstitute = null;
                Person person = null;
                Precard precardAlias = null;
                UIValidationGroup uiValidationGroupAlias = null;
                UIValidationRule uiValidationRuleAlias = null;
                UIValidationRuleGroup uiValidationRuleGroupAlias = null;
                UIValidationRuleGroupPrecard uiValidationRuleGroupPrecardAlias = null;
                const string requestSubstituteUIValidationRuleCode = "33";
                BPerson bPerson = new BPerson();
                BUIValidationGroup bUIValidationGroup = new BUIValidationGroup();              
                 UIValidationRuleGroup RuleGroup = new UIValidationRuleGroup();
                JavaScriptSerializer JsSerializer = new JavaScriptSerializer();
                NHibernate.IQueryOver<UIValidationRuleGroup, UIValidationRuleGroup> uiValidationRuleGroup = null;

                if (request != null && request.Person != null && request.Person.ID != 0)
                {
                    person = NHSession.QueryOver<Person>()
                                      .Where(x => x.ID == request.Person.ID)
                                      .SingleOrDefault();
                    if (person != null && person.PersonTASpec != null && person.PersonTASpec.UIValidationGroup != null)
                    {
                        uiValidationRuleGroup = this.NHSession.QueryOver<UIValidationRuleGroup>(() => uiValidationRuleGroupAlias)
                                                                                     .JoinAlias(() => uiValidationRuleGroupAlias.ValidationGroup, () => uiValidationGroupAlias)
                                                                                     .JoinAlias(() => uiValidationRuleGroupAlias.ValidationRule, () => uiValidationRuleAlias)
                                                                                     .JoinAlias(() => uiValidationRuleGroupAlias.PrecardList, () => uiValidationRuleGroupPrecardAlias)
                                                                                     .JoinAlias(() => uiValidationRuleGroupPrecardAlias.Precard, () => precardAlias)
                                                                                     .Where(() => uiValidationGroupAlias.ID == person.PersonTASpec.UIValidationGroup.ID &&
                                                                                                  uiValidationRuleGroupAlias.Active &&
                                                                                                  uiValidationRuleAlias.ExistTag &&
                                                                                                  uiValidationRuleAlias.CustomCode == requestSubstituteUIValidationRuleCode &&
                                                                                                  uiValidationRuleGroupPrecardAlias.Active &&
                                                                                                  precardAlias.ID == request.Precard.ID
                                                                                           );

                        if (request.Person.ID != request.User.Person.ID)                                                  
                            RuleGroup = uiValidationRuleGroup.Where(x => uiValidationRuleGroupPrecardAlias.Operator).SingleOrDefault();                        
                        else
                        RuleGroup = uiValidationRuleGroup.SingleOrDefault();                       
                        if (RuleGroup != null && RuleGroup.Tag != null && RuleGroup.Tag != string.Empty)
                            {
                                R33_UiValidationRuleTagValueProxy r33_UiValidationRuleTagValueProxy = JsSerializer.Deserialize<R33_UiValidationRuleTagValueProxy>(RuleGroup.Tag);
                                if (r33_UiValidationRuleTagValueProxy.IsForceConfirmByRequestSubstitute)
                                {
                                    requestSubstitute = new RequestSubstitute();
                                    requestSubstitute.Request = request;
                                    requestSubstitute.SubstitutePerson = request.SubstitutePerson;
                                    requestSubstitute.OperationDate = DateTime.Now;
                                    this.SaveChanges(requestSubstitute, UIActionType.ADD);
                                }
                            }                                               
                    }
                    NHSession.Evict(person);
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<RequestSubstitute>.LogException(ex, "BRequestSubstitute", "InsertRequestSubstitute");
                throw ex;
            }
        }

        public RequestSubstitute GetRequestSubstitute(decimal requestID, decimal substitutePersonID)
        {
            try
            {
                Person substitutePersonAlias = null;
                Request requestAlias = null;
                RequestSubstitute requestSubstituteAlias = null;
                RequestSubstitute requestSubstitute = this.NHSession.QueryOver<RequestSubstitute>(() => requestSubstituteAlias)
                                                                    .JoinAlias(() => requestSubstituteAlias.Request, () => requestAlias)
                                                                    .JoinAlias(() => requestSubstituteAlias.SubstitutePerson, () => substitutePersonAlias)
                                                                    .Where(() => substitutePersonAlias.ID == substitutePersonID &&
                                                                                 requestAlias.ID == requestID
                                                                          )
                                                                    .SingleOrDefault();
                return requestSubstitute;
            }
            catch (Exception ex)
            {
                BaseBusiness<RequestSubstitute>.LogException(ex, "BRequestSubstitute", "GetRequestSubstitute");
                throw ex;
            }
        }

        protected override void OnSaveChangesSuccess(RequestSubstitute obj, UIActionType action)
        {
            if (action == UIActionType.ADD)
            {
                if (obj.Request != null && obj.Request.ID != 0)
                {
                    BRequest bRequest = new BRequest();
                    Request request = obj.Request;
                    request.RequestSubstitute = obj;
                    bRequest.SaveChanges(request, UIActionType.EDIT);
                }
            }
        }

        public void UpdateRequestSubstituteAccordingToDepartmentOrGradChange(Person person)
        {
            try
            {
                string Description = string.Empty;
                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    Description = "N'رد درخواست به دلیل تغییر مشخصات پرسنل درخواست کننده یا پرسنل جانشین درخواست'";
                }
                else
                {
                    Description = "N'Request Reject because Personel Specifications Change'";
                }
                if (person.Grade.ID != 0)
                {
                    string SQLCommand = "update rSubstitute " +
                                      " set requestSubstitute_Confirmed = 0 , " +
                                      " requestSubstitute_Description = " + Description + " " +
                                     " FROM " +
                                     " TA_RequestSubstitute rSubstitute " +
                                     " JOIN " +
                                     " (select * from TA_Request " +
                                      " inner join TA_Person " +
                                      " on    request_PersonID = Prs_ID " +
                                      " where prs_GradeId !=:gradeId and Prs_DepartmentId !=:depId " +
                                      " )request " +
                                      " on  rSubstitute.requestSubstitute_RequestID = request.request_ID " +
                                      " where  rSubstitute.requestSubstitute_SubstituteID =:PersonId and rSubstitute.requestSubstitute_Confirmed is null  ";


                    NHibernateSessionManager.Instance.GetSession().CreateSQLQuery(SQLCommand)
                                                                  .SetParameter("PersonId", person.ID)
                                                                  .SetParameter("gradeId", person.Grade.ID)
                                                                  .SetParameter("depId", person.Department.ID)
                                                                  .ExecuteUpdate();
                    string sqlCommand = " update rSubstitute " +
                                           "set requestSubstitute_Confirmed = 0 , " +
                                            "requestSubstitute_Description = " + Description + " " +
                                            "FROM " +
                                            "TA_RequestSubstitute rSubstitute " +
                                            "JOIN " +
                                            "(select * from TA_Request " +
                                            "inner join TA_Person " +
                                            "on    request_PersonID = Prs_ID " +
                                            "where prs_GradeId =:gradId and Prs_DepartmentId =:DepId " +
                                            ")request " +
                                            "on  rSubstitute.requestSubstitute_RequestID = request.request_ID " +
                                            "INNER JOIN TA_Person prs " +
                                            "on prs.Prs_ID = rSubstitute.requestSubstitute_SubstituteID " +
                                            "where  request.request_PersonID =:PersonId and " +
                                                    "rSubstitute.requestSubstitute_Confirmed is null and " +
                                                    "prs.Prs_DepartmentId !=:DepId  and " +
                                                    "(prs.prs_GradeId !=:gradId or prs.prs_GradeId is null) ";


                    NHibernateSessionManager.Instance.GetSession().CreateSQLQuery(sqlCommand)
                                                                  .SetParameter("PersonId", person.ID)
                                                                  .SetParameter("gradId", person.Grade.ID)
                                                                  .SetParameter("DepId", person.Department.ID)
                                                                  .ExecuteUpdate();

                }
                else
                {
                    string SQLCommand = " update rSubstitute " +
                                        " set requestSubstitute_Confirmed = 0 , " +
                                        " requestSubstitute_Description = " + Description + " " +
                                        " FROM " +
                                        " TA_RequestSubstitute rSubstitute " +
                                        " JOIN " +
                                        " (select * from TA_Request " +
                                        " inner join TA_Person " +
                                        " on    request_PersonID = Prs_ID " +
                                        " where Prs_DepartmentId !=:depId " +
                                        " )request " +
                                        " on  rSubstitute.requestSubstitute_RequestID = request.request_ID " +
                                        " where ( rSubstitute.requestSubstitute_SubstituteID =:PersonId and rSubstitute.requestSubstitute_Confirmed is null ) or ( request.request_PersonID =:PersonId and rSubstitute.requestSubstitute_Confirmed is null ) ";


                    NHibernateSessionManager.Instance.GetSession().CreateSQLQuery(SQLCommand)
                                                                  .SetParameter("PersonId", person.ID)
                                                                  .SetParameter("depId", person.Department.ID)
                                                                  .ExecuteUpdate();
                    string sqlCommand = " update rSubstitute " +
                                       "set requestSubstitute_Confirmed = 0 , " +
                                        "requestSubstitute_Description = " + Description + " " +
                                        "FROM " +
                                        "TA_RequestSubstitute rSubstitute " +
                                        "JOIN " +
                                        "(select * from TA_Request " +
                                        "inner join TA_Person " +
                                        "on    request_PersonID = Prs_ID " +
                                        "where Prs_DepartmentId =:DepId " +
                                        ")request " +
                                        "on  rSubstitute.requestSubstitute_RequestID = request.request_ID " +
                                        "INNER JOIN TA_Person prs " +
                                        "on prs.Prs_ID = rSubstitute.requestSubstitute_SubstituteID " +
                                        "where  request.request_PersonID =:PersonId and " +
                                                "rSubstitute.requestSubstitute_Confirmed is null and " +
                                                "prs.Prs_DepartmentId !=:DepId ";

                    NHibernateSessionManager.Instance.GetSession().CreateSQLQuery(sqlCommand)
                                                                  .SetParameter("PersonId", person.ID)
                                                                  .SetParameter("DepId", person.Department.ID)
                                                                  .ExecuteUpdate();

                }

            }
            catch (Exception ex)
            {
                BaseBusiness<RequestSubstitute>.LogException(ex, "BRequestSubstitute", "UpdateRequestSubstituteAccordingToDepartmentOrGradChange");
                throw ex;
            }
        }

        public void UpdateRequestSubstituteAccordingToInActiveOrIsDeletedPerson(decimal substituteId)
        {
            try
            {
                string Description = string.Empty;
                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    Description = "N' رد درخواست به دلیل غیر فعال شدن یا حذف پرسنل جانشین درخواست'";
                }
                else
                {
                    Description = "N'Request Reject because Ruqest Substitute Personnel Is InActive Or Deleted'";
                }
                string SQLCommand = "update TA_RequestSubstitute " +
                                    " set    requestSubstitute_Confirmed  = 0 , " +
                                           " requestSubstitute_Description = " + Description + " " +
                                     " where  requestSubstitute_SubstituteID =:substituteId and " +
                                            " requestSubstitute_Confirmed is null";

                NHibernateSessionManager.Instance.GetSession().CreateSQLQuery(SQLCommand)
                                                              .SetParameter("substituteId", substituteId)
                                                              .ExecuteUpdate();
            }
            catch (Exception ex)
            {
                BaseBusiness<RequestSubstitute>.LogException(ex, "BRequestSubstitute", "UpdateRequestSubstituteAccordingToInActiveOrIsDeletedPerson");
                throw ex;
            }
        }

        public void UpdateRequestSubstituteOfOrganicInfo(List<decimal> personIds)
        {
            try
            {
                string Description = string.Empty;
                string SQLCommand = string.Empty;
                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    Description = "N'رد درخواست به دلیل تغییر مشخصات پرسنل درخواست کننده یا پرسنل جانشین درخواست'";
                }
                else
                {
                    Description = "N'Request Reject because Personel Specifications Change'";
                }               
                if (personIds.Count < operationBatchSizeValue && operationBatchSizeValue < 2100)
                {
                    SQLCommand = "update rSubstitute " +
                                 "set requestSubstitute_Confirmed = 0 , " +
                                 "requestSubstitute_Description = " + Description + " " +
                                 "from TA_RequestSubstitute rSubstitute " +
                                 "INNER JOIN TA_Request request " +
                                 "ON  rSubstitute.requestSubstitute_RequestID = request.request_ID " +

                                 "where  rSubstitute.requestSubstitute_SubstituteID IN (:personIds) or " +
                                         "request.request_PersonID IN (:personIds) ";
                    NHibernateSessionManager.Instance.GetSession().CreateSQLQuery(SQLCommand)
                                                                .SetParameterList("personIds", personIds.ToArray())                                                               
                                                                .ExecuteUpdate();
                }
                else
                {
                    TempRepository tempRep = new TempRepository(false);
                    string operationGUID = tempRep.InsertTempList(personIds);
                    SQLCommand = " update rSubstitute " +
                                 " set requestSubstitute_Confirmed = 0 , " +
                                 " requestSubstitute_Description = " + Description + " " +
                                 " from TA_RequestSubstitute rSubstitute " +
                                 " INNER JOIN TA_Request request " +
                                 " ON  rSubstitute.requestSubstitute_RequestID = request.request_ID " +
                                 " INNER JOIN TA_Temp temp " +
                                 " ON  rSubstitute.requestSubstitute_SubstituteID = temp.temp_ObjectID  OR  request.request_PersonID = temp.temp_ObjectID " +
                                 " where temp_OperationGUID =:operationGUID ";
                    NHibernateSessionManager.Instance.GetSession().CreateSQLQuery(SQLCommand)
                                                               .SetParameter("operationGUID", operationGUID)
                                                               .ExecuteUpdate();
                    tempRep.DeleteTempList(operationGUID);
                }
            }
            catch(Exception ex)
            {
                LogException(ex, "BRequestSubstitute", "UpdateRequestSubstituteOfOrganicInfo");
                throw ex;
            }
            
        }

        public IList<Request> GetUnconfirmedRequestsByRequestSubstitute(IList<Request> requestsList)
        {
            try
            {
                IList<Request> unConfirmedRequestsList = null;
                RequestSubstitute requestSubstituteAlias = null;
                GTS.Clock.Model.Temp.Temp tempAlias = new Model.Temp.Temp();
                Request requestAlias = null;
                BTemp bTemp = new BTemp();
                if (requestsList.Count() < this.operationBatchSizeValue && this.operationBatchSizeValue < 2100)
                {
                    unConfirmedRequestsList = this.NHSession.QueryOver<RequestSubstitute>(() => requestSubstituteAlias)
                                                            .JoinAlias(() => requestSubstituteAlias.Request, () => requestAlias)
                                                            .Where(() => !(bool)requestSubstituteAlias.Confirmed &&
                                                                           requestAlias.ID.IsIn(requestsList.Select(x => x.ID).ToArray<decimal>())
                                                                  )
                                                            .Select(x => x.Request)
                                                            .List<Request>();
                }
                else
                {
                    string operationGUID = bTemp.InsertTempList(requestsList.Select(x => x.ID).ToList<decimal>());
                    unConfirmedRequestsList = this.NHSession.QueryOver<RequestSubstitute>(() => requestSubstituteAlias)
                                                            .JoinAlias(() => requestSubstituteAlias.Request, () => requestAlias)
                                                            .JoinAlias(() => requestAlias.TempList, () => tempAlias)
                                                            .Where(() => !(bool)requestSubstituteAlias.Confirmed &&
                                                                          tempAlias.OperationGUID == operationGUID
                                                                  )
                                                            .Select(x => x.Request)
                                                            .List<Request>();
                }

                return unConfirmedRequestsList;
            }
            catch (Exception ex)
            {
                LogException(ex, "BRequestSubstitute", "GetUnconfirmedRequestByRequestSubstitute");
                throw ex;
            }
        }

    }
}
