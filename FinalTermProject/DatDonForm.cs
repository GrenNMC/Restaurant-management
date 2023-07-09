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
using System.Data.Entity;
using System.Data.Entity.Core.Objects;

namespace FinalTermProject
{
    public partial class DatDonForm : Form
    {
        private bool isReseting = false;
        private Context context;

        public DatDonForm()
        {
            InitializeComponent();
            context = new Context();
        }

        public DatDonForm(Context context)
        {
            InitializeComponent();
            this.context = context;
        }

        #region Sự kiện Form Load

        private void DatDonForm_Load(object sender, EventArgs e)
        {
            // Mặc định combobox là giao đồ ăn
            cbAnTaiCho.SelectedIndex = 0;

            try
            {
                // Thực hiện truy vấn trả về danh sách nhân viên
                var listEmployee = from emp in context.Employees
                                   select new
                                   {
                                       emp.EmployeeID,
                                       FullName = emp.LastName + " " + emp.FirstName
                                   };

                // Đưa kết quả có được từ truy vấn lên DataGridView
                cbNhanVien.DataSource = listEmployee.ToList();
                cbNhanVien.ValueMember = "EmployeeID";
                cbNhanVien.DisplayMember = "FullName";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Không lấy được dữ liệu từ bảng Employees");
            }
        }
        #endregion

        #region Sự Kiện nút Hủy Bỏ

        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region Sự Kiện nút Reset

        private void btnReset_Click(object sender, EventArgs e)
        {
            // Kích hoạt biến isReseting
            isReseting = true;

            // Mặc định combobox là giao đồ ăn
            cbAnTaiCho.SelectedIndex = 0;

            // Cho người dùng thao tác với combobox nhân viên
            cbNhanVien.Enabled = true;

            // Ẩn các thông tin của mục Ăn tại chỗ
            lblLoaiBan.Visible = false;
            cbTable.Visible = false;
            cbTable.SelectedIndex = -1;

            // Không cho người dùng bấm nút đặt hàng
            btnDatHang.Enabled = false;

            // Mặc định nhân viên phục vụ là nhân viên đầu tiên
            cbNhanVien.SelectedIndex = 0;

            // Cho người dùng nhập thông tin vào các textbox
            gbKhachHang.Enabled = true;

            // Ẩn các label cảnh báo
            lblStatus1.Visible = false;
            lblStatus2.Visible = false;
            lblStatus3.Visible = false;
            lblStatus4.Visible = false;

            // Reset các textbox
            tbTenKH.ResetText();
            tbDiaChiKH.ResetText();
            tbEmailKH.ResetText();
            tbSDTKH.ResetText();
            tbQuocTichKH.ResetText();
            tbDiaChiKH.ResetText();
            tbThanhPhoKH.ResetText();

            // Mặc định các combobox chọn sản phẩm là không chọn
            cbSanPham1.SelectedIndex = -1;
            cbSanPham2.SelectedIndex = -1;
            cbSanPham3.SelectedIndex = -1;
            cbSanPham4.SelectedIndex = -1;

            // Đưa các số lượng sản phẩm ở các textbox về 0 
            tbSLSanPham1.Text = 0 + "";
            tbSLSanPham2.Text = 0 + "";
            tbSLSanPham3.Text = 0 + "";
            tbSLSanPham4.Text = 0 + "";

            // Đưa giá ở các textbox về 0
            tbGiaSP1.Text = 0 + "";
            tbGiaSP2.Text = 0 + "";
            tbGiaSP3.Text = 0 + "";
            tbGiaSP4.Text = 0 + "";

            // Vô hiệu các nút thêm số lượng sản phẩm
            btnMoreSP1.Enabled = false;
            btnMoreSP2.Enabled = false;
            btnMoreSP3.Enabled = false;
            btnMoreSP4.Enabled = false;

            // Vô hiệu các nút giảm số lượng sản phẩm
            btnLessSP1.Enabled = false;
            btnLessSP2.Enabled = false;
            btnLessSP3.Enabled = false;
            btnLessSP4.Enabled = false;

            // Đưa tổng tiền về 0
            tbTongTien.Text = 0 + "";

            // Không cho phép thao tác trên groupbox chọn sản phẩm nữa
            gbDonHang.Enabled = false;
        }

        #endregion

        #region Sự Kiện nhấn nút OK

