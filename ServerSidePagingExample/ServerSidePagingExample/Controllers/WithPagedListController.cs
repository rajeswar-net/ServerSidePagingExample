using PagedList;
using ServerSidePagingExample.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServerSidePagingExample.Controllers
{
    public class WithPagedListController : Controller
    {
        private SalesOrderService _salesOrderService;
        public WithPagedListController()
        {
            _salesOrderService = new SalesOrderService();
        }
        // GET: WithPagedList
        public ActionResult Index(int? page)
        {
            int pageNumber = page ?? 1;
            int pageSize = 10;
            var lstSalesOrderDetails = _salesOrderService.LoadData();
            //var salesList = (from s in lstSalesOrderDetails
            //                orderby s.ProductName
            //                select s).Skip((pageNumber-1)*pageSize).Take(pageSize);
            return View(lstSalesOrderDetails.ToPagedList(pageNumber,pageSize));
        }
    }
}