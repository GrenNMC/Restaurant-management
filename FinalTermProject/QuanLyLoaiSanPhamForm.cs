using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace FinalTermProject
{
    public partial class QuanLyLoaiSanPhamForm : Form
    {
        Context context;
        private int isInserting;

        public QuanLyLoaiSanPhamForm()
        {
            InitializeComponent();
            context = new Context();
        }

        public QuanLyLoaiSanPhamForm(Context context)
        {
            InitializeComponent();
            this.context = context;
        }

        private void LoadData()
        {
            //Cho người dùng thao tác lên datagridview thi bắt đầu load dữ liệu
            dgvLoaiSP.Enabled = true;
            //Không cho phép người dùng thao tác lên các nút Lưu/Huỷ bỏ và các textbox
            groupBox1.Enabled = false;
            btnLuu.Enabled = false;
            btnHuyBo.Enabled = false;
            //Cho phép người dùng thao tác lên các nút Thêm/Xoá/Sửa/Reset/Trở về
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            btnReload.Enabled = true;
            btnTroVe.Enabled = true;
            //Reset lại các textbox
            tbCategoryID.ResetText();
            tbCategoryName.ResetText();
            tbDescription.ResetText();

            try
            {
                //Tạo câu truy vấn để lấy dữ liệu
                var query = from categories in context.Categories
                            select new
                            {
                                categories.CategoryID,
                                categories.CategoryName,
                                categories.Description,
                            };
                //Đem dữ liệu vừa truy vấn lên datadridview
                dgvLoaiSP.DataSource = query.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Không lấy được dữ liệu từ bảng Categories");
            }
        }

        private void QuanLyLoaiSanPhamForm_Load(object sender, EventArgs e)
        {
            //Load dữ liệu
            LoadData();
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            //Load dữ liệu
            LoadData();
        }

        private void btnTroVe_Click(object sender, EventArgs e)
        {
            //kết thúc
            this.Close();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            // Khi đã bấm nút thêm thì không cho phép người dùng thao tác trên DataGridView
            dgvLoaiSP.Enabled = false;

            // Kích hoạt biến isInserting
            isInserting = 1;

            // Xóa nội dung trong các textbox
            tbCategoryID.ResetText();
            tbCategoryName.ResetText();
            tbDescription.ResetText();

            // Cho phép người dùng thao tác với nút Lưu/Hủy Bỏ và cho phép nhập thông tin vào các textbox
            groupBox1.Enabled = true;
            btnLuu.Enabled = true;
            btnHuyBo.Enabled = true;

            //Không cho thao tác trên các nút Thêm/Sửa/Xóa/Reload/Trở Về
            btnThem.Enabled = false;
            btnXoa.Enabled = false;
            btnSua.Enabled = false;
            btnReload.Enabled = false;
            btnTroVe.Enabled = false;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            isInserting = 0;
            //Tạo biến để lấy dòng đã chọn trên datagridview để tiến hành chỉnh sửa dữ liệu trên dòng đó
            int r = dgvLoaiSP.CurrentCell.RowIndex;
            //Hiển thị lên textbox
            tbCategoryID.Text = dgvLoaiSP.Rows[r].Cells[0].Value.ToString();
            tbCategoryName.Text = dgvLoaiSP.Rows[r].Cells[1].Value.ToString();
            if (dgvLoaiSP.Rows[r].Cells[2].Value != null)
                tbDescription.Text = dgvLoaiSP.Rows[r].Cells[2].Value.ToString();
            else
                tbDescription.Text = "";
            // Cho phép người dùng thao tác với nút Lưu/Hủy Bỏ và cho phép nhập thông tin vào các textbox
            groupBox1.Enabled = true;
            btnLuu.Enabled = true;
            btnHuyBo.Enabled = true;
            //Không cho thao tác trên các nút Thêm/Sửa/Xóa/Reload/Trở Về
            btnThem.Enabled = false;
            btnXoa.Enabled = false;
            btnSua.Enabled = false;
            btnReload.Enabled = false;
            btnTroVe.Enabled = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (tbCategoryID.Text == "")
                MessageBox.Show("Hãy Chọn Loại Sản Phẩm Muốn Xóa!!!!","Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Information);
            else
            {
                DialogResult dialog;
                dialog = MessageBox.Show("Bạn có chắc muốn xóa loại sản phẩm có mã: " + tbCategoryID.Text, "Lưu Ý", MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                if (dialog == DialogResult.OK)
                {
                    try
                    {
                        //Tạo truy vấn để lấy dòng dữ liệu cần xoá
                        var category = context.Categories.Find(Convert.ToInt32(tbCategoryID.Text.Trim()));

                        if (category != null)
                        {
                            //Xoá dữ liệu
                            context.Categories.Remove(category);
                            context.SaveChanges();// Lưu các thay đổi
                        }

                        LoadData();

                        MessageBox.Show("Đã Xóa","Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi: " + ex.Message, "Không xóa được!!!",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    }

                }
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                // Thêm dữ liệu
                if (isInserting == 1)
                {
                    var category = new EntityClass.Category
                    {
                        CategoryName = tbCategoryName.Text.Trim(),
                        Description = tbDescription.Text.Trim(),
                    };

                    context.Categories.Add(category);
                    context.SaveChanges();
                }
                // Sửa dữ liệu
                else if (isInserting == 0)
                {
                    var category = context.Categories.Find(Convert.ToInt32(tbCategoryID.Text.Trim()));

                    if (category != null)
                    {
                        category.CategoryName = tbCategoryName.Text.Trim();
                        category.Description = tbDescription.Text.Trim();

                        context.SaveChanges();
                    }
                }
                // Thông báo
                MessageBox.Show("Đã Xong","Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Information);
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Không thể hoàn thành thao tác",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }

        }

        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            //Cho người dùng thao tác trở lại lên datagridview khi ấn vào nút huỷ bỏ
            dgvLoaiSP.Enabled = true;

            // Không cho phép người dùng thao tác với nút Lưu/Hủy Bỏ và các thông tin trên các textbox
            groupBox1.Enabled = false;
            btnLuu.Enabled = false;
            btnHuyBo.Enabled = false;

            //Cho thao tác trên các nút Thêm/Sửa/Xóa/Reload/Trở Về
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            btnReload.Enabled = true;
            btnTroVe.Enabled = true;

            //Reset các textbox 
            tbCategoryID.ResetText();
            tbCategoryName.ResetText();
            tbDescription.ResetText();
        }

        private void dgvLoaiSP_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Thứ tự dòng vừa click trên DataGridView
            int r = dgvLoaiSP.CurrentCell.RowIndex;

            // Chuyển thông tin lên các textbox
            tbCategoryID.Text = dgvLoaiSP.Rows[r].Cells[0].Value.ToString();
            tbCategoryName.Text = dgvLoaiSP.Rows[r].Cells[1].Value.ToString();
            if (dgvLoaiSP.Rows[r].Cells[2].Value != null)
                tbDescription.Text = dgvLoaiSP.Rows[r].Cells[2].Value.ToString();
            else
                tbDescription.Text = "";
        }
    }
}
