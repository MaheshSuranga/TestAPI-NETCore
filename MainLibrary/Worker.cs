using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MainLibrary
{
    public class Worker
    {
        public static int NewWorkerOutput = 0;
        public static async Task<WorkerData> FindWorkerDataByIdAsync(string code, string owner)
        {
            var output = new WorkerData();
            SqlConnection Conn = new SqlConnection(GloablVars.databaseconnectionstring);
            SqlCommand cmd = new SqlCommand("GetWorker", Conn);

            try
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("code", code);
                cmd.Parameters.AddWithValue("owner", owner);

                await Conn.OpenAsync();

                SqlDataReader rd = await cmd.ExecuteReaderAsync();

                while (await rd.ReadAsync())
                {
                    output.name = rd.GetString(0);
                    output.description = await rd.IsDBNullAsync(1) ? rd.GetString(1) : string.Empty;
                    output.location = rd.GetString(2);
                    output.position = rd.GetString(3);
                    output.date = rd.GetDateTime(4);
                }
                Conn.Close();
            }
            catch (Exception)
            {

                Conn.Close();
            }
            return output;
        }
        public static async Task<string> CreateNewAsync(string name, string description, string location,
            string position, string owner)
        {
            NewWorkerOutput = 0;
            string code = await CreateUniqueIdAsync();

            await Task.WhenAll(CreateWorkerAsync(code, name, description, location, position, owner),
                WorkerStatistic.CreateWorkerStatisticsAsync(code,owner));
            if (NewWorkerOutput == 2)
            {
                return code;
            }
            else
            {
                return "";
            }
        }
        public static async Task CreateWorkerAsync(string code, string name, string description, string location,
            string position, string owner)
        {
            SqlConnection Conn = new SqlConnection(GloablVars.databaseconnectionstring);
            SqlCommand cmd = new SqlCommand("InsertWorker", Conn);

            try
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("code", code);
                cmd.Parameters.AddWithValue("name", name);
                cmd.Parameters.AddWithValue("description", description);
                cmd.Parameters.AddWithValue("location", location);
                cmd.Parameters.AddWithValue("position", position);
                cmd.Parameters.AddWithValue("owner", owner);

                await Conn.OpenAsync();
                await cmd.ExecuteNonQueryAsync();

                Conn.Close();
                NewWorkerOutput++;
            }
            catch (Exception)
            {

                Conn.Close();
            }
        }
        public static async Task<bool> DeleteWorkerAsync(string code, string owner)
        {
            bool output = false;
            SqlConnection Conn = new SqlConnection(GloablVars.databaseconnectionstring);
            SqlCommand cmd = new SqlCommand("DeleteWorker", Conn);

            try
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("code", code);
                cmd.Parameters.AddWithValue("owner", owner);

                await Conn.OpenAsync();
                await cmd.ExecuteNonQueryAsync();

                SqlCommand cmd2 = cmd.Clone();
                cmd2.CommandText = "";
                await cmd2.ExecuteNonQueryAsync();
                Conn.Close();
                output = true;
            }
            catch (Exception)
            {
                Conn.Close();
            }
            return output;
        }
        public static async Task<bool> UpadteWorkerAsync(string code, string name, string description,
            string location, string position, string owner)
        {
            bool output = false;
            SqlConnection Conn = new SqlConnection(GloablVars.databaseconnectionstring);
            SqlCommand cmd = new SqlCommand("UpdateWorker", Conn);

            try
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("code", code);
                cmd.Parameters.AddWithValue("name", name);
                cmd.Parameters.AddWithValue("description", description);
                cmd.Parameters.AddWithValue("location", location);
                cmd.Parameters.AddWithValue("position", position);
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
        public static async Task<string> CreateUniqueIdAsync()
        {
            string h;
            string j = "0";

            do
            {
                SqlConnection Conn = new SqlConnection(GloablVars.databaseconnectionstring);
                SqlCommand cmd = new SqlCommand("CheckWorkerCode", Conn);

                h = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 16).ToString();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("code", h);

                await Conn.OpenAsync();

                j = (await cmd.ExecuteScalarAsync()).ToString();
                Conn.Close();
            } while (j == "0" || Regex.IsMatch(h, "[^A-Za-z0-9]+"));

            return h;
        }
        public class WorkerData
        {
            public string name { get; set; }
            public string description { get; set; }
            public string location { get; set; }
            public string position { get; set; }
            public string owner { get; set; }
            public DateTime date { get; set; }

        }
    }
}
