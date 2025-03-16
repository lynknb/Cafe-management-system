using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DAO
{
    public class TableDAO
    {
        private static TableDAO instance;

        public static TableDAO Instance
        {
            get { if (instance == null) instance = new TableDAO(); return TableDAO.instance; }
            private set { instance = value; }
        }
        public static int TableWidth = 100;
        public static int TableHeight = 100;
        private TableDAO() { }

        public void SwitchTable(int id1, int id2)
        {
            DataProvider.Instance.ExecuteQuery(" EXEC USP_SwitchTable1 @idTable1 , @idTable2", new object[] { id1, id2 });
        }

        public List<Table> LoadTableList()
        {
            List<Table> tableList = new List<Table>();
            DataTable data = DataProvider.Instance.ExecuteQuery("USP_GetTableList");

            foreach(DataRow item in data.Rows)
            {
                Table table = new Table(item);
                tableList.Add(table);
            }    
            return tableList;
        }
        public bool InsertTableFood (string name, string status)
        {
            string query = string.Format("INSERT dbo.TableFood (name)VALUES (N'{0}', {1})", name, status);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }
        public bool UpdateTableFood(string name, int id, string status)
        {

            string query = string.Format("UPDATE dbo.TableFood SET  status = N'{0}' WHERE id = {1}", status, id);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }
        public bool DeleteTableFood(int id)
        {  // BillInfDAO.Instance.DeleteBillInfoByFoodID(idFood);

             string query = string.Format("DELETE TableFood WHERE id = {0}", id);
        int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }
    }
}
