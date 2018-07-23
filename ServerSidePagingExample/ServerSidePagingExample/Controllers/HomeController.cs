using ServerSidePagingExample.Services;
using ServerSidePagingExample.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServerSidePagingExample.Controllers
{
    public class HomeController : Controller
    {
        private SalesOrderService _salesOrderService;
        public HomeController()
        {
            _salesOrderService = new SalesOrderService();
        }
        public ActionResult Index()
        {
            return View();
                    }
public ActionResult getSalesOrders()
    {
            // Initialization.   
            JsonResult result = new JsonResult();
            try
            {
                // Initialization.   
                //string search = Request.Form.GetValues("search[value]")[0];
                //string draw = Request.Form.GetValues("draw")[0];
                //string order = Request.Form.GetValues("order[0][column]")[0];
                //string orderDir = Request.Form.GetValues("order[0][dir]")[0];
                int startRec = Convert.ToInt32(Request.Form.GetValues("start")[0]);
                int pageSize = Convert.ToInt32(Request.Form.GetValues("length")[0]);
                // Loading.   
                List<SalesOrderDetail> data = _salesOrderService.LoadData();
                // Total record count.   
                int totalRecords = data.Count;

                int recFilter = data.Count;
                // Apply pagination.   
                data = data.Skip(startRec).Take(pageSize).ToList();
                // Loading drop down lists.   
                result = this.Json(new
                {
                    recordsTotal = totalRecords,
                    recordsFiltered = recFilter,
                    data = data
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                // Info   
                Console.Write(ex);
            }
            // Return info.   
            return this.Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}