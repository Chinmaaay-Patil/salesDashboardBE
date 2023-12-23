using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesDashboard.Model;
using SalesDashboard.QuerySetting;
using System.Data;

namespace SalesDashboard.Controllers
{
    public class SalesController : Controller
    {
        private readonly Salesdashboard _salesdashboard;

        public SalesController(Salesdashboard salesdashboard)
        {
            _salesdashboard = salesdashboard;
        }

        [HttpGet("GetSalesPersonList")]
        public List<SalesPersons> GetSalesPersonList()
        {
            List<SalesPersons> obj = new List<SalesPersons>();
            obj = _salesdashboard.GetSalesPersonList();
            return obj;
        }
        [HttpGet("GetDashboardData")]
        public IActionResult GetDashboardData(string Fromdate,string Todate,int SalesPersonID)
        {
            DataSet dataSet = new DataSet();

            if (SalesPersonID==null) {
                SalesPersonID = 0;
            }
            if (Fromdate == null|| Fromdate == "")
            {
                Fromdate = DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00"; 
            }
            if (Todate == ""|| Todate == null)
            {
                Todate = DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59"; 
            }
            try
            {
                dataSet = _salesdashboard.GetDashboardData(Fromdate, Todate, SalesPersonID);
                var jsonResult = new JsonResult(dataSet.Tables.Cast<DataTable>().Select(table =>
                table.Rows.Cast<DataRow>().Select(row =>
                    table.Columns.Cast<DataColumn>().ToDictionary(column => column.ColumnName, column => row[column])
                    )
                ));
                return jsonResult;
            }
            catch(Exception ex)
            {
                return null;

            }

            
        }

        [HttpGet("GetSalesList")]
        public IActionResult GetSalesList(string Fromdate, string Todate, int SalesPersonID,int VersionID=0,int StateID=0)
        {
            DataSet dataSet = new DataSet();

            if (SalesPersonID == null)
            {
                SalesPersonID = 0;
            }
            if (Fromdate == null || Fromdate == "")
            {
                Fromdate = DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00";
            }
            if (Todate == "" || Todate == null)
            {
                Todate = DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59";
            }
            try
            {
                dataSet = _salesdashboard.GetSalesList(Fromdate, Todate, SalesPersonID, VersionID, StateID);
                var jsonResult = new JsonResult(dataSet.Tables.Cast<DataTable>().Select(table =>
                table.Rows.Cast<DataRow>().Select(row =>
                    table.Columns.Cast<DataColumn>().ToDictionary(column => column.ColumnName, column => row[column])
                    )
                ));
                return jsonResult;
            }
            catch (Exception ex)
            {
                return null;

            }


        }
    }
}
