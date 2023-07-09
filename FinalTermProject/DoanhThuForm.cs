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
using System.Globalization;

namespace FinalTermProject
{
    public partial class DoanhThuForm : Form
    {
        private Context context;

        public DoanhThuForm()
        {
            InitializeComponent();
            context = new Context();
        }

        public DoanhThuForm(Context context)
        {
            InitializeComponent();
            this.context = context;
        }

        private void DoanhThuForm_Load(object sender, EventArgs e)
        {
            // Thực hiện truy vấn để tìm tổng tiền kiếm được từ hóa đơn của từng tháng - năm
            var query = from order in context.Orders
                        group order by new
                        {
                            Year = order.OrderDate.Value.Year,
                            Month = order.OrderDate.Value.Month
                        } into gb
                        select new
                        {
                            Date = "Tháng " + gb.Key.Month + ", " + gb.Key.Year,
                            Income = gb.Sum(x => x.Total) + " VNĐ"
                        };

            // Đưa dữ liệu lên DataGridView
            dgvDoanhThu.DataSource = query.ToList();
        }
    }
}
