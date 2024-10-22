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
        public void AddActivity(Guid ownerId, string title)
        {
            ActivityHelper helper = new ActivityHelper(UserConnection);
            helper.AddActivity(ownerId, title);
        }

        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        public List<ActivityBaseData> GetActivities(Guid ownerId)
        {
            ActivityHelper helper = new ActivityHelper(UserConnection);
            return helper.GetActivities(ownerId);
        }

        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        public ActivityFullData GetActivity(Guid activityId)
        {
            ActivityHelper helper = new ActivityHelper(UserConnection);
            return helper.GetActivity(activityId);
        }

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        public void DeleteActivity(Guid activityId)
        {
            ActivityHelper helper = new ActivityHelper(UserConnection);
            helper.DeleteActivity(activityId);
        }

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        public void SetActivityStatusFinished(Guid activityId)
        {
            ActivityHelper helper = new ActivityHelper(UserConnection);
            helper.SetActivityStatusFinished(activityId);
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
