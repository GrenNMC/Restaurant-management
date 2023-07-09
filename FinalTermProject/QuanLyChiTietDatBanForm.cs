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
    public partial class QuanLyChiTietDatBanForm : Form
    {
        private Context context;

        public QuanLyChiTietDatBanForm()
        {
            InitializeComponent();
            context = new Context();    
        }

        public QuanLyChiTietDatBanForm(Context context)
        {
            InitializeComponent();
            this.context = context;
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnTroVe_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            //Cho người dùng thao tác trở lại lên datagridview khi ấn vào nút huỷ bỏ
            dgvDatBan.Enabled = true;

            // Không cho phép người dùng thao tác với nút Lưu/Hủy Bỏ và các thông tin trên các textbox
            groupBox1.Enabled = false;
            btnLuu.Enabled = false;
            btnHuyBo.Enabled = false;

            //Cho thao tác trên các nút Thêm/Sửa/Xóa/Reload/Trở Về
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            btnReload.Enabled = true;
            btnTroVe.Enabled = true;

            //Reset các textbox 
            tbMaKH.ResetText();
            cbLoaiBan.ResetText();
            tbNgayDat.ResetText();
            dtpNgayHetHan.ResetText();
        }

        private void dgvDatBan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Lấy dữ liệu từ datagridview
            int r = dgvDatBan.CurrentCell.RowIndex;
            //Đem dữ liệu lên các textbox ...
            tbMaKH.Text = dgvDatBan.Rows[r].Cells[0].Value.ToString();
            cbLoaiBan.Text = dgvDatBan.Rows[r].Cells[1].Value.ToString();
            tbNgayDat.Text = dgvDatBan.Rows[r].Cells[2].Value.ToString();
            dtpNgayHetHan.Text = dgvDatBan.Rows[r].Cells[3].Value.ToString();
            dtpBeginTime.Text = dgvDatBan.Rows[r].Cells[4].Value.ToString();
            dtpEndTime.Text = dgvDatBan.Rows[r].Cells[5].Value.ToString();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (tbMaKH.Text == "" || cbLoaiBan.Text == "")
                MessageBox.Show("Hãy Chọn Chi Tiết Đơn Đặt Bàn Muốn Xóa!!!!","Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Information);
            else
            {
                DialogResult dialog;
                dialog = MessageBox.Show("Bạn có chắc muốn xóa chi tiết đặt bàn này", "Lưu Ý", MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                if (dialog == DialogResult.OK)
                {
                    try
                    {
                        //Truy vấn để lấy dữ liệu cần xoá
                        var bd = context.BookingDetails.Find(Convert.ToInt32(tbMaKH.Text.Trim()),
                            Convert.ToInt32(cbLoaiBan.Text.Trim()));

                        if (bd != null)
                        {
                            //Xoá dòng đã chọn
                            context.BookingDetails.Remove(bd);
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

        private void btnSua_Click(object sender, EventArgs e)
        {
            //Cho phép thao tác lên dữ liệu có trong datagridview
            dgvDatBan.Enabled = true;
            // Cho phép người dùng thao tác với nút Lưu/Hủy Bỏ và cho phép nhập thông tin vào các textbox
            groupBox1.Enabled = true;
            btnLuu.Enabled = true;
            btnHuyBo.Enabled = true;
            //Không cho thao tác trên các nút Thêm/Sửa/Xóa/Reload/Trở Về
            btnXoa.Enabled = false;
            btnSua.Enabled = false;
            btnReload.Enabled = false;
            btnTroVe.Enabled = false;
            //Tạo biến để lấy dòng đã chọn trên datagridview để tiến hành chỉnh sửa dữ liệu trên dòng đó
            int r = dgvDatBan.CurrentCell.RowIndex;
            //Hiển thị lên textbox
            tbMaKH.Text = dgvDatBan.Rows[r].Cells[0].Value.ToString();
            cbLoaiBan.Text = dgvDatBan.Rows[r].Cells[1].Value.ToString();
            tbNgayDat.Text = dgvDatBan.Rows[r].Cells[2].Value.ToString();
            dtpNgayHetHan.Text = dgvDatBan.Rows[r].Cells[3].Value.ToString();
            dtpBeginTime.Text = dgvDatBan.Rows[r].Cells[4].Value.ToString();
            dtpEndTime.Text = dgvDatBan.Rows[r].Cells[5].Value.ToString();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                //Chọn khách hàng và bàn hợp lí
                var table = context.Tables.Find(Convert.ToInt32(cbLoaiBan.Text.Trim()));
                var customer = context.Customers.Find(Convert.ToInt32(tbMaKH.Text.Trim()));

                if (table != null && customer != null)
                {
                    var bd = context.BookingDetails.Find(Convert.ToInt32(tbMaKH.Text.Trim()),
                                Convert.ToInt32(cbLoaiBan.Text.Trim()));

                    if (bd != null)
                    {
                        bd.TableID = table.TableID;
                        bd.CustomerID = customer.CustomerID;
                        bd.BeginTime = dtpBeginTime.Value.TimeOfDay;
                        bd.EndTime = dtpEndTime.Value.TimeOfDay;

                        if (DateTime.Compare(dtpNgayHetHan.Value.Date, DateTime.Now) < 0)
                        {
                            MessageBox.Show("Hãy chọn ngày ăn hợp lý!!!","Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Information);
                            //return;
                        }
                        else
                            bd.ReservationDate = dtpNgayHetHan.Value.Date;

                        context.SaveChanges();

                        MessageBox.Show("Đã Xong");
                    }
                }
                else
                {
                    if (table == null)
                        MessageBox.Show("Bàn đã nhập không tồn tại","Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    if (customer == null)
                        MessageBox.Show("Khách hàng đã nhập không tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Không thể hoàn thành thao tác", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadData()
        {
            //Cho phép thao tác lên dữ liệu có trong datagridview
            dgvDatBan.Enabled = true;
            //Ngăn chặn người dùng thao tác lên các nút Lưu/Huỷ bỏ và các textbox
            groupBox1.Enabled = false;
            btnLuu.Enabled = false;
            btnHuyBo.Enabled = false;
            //Cho phép người dùng thao tác lên các nút Thêm/Xoá/Sửa/Reset/Trở về
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            btnReload.Enabled = true;
            btnTroVe.Enabled = true;
            //reset lại các textbox
            tbMaKH.ResetText();
            cbLoaiBan.ResetText();
            tbNgayDat.ResetText();
            dtpNgayHetHan.ResetText();

            try
            {
                //Tạo câu truy vấn để lấy dữ liệu
                var query = from bd in context.BookingDetails
                            select new
                            {
                                bd.CustomerID,
                                bd.TableID,
                                bd.BookingDate,
                                bd.ReservationDate,
                                bd.BeginTime,
                                bd.EndTime
                            };

                dgvDatBan.DataSource = query.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Không lấy được dữ liệu từ bảng BookingDetails",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void QuanLyChiTietDatBanForm_Load(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
