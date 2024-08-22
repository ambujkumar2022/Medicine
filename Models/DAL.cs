using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using e_Medicine.Controllers;
using Microsoft.Extensions.Configuration;

namespace e_Medicine.Models
{
    public class DAL
    {
        public Response register(Users users, SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("sp_register", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FirstName", users.FirstName);
            cmd.Parameters.AddWithValue("@LastName", users.LastName);
            cmd.Parameters.AddWithValue("@PassW", users.PassW);
            cmd.Parameters.AddWithValue("@Email", users.Email);
            cmd.Parameters.AddWithValue("@Fund", 0);
            cmd.Parameters.AddWithValue("@Type", "Users");
            cmd.Parameters.AddWithValue("@Status", "Pending");
            //cmd.Parameters.AddWithValue("@DateOfCreate", users.FirstName);

            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "User registered successfully";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "User registration failed";
            }
            return response;
        }

        public Response login(Users users, SqlConnection connection)
        {
            Response response = new Response();
            SqlDataAdapter da = new SqlDataAdapter("sp_login", connection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@Email", users.Email);
            da.SelectCommand.Parameters.AddWithValue("@PassW", users.PassW);
            DataTable dt = new DataTable();
            da.Fill(dt);

            Users user = new Users();
            if (dt.Rows.Count > 0)
            {
                user.Id = Convert.ToInt32(dt.Rows[0]["ID"]);
                user.FirstName = Convert.ToString(dt.Rows[0]["FirstName"]);
                user.LastName = Convert.ToString(dt.Rows[0]["LastName"]);
                user.Email = Convert.ToString(dt.Rows[0]["Email"]);
                user.Type = Convert.ToString(dt.Rows[0]["Type"]);
                response.StatusCode = 200;
                response.StatusMessage = "User is valid";
                response.user = user;

            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "User is Invalid";
            }

            return response;
        }

        public Response viewUser(Users users, SqlConnection connection)
        {
            SqlDataAdapter da = new SqlDataAdapter("p_viewUser", connection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@ID", users.Id);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Response response = new Response();
            Users user = new Users();
            if (dt.Rows.Count > 0)
            {
                user.Id = Convert.ToInt32(dt.Rows[0]["ID"]);
                user.FirstName = Convert.ToString(dt.Rows[0]["FirstName"]);
                user.LastName = Convert.ToString(dt.Rows[0]["LastName"]);
                user.Email = Convert.ToString(dt.Rows[0]["Email"]);
                user.Fund = Convert.ToDecimal(dt.Rows[0]["Fund"]);
                user.Type = Convert.ToString(dt.Rows[0]["Type"]);
                user.DateOfCreate = Convert.ToDateTime(dt.Rows[0]["DateTime"]);
                user.PassW = Convert.ToString(dt.Rows[0]["PassW"]);
                response.StatusCode = 200;
                response.StatusMessage = "User Exists";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "User Doesn't Exists";
            }
            return response;
        }

        public Response updateProfile(Users users, SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("sp_updateProfile", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FirstName", users.FirstName);
            cmd.Parameters.AddWithValue("@LastName", users.LastName);
            cmd.Parameters.AddWithValue("@PassW", users.PassW);
            cmd.Parameters.AddWithValue("@Email", users.Email);
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Record updated successfully";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Error Occured.Try after sometime";
            }

            return response;
        }

        public Response addToCart(Cart cart, SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("sp_AddToCart", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", cart.Id);
            cmd.Parameters.AddWithValue("@UnitPrice", cart.UnitPrice);
            cmd.Parameters.AddWithValue("@Discount", cart.Discount);
            cmd.Parameters.AddWithValue("@Qty", cart.Qty);
            cmd.Parameters.AddWithValue("@TotPrice", cart.TotPrice);
            cmd.Parameters.AddWithValue("@MedicineId", cart.MedicineId);

            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Item added successfully";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Item could not be added";
            }
            return response;
        }
        public Response placeOrder(Users users, SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("sp_PlaceOrder", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", users.Id);
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Order has been placed successfully";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Order couldn't be placed";
            }
            return response;
        }

        public Response orderList(Users users, SqlConnection connection)
        {
            Response response = new Response();
            List<Orders> listOrder = new List<Orders>();
            SqlDataAdapter da = new SqlDataAdapter("sp_OrderList", connection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@Type", users.Type);
            da.SelectCommand.Parameters.AddWithValue("@Id", users.Id);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Orders order = new Orders();
                    order.Id = Convert.ToInt32(dt.Rows[i]["ID"]);
                    order.OrderNo = Convert.ToString(dt.Rows[i]["OrderNo"]);
                    order.OrderTot = Convert.ToDecimal(dt.Rows[i]["OrderTotal"]);
                    order.OrderStatus = Convert.ToString(dt.Rows[i]["OrderStatus"]);
                    listOrder.Add(order);
                }
                if (listOrder.Count > 0)
                {
                    response.StatusCode = 200;
                    response.StatusMessage = "Order details fetched";
                }
                else
                {
                    response.StatusCode = 100;
                    response.StatusMessage = "Order details not available";
                    response.listOrders = null;
                }

            }
            return response;

        }
        
        public Response addUpdateMedicine(Medicine medicines, SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("sp_AddUpdateMedicine", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Name", medicines.Name);
            cmd.Parameters.AddWithValue("@Manufacturer", medicines.Manufacturer);
            cmd.Parameters.AddWithValue("@UnitPrice", medicines.UnitPrice);
            cmd.Parameters.AddWithValue("@Discount", medicines.Discount);
            cmd.Parameters.AddWithValue("@Qty", medicines.Qty);
            cmd.Parameters.AddWithValue("@DateOfExp", medicines.DateOfExp);
            cmd.Parameters.AddWithValue("@ImgUrl", medicines.ImgUrl);
            cmd.Parameters.AddWithValue("@Status", medicines.Status);
            cmd.Parameters.AddWithValue("@Type", medicines.Type);

            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if(i>0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Medicine inserted successfully";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Medicine not saved. Try Again";
            }
            return response;
        }

        public Response userList( SqlConnection connection)
        {
            Response response = new Response();
            List<Users> listUsers = new List<Users>();
            SqlDataAdapter da = new SqlDataAdapter("sp_UserList", connection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Users user = new Users();
                    user.Id = Convert.ToInt32(dt.Rows[i]["ID"]);
                    user.FirstName = Convert.ToString(dt.Rows[i]["FirstName"]);
                    user.LastName = Convert.ToString(dt.Rows[i]["LastName"]);
                    user.PassW = Convert.ToString(dt.Rows[i]["PassW"]);
                    user.Email = Convert.ToString(dt.Rows[i]["Email"]);
                    user.Fund = Convert.ToDecimal(dt.Rows[i]["Fund"]);
                    user.Type = Convert.ToString(dt.Rows[i]["Type"]);
                    user.Status = Convert.ToInt16(dt.Rows[i]["Status"]);
                    user.DateOfCreate = Convert.ToDateTime(dt.Rows[i]["DateOfCreate"]);
                    listUsers.Add(user);
                }
                if (listUsers.Count > 0)
                {
                    response.StatusCode = 200;
                    response.StatusMessage = "Users details fetched";
                    response.listUsers = listUsers;
                }
                else
                {
                    response.StatusCode = 100;
                    response.StatusMessage = "Users details not available";
                    response.listUsers = null;
                }

            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "User details are not available";
                response.listUsers = null;
            }
            return response;

        }

    }
}