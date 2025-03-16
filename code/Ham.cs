using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace QuanLyQuanCafe
{
    internal class Ham
    {
        public void Ketnoi(SqlConnection conn)
        {
            string chuoiketnoi = "SERVER = wyle\\SQLEXPRESS; database = QuanLyQuanCafe; Integrated Security = True";
            conn.ConnectionString = chuoiketnoi;
            conn.Open();
        }
        public void HienthiDuLieuDG(DataGridView dg, string sql, SqlConnection conn)
        {
            SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
            DataSet dataset = new DataSet();
            adapter.Fill(dataset, "new table");

            dg.DataSource = dataset;
            dg.DataMember = "new table";

        }
    }
}
