using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace FinalTermProject
{
    public partial class DatBanForm : Form
    {
        private Context context;

        public DatBanForm()
        {
            InitializeComponent();
            context = new Context();
        }

        public DatBanForm(Context context)
        {
            InitializeComponent();
            this.context = context;
        }

        private void DatBanForm_Load(object sender, EventArgs e)
        {
        }

        private void btnDatBan_Click(object sender, EventArgs e)
        {
            // Nếu người dùng đã chọn bàn đề đặt
            if (cbBan.SelectedIndex != -1)
            {
                try
                {
                    // Tìm khách hàng có SĐT vừa nhập
                    EntityClass.Customer cust = context.Customers.Where(c => c.Phone == tbSDT.Text.Trim()).FirstOrDefault();

                    // Nếu không tìm thấy tiến hành thêm khách hàng mới với thông tin đã nhập vào CSDL
                    if (cust == null)
                    {
                        var customer = new EntityClass.Customer
                        {
                            ContactName = tbTenKH.Text.Trim(),
                            Nationality = tbQuocTich.Text.Trim(),
                            Phone = tbSDT.Text.Trim()
                        };

                        context.Customers.Add(customer);
                        context.SaveChanges();

                        cust = customer;
                    }

                    // Thêm thông tin đặt bàn vào CSDL
                    var bd = new EntityClass.BookingDetail
                    {
                        Customer = cust,
                        Table = context.Tables.Find(Convert.ToInt32(cbBan.SelectedValue)),
                        ReservationDate = dtpNgayAn.Value.Date,
                        BeginTime = dtpBeginTime.Value.TimeOfDay,
                        EndTime = dtpEndTime.Value.TimeOfDay,
                        BookingDate = DateTime.Now
                    };

                    context.BookingDetails.Add(bd);
                    context.SaveChanges();

                    MessageBox.Show("Đã đặt bàn thành công");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Không thể hoàn thành thao tác");
                }

                btnReset_Click(sender, e);
            }
            // Nếu người dùng chưa chọn bàn thì nhắc nhở
            else
                if (cbBan.SelectedIndex == -1)
                lblStatus4.Visible = true;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            // Cho phép thao tác trên groupbox chứa thông tin khách hàng
            groupBox1.Enabled = true;

            // Ẩn các label cảnh báo
            lblStatus1.Visible = false;
            lblStatus2.Visible = false;
            lblStatus3.Visible = false;
            lblStatus4.Visible = false;

            // Xóa nội dung trong các textbox
            tbTenKH.ResetText();
            tbQuocTich.ResetText();
            dtpNgayAn.ResetText();
            tbSDT.ResetText();

            // Không cho chọn bàn
            cbBan.Enabled = false;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            // Ẩn các label cảnh báo
            lblStatus1.Visible = false;
            lblStatus2.Visible = false;
            lblStatus3.Visible = false;
            lblStatus4.Visible = false;
            lblStatus5.Visible = false;

            // Nếu người dùng đã nhập đủ thông tin
            if (tbTenKH.Text != "" && tbSDT.Text != "" && dtpNgayAn.Text != "" && dtpBeginTime.Text != "" && dtpEndTime.Text != "")
            {
                // Kiểm tra Ngày ăn có hợp lệ không nếu không hiện cảnh báo
                if (dtpNgayAn.Value.Date < DateTime.Now.Date)
                {
                    lblStatus2.Visible = true;
                    return;
                }

                // Kiểm tra giờ ăn có hợp lệ không nếu không hiện cảnh báo
                if (dtpBeginTime.Value.TimeOfDay.TotalSeconds >= dtpEndTime.Value.TimeOfDay.TotalSeconds)
                {
                    lblStatus5.Visible = true;
                    return;
                }

                // Cho phép người dùng chọn bàn
                cbBan.Enabled = true;

                try
                {
                    // Truy vấn tả về toàn bộ bàn trong bảng Tables
                    var tables = from t in context.Tables
                                 select new
                                 {
                                     t.TableID,
                                     t.TableName
                                 };

                    // Truy vấn trả về những bàn đã có người đặt trong khoảng thời gian người dùng cung cấp
                    var reservationAtThisTime = from bd in context.BookingDetails
                                                where bd.ReservationDate == dtpNgayAn.Value.Date
                                                      && ((bd.BeginTime > dtpBeginTime.Value.TimeOfDay
                                                           && bd.EndTime < dtpEndTime.Value.TimeOfDay)
                                                           || bd.BeginTime < dtpEndTime.Value.TimeOfDay)
                                                select new
                                                {
                                                    bd.TableID,
                                                    bd.Table.TableName
                                                };

                    // Dùng phép hiệu 2 danh sách để ra những bàn khả dụng tại thời điểm khách ăn
                    var query = tables.Except(reservationAtThisTime);

                    // Đưa dữ liệu có được từ truy vấn lên combobox
                    cbBan.DataSource = query.ToList();
                    cbBan.ValueMember = "TableID";
                    cbBan.DisplayMember = "TableName";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Không lấy được dữ liệu từ bảng Tables");
                }

                groupBox1.Enabled = false;
            }
            // Nếu người dùng nhập thiếu thông tin thì nhắc nhở
            else
            {
                if (tbTenKH.Text == "")
                    lblStatus1.Visible = true;
                if (dtpNgayAn.Text == "")
                    lblStatus2.Visible = true;
                if (tbSDT.Text == "")
                    lblStatus3.Visible = true;
            }
        }

        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
