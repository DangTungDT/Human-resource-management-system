using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using DTO;

namespace GUI
{
    public partial class ucChucVu : UserControl
    {
        BLLPhongBan bllPhongBan;
        BLLChucVu bllChucVu;
        int idChucVu;
        public ucChucVu(string conn)
        {
            bllPhongBan = new BLLPhongBan(conn);
            bllChucVu = new BLLChucVu(conn);
            InitializeComponent();
        }
        public ucChucVu()
        {
            InitializeComponent();
        }

        private void txtLuongCoBan_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtTyLeHoaHong_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void ucChucVu_Load(object sender, EventArgs e)
        {
            cbPhongBan.DataSource = bllPhongBan.GetAllPhongBan();
            cbPhongBan.DisplayMember = "Tên phòng ban";
            cbPhongBan.ValueMember = "Mã phòng ban";

            dgvChucVu.DataSource = bllChucVu.GetAll();

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            DTOChucVu dto = new DTOChucVu()
            {
                TenChucVu = txtTenChucVu.Text,
                LuongCoBan = decimal.Parse(txtLuongCoBan.Text),
                TyLeHoaHong = decimal.Parse(txtTyLeHoaHong.Text),
                MoTa = txtMoTa.Text,
                IdPhongBan = Convert.ToInt32(cbPhongBan.SelectedValue),
            };
            if(bllChucVu.Insert(dto))
            {
                MessageBox.Show("Thêm thành công!", "Thông báo");
                dgvChucVu.DataSource = bllChucVu.GetAll();
            }
            else
            {
                MessageBox.Show("Thêm thất bại!", "Thông báo");
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            DTOChucVu dto = new DTOChucVu()
            {
                Id = idChucVu,
                TenChucVu = txtTenChucVu.Text,
                LuongCoBan = decimal.Parse(txtLuongCoBan.Text),
                TyLeHoaHong = decimal.Parse(txtTyLeHoaHong.Text),
                MoTa = txtMoTa.Text,
                IdPhongBan = Convert.ToInt32(cbPhongBan.SelectedValue),
            };
            if (bllChucVu.Update(dto))
            {
                MessageBox.Show("Cập nhật thành công!", "Thông báo");
                dgvChucVu.DataSource = bllChucVu.GetAll();
            }
            else
            {
                MessageBox.Show("Cập nhật thất bại!", "Thông báo");
            }
        }

        private void dgvChucVu_Click(object sender, EventArgs e)
        {
        }

        private void dgvChucVu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvChucVu.Rows[e.RowIndex];
                int dong = dgvChucVu.CurrentCell.RowIndex;
                idChucVu = int.Parse(row.Cells["Mã chức vụ"].Value.ToString());
                txtTenChucVu.Text = row.Cells["Tên chức vụ"].Value.ToString();
                txtLuongCoBan.Text = row.Cells["Lương cơ bản"].Value.ToString();
                txtTyLeHoaHong.Text = row.Cells["Tỷ lệ hoa hồng"].Value.ToString();
                txtMoTa.Text = row.Cells["Mô tả"].Value?.ToString();
                cbPhongBan.SelectedValue = row.Cells["Phòng ban"].Value.ToString();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if(idChucVu > 0)
            {
                bllChucVu.Delete(idChucVu);
                MessageBox.Show("Xóa thành công!", "Thông báo");
                dgvChucVu.DataSource = bllChucVu.GetAll();
            }
            else
            {
                MessageBox.Show("Xóa thành công!", "Thông báo");
            }
        }
    }
}
