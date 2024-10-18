using DAL.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class LopService
    {
        Model1 model = new Model1();

        public List<Lop> GetAll()
        {
                return model.Lops.ToList();
        }
        public Lop GetLopByMaLop(string maLop)
        {
                return model.Lops.FirstOrDefault(l => l.MaLop == maLop);
        }
        public bool ThemLop(Lop lop)
        {
                model.Lops.Add(lop);
                return model.SaveChanges() > 0;
        }
        public bool SuaLop(Lop lop)
        {
                var existingLop = model.Lops.FirstOrDefault(l => l.MaLop == lop.MaLop);
                if (existingLop != null)
                {
                    existingLop.TenLop = lop.TenLop;
                    return model.SaveChanges() > 0;
                }
                return false;
        }
        public bool XoaLop(Lop lop)
        {
                var existingLop = model.Lops.FirstOrDefault(l => l.MaLop == lop.MaLop);
                if (existingLop != null)
                {
                    model.Lops.Remove(existingLop);
                    return model.SaveChanges() > 0;
                }
                return false;
        }
    }
}
