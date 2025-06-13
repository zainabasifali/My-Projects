using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace first.Models
{
    public class AttendanceViewModel
    {
        public DateTime date { get; set; }
        public string status { get; set; }
        public string userName { get; set; }
        public string markedByName { get; set; }
    }
}