using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web_Project
{
    public partial class SiteMaster : MasterPage
    {
        public SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Hotel"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user_role"] == null)
            {
                return;
            }

            DataTable dt = new DataTable();
            using (conn)
            {
                SqlDataAdapter ad = new SqlDataAdapter(string.Format("SELECT * FROM Role_Page WHERE user_role LIKE '%{0}%' AND page_status = 1 ORDER BY sort", Session["user_role"].ToString()), conn);
                ad.Fill(dt);
            }
            PageList.DataSource = dt;
            PageList.DataBind();
        }
    }
}