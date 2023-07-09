using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace FinalTermProject
{
    public partial class LoginForm : Form
    {
        public string UserID, Password;
        private bool isAdmin, isLoggedIn;
        private EntityClass.User user = new EntityClass.User();
        private Context context;

        // Property trả về xem có phải user vừa đăng nhập là quản lý không
        public bool IsAdmin { get { return isAdmin; } }

        // Property trả về user vừa đăng nhập
        public EntityClass.User User { get { return user; } }

        // Property trả về trạng thái đã đăng nhập/chưa đăng nhập
        public bool IsLoggedIn { get { return isLoggedIn; } }

        private void lblTaoTaiKhoan_MouseEnter(object sender, EventArgs e)
        {
            // Nếu chuột di chuyển đến label tạo tài khoản thì đổi label thành màu xanh nhạt
            lblTaoTaiKhoan.ForeColor = Color.LightBlue;
        }

        private void lblTaoTaiKhoan_MouseLeave(object sender, EventArgs e)
        {
            // Nếu chuột di chuyển ra khỏi label tạo tài khoản thì đổi label thành màu xanh
            lblTaoTaiKhoan.ForeColor = Color.Blue;
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            this.AcceptButton = btnLogin;
        }

        private void lblEye_Click(object sender, EventArgs e)
        {
            // Nếu nhấn vào con mắt ở textbox mật khẩu thì sẽ hiện mật khẩu và ngược lại nếu nhấn lần nữa
            if (tbPass.PasswordChar == '•')
                tbPass.PasswordChar = '\0';
            else
                tbPass.PasswordChar = '•';
        }

        private void tbPass_TextChanged(object sender, EventArgs e)
        {
            // Nếu textbox mật khẩu trống thì sẽ không hiện con mắt
            if (tbPass.Text == "")
                lblEye.Visible = false;
            else
                lblEye.Visible = true;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lblTaoTaiKhoan_Click(object sender, EventArgs e)
        {
            TaoTaiKhoanForm frm = new TaoTaiKhoanForm();
            frm.ShowDialog();
        }

        private SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

        public LoginForm()
        {
            InitializeComponent();
            this.context = new Context();
        }
        public LoginForm(Context context)
        {
            InitializeComponent();
            this.context = context;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // Lấy nội dung từ 2 textbox User và Pass gắn vào 2 biến UserID và Password
            UserID = tbUser.Text.Trim();
            Password = tbPass.Text.Trim();

            try
            {
                // Thực hiện truy vấn trả về những user có tên đăng nhập trùng với UserID và mật khẩu trùng với Password
                var query = from user in context.Users
                            where user.UserName == UserID && user.Password == Password
                            select user;

                // Đếm số lượng user trong kết quả trả về của truy vấn query
                int result = query.Count();

                // Nếu user có tồn tại trên CSDL
                if (result > 0)
                {
                    // Kiếm tra xem User đó phải quản lý không
                    result = query.Where(x => x.IsManager == true).Count();

                    // Nếu phải
                    if (result > 0)
                    {
                        isAdmin = true;
                        isLoggedIn = true;

                        // Sửa đổi thuộc tính thời gian đăng nhập cuối cùng của user đó lên CSDL
                        query.First().LastedLoginDate = DateTime.Now;
                        context.SaveChanges();

                        user = query.First();

                        MessageBox.Show("Đăng nhập thành công");
                        this.Close();
                    }
                    else
                    {
                        // Kiếm tra xem User đó phải nhân viên không
                        result = query.Where(x => x.IsManager == false).Count();

                        // Nếu không phải
                        if (result <= 0)
                        {
                            // Thông báo sai tài khoản/mật khẩu
                            this.lblStatus.Visible = true;
                            return;
                        }
                        // Nếu phải
                        else
                        {
                            isAdmin = false;
                            isLoggedIn = true;

                            // Sửa đổi thuộc tính thời gian đăng nhập cuối cùng của user đó lên CSDL
                            query.First().LastedLoginDate = DateTime.Now;
                            context.SaveChanges();

                            user = query.First();

                            MessageBox.Show("Đăng nhập thành công");
                            this.Close();
                        }
                    }
                }
                // Nếu user không tồn tại trên CSDL
                else
                {
                    this.lblStatus.Visible = true;
                    return;
                }
            }
            catch
            {
                this.lblStatus.Visible = true;
                return;
            }
        }
    }
}
