namespace TaskManagerApp.DataAccess
{
    using global::TaskManagerApp.TaskManagerApp.DataAccess;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class TaskContext : DbContext
    {
        // Контекст настроен для использования строки подключения "TaskContext" из файла конфигурации  
        // приложения (App.config или Web.config). По умолчанию эта строка подключения указывает на базу данных 
        // "TaskManagerApp.DataAccess.TaskContext" в экземпляре LocalDb. 
        // 
        // Если требуется выбрать другую базу данных или поставщик базы данных, измените строку подключения "TaskContext" 
        // в файле конфигурации приложения.
        public TaskContext()
            : base("name=TaskContext1")
        {
        }

        // Добавьте DbSet для каждого типа сущности, который требуется включить в модель. Дополнительные сведения 
        // о настройке и использовании модели Code First см. в статье http://go.microsoft.com/fwlink/?LinkId=390109.

         public virtual DbSet<Task> Tasks { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}