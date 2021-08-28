using System.Collections.Generic;

namespace ToDolist.Models
{
    public class Filters
    {

        public Filters(string filterString) // intialize all the prop for every call to Filters objcet
        {
            FilterString = filterString ?? "all-all-all";
            string[] filters = FilterString.Split('-');
            CategoryId = filters[0];
            Due = filters[1];
            StatusId = filters[2];
            
        }
        public string FilterString { get; set; }
        public string CategoryId { get;}
        public string StatusId { get;  }
        public string Due { get;}
         // the coming code to know if any prop has fliter , and to this check if it not "all", then set it to true
        public bool HasCategory => CategoryId.ToLower() != "all";
        public bool HasStatus => StatusId.ToLower() != "all";
        public bool HasDue => Due.ToLower() != "all";
         // the due flitter it self has a sub-flitter past,tody,future
        public static Dictionary<string, string> DueFilterValues =>
        
            new Dictionary<string, string> // another way to make a refernce to your data, no need to make a table in the database , we will use it in the view
            {
                {"Past","past" },
                {"Today","today" },
                {"Future","future" },
            };
        // know to check wich DueFilterValues was selected ? with the string Due prop , we will use this indicator to make color if due data was exceed
        public bool IsPast=> Due.ToLower() == "past";
        public bool IsToday => Due.ToLower() == "today";
        public bool IsFuture => Due.ToLower() == "future";


    }
}
