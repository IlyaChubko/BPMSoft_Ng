using BPMSoft.Web.Common;
using BPMSoft_NgExample.Helpers;
using BPMSoft_NgExample.Models;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;

namespace BPMSoft_NgExample.Services
{
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    public class ActivityService : BaseService
    {
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        public void AddRecord(Guid contactId, ActivityBaseData data)
        {
            ActivityHelper helper = new ActivityHelper(UserConnection);
            helper.AddRecord(contactId, data);
        }

        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        public List<ActivityBaseData> GetRecords(Guid ownerId)
        {
            ActivityHelper helper = new ActivityHelper(UserConnection);
            return helper.GetRecords(ownerId);
        }

        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        public ActivityFullData GetRecord(Guid activityId)
        {
            ActivityHelper helper = new ActivityHelper(UserConnection);
            return helper.GetRecord(activityId);
        }

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        public void CheckRecord(Guid activityId, bool isChecked)
        {
            ActivityHelper helper = new ActivityHelper(UserConnection);
            helper.CheckRecord(activityId, isChecked);
        }

        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        public List<StatusData> GetStatuses()
        {
            ActivityHelper helper = new ActivityHelper(UserConnection);
            return helper.GetStatuses();
        }
    }
}
