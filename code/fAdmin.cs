using QuanLyQuanCafe.DAO;
using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;


namespace QuanLyQuanCafe
{
    public partial class fAdmin : Form
    {
        private DataTable NhanVien;
        // BindingSource foodList = new BindingSource();
        BindingSource accountList = new BindingSource();
        public Account loginAccount;
        public fAdmin()
        {
            InitializeComponent();
            // dtgvFood.DataSource = foodList;
            dtgvAccount.DataSource = accountList;
            // LoadAccountList();

            LoadFoodList();
            LoadListFood();
            LoadListCategory();
            LoadListTable();
            LoadAccount();

            LoadListBillByDate(dtpkFromDate.Value, dtpkToDate.Value); // de cho thong ke load lien khoi bam
            LoadDateTimePickerBill();
            LoadCategoryIntoCombobox(cbFoodCategory);
            AddBinding();
            AddAccountBinding();

        }
        #region methods
        void AddAccountBinding()
        {
            txtUserName.DataBindings.Add(new Binding("Text", dtgvAccount.DataSource, "UserName"));
            txtDisplayName.DataBindings.Add(new Binding("Text", dtgvAccount.DataSource, "DisplayName"));
            nmAccountType.DataBindings.Add(new Binding("Value", dtgvAccount.DataSource, "Type", true, DataSourceUpdateMode.Never));
        }
        void LoadAccount()
        {
            accountList.DataSource = AccountDAO.Instance.GetListAccount();
        }
        List<Food> SearchFoodByName(string name)
        {
            List<Food> listfood = FoodDAO.Instance.SearchFoodByName(name);

            return listfood;
        }
        void LoadAccountList()
        {
            string query = "EXEC dbo.USP_GetAccountByUserName @userName ";
            //   string query = "select * from Account";


            dtgvAccount.DataSource = DataProvider.Instance.ExecuteQuery(query, new object[] { "admin" });

        }


        void LoadFoodList()
        {
            string query = "select id AS [STT], name AS [Tên món ăn], idCategory, price AS [Giá] from dbo.Food";

            dtgvFood.DataSource = DataProvider.Instance.ExecuteQuery(query);
        }
        void LoadListBillByDate(DateTime checkIn, DateTime checkOut)
        {
            dtgvBill.DataSource = BillDAO.Instance.GetBillListByDate(checkIn, checkOut);
        }

