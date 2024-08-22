using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using e_Medicine.Models;
using System.Data.SqlClient;

namespace e_Medicine.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public UsersController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("registration")]

        public Response register(Users users)
        {
            Response response = new Response();
            DAL dal = new DAL();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("EMedCS").ToString());
            response = dal.register(users, connection);
            return response;
        }

        [HttpPost]
        [Route("login")]
        public Response login(Users users)
        {
            DAL dal = new DAL();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("EMedCS").ToString());
            Response response = dal.login(users, connection);
            return response;
        }

        [HttpPost]
        [Route("viewUser")]
        public Response viewUser(Users users) 
        {
            DAL dal = new DAL();
            SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("EMedCS").ToString());
            Response response = dal.viewUser(users, conn);
            return response;
        }

        [HttpPost]
        [Route("updateProfile")]
        public Response updateProfile(Users users)
        {
            
            DAL dal = new DAL();
            SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("EMedCS").ToString());
            Response response = dal.updateProfile(users,conn);
            return response;
        }
    }
}
