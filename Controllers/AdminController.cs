﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;
using e_Medicine.Models;

namespace e_Medicine.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public AdminController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        [HttpPost]
        [Route("addUpdateMedicine")]
        public Response addUpdateMedicine(Medicine medicines)
        {
            DAL dal = new DAL();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("EMedCS").ToString());
            Response response = dal.addUpdateMedicine(medicines, connection);
            return response;
        }

        [HttpGet]
        [Route("userList")]
        public Response userList()
        {
            DAL dal = new DAL();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("EMedCS").ToString());
            Response response = dal.userList(connection);
            return response;
        }
    }
}