        private void btnOK_Click(object sender, EventArgs e)
        {
            // Ẩn các label cảnh báo
            lblStatus1.Visible = false;
            lblStatus2.Visible = false;
            lblStatus3.Visible = false;
            lblStatus4.Visible = false;

            // Nếu người dùng đã chọn Ăn Tại Chỗ hay Giao Đồ Ăn
            if (cbAnTaiCho.SelectedIndex != -1)
            {
                // Nếu chọn giao đồ ăn
                if (cbAnTaiCho.SelectedIndex == 0)
                {
                    // Kiểm tra các textbox bắt buộc phải nhập có trống không nếu không bắt đầu quá trình đặt đơn
                    if (tbDiaChiKH.Text != "" && tbTenKH.Text != ""
                        && tbThanhPhoKH.Text != "" && tbSDTKH.Text != "")
                    {
                        //  Cho phép người dùng thao tác với groupbox thông tin đơn hàng và vô hiệu groupbox thông tin khách hàng
                        gbDonHang.Enabled = true;
                        gbKhachHang.Enabled = false;

                        try
                        {
                            // thực hiện truy vấn lấy list các sản phẩm đang còn hàng
                            var sp1 = from product in context.Products
                                      where product.UnitsInStock > 0
                                      select new { product.ProductName, product.ProductID };

                            // Đưa dữ liệu có được từ truy vấn trên vào combobox sản phẩm 1
                            cbSanPham1.DataSource = sp1.ToList();
                            cbSanPham1.ValueMember = "ProductID";
                            cbSanPham1.DisplayMember = "ProductName";
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Lỗi: " + ex.Message, "Không lấy được dữ liệu từ bảng Products");
                        }
                    }
                    // Nếu người dùng bỏ trống ô nào thì nhắc nhở điền ô đó
                    else
                    {
                        if (tbTenKH.Text == "")
                            lblStatus1.Visible = true;
                        if (tbDiaChiKH.Text == "")
                            lblStatus2.Visible = true;
                        if (tbSDTKH.Text == "")
                            lblStatus3.Visible = true;
                        if (tbThanhPhoKH.Text == "")
                            lblStatus4.Visible = true;
                    }
                }
                // Nếu chọn ăn tại chỗ
                else if (cbAnTaiCho.SelectedIndex == 1)
                {
                    // Kiểm tra xem người dùng đã nhập đầy đủ các textbox bắt buộc và đã chọn bàn chưa nếu đã chọn
                    // thì bắt đầu quá trình đặt đơn
                    if (tbTenKH.Text != "" && cbTable.SelectedIndex != -1 && cbAnTaiCho.SelectedItem != null && tbSDTKH.Text != "")
                    {
                        //  Cho phép người dùng thao tác với groupbox thông tin đơn hàng và vô hiệu groupbox thông tin khách hàng
                        gbDonHang.Enabled = true;
                        gbKhachHang.Enabled = false;

                        try
                        {
                            // thực hiện truy vấn lấy list các sản phẩm đang còn hàng
                            var sp1 = from product in context.Products
                                      where product.UnitsInStock > 0
                                      select new { product.ProductName, product.ProductID };

                            // Đưa dữ liệu có được từ truy vấn trên vào combobox sản phẩm 1
                            cbSanPham1.DataSource = sp1.ToList();
                            cbSanPham1.ValueMember = "ProductID";
                            cbSanPham1.DisplayMember = "ProductName";
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Lỗi: " + ex.Message, "Không lấy được dữ liệu từ bảng Products");
                        }
                    }
                    else
                    {
                        // Nếu người dùng bỏ trống ô nào thì nhắc nhở điền ô đó
                        if (tbTenKH.Text == "")
                            lblStatus1.Visible = true;
                        if (cbTable.SelectedIndex == -1)
                            lblStatus2.Visible = true;
                        if (tbSDTKH.Text == "")
                            lblStatus3.Visible = true;
                    }
                }
            }
            else
            {
                MessageBox.Show("Hãy chọn loại đơn là Ăn Tại Chỗ hay Giao Đồ Ăn");
                return;
            }    
        }

        #endregion

        #region Sự kiện thay đổi textBox số lượng Sản Phẩm

        // Nếu người dùng chỉnh sửa textbox số lượng sản phẩm thì sẽ hủy kích hoạt biến isResting
        private void tbSLSanPham1_MouseClick(object sender, MouseEventArgs e)
        {
            isReseting = false;
        }

        private void tbSLSanPham2_MouseClick(object sender, MouseEventArgs e)
        {
            isReseting = false;
        }

        private void tbSLSanPham3_MouseClick(object sender, MouseEventArgs e)
        {
            isReseting = false;
        }

        private void tbSLSanPham4_MouseClick(object sender, MouseEventArgs e)
        {
            isReseting = false;
        }

