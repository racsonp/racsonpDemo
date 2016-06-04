using System;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
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

        [DataType(DataType.MultilineText)]        
        public string Query { get; set; }
       // public int Top { get; set; }

        public int Format { get; set; }

        public DataTable DataTable { get; set; }


    }

    public class SqlBoxExecuter
    {
        private readonly SqlBox _slqBox = new  SqlBox();

        public SqlBox  GetData(string sql)
        {
            _slqBox.Query = sql;

            try
            {
                var connString = ConfigurationManager.ConnectionStrings["DataBaseContext"].ConnectionString;
                var results = new DataTable();

                using (SqlConnection conn = new SqlConnection(connString))
                using (SqlCommand command = new SqlCommand(sql, conn))
                using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command))
                    dataAdapter.Fill(results);

                _slqBox.DataTable = results;
                _slqBox.Successful = true;
              
            }
            catch (Exception ex)
            {
                _slqBox.Successful = false;
                _slqBox.Message = ex.Message;
                _slqBox.Query = sql;
            }

            return _slqBox;

        }


    }
}

