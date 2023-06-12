using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVCSample.Models;

namespace MVCSample.DAL
{
    public interface IMahasiswa
    {
        public IEnumerable<Mahasiswa> getAll();
        public IEnumerable<Mahasiswa> getByNIM(string id);
        public IEnumerable<Mahasiswa> getByNama(string nama);
        public Mahasiswa getById(string id);
        public void Insert(Mahasiswa mhs);
        public void Update(Mahasiswa mhs);
        public void Delete(string nim);
        
        
    }
}