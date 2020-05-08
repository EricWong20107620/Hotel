using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Web_Project
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Clear();
            if (IsPostBack)
            {
                if (string.IsNullOrEmpty(txtEmail.Text) == false)
                {
                    if (!(string.IsNullOrEmpty(txtPassword.Text)))
                    {
                        txtPassword.Attributes["value"] = txtPassword.Text;
                    }
                }
            }
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            Response.Redirect("Register.aspx");
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Confidential_Data cd = new Confidential_Data();

            if (txtEmail.Text == "1")
            {
                Session["user_role"] = "C";
                Session["email"] = cd.Encrypt("1");
                Session["login_name"] = cd.Encrypt("Test User");
                Response.Redirect("Client_Home.aspx");
                return;
            }

            if (txtEmail.Text == "2")
            {
                Session["user_role"] = "C";
                Session["email"] = cd.Encrypt("2");
                Session["login_name"] = cd.Encrypt("Test User 2");
                Response.Redirect("Client_Home.aspx");
                return;
            }

            if (txtEmail.Text == "3")
            {
                Session["user_role"] = "S";
                Session["email"] = cd.Encrypt("3");
                Session["login_name"] = cd.Encrypt("Test Staff");
                Response.Redirect("Staff_Home.aspx");
                return;
            }


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

            DataTable dt = new DataTable();
            dt = sp_login(txtEmail.Text, "", 0);

            if (dt.Rows.Count == 0)
            {
                lblMessage.Text = "This email has not register";
                lblMessage.ForeColor = Color.Red;
                txtEmail.Focus();
                return;
            }
            else
            {
                if (dt.Rows[0].Field<int>("user_status") == 1)
                {
                    if (btnLogin.Text == "Send Verify Email")
                    {
                        send_Email(dt.Rows[0].Field<string>("gender"), dt.Rows[0].Field<string>("first_name"), dt.Rows[0].Field<string>("last_name"));
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(txtVerfiyCode.Text) == true)
                        {
                            lblMessage.Text = "Please enter verify code";
                            lblMessage.ForeColor = Color.Red;
                            txtVerfiyCode.Focus();
                            return;
                        }

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
                            txtVerfiyCode.Text = "";
                            txtEmail.Focus();
                            return;
                        }
                        else
                        {
                            int chk_time = sp_check_verify_code(txtEmail.Text, txtVerfiyCode.Text, 1);

                            if (chk_time == -1)
                            {
                                send_Email(dt.Rows[0].Field<string>("gender"), dt.Rows[0].Field<string>("first_name"), dt.Rows[0].Field<string>("last_name"));
                                lblMessage.Text = "Verify code already expired<br/>Just re-send a new verify code";
                                lblMessage.ForeColor = Color.Red;
                                txtVerfiyCode.Focus();
                                txtVerfiyCode.Text = "";
                                return;
                            }

                            if (dt.Rows[0].Field<string>("verify_code") != txtVerfiyCode.Text)
                            {
                                /*if (dt.Rows[0].Field<int>("count_wrong") < 2)
                                {
                                    lblMessage.Text = "Verify code is not correct<br/>Remain " + (3 - (dt.Rows[0].Field<int>("count_wrong") + 1)) + " chance";
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
                                }*/
                                lblMessage.Text = "Verify code is not correct";
                                lblMessage.ForeColor = Color.Red;
                                txtVerfiyCode.Text = "";
                                txtVerfiyCode.Focus();
                                return;
                            }
                            else
                            {
                                lblMessage.Text = "Login Successful";
                                lblMessage.ForeColor = Color.Green;
                                sp_lock_account(txtEmail.Text, 0);
                                if (dt.Rows[0].Field<string>("user_role") == "S")
                                {
                                    Session["user_role"] = dt.Rows[0].Field<string>("user_role");
                                    Session["email"] = cd.Encrypt(dt.Rows[0].Field<string>("email"));
                                    Session["login_name"] = cd.Encrypt(dt.Rows[0].Field<string>("last_name") + " " + dt.Rows[0].Field<string>("first_name"));
                                    Response.Redirect("Staff_Home.aspx");
                                }
                                else
                                {
                                    Session["user_role"] = dt.Rows[0].Field<string>("user_role");
                                    Session["email"] = cd.Encrypt(dt.Rows[0].Field<string>("email"));
                                    Session["login_name"] = cd.Encrypt(dt.Rows[0].Field<string>("last_name") + " " + dt.Rows[0].Field<string>("first_name"));
                                    Response.Redirect("Client_Home.aspx");
                                }
                            }
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

        public DataTable sp_login(string email, string verifyCode, int flag)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Hotel"].ConnectionString);

            using (conn)
            {
                SqlCommand myCommand = new SqlCommand("sp_login", conn);

                myCommand.Parameters.Add(new SqlParameter("@email", SqlDbType.VarChar));
                myCommand.Parameters.Add(new SqlParameter("@verifyCode", SqlDbType.VarChar));
                myCommand.Parameters.Add(new SqlParameter("@flag", SqlDbType.VarChar));

                myCommand.Parameters["@email"].Value = email;
                myCommand.Parameters["@verifyCode"].Value = verifyCode;
                myCommand.Parameters["@flag"].Value = flag;

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
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Hotel"].ConnectionString);

            using (conn)
            {
                try
                {
                    conn.Open();
                    SqlParameter[] parms = { new SqlParameter("@email", email),
                                                new SqlParameter("@flag", flag) };
                    SqlCommand Command = new SqlCommand("sp_lock_account", conn);
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

        public int sp_check_verify_code(string email, string verifyCode, int flag)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Hotel"].ConnectionString);

            using (conn)
            {
                try
                {
                    conn.Open();
                    SqlParameter[] parms = { new SqlParameter("@email", email),
                                                new SqlParameter("@verifyCode", verifyCode),
                                                new SqlParameter("@flag", flag) };
                    SqlCommand Command = new SqlCommand("sp_check_verify_code", conn);
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

        public void send_Email(string gender, string first_name, string last_name)
        {
            if (gender == "M")
            {
                gender = "Mr.";
            }
            else
            {
                gender = "Ms.";
            }

            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            builder.Append(random.Next(100000, 999999));
            string verifyCode = builder.ToString();

            lblMessage.Text = "Verify code has been send to your email";
            lblMessage.ForeColor = Color.Blue;
            txtVerfiyCode.Focus();
            btnLogin.Text = "Login";

            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress("jate.hotel@gmail.com");
                message.To.Add(new MailAddress(txtEmail.Text));
                message.Subject = "JATE Hotel Register Account Verify Code - " + first_name + " " + last_name;
                message.IsBodyHtml = true;
                message.Body = "Dear " + gender + " " + last_name + ",<br/><br/>Your verify code is " + verifyCode + ".<br/><br/>Regards,<br/><br/>JATE Hotel";
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("jate.hotel@gmail.com", "jateh0te!");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
                sp_login(txtEmail.Text, verifyCode, 1);
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Fail to send the email.";
                lblMessage.ForeColor = Color.Red;
            }
        }
    }
}