using BPMSoft.Common;
using BPMSoft.Core;
using BPMSoft.Core.DB;
using BPMSoft.Core.Entities;
using BPMSoft_NgExample.Base;
using BPMSoft_NgExample.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Xml.Linq;

namespace BPMSoft_NgExample.Helpers
{
	public class ActivityHelper
    {
		#region Fields

		private readonly UserConnection _userConnection;

        #endregion

        #region Constructor

        public ActivityHelper(UserConnection userConnection)
		{
			_userConnection = userConnection;
		}

		#endregion

		#region Methods: Private

		#endregion

		#region Methods: Public

        public void AddRecord(Guid contactId, ActivityBaseData data)
        {
            var schema = _userConnection.EntitySchemaManager.GetInstanceByName("Activity");
            var entity = schema.CreateEntity(_userConnection);
            entity.SetColumnValue("Id", data.Id);
            entity.SetColumnValue("StatusId", ConstCs.Activity.Status.NotStarted);
            entity.SetColumnValue("AuthorId", _userConnection.CurrentUser.ContactId);
            entity.SetColumnValue("PriorityId", ConstCs.Activity.Priority.Medium);
            entity.SetColumnValue("ActivityCategoryId", ConstCs.Activity.Category.Todo);
            entity.SetColumnValue("Title", data.Title);
            entity.SetColumnValue("OwnerId", contactId);
            entity.Save();
        }

        public List<ActivityBaseData> GetRecords(Guid ownerId)
        {
            var esq = new EntitySchemaQuery(_userConnection.EntitySchemaManager, "Activity");
            var idColumn = esq.AddColumn("Id");
            var titleColumn = esq.AddColumn("Title");
            var startDateColumn = esq.AddColumn("StartDate");
            var statusColumn = esq.AddColumn("Status.Id");
            esq.Filters.Add(esq.CreateFilterWithParameters(FilterComparisonType.Equal, "Owner.Id", ownerId));
            var entities = esq.GetEntityCollection(_userConnection);
            return entities.Select(entity => new ActivityBaseData
            {
                Id = entity.GetTypedColumnValue<Guid>(idColumn.Name),
                Title = entity.GetTypedColumnValue<string>(titleColumn.Name),
                StartDate = entity.GetTypedColumnValue<DateTime>(startDateColumn.Name).ToString("dd.MM.yyyy"),
                StatusId = entity.GetTypedColumnValue<Guid>(statusColumn.Name)
            }).ToList();
        }

        public ActivityFullData GetRecord(Guid activityId)
        {
            var esq = new EntitySchemaQuery(_userConnection.EntitySchemaManager, "Activity");
            var titleColumn = esq.AddColumn("Title");
            var startDateColumn = esq.AddColumn("StartDate");
            var dueDateColumn = esq.AddColumn("DueDate");
            var statusColumn = esq.AddColumn("Status.Id");
            var authorColumn = esq.AddColumn("Author.Name");
            var categoryColumn = esq.AddColumn("ActivityCategory.Name");
            esq.Filters.Add(esq.CreateFilterWithParameters(FilterComparisonType.Equal, "Id", activityId));
            var entity = esq.GetEntityCollection(_userConnection).FirstOrDefault();
            return entity != null ? new ActivityFullData
            {
                Id = activityId,
                Title = entity.GetTypedColumnValue<string>(titleColumn.Name),
                StartDate = entity.GetTypedColumnValue<DateTime>(startDateColumn.Name).ToString("dd.MM.yyyy"),
                EndDate = entity.GetTypedColumnValue<DateTime>(dueDateColumn.Name).ToString("dd.MM.yyyy"),
                StatusId = entity.GetTypedColumnValue<Guid>(statusColumn.Name),
                Author = entity.GetTypedColumnValue<string>(authorColumn.Name),
                Category = entity.GetTypedColumnValue<string>(categoryColumn.Name)
            } : null;
        }

        public void DeleteRecord(Guid activityId)
        {
            var esq = new EntitySchemaQuery(_userConnection.EntitySchemaManager, "Activity");
            esq.AddAllSchemaColumns();
            var entity = esq.GetEntity(_userConnection, activityId);
            entity.Delete();
        }

        public void CheckRecord(Guid activityId, bool isChecked)
        {
            var statusId = isChecked ? ConstCs.Activity.Status.Finished : ConstCs.Activity.Status.InProgress;
            var schema = _userConnection.EntitySchemaManager.GetInstanceByName("Activity");
            var entity = schema.CreateEntity(_userConnection);
            if (entity.FetchFromDB(activityId))
            {
                entity.SetColumnValue("StatusId", statusId);
                entity.Save();
            }
        }

        public List<StatusData> GetStatuses()
        {
            var esq = new EntitySchemaQuery(_userConnection.EntitySchemaManager, "ActivityStatus");
            var idColumn = esq.AddColumn("Id");
            var nameColumn = esq.AddColumn("Name");
            var finishColumn = esq.AddColumn("Finish");
            var entities = esq.GetEntityCollection(_userConnection);
            return entities.Select(entity => new StatusData
            {
                Id = entity.GetTypedColumnValue<Guid>(idColumn.Name),
                Name = entity.GetTypedColumnValue<string>(nameColumn.Name),
                IsFinal = entity.GetTypedColumnValue<bool>(finishColumn.Name)
            }).ToList();
        }
        #endregion
    }
}
