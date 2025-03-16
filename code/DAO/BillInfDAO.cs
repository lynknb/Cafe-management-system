using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DAO
{
    public class BillInfDAO
    {
        private static BillInfDAO instance;
        public static BillInfDAO Instance
        { 
            get { if (instance == null) instance = new BillInfDAO(); return BillInfDAO.instance; }
           private set { BillInfDAO.instance = value;}
        }

        private BillInfDAO() { }

      /*  public void DeleteBillInfoByFoodID(int id)
        {
            DataProvider.Instance.ExecuteQuery("Delete dbo.BillInfo where idFood = " + id);
        }*/
        public List<BillInfo> GetListBillInfo(int id)
        {
            List<BillInfo> listBillInfo = new List<BillInfo>();
            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT * FROM dbo.BillInfo WHERE idBill = " + id);
            foreach (DataRow item in data.Rows)
            {
                BillInfo info = new BillInfo(item);
                listBillInfo.Add(info);
            }
            return listBillInfo;
        } 

         public void InsertBillInfo(int idBill, int idFood, int count)
        {
            DataProvider.Instance.ExecuteNonQuery("EXEC USP_InsertBillInfo @idBill , @idFood , @count", new object[] { idBill, idFood, count });
            
        }
        
    }
}
