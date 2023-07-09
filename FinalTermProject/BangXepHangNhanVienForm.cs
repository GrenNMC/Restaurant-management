using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace FinalTermProject
{
    public partial class BangXepHangNhanVienForm : Form
    {
        private Context context;

        private void btnLoc_Click(object sender, EventArgs e)
        {
            var query = from o in context.Orders
                        join od in context.OrderDetails on o.OrderID equals od.OrderID
                        join emp in context.Employees on o.Employee.EmployeeID equals emp.EmployeeID
                        group o by new
                        {
                            Year = o.OrderDate.Value.Year,
                            Month = o.OrderDate.Value.Month,
                            Employee = emp.LastName + " " + emp.FirstName
                        } into gb 
                        orderby gb.Sum(x => x.Total) descending
                        select new
                        {
                            Date = "Tháng " + gb.Key.Month + ", " + gb.Key.Year,
                            Employee = gb.Key.Employee,
                            Income = gb.Sum(x => x.Total) + " VNĐ"
                        };

            dgvLeader.DataSource = query.Where(x => x.Date == "Tháng " + cbThang.Text + ", " + cbNam.Text).ToList();
            dgvLeader.Columns[0].Visible = false;
        }

        private void LoadData()
        {
            var year = from o in context.Orders
                       select new
                       {
                           Year = o.OrderDate.Value.Year
                       };
            cbNam.DataSource = year.Distinct().ToList();
            cbNam.DisplayMember = "Year";

            var month = from o in context.Orders
                        select new
                        {
                            Month = o.OrderDate.Value.Month
                        };
            cbThang.DataSource = month.Distinct().ToList();
            cbThang.DisplayMember = "Month";
        }

        public BangXepHangNhanVienForm()
        {
            InitializeComponent();
            context = new Context();
            LoadData();
        }

        public BangXepHangNhanVienForm(Context context)
        {
            InitializeComponent();
            this.context = context;
            LoadData();
        }

    }
}