        private void tbSLSanPham1_TextChanged(object sender, EventArgs e)
        {
            // Kiểm tra textbox số lượng có trống nếu có thì hiển thị 0
            if (tbSLSanPham1.Text == "")
                tbSLSanPham1.Text = "0";
            
            // Nếu số lượng sản phẩm hiện tại > 0 thì cho phép người dùng chọn sản phẩm tiếp theo
            if (Convert.ToInt32(tbSLSanPham1.Text) > 0)
            {
                cbSanPham2.Enabled = true;
                btnLessSP1.Enabled = true;
                btnDatHang.Enabled = true;
            }
            // Ngược lại thì vô hiệu hóa combobox sản phẩm tiếp theo và vô hiệu nút đặt hàng/giảm số lượng sản phẩm
            else
            {
                cbSanPham2.Enabled = false;
                btnLessSP1.Enabled = false;
                btnDatHang.Enabled = false;
            }

            // Nếu đang không Reset
            if (!isReseting)
            {
                // Gán thông tin từ textbox số lượng và combobox sản phẩm gán vào biến quantity và productID
                int quantity = Convert.ToInt32(tbSLSanPham1.Text),
                    productID = (int)cbSanPham1.SelectedValue;
                
                // Tìm sản phẩm có mã trùng với biến productID
                var product = context.Products.Find(productID);

                // Khởi tạo biến discount
                decimal discount = 1m;

                // Nếu khách ăn tại chỗ và ăn bàn VIP thì tăng giá trị hóa đơn thêm 20%
                if (cbTable.Visible == true && cbTable.Text.Trim().Contains("VIP"))
                    discount += 0.2m;

                // Nếu khách đặt/ăn 1 món từ 10 dĩa trở lên thì giảm 30% giá trị hóa đơn 
                if (quantity >= 10)
                    discount -= 0.3m;

                // khởi tạo biến kết quả lưu tổng tiền của sản phẩm 1
                long result = (long)(product.UnitPrice * quantity * discount);

                // Đưa giá trị của biến result lên textbox
                tbGiaSP1.Text = result + "";
            }
        }

        private void tbSLSanPham2_TextChanged(object sender, EventArgs e)
        {
            // Kiểm tra textbox số lượng có trống nếu có thì hiển thị 0
            if (tbSLSanPham2.Text == "")
                tbSLSanPham2.Text = "0";

            // Nếu số lượng sản phẩm hiện tại > 0 thì cho phép người dùng chọn sản phẩm tiếp theo
            if (Convert.ToInt32(tbSLSanPham2.Text) > 0)
            {
                cbSanPham3.Enabled = true;
                btnLessSP2.Enabled = true;
            }
            // Ngược lại thì vô hiệu hóa combobox sản phẩm tiếp theo và vô hiệu nút đặt hàng/giảm số lượng sản phẩm
            else
            {
                cbSanPham3.Enabled = false;
                btnLessSP2.Enabled = false;
            }

            // Nếu đang không Reset
            if (!isReseting)
            {
                // Gán thông tin từ textbox số lượng và combobox sản phẩm gán vào biến quantity và productID
                int quantity = Convert.ToInt32(tbSLSanPham2.Text),
                    productID = (int)cbSanPham2.SelectedValue;

                // Tìm sản phẩm có mã trùng với biến productID
                var product = context.Products.Find(productID);

                // Khởi tạo biến discount
                decimal discount = 1m;

                // Nếu khách ăn tại chỗ và ăn bàn VIP thì tăng giá trị hóa đơn thêm 20%
                if (cbTable.Visible == true && cbTable.Text.Trim().Contains("VIP"))
                    discount += 0.2m;

                // Nếu khách đặt/ăn 1 món từ 10 dĩa trở lên thì giảm 30% giá trị hóa đơn 
                if (quantity >= 10)
                    discount -= 0.3m;

                // khởi tạo biến kết quả lưu tổng tiền của sản phẩm 1
                long result = (long)(product.UnitPrice * quantity * discount);

                // Đưa giá trị của biến result lên textbox
                tbGiaSP2.Text = result + "";
            }
        }

