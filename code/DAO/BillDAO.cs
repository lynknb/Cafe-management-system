using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyQuanCafe.DAO
{
    public class BillDAO
    {
        private static BillDAO instance;
       // public fTableManager f=new fTableManager();

        public static BillDAO Instance 
        {
           get { if (instance == null) instance = new BillDAO(); return BillDAO.instance; }
           private set { instance = value; }
        }

        private BillDAO() { }

        /// <summary>
        /// Thành công: bill ID
        /// thất bại: -1
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        /*private string connectionSTR = "Data Source=.\\sqlexpress; Initial Catalog=QuanLyQuanCafe2; Integrated Security=True";
        public int createNewBill()
        {
            int b;
            using (SqlConnection con = new SqlConnection(connectionSTR))
            {
                string sql = "SELECT count(id) FROM [dbo].[Bill]";
                DataSet ds = new DataSet();
                SqlDataAdapter dap = new SqlDataAdapter(sql, con);
                dap.Fill(ds);
                b = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
            }
            using (SqlConnection con = new SqlConnection(connectionSTR ))
            {
                string sql = "exec"
            }

            return b++;
        }*/
        public int GetUncheckBillIDByTableID(int id)
        {

            //return (int)DataProvider.Instance.ExecuteScalar(""); 
           DataTable data = DataProvider.Instance.ExecuteQuery("SELECT * FROM dbo.Bill WHERE idTable = '" + id + "' AND status = 0");

            if (data.Rows.Count > 0)
           {
               Bill bill = new Bill(data.Rows[0]);
                return bill.ID;
            }

            return -1;
        }
        



        public void CheckOut(int id, int discount, float totalPrice)
        {
            string query = "UPDATE dbo.Bill SET  DateCheckOut = GETDATE(), status = 1, " + "discount = " + discount + ", totalPrice = " + totalPrice + " WHERE id = " + id;
            DataProvider.Instance.ExecuteNonQuery(query);
        }


        public void InsertBill(int id)
        {
            DataProvider.Instance.ExecuteQuery(" EXEC USP_InsertBill @idTable", new object[] { id });
        }

        public DataTable GetBillListByDate(DateTime checkIn, DateTime checkOut)
        {
            return DataProvider.Instance.ExecuteQuery("EXEC USP_GetListBillByDate @checkIn , @checkOut", new object[] {checkIn, checkOut});

        }
        public int GetMaxIDBill()
        {
            try
            {
                return (int)DataProvider.Instance.ExecuteScalar("SELECT MAX(id) FROM dbo.Bill");
            }
            catch
            {
                return -1;
            }
        }
        public DataTable GetBillListByDateAndPage(DateTime checkIn, DateTime checkOut, int pageNum)
        {
            return DataProvider.Instance.ExecuteQuery("exec USP_GetListBillByDateAndPage @checkIn , @checkOut , @page", new object[] { checkIn, checkOut, pageNum });
        }
        public int GetNumBillListByDate(DateTime checkIn, DateTime checkOut)
        {
            return (int)DataProvider.Instance.ExecuteScalar("EXEC USP_GetNumBillByDate @checkIn , @checkOut", new object[] { checkIn, checkOut });
        }
    }
}
