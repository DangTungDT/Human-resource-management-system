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
        int idChucVu = 0;
        DTOChucVu _oldPosition;
        public  ucChucVu(string conn)
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
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) || e.KeyChar == ' ')
            {
                e.Handled = true;
            }
        }

        private void ucChucVu_Load(object sender, EventArgs e)
        {
            var listPhongBan = bllPhongBan.GetAllPhongBan();
            cbPhongBan.DataSource = listPhongBan;
            cbPhongBan.DisplayMember = "Tên phòng ban";
            cbPhongBan.ValueMember = "Mã phòng ban";

            cmbFindDepartment.DataSource = listPhongBan;
            cmbFindDepartment.DisplayMember = "Tên phòng ban";
            cmbFindDepartment.ValueMember = "Mã phòng ban";

            dgvChucVu.DataSource = bllChucVu.GetAll();

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if(cbPhongBan.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn phòng ban để thêm!", "Thông báo");
                return;
            }

            if(!bllChucVu.CheckPosition(txtTenChucVu.Text, int.Parse(cbPhongBan.SelectedValue.ToString())))
            {
                DialogResult check = MessageBox.Show("Phòng ban hiện đã có chức vụ này bạn có chắc muốn thêm tiếp?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if(check == DialogResult.No)
                {
                    return;
                }
            }

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
                ResetValueInput();
                dgvChucVu.DataSource = bllChucVu.GetAll();
            }
            else
            {
                MessageBox.Show("Thêm thất bại!", "Thông báo");
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if(idChucVu != 0)
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

                if(_oldPosition.Id == dto.Id &&
                    _oldPosition.TenChucVu == dto.TenChucVu &&
                    _oldPosition.LuongCoBan == dto.LuongCoBan &&
                    _oldPosition.TyLeHoaHong == dto.TyLeHoaHong &&
                    _oldPosition.MoTa == dto.MoTa &&
                    _oldPosition.IdPhongBan == dto.IdPhongBan)
                {
                    MessageBox.Show("Hiện không thấy thông tin mới, vui lòng cập nhật thông tin trước!", "Thông báo");
                    return;
                }

                if (bllChucVu.Update(dto))
                {
                    MessageBox.Show("Cập nhật thành công!", "Thông báo");
                    ResetValueInput();
                    dgvChucVu.DataSource = bllChucVu.GetAll();
                }
                else
                {
                    MessageBox.Show("Cập nhật thất bại!", "Thông báo");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn đối tượng để xóa!", "Thông báo");
            }

        }

        private void ResetValueInput()
        {
            txtTenChucVu.Text = "";
            txtLuongCoBan.Text = "";
            txtTyLeHoaHong.Text = "";
            cbPhongBan.SelectedValue = 1;
            txtMoTa.Text = "";
            idChucVu = 0;
        }

        private void dgvChucVu_Click(object sender, EventArgs e)
        {
        }

        private void dgvChucVu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvChucVu.Rows[e.RowIndex].Cells[1].Value != null &&
                dgvChucVu.Rows[e.RowIndex].Cells[2].Value.ToString() != "" &&
                dgvChucVu.Rows[e.RowIndex].Cells.Count > 0
                )
            {
                DataGridViewRow row = dgvChucVu.Rows[e.RowIndex];
                int dong = dgvChucVu.CurrentCell.RowIndex;
                idChucVu = int.Parse(row.Cells["Mã chức vụ"].Value.ToString());
                txtTenChucVu.Text = row.Cells["Tên chức vụ"].Value.ToString();
                txtLuongCoBan.Text = row.Cells["Lương cơ bản"].Value.ToString();
                txtTyLeHoaHong.Text = row.Cells["Tỷ lệ hoa hồng"].Value.ToString();
                txtMoTa.Text = row.Cells["Mô tả"].Value?.ToString();
                cbPhongBan.SelectedValue = row.Cells["Phòng ban"].Value.ToString();


                _oldPosition = new DTOChucVu()
                {
                    Id = int.Parse(row.Cells["Mã chức vụ"].Value.ToString()),
                    TenChucVu = txtTenChucVu.Text,
                    LuongCoBan = decimal.Parse(txtLuongCoBan.Text),
                    TyLeHoaHong = decimal.Parse(txtTyLeHoaHong.Text),
                    MoTa = txtMoTa.Text,
                    IdPhongBan = Convert.ToInt32(cbPhongBan.SelectedValue),
                };
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult check = MessageBox.Show("Bạn có muốn xóa?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (check == DialogResult.No)
            {
                return;
            }
            if (idChucVu > 0)
            {
                bllChucVu.Delete(idChucVu);
                MessageBox.Show("Xóa thành công!", "Thông báo");
                ResetValueInput();
                dgvChucVu.DataSource = bllChucVu.GetAll();
            }
            else if(idChucVu == 0)
            {
                MessageBox.Show("Vui lòng chọn đối tượng để xóa!", "Thông báo");
            }
            else
            {
                MessageBox.Show("Xóa thất bại!", "Thông báo");
            }
        }

        private void btnResetDGV_Click(object sender, EventArgs e)
        {
            dgvChucVu.DataSource = bllChucVu.GetAll();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {

            if(cmbFindDepartment.SelectedValue != null)
            {
                dgvChucVu.DataSource = bllChucVu.GetPositionByDepartment(int.Parse(cmbFindDepartment.SelectedValue.ToString()));
            }
            else
            {
                dgvChucVu.DataSource = bllChucVu.GetAll();
            }
        }

        private void txtTyLeHoaHong_TextChanged(object sender, EventArgs e)
        {
            if(txtTyLeHoaHong.Text != "" &&int.Parse(txtTyLeHoaHong.Text) >= 100)
            {
                txtTyLeHoaHong.Text = "100";
            }
        }

        private void txtTenChucVu_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Cho phép phím điều khiển như Backspace, Delete, Left, Right
            if (char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            // Chặn số
            else if (char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
            // Chặn ký tự đặc biệt (chỉ cho phép chữ cái và khoảng trắng)
            else if (!char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void dgvChucVu_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
