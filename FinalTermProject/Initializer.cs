using System;
using System.Data.Entity;

namespace FinalTermProject
{
    public class Initializer<T> : DropCreateDatabaseIfModelChanges<Context>
    {
        protected override void Seed(Context context)
        {
            #region Tables

            EntityClass.Table t1 = new EntityClass.Table
            {
                TableName = "Common 1"
            };
            EntityClass.Table t2 = new EntityClass.Table
            {
                TableName = "Common 2"
            };
            EntityClass.Table t3 = new EntityClass.Table
            {
                TableName = "Common 3"
            };
            EntityClass.Table t4 = new EntityClass.Table
            {
                TableName = "Common 4"
            };
            EntityClass.Table t5 = new EntityClass.Table
            {
                TableName = "Common 5"
            };
            EntityClass.Table t6 = new EntityClass.Table
            {
                TableName = "Common 6"
            };
            EntityClass.Table t7 = new EntityClass.Table
            {
                TableName = "Common 7"
            };
            EntityClass.Table t8 = new EntityClass.Table
            {
                TableName = "Common 8"
            };
            EntityClass.Table t9 = new EntityClass.Table
            {
                TableName = "Common 9"
            };
            EntityClass.Table t10 = new EntityClass.Table
            {
                TableName = "Common 10"
            };
            EntityClass.Table t11 = new EntityClass.Table
            {
                TableName = "VIP 1",
                Description = "View đẹp tầng trệt"
            };
            EntityClass.Table t12 = new EntityClass.Table
            {
                TableName = "VIP 2",
                Description = "Yên tĩnh, lãng mạn tầng trệt"
            };
            EntityClass.Table t13 = new EntityClass.Table
            {
                TableName = "VIP 3",
                Description = "View đẹp tầng 1"
            };
            EntityClass.Table t14 = new EntityClass.Table
            {
                TableName = "VIP 4",
                Description = "View đẹp tầng 2"
            };
            EntityClass.Table t15 = new EntityClass.Table
            {
                TableName = "VIP 5",
                Description = "Sang trọng tầng trệt"
            };

            context.Tables.Add(t1);
            context.Tables.Add(t2);
            context.Tables.Add(t3);
            context.Tables.Add(t4);
            context.Tables.Add(t5);
            context.Tables.Add(t6);
            context.Tables.Add(t7);
            context.Tables.Add(t8);
            context.Tables.Add(t9);
            context.Tables.Add(t10);
            context.Tables.Add(t11);
            context.Tables.Add(t12);
            context.Tables.Add(t13);
            context.Tables.Add(t14);
            context.Tables.Add(t15);

            #endregion

            #region Employee

            EntityClass.Employee emp1 = new EntityClass.Employee
            {
                LastName = "Bùi",
                FirstName = "Huy",
                BirthDate = new DateTime(year: 2002, month: 1, day: 27),
                HireDate = new DateTime(year: 2020, month: 6, day: 1),
                Address = "Vincom Quận 9",
                HomePhone = "0654654545",
                Nationality = "Việt Nam",
                Gender = true,
                Salary = 50000000
            };
            EntityClass.Employee emp2 = new EntityClass.Employee
            {
                LastName = "Nguyễn",
                FirstName = "Nhựt",
                BirthDate = new DateTime(year: 2002, month: 5, day: 17),
                HireDate = new DateTime(year: 2021, month: 4, day: 29),
                Address = "Vincom Thủ Đức",
                HomePhone = "0322344565",
                Nationality = "Việt Nam",
                Gender = true,
                Salary = 50000000
            };
            EntityClass.Employee emp3 = new EntityClass.Employee
            {
                LastName = "Nguyễn",
                FirstName = "Cường",
                BirthDate = new DateTime(year: 2002, month: 6, day: 12),
                HireDate = new DateTime(year: 2021, month: 4, day: 25),
                Address = "Vincom Quận 7",
                HomePhone = "0623456366",
                Nationality = "Việt Nam",
                Gender = true,
                Salary = 50000000
            };
            EntityClass.Employee emp4 = new EntityClass.Employee
            {
                LastName = "Salah",
                FirstName = "Mohamed",
                BirthDate = new DateTime(year: 1992, month: 6, day: 15),
                HireDate = new DateTime(year: 2017, month: 6, day: 22),
                Address = "205 Lê Văn Việt",
                HomePhone = "0879516546",
                Nationality = "Ai Cập",
                Gender = true,
                Salary = 1200000
            };
            EntityClass.Employee emp5 = new EntityClass.Employee
            {
                LastName = "Klose",
                FirstName = "Josef",
                BirthDate = new DateTime(year: 1978, month: 7, day: 9),
                HireDate = new DateTime(year: 2016, month: 5, day: 15),
                Address = "3/7 Hoàng Diệu 2",
                HomePhone = "0345346775",
                Nationality = "Đức",
                Gender = true,
                Salary = 2100000
            };
            EntityClass.Employee emp6 = new EntityClass.Employee
            {
                LastName = "Bành",
                FirstName = "Tèo",
                BirthDate = new DateTime(year: 2001, month: 3, day: 21),
                HireDate = new DateTime(year: 2019, month: 5, day: 8),
                Address = "Vincom Quận 1",
                HomePhone = "0155445636",
                Nationality = "Việt Nam",
                Gender = true,
                Salary = 2000000
            };
            EntityClass.Employee emp7 = new EntityClass.Employee
            {
                LastName = "Lewandowski",
                FirstName = "Robert",
                BirthDate = new DateTime(year: 1988, month: 8, day: 21),
                HireDate = new DateTime(year: 2014, month: 7, day: 9),
                Address = "1 Võ Văn Ngân",
                HomePhone = "0324223523",
                Nationality = "Ba Lan",
                Gender = true,
                Salary = 23000000
            };
            EntityClass.Employee emp8 = new EntityClass.Employee
            {
                LastName = "Duy",
                FirstName = "Nguyễn",
                BirthDate = new DateTime(year: 2002, month: 2, day: 6),
                HireDate = new DateTime(year: 2020, month: 4, day: 2),
                Address = "Vincom Quận 7",
                HomePhone = "0223544571",
                Nationality = "Việt Nam",
                Gender = false,
                Salary = 4000000
            };
            EntityClass.Employee emp9 = new EntityClass.Employee
            {
                LastName = "Na",
                FirstName = "Cà",
                BirthDate = new DateTime(year: 2000, month: 2, day: 29),
                HireDate = new DateTime(year: 2022, month: 2, day: 28),
                Address = "Ngã Tư Thủ Đức",
                HomePhone = "0112432245",
                Nationality = "Trung Quốc",
                Gender = false,
                Salary = 1000000
            };
            EntityClass.Employee emp10 = new EntityClass.Employee
            {
                LastName = "Puyol",
                FirstName = "Carles",
                BirthDate = new DateTime(year: 1978, month: 4, day: 13),
                HireDate = new DateTime(year: 2014, month: 3, day: 2),
                Address = "7 Kha Vạn Cân",
                HomePhone = "0234234231",
                Nationality = "Tây Ban Nha",
                Gender = true,
                Salary = 4500000
            };

            context.Employees.Add(emp1);
            context.Employees.Add(emp2);
            context.Employees.Add(emp3);
            context.Employees.Add(emp4);
            context.Employees.Add(emp5);
            context.Employees.Add(emp6);
            context.Employees.Add(emp7);
            context.Employees.Add(emp8);
            context.Employees.Add(emp9);
            context.Employees.Add(emp10);


            #endregion

            #region Product

            #region Appetizer

            EntityClass.Product a1 = new EntityClass.Product
            {
                ProductName = "Súp hải sản",
                UnitPrice = 189000,
                UnitsInStock = 40
            };
            context.Products.Add(a1);

            EntityClass.Product a2 = new EntityClass.Product
            {
                ProductName = "Súp măng tây",
                UnitPrice = 120000,
                UnitsInStock = 25
            };
            context.Products.Add(a2);

            EntityClass.Product a3 = new EntityClass.Product
            {
                ProductName = "Baguette",
                UnitPrice = 20000,
                UnitsInStock = 50
            };
            context.Products.Add(a3);

            EntityClass.Product a4 = new EntityClass.Product
            {
                ProductName = "Cobb salad",
                UnitPrice = 80000,
                UnitsInStock = 30
            };
            context.Products.Add(a4);

            EntityClass.Product a5 = new EntityClass.Product
            {
                ProductName = "Salad đậu đen Mexico",
                UnitPrice = 130000,
                UnitsInStock = 25
            };
            context.Products.Add(a5);

            #endregion

            #region Main Course

            EntityClass.Product mc1 = new EntityClass.Product
            {
                ProductName = "Ức vịt sốt vang đỏ",
                UnitPrice = 335000,
                UnitsInStock = 20
            };
            context.Products.Add(mc1);

            EntityClass.Product mc2 = new EntityClass.Product
            {
                ProductName = "Sường heo nướng BBC",
                UnitPrice = 365000,
                UnitsInStock = 20
            };
            context.Products.Add(mc2);

            EntityClass.Product mc3 = new EntityClass.Product
            {
                ProductName = "Bò beefsteak sốt tiêu",
                UnitPrice = 450000,
                UnitsInStock = 25
            };
            context.Products.Add(mc3);

            EntityClass.Product mc4 = new EntityClass.Product
            {
                ProductName = "Cá hồi sốt mật ong",
                UnitPrice = 460000,
                UnitsInStock = 20
            };
            context.Products.Add(mc4);

            EntityClass.Product mc5 = new EntityClass.Product
            {
                ProductName = "Cá hồi sốt Teryaki",
                UnitPrice = 400000,
                UnitsInStock = 15
            };
            context.Products.Add(mc5);

            EntityClass.Product mc6 = new EntityClass.Product
            {
                ProductName = "Tôm hùm sốt rượu Cognac",
                UnitPrice = 865000,
                UnitsInStock = 10
            };
            context.Products.Add(mc6);

            EntityClass.Product mc7 = new EntityClass.Product
            {
                ProductName = "Sườn cừu nướng rượu rum – đậu lentils",
                UnitPrice = 750000,
                UnitsInStock = 10
            };
            context.Products.Add(mc7);

            EntityClass.Product mc8 = new EntityClass.Product
            {
                ProductName = "Gà rán đút lò",
                UnitPrice = 275000,
                UnitsInStock = 20
            };
            context.Products.Add(mc8);

            EntityClass.Product mc9 = new EntityClass.Product
            {
                ProductName = "Ức gà nhồi jambon phô mai",
                UnitPrice = 265000,
                UnitsInStock = 15
            };
            context.Products.Add(mc9);

            EntityClass.Product mc10 = new EntityClass.Product
            {
                ProductName = "Cá lóc dồn thịt hấp bầu",
                UnitPrice = 525000,
                UnitsInStock = 10
            };
            context.Products.Add(mc10);

            #endregion

            #region Vegetable

            EntityClass.Product v1 = new EntityClass.Product
            {
                ProductName = "Combo giá đỗ - cà rốt - củ cải",
                UnitPrice = 65000,
                UnitsInStock = 20
            };
            context.Products.Add(v1);

            EntityClass.Product v2 = new EntityClass.Product
            {
                ProductName = "Combo cần tây - dưa leo - măng tây",
                UnitPrice = 65000,
                UnitsInStock = 20
            };
            context.Products.Add(v2);

            EntityClass.Product v3 = new EntityClass.Product
            {
                ProductName = "Combo giá đỗ - cà chua - hành lá",
                UnitPrice = 65000,
                UnitsInStock = 20
            };
            context.Products.Add(v3);

            EntityClass.Product v4 = new EntityClass.Product
            {
                ProductName = "Combo củ dền - súp lơ - cà rốt",
                UnitPrice = 65000,
                UnitsInStock = 20
            };
            context.Products.Add(v4);

            EntityClass.Product v5 = new EntityClass.Product
            {
                ProductName = "Combo đậu hà lan - đậu que - nấm",
                UnitPrice = 65000,
                UnitsInStock = 20
            };
            context.Products.Add(v5);

            #endregion

            #region Dessert

            EntityClass.Product d1 = new EntityClass.Product
            {
                ProductName = "Cocktail",
                UnitPrice = 70000,
                UnitsInStock = 30
            };
            context.Products.Add(d1);

            EntityClass.Product d2 = new EntityClass.Product
            {
                ProductName = "Bánh quy (hộp)",
                UnitPrice = 120000,
                UnitsInStock = 15
            };
            context.Products.Add(d2);

            EntityClass.Product d3 = new EntityClass.Product
            {
                ProductName = "Sữa chua tiệt trùng",
                UnitPrice = 35000,
                UnitsInStock = 50
            };
            context.Products.Add(d3);

            EntityClass.Product d4 = new EntityClass.Product
            {
                ProductName = "Kem tươi",
                UnitPrice = 50000,
                UnitsInStock = 30
            };
            context.Products.Add(d4);

            EntityClass.Product d5 = new EntityClass.Product
            {
                ProductName = "Trái cây (đĩa)",
                UnitPrice = 80000,
                UnitsInStock = 40
            };
            context.Products.Add(d5);

            #endregion

            #region Drink

            EntityClass.Product dr1 = new EntityClass.Product
            {
                ProductName = "Bia Heineken - Lon bạc (thùng)",
                UnitPrice = 450000,
                UnitsInStock = 30
            };
            context.Products.Add(dr1);

            EntityClass.Product dr2 = new EntityClass.Product
            {
                ProductName = "Rượu Chivas Regal 18 Years Gold Signature",
                UnitPrice = 1450000,
                UnitsInStock = 15
            };
            context.Products.Add(dr2);

            EntityClass.Product dr3 = new EntityClass.Product
            {
                ProductName = "Nước giải khát có ga (lon)",
                UnitPrice = 20000,
                UnitsInStock = 70
            };
            context.Products.Add(dr3);

            EntityClass.Product dr4 = new EntityClass.Product
            {
                ProductName = "Rượu Soju Hàn Quốc",
                UnitPrice = 70000,
                UnitsInStock = 30
            };
            context.Products.Add(dr4);

            EntityClass.Product dr5 = new EntityClass.Product
            {
                ProductName = "Nước khoàng (chai)",
                UnitPrice = 15000,
                UnitsInStock = 50
            };
            context.Products.Add(dr5);

            #endregion

            #endregion

            #region Category

            EntityClass.Category c1 = new EntityClass.Category
            {
                CategoryName = "Appetizer",
                Description = "Món khai vị",
            };
            c1.Products.Add(a1);
            c1.Products.Add(a2);
            c1.Products.Add(a3);
            c1.Products.Add(a4);
            c1.Products.Add(a5);
            context.Categories.Add(c1);

            EntityClass.Category c2 = new EntityClass.Category
            {
                CategoryName = "Main Course",
                Description = "Món chính",
            };
            c2.Products.Add(mc1);
            c2.Products.Add(mc2);
            c2.Products.Add(mc3);
            c2.Products.Add(mc4);
            c2.Products.Add(mc5);
            c2.Products.Add(mc6);
            c2.Products.Add(mc7);
            c2.Products.Add(mc8);
            c2.Products.Add(mc9);
            c2.Products.Add(mc10);
            context.Categories.Add(c2);

            EntityClass.Category c3 = new EntityClass.Category
            {
                CategoryName = "Vegetable",
                Description = "Món rau",
            };
            c3.Products.Add(v1);
            c3.Products.Add(v2);
            c3.Products.Add(v3);
            c3.Products.Add(v4);
            c3.Products.Add(v5);
            context.Categories.Add(c3);

            EntityClass.Category c4 = new EntityClass.Category
            {
                CategoryName = "Dessert",
                Description = "Món tráng miệng"
            };
            c4.Products.Add(d1);
            c4.Products.Add(d2);
            c4.Products.Add(d3);
            c4.Products.Add(d4);
            c4.Products.Add(d5);
            context.Categories.Add(c4);

            EntityClass.Category c5 = new EntityClass.Category
            {
                CategoryName = "Drink",
                Description = "Thức uống"
            };
            c5.Products.Add(dr1);
            c5.Products.Add(dr2);
            c5.Products.Add(dr3);
            c5.Products.Add(dr4);
            c5.Products.Add(dr5);
            context.Categories.Add(c5);

            #endregion

            #region Customer

            EntityClass.Customer cust1 = new EntityClass.Customer
            {
                ContactName = "Kaiser",
                Nationality = "Nhật Bản",
                Adress = "Vincom Quận 6",
                City = "Hồ Chí Minh",
                Phone = "0654897878",
                Email = "dnak@outlook.com",
            };
            EntityClass.Customer cust2 = new EntityClass.Customer
            {
                ContactName = "Nguyễn Duy Tân",
                Nationality = "Việt Nam",
                Adress = "6/9 Phạm Văn Đồng",
                City = "Hồ Chí Minh",
                Phone = "0456457328",
                Email = "dtn2703@gmail.com",
            };
            EntityClass.Customer cust3 = new EntityClass.Customer
            {
                ContactName = "Ronaldo De Lima",
                Nationality = "Brazil",
                Phone = "0544897878",
                Email = "ronaldo@yahoo.com",
            };
            EntityClass.Customer cust4 = new EntityClass.Customer
            {
                ContactName = "Antoinne Griezmann",
                Nationality = "Pháp",
                Phone = "0345346878",
                Email = "chicken@yahoo.com.fr",
            };
            EntityClass.Customer cust5 = new EntityClass.Customer
            {
                ContactName = "Bành Văn Đậu",
                Nationality = "Việt Nam",
                Adress = "4/7 Tân Lập 1",
                City = "Hồ Chí Minh",
                Phone = "0435346668",
                Email = "vtg@gmail.com",
            };
            EntityClass.Customer cust6 = new EntityClass.Customer
            {
                ContactName = "Nguyễn Văn Tèo",
                Nationality = "Việt Nam",
                Adress = "Vincom Plaza Quận 3",
                City = "Hồ Chí Minh",
                Phone = "0568679576",
                Email = "ạkssdfg@outlook.com",
            };
            EntityClass.Customer cust7 = new EntityClass.Customer
            {
                ContactName = "くさなぎ きょう",
                Nationality = "Nhật Bản",
                Phone = "0654654238"
            };
            EntityClass.Customer cust8 = new EntityClass.Customer
            {
                ContactName = "Anong Phunsawwat",
                Nationality = "Thái Lan",
                Phone = "0682768317",
                Email = "hfhgf@msnd.com",
            };
            EntityClass.Customer cust9 = new EntityClass.Customer
            {
                ContactName = "ง่วงนอน",
                Nationality = "Thái Lan",
                Adress = "Làng Đại Học",
                City = "Hồ Chí Minh",
                Phone = "0234235212",
            };
            EntityClass.Customer cust10 = new EntityClass.Customer
            {
                ContactName = "Uzumaki Sasuke",
                Nationality = "Nhật Bản",
                Adress = "KTX D2",
                City = "Hồ Chí Minh",
                Phone = "0561613232",
                Email = "sdfsgdfg@gmail.com",
            };
            EntityClass.Customer cust11 = new EntityClass.Customer
            {
                ContactName = "Harry Maguire",
                Nationality = "Anh",
                Phone = "0634534678",
                Email = "tauhai@manunited.com",
            };
            EntityClass.Customer cust12 = new EntityClass.Customer
            {
                ContactName = "Nguyễn Văn Chí Khanh",
                Nationality = "Việt Nam",
                Adress = "Vincom Plaza Quận 7",
                City = "Hồ Chí Minh",
                Phone = "0987323428",
                Email = "sdfsg@yahoo.com",
            };
            EntityClass.Customer cust13 = new EntityClass.Customer
            {
                ContactName = "تعب جدا",
                Nationality = "Ả-rập Xê-út",
                Phone = "0465437878",
            };
            EntityClass.Customer cust14 = new EntityClass.Customer
            {
                ContactName = "Nguyễn Việt Hoàng",
                Nationality = "Việt Nam",
                Adress = "87/14/25 Dân Chủ",
                Phone = "0513137846",
                Email = "sdg235@outlook.com",
            };
            EntityClass.Customer cust15 = new EntityClass.Customer
            {
                ContactName = "Trần Thái Băng",
                Nationality = "Việt Nam",
                City = "Bình Dương",
                Phone = "0345346778",
            };
            EntityClass.Customer cust16 = new EntityClass.Customer
            {
                ContactName = "Võ Trường Giang",
                Nationality = "Việt Nam",
                Adress = "2/7 Tân Lập 2/1",
                City = "Hồ Chí Minh",
                Phone = "0561065462",
                Email = "vgtasdf@navi.vn",
            };
            EntityClass.Customer cust17 = new EntityClass.Customer
            {
                ContactName = "Путин Великий",
                Nationality = "Nga",
                Phone = "0946546548",
            };
            EntityClass.Customer cust18 = new EntityClass.Customer
            {
                ContactName = "Đan Nguyên",
                Nationality = "Mỹ",
                City = "Cali",
                Phone = "0545454618",
            };
            EntityClass.Customer cust19 = new EntityClass.Customer
            {
                ContactName = "Gianluigi Buffon",
                Nationality = "Ý",
                Phone = "0435648789",
                Email = "buffon@46498h.com",
            };
            EntityClass.Customer cust20 = new EntityClass.Customer
            {
                ContactName = "Andrea Pirlo",
                Nationality = "Ý",
                Phone = "0234236668",
                Email = "dshgjf@ook.com",
            };

            context.Customers.Add(cust1);
            context.Customers.Add(cust2);
            context.Customers.Add(cust3);
            context.Customers.Add(cust4);
            context.Customers.Add(cust5);
            context.Customers.Add(cust6);
            context.Customers.Add(cust7);
            context.Customers.Add(cust8);
            context.Customers.Add(cust9);
            context.Customers.Add(cust10);
            context.Customers.Add(cust11);
            context.Customers.Add(cust12);
            context.Customers.Add(cust13);
            context.Customers.Add(cust14);
            context.Customers.Add(cust15);
            context.Customers.Add(cust16);
            context.Customers.Add(cust17);
            context.Customers.Add(cust18);
            context.Customers.Add(cust19);
            context.Customers.Add(cust20);

            #endregion

            #region BookingDetail

            EntityClass.BookingDetail bd1 = new EntityClass.BookingDetail
            {
                Customer = cust8,
                Table = t2,
                BookingDate = new DateTime(year: 2022, month: 6, day: 11),
                ReservationDate = new DateTime(year: 2022, month: 6, day: 14),
                BeginTime = new TimeSpan(hours: 8, minutes: 0, seconds: 0),
                EndTime = new TimeSpan(hours: 10, minutes: 0, seconds: 0)
            };
            EntityClass.BookingDetail bd2 = new EntityClass.BookingDetail
            {
                Customer = cust6,
                Table = t15,
                BookingDate = new DateTime(year: 2022, month: 6, day: 11),
                ReservationDate = new DateTime(year: 2022, month: 6, day: 12),
                BeginTime = new TimeSpan(hours: 18, minutes: 0, seconds: 0),
                EndTime = new TimeSpan(hours: 21, minutes: 0, seconds: 0)
            };
            EntityClass.BookingDetail bd3 = new EntityClass.BookingDetail
            {
                Customer = cust7,
                Table = t8,
                BookingDate = new DateTime(year: 2022, month: 6, day: 11),
                ReservationDate = new DateTime(year: 2022, month: 6, day: 13),
                BeginTime = new TimeSpan(hours: 10, minutes: 0, seconds: 0),
                EndTime = new TimeSpan(hours: 12, minutes: 0, seconds: 0)
            };
            EntityClass.BookingDetail bd4 = new EntityClass.BookingDetail
            {
                Customer = cust7,
                Table = t10,
                BookingDate = new DateTime(year: 2022, month: 6, day: 12),
                ReservationDate = new DateTime(year: 2022, month: 6, day: 18),
                BeginTime = new TimeSpan(hours: 18, minutes: 0, seconds: 0),
                EndTime = new TimeSpan(hours: 21, minutes: 0, seconds: 0)
            };
            EntityClass.BookingDetail bd5 = new EntityClass.BookingDetail
            {
                Customer = cust14,
                Table = t6,
                BookingDate = new DateTime(year: 2022, month: 6, day: 12),
                ReservationDate = new DateTime(year: 2022, month: 6, day: 13),
                BeginTime = new TimeSpan(hours: 15, minutes: 0, seconds: 0),
                EndTime = new TimeSpan(hours: 16, minutes: 0, seconds: 0)
            };
            EntityClass.BookingDetail bd6 = new EntityClass.BookingDetail
            {
                Customer = cust18,
                Table = t2,
                BookingDate = new DateTime(year: 2022, month: 6, day: 12),
                ReservationDate = new DateTime(year: 2022, month: 6, day: 15),
                BeginTime = new TimeSpan(hours: 19, minutes: 0, seconds: 0),
                EndTime = new TimeSpan(hours: 21, minutes: 0, seconds: 0)
            };
            EntityClass.BookingDetail bd7 = new EntityClass.BookingDetail
            {
                Customer = cust19,
                Table = t4,
                BookingDate = new DateTime(year: 2022, month: 6, day: 11),
                ReservationDate = new DateTime(year: 2022, month: 6, day: 17),
                BeginTime = new TimeSpan(hours: 12, minutes: 0, seconds: 0),
                EndTime = new TimeSpan(hours: 14, minutes: 0, seconds: 0)
            };

            context.BookingDetails.Add(bd1);
            context.BookingDetails.Add(bd2);
            context.BookingDetails.Add(bd3);
            context.BookingDetails.Add(bd4);
            context.BookingDetails.Add(bd5);
            context.BookingDetails.Add(bd6);
            context.BookingDetails.Add(bd7);

            #endregion

            #region Order

            EntityClass.Order o1 = new EntityClass.Order
            {
                Customer = cust2,
                Employee = emp1,
                OrderDate = new DateTime(year: 2022, month: 6, day: 12),
                ShipAddress = cust2.Adress,
                ShipCity = cust2.City,
                Total = 0
            };
            EntityClass.Order o2 = new EntityClass.Order
            {
                Customer = cust13,
                Employee = emp1,
                OrderDate = new DateTime(year: 2022, month: 6, day: 11),
                Table = t11,
                Total = 0
            };
            EntityClass.Order o3 = new EntityClass.Order
            {
                Customer = cust15,
                Employee = emp1,
                OrderDate = new DateTime(year: 2022, month: 6, day: 12),
                Table = t3,
                Total = 0
            };
            EntityClass.Order o4 = new EntityClass.Order
            {
                Customer = cust1,
                Employee = emp2,
                OrderDate = new DateTime(year: 2022, month: 6, day: 12),
                ShipAddress = cust1.Adress,
                ShipCity = cust1.City,
                Total = 0
            };
            EntityClass.Order o5 = new EntityClass.Order
            {
                Customer = cust12,
                Employee = emp3,
                OrderDate = new DateTime(year: 2022, month: 6, day: 11),
                Table = t2,
                Total = 0
            };
            EntityClass.Order o6 = new EntityClass.Order
            {
                Customer = cust17,
                Employee = emp4,
                OrderDate = new DateTime(year: 2022, month: 6, day: 12),
                Table = t14,
                Total = 0
            };
            EntityClass.Order o7 = new EntityClass.Order
            {
                Customer = cust20,
                Employee = emp5,
                OrderDate = new DateTime(year: 2022, month: 6, day: 11),
                Table = t14,
                Total = 0
            };
            EntityClass.Order o8 = new EntityClass.Order
            {
                Customer = cust5,
                Employee = emp6,
                OrderDate = new DateTime(year: 2022, month: 6, day: 12),
                ShipAddress = cust5.Adress,
                ShipCity = cust5.City,
                Total = 0
            };
            EntityClass.Order o9 = new EntityClass.Order
            {
                Customer = cust3,
                Employee = emp7,
                OrderDate = new DateTime(year: 2022, month: 6, day: 12),
                Table = t12,
                Total = 0
            };
            EntityClass.Order o10 = new EntityClass.Order
            {
                Customer = cust10,
                Employee = emp8,
                OrderDate = new DateTime(year: 2022, month: 6, day: 11),
                ShipAddress = cust10.Adress,
                ShipCity = cust10.City,
                Total = 0
            };
            EntityClass.Order o11 = new EntityClass.Order()
            {
                Customer = cust16,
                Employee = emp9,
                OrderDate = new DateTime(year: 2022, month: 6, day: 11),
                ShipAddress = cust16.Adress,
                ShipCity = cust16.City,
                Total = 0
            };
            EntityClass.Order o12 = new EntityClass.Order()
            {
                Customer = cust9,
                Employee = emp9,
                OrderDate = new DateTime(year: 2022, month: 6, day: 11),
                ShipAddress = cust9.Adress,
                ShipCity = cust9.City,
                Total = 0
            };
            EntityClass.Order o13 = new EntityClass.Order()
            {
                Customer = cust4,
                Employee = emp10,
                OrderDate = new DateTime(year: 2022, month: 6, day: 12),
                Table = t9,
                Total = 0
            };
            EntityClass.Order o14 = new EntityClass.Order()
            {
                Customer = cust11,
                Employee = emp10,
                OrderDate = new DateTime(year: 2022, month: 6, day: 11),
                Table = t13,
                Total = 0
            };
            EntityClass.Order o15 = new EntityClass.Order()
            {
                Customer = cust11,
                Employee = emp10,
                OrderDate = new DateTime(year: 2022, month: 6, day: 12),
                Table = t3,
                Total = 0
            };

            context.Orders.Add(o1);
            context.Orders.Add(o2);
            context.Orders.Add(o3);
            context.Orders.Add(o4);
            context.Orders.Add(o5);
            context.Orders.Add(o6);
            context.Orders.Add(o7);
            context.Orders.Add(o8);
            context.Orders.Add(o9);
            context.Orders.Add(o10);
            context.Orders.Add(o11);
            context.Orders.Add(o12);
            context.Orders.Add(o13);
            context.Orders.Add(o14);
            context.Orders.Add(o15);

            #endregion

            #region OrderDetail

            #region Order1
            EntityClass.OrderDetail od1_1 = new EntityClass.OrderDetail
            {
                Product = a1,
                Order = o1,
                Quantity = 2,
                Discount = 1
            };
            od1_1.UnitPrice = (int)(od1_1.Quantity * od1_1.Product.UnitPrice * od1_1.Discount);
            o1.Total += od1_1.UnitPrice;

            EntityClass.OrderDetail od1_2 = new EntityClass.OrderDetail
            {
                Product = mc3,
                Order = o1,
                Quantity = 2,
                Discount = 1
            };
            od1_2.UnitPrice = (int)(od1_2.Quantity * od1_2.Product.UnitPrice * od1_2.Discount);
            o1.Total += od1_2.UnitPrice;

            EntityClass.OrderDetail od1_3 = new EntityClass.OrderDetail
            {
                Product = dr2,
                Order = o1,
                Quantity = 1,
                Discount = 1
            };
            od1_3.UnitPrice = (int)(od1_3.Quantity * od1_3.Product.UnitPrice * od1_3.Discount);
            o1.Total += od1_3.UnitPrice;

            o1.OrderDetails.Add(od1_1);
            o1.OrderDetails.Add(od1_2);
            o1.OrderDetails.Add(od1_3);

            #endregion

            #region Order2
            EntityClass.OrderDetail od2_1 = new EntityClass.OrderDetail
            {
                Product = mc1,
                Order = o2,
                Quantity = 1,
                Discount = 1 + 0.2f
            };
            od2_1.UnitPrice = (int)(od2_1.Quantity * od2_1.Product.UnitPrice * od2_1.Discount);
            o2.Total += od2_1.UnitPrice;

            EntityClass.OrderDetail od2_2 = new EntityClass.OrderDetail
            {
                Product = dr5,
                Order = o2,
                Quantity = 1,
                Discount = 1 + 0.2f
            };
            od2_2.UnitPrice = (int)(od2_2.Quantity * od2_2.Product.UnitPrice * od2_2.Discount);
            o2.Total += od2_2.UnitPrice;

            o2.OrderDetails.Add(od2_2);
            o2.OrderDetails.Add(od2_1);

            #endregion

            #region Order3
            EntityClass.OrderDetail od3_1 = new EntityClass.OrderDetail
            {
                Product = a4,
                Order = o3,
                Quantity = 1,
                Discount = 1
            };
            od3_1.UnitPrice = (int)(od3_1.Quantity * od3_1.Product.UnitPrice * od3_1.Discount);
            o3.Total += od3_1.UnitPrice;

            EntityClass.OrderDetail od3_2 = new EntityClass.OrderDetail
            {
                Product = mc2,
                Order = o3,
                Quantity = 1,
                Discount = 1
            };
            od3_2.UnitPrice = (int)(od3_2.Quantity * od3_2.Product.UnitPrice * od3_2.Discount);
            o3.Total += od3_2.UnitPrice;

            EntityClass.OrderDetail od3_3 = new EntityClass.OrderDetail
            {
                Product = d3,
                Order = o3,
                Quantity = 1,
                Discount = 1
            };
            od3_3.UnitPrice = (int)(od3_3.Quantity * od3_3.Product.UnitPrice * od3_3.Discount);
            o3.Total += od3_3.UnitPrice;

            EntityClass.OrderDetail od3_4 = new EntityClass.OrderDetail
            {
                Product = dr2,
                Order = o3,
                Quantity = 1,
                Discount = 1
            };
            od3_4.UnitPrice = (int)(od3_4.Quantity * od3_4.Product.UnitPrice * od3_4.Discount);
            o3.Total += od3_4.UnitPrice;

            o3.OrderDetails.Add(od3_1);
            o3.OrderDetails.Add(od3_2);
            o3.OrderDetails.Add(od3_3);
            o3.OrderDetails.Add(od3_4);

            #endregion

            #region Order4
            EntityClass.OrderDetail od4_1 = new EntityClass.OrderDetail
            {
                Product = dr5,
                Order = o4,
                Quantity = 10,
                Discount = 1 - 0.3f
            };
            od4_1.UnitPrice = (int)(od4_1.Quantity * od4_1.Product.UnitPrice * od4_1.Discount);
            o4.Total += od4_1.UnitPrice;

            o4.OrderDetails.Add(od4_1);
            #endregion

            #region Order5
            EntityClass.OrderDetail od5_1 = new EntityClass.OrderDetail
            {
                Product = mc8,
                Order = o5,
                Quantity = 1,
                Discount = 1
            };
            od5_1.UnitPrice = (int)(od5_1.Quantity * od5_1.Product.UnitPrice * od5_1.Discount);
            o5.Total += od5_1.UnitPrice;

            EntityClass.OrderDetail od5_2 = new EntityClass.OrderDetail
            {
                Product = d1,
                Order = o5,
                Quantity = 1,
                Discount = 1
            };
            od5_2.UnitPrice = (int)(od5_2.Quantity * od5_2.Product.UnitPrice * od5_2.Discount);
            o5.Total += od5_2.UnitPrice;

            o5.OrderDetails.Add(od5_2);
            o5.OrderDetails.Add(od5_1);

            #endregion

            #region Order6
            EntityClass.OrderDetail od6_1 = new EntityClass.OrderDetail
            {
                Product = a2,
                Order = o6,
                Quantity = 3,
                Discount = 1 + 0.2f
            };
            od6_1.UnitPrice = (int)(od6_1.Quantity * od6_1.Product.UnitPrice * od6_1.Discount);
            o6.Total += od6_1.UnitPrice;

            EntityClass.OrderDetail od6_2 = new EntityClass.OrderDetail
            {
                Product = mc7,
                Order = o6,
                Quantity = 3,
                Discount = 1 + 0.2f
            };
            od6_2.UnitPrice = (int)(od6_2.Quantity * od6_2.Product.UnitPrice * od6_2.Discount);
            o6.Total += od6_2.UnitPrice;

            EntityClass.OrderDetail od6_3 = new EntityClass.OrderDetail
            {
                Product = v5,
                Order = o6,
                Quantity = 3,
                Discount = 1 + 0.2f
            };
            od6_3.UnitPrice = (int)(od6_3.Quantity * od6_3.Product.UnitPrice * od6_3.Discount);
            o6.Total += od6_3.UnitPrice;

            EntityClass.OrderDetail od6_4 = new EntityClass.OrderDetail
            {
                Product = dr3,
                Order = o6,
                Quantity = 3,
                Discount = 1 + 0.2f
            };
            od6_4.UnitPrice = (int)(od6_4.Quantity * od6_4.Product.UnitPrice * od6_4.Discount);
            o6.Total += od6_4.UnitPrice;

            o6.OrderDetails.Add(od6_1);
            o6.OrderDetails.Add(od6_2);
            o6.OrderDetails.Add(od6_3);
            o6.OrderDetails.Add(od6_4);

            #endregion

            #region Order7
            EntityClass.OrderDetail od7_1 = new EntityClass.OrderDetail
            {
                Product = mc9,
                Order = o7,
                Quantity = 1,
                Discount = 1 + 0.2f
            };
            od7_1.UnitPrice = (int)(od7_1.Quantity * od7_1.Product.UnitPrice * od7_1.Discount);
            o7.Total += od7_1.UnitPrice;

            EntityClass.OrderDetail od7_2 = new EntityClass.OrderDetail
            {
                Product = dr2,
                Order = o7,
                Quantity = 1,
                Discount = 1 + 0.2f
            };
            od7_2.UnitPrice = (int)(od7_2.Quantity * od7_2.Product.UnitPrice * od7_2.Discount);
            o7.Total += od7_2.UnitPrice;

            o7.OrderDetails.Add(od7_1);
            o7.OrderDetails.Add(od7_2);

            #endregion

            #region Order8
            EntityClass.OrderDetail od8_1 = new EntityClass.OrderDetail
            {
                Product = mc10,
                Order = o8,
                Quantity = 6,
                Discount = 1
            };
            od8_1.UnitPrice = (int)(od8_1.Quantity * od8_1.Product.UnitPrice * od8_1.Discount);
            o8.Total += od8_1.UnitPrice;

            o8.OrderDetails.Add(od8_1);
            #endregion

            #region Order9
            EntityClass.OrderDetail od9_1 = new EntityClass.OrderDetail
            {
                Product = a1,
                Order = o9,
                Quantity = 4,
                Discount = 1 + 0.2f
            };
            od9_1.UnitPrice = (int)(od9_1.Quantity * od9_1.Product.UnitPrice * od9_1.Discount);
            o9.Total += od9_1.UnitPrice;

            EntityClass.OrderDetail od9_2 = new EntityClass.OrderDetail
            {
                Product = mc10,
                Order = o9,
                Quantity = 4,
                Discount = 1 + 0.2f
            };
            od9_2.UnitPrice = (int)(od9_2.Quantity * od9_2.Product.UnitPrice * od9_2.Discount);
            o9.Total += od9_2.UnitPrice;

            EntityClass.OrderDetail od9_3 = new EntityClass.OrderDetail
            {
                Product = d3,
                Order = o9,
                Quantity = 4,
                Discount = 1 + 0.2f
            };
            od9_3.UnitPrice = (int)(od9_3.Quantity * od9_3.Product.UnitPrice * od9_3.Discount);
            o9.Total += od9_3.UnitPrice;

            EntityClass.OrderDetail od9_4 = new EntityClass.OrderDetail
            {
                Product = dr1,
                Order = o9,
                Quantity = 2,
                Discount = 1 + 0.2f
            };
            od9_4.UnitPrice = (int)(od9_4.Quantity * od9_4.Product.UnitPrice * od9_4.Discount);
            o9.Total += od9_4.UnitPrice;

            o9.OrderDetails.Add(od9_1);
            o9.OrderDetails.Add(od9_2);
            o9.OrderDetails.Add(od9_3);
            o9.OrderDetails.Add(od9_4);

            #endregion

            #region Order10
            EntityClass.OrderDetail od10_1 = new EntityClass.OrderDetail
            {
                Product = v4,
                Order = o10,
                Quantity = 3,
                Discount = 1
            };
            od10_1.UnitPrice = (int)(od10_1.Quantity * od10_1.Product.UnitPrice * od10_1.Discount);
            o10.Total += od10_1.UnitPrice;

            EntityClass.OrderDetail od10_2 = new EntityClass.OrderDetail
            {
                Product = dr5,
                Order = o10,
                Quantity = 2,
                Discount = 1
            };
            od10_2.UnitPrice = (int)(od10_2.Quantity * od10_2.Product.UnitPrice * od10_2.Discount);
            o10.Total += od10_2.UnitPrice;

            o10.OrderDetails.Add(od10_1);
            o10.OrderDetails.Add(od10_2);

            #endregion

            #region Order11
            EntityClass.OrderDetail od11_1 = new EntityClass.OrderDetail
            {
                Product = mc8,
                Order = o11,
                Quantity = 12,
                Discount = 1 - 0.3f
            };
            od11_1.UnitPrice = (int)(od11_1.Quantity * od11_1.Product.UnitPrice * od11_1.Discount);
            o11.Total += od11_1.UnitPrice;

            EntityClass.OrderDetail od11_2 = new EntityClass.OrderDetail
            {
                Product = v5,
                Order = o11,
                Quantity = 6,
                Discount = 1
            };
            od11_2.UnitPrice = (int)(od11_2.Quantity * od11_2.Product.UnitPrice * od11_2.Discount);
            o11.Total += od11_2.UnitPrice;

            EntityClass.OrderDetail od11_3 = new EntityClass.OrderDetail
            {
                Product = d1,
                Order = o11,
                Quantity = 12,
                Discount = 1 - 0.3f
            };
            od11_3.UnitPrice = (int)(od11_3.Quantity * od11_3.Product.UnitPrice * od11_3.Discount);
            o11.Total += od11_3.UnitPrice;

            EntityClass.OrderDetail od11_4 = new EntityClass.OrderDetail
            {
                Product = dr1,
                Order = o11,
                Quantity = 4,
                Discount = 1
            };
            od11_4.UnitPrice = (int)(od11_4.Quantity * od11_4.Product.UnitPrice * od11_4.Discount);
            o11.Total += od11_4.UnitPrice;

            o11.OrderDetails.Add(od11_1);
            o11.OrderDetails.Add(od11_2);
            o11.OrderDetails.Add(od11_3);
            o11.OrderDetails.Add(od11_4);

            #endregion

            #region Order12
            EntityClass.OrderDetail od12_1 = new EntityClass.OrderDetail
            {
                Product = a3,
                Order = o12,
                Quantity = 2,
                Discount = 1
            };
            od12_1.UnitPrice = (int)(od12_1.Quantity * od12_1.Product.UnitPrice * od12_1.Discount);
            o12.Total += od12_1.UnitPrice;

            o12.OrderDetails.Add(od12_1);
            #endregion

            #region Order13
            EntityClass.OrderDetail od13_1 = new EntityClass.OrderDetail
            {
                Product = a5,
                Order = o13,
                Quantity = 1,
                Discount = 1
            };
            od13_1.UnitPrice = (int)(od13_1.Quantity * od13_1.Product.UnitPrice * od13_1.Discount);
            o13.Total += od13_1.UnitPrice;

            EntityClass.OrderDetail od13_2 = new EntityClass.OrderDetail
            {
                Product = mc6,
                Order = o13,
                Quantity = 1,
                Discount = 1
            };
            od13_2.UnitPrice = (int)(od13_2.Quantity * od13_2.Product.UnitPrice * od13_2.Discount);
            o13.Total += od13_2.UnitPrice;

            o13.OrderDetails.Add(od13_1);
            o13.OrderDetails.Add(od13_2);

            #endregion

            #region Order14
            EntityClass.OrderDetail od14_1 = new EntityClass.OrderDetail
            {
                Product = mc9,
                Order = o14,
                Quantity = 2,
                Discount = 1 + 0.2f
            };
            od14_1.UnitPrice = (int)(od14_1.Quantity * od14_1.Product.UnitPrice * od14_1.Discount);
            o14.Total += od14_1.UnitPrice;

            EntityClass.OrderDetail od14_2 = new EntityClass.OrderDetail
            {
                Product = d5,
                Order = o14,
                Quantity = 10,
                Discount = (float)Math.Round(1 + 0.2f - 0.3f, 1)
            };
            od14_2.UnitPrice = (int)(od14_2.Quantity * od14_2.Product.UnitPrice * od14_2.Discount);
            o14.Total += od14_2.UnitPrice;

            EntityClass.OrderDetail od14_3 = new EntityClass.OrderDetail
            {
                Product = dr2,
                Order = o14,
                Quantity = 1,
                Discount = 1 + 0.2f
            };
            od14_3.UnitPrice = (int)(od14_3.Quantity * od14_3.Product.UnitPrice * od14_3.Discount);
            o14.Total += od14_3.UnitPrice;

            o14.OrderDetails.Add(od14_1);
            o14.OrderDetails.Add(od14_2);
            o14.OrderDetails.Add(od14_3);

            #endregion

            #region Order15
            EntityClass.OrderDetail od15_1 = new EntityClass.OrderDetail
            {
                Product = a1,
                Order = o15,
                Quantity = 2,
                Discount = 1
            };
            od15_1.UnitPrice = (int)(od15_1.Quantity * od15_1.Product.UnitPrice * od15_1.Discount);
            o15.Total += od15_1.UnitPrice;

            EntityClass.OrderDetail od15_2 = new EntityClass.OrderDetail
            {
                Product = dr5,
                Order = o15,
                Quantity = 2,
                Discount = 1
            };
            od15_2.UnitPrice = (int)(od15_2.Quantity * od15_2.Product.UnitPrice * od15_2.Discount);
            o15.Total += od15_2.UnitPrice;

            o15.OrderDetails.Add(od15_1);
            o15.OrderDetails.Add(od15_2);

            #endregion

            #endregion

            base.Seed(context);
        }
    }
}
