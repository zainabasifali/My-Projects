using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace first.Models
{
    public class BillView
    {
        public int id { get; set; }
        public string name { get; set; }
        public int booking_id { get; set; }
        public decimal room_charges { get; set; }
        public Nullable<decimal> other_charges { get; set; }
        public decimal total_amount { get; set; }
        public Nullable<System.DateTime> generated_at { get; set; }

    }
}