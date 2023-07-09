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
    public partial class QuanLySanPhamForm : Form
    {
        private int isInserting;
        private Context context;

        public QuanLySanPhamForm()
        {
            InitializeComponent();
            context = new Context();
        }

        private void QuanLySanPhamForm_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        public QuanLySanPhamForm(Context context)
        {
            InitializeComponent();
            this.context = context;
        }

        private void LoadData()
        {
            //Cho người dùng thao tác lên datagridview thi bắt đầu load dữ liệu
            dgvSanPham.Enabled = true;

            //Ngăn chặn người dùng thao tác lên các nút Lưu/Huỷ bỏ và các textbox
            groupBox1.Enabled = false;
            btnLuu.Enabled = false;
            btnHuyBo.Enabled = false;

            //Cho phép người dùng thao tác lên các nút Thêm/Xoá/Sửa/Reset/Trở về
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            btnReload.Enabled = true;
            btnTroVe.Enabled = true;

            //reset lại các textbox
            tbMaSP.ResetText();
            tbTenSP.ResetText();
            tbConLai.ResetText();
            tbGiaTien.ResetText();

            try
            {
                //Tạo câu truy vấn để lấy dữ liệu
                var query = from category in context.Categories
                            select new
                            {
                                category.CategoryID,
                                category.CategoryName
                            };
                var result = query.ToList();
                var result2 = query.ToList();

                //Đem dữ liệu vừa truy vấn lên combobox
                cbCategory.ValueMember = "CategoryID";
                cbCategory.DisplayMember = "CategoryName";
                cbCategory.DataSource = result;
                
                cbLoaiSP.ValueMember = "CategoryID";
                cbLoaiSP.DisplayMember = "CategoryName";
                cbLoaiSP.DataSource = result2;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Không lấy được dữ liệu từ bảng Categories");
            }

            cbCategory.Visible = false;
            lblChonCategory.Visible = false;

            switch (cbFilter.SelectedIndex)
            {
                case 0:
                    lblChonCategory.Visible = true;
                    cbCategory.Visible = true;
                    cbCategory.SelectedIndex = 0;
                    break;
                case 1:
                    var query1 = from product in context.Products
                                 where product.UnitsInStock > 0
                                 select new
                                 {
                                     product.ProductID,
                                     product.ProductName,
                                     product.Category.CategoryName,
                                     product.UnitPrice,
                                     product.UnitsInStock
                                 };
                    dgvSanPham.DataSource = query1.ToList();
                    break;
                case 2:
                    var query2 = from product in context.Products
                                 where product.UnitsInStock <= 0
                                 select new
                                 {
                                     product.ProductID,
                                     product.ProductName,
                                     product.Category.CategoryName,
                                     product.UnitPrice,
                                     product.UnitsInStock
                                 };
                    dgvSanPham.DataSource = query2.ToList();
                    break;
                default:
                    var query = from product in context.Products
                                select new
                                {
                                    product.ProductID,
                                    product.ProductName,
                                    product.Category.CategoryName,
                                    product.UnitPrice,
                                    product.UnitsInStock
                                };
                    dgvSanPham.DataSource = query.ToList();
                    break;
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            // Khi đã bấm nút thêm thì không cho phép người dùng thao tác trên DataGridView
            dgvSanPham.Enabled = false;

            // Kích hoạt biến isInserting
            isInserting = 1;

            // Xóa nội dung trong các textbox
            tbMaSP.ResetText();
            tbTenSP.ResetText();
            tbConLai.ResetText();
            tbGiaTien.ResetText();

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

            cbLoaiSP.SelectedIndex = 0;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            isInserting = 0;
            //Tạo biến để lấy dòng đã chọn trên datagridview để tiến hành chỉnh sửa dữ liệu trên dòng đó
            int r = dgvSanPham.CurrentCell.RowIndex;
            //Hiển thị lên textbox
            tbMaSP.Text = dgvSanPham.Rows[r].Cells[0].Value.ToString();
            tbTenSP.Text = dgvSanPham.Rows[r].Cells[1].Value.ToString();
            tbGiaTien.Text = dgvSanPham.Rows[r].Cells[3].Value.ToString();
            tbConLai.Text = dgvSanPham.Rows[r].Cells[4].Value.ToString();
            cbLoaiSP.Text = dgvSanPham.Rows[r].Cells[2].Value.ToString();
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

        private void dgvSanPham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Thứ tự dòng vừa click trên DataGridView
            int r = dgvSanPham.CurrentCell.RowIndex;
            // Chuyển thông tin lên các textbox
            tbMaSP.Text = dgvSanPham.Rows[r].Cells[0].Value.ToString();
            tbTenSP.Text = dgvSanPham.Rows[r].Cells[1].Value.ToString();
            tbGiaTien.Text = dgvSanPham.Rows[r].Cells[3].Value.ToString();
            tbConLai.Text = dgvSanPham.Rows[r].Cells[4].Value.ToString();
            cbLoaiSP.Text = dgvSanPham.Rows[r].Cells[2].Value.ToString();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (tbMaSP.Text == "")
                MessageBox.Show("Hãy Chọn Sản Phẩm Muốn Xóa!!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                DialogResult dialog;
                dialog = MessageBox.Show("Bạn có chắc muốn xóa sản phẩm có mã: " + tbMaSP.Text, "Lưu Ý", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialog == DialogResult.OK)
                {
                    try
                    {
                        //Tạo truy vấn để lấy dòng dữ liệu cần xoá
                        var product = context.Products.Find(Convert.ToInt32(tbMaSP.Text.Trim()));

                        if (product != null)
                        {
                            //Xoá dữ liệu
                            context.Products.Remove(product);
                            context.SaveChanges();// Lưu các thay đổi
                        }

                        LoadData();

                        MessageBox.Show("Đã Xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi: " + ex.Message, "Không xóa được!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            //Load lại dữ liệu
            LoadData();
            cbCategory.Visible = false;
            lblChonCategory.Visible = false;

            var query = from product in context.Products
                        select new
                        {
                            product.ProductID,
                            product.ProductName,
                            product.Category.CategoryName,
                            product.UnitPrice,
                            product.UnitsInStock
                        };
            dgvSanPham.DataSource = query.ToList();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                // Thêm dữ liệu
                if (isInserting == 1)
                {
                    var category = context.Categories.Find(Convert.ToInt32(cbLoaiSP.SelectedValue));
                    if (category != null)
                    {
                        var product = new EntityClass.Product
                        {
                            ProductName = tbTenSP.Text.Trim(),
                            UnitPrice = Convert.ToInt32(tbGiaTien.Text.Trim()),
                            UnitsInStock = Convert.ToInt16(tbConLai.Text.Trim()),
                            Category = category
                        };

                        context.Products.Add(product);

                        context.SaveChanges();
                    }
                    else
                        MessageBox.Show("Hãy chọn lại loại sản phẩm","Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
                // Sửa dữ liệu
                else
                {
                    var category = context.Categories.Find(Convert.ToInt32(cbLoaiSP.SelectedValue));

                    if (category != null)
                    {
                        var product = context.Products.Find(Convert.ToInt32(tbMaSP.Text.Trim()));

                        if (product != null)
                        {
                            product.ProductName = tbTenSP.Text.Trim();
                            product.UnitPrice = Convert.ToInt32(tbGiaTien.Text.Trim());
                            product.UnitsInStock = Convert.ToInt16(tbConLai.Text.Trim());
                            product.Category = category;
                        }    

                        context.SaveChanges();
                    }
                }

                MessageBox.Show("Đã Xong", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Không thể hoàn thành thao tác", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            //Cho người dùng thao tác trở lại lên datagridview khi ấn vào nút huỷ bỏ
            dgvSanPham.Enabled = true;

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
            tbMaSP.ResetText();
            tbTenSP.ResetText();
            tbConLai.ResetText();
            tbGiaTien.ResetText();

            cbLoaiSP.SelectedIndex = 0;
        }

        private void btnTroVe_Click(object sender, EventArgs e)
        {
            //Kết thúc
            this.Close();
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void cbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Truy vấn dựa theo CategoryID để lọc trong combobox
            var query0 = from product in context.Products
                         where product.CategoryID == (int)cbCategory.SelectedValue
                         select new
                         {
                             product.ProductID,
                             product.ProductName,
                             product.Category.CategoryName,
                             product.UnitPrice,
                             product.UnitsInStock
                         };
            //Đưa dữ liệu lên datagridview
            dgvSanPham.DataSource = query0.ToList();
        }
    }
}
