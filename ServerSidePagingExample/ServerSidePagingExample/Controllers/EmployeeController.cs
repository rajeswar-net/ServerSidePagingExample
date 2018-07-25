using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Web;
using System.Web.Mvc;

namespace ServerSidePagingExample.Controllers
{
    public class EmployeeController : Controller
    {

        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LoadEmployeeData()
        {
            try
            {

                var draw = Request.Form.GetValues("draw").FirstOrDefault();
                var start = Request.Form.GetValues("start").FirstOrDefault();
                var length = Request.Form.GetValues("length").FirstOrDefault();
                var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
                var sortColumnDirection = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
                var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();

                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;
                using (var _emplpoyeeEntity = new EmployeeTrackerEntities())
                {

                    _emplpoyeeEntity.Database.Log = logLinqResult;
                    var employeeData = (from e in _emplpoyeeEntity.employees
                                        select new
                                        {
                                            employee_id = e.employee_id,
                                            first_name = e.first_name,
                                            last_name = e.last_name,
                                            sex = e.sex,
                                            salary = Math.Round(e.salary, 2),
                                            start_date = e.start_date,
                                            age = e.age
                                        });

                    if (!string.IsNullOrEmpty(sortColumn) && !string.IsNullOrEmpty(sortColumnDirection))
                    {
                        employeeData = employeeData.OrderBy(sortColumn + " " + sortColumnDirection);
                    }

                    if (!string.IsNullOrEmpty(searchValue))
                    {
                        employeeData = employeeData.Where(e => e.first_name.Contains(searchValue) || e.last_name.Contains(searchValue));
                    }
                    recordsTotal = employeeData.Count();
                    var finalData = employeeData.Skip(skip).Take(pageSize).ToList();

                    return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = finalData });
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void logLinqResult(string strLinq)
        {
            Console.WriteLine(strLinq);
        }

        public ActionResult WithPagedList(int? page)
        {
            int pageNumber = page ?? 1;
            int pageSize = 10;
            using (var _emplpoyeeEntity = new EmployeeTrackerEntities())
            {
                _emplpoyeeEntity.Database.Log = logLinqResult;
                List<employee> employeeData = (from e in _emplpoyeeEntity.employees
                                               select e).OrderBy("employee_id").ToList();
                //.Skip((pageNumber - 1) * pageSize).Take(pageSize)
                return View(employeeData.ToPagedList(pageNumber, pageSize));
            }
        }
    }
}