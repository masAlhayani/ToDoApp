using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ToDolist.Models
{
    public class ToDo
    {
        public int ToDoID { get; set; }

        [Required(ErrorMessage = "Please Enter task Descrption name")]
        public string TaskDescrption { get; set; }
        [Required(ErrorMessage = "Please Select Category")]
        public string CategoryID { get; set; }
        public Category Category { get; set; }
        [Required(ErrorMessage = "Please Status Category")]
        public string StatusId { get; set; }
        public Status Status { get; set; }
        [Required(ErrorMessage = "Date Can not be empty")]
        public DateTime? DueDate { get; set; }

        public bool overDue => StatusId == "open" && DateTime.Now > DueDate;



    }
}
