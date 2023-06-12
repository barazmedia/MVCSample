using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVCSample.Models;
using System.Data.SqlClient;
using Dapper;

namespace MVCSample.DAL
{
    public class MahasiswaDAL : IMahasiswa
    {
        private IConfiguration _config;
        public MahasiswaDAL(IConfiguration config)
        {
            _config = config;
        }
        public string getStrCon()
        {
            return _config.GetConnectionString("DefaultConnection");
        }
        public IEnumerable<Mahasiswa> getAll()
        {
            using(SqlConnection Con = new SqlConnection(getStrCon()))
            {
                var strSql = "Select * from mahasiswa order by nama";
                return Con.Query<Mahasiswa>(strSql);
            }
        }

        public Mahasiswa getById(string id)
        {
            using(SqlConnection Con = new SqlConnection(getStrCon()))
            {
                var strSql = $"select * from mahasiswa where nim = @Nim";
                var param = new {Nim = id};
                try
                {
                   return Con.QueryFirstOrDefault<Mahasiswa>(strSql,param);
                }
                catch(SqlException ex)
                {
                    throw new Exception($"Error : {ex.Message}");
                }
            }
        }

        public IEnumerable<Mahasiswa> getByNIM(string id)
        {
            using(SqlConnection Con = new SqlConnection(getStrCon()))
            {
                var strSql = @"select * from mahasiswa where nim = @Nim";
                var param = new { Nim = id};
                return Con.Query<Mahasiswa>(strSql,param);
            }
        }

        public IEnumerable<Mahasiswa> getByNama(string nama)
        {
            using(SqlConnection Con = new SqlConnection(getStrCon()))
            {
                var strSql = @"select * from mahasiswa where nama like @Nama";
                var param = new { Nama = "%"+nama+"%"};
                return Con.Query<Mahasiswa>(strSql,param);
            }
        }

        public void Insert(Mahasiswa mhs)
        {
            using(SqlConnection Con = new SqlConnection(getStrCon()))
            {
                var strSql = @"insert into mahasiswa (nim,nama,alamat,email,telpon) values (@Nim,@Nama,@Alamat,@Email,@Telpon)";
                var param = new {Nim = mhs.Nim, Nama = mhs.Nama, Alamat = mhs.Alamat, Email = mhs.Email, Telpon = mhs.Telpon};
                try
                {
                    Con.Execute(strSql,param);
                }
                catch(SqlException ex)
                {
                    throw new Exception($"Error, {ex.Message}");
                }
            }
        }

        public void Update(Mahasiswa mhs)
        {
            using(SqlConnection Con = new SqlConnection(getStrCon()))
            {
                var strSql = $"update mahasiswa set nama=@Nama,alamat=@Alamat,email=@Email,telpon=@Telpon where nim=@Nim";
                var param = new {Nim=mhs.Nim, Nama=mhs.Nama,Alamat=mhs.Alamat,Email=mhs.Email,Telpon=mhs.Telpon};
                try
                {
                    Con.Execute(strSql,param);
                }
                catch(Exception ex)
                {
                    throw new Exception($"Error : {ex.Message}");
                }
            }
        }

        public void Delete(string nim)
        {
             using(SqlConnection Con = new SqlConnection(getStrCon()))
            {
                var strSql = $"delete from mahasiswa where nim=@Nim";
                var param = new {Nim=nim};
                try
                {
                    Con.Execute(strSql,param);
                }
                catch(Exception ex)
                {
                    throw new Exception($"Error : {ex.Message}");
                }
            }
        }
    }
}