using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace AccountsLibrary
{
    public class Authorization
    {
        public static async Task<bool> VerifyToken(string token)
        {
            string output = "0";
            SqlConnection Conn = new SqlConnection(GloablVars.databaseconnectionstring);
            SqlCommand cmd = new SqlCommand("VerifyToken", Conn);


            try
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("token", token);

                await Conn.OpenAsync();
                output = (await cmd.ExecuteScalarAsync()).ToString();
                Conn.Close();
            }
            catch (Exception)
            {
                Conn.Close();
                return false;
            }
            if (output == "0")
            {
                return false;
            }
            return true;
        }
        public static async Task<string> RetrieveId(string token)
        {
            string output = "0";
            SqlConnection Conn = new SqlConnection(GloablVars.databaseconnectionstring);
            SqlCommand cmd = new SqlCommand("GetUserIdByToken", Conn);


            try
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("token", token);

                await Conn.OpenAsync();
                output = (await cmd.ExecuteScalarAsync()).ToString();
                Conn.Close();
            }
            catch (Exception)
            {
                Conn.Close();
                return "";
            }
            if (output == "0")
            {
                return "";
            }
            return output;
        }
    }
}
