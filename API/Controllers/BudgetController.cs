using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Authorize]
    public class BudgetController : BaseApiController
    {
        private readonly DataContext _context;

        public BudgetController(DataContext context)
        {
            _context = context;
        }

        // GET: api/<BudgetController>
        [HttpGet]
        public ActionResult Get()
        {
            return Ok();
        }

        // GET api/<BudgetController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<BudgetController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<BudgetController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BudgetController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