        private void tbSLSanPham3_TextChanged(object sender, EventArgs e)
        {
            // Kiểm tra textbox số lượng có trống nếu có thì hiển thị 0
            if (tbSLSanPham3.Text == "")
                tbSLSanPham3.Text = "0";

            // Nếu số lượng sản phẩm hiện tại > 0 thì cho phép người dùng chọn sản phẩm tiếp theo
            if (Convert.ToInt32(tbSLSanPham3.Text) > 0)
            {
                cbSanPham4.Enabled = true;
                btnLessSP3.Enabled = true;
            }
            // Ngược lại thì vô hiệu hóa combobox sản phẩm tiếp theo và vô hiệu nút đặt hàng/giảm số lượng sản phẩm
            else
            {
                cbSanPham4.Enabled = false;
                btnLessSP3.Enabled = false;
            }

            // Nếu đang không Reset
            if (!isReseting)
            {
                // Gán thông tin từ textbox số lượng và combobox sản phẩm gán vào biến quantity và productID
                int quantity = Convert.ToInt32(tbSLSanPham3.Text),
                    productID = (int)cbSanPham3.SelectedValue;

                // Tìm sản phẩm có mã trùng với biến productID
                var product = context.Products.Find(productID);

                // Khởi tạo biến discount
                decimal discount = 1m;

                // Nếu khách ăn tại chỗ và ăn bàn VIP thì tăng giá trị hóa đơn thêm 20%
                if (cbTable.Visible == true && cbTable.Text.Trim().Contains("VIP"))
                    discount += 0.2m;

                // Nếu khách đặt/ăn 1 món từ 10 dĩa trở lên thì giảm 30% giá trị hóa đơn 
                if (quantity >= 10)
                    discount -= 0.3m;

                // khởi tạo biến kết quả lưu tổng tiền của sản phẩm 1
                long result = (long)(product.UnitPrice * quantity * discount);

                // Đưa giá trị của biến result lên textbox
                tbGiaSP3.Text = result + "";
            }
        }

        private void tbSLSanPham4_TextChanged(object sender, EventArgs e)
        {
            if (tbSLSanPham4.Text == "")
                tbSLSanPham4.Text = "0";

            if (Convert.ToInt32(tbSLSanPham3.Text) > 0)
                btnLessSP4.Enabled = true;
            else
                btnLessSP4.Enabled = false;

            if (!isReseting)
            {
                // Gán thông tin từ textbox số lượng và combobox sản phẩm gán vào biến quantity và productID
                int quantity = Convert.ToInt32(tbSLSanPham4.Text),
                    productID = (int)cbSanPham4.SelectedValue;

                // Tìm sản phẩm có mã trùng với biến productID
                var product = context.Products.Find(productID);

                // Khởi tạo biến discount
                decimal discount = 1m;

                // Nếu khách ăn tại chỗ và ăn bàn VIP thì tăng giá trị hóa đơn thêm 20%
                if (cbTable.Visible == true && cbTable.Text.Trim().Contains("VIP"))
                    discount += 0.2m;

                // Nếu khách đặt/ăn 1 món từ 10 dĩa trở lên thì giảm 30% giá trị hóa đơn 
                if (quantity >= 10)
                    discount -= 0.3m;

                // khởi tạo biến kết quả lưu tổng tiền của sản phẩm 1
                long result = (long)(product.UnitPrice * quantity * discount);

                // Đưa giá trị của biến result lên textbox
                tbGiaSP4.Text = result + "";
            }
        }

        #endregion

        #region Sự Kiện các nút + -

        // Nếu người dùng bấm + thì số lượng sản phẩm trong textbox + 1
        private void btnLessSP1_Click(object sender, EventArgs e)
        {
            isReseting = false;
            tbSLSanPham1.Text = (Convert.ToInt32(tbSLSanPham1.Text) - 1) + "";
        }

        private void btnMoreSP1_Click(object sender, EventArgs e)
        {
            isReseting = false;
            tbSLSanPham1.Text = (Convert.ToInt32(tbSLSanPham1.Text) + 1) + "";
        }

        private void btnLessSP2_Click(object sender, EventArgs e)
        {
            isReseting = false;
            tbSLSanPham2.Text = (Convert.ToInt32(tbSLSanPham2.Text) - 1) + "";
        }

        private void btnMoreSP2_Click(object sender, EventArgs e)
        {
            isReseting = false;
            tbSLSanPham2.Text = (Convert.ToInt32(tbSLSanPham2.Text) + 1) + "";
        }

        private void btnLessSP3_Click(object sender, EventArgs e)
        {
            isReseting = false;
            tbSLSanPham3.Text = (Convert.ToInt32(tbSLSanPham3.Text) - 1) + "";
        }

        private void btnMoreSP3_Click(object sender, EventArgs e)
        {
            isReseting = false;
            tbSLSanPham3.Text = (Convert.ToInt32(tbSLSanPham3.Text) + 1) + "";
        }

