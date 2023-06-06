using Infrastructure.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Infrastructure.Entity
{
    public class Task
    {


        public int TaskId { get; set; }
        public string TaskDescription { get; set; }
        public string Technology { get; set; }
        public bool Assigned { get; set; }
        public int WorkerId { get; set; }
        public TaskStatus StatusOfTask { get; set; }

    }
}
