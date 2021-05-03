using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoginAPI.Models
{
    public class Student
    {
        public  string Name { get; set; }

        public int English { get; set; }
        public int Hindi { get; set; }

        public int Malayalam { get; set; }

        public string Status { get; set; }

        public string Message { get; set; }
    }
}