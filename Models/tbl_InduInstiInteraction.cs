using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class tbl_InduInstiInteraction
    {
        public int id { get; set; }
        public int? teacher_id { get; set; }
        public string selected_option { get; set; }
        public string selected_sem { get; set; }
        public string selected_year { get; set; }
        public string details { get; set; }
        public string? file_path { get; set; }
        public string? date { get; set; }
    }
}
