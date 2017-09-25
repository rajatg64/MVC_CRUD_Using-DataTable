using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCCurdDatatable.Models;
using System.Data.Entity;

namespace MVCCurdDatatable.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetData()
        {
            using (DbModel db= new DbModel())
            {
                List<Employee> empList = db.Employees.ToList<Employee>();
                return Json(new { data = empList }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult AddorEdit(int id = 0)
        {
            if (id == 0)
            {
                return View(new Employee());
            }
            else
            {
                using (DbModel db = new DbModel())
                {
                    return View(db.Employees.Where(x => x.EmployeeId == id).FirstOrDefault<Employee>());
                    
                }

            }
        }
        [HttpPost]
        public ActionResult AddorEdit(Employee emp)
        {
            using (DbModel db = new DbModel())
            {
                if (emp.EmployeeId == 0)
                {
                    db.Employees.Add(emp);
                    db.SaveChanges();
                    return Json(new { success = true, message = "Saved Sucessfully" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    db.Entry(emp).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(new { success = true, message = "Updated Sucessfully" }, JsonRequestBehavior.AllowGet);
                }
                
            }
                
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            using (DbModel db = new DbModel())
            {
                Employee emp = db.Employees.Where(x => x.EmployeeId == id).FirstOrDefault<Employee>();
                db.Employees.Remove(emp);
                db.SaveChanges();
                return Json(new { success = true, message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}