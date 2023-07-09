using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace FinalTermProject
{
    public partial class TaoTaiKhoanForm : Form
    {
        private Context context;

        public TaoTaiKhoanForm()
        {
            InitializeComponent();
            this.context = new Context();
        }

        public TaoTaiKhoanForm(Context context)
        {
            InitializeComponent();
            this.context = context;
        }

        private void TaoTaiKhoanForm_Load(object sender, EventArgs e)
        {

        }

        //form đăng ký
        private void btnDangKy_Click(object sender, EventArgs e)
        {
            //Kiểm tra trong các textbox có trống không nếu có hiện các label cảnh báo
            if (tbLGNAME.Text == "")
            {
                lblBoTrong1.Visible = true;
                return;
            }
            if (tbPass.Text == "")
            {
                lblBoTrong2.Visible = true;
                return;
            }
            if (cbRole.Text == "")
            {
                lblBoTrong3.Visible = true;
                return;
            }
            if (tbPass.Text != tbConfirmPass.Text)
            {
                lblStatus2.Visible = true;
                tbPass.ResetText();
                tbConfirmPass.ResetText();
                cbRole.SelectedIndex = -1;
                return;
            }

            try
            {
                // Truy vấn tả về những user trong bảng Users có tên đăng nhập trùng với tên đăng nhập do người dùng
                // nhập vào textbox
                var query = from user in context.Users
                            where user.UserName == tbLGNAME.Text.Trim()
                            select user;

                // Đếm số user có tên đăng nhập trùng với tên đăng nhập do người dùng nhập vào textbox
                int result = query.Count();

                // Nếu có tồn tại user trùng tên đăng nhập thì hiển thị các label cảnh báo và reset lại các textbox
                if (result > 0)
                {
                    MessageBox.Show("Tên tài khoản đã được lấy","Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Information);

                    tbLGNAME.ResetText();
                    tbPass.ResetText();
                    tbConfirmPass.ResetText();
                    cbRole.SelectedIndex = -1;

                    lblBoTrong1.Visible = lblBoTrong2.Visible =
                        lblBoTrong3.Visible = lblStatus2.Visible = false;

                    return;
                }

                // Tạo một user có tên đăng nhập và password do người dùng nhập
                EntityClass.User newUser = new EntityClass.User
                {
                    UserName = tbLGNAME.Text.Trim(),
                    Password = tbPass.Text.Trim(),
                    CreatedDate = DateTime.Now,
                };

                // Nếu người dùng chọn vai trò quản lý
                if (cbRole.SelectedItem == "QUANLY")
                {
                    // Mở form AdminKeyForm để yêu người dùng nhập mã đăng ký
                    AdminKeyForm frm = new AdminKeyForm();
                    frm.ShowDialog();
                    // Nếu người dùng nhập đúng mã đăng ký thì tiến hành add user có vai trò quản lý vào CSDL
                    if (frm.IsTrue)
                    {
                        newUser.IsManager = true;
                        context.Users.Add(newUser);
                        context.SaveChanges();
                    }
                    else
                    {
                        MessageBox.Show("Mã Admin Sai!!!!");
                        return;
                    }
                }
                // Nếu người dùng chọn vai trò nhân viên thì tiến hành add user có vai trò nhân viên vào CSDL
                else
                {
                    newUser.IsManager = false;
                    context.Users.Add(newUser);
                    context.SaveChanges();
                }

                // Thông báo tạo tài khoản thành công
                MessageBox.Show("Tạo tài khoản thành công");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Không thể đăng ký");
            }
        }

        private void btnTroVe_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
