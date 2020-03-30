using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using Dapper.Contrib.Extensions;
using Week0329.Mvc.Models;
using System.Configuration;

namespace Week0329.Mvc.Controllers
{
    public class BuyController : Controller
    {

        public ActionResult Show()
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Conn"].ConnectionString))
            {
                List<Tickets> list = conn.GetAll<Tickets>().ToList();
                return View(list);
            }
        }
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(Tickets t)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Conn"].ConnectionString))
            {
                long add = conn.Insert<Tickets>(t);
                if (add > 0)
                {
                    return Content("<script>alert('添加成功！');location.href='/Buy/Show';</script>");
                }
                else
                {
                    return Content("<script>alert('添加失败！');location.href='/Buy/Show';</script>");
                }
            }
        }
        
        public ActionResult BuyT()
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Conn"].ConnectionString))
            {
                var t = conn.GetAll<Tickets>().Where(s => !s.IsSaleOut);
                if (t.Count() < 1)
                {
                    return Content("<script>alert('没有票了!');</script>");
                }

                int Uid = ((UserInfo)Session["u"]).Id;

                var bt = conn.GetAll<Orders>().Where(s => s.UserId == Uid && s.DepartTime.Subtract(DateTime.Now).Ticks > 0);
                if (bt.Count() > 0)
                {
                    return Content("<script>alert('已持有票，无需再次购买!');</script>");
                }

                Queue<Tickets> tickets = new Queue<Tickets>();
                foreach (Tickets item in t)
                {
                    tickets.Enqueue(item);
                }

                bool isok = false;
                Tickets t1 = null;
                while (tickets.Count() > 0)
                {
                    t1 = tickets.Dequeue();

                    isok = conn.Insert<Orders>(new Orders
                    {
                        UserId = Uid,
                        TicketId = t1.Id,
                        Price = t1.TicketPrice,
                        OrderTime = DateTime.Now,
                        DepartTime = t1.OpenPoint.AddDays(1)
                    }) > 0;

                    break;
                }

                if (isok)
                {
                    conn.Update<Tickets>(new Tickets
                    {
                        Id = t1.Id,
                        ArrivalStation = t1.ArrivalStation,
                        DepartureStation = t1.DepartureStation,
                        SeatLevel = t1.SeatLevel,
                        SeatNo = t1.SeatNo,
                        TicketPrice = t1.TicketPrice,
                        TrainNumber = t1.TrainNumber,
                        OpenPoint = DateTime.Now.AddDays(1),
                        IsSaleOut = true
                    });

                    return Content("<script>alert('购票成功！');</script>");
                }
            }
            return View();
        }
    }
}