        private void btnLessSP4_Click(object sender, EventArgs e)
        {
            isReseting = false;
            tbSLSanPham4.Text = (Convert.ToInt32(tbSLSanPham4.Text) - 1) + "";
        }

        private void btnMoreSP4_Click(object sender, EventArgs e)
        {
            isReseting = false;
            tbSLSanPham4.Text = (Convert.ToInt32(tbSLSanPham4.Text) + 1) + "";
        }

        #endregion

        #region Sự Kiện thay đổi comboBox Sản Phẩm

        private void cbSanPham1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Cho phép nhấn nút + ở textbox số lượng sản phẩm
            btnMoreSP1.Enabled = true;
            // Cho phép thao tác với textbox số lượng sản phẩm
            tbSLSanPham1.Text = 0 + "";
            tbSLSanPham1.Enabled = true;

            try
            {
                // thực hiện truy vấn lấy list các sản phẩm đang còn hàng
                var sp2 = from product in context.Products
                          where product.UnitsInStock > 0
                          select new { product.ProductName, product.ProductID };

                // Đưa dữ liệu có được từ truy vấn trên vào combobox sản phẩm 2
                cbSanPham2.DataSource = sp2.ToList();
                cbSanPham2.ValueMember = "ProductID";
                cbSanPham2.DisplayMember = "ProductName";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Không lấy được dữ liệu từ bảng Products");
            }
        }

        private void cbSanPham2_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Cho phép nhấn nút + ở textbox số lượng sản phẩm
            btnMoreSP2.Enabled = true;
            // Cho phép thao tác với textbox số lượng sản phẩm
            tbSLSanPham2.Text = 0 + "";
            tbSLSanPham2.Enabled = true;

