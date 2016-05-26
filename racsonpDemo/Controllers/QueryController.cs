using System;
using System.Collections.Generic;
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


        public DataTable GetData()
        {
//        
//            Server=4baa651c-5c14-4e85-b2d9-a612008b807f.sqlserver.sequelizer.com
//;Database=db4baa651c5c144e85b2d9a612008b807f
//;User ID=aoegfdnjackygygk
//;Password=Y7XDMW7dHrqxkXab2nvAi3W7xcWKvFqkFjhzxV8nKhN3YikjexebKVq5jZgJ8vPc;
          
            //string connString = "Data Source=NITRO;Initial Catalog=db4baa651c5c144e85b2d9a612008b807f;User ID=sa;Password=Microsoft201";
            string connString = "Data Source=4baa651c-5c14-4e85-b2d9-a612008b807f.sqlserver.sequelizer.com;Initial Catalog=db4baa651c5c144e85b2d9a612008b807f;User ID=aoegfdnjackygygk;Password=Y7XDMW7dHrqxkXab2nvAi3W7xcWKvFqkFjhzxV8nKhN3YikjexebKVq5jZgJ8vPc";
            var query = "SELECT * FROM [dbo].[Producto]";
            DataTable results = new DataTable();

            using (SqlConnection conn = new SqlConnection(connString))
            using (SqlCommand command = new SqlCommand(query, conn))
            using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command))
                dataAdapter.Fill(results);
            return results;
        }

        //public async Task<ActionResult> ExportExcel(string sqlQuery)
        //{
        //    return RedirectToAction("Index");
        //    // Do stuff with the id
        //}
       
  
            
        
        
        public ActionResult ExportExcel(string sqlQuery)
        {

            //http://techbrij.com/export-excel-xls-xlsx-asp-net-npoi-epplus

            DataTable dt = GetData();




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
            DataTable dt =  GetData();

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


       public ActionResult Consulta()
      //  public ActionResult Consulta()
        {
            ViewBag.Active = new List<SelectListItem> {                  
                 new SelectListItem { Text = "EXCEL", Value = "1"},                   
                 new SelectListItem { Text = "CSV", Value = "2"}
             };

           var sqlBox = new SqlBox();
           sqlBox.Format = 1;
            return View(sqlBox);
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
          
               var sql = new SqlBoxExcuter().GetData(sqlBox.Query);
               return View(sql); 
           
          
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

           //var sql = new SqlBoxExcuter().GetData(sqlBox.Query);
           //return RedirectToAction("Index");
           DataTable dt = new SqlBoxExcuter().GetData(sqlBox.Query).DataTable;

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
