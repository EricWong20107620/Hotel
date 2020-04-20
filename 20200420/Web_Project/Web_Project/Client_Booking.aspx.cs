using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;

namespace Web_Project
{
    public partial class Client_Booking : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["login_name"] == null || Session["user_role"].ToString() != "C")
            {
                Response.Redirect("Login.aspx");
                return;
            }
        }

        protected void ddlRoomType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlRoomType.SelectedValue.ToString() != "0")
            {
                DataTable dt = sp_room_detail(ddlRoomType.SelectedValue.ToString());
                int price = dt.Rows[0].Field<int>("price_per_day");

                txtMaxOccupancy.Text = dt.Rows[0].Field<string>("max_occupancy");
                txtRoomSize.Text = dt.Rows[0].Field<string>("room_size");
                txtBathTub.Text = dt.Rows[0].Field<string>("bath_tub");
                txtDesignerChair.Text = dt.Rows[0].Field<string>("designer_chair");
                txtInternet.Text = dt.Rows[0].Field<string>("internet");
                txtSmartTV.Text = dt.Rows[0].Field<string>("smart_tv");
                txtHairDeyer.Text = dt.Rows[0].Field<string>("hair_deyer");
                txtInRoomSafe.Text = dt.Rows[0].Field<string>("in_room_safe");
                txtMiniFridge.Text = dt.Rows[0].Field<string>("mini_fridge");
                txtReplenished.Text = dt.Rows[0].Field<string>("replenished");
                txtHouseKeeping.Text = dt.Rows[0].Field<string>("house_keeping");
                txtBedSofa.Text = dt.Rows[0].Field<string>("bed_sofa");
                txtOptionalBreakfast.Text = dt.Rows[0].Field<string>("optional_breakfast");
                txtPricePerDay.Text = price.ToString();
                if (txtCheckInDate.Text != "" && txtCheckOutDate.Text != "")
                {
                    TimeSpan range = Convert.ToDateTime(txtCheckOutDate.Text) - Convert.ToDateTime(txtCheckInDate.Text);
                    int days = Convert.ToInt32(range.Days);
                    if (days == 0)
                    {
                        days = 1;
                    }
                    txtTotalPrice.Text = Convert.ToString(price * days);
                }
            }
        }


        protected void txtCheckInDate_TextChanged(object sender, EventArgs e)
        {
            refresh_room_status();

            if (txtCheckInDate.Text != "" && txtCheckOutDate.Text != "")
            {
                if (Convert.ToDateTime(txtCheckInDate.Text) > Convert.ToDateTime(txtCheckOutDate.Text))
                {
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert Message", "alert('Check out date must less than check in date')", true);
                    lblMessage.Text = "Check out date must less than check in date";
                    lblMessage.ForeColor = Color.Red;
                    txtCheckInDate.Focus();
                    txtCheckInDate.Text = "";
                    txtCheckOutDate.Text = "";
                    return;
                }
            }

            if (txtPricePerDay.Text != "" && txtCheckInDate.Text != "" && txtCheckOutDate.Text != "")
            {
                TimeSpan range = Convert.ToDateTime(txtCheckOutDate.Text) - Convert.ToDateTime(txtCheckInDate.Text);
                int price = Convert.ToInt32(txtPricePerDay.Text);
                int days = Convert.ToInt32(range.Days);
                if (days == 0)
                {
                    days = 1;
                }
                txtTotalPrice.Text = Convert.ToString(price * days);
            }
        }

        protected void txtCheckOutDate_TextChanged(object sender, EventArgs e)
        {
            if (txtCheckInDate.Text != "" && txtCheckOutDate.Text != "")
            {
                if (Convert.ToDateTime(txtCheckInDate.Text) > Convert.ToDateTime(txtCheckOutDate.Text))
                {
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert Message", "alert('Check out date must less than check in date')", true);
                    lblMessage.Text = "Check out date must less than check in date";
                    lblMessage.ForeColor = Color.Red;
                    txtCheckInDate.Focus();
                    txtCheckInDate.Text = "";
                    txtCheckOutDate.Text = "";
                    return;
                }
            }

            if (txtPricePerDay.Text != "" && txtCheckInDate.Text != "" && txtCheckOutDate.Text != "")
            {
                TimeSpan range = Convert.ToDateTime(txtCheckOutDate.Text) - Convert.ToDateTime(txtCheckInDate.Text);
                int price = Convert.ToInt32(txtPricePerDay.Text);
                int days = Convert.ToInt32(range.Days);
                if (days == 0)
                {
                    days = 1;
                }
                txtTotalPrice.Text = Convert.ToString(price * days);
            }
        }

        protected void btnBooking_Click(object sender, EventArgs e)
        {
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

            if (ddlRoomType.SelectedValue == "0") 
            {
                lblMessage.Text = "Please select the room type";
                lblMessage.ForeColor = Color.Red;
                ddlRoomType.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtCreditCard.Text) == true)
            {
                lblMessage.Text = "Please enter your credit card number";
                lblMessage.ForeColor = Color.Red;
                txtCreditCard.Focus();
                return;
            }

            sp_booking(Session["email"].ToString(), ddlRoomType.SelectedValue, ddlBedType.SelectedValue, txtCheckInDate.Text, txtCheckOutDate.Text, txtTotalPrice.Text, txtRemark.Text, txtCreditCard.Text);
            refresh_room_status();
        }

        public void refresh_room_status()
        {
            if (txtCheckInDate.Text != "")
            {
                DataTable dt = sp_room_status(txtCheckInDate.Text);
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

        public DataTable sp_room_status(string check_in_date)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Hotel"].ConnectionString);

            using (conn)
            {
                SqlCommand myCommand = new SqlCommand("sp_room_status", conn);

                myCommand.Parameters.Add(new SqlParameter("@check_in_date", SqlDbType.VarChar));

                myCommand.Parameters["@check_in_date"].Value = check_in_date;

                myCommand.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter sqlAdapter = new SqlDataAdapter(myCommand);

                DataTable dt = new DataTable();
                sqlAdapter.Fill(dt);
                conn.Close();
                myCommand.Dispose();

                return dt;
            }
        }

        public int sp_booking(string email, string room_id, string bed_type, string check_in_date, string check_out_date, string price, string remark, string credit_card)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Hotel"].ConnectionString);

            using (conn)
            {
                try
                {
                    conn.Open();
                    SqlParameter[] parms = { new SqlParameter("@email", email),
                                                new SqlParameter("@room_id", room_id),
                                                new SqlParameter("@bed_type", bed_type),
                                                new SqlParameter("@check_in_date", check_in_date),
                                                new SqlParameter("@check_out_date", check_out_date),
                                                new SqlParameter("@price", price),
                                                new SqlParameter("@remark", remark),
                                                new SqlParameter("@credit_card", credit_card) };
                    SqlCommand Command = new SqlCommand("sp_booking", conn);
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