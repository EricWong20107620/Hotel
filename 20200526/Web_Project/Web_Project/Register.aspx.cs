using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Web_Project
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Clear();
            if (IsPostBack)
            {
                if (!(string.IsNullOrEmpty(txtPassword.Text)))
                {
                    txtPassword.Attributes["value"] = txtPassword.Text;
                }

                if (!(string.IsNullOrEmpty(txtConfirmPassword.Text)))
                {
                    txtConfirmPassword.Attributes["value"] = txtConfirmPassword.Text;
                }
            }
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtEmail.Text) == true)
            {
                lblMessage.Text = "Please enter your email";
                lblMessage.ForeColor = Color.Red;
                txtEmail.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtFirstName.Text) == true)
            {
                lblMessage.Text = "Please enter your first name";
                lblMessage.ForeColor = Color.Red;
                txtFirstName.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtLastName.Text) == true)
            {
                lblMessage.Text = "Please enter your last name";
                lblMessage.ForeColor = Color.Red;
                txtLastName.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtPhone.Text) == true)
            {
                lblMessage.Text = "Please enter your phone number";
                lblMessage.ForeColor = Color.Red;
                txtPhone.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtPassport.Text) == true)
            {
                lblMessage.Text = "Please enter your passport number";
                lblMessage.ForeColor = Color.Red;
                txtPassport.Focus();
                return;
            }

            if (txtPassword.Enabled == true)
            {
                if (string.IsNullOrEmpty(txtPassword.Text) == true)
                {
                    lblMessage.Text = "Please enter your password";
                    lblMessage.ForeColor = Color.Red;
                    txtPassword.Focus();
                    return;
                }
                else
                {
                    if (txtConfirmPassword.Text != txtPassword.Text)
                    {
                        lblMessage.Text = "Confirm password is not correct";
                        lblMessage.ForeColor = Color.Red;
                        txtConfirmPassword.Focus();
                        return;
                    }
                    else
                    {
                        if (!(txtPassword.Text.Length >= 8 && txtPassword.Text.Length <= 15 && txtPassword.Text.Any(char.IsDigit) && txtPassword.Text.Any(char.IsLetter) && (txtPassword.Text.Any(char.IsSymbol) || txtPassword.Text.Any(char.IsPunctuation))))
                        {
                            lblMessage.Text = "Password length : 8 to 15 characters<br/>At leaset one lower-case letter<br/>At leaset one upper-case letter<br/>At leaset one special character";
                            lblMessage.ForeColor = Color.Red;
                            txtPassword.Focus();
                            return;
                        }
                    }
                }
            }

            if (string.IsNullOrEmpty(txtQuestionOne.Text) == true)
            {
                lblMessage.Text = "Please answer question one";
                lblMessage.ForeColor = Color.Red;
                txtQuestionOne.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtQuestionTwo.Text) == true)
            {
                lblMessage.Text = "Please answer question two";
                lblMessage.ForeColor = Color.Red;
                txtQuestionTwo.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtQuestionThree.Text) == true)
            {
                lblMessage.Text = "Please answer question three";
                lblMessage.ForeColor = Color.Red;
                txtQuestionThree.Focus();
                return;
            }

            if (btnRegister.Text == "Send Verify Email")
            {
                send_Email();
            }
            else
            {
                if (string.IsNullOrEmpty(txtVerfiyCode.Text) == true)
                {
                    lblMessage.Text = "Please enter your verify code";
                    lblMessage.ForeColor = Color.Red;
                    txtVerfiyCode.Focus();
                    return;
                }

                int chk_time = sp_check_verify_code(txtEmail.Text, txtVerfiyCode.Text, 1);
                int chk_same = sp_check_verify_code(txtEmail.Text, txtVerfiyCode.Text, 2);

                if (chk_time == -1)
                {
                    send_Email();
                    lblMessage.Text = "Verify code already expired<br/>Just re-send a new verify code";
                    lblMessage.ForeColor = Color.Red;
                    txtVerfiyCode.Focus();
                    return;
                }

                if (chk_same == 1)
                {
                    lblMessage.Text = "Register successful";
                    lblMessage.ForeColor = Color.Green;
                    btnRegister.Text = "Send Verify Email";
                    txtEmail.Enabled = true;
                    txtFirstName.Enabled = true;
                    txtLastName.Enabled = true;
                    ddlGender.Enabled = true;
                    txtPhone.Enabled = true;
                    txtPassport.Enabled = true;
                    txtPassword.Enabled = true;
                    txtConfirmPassword.Enabled = true;
                    txtQuestionOne.Enabled = true;
                    txtQuestionTwo.Enabled = true;
                    txtQuestionThree.Enabled = true;
                }
                else if (chk_same == -1)
                {
                    lblMessage.Text = "Verify code is wrong";
                    lblMessage.ForeColor = Color.Red;
                    txtVerfiyCode.Focus();
                }
                else
                {
                    lblMessage.Text = "FAIL REGISTER. PLEASE CONTACT ADMIN";
                    lblMessage.ForeColor = Color.Red;
                }
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }

        public void send_Email()
        {
            string gender;

            if (ddlGender.SelectedValue.ToString() == "M")
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

            int result = sp_register(txtEmail.Text, txtPassword.Text, txtFirstName.Text, txtLastName.Text, txtPassport.Text, ddlGender.SelectedValue.ToString(), txtPhone.Text, txtQuestionOne.Text, txtQuestionTwo.Text, txtQuestionThree.Text, verifyCode);

            if (result == 1)
            {
                lblMessage.Text = "Verify code has been send to your email";
                lblMessage.ForeColor = Color.Blue;
                txtVerfiyCode.Focus();
                btnRegister.Text = "Register";
                txtEmail.Enabled = false;
                txtFirstName.Enabled = false;
                txtLastName.Enabled = false;
                ddlGender.Enabled = false;
                txtPhone.Enabled = false;
                txtPassport.Enabled = false;
                txtPassword.Enabled = false;
                txtConfirmPassword.Enabled = false;
                txtQuestionOne.Enabled = false;
                txtQuestionTwo.Enabled = false;
                txtQuestionThree.Enabled = false;
            }
            else if (result == -1)
            {
                lblMessage.Text = "This email has been register";
                lblMessage.ForeColor = Color.Red;
                txtEmail.Focus();
                return;
            }
            else
            {
                lblMessage.Text = "FAIL REGISTER. PLEASE CONTACT ADMIN";
                lblMessage.ForeColor = Color.Red;
                return;
            }

            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress("jate.hotel@gmail.com");
                message.To.Add(new MailAddress(txtEmail.Text));
                message.Subject = "JATE Hotel Register Account Verify Code - " + txtFirstName.Text + " " + txtLastName.Text;
                message.IsBodyHtml = true;
                message.Body = "Dear " + gender + " " + txtLastName.Text + ",<br/><br/>Your verify code is " + verifyCode + ".<br/><br/>Regards,<br/><br/>JATE Hotel";
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("jate.hotel@gmail.com", "jateh0te!");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Fail to send the email.";
                lblMessage.ForeColor = Color.Red;
            }
        }

        public int sp_register(string email, string password, string firstName, string lastName, string passport, string gender, string phoneNumber, string questionOne, string questionTwo, string questionThree, string verifyCode)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Hotel"].ConnectionString);

            using (conn)
            {
                try
                {
                    conn.Open();
                    SqlParameter[] parms = { new SqlParameter("@email", email),
                                                new SqlParameter("@password", password),
                                                new SqlParameter("@firstName", firstName),
                                                new SqlParameter("@lastName", lastName),
                                                new SqlParameter("@passport", passport),
                                                new SqlParameter("@gender", gender),
                                                new SqlParameter("@phoneNumber", phoneNumber),
                                                new SqlParameter("@questionOne", questionOne),
                                                new SqlParameter("@questionTwo", questionTwo),
                                                new SqlParameter("@questionThree", questionThree),
                                                new SqlParameter("@verifyCode", verifyCode) };
                    SqlCommand Command = new SqlCommand("sp_register", conn);
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
    }
}