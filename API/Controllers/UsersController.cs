using Microsoft.AspNetCore.Mvc;
using API.Entities;
using API.Data;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

    [ApiController, Route("api/[controller]/")]

public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly DataContext _dataContext;

        // Data context will be scoped only for the HTTP request that will be done
        // when http request arrives the framework will check out controller and use the datacontext
        // to create db connection and retrieve the data and once it serves it it will be disposed of
        public UsersController(ILogger<UsersController> logger, DataContext dataContext)
        {
            _logger = logger;
            _dataContext = dataContext;
        }   

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers() {

            var users = await _dataContext.Users.ToListAsync();

            // this will actually return a 200 http respose (OK) 
            // since we specified the return type the framework create the correct
            // type of http response
            return users;
        }  

        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetUser(int id) {
            return await _dataContext.Users.FindAsync(id);
        }   

    
}