            try
            {
                // thực hiện truy vấn lấy list các sản phẩm đang còn hàng
                var sp3 = from product in context.Products
                          where product.UnitsInStock > 0
                          select new { product.ProductName, product.ProductID };

                // Đưa dữ liệu có được từ truy vấn trên vào combobox sản phẩm 3
                cbSanPham3.DataSource = sp3.ToList();
                cbSanPham3.ValueMember = "ProductID";
                cbSanPham3.DisplayMember = "ProductName";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Không lấy được dữ liệu từ bảng Products");
            }
        }

        private void cbSanPham3_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Cho phép nhấn nút + ở textbox số lượng sản phẩm
            btnMoreSP3.Enabled = true;
            // Cho phép thao tác với textbox số lượng sản phẩm
            tbSLSanPham3.Text = 0 + "";
            tbSLSanPham3.Enabled = true;

            try
            {
                // thực hiện truy vấn lấy list các sản phẩm đang còn hàng
                var sp4 = from product in context.Products
                          where product.UnitsInStock > 0
                          select new { product.ProductName, product.ProductID };

                // Đưa dữ liệu có được từ truy vấn trên vào combobox sản phẩm 3
                cbSanPham4.DataSource = sp4.ToList();
                cbSanPham4.ValueMember = "ProductID";
                cbSanPham4.DisplayMember = "ProductName";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Không lấy được dữ liệu từ bảng Products");
            }
        }

        private void cbSanPham4_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Cho phép nhấn nút + ở textbox số lượng sản phẩm
            btnMoreSP4.Enabled = true;
            // Cho phép thao tác với textbox số lượng sản phẩm
            tbSLSanPham4.Text = 0 + "";
            tbSLSanPham4.Enabled = true;
        }

        #endregion

        #region Sự Kiện thay đổi textBox giá Sản Phẩm
        
        // Cập nhật lại giá trị của ô tổng tiền nếu có sự thay đổi ở thông tin đặt hàng
        private void tbGiaSP1_TextChanged(object sender, EventArgs e)
        {
            tbTongTien.Text = Convert.ToInt64(tbGiaSP1.Text) + Convert.ToInt64(tbGiaSP2.Text) +
                Convert.ToInt64(tbGiaSP3.Text) + Convert.ToInt64(tbGiaSP4.Text) + "";
        }

        private void tbGiaSP2_TextChanged(object sender, EventArgs e)
        {
            tbTongTien.Text = Convert.ToInt64(tbGiaSP1.Text) + Convert.ToInt64(tbGiaSP2.Text) +
                Convert.ToInt64(tbGiaSP3.Text) + Convert.ToInt64(tbGiaSP4.Text) + "";
        }

        private void tbGiaSP3_TextChanged(object sender, EventArgs e)
        {
            tbTongTien.Text = Convert.ToInt64(tbGiaSP1.Text) + Convert.ToInt64(tbGiaSP2.Text) +
                Convert.ToInt64(tbGiaSP3.Text) + Convert.ToInt64(tbGiaSP4.Text) + "";
        }

        private void tbGiaSP4_TextChanged(object sender, EventArgs e)
        {
            tbTongTien.Text = Convert.ToInt64(tbGiaSP1.Text) + Convert.ToInt64(tbGiaSP2.Text) +
                Convert.ToInt64(tbGiaSP3.Text) + Convert.ToInt64(tbGiaSP4.Text) + "";
        }

        #endregion

        #region Sự Kiện nút đặt đơn

        private void btnDatHang_Click(object sender, EventArgs e)
        {
            short result = 0;

            // Bắt đầu transaction
            using (DbContextTransaction transaction = context.Database.BeginTransaction())
            {
                try
                {
                    // Tìm nhân viên có mã nhân viên được người dùng chọn
                    EntityClass.Employee emp = context.Employees.Find(Convert.ToInt32(cbNhanVien.SelectedValue));
                    // Tìm khách hàng có số điện thoại vừa nhập 
                    EntityClass.Customer cust = context.Customers.Where(c => c.Phone == tbSDTKH.Text.Trim()).FirstOrDefault();

                    // Nếu không có khách hàng nào có SĐT thì tiến hành tạo khách hàng mới và thêm vào CSDL
                    if (cust == null)
                    {
                        var customer = new EntityClass.Customer
                        {
                            ContactName = tbTenKH.Text.Trim(),
                            Nationality = tbQuocTichKH.Text.Trim(),
                            Phone = tbSDTKH.Text.Trim()
                        };

                        context.Customers.Add(customer);
                        context.SaveChanges();

                        cust = customer;
                    }

                    // Tạo đối tượng Order order
                    EntityClass.Order order = new EntityClass.Order();

                    // Gán cust và emp vào thuộc tính Customer và Employee
                    // (Quan hệ 3 ngôi: Order - Customer - Employee) 
                    order.Customer = cust;
                    order.Employee = emp;

                    // Nếu khách ăn tại chỗ
                    if (cbTable.Visible)
                    {
                        // Tạo và gán đồi tượng Table table cho order 
                        EntityClass.Table table = context.Tables.Find(Convert.ToInt32(cbTable.SelectedValue));
                        order.Table = table;
                    }

                    // Gán ngày đặt đơn chính là thời điểm hiện tại
                    order.OrderDate = DateTime.Now;
                    
                    // Nếu khách đặt giao đồ ăn
                    if (tbDiaChiKH.Visible)
                    {
                        // Gán thông tin địa chỉ từ các textbox vào order
                        order.ShipAddress = tbDiaChiKH.Text.Trim();
                        order.ShipCity = tbThanhPhoKH.Text.Trim();
                    }

                    // Thêm order vào CSDL
                    context.Orders.Add(order);
                    context.SaveChanges();

                    // Nếu khách đã chọn món 1
                    if (Convert.ToInt32(tbSLSanPham1.Text) > 0)
                    {
                        // Tìm sản phẩm có mã sản phẩm mà người dùng chọn
                        var product1 = context.Products.Find(Convert.ToInt32(cbSanPham1.SelectedValue));

                        // Kiểm tra xem còn món không
                        result = (short)(product1.UnitsInStock - Convert.ToInt16(tbSLSanPham1.Text));
                        
                        // Nếu không quăng exception để catch
                        if (result < 0)
                            throw new Exception("Sản phẩm 1 không đủ số lượng phục vụ !!!");

                        // Cập nhật lại số lượng còn lại của món
                        product1.UnitsInStock = result;

                        // Tạo đối tượng OrderDetail od1
                        EntityClass.OrderDetail od1 = new EntityClass.OrderDetail();

                        // Gán giá trị vào các thuộc tính của od1
                        od1.Quantity = Convert.ToInt16(tbSLSanPham1.Text.Trim());
                        od1.UnitPrice = Convert.ToInt32(tbGiaSP1.Text.Trim());
                        od1.Product = product1;
                        od1.Discount = 1;

                        // Cập nhật lại thuộc tính Discount nếu khách dùng bàn VIP
                        if (cbTable.Visible == true && cbTable.Text.Trim().Contains("VIP"))
                            od1.Discount += 0.2f;

                        // Cập nhật lại thuộc tính Discount nếu khách dùng một món trên 10 dĩa
                        if (od1.Quantity >= 10)
                            od1.Discount -= 0.3f;

                        // Gán od1 vào thuộc tính OrderDetails của order (Quan hệ One-To-Many)
                        order.OrderDetails.Add(od1);
                        context.SaveChanges();

                        // Nếu khách đã chọn món 2
                        if (Convert.ToInt32(tbSLSanPham2.Text) > 0)
                        {
                            // Tìm sản phẩm có mã sản phẩm mà người dùng chọn
                            var product2 = context.Products.Find(Convert.ToInt32(cbSanPham2.SelectedValue));

                            // Kiểm tra xem còn món không
                            result = (short)(product2.UnitsInStock - Convert.ToInt16(tbSLSanPham2.Text));

                            // Nếu không quăng exception để catch
                            if (result < 0)
                                throw new Exception("Sản phẩm 2 không đủ số lượng phục vụ !!!");

                            // Cập nhật lại số lượng còn lại của món
                            product2.UnitsInStock = result;

                            // Tạo đối tượng OrderDetail od2
                            EntityClass.OrderDetail od2 = new EntityClass.OrderDetail();

                            // Gán giá trị vào các thuộc tính của od2
                            od2.Quantity = Convert.ToInt16(tbSLSanPham2.Text.Trim());
                            od2.UnitPrice = Convert.ToInt32(tbGiaSP2.Text.Trim());
                            od2.Product = product2;
                            od2.Discount = 1;

                            // Cập nhật lại thuộc tính Discount nếu khách dùng bàn VIP
                            if (cbTable.Visible == true && cbTable.Text.Trim().Contains("VIP"))
                                od2.Discount += 0.2f;

                            // Cập nhật lại thuộc tính Discount nếu khách dùng một món trên 10 dĩa
                            if (od2.Quantity >= 10)
                                od2.Discount -= 0.3f;

                            // Gán od2 vào thuộc tính OrderDetails của order (Quan hệ One-To-Many)
                            order.OrderDetails.Add(od2);
                            context.SaveChanges();

                            // Nếu khách đã chọn món 3
                            if (Convert.ToInt32(tbSLSanPham3.Text) > 0)
                            {
                                // Tìm sản phẩm có mã sản phẩm mà người dùng chọn
                                var product3 = context.Products.Find(Convert.ToInt32(cbSanPham3.SelectedValue));

                                // Kiểm tra xem còn món không
                                result = (short)(product3.UnitsInStock - Convert.ToInt16(tbSLSanPham3.Text));

                                // Nếu không quăng exception để catch
                                if (result < 0)
                                    throw new Exception("Sản phẩm 3 không đủ số lượng phục vụ !!!");

                                // Cập nhật lại số lượng còn lại của món
                                product3.UnitsInStock = result;

                                // Tạo đối tượng OrderDetail od3
                                EntityClass.OrderDetail od3 = new EntityClass.OrderDetail();

                                // Gán giá trị vào các thuộc tính của od3
                                od3.Quantity = Convert.ToInt16(tbSLSanPham3.Text.Trim());
                                od3.UnitPrice = Convert.ToInt32(tbGiaSP3.Text.Trim());
                                od3.Product = product3;
                                od3.Discount = 1;

                                // Cập nhật lại thuộc tính Discount nếu khách dùng bàn VIP
                                if (cbTable.Visible == true && cbTable.Text.Trim().Contains("VIP"))
                                    od3.Discount += 0.2f;

                                // Cập nhật lại thuộc tính Discount nếu khách dùng một món trên 10 dĩa
                                if (od3.Quantity >= 10)
                                    od3.Discount -= 0.3f;

                                // Gán od3 vào thuộc tính OrderDetails của order (Quan hệ One-To-Many)
                                order.OrderDetails.Add(od3);
                                context.SaveChanges();

                                // Nếu khách đã chọn món 4
                                if (Convert.ToInt32(tbSLSanPham4.Text) > 0)
                                {
                                    // Tìm sản phẩm có mã sản phẩm mà người dùng chọn
                                    var product4 = context.Products.Find(Convert.ToInt32(cbSanPham4.SelectedValue));

                                    // Kiểm tra xem còn món không
                                    result = (short)(product4.UnitsInStock - Convert.ToInt16(tbSLSanPham4.Text));

                                    // Nếu không quăng exception để catch
                                    if (result < 0)
                                        throw new Exception("Sản phẩm 4 không đủ số lượng phục vụ !!!");

                                    // Cập nhật lại số lượng còn lại của món
                                    product4.UnitsInStock = result;

                                    // Tạo đối tượng OrderDetail od4
                                    EntityClass.OrderDetail od4 = new EntityClass.OrderDetail();

                                    // Gán giá trị vào các thuộc tính của od4
                                    od4.Quantity = Convert.ToInt16(tbSLSanPham4.Text.Trim());
                                    od4.UnitPrice = Convert.ToInt32(tbGiaSP4.Text.Trim());
                                    od4.Product = product4;
                                    od4.Discount = 1;

                                    // Cập nhật lại thuộc tính Discount nếu khách dùng bàn VIP
                                    if (cbTable.Visible == true && cbTable.Text.Trim().Contains("VIP"))
                                        od4.Discount += 0.2f;

                                    // Cập nhật lại thuộc tính Discount nếu khách dùng một món trên 10 dĩa
                                    if (od4.Quantity >= 10)
                                        od4.Discount -= 0.3f;

                                    // Gán od4 vào thuộc tính OrderDetails của order (Quan hệ One-To-Many)
                                    order.OrderDetails.Add(od4);
                                    context.SaveChanges();
                                }
                            }
                        }
                    }

                    // Gán thông tin từ textbox tổng giá tiền vào thuộc tính total của order
                    order.Total = Convert.ToInt32(tbTongTien.Text.Trim());
                    context.SaveChanges();

                    MessageBox.Show("Đã đặt đơn thành công");

                    // Commit transaction - áp dụng những thay đổi vừa rồi lên CSDL
                    transaction.Commit();
                }
                catch(Exception ex)
                {
                    // Nếu gặp lỗi thì phục hồi lại hiện trạng lúc chưa đặt đơn
                    transaction.Rollback();
                    MessageBox.Show("Lỗi: " + ex.Message, "Không thể hoàn thành thao tác");
                }
            }

            btnReset_Click(sender, e);
        }

        #endregion

        #region Sự kiện comboBox Ăn Tại Chỗ
        private void cbAnTaiCho_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Ẩn các label cảnh báo
            lblStatus1.Visible = false;
            lblStatus2.Visible = false;
            lblStatus3.Visible = false;
            lblStatus4.Visible = false;

            // Nếu chọn ăn tại chỗ
            if (cbAnTaiCho.SelectedIndex == 1)
            {
                // Ẩn các mục chứa thông tin giao hàng
                lblThanhPho.Visible = false;
                tbThanhPhoKH.Visible = false;
                lblDiaChi.Visible = false;
                tbDiaChiKH.Visible = false;

                // Hiện các mục chứa thông tin bàn cho người dùng chọn
                lblLoaiBan.Visible = true;
                cbTable.Visible = true;

                // Truy vấn tả về toàn bộ bàn trong bảng Tables
                var tables = from t in context.Tables
                             select new
                             {
                                 t.TableID,
                                 t.TableName
                             };

                // Khởi tạo giờ ăn và giờ kết thúc
                TimeSpan beginTime = new TimeSpan(hours: DateTime.Now.Hour, minutes: DateTime.Now.Minute, seconds: DateTime.Now.Second);
                TimeSpan endTime = beginTime + TimeSpan.FromHours(2);

                // Truy vấn trả về những bàn đã có người đặt trong thời gian 2 tiếng kể từ hiện tại đang đặt đơn
                var reservationAtThisTime = from bd in context.BookingDetails
                                            where bd.ReservationDate == (DateTime.Now)
                                                  && ((bd.BeginTime > beginTime
                                                       && bd.EndTime < endTime)
                                                       || bd.BeginTime < endTime)
                                            select new
                                            {
                                                bd.TableID,
                                                bd.Table.TableName
                                            };

                // Dùng phép hiệu 2 danh sách để ra những bàn khả dụng tại thời điểm khách ăn
                var query = tables.Except(reservationAtThisTime);

                // Đưa dữ liệu có được từ truy vấn lên combobox
                cbTable.DataSource = query.ToList();
                cbTable.DisplayMember = "TableName";
                cbTable.ValueMember = "TableID";

                cbTable.SelectedIndex = -1;
            }
            // Nếu người dùng chọn giao đồ ăn
            else if (cbAnTaiCho.SelectedIndex == 0)
            {
                // Hiện các mục chứa thông tin giao hàng
                lblThanhPho.Visible = true;
                tbThanhPhoKH.Visible = true;
                lblDiaChi.Visible = true;
                tbDiaChiKH.Visible = true;

                // Ẩn các mục chứa thông tin bàn
                lblLoaiBan.Visible = false;
                cbTable.Visible = false;
            }    
        }
        #endregion

    }
}
