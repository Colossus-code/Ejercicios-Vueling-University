using Infrastructure.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Infrastructure.IRepository
{
    public interface IRepositoryTask
    {
        List<Task> getTasks();

        bool setTaskList(Task newTask);

        bool setTask(Task newTask);

        Task getTaskById(int taskId);
    }
}
