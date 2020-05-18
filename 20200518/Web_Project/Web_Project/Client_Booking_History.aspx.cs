using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI.WebControls;

namespace Web_Project
{
    public partial class Client_Booking_History : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Confidential_Data cd = new Confidential_Data();

            if (Session["login_name"] == null || Session["user_role"].ToString() != "C")
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                sp_refresh_booking(cd.Decrypt(Session["email"].ToString()), 1);
            }
        }

        protected void txtCheckInDate_TextChanged(object sender, EventArgs e)
        {
            if (txtCheckInDate.Text != "" && txtCheckOutDate.Text != "")
            {
                if (Convert.ToDateTime(txtCheckInDate.Text) > Convert.ToDateTime(txtCheckOutDate.Text))
                {
                    lblMessage.Text = "Check out date must less than check in date";
                    lblMessage.ForeColor = Color.Red;
                    txtCheckInDate.Focus();
                    txtCheckInDate.Text = "";
                    txtCheckOutDate.Text = "";
                    return;
                }
                refresh_room_status();
            }

            cal_total_price();
        }

        protected void txtCheckOutDate_TextChanged(object sender, EventArgs e)
        {
            if (txtCheckInDate.Text != "" && txtCheckOutDate.Text != "")
            {
                if (Convert.ToDateTime(txtCheckInDate.Text) > Convert.ToDateTime(txtCheckOutDate.Text))
                {
                    lblMessage.Text = "Check out date must less than check in date";
                    lblMessage.ForeColor = Color.Red;
                    txtCheckInDate.Focus();
                    txtCheckInDate.Text = "";
                    txtCheckOutDate.Text = "";
                    return;
                }
                refresh_room_status();
            }

            cal_total_price();
        }

        protected void ddlStandardRoomAmount_SelectedIndexChanged(object sender, EventArgs e)
        {
            cal_total_price();
        }

        protected void ddlDeluxeRoomAmount_SelectedIndexChanged(object sender, EventArgs e)
        {
            cal_total_price();
        }

        protected void ddlStudioRoomAmount_SelectedIndexChanged(object sender, EventArgs e)
        {
            cal_total_price();
        }

        protected void BookingList_ItemCommand(object source, DataListCommandEventArgs e)
        {
            Confidential_Data cd = new Confidential_Data();
            HiddenField hf = (HiddenField)e.Item.FindControl("hfBookingId");
            DataTable dt = sp_booking_detail(hf.Value);
            DateTime check_in_date = dt.Rows[0].Field<DateTime>("check_in_date");
            DateTime check_out_date = dt.Rows[0].Field<DateTime>("check_out_date");
            int standard = dt.Rows[0].Field<int>("standard_room_amount");
            int deluxe = dt.Rows[0].Field<int>("deluxe_room_amount");
            int studio = dt.Rows[0].Field<int>("studio_room_amount");
            int price = dt.Rows[0].Field<int>("price");
            hfBookingId2.Value = hf.Value;
            txtCheckInDate.Text = check_in_date.ToString("yyyy-MM-dd");
            txtCheckOutDate.Text = check_out_date.ToString("yyyy-MM-dd");
            ddlBedType.SelectedValue = dt.Rows[0].Field<string>("bed_type");
            ddlStandardRoomAmount.SelectedValue = standard.ToString();
            ddlDeluxeRoomAmount.SelectedValue = deluxe.ToString();
            ddlStudioRoomAmount.SelectedValue = studio.ToString();
            txtTotalPrice.Text = price.ToString();
            txtCreditCard.Text = cd.Decrypt(dt.Rows[0].Field<string>("credit_card"));
            txtRemark.Text = dt.Rows[0].Field<string>("remarks");
            refresh_room_status();
            lblMessage.Text = "";
            if (DateTime.Today >= check_in_date.AddDays(-3))
            {
                btnUpdate.Visible = false;
            }
            else
            {
                btnUpdate.Visible = true;
            }
        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            Confidential_Data cd = new Confidential_Data();

            if (btnFilter.Text == "Show all booking")
            {
                sp_refresh_booking(cd.Decrypt(Session["email"].ToString()), 2);
                btnFilter.Text = "Only show available booking";
            }
            else
            {
                sp_refresh_booking(cd.Decrypt(Session["email"].ToString()), 1);
                btnFilter.Text = "Show all booking";
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            Confidential_Data cd = new Confidential_Data();

            if (Session["login_name"] == null || Session["user_role"].ToString() != "C")
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (string.IsNullOrEmpty(hfBookingId2.Value) == true)
            {
                lblMessage.Text = "Please select the booking";
                lblMessage.ForeColor = Color.Red;
                txtCheckInDate.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtCheckInDate.Text) == true)
            {
                lblMessage.Text = "Please select the check in date";
                lblMessage.ForeColor = Color.Red;
                txtCreditCard.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtCheckOutDate.Text) == true)
            {
                lblMessage.Text = "Please select the check out date";
                lblMessage.ForeColor = Color.Red;
                txtCreditCard.Focus();
                return;
            }

            if (ddlStandardRoomAmount.SelectedValue == "0" && ddlDeluxeRoomAmount.SelectedValue == "0" && ddlStudioRoomAmount.SelectedValue == "0")
            {
                lblMessage.Text = "At lease book one room";
                lblMessage.ForeColor = Color.Red;
                ddlStandardRoomAmount.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtCreditCard.Text) == true)
            {
                lblMessage.Text = "Please enter your credit card number";
                lblMessage.ForeColor = Color.Red;
                txtCreditCard.Focus();
                return;
            }

            if (Convert.ToDateTime(txtCheckInDate.Text) < DateTime.Today)
            {
                lblMessage.Text = "Check in date must equal today";
                lblMessage.ForeColor = Color.Red;
                txtCheckInDate.Text = "";
                txtCheckInDate.Focus();
                return;
            }

            DataTable dt = sp_room_status(txtCheckInDate.Text, txtCheckOutDate.Text);

            if (ddlStandardRoomAmount.SelectedValue != "0")
            {
                if (dt.Rows[0].Field<int>("total_standard") - dt.Rows[0].Field<int>("booked_standard") - Convert.ToInt32(ddlStandardRoomAmount.SelectedValue) <= 0)
                {
                    lblMessage.Text = "No available standard room in this date range";
                    lblMessage.ForeColor = Color.Red;
                    txtCreditCard.Focus();
                    return;
                }
            }

            if (ddlDeluxeRoomAmount.SelectedValue != "0")
            {
                if (dt.Rows[0].Field<int>("total_deluxe") - dt.Rows[0].Field<int>("booked_deluxe") - Convert.ToInt32(ddlDeluxeRoomAmount.SelectedValue) <= 0)
                {
                    lblMessage.Text = "No available deluxe room in this date range";
                    lblMessage.ForeColor = Color.Red;
                    txtCreditCard.Focus();
                    return;
                }
            }

            if (ddlStudioRoomAmount.SelectedValue != "0")
            {
                if (dt.Rows[0].Field<int>("total_studio") - dt.Rows[0].Field<int>("booked_studio") - Convert.ToInt32(ddlStudioRoomAmount.SelectedValue) <= 0)
                {
                    lblMessage.Text = "No available studio room in this date range";
                    lblMessage.ForeColor = Color.Red;
                    txtCreditCard.Focus();
                    return;
                }
            }

            if (sp_update_booking(hfBookingId2.Value, txtCheckInDate.Text, txtCheckOutDate.Text, ddlBedType.SelectedValue, ddlStandardRoomAmount.SelectedValue, ddlDeluxeRoomAmount.SelectedValue, ddlStudioRoomAmount.SelectedValue, txtTotalPrice.Text, txtCreditCard.Text, txtRemark.Text) >= 1)
            {
                lblMessage.Text = "Update Successful";
                lblMessage.ForeColor = Color.Green;
                refresh_room_status();
                field_data_control();
                if (btnFilter.Text == "Show all booking")
                {
                    sp_refresh_booking(cd.Decrypt(Session["email"].ToString()), 1);
                }
                else
                {
                    sp_refresh_booking(cd.Decrypt(Session["email"].ToString()), 2);
                }
            }
            else
            {
                lblMessage.Text = "Booking Fail. Please contact admin";
                lblMessage.ForeColor = Color.Red;
            }
        }

        public void field_data_control()
        {
            txtCheckInDate.Text = "";
            txtCheckOutDate.Text = "";
            ddlBedType.SelectedValue = "King Bed";
            ddlStandardRoomAmount.SelectedValue = "0";
            ddlDeluxeRoomAmount.SelectedValue = "0";
            ddlStudioRoomAmount.SelectedValue = "0";
            txtTotalPrice.Text = "";
            txtCreditCard.Text = "";
            txtRemark.Text = "";
        }

        public void cal_total_price()
        {
            if (txtCheckInDate.Text != "" && txtCheckOutDate.Text != "")
            {
                TimeSpan range = Convert.ToDateTime(txtCheckOutDate.Text) - Convert.ToDateTime(txtCheckInDate.Text);
                int dt_standard = sp_room_detail("1").Rows[0].Field<int>("price_per_day");
                int dt_Deluxe = sp_room_detail("2").Rows[0].Field<int>("price_per_day");
                int dt_Studio = sp_room_detail("3").Rows[0].Field<int>("price_per_day");
                int days = Convert.ToInt32(range.Days);
                if (days == 0)
                {
                    days = 1;
                }
                txtTotalPrice.Text = Convert.ToString((Convert.ToInt16(ddlStandardRoomAmount.SelectedValue) * dt_standard) + (Convert.ToInt16(ddlDeluxeRoomAmount.SelectedValue) * dt_Deluxe) + (Convert.ToInt16(ddlStudioRoomAmount.SelectedValue) * dt_Studio));
            }
        }

        public void refresh_room_status()
        {
            if (txtCheckInDate.Text != "")
            {
                DataTable dt = sp_room_status(txtCheckInDate.Text, txtCheckOutDate.Text);
                if (dt.Rows[0].Field<int>("total_standard") - dt.Rows[0].Field<int>("booked_standard") >= Convert.ToInt32(dt.Rows[0].Field<int>("total_standard") / 2))
                {
                    lblStandardRoomAmount.ForeColor = Color.Green;
                    lblStandardRoomAmount.Text = dt.Rows[0].Field<int>("total_standard") - dt.Rows[0].Field<int>("booked_standard") + " / " + dt.Rows[0].Field<int>("total_standard");
                }
                else if (dt.Rows[0].Field<int>("total_standard") - dt.Rows[0].Field<int>("booked_standard") >= Convert.ToInt32(dt.Rows[0].Field<int>("total_standard") / 3) && dt.Rows[0].Field<int>("total_standard") - dt.Rows[0].Field<int>("booked_standard") < Convert.ToInt32(dt.Rows[0].Field<int>("total_standard") / 2))
                {
                    lblStandardRoomAmount.ForeColor = Color.Orange;
                    lblStandardRoomAmount.Text = dt.Rows[0].Field<int>("total_standard") - dt.Rows[0].Field<int>("booked_standard") + " / " + dt.Rows[0].Field<int>("total_standard");
                }
                else
                {
                    lblStandardRoomAmount.ForeColor = Color.Red;
                    lblStandardRoomAmount.Text = dt.Rows[0].Field<int>("total_standard") - dt.Rows[0].Field<int>("booked_standard") + " / " + dt.Rows[0].Field<int>("total_standard");
                }

                if (dt.Rows[0].Field<int>("total_deluxe") - dt.Rows[0].Field<int>("booked_deluxe") >= Convert.ToInt32(dt.Rows[0].Field<int>("total_deluxe") / 2))
                {
                    lblDeluxeRoomAmount.ForeColor = Color.Green;
                    lblDeluxeRoomAmount.Text = dt.Rows[0].Field<int>("total_deluxe") - dt.Rows[0].Field<int>("booked_deluxe") + " / " + dt.Rows[0].Field<int>("total_deluxe");
                }
                else if (dt.Rows[0].Field<int>("total_deluxe") - dt.Rows[0].Field<int>("booked_deluxe") >= Convert.ToInt32(dt.Rows[0].Field<int>("total_deluxe") / 3) && dt.Rows[0].Field<int>("total_deluxe") - dt.Rows[0].Field<int>("booked_deluxe") < Convert.ToInt32(dt.Rows[0].Field<int>("total_deluxe") / 2))
                {
                    lblDeluxeRoomAmount.ForeColor = Color.Orange;
                    lblDeluxeRoomAmount.Text = dt.Rows[0].Field<int>("total_deluxe") - dt.Rows[0].Field<int>("booked_deluxe") + " / " + dt.Rows[0].Field<int>("total_deluxe");
                }
                else
                {
                    lblDeluxeRoomAmount.ForeColor = Color.Red;
                    lblDeluxeRoomAmount.Text = dt.Rows[0].Field<int>("total_deluxe") - dt.Rows[0].Field<int>("booked_deluxe") + " / " + dt.Rows[0].Field<int>("total_deluxe");
                }

                if (dt.Rows[0].Field<int>("total_studio") - dt.Rows[0].Field<int>("booked_studio") >= Convert.ToInt32(dt.Rows[0].Field<int>("total_studio") / 2))
                {
                    lblStudioRoomAmount.ForeColor = Color.Green;
                    lblStudioRoomAmount.Text = dt.Rows[0].Field<int>("total_studio") - dt.Rows[0].Field<int>("booked_studio") + " / " + dt.Rows[0].Field<int>("total_studio");
                }
                else if (dt.Rows[0].Field<int>("total_studio") - dt.Rows[0].Field<int>("booked_studio") >= Convert.ToInt32(dt.Rows[0].Field<int>("total_studio") / 3) && dt.Rows[0].Field<int>("total_studio") - dt.Rows[0].Field<int>("booked_studio") < Convert.ToInt32(dt.Rows[0].Field<int>("total_studio") / 2))
                {
                    lblStudioRoomAmount.ForeColor = Color.Orange;
                    lblStudioRoomAmount.Text = dt.Rows[0].Field<int>("total_studio") - dt.Rows[0].Field<int>("booked_studio") + " / " + dt.Rows[0].Field<int>("total_studio");
                }
                else
                {
                    lblStudioRoomAmount.ForeColor = Color.Red;
                    lblStudioRoomAmount.Text = dt.Rows[0].Field<int>("total_studio") - dt.Rows[0].Field<int>("booked_studio") + " / " + dt.Rows[0].Field<int>("total_studio");
                }
            }
        }

        public DataTable sp_room_detail(string room_id)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Hotel"].ConnectionString);

            using (conn)
            {
                SqlCommand myCommand = new SqlCommand("sp_room_detail", conn);

                myCommand.Parameters.Add(new SqlParameter("@room_id", SqlDbType.VarChar));

                myCommand.Parameters["@room_id"].Value = room_id;

                myCommand.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter sqlAdapter = new SqlDataAdapter(myCommand);

                DataTable dt = new DataTable();
                sqlAdapter.Fill(dt);
                conn.Close();
                myCommand.Dispose();

                return dt;
            }
        }

        public DataTable sp_room_status(string check_in_date, string check_out_date)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Hotel"].ConnectionString);

            using (conn)
            {
                SqlCommand myCommand = new SqlCommand("sp_room_status", conn);

                myCommand.Parameters.Add(new SqlParameter("@check_in_date", SqlDbType.VarChar));
                myCommand.Parameters.Add(new SqlParameter("@check_out_date", SqlDbType.VarChar));

                myCommand.Parameters["@check_in_date"].Value = check_in_date;
                myCommand.Parameters["@check_out_date"].Value = check_out_date;

                myCommand.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter sqlAdapter = new SqlDataAdapter(myCommand);

                DataTable dt = new DataTable();
                sqlAdapter.Fill(dt);
                conn.Close();
                myCommand.Dispose();

                return dt;
            }
        }

        public void sp_refresh_booking(string email, int flag)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Hotel"].ConnectionString);

            using (conn)
            {
                SqlCommand myCommand = new SqlCommand("sp_refresh_booking", conn);

                myCommand.Parameters.Add(new SqlParameter("@email", SqlDbType.VarChar));
                myCommand.Parameters.Add(new SqlParameter("@flag", SqlDbType.VarChar));

                myCommand.Parameters["@email"].Value = email;
                myCommand.Parameters["@flag"].Value = flag;

                myCommand.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter sqlAdapter = new SqlDataAdapter(myCommand);

                DataTable dt = new DataTable();
                sqlAdapter.Fill(dt);
                conn.Close();
                myCommand.Dispose();

                BookingList.DataSource = dt;
                BookingList.DataBind();
            }
        }

        public DataTable sp_booking_detail(string id)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Hotel"].ConnectionString);

            using (conn)
            {
                SqlCommand myCommand = new SqlCommand("sp_booking_detail", conn);

                myCommand.Parameters.Add(new SqlParameter("@id", SqlDbType.VarChar));

                myCommand.Parameters["@id"].Value = id;

                myCommand.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter sqlAdapter = new SqlDataAdapter(myCommand);

                DataTable dt = new DataTable();
                sqlAdapter.Fill(dt);
                conn.Close();
                myCommand.Dispose();

                return dt;
            }
        }

        public int sp_update_booking(string id, string check_in_date, string check_out_date, string bed_type, string standard_room_amount, string deluxe_room_amount, string studio_room_amount, string price, string credit_card, string remarks)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Hotel"].ConnectionString);
            Confidential_Data cd = new Confidential_Data();

            using (conn)
            {
                try
                {
                    conn.Open();
                    SqlParameter[] parms = { new SqlParameter("@id", id),
                                                new SqlParameter("@check_in_date", check_in_date),
                                                new SqlParameter("@check_out_date", check_out_date),
                                                new SqlParameter("@bed_type", bed_type),
                                                new SqlParameter("@standard_room_amount", standard_room_amount),
                                                new SqlParameter("@deluxe_room_amount", deluxe_room_amount),
                                                new SqlParameter("@studio_room_amount", studio_room_amount),
                                                new SqlParameter("@price", price),
                                                new SqlParameter("@credit_card", cd.Encrypt(credit_card)),
                                                new SqlParameter("@remarks", remarks) };
                    SqlCommand Command = new SqlCommand("sp_update_booking", conn);
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