using QuanLyQuanCafe.DAO;
using QuanLyQuanCafe.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyQuanCafe
{
    public partial class fTableManager : Form
    {
        private Account loginAccount;
        public Account LoginAccount
        {
            get { return loginAccount; }
            set{loginAccount = value; ChangeAccount(loginAccount.Type);}
        }

        public fTableManager(Account acc)
        {
            InitializeComponent();
            this.LoginAccount = acc;
            LoadTable();
            LoadCataegory();
            LoadComboboxTable(cbSwitchTable);
            //LoadFoodListByCategoryID();
        }
        #region Method
        void ChangeAccount(int type)
        {
            adminToolStripMenuItem.Enabled = type == 1;
            thôngTinTàiKhoảnToolStripMenuItem.Text += " (" + LoginAccount.DisplayName + ")";
        }
        void LoadCataegory()
        {
            List<Category> listCatedgory = CategoryDAO.Instance.GetListCategory();
            cbCategory.DataSource = listCatedgory;
            cbCategory.DisplayMember = "Name";
        }
        void LoadFoodListByCategoryID(int id)
        {
            List<Food> listFood = FoodDAO.Instance.GetFoodByCategoryID(id);
            cbFood.DataSource = listFood;
            cbFood.DisplayMember = "Name";
        }
        public void LoadTable()
        {
            flpTable.Controls.Clear();
            List<Table> tableList = TableDAO.Instance.LoadTableList();

            foreach (Table item in tableList)
            {
                Button btn = new Button() { Width = TableDAO.TableWidth, Height = TableDAO.TableHeight };
                btn.Text = item.Name + Environment.NewLine + item.Status;
                btn.Click += Btn_Click;
                btn.Tag = item;
                switch (item.Status)
                {
                    case "Trống":
                        btn.BackColor = Color.LightCyan;
                        break;
                    default:
                        btn.BackColor = Color.Aqua;
                        break;
                }
                flpTable.Controls.Add(btn);

            }
        }

        public void ShowBill(int id)
        {
            lvBill.Items.Clear();
            List<QuanLyQuanCafe.DTO.Menu> listBillInfo = MenuDAO.Instance.GetListMenuByTable(id);
            float totalPrice = 0;
            foreach (QuanLyQuanCafe.DTO.Menu item in listBillInfo)
            {
                ListViewItem lsvItem = new ListViewItem(item.FoodName.ToString());
                lsvItem.SubItems.Add(item.Count.ToString());
                lsvItem.SubItems.Add(item.Price.ToString());
                lsvItem.SubItems.Add(item.TotalPrice.ToString());
                totalPrice += item.TotalPrice;
                lvBill.Items.Add(lsvItem);
            }
            CultureInfo culture = new CultureInfo("vi-VN");
            //  Thread.CurrentThread.CurrentCulture = culture;
            txtTotalPrice.Text = totalPrice.ToString("c", culture);
            LoadTable();
        }
        void LoadComboboxTable(ComboBox cb)
        {
            cb.DataSource = TableDAO.Instance.LoadTableList();
            cb.DisplayMember = "Name";
        }

        #endregion

        #region Events
        private void Btn_Click(object sender, EventArgs e)
        {
            int tableID = ((sender as Button).Tag as Table).ID;
            lvBill.Tag = (sender as Button).Tag;
            ShowBill(tableID);
        }
        
        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có thật sự muốn đăng xuất phải không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.Yes)
            {

            }
            this.Close();

        }

        private void thôngTinCáNhânToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fAccountProfile f = new fAccountProfile(LoginAccount);
            f.UpdateAccount += f_UpdateAccount;
            f.ShowDialog();

        }
       void f_UpdateAccount(object sender, AccountEvent e)
        {
            thôngTinTàiKhoảnToolStripMenuItem.Text = "Thông tin tài khoản (" + e.Acc.DisplayName + ")";
        }
        private void admin0ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fAdmin f = new fAdmin();
            f.loginAccount = LoginAccount;
            //quan ly mon an
            f.InsertFood += f_InsertFood;
            f.DeleteFood += f_DeleteFood;
            f.UpdateFood += f_UpdateFood;

            //quan ly danh muc
            f.InsertCategory += f_InsertCategory;
            f.DeleteCategory += f_DeleteCategory;
            f.UpdateCategory += f_UpdateCategory;

            //quan ly ban an
            f.InsertTableFood += f_InsertTableFood;
            f.DeleteTableFood += f_DeleteTableFood;
            f.UpdateTableFood += f_UpdateTableFood;
            f.ShowDialog();
        }
        void f_UpdateFood(object sender, EventArgs e)
        {
            LoadFoodListByCategoryID((cbCategory.SelectedItem as Category).ID);
            if (lvBill.Tag != null)
                ShowBill((lvBill.Tag as Table).ID);
        }

        void f_DeleteFood(object sender, EventArgs e)
        {
            LoadFoodListByCategoryID((cbCategory.SelectedItem as Category).ID);
            if (lvBill.Tag != null)
                ShowBill((lvBill.Tag as Table).ID);
            LoadTable();
        }

        void f_InsertFood(object sender, EventArgs e)
        {
            LoadFoodListByCategoryID((cbCategory.SelectedItem as Category).ID);
            if (lvBill.Tag != null)
                ShowBill((lvBill.Tag as Table).ID);
        }

        //DANH MUC
        void f_UpdateCategory(object sender, EventArgs e)
        {
            LoadCataegory();
            if (lvBill.Tag != null)
                ShowBill((lvBill.Tag as Table).ID);
        }

        void f_DeleteCategory(object sender, EventArgs e)
        {
            LoadCataegory();
            if (lvBill.Tag != null)
                ShowBill((lvBill.Tag as Table).ID);
            
        }
        
        void f_InsertCategory(object sender, EventArgs e)
        {
            LoadCataegory();
            if (lvBill.Tag != null)
                ShowBill((lvBill.Tag as Table).ID);
        }

        //Ban An
        void f_UpdateTableFood(object sender, EventArgs e)
        {
            LoadTable();
            if (lvBill.Tag != null)
                ShowBill((lvBill.Tag as Table).ID);
        }

        void f_DeleteTableFood(object sender, EventArgs e)
        {
            LoadTable();
            if (lvBill.Tag != null)
                ShowBill((lvBill.Tag as Table).ID);

        }

        void f_InsertTableFood(object sender, EventArgs e)
        {
            LoadTable();
            if (lvBill.Tag != null)
                ShowBill((lvBill.Tag as Table).ID);
            LoadTable();
        }
        private void thôngTinTàiKhoảnToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }


        private void cbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = 0;
            ComboBox cb = sender as ComboBox;

            if (cb.SelectedItem == null)
                return;

            Category selected = cb.SelectedItem as Category;
            id = selected.ID;
            LoadFoodListByCategoryID(id);
        }
        private void btnAddFood_Click(object sender, EventArgs e)
        {
            Table table = lvBill.Tag as Table;

            if (table == null)
            {
                MessageBox.Show("Vui lòng chọn bàn");
                return;
            }
            int idBill = BillDAO.Instance.GetUncheckBillIDByTableID(table.ID);
            int foodID = (cbFood.SelectedItem as Food).ID;
            int count = (int)nmFoodCount.Value;
            if (idBill == -1)
            {
                BillDAO.Instance.InsertBill(table.ID);
                BillInfDAO.Instance.InsertBillInfo(BillDAO.Instance.GetMaxIDBill(), foodID, count);
            }
            else
            {
                BillInfDAO.Instance.InsertBillInfo(idBill, foodID, count);
            }
            ShowBill(table.ID);
            LoadTable();
        }


        private void btnCheckOut_Click(object sender, EventArgs e)
        {
            Table table = lvBill.Tag as Table; // lấy Table hiện tại

            int idBill = BillDAO.Instance.GetUncheckBillIDByTableID(table.ID);
            int discount = (int)nmDiscount.Value;

            double totalPrice = Convert.ToDouble(txtTotalPrice.Text.Split(',')[0].Replace(".", ""));
            double finalTotalPrice = totalPrice - (totalPrice / 100) * discount;

            if (idBill != -1)
            {
                if (MessageBox.Show(string.Format("Bạn có chắc thanh toán hóa đơn cho {0}\nTổng tiền-(Tổng tiền/100)xGiảm giá = {1} - ({1} / 100) x {2} = {3}", table.Name, totalPrice, discount, finalTotalPrice), "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    //  MessageBox.Show("Bạn có chắc thanh toán hóa đơn cho " + table.Name);
                    BillDAO.Instance.CheckOut(idBill, discount, (float)finalTotalPrice);
                    ShowBill(table.ID);

                    LoadTable();
                }
            }
        }


        private void lvBill_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnSwitchTable_Click(object sender, EventArgs e)
        {
            
            /* int id1 = (lvBill.Tag as Table).ID;

              int id2 = (cbSwitchTable.SelectedItem as Table).ID;
            if (MessageBox.Show(string.Format("Bạn có muốn chuyển bàn {0} qua bàn {1} không", (lvBill.Tag as Table).Name, (cbSwitchTable.SelectedItem as Table).Name), "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                TableDAO.Instance.SwitchTable(id1, id2);

                LoadTable();

            }
*/
             int id1 = (lvBill.Tag as Table).ID; //lấy ra id table đang chọn

            int id2 = (cbSwitchTable.SelectedItem as Table).ID; //lấy ra id table muốn chuyển bàn
            if (MessageBox.Show(string.Format("Bạn có muốn chuyển bàn {0} qua bàn {1} không?", (lvBill.Tag as Table).Name, (cbSwitchTable.SelectedItem as Table).Name), "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                TableDAO.Instance.SwitchTable(id1, id2);


                DataProvider.Instance.ExecuteNonQuery("UPDATE TableFood SET STATUS = N'Trống' WHERE id= " + id1);
                DataProvider.Instance.ExecuteNonQuery("UPDATE TableFood SET STATUS = N'Có người' WHERE id= " + id2);

                LoadTable();

            }
           
        }
        #endregion

        private void txtTotalPrice_TextChanged(object sender, EventArgs e)
        {

        }

        private void hóaĐơnToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}


