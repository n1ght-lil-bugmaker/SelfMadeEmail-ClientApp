using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientApp.Model
{
    public class Email
    {
        public int Id { get; set; }
        public string Source { get; set; }
        public string Target { get; set; }
        public DateTime SendedTime { get; set; }
        public string Text { get; set; }
        public string Theme { get; set; }

    }
}