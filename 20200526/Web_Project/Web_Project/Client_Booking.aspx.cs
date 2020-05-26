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
                field_data_control(1);
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

        protected void btnBooking_Click(object sender, EventArgs e)
        {
            Confidential_Data cd = new Confidential_Data();

            if (Session["login_name"] == null || Session["user_role"].ToString() != "C")
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (string.IsNullOrEmpty(txtCheckInDate.Text) == true)
            {
                lblMessage.Text = "Please select the check in date";
                lblMessage.ForeColor = Color.Red;
                txtCheckInDate.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtCheckOutDate.Text) == true)
            {
                lblMessage.Text = "Please select the check out date";
                lblMessage.ForeColor = Color.Red;
                txtCheckOutDate.Focus();
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
            else
            {
                if (txtCreditCard.Text.Length != 16)
                {
                    lblMessage.Text = "Credit Card length must 16 digital";
                    lblMessage.ForeColor = Color.Red;
                    txtCreditCard.Focus();
                    return;
                }
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
                    return;
                }
            }

            if (ddlDeluxeRoomAmount.SelectedValue != "0")
            {
                if (dt.Rows[0].Field<int>("total_deluxe") - dt.Rows[0].Field<int>("booked_deluxe") - Convert.ToInt32(ddlDeluxeRoomAmount.SelectedValue) <= 0)
                {
                    lblMessage.Text = "No available deluxe room in this date range";
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
            }

            if (ddlStudioRoomAmount.SelectedValue != "0")
            {
                if (dt.Rows[0].Field<int>("total_studio") - dt.Rows[0].Field<int>("booked_studio") - Convert.ToInt32(ddlStudioRoomAmount.SelectedValue) <= 0)
                {
                    lblMessage.Text = "No available studio room in this date range";
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
            }

            if (sp_booking(cd.Decrypt(Session["email"].ToString()), ddlStandardRoomAmount.SelectedValue, ddlDeluxeRoomAmount.SelectedValue, ddlStudioRoomAmount.SelectedValue, ddlBedType.SelectedValue, txtCheckInDate.Text, txtCheckOutDate.Text, txtTotalPrice.Text, txtRemark.Text, txtCreditCard.Text) >= 1)
            {
                lblMessage.Text = "Booking Successful";
                lblMessage.ForeColor = Color.Green;
                refresh_room_status();
                field_data_control(2);
            }
            else
            {
                lblMessage.Text = "Booking Fail. Please contact admin";
                lblMessage.ForeColor = Color.Red;
            }
        }

        public void field_data_control(int flag)
        {
            if (flag == 1)
            {
                DataTable dt = sp_room_detail(ddlRoomType.SelectedValue.ToString());
                int price = dt.Rows[0].Field<int>("price_per_day");

                imgRoom.ImageUrl = dt.Rows[0].Field<string>("picture_path");
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
                cal_total_price();
            }
            else
            {
                imgRoom.ImageUrl = "";
                txtCheckInDate.Text = "";
                txtCheckOutDate.Text = "";
                ddlRoomType.SelectedValue = "0";
                ddlBedType.SelectedValue = "King Bed";
                txtMaxOccupancy.Text = "";
                txtRoomSize.Text = "";
                txtBathTub.Text = "";
                txtDesignerChair.Text = "";
                txtInternet.Text = "";
                txtSmartTV.Text = "";
                txtHairDeyer.Text = "";
                txtInRoomSafe.Text = "";
                txtMiniFridge.Text = "";
                txtReplenished.Text = "";
                txtHouseKeeping.Text = "";
                txtBedSofa.Text = "";
                txtOptionalBreakfast.Text = "";
                txtPricePerDay.Text = "";
                ddlStandardRoomAmount.SelectedValue = "0";
                ddlDeluxeRoomAmount.SelectedValue = "0";
                ddlStudioRoomAmount.SelectedValue = "0";
                txtTotalPrice.Text = "";
                txtCreditCard.Text = "";
                txtRemark.Text = "";
            }
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

        public int sp_booking(string email, string standard_room_amount, string deluxe_room_amount, string studio_room_amount, string bed_type, string check_in_date, string check_out_date, string price, string remarks, string credit_card)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Hotel"].ConnectionString);
            Confidential_Data cd = new Confidential_Data();

            using (conn)
            {
                try
                {
                    conn.Open();
                    SqlParameter[] parms = { new SqlParameter("@email", email),
                                                new SqlParameter("@standard_room_amount", standard_room_amount),
                                                new SqlParameter("@deluxe_room_amount", deluxe_room_amount),
                                                new SqlParameter("@studio_room_amount", studio_room_amount),
                                                new SqlParameter("@bed_type", bed_type),
                                                new SqlParameter("@check_in_date", check_in_date),
                                                new SqlParameter("@check_out_date", check_out_date),
                                                new SqlParameter("@price", price),
                                                new SqlParameter("@remarks", remarks),
                                                new SqlParameter("@credit_card_1", cd.Encrypt(credit_card.Substring(0, 8))),
                                                new SqlParameter("@credit_card_2", cd.Encrypt(credit_card.Substring(8, 8))) };
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