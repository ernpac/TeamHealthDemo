using System;
using System.Collections.Generic;

#nullable disable

namespace TeamHealthDemo.Models
{
    public partial class Blog
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Post { get; set; }
        public DateTime? Posted { get; set; }
        public int Authorid { get; set; }
    }
}
