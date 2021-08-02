using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace TeamHealthDemo.Models
{
    public class DataLayer
    {
        public static async Task<List<Blog>> GetBlogPosts(int PageNumber, int PageSize, string Sort, string SortDirection, DemoContext _db)
        {
            if (PageNumber == 0) PageNumber = 1;
            if (PageSize == 0) PageSize = 3;
            var sortBy = Sort + " " + SortDirection;

            try
            {
                var results = await _db.Blogs
                                .OrderBy(sortBy)
                                .Skip(PageSize * (PageNumber - 1))
                                .Take(PageSize)                                
                                .ToListAsync();

                return results;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<Blog> AddBlogPost(Blog blog, DemoContext _db)
        {
            blog.Posted = DateTime.Now;
            try
            {
                _db.Blogs.Add(blog);
                await _db.SaveChangesAsync();
                SendMail(blog.Title);
                return blog;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public static void SendMail(string title)
        {
            try
            {
                var client = new SmtpClient("smtp.mailtrap.io", 2525)
                {
                    Credentials = new NetworkCredential("7fede3d193c136", "42aafbc1f63778"),
                    EnableSsl = true
                };
                client.Send("ernpac@gmail.com", "ernpac@gmail.com", "New Blog Posted", "A new post with the title \"" + title + "\" was added to the blog.");
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
