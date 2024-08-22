using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using e_Medicine.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.SqlClient;
using System.Data;

namespace e_Medicine.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicinesController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public MedicinesController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("addToCart")]
        public Response addToCart(Cart cart)
        {
            DAL dal = new DAL();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("EMedCS").ToString());
            Response response = dal.addToCart(cart, connection);
            return response;
        }

        [HttpPost]
        [Route("placeOrder")]

        public Response placeOrder(Users users)
        {
            DAL dal = new DAL();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("EMedCS").ToString());
            Response response = dal.placeOrder(users, connection);
            return response;
        }

        [HttpPost]
        [Route("orderList")]

        public Response orderList(Users users)
        {
            DAL dal = new DAL();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("EMedCS").ToString());
            Response response = dal.orderList(users, connection);
            return response;
        }
    }
}
