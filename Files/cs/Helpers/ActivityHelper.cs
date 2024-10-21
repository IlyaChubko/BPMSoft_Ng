using BPMSoft.Core;
using BPMSoft_NgExample.Models;
using Common.Logging;
using System;
using System.Collections.Generic;

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

		public void AddActivity(string title)
		{
			throw new System.NotImplementedException();
		}

        public List<ActivityBaseData> GetActivities(Guid ownerId)
        {
            return new List<ActivityBaseData>() {
                new ActivityBaseData {
                    Id = new Guid("76a544ff-25e4-4b70-a470-e49aeddd0611"),
                    Title = "Отправить письмо на согласование 1",
                    StartDate = DateTime.Now.ToString("yyyy-MM-dd"),
                    StatusId =  new Guid("201cfba8-58e6-df11-971b-001d60e938c6")
                },
                new ActivityBaseData {
                    Id = new Guid("76a544ff-25e4-4b70-a470-e49aeddd0612"),
                    Title = "Отправить письмо на согласование 2",
                    StartDate = DateTime.Now.ToString("yyyy-MM-dd"),
                    StatusId =  new Guid("384d4b84-58e6-df11-971b-001d60e938c6")
                },
                new ActivityBaseData {
                    Id = new Guid("76a544ff-25e4-4b70-a470-e49aeddd0613"),
                    Title = "Отправить письмо на согласование 3",
                    StartDate = DateTime.Now.ToString("yyyy-MM-dd"),
                    StatusId =  new Guid("394d4b84-58e6-df11-971b-001d60e938c6")
                },
                new ActivityBaseData {
                    Id = new Guid("76a544ff-25e4-4b70-a470-e49aeddd0614"),
                    Title = "Отправить письмо на согласование 4",
                    StartDate = DateTime.Now.ToString("yyyy-MM-dd"),
                    StatusId =  new Guid("4bdbb88f-58e6-df11-971b-001d60e938c6")
                },
            };
        }

        public ActivityFullData GetActivity(Guid activityId)
        {
            string title = "Отправить письмо на согласование";
            DateTime startDate = DateTime.Now;
            DateTime endDate = DateTime.Now;
            string owner = "Иванов Иван Иванович";
            string category = "Выполнить";

            ActivityFullData response = new ActivityFullData
            {
                Id = activityId,
                Title = title,
                StartDate = startDate.ToString("yyyy-MM-dd"),
                EndDate = endDate.ToString("yyyy-MM-dd"),
                StatusId = new Guid("394d4b84-58e6-df11-971b-001d60e938c6"),
                Owner = owner,
                Category = category
            };
            return response;
        }

        public void DeleteActivity(Guid activityId)
        {
            throw new System.NotImplementedException();
        }

        public List<StatusData> GetStatuses()
        {
            return new List<StatusData>() {
                new StatusData {
                    Id = new Guid("201cfba8-58e6-df11-971b-001d60e938c6"),
                    Name = "Отменена",
                    IsFinal = true,
                },
                new StatusData {
                    Id = new Guid("384d4b84-58e6-df11-971b-001d60e938c6"),
                    Name = "Не начата",
                    IsFinal = false,
                },
                new StatusData {
                    Id = new Guid("394d4b84-58e6-df11-971b-001d60e938c6"),
                    Name = "В работе",
                    IsFinal = false,
                },
                new StatusData {
                    Id = new Guid("4bdbb88f-58e6-df11-971b-001d60e938c6"),
                    Name = "Завершена",
                    IsFinal = true
                }
            };
        }

        #endregion
    }
}
