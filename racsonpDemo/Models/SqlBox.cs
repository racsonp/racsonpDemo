using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.EnterpriseServices.Internal;

namespace racsonpDemo.Models
{
    public class SqlBox
    {

        public SqlBox()
        {
            DataTable = new DataTable();
            Format = 1;
        }

        public bool Successful { get; set; }
        public string Message { get; set; }



        //public DataTable GetData()
        //{

        //    string connString = "Data Source=NITRO;Initial Catalog=dbcfbd8711d7404f4ea163a5e6004b84a5;Integrated Security=SSPI";
        //    var query = "SELECT * FROM [dbo].[Producto]";
        //    DataTable results = new DataTable();

        //    using (SqlConnection conn = new SqlConnection(connString))
        //    using (SqlCommand command = new SqlCommand(query, conn))
        //    using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command))
        //        dataAdapter.Fill(results);
        //    return results;
        //}

        //private string _query = "SELECT Id, Nombre, Precio, Tienda FROM [dbo].[Producto]";       
        //public string Query
        //{
        //    get
        //    {
        //        return _query;
        //    }
        //    set
        //    {
        //        _query = value;
        //    }
        //}


        [DataType(DataType.MultilineText)]
        
        public string Query { get; set; }
        public int Top { get; set; }

        //public string Format { get; set; }
        public int Format { get; set; }
        //public DataTable DataTable { get; set; }

        //private DataTable _dataTable;
        //public DataTable DataTable
        //{
        //    get
        //    {
        //        return GetData();
        //    }
        //    set
        //    {
        //        _dataTable = value;
        //    }
        //}


        public DataTable DataTable { get; set; }


    }

    public class SqlBoxExcuter
    {
        private SqlBox _slqBox = new  SqlBox();

        public SqlBox  GetData(string sql)
        {
            _slqBox.Query = sql;

            try
            {
                string connString = "Data Source=4baa651c-5c14-4e85-b2d9-a612008b807f.sqlserver.sequelizer.com;Initial Catalog=db4baa651c5c144e85b2d9a612008b807f;User ID=aoegfdnjackygygk;Password=Y7XDMW7dHrqxkXab2nvAi3W7xcWKvFqkFjhzxV8nKhN3YikjexebKVq5jZgJ8vPc";
                //string connString = "Data Source=NITRO;Initial Catalog=dbcfbd8711d7404f4ea163a5e6004b84a5;Integrated Security=SSPI";
                var query = sql;
                DataTable results = new DataTable();

                using (SqlConnection conn = new SqlConnection(connString))
                using (SqlCommand command = new SqlCommand(query, conn))
                using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command))
                    dataAdapter.Fill(results);

                _slqBox.DataTable = results;
                _slqBox.Successful = true;
              
            }
            catch (Exception ex)
            {
                _slqBox.Successful = false;
                _slqBox.Message = ex.Message;
                
            }

            return _slqBox;

        }


    }
}

