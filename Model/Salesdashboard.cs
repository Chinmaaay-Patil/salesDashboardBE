using Microsoft.Extensions.Configuration;
using SalesDashboard.QuerySetting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SalesDashboard.Model
{

    public class Salesdashboard
    {
        private readonly Microsoft.Extensions.Configuration.IConfiguration _configuration;

        public Salesdashboard(Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<SalesPersons> GetSalesPersonList()
        {
            List<SalesPersons> obj = new List<SalesPersons>();
            var connectionstring = _configuration.GetConnectionString("SalesConnectionString");
            SqlConnection connection = new SqlConnection(connectionstring);
            string selectStatement = "Select * from SalesPerson";
            connection.Open();

            SqlCommand commands = new SqlCommand(selectStatement, connection);

            DataTable dataTable = new DataTable();

            SqlDataAdapter dataAdapter = new SqlDataAdapter(commands);
            dataAdapter.Fill(dataTable);
            foreach (DataRow row in dataTable.Rows)
            {
                SalesPersons obj1 = new SalesPersons();
                obj1.SID =int.Parse(row["SID"].ToString());
                obj1.SalesPersonName = row["SalesPersonName"].ToString();
                obj1.Designation = row["Designation"].ToString();
                obj1.Mobile = row["Mobile"].ToString();
                obj1.Email = row["Email"].ToString();
                obj.Add(obj1);
            }
            connection.Close();
            return obj;
        }

        public DataSet GetDashboardData(string Fromdate, string Todate, int SalesPersonID)
        {
            var connectionstring = _configuration.GetConnectionString("SalesConnectionString");
            DataSet dataSet = new DataSet();

            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SalesDashboard", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@FromDate", SqlDbType.VarChar) { Value = Fromdate });
                    command.Parameters.Add(new SqlParameter("@ToDate", SqlDbType.VarChar) { Value = Todate });
                    command.Parameters.Add(new SqlParameter("@SalesPersonID", SqlDbType.Int) { Value = SalesPersonID });

                    using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command))
                    {
                        dataAdapter.Fill(dataSet);
                    }
                }
            }
            return dataSet;
        }
        public DataSet GetSalesList(string Fromdate, string Todate, int SalesPersonID, int VersionID = 0, int StateID = 0)
        {
            var connectionstring = _configuration.GetConnectionString("SalesConnectionString");
            DataSet dataSet = new DataSet();

            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SalesList", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@FromDate", SqlDbType.VarChar) { Value = Fromdate });
                    command.Parameters.Add(new SqlParameter("@ToDate", SqlDbType.VarChar) { Value = Todate });
                    command.Parameters.Add(new SqlParameter("@SalesPersonID", SqlDbType.Int) { Value = SalesPersonID });
                    command.Parameters.Add(new SqlParameter("@VersionID", SqlDbType.Int) { Value = VersionID });
                    command.Parameters.Add(new SqlParameter("@StateID", SqlDbType.Int) { Value = StateID });

                    using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command))
                    {
                        dataAdapter.Fill(dataSet);
                    }
                }
            }
            return dataSet;
        }
    }
    
}
