using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using racsonpDemo.Models;

namespace racsonpDemo.Controllers
{
    public class QueryController : Controller
    {
        public ActionResult Config()
        {

            return View();
        }

        // GET: Query
        public ActionResult Index()
        {

            ViewBag.Active = new List<SelectListItem> {                  
                 new SelectListItem { Text = "EXCEL", Value = "1"},                   
                 new SelectListItem { Text = "CSV", Value = "2"}
             };
           // var model = new ProductModel();
           // return View(model);


           // DataTable dt = new SqlBox().GetData();

            var model = new SqlBox();
            model.Query = "SELECT Nombre, Precio, Tienda FROM Producto";
            model.Format = 1;
            return View(model);
            // return View();
        }

        public ActionResult Consulta()
        {
            return RedirectToAction("Index");
            //ViewBag.Active = new List<SelectListItem> {                  
            //       new SelectListItem { Text = "EXCEL", Value = "1"},                   
            //       new SelectListItem { Text = "CSV", Value = "2"}
            //   };

            //var sqlBox = new SqlBox { Format = 1 };
            //return View(sqlBox);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Consulta([Bind(Include = "Query, Format")] SqlBox sqlBox)
        {

            ViewBag.Active = new List<SelectListItem> {                  
                 new SelectListItem { Text = "EXCEL", Value = "1"},                   
                 new SelectListItem { Text = "CSV", Value = "2"}
             };


            if (String.IsNullOrEmpty(sqlBox.Query))
            {
                return RedirectToAction("Index");
            }

            var sql = new SqlBoxExecuter().GetData(sqlBox.Query);
            return View(sql);


        }

  
        public ActionResult ExportExcel(string sqlQuery)
        {

            //http://techbrij.com/export-excel-xls-xlsx-asp-net-npoi-epplus

            DataTable dt = new SqlBoxExecuter().GetData(sqlQuery).DataTable;

    

            //Create a dummy GridView

            var gridView1 = new GridView
            {
                AllowPaging = false,
                DataSource = dt
            };

            gridView1.DataBind();


            Response.Clear();            
            Response.Buffer = true;
            Response.AddHeader("content-disposition","attachment;filename=DataTable.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";

            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            for (int i = 0; i < gridView1.Rows.Count; i++)
            {
                //Apply text style to each Row
                gridView1.Rows[i].Attributes.Add("class", "textmode");
            }

            gridView1.RenderControl(hw);

            //style to format numbers to string

            string style = @"<style> .textmode { mso-number-format:\@; } </style>";

            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();

            return RedirectToAction("Index");
        }

        public ActionResult ExportCSV()
        {

            var sqlQuery = "Select";
            DataTable dt = new SqlBoxExecuter().GetData(sqlQuery).DataTable;

            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=DataTable.csv");
            Response.Charset = "";
            Response.ContentType = "application/text";

            StringBuilder sb = new StringBuilder();

            for (int k = 0; k < dt.Columns.Count; k++)
            {
                //add separator
                sb.Append(dt.Columns[k].ColumnName + ',');
            }
            //append new line

            sb.Append("\r\n");

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                for (int k = 0; k < dt.Columns.Count; k++)
                {
                    //add separator
                    sb.Append(dt.Rows[i][k].ToString().Replace(",", ";") + ',');
                }
                //append new line
                sb.Append("\r\n");
            }
            Response.Output.Write(sb.ToString());
            Response.Flush();
            Response.End();

            return RedirectToAction("Index");
        }


         [HttpPost]
       public ActionResult Export([Bind(Include = "Query, Format")] SqlBox sqlBox)
       {
           ViewBag.Active = new List<SelectListItem> {                  
                 new SelectListItem { Text = "EXCEL", Value = "1"},                   
                 new SelectListItem { Text = "CSV", Value = "2"}
             };


           if (String.IsNullOrEmpty(sqlBox.Query))
           {
               return RedirectToAction("Index");
           }

           //var sql = new SqlBoxExecuter().GetData(sqlBox.Query);
           //return RedirectToAction("Index");
           DataTable dt = new SqlBoxExecuter().GetData(sqlBox.Query).DataTable;

             if (sqlBox.Format == 2)
             {
                

                 Response.Clear();
                 Response.Buffer = true;
                 Response.AddHeader("content-disposition", "attachment;filename=DataTable.csv");
                 Response.Charset = "";
                 Response.ContentType = "application/text";

                 StringBuilder sb = new StringBuilder();

                 for (int k = 0; k < dt.Columns.Count; k++)
                 {
                     //add separator
                     sb.Append(dt.Columns[k].ColumnName + ',');
                 }
                 //append new line

                 sb.Append("\r\n");

                 for (int i = 0; i < dt.Rows.Count; i++)
                 {

                     for (int k = 0; k < dt.Columns.Count; k++)
                     {
                         //add separator
                         sb.Append(dt.Rows[i][k].ToString().Replace(",", ";") + ',');
                     }
                     //append new line
                     sb.Append("\r\n");
                 }
                 Response.Output.Write(sb.ToString());
                 Response.Flush();
                 Response.End();

                 
             }
             else
             {
                
                 //Create a dummy GridView

                 var gridView1 = new GridView
                 {
                     AllowPaging = false,
                     DataSource = dt
                 };

                 gridView1.DataBind();



                 Response.Clear();
                 Response.Buffer = true;
                 Response.AddHeader("content-disposition", "attachment;filename=DataTable.xls");
                 Response.Charset = "";
                 Response.ContentType = "application/vnd.ms-excel";

                 StringWriter sw = new StringWriter();
                 HtmlTextWriter hw = new HtmlTextWriter(sw);

                 for (int i = 0; i < gridView1.Rows.Count; i++)
                 {
                     //Apply text style to each Row
                     gridView1.Rows[i].Attributes.Add("class", "textmode");
                 }

                 gridView1.RenderControl(hw);

                 //style to format numbers to string

                 string style = @"<style> .textmode { mso-number-format:\@; } </style>";

                 Response.Write(style);
                 Response.Output.Write(sw.ToString());
                 Response.Flush();
                 Response.End();             
                 
             }

             return RedirectToAction("Index");
       }
    }
}
