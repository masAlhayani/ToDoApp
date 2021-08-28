using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ToDolist.Models;

namespace ToDolist.Controllers
{
    public class HomeController : Controller
    {
        private readonly ToDoListContext context;
        public HomeController(ToDoListContext ctx)
        {
            context = ctx;
        }

        public IActionResult Index(string id)
        {
            var model = new ViewModel();
            model.Filters = new Filters(id);
            model.DueFilters = Filters.DueFilterValues;
            model.Category = context.Categories.ToList();
            model.Status = context.Statuses.ToList();
           
        IQueryable<ToDo> query = context.ToDos.Include(t=>t.Category).Include(t=>t.Status);

            if (model.Filters.HasCategory) //filter 1 using if statment not else-if, beacuse we want to check all Fillters (conditions) ,May more than one flitter applied 
                query = query.Where(t=>t.CategoryID==model.Filters.CategoryId);
            if (model.Filters.HasStatus)
                query = query.Where(t=>t.StatusId==model.Filters.StatusId); //filter 2
            if (model.Filters.HasDue) //filter 3 
            {
                DateTime TodayDate = DateTime.Today;
                if (model.Filters.IsToday)                             // value to make isTody , IsPast,IsFuture true will be assign later
                    query = query.Where(t => t.DueDate == TodayDate);
                else if
                    (model.Filters.IsPast)
                    query = query.Where(t => t.DueDate < TodayDate);
                else if
                  (model.Filters.IsFuture)
                    query = query.Where(t => t.DueDate > TodayDate);
            }
            var Tasks = query.OrderBy(m=>m.DueDate).ToList();
            model.Task = Tasks;        
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // add task action
        [HttpGet]
        public IActionResult Add()
        {
            ViewModel model = new ViewModel();
            model.Category = context.Categories.ToList();
            model.Status = context.Statuses.ToList();


            return View(model);
        }
        [HttpPost]
        public IActionResult Add(ViewModel model)
        {
            if (ModelState.IsValid)
            {
                context.ToDos.Add(model.CurrentTask);
                context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            else
                model.Category = context.Categories.ToList(); //  we need it to display again if user do something wrong
            model.Status = context.Statuses.ToList();
            return View(model);
            
            
        }

        [HttpPost]
        public IActionResult EditDelete([FromRoute] string id, ToDo selected) // passing the last filtter from route to keep user at same fliter at th end
        {
            if (selected.StatusId == null)
                context.ToDos.Remove(selected);
            else
            {
                string newStatusId = selected.StatusId;
                selected = context.ToDos.Find(selected.ToDoID);
                selected.StatusId = newStatusId;
                context.ToDos.Update(selected);
            }

            context.SaveChanges();

            return RedirectToAction("Index", "Home", new {  ID = id });  // passing ID from the route back to index
        }

        [HttpPost]
        public IActionResult Filter(string[] filter)
        {
            string id = string.Join('-',filter);
            return RedirectToAction("Index","Home",new { ID=id});
        }
    }
}
