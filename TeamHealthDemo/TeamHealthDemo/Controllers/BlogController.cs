using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamHealthDemo.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TeamHealthDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private DemoContext _db = new DemoContext();

        public BlogController(DemoContext db)
        {
            _db = db;
        }

        // GET: api/<BlogController>
        [HttpGet]
        public async Task<IActionResult> Get(int PageSize, int PageNumber, string Sort, string SortDirection)
        {
            var results = await DataLayer.GetBlogPosts(PageNumber, PageSize, Sort, SortDirection, _db);
            return Ok(results);
        }


        // POST api/<BlogController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Blog blog)
        {
            var result = await DataLayer.AddBlogPost(blog, _db);
            return CreatedAtAction(nameof(BlogController), new { Blog = blog });
        }

    }
}
