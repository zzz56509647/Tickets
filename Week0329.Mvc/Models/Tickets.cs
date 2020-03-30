using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Week0329.Mvc.Models
{
    [Table("Tickets")]
    public class Tickets
    {
        public int Id { get; set; }
        [Display(Name = "出发站")]
        public string DepartureStation { get; set; } //出发站
        [Display(Name = "到达站")]
        public string ArrivalStation { get; set; }  //到达站
        [Display(Name = "车次")]
        public string TrainNumber { get; set; } //车次
        [Display(Name = "开点")]
        public DateTime OpenPoint { get; set; } //开点
        [Display(Name = "座位号")]
        public string SeatNo { get; set; } //座位号
        [Display(Name = "座位等级")]
        public string SeatLevel { get; set; } //座位等级
        [Display(Name = "票价")]
        public decimal TicketPrice { get; set; } //票价
        [Display(Name = "是否售出")]
        public bool IsSaleOut { get; set; } //是否售出
    }
}