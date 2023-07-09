using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;

namespace FinalTermProject
{
    public partial class QuanLyNhanVienForm : Form
    {
        Context context;
        private int isInserting;

        public QuanLyNhanVienForm()
        {
            InitializeComponent();
            context = new Context();
        }

        public QuanLyNhanVienForm(Context context)
        {
            InitializeComponent();
            this.context = context;
        }

        private void LoadData()
        {
            //Cho người dùng thao tác lên datagridview thi bắt đầu load dữ liệu
            dgvNhanVien.Enabled = true;

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
            tbFirstName.ResetText();
            tbLastName.ResetText();
            tbPhoneNumber.ResetText();
            tbMaNV.ResetText();
            tbAddress.ResetText();
            tbNationality.ResetText();
            tbSalary.ResetText();
            dtpBirthDate.ResetText();
            dtpHireDate.ResetText();

            try
            {
                //Tạo câu truy vấn để lấy dữ liệu
                var query = from employee in context.Employees
                             select new
                             {
                                 employee.EmployeeID,
                                 employee.LastName,
                                 employee.FirstName,
                                 employee.Gender,
                                 employee.BirthDate,
                                 employee.HireDate,
                                 employee.Address,
                                 employee.HomePhone,
                                 employee.Salary,
                                 employee.Nationality
                             };
                //Đem dữ liệu vừa truy vấn lên datadridview
                dgvNhanVien.DataSource = query.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Không lấy được dữ liệu từ bảng Employees",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void QuanLyNhanVienForm_Load(object sender, EventArgs e)
        {
            //Load dữ liệu
            LoadData();
        }

        private void btnTroVe_Click(object sender, EventArgs e)
        {
            //Kết thúc
            this.Close();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            // Khi đã bấm nút thêm thì không cho phép người dùng thao tác trên DataGridView
            dgvNhanVien.Enabled = false;

            // Kích hoạt biến isInserting
            isInserting = 1;

            // Xóa nội dung trong các textbox
            tbFirstName.ResetText();
            tbLastName.ResetText();
            tbPhoneNumber.ResetText();
            tbMaNV.ResetText();
            tbAddress.ResetText();
            tbNationality.ResetText();
            tbSalary.ResetText();
            dtpBirthDate.ResetText();
            dtpHireDate.ResetText();

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

        private void btnLuu_Click(object sender, EventArgs e)
        {
            using (DbContextTransaction transaction = context.Database.BeginTransaction())
            {
                try
                {
                    // Thêm dữ liệu
                    if (isInserting == 1)
                    {
                        var employee = new EntityClass.Employee
                        {
                            FirstName = tbFirstName.Text.Trim(),
                            LastName = tbLastName.Text.Trim(),
                            Address = tbAddress.Text.Trim(),
                            Salary = Convert.ToInt32(tbSalary.Text.Trim()),
                            HomePhone = tbPhoneNumber.Text.Trim(),
                            Nationality = tbNationality.Text.Trim(),
                            Gender = chbGioiTinh.Checked,
                            BirthDate = dtpBirthDate.Value.Date,
                            HireDate = dtpHireDate.Value.Date
                        };

                        context.Employees.Add(employee);
                        context.SaveChanges();
                    }
                    // Sửa dữ liệu
                    else if (isInserting == 0)
                    {
                        var employee = context.Employees.Find(Convert.ToInt32(tbMaNV.Text.Trim()));

                        if (employee != null)
                        {
                            employee.FirstName = tbFirstName.Text.Trim();
                            employee.LastName = tbLastName.Text.Trim();
                            employee.Address = tbAddress.Text.Trim();
                            employee.Salary = Convert.ToInt32(tbSalary.Text.Trim());
                            employee.HomePhone = tbPhoneNumber.Text.Trim();
                            employee.Nationality = tbNationality.Text.Trim();
                            employee.Gender = chbGioiTinh.Checked;
                            employee.BirthDate = dtpBirthDate.Value.Date;
                            employee.HireDate = dtpHireDate.Value.Date;

                            context.SaveChanges();
                        }
                    }

                    if (DateTime.Now.Year - dtpBirthDate.Value.Year < 18)
                        throw new Exception("Under 18 year olds");
                    // Thông báo
                    MessageBox.Show("Đã Xong","Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Information);

                    transaction.Commit();

                    LoadData();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("Lỗi: " + ex.Message, "Không thể hoàn thành thao tác",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }
        }

        private void dgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Thứ tự dòng vừa click trên DataGridView
            int r = dgvNhanVien.CurrentCell.RowIndex;

            // Chuyển thông tin lên các textbox
            tbMaNV.Text = dgvNhanVien.Rows[r].Cells[0].Value.ToString();
            tbLastName.Text = dgvNhanVien.Rows[r].Cells[1].Value.ToString();
            tbFirstName.Text = dgvNhanVien.Rows[r].Cells[2].Value.ToString();
            dtpBirthDate.Text = dgvNhanVien.Rows[r].Cells[4].Value.ToString();
            dtpHireDate.Text = dgvNhanVien.Rows[r].Cells[5].Value.ToString();
            tbAddress.Text = dgvNhanVien.Rows[r].Cells[6].Value.ToString();
            tbPhoneNumber.Text = dgvNhanVien.Rows[r].Cells[7].Value.ToString();
            tbSalary.Text = dgvNhanVien.Rows[r].Cells[8].Value.ToString();
            tbNationality.Text = dgvNhanVien.Rows[r].Cells[9].Value.ToString();
            chbGioiTinh.Checked = (bool)dgvNhanVien.Rows[r].Cells[3].Value;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (tbMaNV.Text == "")
                MessageBox.Show("Hãy Chọn Nhân Viên Muốn Xóa!!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                DialogResult dialog;
                dialog = MessageBox.Show("Bạn có chắc muốn xóa nhân viên có mã: " + tbMaNV.Text, "Lưu Ý", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialog == DialogResult.OK)
                {
                    try
                    {
                        //Tạo truy vấn để lấy dòng dữ liệu cần xoá
                        var employee = context.Employees.Find(Convert.ToInt32(tbMaNV.Text.Trim()));

                        if (employee != null)
                        {
                            //Xoá dữ liệu
                            context.Employees.Remove(employee);
                            context.SaveChanges();// Lưu các thay đổi
                        }

                        LoadData();

                        MessageBox.Show("Đã Xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi: " + ex.Message, "Không xóa được!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            isInserting = 0;
            //Tạo biến để lấy dòng đã chọn trên datagridview để tiến hành chỉnh sửa dữ liệu trên dòng đó
            int r = dgvNhanVien.CurrentCell.RowIndex;
            //Hiển thị lên textbox
            tbMaNV.Text = dgvNhanVien.Rows[r].Cells[0].Value.ToString();
            tbLastName.Text = dgvNhanVien.Rows[r].Cells[1].Value.ToString();
            tbFirstName.Text = dgvNhanVien.Rows[r].Cells[2].Value.ToString();
            dtpBirthDate.Text = dgvNhanVien.Rows[r].Cells[4].Value.ToString();
            dtpHireDate.Text = dgvNhanVien.Rows[r].Cells[5].Value.ToString();
            tbAddress.Text = dgvNhanVien.Rows[r].Cells[6].Value.ToString();
            tbPhoneNumber.Text = dgvNhanVien.Rows[r].Cells[7].Value.ToString();
            tbSalary.Text = dgvNhanVien.Rows[r].Cells[8].Value.ToString();
            tbNationality.Text = dgvNhanVien.Rows[r].Cells[9].Value.ToString();
            chbGioiTinh.Checked = (bool)dgvNhanVien.Rows[r].Cells[3].Value;
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

        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            //Cho người dùng thao tác trở lại lên datagridview khi ấn vào nút huỷ bỏ
            dgvNhanVien.Enabled = true;

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
            tbFirstName.ResetText();
            tbLastName.ResetText();
            tbPhoneNumber.ResetText();
            tbMaNV.ResetText();
            tbAddress.ResetText();
            tbNationality.ResetText();
            tbSalary.ResetText();
            dtpBirthDate.ResetText();
            dtpHireDate.ResetText();
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            //Load dữ liệu
            LoadData();
        }
    }
}
