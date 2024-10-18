using BUS;
using DAL.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace De01
{
    public partial class Form1 : Form
    {
        private readonly LopService lopService = new LopService();
        private readonly StudentService studentService= new StudentService();
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            setGridViewStyle(dgvSinhVien);
            var listLops = lopService.GetAll();
            var listSinhviens = studentService.GetAll();
            FillLopCombobox(listLops);
            BindGrid(listSinhviens);
        }
        private void setGridViewStyle(DataGridView dgv)
        {
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.AllowUserToResizeRows = false;
            dgv.RowHeadersVisible = false;
        }

        private void FillLopCombobox(List<Lop> listLops)
        {
            listLops.Insert(0, new Lop());
            this.cboLop.DataSource = listLops;
            this.cboLop.DisplayMember = "TenLop";
            this.cboLop.ValueMember = "MaLop";
        }

        private void BindGrid(List<Sinhvien> listSinhviens)
        {
            dgvSinhVien.Rows.Clear();
            foreach (var item in listSinhviens)
            {
                int index = dgvSinhVien.Rows.Add();
                dgvSinhVien.Rows[index].Cells[0].Value = item.MaSV;
                dgvSinhVien.Rows[index].Cells[1].Value = item.HotenSV;
                if (item.Lop != null)
                    dgvSinhVien.Rows[index].Cells[2].Value = item.Lop.TenLop;
                dgvSinhVien.Rows[index].Cells[3].Value = item.NgaySinh + "";
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            Sinhvien sv = new Sinhvien();
            sv.MaSV = txtMaSV.Text;
            sv.HotenSV = txtHotenSV.Text;
            sv.NgaySinh = dtNgaysinh.Value;
            sv.MaLop = cboLop.SelectedValue.ToString();
            studentService.AddOrUpdate(sv);
            BindGrid(studentService.GetAll());
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string maSV = txtMaSV.Text;
                studentService.Delete(maSV);
                BindGrid(studentService.GetAll());
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            Sinhvien sv = new Sinhvien();
            sv.MaSV = txtMaSV.Text;
            sv.HotenSV = txtHotenSV.Text;
            sv.NgaySinh = dtNgaysinh.Value;
            sv.MaLop = cboLop.SelectedValue.ToString();

            studentService.AddOrUpdate(sv);
            BindGrid(studentService.GetAll());
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn thoát?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            string keyword = txtTim.Text;
            if (keyword != "")
            {
                var listSinhviens = studentService.Search(keyword);
                BindGrid(listSinhviens);
            }
            else
            {
                var listSinhviens = studentService.GetAll();
                BindGrid(listSinhviens);
            }
        }
    }
}
