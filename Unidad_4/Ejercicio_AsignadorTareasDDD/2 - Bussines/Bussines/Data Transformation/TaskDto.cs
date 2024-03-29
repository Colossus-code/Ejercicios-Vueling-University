﻿using Infrastructure.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Bussines.Data_Transformation
{
    public class TaskDto
    {
        public string TaskDescription { get; set; }
        public string Technology { get; set; }
        public bool Assigned { get; set; }
        public int WorkerId { get; set; }
        public TaskStatus StatusOfTask { get; set; }

        public TaskDto(string taskDesc, string taskTechnology, string taskStatus, bool assigned = false)
        {
            TaskDescription = taskDesc;
            Technology = taskTechnology;
            Assigned = assigned;
            
            switch (taskStatus.Trim().ToLower())
            {
                case "todo":

                    StatusOfTask = TaskStatus.todo;
                    break;
                
                case "doing":

                    StatusOfTask = TaskStatus.doing;
                    break;
                
                case "done":

                    StatusOfTask = TaskStatus.done;
                    break;
            }
       
        }

        public TaskDto(string taskDesc, string taskTechnology, bool assigned = false)
        {
            TaskDescription = taskDesc;
            Technology = taskTechnology;
            Assigned = assigned;
            StatusOfTask = TaskStatus.todo;
        }
    }
}
