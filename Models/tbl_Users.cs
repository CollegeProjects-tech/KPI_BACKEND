using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class tbl_Users
    {
        public Int64 id { get; set; }
        public string user_name { get; set; }
        public string department { get; set; }
        public string contact_no { get; set; }
        public string email { get; set; }
        public int userid { get; set; }
        public string address { get; set; }
        public string password { get; set; }
        public string usertype { get; set; }
        public string flag { get; set; }
    }
}
