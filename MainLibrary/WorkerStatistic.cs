using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace MainLibrary
{
    public class WorkerStatistic
    {
        public static async Task CreateWorkerStatisticsAsync(string code, string owner)
        {
            SqlConnection Conn = new SqlConnection(GloablVars.databaseconnectionstring);
            SqlCommand cmd = new SqlCommand("InsertWorkerStatistic", Conn);

            try
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("code", code);
                cmd.Parameters.AddWithValue("owner", owner);

                await Conn.OpenAsync();
                await cmd.ExecuteNonQueryAsync();

                Worker.NewWorkerOutput++;

                Conn.Close();
            }
            catch (Exception)
            {

                Conn.Close();
            }
        }
        public static async Task<workerstatisticsdata> FindWorkerDataByIdAsync(string code, string owner)
        {
            var output = new workerstatisticsdata();
            SqlConnection Conn = new SqlConnection(GloablVars.databaseconnectionstring);
            SqlCommand cmd = new SqlCommand("GetWorkerStatistic", Conn);

            try
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("code", code);
                cmd.Parameters.AddWithValue("owner", owner);
                await Conn.OpenAsync();
                SqlDataReader rd = await cmd.ExecuteReaderAsync();

                while (await rd.ReadAsync())
                {
                    output.hourlyrate = Convert.ToDouble(rd.GetDecimal(0));
                    output.hoursworked = Convert.ToDouble(rd.GetDecimal(1));
                    output.overtimerate = Convert.ToDouble(rd.GetDecimal(2));
                    output.overtimeworked = Convert.ToDouble(rd.GetDecimal(3));
                }
                Conn.Close();
            }
            catch (Exception)
            {

                Conn.Close();
            }
            return output;
        }
        public static async Task<bool> UpdateWorkerStatisticAsync(string code, double hourlyrate, double hoursworked,
            double overtimerate, double overtimeworked, string owner)
        {
            bool output = false;
            SqlConnection Conn = new SqlConnection(GloablVars.databaseconnectionstring);
            SqlCommand cmd = new SqlCommand("UpdateWorkerStatistic", Conn);

            try
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("code", code);
                cmd.Parameters.AddWithValue("hourlyrate", hourlyrate);
                cmd.Parameters.AddWithValue("hoursworked", hoursworked);
                cmd.Parameters.AddWithValue("overtimerate", overtimerate);
                cmd.Parameters.AddWithValue("overtimeworked", overtimeworked);
                cmd.Parameters.AddWithValue("owner", owner);
                await Conn.OpenAsync();
                await cmd.ExecuteNonQueryAsync();

                Conn.Close();
                output = true;
            }
            catch (Exception)
            {

                Conn.Close();
            }
            return output;
        }
        public class workerstatisticsdata
        {
            public double hourlyrate { get; set; }
            public double hoursworked { get; set; }
            public double overtimerate { get; set; }
            public double overtimeworked { get; set; }
        }
    }
}
