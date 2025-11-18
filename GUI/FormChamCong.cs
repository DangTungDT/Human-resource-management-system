using BLL;
using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class FormChamCong : Form
    {
        BLLNhanVien _bllNhanVien;
        BLLChamCong _bllChamCong;
        string _idNhanVien = "";
        int _idPhongBan = -1;
        bool _chamCongVe = false;
        public FormChamCong(string idNhanVien, int idPhongBan, bool chamCongVe, string conn)
        {
            _bllNhanVien = new BLLNhanVien(conn);
            _bllChamCong = new BLLChamCong(conn);
            _chamCongVe = chamCongVe;
            _idNhanVien = idNhanVien;
            _idPhongBan = idPhongBan;
            InitializeComponent();
        }
        private void FormChamCong_Load(object sender, EventArgs e)
        {
            //Thay đổi nội dung text theo chấm công vào hay ra
            if(_chamCongVe) this.Text = "Chấm công ra cho nhân viên";

            //Lấy dữ liệu nhân viên
            if (_chamCongVe)
            {
                var listNhanVien = _bllNhanVien.LayNhanVienChamCongVe(_idNhanVien, _idPhongBan);
                LoadNhanVienToDgv(listNhanVien);
            }
            else
            {
                var listNhanVien = _bllNhanVien.LayNhanVienQuanLy(_idNhanVien, _idPhongBan);
                LoadNhanVienToDgv(listNhanVien);
            }
        }
        public void LoadNhanVienToDgv(IQueryable<NhanVien> nhanViens)
        {
            // Nếu gọi từ background thread, marshal về UI thread
            if (InvokeRequired)
            {
                Invoke(new Action(() => LoadNhanVienToDgv(nhanViens)));
                return;
            }

            // Nếu null thì clear dgv và return
            if (nhanViens == null)
            {
                dgvNhanVien.DataSource = null;
                dgvNhanVien.Columns.Clear();
                return;
            }

            // Chuyển IQueryable sang danh sách DTO để dễ bind và để cột checkbox chỉnh sửa được.
            var list = nhanViens
                .Select(n => new NhanVienView
                {
                    Id = n.id, // lưu id ẩn
                    TenNhanVien = n.TenNhanVien,
                    NgaySinh = n.NgaySinh,
                    Email = n.Email,
                    ChamCong = true // mặc định checked
                })
                .ToList();

            var binding = new BindingList<NhanVienView>(list);

            // Thiết lập DataGridView
            dgvNhanVien.SuspendLayout();
            dgvNhanVien.AutoGenerateColumns = false;
            dgvNhanVien.AllowUserToAddRows = false;
            dgvNhanVien.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvNhanVien.MultiSelect = false;

            // Xóa mọi cột cũ
            dgvNhanVien.Columns.Clear();

            // Cột Tên nhân viên
            var colTen = new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(NhanVienView.TenNhanVien),
                HeaderText = "Tên nhân viên",
                Name = "colTenNhanVien",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            };
            dgvNhanVien.Columns.Add(colTen);

            // Cột Ngày sinh
            var colNgaySinh = new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(NhanVienView.NgaySinh),
                HeaderText = "Ngày sinh",
                Name = "colNgaySinh",
                ReadOnly = true,
                Width = 120,
                DefaultCellStyle = { Format = "dd/MM/yyyy" }
            };
            dgvNhanVien.Columns.Add(colNgaySinh);

            // Cột Email
            var colEmail = new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(NhanVienView.Email),
                HeaderText = "Email",
                Name = "colEmail",
                ReadOnly = true,
                Width = 220
            };
            dgvNhanVien.Columns.Add(colEmail);

            // Cột Chấm công - checkbox, có thể chỉnh sửa, mặc định true
            var colChamCong = new DataGridViewCheckBoxColumn
            {
                DataPropertyName = nameof(NhanVienView.ChamCong),
                HeaderText = "Chấm công",
                Name = "colChamCong",
                Width = 80,
                TrueValue = true,
                FalseValue = false
            };
            dgvNhanVien.Columns.Add(colChamCong);

            // Gán DataSource
            dgvNhanVien.DataSource = binding;

            dgvNhanVien.ResumeLayout();
        }

        // DTO nội bộ để binding (cho phép chỉnh sửa checkbox)
        // Thêm thuộc tính Id ẩn (không hiển thị trên DataGridView)
        private class NhanVienView
        {
            [Browsable(false)]
            public string Id { get; set; }
            public string TenNhanVien { get; set; }
            public DateTime NgaySinh { get; set; }
            public string Email { get; set; }
            public bool ChamCong { get; set; }
        }

        private void btnChamCong_Click(object sender, EventArgs e)
        {
            try
            {
                List<string> listNhanVien = GetCheckedNhanVienIdsFromDgv();
                if (_chamCongVe)
                {
                    if(listNhanVien.Count < 1)
                    {
                        MessageBox.Show("Không có nhân viên để chấm công ra!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    bool flag = _bllChamCong.CheckAttendanceOutArr(listNhanVien);

                    if (!flag)
                    {
                        //Đã chấm công vào cho tất cả
                        //Sử lý và cập nhật field GioRa cho ChamCong
                        foreach (string idStaff in listNhanVien)
                        {
                            if (idStaff == null) continue;
                            DTOChamCong dto = new DTOChamCong
                            {
                                NgayChamCong = DateTime.Now,
                                GioVao = DateTime.Now.TimeOfDay,
                                IdNhanVien = idStaff
                            };
                            dto.GioRa = DateTime.Now.TimeOfDay;
                            if (!_bllChamCong.UpdateGioRa(dto))
                            {
                                MessageBox.Show("Chấm công về thất bại!", "Thông báo");
                                return;
                            }
                        }
                        MessageBox.Show("Chấm công về cho tất cả thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Đã chấm công về cho tất cả rồi, vui lòng không chấm nữa!", "Thông báo");
                    }
                }
                else
                {
                    if (listNhanVien.Count < 1)
                    {
                        MessageBox.Show("Không có nhân viên để chấm công vào!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    foreach (string idStaff in listNhanVien)
                    {
                        //Bỏ qua id nào bị null
                        if (idStaff == null) continue;

                        //Tạo Và thêm chamCong
                        DTOChamCong dto = new DTOChamCong
                        {
                            NgayChamCong = DateTime.Now,
                            GioVao = DateTime.Now.TimeOfDay,
                            IdNhanVien = idStaff
                        };
                        string result = _bllChamCong.Add(dto).ToLower();

                        if (result == "invalid data")
                        {
                            //Dữ liệu sai
                            MessageBox.Show("Dữ liệu không hợp lệ!", "Thông báo");
                            return;
                        }
                        else if (result == "failed to add data")
                        {
                            //Thêm thất bại
                            MessageBox.Show("Chấm công thất bại!", "Thông báo");
                            return;
                        }
                    }
                    MessageBox.Show("Chấm công thành công cho tất cả!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Lỗi chấm công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Trả về danh sách id (List<string>) của những nhân viên có ChamCong == true trên dgvNhanVien.
        private List<string> GetCheckedNhanVienIdsFromDgv()
        {
            // Commit mọi thay đổi đang diễn ra (đảm bảo checkbox vừa click được áp dụng)
            dgvNhanVien.EndEdit();
            dgvNhanVien.CommitEdit(DataGridViewDataErrorContexts.Commit);

            var ids = new List<string>();

            // Thử lấy trực tiếp từ DataSource nếu là BindingList<NhanVienView>
            var binding = dgvNhanVien.DataSource as BindingList<NhanVienView>;
            if (binding != null)
            {
                ids = binding
                    .Where(x => x != null && x.ChamCong)
                    .Select(x => x.Id)
                    .Where(id => !string.IsNullOrWhiteSpace(id))
                    .ToList();
                return ids;
            }

            // Fallback: duyệt các hàng DataGridView
            foreach (DataGridViewRow row in dgvNhanVien.Rows)
            {
                if (row.IsNewRow) continue;

                // Nếu DataBoundItem là NhanVienView, tận dụng nó
                if (row.DataBoundItem is NhanVienView bound)
                {
                    if (bound.ChamCong && !string.IsNullOrWhiteSpace(bound.Id))
                        ids.Add(bound.Id);
                    continue;
                }

                // Nếu không có DataBoundItem, đọc giá trị ô checkbox trực tiếp
                bool isChecked = false;
                var chkCell = row.Cells["colChamCong"] as DataGridViewCheckBoxCell;
                if (chkCell != null)
                {
                    // cell.Value có thể là bool, string, hoặc null
                    var val = chkCell.Value;
                    if (val is bool b) isChecked = b;
                    else if (val != null) bool.TryParse(val.ToString(), out isChecked);
                }

                if (!isChecked) continue;

                // Cố gắng lấy id từ một cột ẩn tên "colId" nếu tồn tại
                string id = null;
                if (row.Cells["colId"] != null && row.Cells["colId"].Value != null)
                {
                    id = row.Cells["colId"].Value.ToString();
                }
                else
                {
                    // Nếu không có colId, thử lấy từ DataKeys-like cell (nếu bạn thêm id vào Tag, hoặc 1st hidden cell)
                    // Thử lấy từ Tag nếu bạn lưu object vào Tag khi tạo hàng
                    if (row.Tag is NhanVienView tagObj && !string.IsNullOrWhiteSpace(tagObj.Id))
                        id = tagObj.Id;
                }

                if (!string.IsNullOrWhiteSpace(id))
                    ids.Add(id);
            }

            return ids.Distinct().ToList();
        }
    }
}
