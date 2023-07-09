using System;
using System.Windows.Forms;

namespace FinalTermProject
{
    public partial class MainForm : Form
    {
        private bool isAdmin = false, isLoggedIn = false;
        private EntityClass.User user = new EntityClass.User();
        private Context context = new Context();

        public MainForm()
        {
            InitializeComponent();
        }

        #region Xem Danh Mục
        private void xemDanhMuc(int danhMuc)
        {
            // Mở form Xem danh mục dựa vào tham số danhMuc
            DanhMucForm frm = new DanhMucForm(danhMuc);
            frm.ShowDialog();
        }
        private void nhânViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            xemDanhMuc(1);
        }

        private void kháchHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            xemDanhMuc(2);
        }

        private void sảnPhẩmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            xemDanhMuc(3);
        }

        private void hóaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            xemDanhMuc(4);
        }

        private void bànToolStripMenuItem_Click(object sender, EventArgs e)
        {
            xemDanhMuc(6);
        }

        private void chiTiếtĐặtBànToolStripMenuItem_Click(object sender, EventArgs e)
        {
            xemDanhMuc(7);
        }

        #endregion


        private void Login()
        {
            // Mở form Đăng nhập
            LoginForm frm = new LoginForm();
            frm.ShowDialog();
            this.isAdmin = frm.IsAdmin;
            this.isLoggedIn = frm.IsLoggedIn;
            // Kiểm tra xem đã đăng nhập thành công chưa
            if (isLoggedIn)
            {
                // Cho phép sử dụng một số chức năng
                chứcNăngToolStripMenuItem.Enabled = true;
                xemDanhMụcToolStripMenuItem.Enabled = true;
                nhânViênToolStripMenuItem.Enabled = false;
                quảnLýDanhMụcToolStripMenuItem.Enabled = true;
                danhMụcNhânViênToolStripMenuItem.Enabled = false;
                đổiMậtKhẩuToolStripMenuItem.Enabled = true;
                toolStripStatusLabel1.Text = "Đã đăng nhập với vai trò: Nhân Viên";

                // Nếu là quản lý thì cho sử dụng full chức năng
                if (isAdmin)
                {
                    nhânViênToolStripMenuItem.Enabled = true;
                    danhMụcNhânViênToolStripMenuItem.Enabled = true;
                    quảnLíNgườiDùngToolStripMenuItem.Enabled = true;
                    toolStripStatusLabel1.Text = "Đã đăng nhập với vai trò: Quản Lý";
                }
                // Nếu là nhân viên thì hạn chế một số chức năng
                else
                {
                    nhânViênToolStripMenuItem.Enabled = false;
                    danhMụcNhânViênToolStripMenuItem.Enabled = false;
                    quảnLíNgườiDùngToolStripMenuItem.Enabled = false;
                }

                this.user = frm.User;
            }
            // Nếu đăng nhập không thành công thì vô hiệu các chức năng chính
            else
            {
                chứcNăngToolStripMenuItem.Enabled = false;
                xemDanhMụcToolStripMenuItem.Enabled = false;
                quảnLýDanhMụcToolStripMenuItem.Enabled = false;
                đổiMậtKhẩuToolStripMenuItem.Enabled = false;
            }
        }
        private void đăngNhậpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Yêu cầu đăng nhập
            Login();
        }

        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Xác nhận người dùng có muốn thoái chương trình không
            DialogResult result;
            result = MessageBox.Show("Bạn muốn thoát chương trình?", "Thoát Chương Trình", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
                Application.Exit();
        }

        private void danhMụcNhânViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Mở form Quản lý nhân viên
            QuanLyNhanVienForm frm = new QuanLyNhanVienForm(context);
            frm.ShowDialog();
        }

        private void danhMụcSảnPhẩmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Mở form Quản lý sản phẩm
            QuanLySanPhamForm frm = new QuanLySanPhamForm(context);
            frm.ShowDialog();
        }

        private void đặtĐơnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Mở form Đặt đơn
            DatDonForm frm = new DatDonForm(context);
            frm.ShowDialog();
        }

        private void danhMụcKháchHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Mở form Quản lý khách hàng
            QuanLyKhachHangForm frm = new QuanLyKhachHangForm(context);
            frm.ShowDialog();
        }

        private void danhMụcLoạiSảnPhẩmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Mở form Quản lý loại sản phẩm
            QuanLyLoaiSanPhamForm frm = new QuanLyLoaiSanPhamForm(context);
            frm.ShowDialog();
        }

        private void danhMụcBànToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Mở form Quản lý bàn
            QuanLyBanForm frm = new QuanLyBanForm(context);
            frm.ShowDialog();
        }

        private void quảnLíNgườiDùngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Mở form Quản lý người dùng
            QuanLyNguoiDungForm frm = new QuanLyNguoiDungForm(context);
            frm.ShowDialog();
        }

        private void đặtBànToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Mở form Đặt bàn
            DatBanForm frm = new DatBanForm(context);
            frm.ShowDialog();
        }

        private void danhMụcHóaĐơnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Mở form Quản lý hóa đơn
            QuanLyHoaDonForm frm = new QuanLyHoaDonForm(context);
            frm.ShowDialog();
        }

        private void danhMụcChiTiếtĐặtBànToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Mở form Quản lý chi tiết đặt bàn
            QuanLyChiTietDatBanForm frm = new QuanLyChiTietDatBanForm(context);
            frm.ShowDialog();
        }

        private void hướngDẫnSửDụngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Mở form Hướng dẫn sử dụng
            HuongDanSuDungForm frm = new HuongDanSuDungForm();
            frm.ShowDialog();
        }

        private void tácGỉaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Mở form Tác giả
            TacGiaForm frm = new TacGiaForm();
            frm.ShowDialog();
        }

        private void bảngXếpHạngNhânViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Mở form Bảng xếp hạng nhân viên
            BangXepHangNhanVienForm frm = new BangXepHangNhanVienForm(context);
            frm.ShowDialog();
        }

        private void doanhThuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Mở form doanh thu
            DoanhThuForm frm = new DoanhThuForm(context);
            frm.ShowDialog();
        }

        private void đổiMậtKhẩuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Mở form đổi mật khẩu
            DoiMatKhauForm frm = new DoiMatKhauForm(context, user);
            // Cập nhật lại user sau khi đổi mật khẩu
            user = frm.User;
            frm.ShowDialog();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Vô hiệu hóa một số mục trên ToolStripMenu
            quảnLíNgườiDùngToolStripMenuItem.Enabled = false;
            xemDanhMụcToolStripMenuItem.Enabled = false;
            quảnLýDanhMụcToolStripMenuItem.Enabled = false;
            chứcNăngToolStripMenuItem.Enabled = false;
            đổiMậtKhẩuToolStripMenuItem.Enabled = false;
            toolStripStatusLabel1.Text = "";

            // Yêu cầu đăng nhập
            Login();
        }
    }
}
