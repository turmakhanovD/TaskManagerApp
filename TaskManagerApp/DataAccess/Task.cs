using System;

namespace TaskManagerApp.TaskManagerApp.DataAccess
{
    public class Task : Entity
    {
        public string TaskName { get; set; }
        public TaskType TypeTask { get; set; }
        public Periodicity Periodicity { get; set; }
        public DateTime? Date { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string DownloadDirectory { get; set; }
        public string EmailAddress { get; set; }
        public string Message { get; set; }
    }
}
