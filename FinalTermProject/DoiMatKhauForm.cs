using System;
using System.Windows.Forms;

namespace FinalTermProject
{
    public partial class DoiMatKhauForm : Form
    {
        private Context context;
        private EntityClass.User user;

        public EntityClass.User User { get { return user; } }

        public DoiMatKhauForm(Context context, EntityClass.User user)
        {
            InitializeComponent();
            this.context = context;
            this.user = user;
        }

        private void DoiMatKhauForm_Load(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            // Khi người dùng bấm nút OK thì kiểm tra xem 2 textbox có trống không
            if (tbCurrentPass.Text != "" && tbNewPass.Text != "")
            {
                // Nếu không trống kiểm tra password vừa nhập vào có phải của user đang đăng nhập không
                if (user.Password == tbCurrentPass.Text.Trim())
                {
                    // Nếu đúng thì tiến hành kiểm tra xem mật khẩu mới có trùng với ô nhập lại mật khẩu không
                    if (tbNewPass.Text.Trim() == tbConfirmPass.Text.Trim())
                    {
                        // Nếu đúng tiến hành đổi mật khẩu

                        // Sửa password của user thành password mới
                        user.Password = tbNewPass.Text.Trim();
                        // Chuyển đổi state của user thành modified
                        context.Entry(user).State = System.Data.Entity.EntityState.Modified;

                        context.SaveChanges();

                        MessageBox.Show("Đổi mật khẩu thành công");

                        this.Close();
                    }
                    else
                    {
                        // Nếu mật khẩu mới không trùng với ô nhập lại mật khẩu thì hiện cảnh báo
                        lblStatus2.Visible = true;
                        return;
                    }
                }
                // Nếu người dùng nhập không đúng mật khẩu hiện tại của user thì hiện cảnh báo
                else
                {
                    lblStatus1.Visible = true;
                    return;
                }
            }
            // Nếu người dùng bỏ trống thì nhắc nhở người dùng nhập vào
            else
            {
                if (tbNewPass.Text == "")
                    lblEmpty2.Visible = true;
                if (tbCurrentPass.Text == "")
                    lblEmpty1.Visible = true;
                return;
            }
        }
    }
}
