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
    public partial class QuanLyKhachHangForm : Form
    {
        private Context context;
        private int isInserting;

        public QuanLyKhachHangForm()
        {
            InitializeComponent();
            context = new Context();
        }

        public QuanLyKhachHangForm(Context context)
        {
            InitializeComponent();
            this.context = context;
        }

        private void LoadData()
        {
            //Cho phép thao tác lên datagridview
            dgvKhachHang.Enabled = true;
            //Không cho phép thao tác lên các nút Lưu/Huỷ bỏ và các thông tin trên textbox
            groupBox1.Enabled = false;
            btnLuu.Enabled = false;
            btnHuyBo.Enabled = false;
            //Cho phép thao tác lên các nút thêm/xoá /sửa/reload/trở về
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            btnReload.Enabled = true;
            btnTroVe.Enabled = true;
            //reset lại các textbox
            tbCustomerID.ResetText();
            tbContactName.ResetText();
            tbAdress.ResetText();
            tbPhone.ResetText();
            tbNationality.ResetText();
            tbEmail.ResetText();
            tbCity.ResetText();

            try
            {
                //Tạo truy vấn để lấy dữ liệu show lên datagridview
                var query = from customer in context.Customers
                            select new
                            {
                                customer.CustomerID,
                                customer.ContactName,
                                customer.Adress,
                                customer.City,
                                customer.Nationality,
                                customer.Phone,
                                customer.Email,
                            };
                //Hiển thi lên  datagridview
                dgvKhachHang.DataSource = query.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Không lấy được dữ liệu từ bảng Customers",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void QuanLyKhachHangForm_Load(object sender, EventArgs e)
        {
            //Load lại dữ liệu
            LoadData();
        }

        private void btnTroVe_Click(object sender, EventArgs e)
        {          
            this.Close();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            //Không cho phép thao tác lên dữ liệu trên datagridview
            dgvKhachHang.Enabled = false;

            isInserting = 1;
            //Reset lại các textbox để chuẩn bị thêm dữ liệu
            tbCustomerID.ResetText();
            tbContactName.ResetText();
            tbAdress.ResetText();
            tbPhone.ResetText();
            tbNationality.ResetText();
            tbEmail.ResetText();
            tbCity.ResetText();
            //Cho phép thao tác lên các nút lưu/hủy bỏ và lên các dữ liệu trên textbox
            groupBox1.Enabled = true;
            btnLuu.Enabled = true;
            btnHuyBo.Enabled = true;
            //Không cho phép thao tác lên các nút thêm/Xoá/Sửa/Reload/Trở về
            btnThem.Enabled = false;
            btnXoa.Enabled = false;
            btnSua.Enabled = false;
            btnReload.Enabled = false;
            btnTroVe.Enabled = false;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            isInserting = 0;
            //Lấy dòng dữ liệu đã chọn mà cần sửa chữa
            int r = dgvKhachHang.CurrentCell.RowIndex;
            //Hiển thị lên textbox
            tbCustomerID.Text = dgvKhachHang.Rows[r].Cells[0].Value.ToString();
            tbContactName.Text = dgvKhachHang.Rows[r].Cells[1].Value.ToString();
            if (dgvKhachHang.Rows[r].Cells[2].Value != null)
                tbAdress.Text = dgvKhachHang.Rows[r].Cells[2].Value.ToString();
            else
                tbAdress.Text = "";
            if (dgvKhachHang.Rows[r].Cells[3].Value != null)
                tbCity.Text = dgvKhachHang.Rows[r].Cells[3].Value.ToString();
            else
                tbCity.Text = "";
            if (dgvKhachHang.Rows[r].Cells[4].Value != null)
                tbNationality.Text = dgvKhachHang.Rows[r].Cells[4].Value.ToString();
            else
                tbNationality.Text = "";
            if (dgvKhachHang.Rows[r].Cells[5].Value != null)
                tbPhone.Text = dgvKhachHang.Rows[r].Cells[5].Value.ToString();
            else
                tbPhone.Text = "";
            if (dgvKhachHang.Rows[r].Cells[6].Value != null)
                tbEmail.Text = dgvKhachHang.Rows[r].Cells[6].Value.ToString();
            else
                tbEmail.Text = "";
            //Cho phép thao tác lên các nút lưu/hủy bỏ và lên các dữ liệu trên textbox 
            groupBox1.Enabled = true;
            btnLuu.Enabled = true;
            btnHuyBo.Enabled = true;
            //Không cho phép thao tác lên các nút thêm/Xoá/Sửa/Reload/Trở về
            btnThem.Enabled = false;
            btnXoa.Enabled = false;
            btnSua.Enabled = false;
            btnReload.Enabled = false;
            btnTroVe.Enabled = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (tbCustomerID.Text == "")
                MessageBox.Show("Hãy Chọn Khách Hàng Muốn Xóa!!!!","Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Information);
            else
            {
                DialogResult dialog;
                dialog = MessageBox.Show("Bạn có chắc muốn xóa khách hàng có mã: " + tbCustomerID.Text, "Lưu Ý", MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                if (dialog == DialogResult.OK)
                {
                    try
                    {
                        //Tạo truy vấn để lấy ra dữ liệu cần xoá 
                        var customer = context.Customers.Find(Convert.ToInt32(tbCustomerID.Text.Trim()));

                        if (customer != null)
                        {
                            //Xoá dữ liệu
                            context.Customers.Remove(customer);
                            context.SaveChanges();//Lưu các thay đổi sau khi xoá
                        }
                        LoadData();

                        MessageBox.Show("Đã Xóa","Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi: " + ex.Message, "Không xóa được!!!");
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
                    var customer = new EntityClass.Customer
                    {
                        ContactName = tbContactName.Text.Trim(),
                        Adress = tbAdress.Text.Trim(),
                        City = tbCity.Text.Trim(),
                        Nationality = tbNationality.Text.Trim(),
                        Phone = tbPhone.Text.Trim(),
                        Email = tbEmail.Text.Trim()
                    };

                    context.Customers.Add(customer);
                    context.SaveChanges();
                }
                // Sửa dữ liệu
                else if (isInserting == 0)
                {
                    var customer = context.Customers.Find(Convert.ToInt32(tbCustomerID.Text.Trim()));

                    if (customer != null)
                    {
                        customer.ContactName = tbContactName.Text.Trim();
                        customer.Adress = tbAdress.Text.Trim();
                        customer.City = tbCity.Text.Trim();
                        customer.Nationality = tbNationality.Text.Trim();
                        customer.Phone = tbPhone.Text.Trim();
                        customer.Email = tbEmail.Text.Trim();

                        context.SaveChanges();
                    }
                }

                MessageBox.Show("Đã Xong","Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Information);
                //Load lại dữ liệu
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Không thể hoàn thành thao tác");
            }
        }

        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            //Cho người dùng thao tác trở lại lên datagridview khi ấn vào nút huỷ bỏ
            dgvKhachHang.Enabled = true;
            
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
            tbCustomerID.ResetText();
            tbContactName.ResetText();
            tbAdress.ResetText();
            tbPhone.ResetText();
            tbNationality.ResetText();
            tbEmail.ResetText();
            tbCity.ResetText();
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void dgvKhachHang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Thứ tự dòng vừa click trên DataGridView
            int r = dgvKhachHang.CurrentCell.RowIndex;

            // Chuyển thông tin lên các textbox
            tbCustomerID.Text = dgvKhachHang.Rows[r].Cells[0].Value.ToString();
            tbContactName.Text = dgvKhachHang.Rows[r].Cells[1].Value.ToString();
            if (dgvKhachHang.Rows[r].Cells[2].Value != null)
                tbAdress.Text = dgvKhachHang.Rows[r].Cells[2].Value.ToString();
            else
                tbAdress.Text = "";
            if (dgvKhachHang.Rows[r].Cells[3].Value != null)
                tbCity.Text = dgvKhachHang.Rows[r].Cells[3].Value.ToString();
            else
                tbCity.Text = "";
            if (dgvKhachHang.Rows[r].Cells[4].Value != null)
                tbNationality.Text = dgvKhachHang.Rows[r].Cells[4].Value.ToString();
            else
                tbNationality.Text = "";
            if (dgvKhachHang.Rows[r].Cells[5].Value != null)
                tbPhone.Text = dgvKhachHang.Rows[r].Cells[5].Value.ToString();
            else
                tbPhone.Text = "";
            if (dgvKhachHang.Rows[r].Cells[6].Value != null)
                tbEmail.Text = dgvKhachHang.Rows[r].Cells[6].Value.ToString();
            else
                tbEmail.Text = "";
        }
    }
}