        void LoadDateTimePickerBill() // ham de cho picker hien thang hien tai muon thong ke
        {
            DateTime today = DateTime.Now;
            dtpkFromDate.Value = new DateTime(today.Year, today.Month, 1);
            dtpkToDate.Value = dtpkToDate.Value.AddMonths(1).AddDays(-1);
        }
        private void fAdmin_Load(object sender, EventArgs e)
        {
            dtgvFood.Font = new Font("Times New Roman", 12);


        }
        void AddBinding()
        {   //mon an
            txtFoodID.DataBindings.Add(new Binding("Text", dtgvFood.DataSource, "ID", true, DataSourceUpdateMode.Never));
            txtFoodName.DataBindings.Add(new Binding("Text", dtgvFood.DataSource, "Name", true, DataSourceUpdateMode.Never));

            nmFoodPrice.DataBindings.Add(new Binding("Text", dtgvFood.DataSource, "Price", true, DataSourceUpdateMode.Never));

            //danh muc
            txtCategoryID.DataBindings.Add(new Binding("Text", dtgvCategory.DataSource, "ID", true, DataSourceUpdateMode.Never));
            txtCategoryName.DataBindings.Add(new Binding("Text", dtgvCategory.DataSource, "Name", true, DataSourceUpdateMode.Never));

            //ban an
            txtTableID.DataBindings.Add(new Binding("Text", dtgvTable.DataSource, "ID", true, DataSourceUpdateMode.Never)); ;
            txtTableName.DataBindings.Add(new Binding("Text", dtgvTable.DataSource, "Name", true, DataSourceUpdateMode.Never));
            cbTableStatus.DataBindings.Add(new Binding("Text", dtgvTable.DataSource, "Status", true, DataSourceUpdateMode.Never));

        }
        void LoadCategoryIntoCombobox(ComboBox cb)
        {
            cb.DataSource = CategoryDAO.Instance.GetListCategory();
            cb.DisplayMember = "Name";
        }
        void LoadListFood()
        {

            dtgvFood.DataSource = FoodDAO.Instance.GetListFood();
        }
        void AddAccount(string userName, string displayName, int type)
        {
            if (AccountDAO.Instance.InsertAccount(userName, displayName, type))
            {
                MessageBox.Show("Thêm tài khoản thành công");
            }
            else
            {
                MessageBox.Show("Thêm tài khoản thất bại");
            }
            LoadAccount();
        }
        void DeleteAccount(string userName)
        {
            if (loginAccount.UserName.Equals(userName))
            {
                MessageBox.Show("Bạn có thật sự muốn xóa chính bạn!", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                return;
            }
            if (AccountDAO.Instance.DeleteAccount(userName))
            {
                MessageBox.Show("Xóa tài khoản thành công", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show("Xóa tài khoản thất bại");
            }

            LoadAccount();
        }
        void EditAccount(string userName, string displayName, int type)
        {
            if (AccountDAO.Instance.UpdateAccount(userName, displayName, type))
            {
                MessageBox.Show("Cập nhật tài khoản thành công");
            }
            else
            {
                MessageBox.Show("Cập nhật tài khoản thất bại");
            }

            LoadAccount();
        }
        void ResetPass(string userName)
        {
            if (AccountDAO.Instance.ResetPassword(userName))
            {
                MessageBox.Show("Đặt lại mật khẩu thành công", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Đặt lại mật khẩu thất bại");
            }


        }
        void LoadListCategory()
        {
            dtgvCategory.DataSource = CategoryDAO.Instance.GetListCategory();
        }
        void LoadListTable()
        {
            dtgvTable.DataSource = TableDAO.Instance.LoadTableList();
        }
        #endregion


        #region events
        private void btnViewBill_Click(object sender, EventArgs e)
        {
            LoadListBillByDate(dtpkFromDate.Value, dtpkToDate.Value);
        }
        private void btnShowName_Click(object sender, EventArgs e)
        {
            LoadListFood();

        }
        private void btnShowTable_Click(object sender, EventArgs e)
        {
            LoadListTable();
        }

        private void btnShowCategory_Click(object sender, EventArgs e)
        {
            LoadListCategory();
        }

        private void btnAddFood_Click(object sender, EventArgs e) // them mon an
        {
            string name = txtFoodName.Text;
            int categoryID = (cbFoodCategory.SelectedItem as Category).ID;
            float price = (float)nmFoodPrice.Value;

            if (FoodDAO.Instance.InsertFood(name, categoryID, price))
            {
                MessageBox.Show("Thêm nước thành công");
                LoadListFood();
                if (insertFood != null)
                    insertFood(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Đã xảy ra lỗi khi thêm nước");
            }

;
        }
        private void btnEditFood_Click(object sender, EventArgs e) // sua mon an
        {

            string name = txtFoodName.Text;
            int categoryID = (cbFoodCategory.SelectedItem as Category).ID;
            float price = (float)nmFoodPrice.Value;
            int id = Convert.ToInt32(txtFoodID.Text);

            if (FoodDAO.Instance.UpdateFood(id, name, categoryID, price))
            {
                MessageBox.Show("Sửa thức uống thành công");
                LoadListFood();
                if (updateFood != null)
                    updateFood(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Đã xảy ra lỗi khi sửa thức uống");
            }
        }
        private void btnDeleteFood_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtFoodID.Text);

            if (FoodDAO.Instance.DeleteFood(id))
            {
                MessageBox.Show("Xóa thức uống thành công");
                LoadListFood();
                if (deleteFood != null)
                    deleteFood(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Đã xảy ra lỗi khi xóa thức uống");
            }

        }


        private void txtFoodID_TextChanged(object sender, EventArgs e)
        {
            try
            {


                if (dtgvFood.SelectedCells.Count > 0)
                {
                    int id = (int)dtgvFood.SelectedCells[0].OwningRow.Cells["CategoryID"].Value;

                    Category cateogory = CategoryDAO.Instance.GetCategoryByID(id);

                    cbFoodCategory.SelectedItem = cateogory;

                    int index = -1;
                    int i = 0;
                    foreach (Category item in cbFoodCategory.Items)
                    {
                        if (item.ID == cateogory.ID)
                        {
                            index = i;
                            break;
                        }
                        i++;
                    }

                    cbFoodCategory.SelectedIndex = index;
                }
            }
            catch
            {

            }

        }
        // tao event de khi thao tac mon an ben trang Admin thi ben trang quan ly cap nhat theo
        private event EventHandler insertFood;
        public event EventHandler InsertFood
        {
            add { insertFood += value; }
            remove { insertFood -= value; }
        }

        private event EventHandler deleteFood;
        public event EventHandler DeleteFood
        {
            add { deleteFood += value; }
            remove { deleteFood -= value; }
        }

        private event EventHandler updateFood;
        public event EventHandler UpdateFood
        {
            add { updateFood += value; }
            remove { updateFood -= value; }
        }

        //QUAN LY DANH MUC
        private void btnAddCategory_Click(object sender, EventArgs e) // THEM DANH MUC
        {

            string name = txtCategoryName.Text;
            //int id = Convert.ToInt32(txtCategoryID.Text);

            if (CategoryDAO.Instance.InsertCategory(name))
            {
                MessageBox.Show("Thêm danh mục mới thành công");
                LoadListCategory();
                if (insertCategory != null)
                    insertCategory(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Đã xảy ra lỗi khi thêm danh mục");
            }

        }

        private void btnEditCategory_Click(object sender, EventArgs e) // SUA DANH MUC
        {

            string name = txtCategoryName.Text;

            int id = Convert.ToInt32(txtCategoryID.Text);

            if (CategoryDAO.Instance.UpdateCategory(name, id))
            {
                MessageBox.Show("Sửa danh mục thành công");
                LoadListCategory();
                if (updateCategory != null)
                    updateCategory(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Đã xảy ra lỗi khi sửa danh mục");
            }
        }

        private void btnDeleteCategory_Click(object sender, EventArgs e) // XOA DANH MUC
        {
            int id = Convert.ToInt32(txtCategoryID.Text);

            if (CategoryDAO.Instance.DeleteCategory(id))
            {
                MessageBox.Show("Xóa danh mục thành công");
                LoadListCategory();
                if (deleteCategory != null)
                    deleteCategory(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Đã xảy ra lỗi khi xóa danh mục");
            }
        }
        //  QUAN LY BAN ĂN
        private void btnAddTable_Click(object sender, EventArgs e) // THEM BAN AN
        {
            string name = txtTableID.Text;
            string status = Convert.ToString(cbTableStatus.SelectedItem as Table);
            //int id = Convert.ToInt32(txtCategoryID.Text);

            if (TableDAO.Instance.InsertTableFood(name, status))
            {
                MessageBox.Show("Thêm bàn mới thành công");

                if (insertTableFood != null)
                    insertTableFood(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Đã xảy ra lỗi khi thêm bàn");
            }
        }

        private void btnDeleteTable_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtTableID.Text);

            if (CategoryDAO.Instance.DeleteCategory(id))
            {
                MessageBox.Show("Xóa bàn thành công");
                LoadListTable();
                if (deleteTableFood != null)
                    deleteTableFood(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Đã xảy ra lỗi khi xóa bàn");
            }
        }

        private void btnEditTable_Click(object sender, EventArgs e)
        {

            string name = txtTableName.Text;
            string status = Convert.ToString(cbTableStatus.SelectedItem as Table);

            int id = Convert.ToInt32(txtTableID.Text);

            if (TableDAO.Instance.UpdateTableFood(name, id, status))
            {
                MessageBox.Show("Cập nhật bàn thành công");
                LoadListTable();
                if (updateTableFood != null)
                    updateTableFood(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Đã xảy ra lỗi khi cập nhật bàn");
            }
        }
        // tao event de khi thao tac DANH MUC ben trang Admin thi ben trang quan ly cap nhat theo
        private event EventHandler insertCategory;
        public event EventHandler InsertCategory
        {
            add { insertCategory += value; }
            remove { insertCategory -= value; }
        }

        private event EventHandler deleteCategory;
        public event EventHandler DeleteCategory
        {
            add { deleteCategory += value; }
            remove { deleteCategory -= value; }
        }

        private event EventHandler updateCategory;
        public event EventHandler UpdateCategory
        {
            add { updateCategory += value; }
            remove { updateCategory -= value; }
        }

        // tao event de khi thao tac BAN ben trang Admin thi ben trang quan ly cap nhat theo
        private event EventHandler insertTableFood;
        public event EventHandler InsertTableFood
        {
            add { insertTableFood += value; }
            remove { insertTableFood -= value; }
        }

        private event EventHandler deleteTableFood;
        public event EventHandler DeleteTableFood
        {
            add { deleteTableFood += value; }
            remove { deleteTableFood -= value; }
        }

        private event EventHandler updateTableFood;
        public event EventHandler UpdateTableFood
        {
            add { updateTableFood += value; }
            remove { updateTableFood -= value; }
        }

        private void btnSearchFood_Click(object sender, EventArgs e)
        {
            dtgvFood.DataSource = SearchFoodByName(txtSearchFoodNameFoodName.Text);
        }
        //QUAN LY TAI KHOAN
        private void btnShowAccount_Click(object sender, EventArgs e)
        {
            LoadAccount();
        }
        private void btnAddAccount_Click(object sender, EventArgs e)
        {
            string userName = txtUserName.Text;
            string displayName = txtDisplayName.Text;
            int type = (int)nmAccountType.Value;

            AddAccount(userName, displayName, type);
        }

        private void btnDeleteAccount_Click(object sender, EventArgs e)
        {
            string userName = txtUserName.Text;

            DeleteAccount(userName);
        }

        private void btnEditAccount_Click(object sender, EventArgs e)
        {
            string userName = txtUserName.Text;
            string displayName = txtDisplayName.Text;
            int type = (int)nmAccountType.Value;

            EditAccount(userName, displayName, type);
        }
        private void btnResetPassword_Click(object sender, EventArgs e)
        {
            string userName = txtUserName.Text;

            ResetPass(userName);
        }

        #endregion



        private void fAdmin_Load_1(object sender, EventArgs e)
        {
            dtgvFood.Font = new Font("Times New Roman", 12);
            dtgvTable.Font = new Font("Times New Roman", 12);
            dtgvFood.Columns[0].HeaderText = "ID";
            dtgvFood.Columns[1].HeaderText = "Tên món";
            dtgvFood.Columns[2].HeaderText = "Giá";
            dtgvFood.Columns[3].HeaderText = "ID Danh mục";
            dtgvCategory.Columns[0].HeaderText = "ID Danh mục";
            dtgvCategory.Columns[1].HeaderText = "Tên danh mục";
           

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btnFirstBillPage_Click(object sender, EventArgs e)
        {
            txtNumPage.Text = "1";
        }

        private void btnPreviousPage_Click(object sender, EventArgs e)
        {
            int page = Convert.ToInt32(txtNumPage.Text);
            int sumRecord = BillDAO.Instance.GetNumBillListByDate(dtpkFromDate.Value, dtpkToDate.Value);
            if (page < sumRecord)
                page++;
            txtNumPage.Text = page.ToString();
        }

        private void btnLastBillPage_Click(object sender, EventArgs e)
        {
            int sumRecord = BillDAO.Instance.GetNumBillListByDate(dtpkFromDate.Value, dtpkToDate.Value);

            int lastPage = sumRecord / 10;

            if (sumRecord % 10 != 0)
                lastPage++;

            txtNumPage.Text = lastPage.ToString();
        }
        // hàm lập danh sách hóa đơn
        public void ExportRevenue(DataTable dataTable, string sheetName, string title)
        {
            Microsoft.Office.Interop.Excel.Application Excel = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbooks Books;
            Microsoft.Office.Interop.Excel.Workbook Book;
            Microsoft.Office.Interop.Excel.Sheets sheets;
            Microsoft.Office.Interop.Excel.Worksheet sheet;

            Excel.Visible = true;
            Excel.DisplayAlerts = false;
            Excel.Application.SheetsInNewWorkbook = 1;
            Books = Excel.Workbooks;
            Book = (Microsoft.Office.Interop.Excel.Workbook)(Excel.Workbooks.Add(Type.Missing));
            sheets = Book.Worksheets;
            sheet = (Microsoft.Office.Interop.Excel.Worksheet)sheets.get_Item(1);
            sheet.Name = sheetName;

            Microsoft.Office.Interop.Excel.Range head = sheet.get_Range("A1", "E1");
            head.MergeCells = true;
            head.Value2 = title;
            head.Font.Bold = true;
            head.Font.Name = "Times New Roman";
            head.Interior.ColorIndex = 7;
            head.Font.Size = "20";
            head.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            Microsoft.Office.Interop.Excel.Range c11 = sheet.get_Range("A3", "A3");
            c11.Value2 = "Tên bàn";
            c11.ColumnWidth = 15;

            Microsoft.Office.Interop.Excel.Range c12 = sheet.get_Range("B3", "B3");
            c12.Value2 = "Ngày vào";
            c12.ColumnWidth = 20.5;

            Microsoft.Office.Interop.Excel.Range c13 = sheet.get_Range("C3", "C3");
            c13.Value2 = "Ngày ra";
            c13.ColumnWidth = 20.5;

            Microsoft.Office.Interop.Excel.Range c14 = sheet.get_Range("D3", "D3");
            c14.Value2 = "Giảm giá";
            c14.ColumnWidth = 20;

            Microsoft.Office.Interop.Excel.Range c15 = sheet.get_Range("E3", "E3");
            c15.Value2 = "Tổng tiền";
            c15.ColumnWidth = 20;

            //Microsoft.Office.Interop.Excel.Range c16 = sheet.get_Range("F3", "F3");
           // c16.Value2 = "Tổng Tiền";
            //c16.ColumnWidth = 20;


            Microsoft.Office.Interop.Excel.Range rowHead = sheet.get_Range("A3", "E3");
            rowHead.Font.Bold = true;
            rowHead.Interior.ColorIndex = 6;
            rowHead.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;



            object[,] arr = new object[dataTable.Rows.Count, dataTable.Columns.Count];

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                DataRow row = dataTable.Rows[i];
                for (int j = 0; j < dataTable.Columns.Count; j++)
                {
                    
                    arr[i, j] = row[j].ToString();
                }
            }

            int rowStart = 4;
            int colStart = 1;
            int rowEnd = rowStart + dataTable.Rows.Count - 1;
            int colEnd = dataTable.Columns.Count;

            Microsoft.Office.Interop.Excel.Range c1 = (Microsoft.Office.Interop.Excel.Range)sheet.Cells[rowStart, colStart];
            Microsoft.Office.Interop.Excel.Range c2 = (Microsoft.Office.Interop.Excel.Range)sheet.Cells[rowEnd, colEnd];

            Microsoft.Office.Interop.Excel.Range range = sheet.get_Range(c1, c2);
            range.Value2 = arr;
            range.Borders.LineStyle = Microsoft.Office.Interop.Excel.Constants.xlSolid;


        }

        private void btnLapHD_Click(object sender, EventArgs e)
        {
           // string ngayBD = dtpkFromDate.ToString();
           // string ngayKT = dtpkToDate.Value.ToString();
            DataTable dt = new DataTable();
            dt = BillDAO.Instance.GetBillListByDate(dtpkFromDate.Value, dtpkToDate.Value);
            this.ExportRevenue(dt, "Thông kê", "Doanh Thu");//Thông kê: Tên file, Doanh Thu: Dòng tiêu đề cho trang excel

         /*   // Lấy giá trị ngày tháng từ DateTimePicker
            DateTime ngayBD = dtpkFromDate.Value;
            DateTime ngayKT = dtpkToDate.Value;
            // Định dạng lại theo chuẩn của SQL Server
            string ngayBDSQL = ngayBD.ToString("yyyy-MM-dd");
            string ngayKTSQL = ngayKT.ToString("yyyy-MM-dd");*/

        }

      
        private void dtgvFood_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        
    }
}
