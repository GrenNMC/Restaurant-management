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
    public partial class QuanLyNguoiDungForm : Form
    {
        private Context context;

        public QuanLyNguoiDungForm()
        {
            InitializeComponent();
            context = new Context();
        }

        public QuanLyNguoiDungForm(Context context)
        {
            InitializeComponent();
            this.context = context;
        }

        private void LoadData()
        {
            try
            {
                //Truy vấn để lấy dữ liệu người dùng
                var query = from user in context.Users
                            select new
                            {
                                user.UserName,
                                user.IsManager,
                                user.CreatedDate,
                                user.LastedLoginDate,
                            };
                //Đem dữ liệu vừa lấy hiển thị ra datagridview
                dgvNguoiDung.DataSource = query.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Không lấy được dữ liệu người dùng",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (tbUser.Text == "")
                MessageBox.Show("Hãy Chọn Người Dùng Muốn Xóa!!!!","Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Information);
            else
            {
                DialogResult dialog;
                dialog = MessageBox.Show("Bạn có chắc muốn xóa người dùng: " + tbUser.Text, "Lưu Ý", MessageBoxButtons.YesNo,MessageBoxIcon.Question);

                if (dialog == DialogResult.OK)
                {
                    try
                    {
                        //Sử dụng lambda query để truy vấn lấy ra dòng dữ liệu cần xoá
                        var user = context.Users.Where(x => x.UserName == tbUser.Text).FirstOrDefault();

                        if(user != null)
                        {
                            //Xoá dữ liệu
                            context.Users.Remove(user);
                            context.SaveChanges();//lưu thay đổi
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

        private void QuanLyNguoiDungForm_Load(object sender, EventArgs e)
        {
            //Load dữ liệu
            LoadData();
        }

        private void dgvNguoiDung_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Thứ tự dòng vừa click trên DataGridView
            int r = dgvNguoiDung.CurrentCell.RowIndex;

            // Chuyển thông tin lên các textbox
            tbUser.Text = dgvNguoiDung.Rows[r].Cells[0].Value.ToString();
            tbRole.Text = dgvNguoiDung.Rows[r].Cells[1].Value.ToString();
        }
    }
}
