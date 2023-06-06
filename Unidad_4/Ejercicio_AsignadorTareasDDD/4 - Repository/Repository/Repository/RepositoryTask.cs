using Infrastructure.Entity;
using Infrastructure.Enum;
using Infrastructure.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Repository
{
    public class RepositoryTask : IRepositoryTask
    {
        private List<Task> _taskList;
        private Task _newTask;

        private static int iteratorId = 4;
        public RepositoryTask()
        {
            _taskList = new List<Task>()
            {
                new Task()
                {
                    TaskId = 1,
                    TaskDescription = "Create data base on SQL Server",
                    Assigned = true,
                    Technology = "SQL",
                    StatusOfTask = TaskStatus.todo,
                    WorkerId = 1
                },

                new Task()
                {
                    TaskId = 2,
                    TaskDescription = "Update file into Web API",
                    Assigned = true,
                    Technology = "C#",
                    StatusOfTask = TaskStatus.doing,
                    WorkerId = 3
                },
                new Task()
                {
                    TaskId = 3,
                    TaskDescription = "Create PDF with the inform of the month",
                    Assigned = true,
                    Technology = "Java",
                    StatusOfTask = TaskStatus.doing,
                    WorkerId = 2
                },
                new Task()
                {
                    TaskId = 4,
                    TaskDescription = "Create this program :D",
                    Assigned = false,
                    Technology = "C#",
                    StatusOfTask = TaskStatus.todo
                }
            };
        }
        public List<Task> getTasks()
        {
            return _taskList.Where(e => e.TaskId > 0).ToList();
        }
        public bool setTaskList(Task newTask)
        {
            try
            {
                _taskList.Add(newTask);
                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool setTask(Task newTask)
        {

            try
            {
                _newTask = new Task();
                _newTask.TaskId = ++iteratorId;
                _newTask = newTask;

                _taskList.Add(_newTask);
                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }
        public Task getTaskById(int taskId)
        {
            try
            {
                return _taskList.FirstOrDefault(e => e.TaskId == taskId);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
