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
    public partial class QuanLyHoaDonForm : Form
    {
        private Context context;

        public QuanLyHoaDonForm()
        {
            InitializeComponent();
            context = new Context();
    }

        public QuanLyHoaDonForm(Context context)
        {
            InitializeComponent();
            this.context = context;
        }

        private void LoadData()
        {
            //Không cho người dùng thao tác lên datagridview thi bắt đầu load dữ liệu
            groupBox1.Enabled = false;
            //Cho phép người dùng thao tác lên các nút Lưu/Huỷ bỏ và các textbox
            btnXoa.Enabled = true;
            btnReload.Enabled = true;
            btnTroVe.Enabled = true;
            //Reset lại các textbox
            tbMaHoaDon.ResetText();
            tbMaKhachHang.ResetText();
            tbMaBan.ResetText();
            tbMaNhanVien.ResetText();
            tbDiaChi.ResetText();
            tbThanhPho.ResetText();

            try
            {
                //Tạo câu truy vấn để lấy dữ liệu
                var query = from order in context.Orders
                            select new
                            {
                                order.OrderID,
                                order.CustomerID,
                                order.EmployeeID,
                                order.TableID,
                                order.OrderDate,
                                order.ShipAddress,
                                order.ShipCity
                            };
                //Đem dữ liệu vừa truy vấn lên datadridview
                dgvHoaDon.DataSource = query.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Không lấy được dữ liệu từ bảng Orders",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void QuanLyHoaDonForm_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnTroVe_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (tbMaHoaDon.Text == "")
                MessageBox.Show("Hãy Chọn Hóa Đơn Muốn Xóa!!!!");
            else
            {
                DialogResult dialog;
                dialog = MessageBox.Show("Bạn có chắc muốn xóa hóa đơn có mã: " + tbMaHoaDon.Text, "Lưu Ý", MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                if (dialog == DialogResult.OK)
                {
                    try
                    {
                        //Truy vấn để lấy ra dữ liệu cần xoá
                        var order = context.Orders.Find(Convert.ToInt32(tbMaHoaDon.Text.Trim()));
                        if(order != null)
                        {
                            //Xoá dữ liệu đã chọn
                            context.Orders.Remove(order);
                            context.SaveChanges();//Lưu các thay đổi
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

        private void dgvHoaDon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Thứ tự dòng vừa click trên DataGridView
            int r = dgvHoaDon.CurrentCell.RowIndex;

            // Chuyển thông tin lên các textbox
            tbMaHoaDon.Text = dgvHoaDon.Rows[r].Cells[0].Value.ToString();
            tbMaKhachHang.Text = dgvHoaDon.Rows[r].Cells[1].Value.ToString();
            tbMaNhanVien.Text = dgvHoaDon.Rows[r].Cells[2].Value.ToString();
            if (dgvHoaDon.Rows[r].Cells[3].Value != null)
                tbMaBan.Text = dgvHoaDon.Rows[r].Cells[3].Value.ToString();
            else
                tbMaBan.Text = "";
            tbNgayDat.Text = dgvHoaDon.Rows[r].Cells[4].Value.ToString();
            if (dgvHoaDon.Rows[r].Cells[5].Value != null)
                tbDiaChi.Text = dgvHoaDon.Rows[r].Cells[5].Value.ToString();
            else
                tbDiaChi.Text = "";
            if (dgvHoaDon.Rows[r].Cells[6].Value != null)
                tbThanhPho.Text = dgvHoaDon.Rows[r].Cells[6].Value.ToString();
            else
                tbThanhPho.Text = "";
        }
    }
}
