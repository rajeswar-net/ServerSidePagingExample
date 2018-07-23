using ServerSidePagingExample.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;

namespace ServerSidePagingExample.Services
{
    public class SalesOrderService
    {
        public SalesOrderService()
        {

        }

        public List<SalesOrderDetail> LoadData()
        {
            List<SalesOrderDetail> lstSODetail = new List<SalesOrderDetail>();
            try
            {
                string line = string.Empty;
                string srcFilePath = "files/SalesOrderDetail.txt";
                var rootPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
                var fullPath = Path.Combine(rootPath, srcFilePath);
                string filePath = new Uri(fullPath).LocalPath;
                StreamReader sr = new StreamReader(new FileStream(filePath, FileMode.Open, FileAccess.Read));

                while ((line = sr.ReadLine()) != null)
                {

                    SalesOrderDetail salesOrderDetail = new SalesOrderDetail();
                    string[] info = line.Split(',');

                    salesOrderDetail.Sr = Convert.ToInt32(info[0].ToString());
                    salesOrderDetail.OrderTrackNumber = info[1].ToString();
                    salesOrderDetail.Quantity = Convert.ToInt32(info[2].ToString());
                    salesOrderDetail.ProductName = info[3].ToString();
                    salesOrderDetail.SpecialOffer = info[4].ToString();
                    salesOrderDetail.UnitPrice = Convert.ToDouble(info[5].ToString());
                    salesOrderDetail.UnitPriceDiscount = Convert.ToDouble(info[6].ToString());

                    lstSODetail.Add(salesOrderDetail);
                }

                sr.Dispose();
                sr.Close();
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            return lstSODetail;
        }
    }
}