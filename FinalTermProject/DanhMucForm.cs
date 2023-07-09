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
    public partial class DanhMucForm : Form
    {
        private int danhMuc;

        public DanhMucForm(int danhMuc)
        {
            InitializeComponent();
            this.danhMuc = danhMuc;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DanhMucForm_Load(object sender, EventArgs e)
        {
            try
            {
                using (var context = new Context())
                {
                    switch (danhMuc)
                    {
                        case 1:
                            // Thiết lập title
                            this.Text = "Danh Mục Nhân Viên";
                            lblDanhMuc.Text = "DANH MỤC NHÂN VIÊN";
                            
                            // Truy vấn trả về toàn bộ nhân viên
                            var query1 = from employee in context.Employees
                                        select new
                                        {
                                            employee.EmployeeID,
                                            Name = employee.FirstName + " " + employee.LastName,
                                            Male = employee.Gender,
                                            employee.HomePhone,
                                            employee.Nationality,
                                            employee.BirthDate,
                                            employee.HireDate,
                                            employee.Address,
                                            employee.Salary
                                        };

                            // Đưa dữ liệu lên DataGridView
                            dgvDanhMuc.DataSource = query1.ToList();
                            break;
                        case 2:
                            // Thiết lập title
                            this.Text = "Danh Mục Khách Hàng";
                            lblDanhMuc.Text = "DANH MỤC KHÁCH HÀNG";

                            // Đưa dữ liệu lên DataGridView
                            dgvDanhMuc.DataSource = context.Customers.ToList();
                            dgvDanhMuc.Columns[7].Visible = false;
                            dgvDanhMuc.Columns[8].Visible = false;
                            break;
                        case 3:
                            // Thiết lập title
                            this.Text = "Danh Mục Sản Phẩm";
                            lblDanhMuc.Text = "DANH MỤC SẢN PHẨM";

                            // Truy vấn trả về toàn bộ sản phẩm của bảng Products
                            var query3 = from product in context.Products
                                        select new
                                        {
                                            product.ProductID,
                                            product.ProductName,
                                            product.UnitPrice,
                                            product.Category.CategoryName,
                                            product.UnitsInStock
                                        };

                            // Đưa dữ liệu lên DataGridView
                            dgvDanhMuc.DataSource = query3.ToList();
                            break;
                        case 4:
                            // Thiết lập title
                            this.Text = "Danh Mục Hóa Đơn";
                            lblDanhMuc.Text = "DANH MỤC HÓA ĐƠN";

                            // Truy vấn trả về thông tin toàn bộ hóa đơn
                            var query4 = from order in context.Orders
                                         select new
                                         {
                                             order.OrderID,
                                             Customer = order.Customer.ContactName,
                                             Employee = order.Employee.LastName + " " + order.Employee.FirstName,
                                             Table = order.Table.TableName,
                                             order.OrderDate,
                                             Address = order.ShipAddress + ", " + order.ShipCity,
                                             order.Total
                                         };

                            // Đưa dữ liệu lên DataGridView
                            dgvDanhMuc.DataSource = query4.ToList();
                            break;
                        case 6:
                            // Thiết lập title
                            this.Text = "Danh Mục Bàn";
                            lblDanhMuc.Text = "DANH MỤC BÀN";

                            // Đưa dữ liệu lên DataGridView
                            dgvDanhMuc.DataSource = context.Tables.ToList();
                            dgvDanhMuc.Columns[3].Visible = false;
                            dgvDanhMuc.Columns[4].Visible = false;
                            break;
                        case 7:
                            // Thiết lập title
                            this.Text = "Danh Mục Chi Tiết Đặt Bàn";
                            lblDanhMuc.Text = "DANH MỤC CHI TIẾT ĐẶT BÀN";

                            // Thực hiện truy vấn trả về thông tin đặt bàn
                            var query7 = from bookingDetail in context.BookingDetails
                                         join table in context.Tables on bookingDetail.TableID equals table.TableID
                                         select new
                                         {
                                             bookingDetail.Customer.ContactName,
                                             table.TableName,
                                             bookingDetail.BookingDate,
                                             bookingDetail.ReservationDate,
                                             bookingDetail.BeginTime,
                                             bookingDetail.EndTime
                                         };

                            // Đưa dữ liệu lên DataGridView
                            dgvDanhMuc.DataSource = query7.ToList();
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Không lấy được dữ liệu");
            }
        }
    }
}
