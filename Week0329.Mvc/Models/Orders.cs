using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper.Contrib.Extensions;

namespace Week0329.Mvc.Models
{
    [Table("Orders")]
    public class Orders
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int TicketId { get; set; }
        public decimal Price { get; set; }
        public DateTime OrderTime { get; set; }
        public DateTime DepartTime { get; set; }
    }
}