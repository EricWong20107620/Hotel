using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;

namespace Web_Project
{
    public partial class Staff_Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Confidential_Data cd = new Confidential_Data();

            if (Session["login_name"] == null || Session["user_role"].ToString() != "S")
            {
                Response.Redirect("Login.aspx");
                return;
            }

            lblLogin.Text = "Welcome to JATE Hotel, " + cd.Decrypt(Session["login_name"].ToString());

            DataTable dt = new DataTable();
            dt = get_Count_Inquiry();
            int count_Need_Reply_Inquiry = dt.Rows[0].Field<int>("Need_Reply_Inquiry");
            int count_Not_Yet_Close_Inquirry = dt.Rows[0].Field<int>("Not_Yet_Close_Inquirry");

            lblCountNeedReplyInquiry.Text = count_Need_Reply_Inquiry.ToString();
            lblCountNotYetCloseInquirry.Text = count_Not_Yet_Close_Inquirry.ToString();

            if (count_Need_Reply_Inquiry > 0)
            {
                lblCountNeedReplyInquiry.ForeColor = Color.Red;
            }
            else
            {
                lblCountNeedReplyInquiry.ForeColor = Color.Green;
            }

            if (count_Not_Yet_Close_Inquirry > 0)
            {
                lblCountNotYetCloseInquirry.ForeColor = Color.Red;
            }
            else
            {
                lblCountNotYetCloseInquirry.ForeColor = Color.Green;
            }
        }

        public DataTable get_Count_Inquiry()
        {
            DataTable dt = new DataTable();
            string query = "SELECT (SELECT COUNT(*) FROM inquiry_master WHERE inquiry_status = 'P') AS Need_Reply_Inquiry, (SELECT COUNT(*) FROM inquiry_master WHERE inquiry_status <> 'C' AND DATEADD(DD, 10, create_date) >= GETDATE()) AS Not_Yet_Close_Inquirry";
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Hotel"].ConnectionString);
            SqlCommand cmd = new SqlCommand(query, conn);
            conn.Open();

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            conn.Close();
            da.Dispose();
            return dt;
        }
    }
}