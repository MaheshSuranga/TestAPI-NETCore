using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AccountsLibrary
{
    public class Register
    {
        public static async Task<string> CreateAsync(string email, string pass, string entity)
        {
            try
            {
                if (await CheckIfEmailExistsAsync(email) == "0")
                {
                    return "2";
                }
            }
            catch (Exception)
            {

                return "0";
            }
            try
            {
                
                string id = await CreateUniqueIdAsync();
                string passhash = ShaHash(pass);

                await Task.WhenAll(InsertDetailsAsync(id, email, entity), InsertUserCredentialsAsync(id, passhash));
            }
            catch (Exception ex)
            {

                return ex.ToString();
            }
            return "1";
        }
        public static async Task InsertDetailsAsync(string code, string email, string entity)
        {
            SqlConnection Conn = new SqlConnection(GloablVars.databaseconnectionstring);
            SqlCommand cmd = new SqlCommand("InsertUserDetails", Conn);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("code", code);
            cmd.Parameters.AddWithValue("email", email);
            cmd.Parameters.AddWithValue("entity", entity);

            await Conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
            Conn.Close();
        }
        public static async Task InsertUserCredentialsAsync(string code, string pass)
        {
            SqlConnection Conn = new SqlConnection(GloablVars.databaseconnectionstring);
            SqlCommand cmd = new SqlCommand("InsertUserCredentials", Conn);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("code", code);
            cmd.Parameters.AddWithValue("pass", pass);

            await Conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
            Conn.Close();
        }
        public static async Task<string> CreateUniqueIdAsync()
        {
            string h;
            string j = "0";

            do
            {
                SqlConnection Conn = new SqlConnection(GloablVars.databaseconnectionstring);
                SqlCommand cmd = new SqlCommand("CheckUserCode", Conn);

                h = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 16).ToString();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("code", h);

                await Conn.OpenAsync();

                j = (await cmd.ExecuteScalarAsync()).ToString();
                Conn.Close();
            } while (j == "0" || Regex.IsMatch(h, "[^A-Za-z0-9]+"));

            return h;
        }
        public static async Task<string> CheckIfEmailExistsAsync(string email)
        {
            string j = "0";
            SqlConnection Conn = new SqlConnection(GloablVars.databaseconnectionstring);
            SqlCommand cmd = new SqlCommand("CheckEmail", Conn);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("email", email);

            await Conn.OpenAsync();

            j = (await cmd.ExecuteScalarAsync()).ToString();
            Conn.Close();

            return j;
        }

        private static string ShaHash(string value)
        {
            using(var hmac = new HMACSHA512(Encoding.UTF32.GetBytes(value)))
            {
                return ByteToString(hmac.ComputeHash(Encoding.UTF32.GetBytes(value)));
            }
        }
        static string ByteToString(IEnumerable<byte> data)
        {
            return string.Concat(data.Select(b => b.ToString("x2")));
        }
    }
}
