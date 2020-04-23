using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web_Project
{
    public partial class Handle_Inquiry : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["login_name"] == null || Session["user_role"].ToString() != "S")
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (!Page.IsPostBack)
            {
                sp_refresh_inquiry_master("", ddlStatus.SelectedValue, 1);
            }
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSubject.Text) == true)
            {
                lblAlert.Text = "Please select your inquiry";
                lblAlert.ForeColor = Color.Red;
                txtSubject.Focus();
                return;
            }

            if (txtStatus.Text == "Closed")
            {
                lblAlert.Text = "This inquiry already closed.<br/>The message cannot send out.";
                lblAlert.ForeColor = Color.Red;
                return;
            }
            else
            {
                int result = sp_inquiry(hfIssueId2.Value, Session["email"].ToString(), txtSubject.Text, txtCategory.Text, txtMessage.Text, 3);
                if (result >= 0)
                {
                    lblAlert.Text = "You have reply to client.";
                    lblAlert.ForeColor = Color.Green;
                    sp_refresh_inquiry_master("", ddlStatus.SelectedValue, 1);
                    sp_refresh_inquiry_detail(hfIssueId2.Value, 1);
                    txtMessage.Text = "";
                    txtMessage.Focus();
                }
                else
                {
                    lblAlert.Text = "FAIL SEND OUT. PLEASE CONTACT ADMIN";
                    lblAlert.ForeColor = Color.Red;
                }
            }
        }

        protected void InquiryList_ItemCommand(object source, DataListCommandEventArgs e)
        {
            HiddenField hf = (HiddenField)e.Item.FindControl("hfIssueId");
            sp_refresh_inquiry_detail(hf.Value, 1);
        }

        public int sp_inquiry(string id, string email, string subject, string category, string message, int flag)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Hotel"].ConnectionString);

            using (conn)
            {
                try
                {
                    conn.Open();
                    SqlParameter[] parms = { new SqlParameter("@id", id),
                                                new SqlParameter("@email", email),
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

        public void sp_refresh_inquiry_master(string email, string status, int flag)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Hotel"].ConnectionString);

            using (conn)
            {
                SqlCommand myCommand = new SqlCommand("sp_refresh_inquiry_master", conn);

                myCommand.Parameters.Add(new SqlParameter("@email", SqlDbType.VarChar));
                myCommand.Parameters.Add(new SqlParameter("@status", SqlDbType.VarChar));
                myCommand.Parameters.Add(new SqlParameter("@flag", SqlDbType.VarChar));

                myCommand.Parameters["@email"].Value = email;
                myCommand.Parameters["@status"].Value = status;
                myCommand.Parameters["@flag"].Value = flag;

                myCommand.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter sqlAdapter = new SqlDataAdapter(myCommand);

                DataTable dt = new DataTable();
                sqlAdapter.Fill(dt);
                conn.Close();
                myCommand.Dispose();

                InquiryList.DataSource = dt;
                InquiryList.DataBind();
            }
        }

        public void sp_refresh_inquiry_detail(string id, int flag)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Hotel"].ConnectionString);

            using (conn)
            {
                SqlCommand myCommand = new SqlCommand("sp_refresh_inquiry_detail", conn);

                myCommand.Parameters.Add(new SqlParameter("@id", SqlDbType.VarChar));
                myCommand.Parameters.Add(new SqlParameter("@flag", SqlDbType.VarChar));

                myCommand.Parameters["@id"].Value = id;
                myCommand.Parameters["@flag"].Value = flag;

                myCommand.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter sqlAdapter = new SqlDataAdapter(myCommand);

                DataTable dt = new DataTable();
                sqlAdapter.Fill(dt);
                conn.Close();
                myCommand.Dispose();

                txtSubject.Text = dt.Rows[0]["issue_subject"].ToString();
                txtCategory.Text = dt.Rows[0]["issue_category"].ToString();
                txtStatus.Text = dt.Rows[0]["inquiry_status"].ToString();
                hfIssueId2.Value = dt.Rows[0]["issue_id"].ToString();
                InquiryDetail.DataSource = dt;
                InquiryDetail.DataBind();
            }
        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            sp_refresh_inquiry_master("", ddlStatus.SelectedValue, 1);
        }

        protected void btnAutoClose_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Hotel"].ConnectionString);

            using (conn)
            {
                SqlCommand cmd = new SqlCommand("UPDATE inquiry_master SET inquiry_status = 'C' WHERE DATEADD(DD, 10, create_date) <= GETDATE()", conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                sp_refresh_inquiry_master("", ddlStatus.SelectedValue, 1);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert Message", "alert('Successful close all no action over 10 days inquiries')", true);
                return;
            }
        }
    }
}