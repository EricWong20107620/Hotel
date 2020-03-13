using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web_Project
{
    public partial class Login : System.Web.UI.Page
    {
        public SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Hotel"].ConnectionString);
        public SqlConnection conn2 = new SqlConnection(ConfigurationManager.ConnectionStrings["Hotel"].ConnectionString);
        string questino_no;

        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Clear();

            if (IsPostBack)
            {
                questino_no = lblQuestion.Text.Substring(1, 1);
            }

            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            builder.Append(random.Next(1, 4));
            string randomNo = builder.ToString();

            if (randomNo == "1")
            {
                lblQuestion.Text = "Q1. What’s your hobby ?";
            }
            else if (randomNo == "2")
            {
                lblQuestion.Text = "Q2. What’s your favorite country ?";
            }
            else
            {
                lblQuestion.Text = "Q3. Where are you born?";
            }
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            Response.Redirect("Register.aspx");
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtEmail.Text) == true)
            {
                lblMessage.Text = "Please enter your email";
                lblMessage.ForeColor = Color.Red;
                txtEmail.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtPassword.Text) == true)
            {
                lblMessage.Text = "Please enter your password";
                lblMessage.ForeColor = Color.Red;
                txtPassword.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtQuestion.Text) == true)
            {
                lblMessage.Text = "Please answer the security question";
                lblMessage.ForeColor = Color.Red;
                txtQuestion.Focus();
                return;
            }

            DataTable dt = new DataTable();
            dt = sp_login(txtEmail.Text, questino_no);

            if (dt.Rows.Count == 0)
            {
                lblMessage.Text = "This email has not register";
                lblMessage.ForeColor = Color.Red;
                txtEmail.Text = "";
                txtPassword.Text = "";
                txtQuestion.Text = "";
                txtEmail.Focus();
                return;
            }
            else
            {
                if (dt.Rows[0].Field<int>("user_status") == 1)
                {
                    if (dt.Rows[0].Field<string>("login_password") != txtPassword.Text)
                    {
                        if (dt.Rows[0].Field<int>("count_wrong") < 2)
                        {
                            lblMessage.Text = "Email or password is not correct<br/>Remain " + (3 - (dt.Rows[0].Field<int>("count_wrong") + 1)) + " chance";
                            lblMessage.ForeColor = Color.Red;
                            sp_lock_account(txtEmail.Text, 1);
                        }
                        else if (dt.Rows[0].Field<int>("count_wrong") == 2)
                        {
                            lblMessage.Text = "Your account has been locked<br/>Please contact admin";
                            lblMessage.ForeColor = Color.Red;
                            sp_lock_account(txtEmail.Text, 2);
                        }
                        else
                        {
                            lblMessage.Text = "Your account has been locked<br/>Please contact admin";
                            lblMessage.ForeColor = Color.Red;
                        }
                        txtEmail.Text = "";
                        txtPassword.Text = "";
                        txtQuestion.Text = "";
                        txtEmail.Focus();
                        return;
                    }
                    else
                    {
                        if (dt.Rows[0].Field<string>("question_answer") != txtQuestion.Text)
                        {
                            if (dt.Rows[0].Field<int>("count_wrong") < 2)
                            {
                                lblMessage.Text = "Security question is not correct<br/>Remain " + (3 - (dt.Rows[0].Field<int>("count_wrong") + 1)) + " chance";
                                lblMessage.ForeColor = Color.Red;
                                sp_lock_account(txtEmail.Text, 1);
                            }
                            else if (dt.Rows[0].Field<int>("count_wrong") == 2)
                            {
                                lblMessage.Text = "Your account has been locked<br/>Please contact admin";
                                lblMessage.ForeColor = Color.Red;
                                sp_lock_account(txtEmail.Text, 2);
                            }
                            else
                            {
                                lblMessage.Text = "Your account has been locked<br/>Please contact admin";
                                lblMessage.ForeColor = Color.Red;
                            }
                            txtQuestion.Text = "";
                            txtQuestion.Focus();
                            return;
                        }
                        else
                        {
                            lblMessage.Text = "Login Successful";
                            lblMessage.ForeColor = Color.Green;
                            sp_lock_account(txtEmail.Text, 0);
                        }
                    }
                }
                else if (dt.Rows[0].Field<int>("user_status") == 2)
                {
                    lblMessage.Text = "Your account has been locked<br/>Please contact admin";
                    lblMessage.ForeColor = Color.Red;
                }
                else
                {
                    lblMessage.Text = "This email has not register";
                    lblMessage.ForeColor = Color.Red;
                }
            }
        }

        public DataTable sp_login(string email, string questionNo)
        {
            using (conn)
            {
                SqlCommand myCommand = new SqlCommand("sp_login", conn);

                myCommand.Parameters.Add(new SqlParameter("@email", SqlDbType.VarChar));
                myCommand.Parameters.Add(new SqlParameter("@questionNo", SqlDbType.VarChar));

                myCommand.Parameters["@email"].Value = txtEmail.Text;
                myCommand.Parameters["@questionNo"].Value = questino_no;

                myCommand.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter sqlAdapter = new SqlDataAdapter(myCommand);

                DataTable dt = new DataTable();
                sqlAdapter.Fill(dt);
                conn.Close();
                myCommand.Dispose();

                return dt;
            }
        }

        public int sp_lock_account(string email, int flag)
        {
            using (conn2)
            {
                try
                {
                    conn2.Open();
                    SqlParameter[] parms = { new SqlParameter("@email", email),
                                                new SqlParameter("@flag", flag) };
                    SqlCommand Command = new SqlCommand("sp_lock_account", conn2);
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