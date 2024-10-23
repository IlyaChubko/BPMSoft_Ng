using BPMSoft.Common;
using BPMSoft.Core;
using BPMSoft.Core.DB;
using BPMSoft.Core.Entities;
using BPMSoft_NgExample.Base;
using BPMSoft_NgExample.Models;
using Common.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.InteropServices.ComTypes;
using System.Xml.Linq;
using static BPMSoft_NgExample.Base.ConstCs;
using static BPMSoft_NgExample.Base.ConstCs.Activity;

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
            Insert insert = (Insert)new Insert(_userConnection)
                .Into("Activity")
                .Set("Id", Column.Parameter(data.Id))
                .Set("StatusId", Column.Parameter(ConstCs.Activity.Status.NotStarted))
                .Set("AuthorId", Column.Parameter(_userConnection.CurrentUser.ContactId))
                .Set("PriorityId", Column.Parameter(ConstCs.Activity.Priority.Medium))
                .Set("ActivityCategoryId", Column.Parameter(ConstCs.Activity.Category.Todo))
                .Set("Title", Column.Parameter(data.Title))
                .Set("OwnerId", Column.Parameter(contactId));
            insert.Execute();
        }

        public List<ActivityBaseData> GetRecords(Guid ownerId)
        {
            List<ActivityBaseData> activityList = new List<ActivityBaseData> { };
            Select select = new Select(_userConnection)
                .Column("Id")
                .Column("Title")
                .Column("StartDate")
                .Column("StatusId")
                .From("Activity")
                .Where("OwnerId").IsEqual(Column.Parameter(ownerId)) as Select;
            using (DBExecutor dbExecutor = _userConnection.EnsureDBConnection())
            {
                using (IDataReader dataReader = select.ExecuteReader(dbExecutor))
                {
                    while (dataReader.Read())
                    {
                        activityList.Add(new ActivityBaseData
                        {
                            Id = dataReader.GetColumnValue<Guid>("Id"),
                            Title = dataReader.GetColumnValue<string>("Title"),
                            StartDate = dataReader.GetColumnValue<DateTime>("StartDate").ToString("dd.MM.yyyy"),
                            StatusId = dataReader.GetColumnValue<Guid>("StatusId"),
                        });
                    }
                }
            }
            return activityList;
        }

        public ActivityFullData GetRecord(Guid activityId)
        {
            ActivityFullData activity = new ActivityFullData {};
            Select select = new Select(_userConnection)
                .Column("Activity", "Title")
                .Column("Activity", "StartDate")
                .Column("Activity", "DueDate")
                .Column("Activity", "StatusId")
                .Column("Author", "Name").As("AuthorName")
                .Column("ActivityCategory", "Name").As("CategoryName")
                .From("Activity")
                .InnerJoin("Contact").As("Author").On("Activity", "AuthorId").IsEqual("Author", "Id")
                .InnerJoin("ActivityCategory").As("ActivityCategory").On("Activity", "ActivityCategoryId").IsEqual("ActivityCategory", "Id")
                .Where("Activity", "Id").IsEqual(Column.Parameter(activityId)) as Select;
            using (DBExecutor dbExecutor = _userConnection.EnsureDBConnection())
            {
                using (IDataReader dataReader = select.ExecuteReader(dbExecutor))
                {
                    if (dataReader.Read())
                    {
                        activity = new ActivityFullData
                        {
                            Id = activityId,
                            Title = dataReader.GetColumnValue<string>("Title"),
                            StartDate = dataReader.GetColumnValue<DateTime>("StartDate").ToString("dd.MM.yyyy"),
                            EndDate = dataReader.GetColumnValue<DateTime>("DueDate").ToString("dd.MM.yyyy"),
                            StatusId = dataReader.GetColumnValue<Guid>("StatusId"),
                            Author = dataReader.GetColumnValue<string>("AuthorName"),
                            Category = dataReader.GetColumnValue<string>("CategoryName")
                        };
                    }
                }
            }
            return activity;
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
            Guid statusId = isChecked ? ConstCs.Activity.Status.Finished : ConstCs.Activity.Status.InProgress;
			Update update = new Update(_userConnection, "Activity")
                .Set("StatusId", Column.Parameter(statusId))
                .Where("Id").IsEqual(Column.Parameter(activityId)) as Update;
            update.Execute();
        }

        public List<StatusData> GetStatuses()
        {
            List<StatusData> list = new List<StatusData>();
            Select select = new Select(_userConnection)
                .Column("Id")
                .Column("Name")
                .Column("Finish")
                .From("ActivityStatus") as Select;
            using (DBExecutor dbExecutor = _userConnection.EnsureDBConnection())
            {
                using (IDataReader dataReader = select.ExecuteReader(dbExecutor))
                {
                    while (dataReader.Read())
                    {
                        list.Add(new StatusData
                        {
                            Id = dataReader.GetColumnValue<Guid>("Id"),
                            Name = dataReader.GetColumnValue<string>("Name"),
                            IsFinal = dataReader.GetColumnValue<bool>("Finish"),
                        });
                    }
                }
            }
            return list;
        }

        #endregion
    }
}
