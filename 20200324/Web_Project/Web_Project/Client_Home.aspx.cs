using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web_Project
{
    public partial class Client_Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["login_name"] == null || Session["user_role"].ToString() != "C")
            {
                Response.Redirect("Login.aspx");
                return;
            }

            lblLogin.Text = "Welcome to JATE Hotel, " + Session["login_name"].ToString();
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            if (Session["login_name"] == null || Session["user_role"].ToString() != "C")
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (string.IsNullOrEmpty(txtSubject.Text) == true)
            {
                lblAlert.Text = "Please enter your issue subject";
                lblAlert.ForeColor = Color.Red;
                txtSubject.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtMessage.Text) == true)
            {
                lblAlert.Text = "Please enter your issue";
                lblAlert.ForeColor = Color.Red;
                txtMessage.Focus();
                return;
            }

            int result = sp_inquiry(Session["email"].ToString(), txtSubject.Text, ddlCategory.SelectedValue, txtMessage.Text, 1);

            if (result >= 0)
            {
                lblAlert.Text = "Your issue has sent to our admin.<br/>We will reply you in 24 hour.";
                lblAlert.ForeColor = Color.Green;
                txtSubject.Text = "";
                txtMessage.Text = "";
                txtSubject.Focus();
            }
            else
            {
                lblAlert.Text = "FAIL SEND OUT. PLEASE CONTACT ADMIN";
                lblAlert.ForeColor = Color.Red;
            }
        }
        public int sp_inquiry(string email, string subject, string category, string message, int flag)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Hotel"].ConnectionString);

            using (conn)
            {
                try
                {
                    conn.Open();
                    SqlParameter[] parms = { new SqlParameter("@email", email),
                                                new SqlParameter("@subject", subject),
                                                new SqlParameter("@category", category),
                                                new SqlParameter("@message", message),
                                                new SqlParameter("@flag", flag) };
                    SqlCommand Command = new SqlCommand("sp_inquiry", conn);
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.Parameters.AddRange(parms);
                    return Command.ExecuteNonQuery();
                }
                catch
                {
                    return 0;
                }
            }
        }
    }
}