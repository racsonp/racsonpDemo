using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace racsonpDemo.Models
{
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
                SetMessage(_slqBox);


              
            }
            catch (Exception ex)
            {
                _slqBox.Successful = false;
                _slqBox.Message = ex.Message;
                _slqBox.Query = sql;
            }

            return _slqBox;

        }

        private void SetMessage(SqlBox sqlBox)
        {

            if(  sqlBox.DataTable.Rows.Count < 0 )
            {
                sqlBox.Message = "No rows result";

            }
        }


    }

    public enum Format
    {
        EXCEL,
        CSV
    }



  
}