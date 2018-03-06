using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlTypes;
using System.Data.SqlClient;

namespace WindowsFormsApplication2
{
    public static class Globals
    {
        public static string email { get; set; }
        public static string connStr { get; set; }
        public static decimal actPrice { get; set; }
    }
}
