using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDolist.Models
{
    public class ViewModel
    {
        public ViewModel()
        {
            CurrentTask = new ToDo(); // to avoid null excpetion for creating new task
        }
        public Filters Filters { get; set; }
        public List<Category> Category { get; set; }
        public List<ToDo> Task { get; set; }
        public ToDo CurrentTask { get; set; }
        public List<Status> Status { get; set; }
        public Dictionary<string,string> DueFilters { get; set; }
    }
}
