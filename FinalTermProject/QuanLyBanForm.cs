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
    public partial class QuanLyBanForm : Form
    {

        private int isInserting;
        private Context context;

        public QuanLyBanForm()
        {
            InitializeComponent();
            context = new Context();
        }

        public QuanLyBanForm(Context context)
        {
            InitializeComponent();
            this.context = context;
        }
        //Hàm load dữ liệu khi hiển thị form
        private void LoadData()
        {
            //Cho người dùng thao tác lên datagridview thi bắt đầu load dữ liệu
            dgvBan.Enabled = true;

            //Ngăn chặn người dùng thao tác lên các nút Lưu/Huỷ bỏ và các textbox
            groupBox1.Enabled = false;
            btnLuu.Enabled = false;
            btnHuyBo.Enabled = false;

            //Cho phép người dùng thao tác lên các nút Thêm/Xoá/Sửa/Reset/Trở về
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            btnReload.Enabled = true;
            btnTroVe.Enabled = true;

            //reset lại các textbox
            tbCategoryID.ResetText();
            tbCategoryName.ResetText();
            tbDescription.ResetText();

            try
            {
                //Tạo câu truy vấn để lấy dữ liệu
                var query = from table in context.Tables
                            select new
                            {
                                table.TableID,
                                table.TableName,
                                table.Description

                            };
                //Đem dữ liệu vừa truy vấn lên datadridview
                dgvBan.DataSource = query.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Không lấy được dữ liệu từ bảng Tables",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void QuanLyBanForm_Load(object sender, EventArgs e)
        {
            //Load dữ liệu
            LoadData();
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            //Load dữ liệu
            LoadData();
        }

        private void btnTroVe_Click(object sender, EventArgs e)
        {   //kết thúc
            this.Close();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            // Khi đã bấm nút thêm thì không cho phép người dùng thao tác trên DataGridView
            dgvBan.Enabled = false;

            // Kích hoạt biến isInserting
            isInserting = 1;

            // Xóa nội dung trong các textbox
            tbCategoryID.ResetText();
            tbCategoryName.ResetText();
            tbDescription.ResetText();

            // Cho phép người dùng thao tác với nút Lưu/Hủy Bỏ và cho phép nhập thông tin vào các textbox
            groupBox1.Enabled = true;
            btnLuu.Enabled = true;
            btnHuyBo.Enabled = true;

            //Không cho thao tác trên các nút Thêm/Sửa/Xóa/Reload/Trở Về
            btnThem.Enabled = false;
            btnXoa.Enabled = false;
            btnSua.Enabled = false;
            btnReload.Enabled = false;
            btnTroVe.Enabled = false;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            isInserting = 0;
            //Tạo biến để lấy dòng đã chọn trên datagridview để tiến hành chỉnh sửa dữ liệu trên dòng đó
            int r = dgvBan.CurrentCell.RowIndex;
            //Hiển thị lên textbox
            tbCategoryID.Text = dgvBan.Rows[r].Cells[0].Value.ToString();
            tbCategoryName.Text = dgvBan.Rows[r].Cells[1].Value.ToString();
            if (dgvBan.Rows[r].Cells[2].Value != null)
                tbDescription.Text = dgvBan.Rows[r].Cells[2].Value.ToString();
            else
                tbDescription.Text = "";
            // Cho phép người dùng thao tác với nút Lưu/Hủy Bỏ và cho phép nhập thông tin vào các textbox
            groupBox1.Enabled = true;
            btnLuu.Enabled = true;
            btnHuyBo.Enabled = true;
            //Không cho thao tác trên các nút Thêm/Sửa/Xóa/Reload/Trở Về
            btnThem.Enabled = false;
            btnXoa.Enabled = false;
            btnSua.Enabled = false;
            btnReload.Enabled = false;
            btnTroVe.Enabled = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (tbCategoryID.Text == "")
                MessageBox.Show("Hãy Chọn Bàn Muốn Xóa!!!!","Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Information);
            else
            {
                DialogResult dialog;
                dialog = MessageBox.Show("Bạn có chắc muốn xóa bàn có mã: " + tbCategoryID.Text, "Lưu Ý", MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                if (dialog == DialogResult.OK)
                {
                    //Tạo truy vấn để lấy dòng dữ liệu cần xoá
                    var table = context.Tables.Find(Convert.ToInt32(tbCategoryID.Text));
                    try
                    {
                        if (table != null)
                        {
                            //Xoá dữ liệu
                            context.Tables.Remove(table);
                            context.SaveChanges();// Lưu các thay đổi
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

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                // Thêm dữ liệu
                if (isInserting == 1)
                {
                    var table = new EntityClass.Table
                    {
                        TableName = tbCategoryName.Text.Trim(),
                        Description = tbDescription.Text.Trim()

                    };
                    context.Tables.Add(table);
                    context.SaveChanges();
                }
                // Sửa dữ liệu
                else if (isInserting == 0)
                {
                    var table = context.Tables.Find(Convert.ToInt32(tbCategoryID.Text.Trim()));
                    {
                        table.TableName = tbCategoryName.Text.Trim();
                        table.Description = tbDescription.Text.Trim();
                        context.SaveChanges();
                    }
                }

                // Thông báo
                MessageBox.Show("Đã Xong","Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Information);

                // Load lại dữ liệu vừa thêm/sửa lên DataGridView
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
            dgvBan.Enabled = true;
           
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
            tbCategoryID.ResetText();
            tbCategoryName.ResetText();
            tbDescription.ResetText();
        }

        private void dgvBan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Thứ tự dòng vừa click trên DataGridView
            int r = dgvBan.CurrentCell.RowIndex;

            // Chuyển thông tin lên các textbox
            tbCategoryID.Text = dgvBan.Rows[r].Cells[0].Value.ToString();
            tbCategoryName.Text = dgvBan.Rows[r].Cells[1].Value.ToString();
            if (dgvBan.Rows[r].Cells[2].Value != null)
                tbDescription.Text = dgvBan.Rows[r].Cells[2].Value.ToString();
            else
                tbDescription.Text = "";
        }
    }
}