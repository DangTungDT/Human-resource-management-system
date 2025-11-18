using BLL;
using DocumentFormat.OpenXml.Office2010.Excel;
using DTO;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace GUI
{
    public partial class ucPhongBan : UserControl
    {
        //Biến toàn cục
        private readonly BLLPhongBan bllPhongBan;
        private DataTable dtPhongBan;
        private int? selectedId = null;
        private DTOPhongBan _oldPhongBan = null;
        //Constructor
        public ucPhongBan(string conn)
        {
            InitializeComponent();

            // Initialize BLL with connection string
            bllPhongBan = new BLLPhongBan(conn);            

            // initial state (in case controls exist)
            if (btnUpdate != null) btnUpdate.Enabled = false;
            if (btnDelete != null) btnDelete.Enabled = false;
        }

        
        private void ucPhongBan_Load(object sender, EventArgs e)
        {
            LoadPhongBan();
            ClearForm();
        }

        private void LoadPhongBan()
        {
            try
            {
                dtPhongBan = bllPhongBan.GetAllPhongBan();
                if (dtPhongBan == null)
                {
                    if (dgvPhongBan != null) dgvPhongBan.DataSource = null;
                    return;
                }

                // bind DataTable
                if (dgvPhongBan != null)
                {
                    dgvPhongBan.DataSource = dtPhongBan;

                    // add delete image column if not present
                    if (!dgvPhongBan.Columns.Contains("Xóa"))
                    {
                        var colDelete = new DataGridViewImageColumn()
                        {
                            Name = "Xóa",
                            HeaderText = "Xóa",
                            ImageLayout = DataGridViewImageCellLayout.Zoom,
                            Width = 50
                        };
                        colDelete.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                        // try load resource image if available
                        try
                        {
                            var img = Properties.Resources.delete;
                            if (img != null) colDelete.Image = img;

                        }
                        catch
                        {
                            // ignore if resources not available
                        }
                        
                        dgvPhongBan.Columns.Add(colDelete);
                        dgvPhongBan.Columns["Xóa"].DisplayIndex = dgvPhongBan.Columns.Count - 1;
                        dgvPhongBan.AllowUserToAddRows = false;
                    }

                    // hide id column if exists (common name "Mã phòng ban")
                    if (dgvPhongBan.Columns.Contains("Mã phòng ban"))
                    {
                        dgvPhongBan.Columns["Mã phòng ban"].Visible = false;
                    }
                    else
                    {
                        // try common id column names
                        foreach (var idName in new[] { "id", "Id", "ID", "Ma", "Mã" })
                        {
                            if (dgvPhongBan.Columns.Contains(idName))
                            {
                                dgvPhongBan.Columns[idName].Visible = false;
                                break;
                            }
                        }
                    }

                    dgvPhongBan.ClearSelection();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu phòng ban: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TxtFindName_TextChanged(object sender, EventArgs e)
        {
            FilterPhongBan();
        }


        //Hàm tìm kiếm phòng ban 
        private void FilterPhongBan()
        {
            if (dtPhongBan == null) return;

            string filter = "1=1";
            if (!string.IsNullOrWhiteSpace(txtFindName.Text))
                filter += $" AND [Tên phòng ban] LIKE '%{txtFindName.Text.Replace("'", "''")}%'";
            // Designer doesn't have txtSearchMoTa; only txtFindName per provided Designer, so we filter by name only

            dtPhongBan.DefaultView.RowFilter = filter;
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            // Keep an explicit update button if designer has it; this mirrors BtnSave update behavior
            try
            {
                if (selectedId == null)
                {
                    MessageBox.Show("Vui lòng chọn phòng ban cần cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtTenPhongBan.Text) || string.IsNullOrWhiteSpace(txtMota.Text))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin phòng ban trước khi cập nhật!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DTOPhongBan pb = new DTOPhongBan
                {
                    Id = selectedId.Value,
                    TenPhongBan = txtTenPhongBan.Text.Trim(),
                    MoTa = txtMota.Text.Trim()
                };
                if(pb.Id == _oldPhongBan.Id && pb.TenPhongBan == _oldPhongBan.TenPhongBan && pb.MoTa == _oldPhongBan.MoTa)
                {
                    MessageBox.Show("Thông tin chưa được thay đổi nên không thể cập nhật, vui lòng thay đổi thông tin trước khi cập nhật!","Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                string updateResult = bllPhongBan.SavePhongBan(pb, isNew: false);
                if(updateResult == "Mô tả không được dài quá 255 ký tự" || 
                    updateResult == "Tên và mô tả phòng ban không được dài quá 255 ký tự!" ||
                    updateResult == "Tên phòng ban không được dài quá 255 ký tự")
                {
                    MessageBox.Show(updateResult, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (updateResult == "Cập nhật thông tin phòng ban thành công!")
                {
                    MessageBox.Show(updateResult, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadPhongBan();
                    ClearForm();
                }
                else if (updateResult == "Cập nhật phòng ban thất bại!")
                {
                    MessageBox.Show(updateResult, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show(updateResult, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật phòng ban: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenPhongBan.Text) || string.IsNullOrWhiteSpace(txtMota.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin phòng ban trước khi thêm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DTOPhongBan pb = new DTOPhongBan
            {
                Id = selectedId ?? 0,
                TenPhongBan = txtTenPhongBan.Text.Trim(),
                MoTa = txtMota.Text.Trim()
            };
            string addResult = bllPhongBan.SavePhongBan(pb, isNew: true);
            if (addResult == "Mô tả không được dài quá 255 ký tự" ||
                    addResult == "Tên và mô tả phòng ban không được dài quá 255 ký tự!" ||
                    addResult == "Tên phòng ban không được dài quá 255 ký tự")
            {
                MessageBox.Show(addResult, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (addResult == "Thêm phòng ban thành công!")
            {
                MessageBox.Show(addResult, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadPhongBan();
                ClearForm();
            }
            else if(addResult == "Thêm phòng ban thất bại!")
            {
                MessageBox.Show(addResult, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }else
            {
                MessageBox.Show(addResult, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedId == null || _oldPhongBan == null)
                {
                    MessageBox.Show("Vui lòng chọn phòng ban cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (MessageBox.Show($"Bạn có chắc muốn xóa phòng ban {_oldPhongBan.TenPhongBan}?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string result = bllPhongBan.DeletePhongBan(selectedId.Value);
                    if (result == "Xóa phòng ban thành công!")
                    {
                        MessageBox.Show($"Xóa đã xóa thành công phòng ban {_oldPhongBan.TenPhongBan}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadPhongBan();
                        ClearForm();
                    }
                    else
                    {
                        MessageBox.Show(result, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa phòng ban: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnFind_Click(object sender, EventArgs e)
        {
            FilterPhongBan();
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            txtFindName.Clear();
            FilterPhongBan();
        }

        private void Dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var grid = dgvPhongBan;

            try
            {

                // Otherwise: select row for edit
                object idObj = null;
                if (grid.Columns.Contains("Mã phòng ban"))
                    idObj = grid.Rows[e.RowIndex].Cells["Mã phòng ban"].Value;
                else if (grid.Columns.Contains("id"))
                    idObj = grid.Rows[e.RowIndex].Cells["id"].Value;
                else
                {
                    // fallback: find first numeric cell
                    foreach (DataGridViewCell cell in grid.Rows[e.RowIndex].Cells)
                    {
                        if (cell.Value != null && int.TryParse(cell.Value.ToString(), out _))
                        {
                            idObj = cell.Value;
                            break;
                        }
                    }
                }
                //Lưu dữ liệu đang chọn để so sánh sự thay đổi
                _oldPhongBan = new DTOPhongBan();
                if (idObj != null && int.TryParse(idObj.ToString(), out int idVal))
                {
                    selectedId = idVal;

                    //Lưu dữ liệu id để so sánh
                    _oldPhongBan.Id = idVal;
                }
                else selectedId = null;

                // Fill form fields from known column names; fallback by name detection
                if (grid.Columns.Contains("Tên phòng ban"))
                    txtTenPhongBan.Text = grid.Rows[e.RowIndex].Cells["Tên phòng ban"].Value?.ToString() ?? "";
                else
                {
                    var col = grid.Columns.Cast<DataGridViewColumn>().FirstOrDefault(c => c.Name.IndexOf("ten", StringComparison.OrdinalIgnoreCase) >= 0);
                    if (col != null)
                        txtTenPhongBan.Text = grid.Rows[e.RowIndex].Cells[col.Name].Value?.ToString() ?? "";
                }

                if (grid.Columns.Contains("Mô tả"))
                    txtMota.Text = grid.Rows[e.RowIndex].Cells["Mô tả"].Value?.ToString() ?? "";
                else
                {
                    var col = grid.Columns.Cast<DataGridViewColumn>().FirstOrDefault(c => c.Name.IndexOf("mo", StringComparison.OrdinalIgnoreCase) >= 0 || c.Name.IndexOf("mota", StringComparison.OrdinalIgnoreCase) >= 0);
                    if (col != null)
                        txtMota.Text = grid.Rows[e.RowIndex].Cells[col.Name].Value?.ToString() ?? "";
                }

                //Lưu dữ liệu tên phòng ban và mô tả để so sánh
                _oldPhongBan.TenPhongBan = txtTenPhongBan.Text;
                _oldPhongBan.MoTa = txtMota.Text;
                
                if (btnUpdate != null) btnUpdate.Enabled = true;
                if (btnDelete != null) btnDelete.Enabled = true;

                // If clicked delete column
                if (grid.Columns[e.ColumnIndex].Name == "Xóa")
                {
                    // try get id using known column names, fallback to first numeric cell
                    int id = -1;
                    if (grid.Columns.Contains("Mã phòng ban") && grid.Rows[e.RowIndex].Cells["Mã phòng ban"].Value != null)
                    {
                        int.TryParse(grid.Rows[e.RowIndex].Cells["Mã phòng ban"].Value.ToString(), out id);
                    }
                    else if (grid.Columns.Contains("id") && grid.Rows[e.RowIndex].Cells["id"].Value != null)
                    {
                        int.TryParse(grid.Rows[e.RowIndex].Cells["id"].Value.ToString(), out id);
                    }
                    else
                    {
                        // fallback: find first numeric cell in the row (excluding image column)
                        foreach (DataGridViewCell cell in grid.Rows[e.RowIndex].Cells)
                        {
                            if (cell.OwningColumn.Name == "Xóa") continue;
                            if (cell.Value != null && int.TryParse(cell.Value.ToString(), out int tmp))
                            {
                                id = tmp;
                                break;
                            }
                        }
                    }

                    if (id > 0)
                    {
                        if (MessageBox.Show($"Bạn có chắc muốn xóa phòng ban {_oldPhongBan.TenPhongBan}?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            string result = bllPhongBan.DeletePhongBan(id);
                            if (result == "Xóa phòng ban thành công!")
                            {
                                MessageBox.Show($"Xóa đã xóa thành công phòng ban {_oldPhongBan.TenPhongBan}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadPhongBan();
                                ClearForm();
                            }
                            else
                            {
                                MessageBox.Show(result, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    return;
                }
            }
            catch (Exception ex)
            {
                // do not crash UI; log minimal info
                Console.WriteLine("dgvPhongBan_CellClick error: " + ex.Message);
            }
        }

        private void ClearForm()
        {
            selectedId = null;
            if (txtTenPhongBan != null) txtTenPhongBan.Clear();
            if (txtMota != null) txtMota.Clear();


            if (dgvPhongBan != null) dgvPhongBan.ClearSelection();

            if (btnUpdate != null) btnUpdate.Enabled = false;
            if (btnDelete != null) btnDelete.Enabled = false;
            _oldPhongBan = null;
        }

        private void grbCRUD_Click(object sender, EventArgs e)
        {

        }
    }
}