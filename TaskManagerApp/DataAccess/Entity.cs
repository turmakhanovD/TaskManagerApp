using System;

namespace TaskManagerApp.TaskManagerApp.DataAccess
{
    public abstract class Entity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}
