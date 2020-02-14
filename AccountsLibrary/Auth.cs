using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AccountsLibrary
{
    public class Auth
    {
        public static async Task<authobject> AuthenticateAsync(string email, string pass)
        {
            var output = new authobject();
            string code = await GetCodeAsync(email);
            if (code == "0")
            {
                output.error = "2";
                return output;
            }
            string hashedpass = ShaHash(pass);
            if (await ComparePasswordAsync(code, hashedpass) == "0")
            {
                output.error = "3";
                return output;
            }
            output.id = code;
            output.token = await CreateAuthTokenAsync(code);
            output.error = "0";

            return output;
        }
        private static async Task<string> GetCodeAsync(string email)
        {
            string code = "0";
            SqlConnection Conn = new SqlConnection(GloablVars.databaseconnectionstring);
            SqlCommand cmd = new SqlCommand("GetUserCodeByEmail", Conn);

            try
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("email", email);

                await Conn.OpenAsync();

                code = (await cmd.ExecuteScalarAsync()).ToString();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                Conn.Close();
            }
            return code;
        }
        private static async Task<string> ComparePasswordAsync(string code, string pass)
        {
            string output = "0";
            SqlConnection Conn = new SqlConnection(GloablVars.databaseconnectionstring);
            SqlCommand cmd = new SqlCommand("ComparePasswordForAuth", Conn);

            try
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("code", code);
                cmd.Parameters.AddWithValue("pass", pass);

                await Conn.OpenAsync();

                output = (await cmd.ExecuteScalarAsync()).ToString();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                Conn.Close();
            }
            return output;
        }
        private static string ShaHash(string value)
        {
            using (var hmac = new HMACSHA512(Encoding.UTF32.GetBytes(value)))
            {
                return ByteToString(hmac.ComputeHash(Encoding.UTF32.GetBytes(value)));
            }
        }
        static string ByteToString(IEnumerable<byte> data)
        {
            return string.Concat(data.Select(b => b.ToString("x2")));
        }

        public class authobject
        {
            public string id { get; set; }
            public string token { get; set; }
            public string error { get; set; }
        }

        private static async Task<string> CreateAuthTokenAsync(string code)
        {
            SqlConnection Conn = new SqlConnection(GloablVars.databaseconnectionstring);
            SqlCommand cmd = new SqlCommand("InsertAuthToken", Conn);

            string token = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 16).ToString();

            try
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("UserCode", code);
                cmd.Parameters.AddWithValue("token", token);

                await Conn.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
            catch (Exception)
            {

                return "";
            }
            finally
            {
                Conn.Close();
            }
            return token;   
        }
    }
}
