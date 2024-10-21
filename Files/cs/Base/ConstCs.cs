using System;

namespace BPMSoft_NgExample.Base
{
	public class ConstCs
    {
        public static class Activity
        {
            public static class Status
            {
                public static readonly Guid Finished = new Guid("4bdbb88f-58e6-df11-971b-001d60e938c6");//Завершена
                public static readonly Guid NotStarted = new Guid("384d4b84-58e6-df11-971b-001d60e938c6");//Не начата
                public static readonly Guid InProgress = new Guid("394D4B84-58E6-DF11-971B-001D60E938C6");//В работе
                public static readonly Guid Canceled = new Guid("201CFBA8-58E6-DF11-971B-001D60E938C6"); //Отменена
            }

            public static class Priority
            {
                public static readonly Guid Low = new Guid("ac96fa02-7fe6-df11-971b-001d60e938c6");//Низкий
                public static readonly Guid Medium = new Guid("ab96fa02-7fe6-df11-971b-001d60e938c6");//Средний
                public static readonly Guid Hight = new Guid("d625a9fc-7ee6-df11-971b-001d60e938c6");//Высокий
            }

            public static class Category
            {
                public static readonly Guid Todo = new Guid("f51c4643-58e6-df11-971b-001d60e938c6");//Выполнить
            }
        }
    }
}
