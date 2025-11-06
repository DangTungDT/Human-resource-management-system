using BLL;
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
        // BLL + data
        private readonly BLLPhongBan bllPhongBan;
        private DataTable dtPhongBan;
        private int? selectedId = null;

        public ucPhongBan(string conn)
        {
            InitializeComponent();

            // Initialize BLL with connection string
            bllPhongBan = new BLLPhongBan(conn);


            // Filter on typing in the left search box (txtFindName)
            if (txtFindName != null)
            {
                txtFindName.TextChanged -= TxtFindName_TextChanged;
                txtFindName.TextChanged += TxtFindName_TextChanged;
            }

            // initial state (in case controls exist)
            if (btnUpdate != null) btnUpdate.Enabled = false;
            if (btnDelete != null) btnDelete.Enabled = false;
        }

        // -------------------- Load & filter --------------------
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

        private void FilterPhongBan()
        {
            if (dtPhongBan == null) return;

            string filter = "1=1";
            if (!string.IsNullOrWhiteSpace(txtFindName.Text))
                filter += $" AND [Tên phòng ban] LIKE '%{txtFindName.Text.Replace("'", "''")}%'";
            // Designer doesn't have txtSearchMoTa; only txtFindName per provided Designer, so we filter by name only

            dtPhongBan.DefaultView.RowFilter = filter;
        }

        // -------------------- CRUD --------------------
        // btnAdd is used as Save/Add; selectedId determines add vs update
        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenPhongBan.Text))
            {
                MessageBox.Show("Vui lòng nhập tên phòng ban!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var pb = new DTOPhongBan
            {
                // DTO must define Id / TenPhongBan / MoTa
                Id = selectedId ?? 0,
                TenPhongBan = txtTenPhongBan.Text.Trim(),
                MoTa = txtMota.Text.Trim()
            };

            try
            {
                if (selectedId == null)
                {
                    // thêm mới
                    bllPhongBan.SavePhongBan(pb, isNew: true);
                    MessageBox.Show("✅ Đã thêm phòng ban mới!");
                }
                else
                {
                    // cập nhật
                    bllPhongBan.SavePhongBan(pb, isNew: false);
                    MessageBox.Show("✏️ Đã cập nhật phòng ban!");
                }

                LoadPhongBan();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu phòng ban: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

                if (string.IsNullOrWhiteSpace(txtTenPhongBan.Text))
                {
                    MessageBox.Show("Vui lòng nhập tên phòng ban!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var pb = new DTOPhongBan
                {
                    Id = selectedId.Value,
                    TenPhongBan = txtTenPhongBan.Text.Trim(),
                    MoTa = txtMota.Text.Trim()
                };

                bllPhongBan.SavePhongBan(pb, isNew: false);
                MessageBox.Show("Đã cập nhật phòng ban!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadPhongBan();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật phòng ban: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenPhongBan.Text))
            {
                MessageBox.Show("Vui lòng nhập tên phòng ban!", "Thông báo");
                return;
            }

            DTOPhongBan pb = new DTOPhongBan
            {
                Id = selectedId ?? 0,
                TenPhongBan = txtTenPhongBan.Text.Trim(),
                MoTa = txtMota.Text.Trim()
            };
            bllPhongBan.SavePhongBan(pb, isNew: true);
            MessageBox.Show("✅ Đã thêm phòng ban mới!");
        }
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedId == null)
                {
                    MessageBox.Show("Vui lòng chọn phòng ban cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (MessageBox.Show("Bạn có chắc muốn xóa phòng ban này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (bllPhongBan.DeletePhongBan(selectedId.Value))
                    {
                        MessageBox.Show("Đã xóa phòng ban.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadPhongBan();
                        ClearForm();
                    }
                    else
                    {
                        MessageBox.Show("Xóa không thành công.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (txtFindName != null) txtFindName.Clear();
            FilterPhongBan();
        }

        private void BtnUndo_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn hoàn tác dữ liệu đang nhập?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                ClearForm();
        }

        private void Dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var grid = dgvPhongBan;

            try
            {
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
                        if (MessageBox.Show("Bạn có chắc muốn xóa phòng ban này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            if (bllPhongBan.DeletePhongBan(id))
                            {
                                LoadPhongBan();
                                ClearForm();
                            }
                            else
                            {
                                MessageBox.Show("Xóa không thành công.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    return;
                }

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

                if (idObj != null && int.TryParse(idObj.ToString(), out int idVal))
                    selectedId = idVal;
                else
                    selectedId = null;

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


                if (btnUpdate != null) btnUpdate.Enabled = true;
                if (btnDelete != null) btnDelete.Enabled = true;
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

            if (btnAdd != null)
            {
                btnAdd.Text = "Tạo phòng ban";
                btnAdd.FillColor = Color.LightGray;
            }

            if (dgvPhongBan != null) dgvPhongBan.ClearSelection();

            if (btnUpdate != null) btnUpdate.Enabled = false;
            if (btnDelete != null) btnDelete.Enabled = false;
        }
    }
}