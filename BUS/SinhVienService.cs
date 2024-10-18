using DAL.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
        public class StudentService
        {
            Model1 model = new Model1();
            public List<Sinhvien> GetAll()
            {
                return model.Sinhviens.ToList();
            }
            public Sinhvien GetStudentByMaSV(string maSV)
            {
                return model.Sinhviens.FirstOrDefault(s => s.MaSV == maSV);
            }
        public void AddOrUpdate(Sinhvien sv)
        {
            model.Sinhviens.AddOrUpdate(sv);
            model.SaveChanges();
        }
        public void Delete(string maSV)
        {
            var sv = model.Sinhviens.FirstOrDefault(p => p.MaSV == maSV);
            if (sv != null)
            {
                model.Sinhviens.Remove(sv);
                model.SaveChanges();
            }
        }
        public List<Sinhvien> Search(string keyword)
        {
            return model.Sinhviens.Where(p => p.MaSV.Contains(keyword) || p.HotenSV.Contains(keyword)).ToList();
        }
    }
}
