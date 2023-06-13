using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using MVCSample.Models;
using System.Text;
using System.Data.SqlClient;
using Dapper;

namespace MVCSample.DAL
{
    public class PenggunaDAL : IPengguna
    {
        private IConfiguration _config;

        public PenggunaDAL(IConfiguration config)
        {
            _config = config;
        }

        public Pengguna cekLogin(string Username, string Password)
        {
            using(SqlConnection Con = new SqlConnection(getConStr()))
            {
                var strSql = $"select * from Pengguna where username=@username and password=@password";
                var param = new {username=Username, password=getMD5(Password)};
                    var data = Con.QuerySingleOrDefault<Pengguna>(strSql,param);
                    if (data != null )
                    {
                        return data;
                    }
                    else
                    {
                        throw new Exception($"Username atau password salah");
                    }
            }
        }

        public string getConStr()
        {
            return _config.GetConnectionString("DefaultConnection");
        }

        public string getMD5(string input)
        {
            using(var md5 = MD5.Create())
            {
                var result = md5.ComputeHash(Encoding.ASCII.GetBytes(input));
                return Encoding.ASCII.GetString(result);
            }
        }
        public void Register(Pengguna pengguna)
        {
            using(SqlConnection Con = new SqlConnection(getConStr()))
            {
                var strSql = @"insert into Pengguna values (@Username,@Password,@Aturan,@Nama,@Telpon)";
                var param = new {
                            Username = pengguna.Username,
                            Password = getMD5(pengguna.Password),
                            Aturan = pengguna.Aturan,
                            Nama = pengguna.Nama,
                            Telpon = pengguna.Telpon
                            };

                try
                {
                    Con.Query(strSql,param);
                }
                catch(SqlException ex)
                {
                    throw new Exception($"Error : {ex.Message}");
                }
            }
        }
    }
}