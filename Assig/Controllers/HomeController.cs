using Assig.Models;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Web;
using System.IO;
using System;

namespace Assig.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext _context = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult About()
        {
            ViewBag.NotificationCount = getNotificationCount();
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.NotificationCount = getNotificationCount();
            ViewBag.Message = "Your contact page.";
            return View();
        }
        public ActionResult Home()
        {
            var machines = _context.Machine.Where(temp=>temp.Available==true).ToList();
            machines.Reverse();
            ViewBag.NotificationCount = getNotificationCount();
            return View(machines);
        }
        public ActionResult Search(string searchString)
        {
            var machines = _context.Machine.ToList();
            if (!string.IsNullOrEmpty(searchString))
            {
                ViewBag.w = searchString;
                var machineSearched = machines.Where(temp => temp.MachineName.Contains(searchString) || temp.Catagory.Contains(searchString)|| temp.Industry.Contains(searchString) && temp.Available==true).ToList();
                //return RedirectToAction("Search", "Home",new { });
                return View("Home",machineSearched);
            }
            return View("Home",machines);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Machine m, HttpPostedFileBase ImageFile, HttpPostedFileBase ImageFile1, HttpPostedFileBase ImageFile2)
        {
            var file = Request.Files["ImageFile"];
            var file1 = Request.Files["ImageFile1"];
            var file2 = Request.Files["ImageFile2"];
            using (var ms = new MemoryStream())
            {
                file.InputStream.CopyTo(ms);
                m.Image = ms.ToArray();
            }
            using (var ms = new MemoryStream())
            {
                file1.InputStream.CopyTo(ms);
                m.Image1 = ms.ToArray();
            }
            using (var ms = new MemoryStream())
            {
                file2.InputStream.CopyTo(ms);
                m.Image2 = ms.ToArray();
            }
            string userName = User.Identity.Name;
            ViewBag.NotificationCount = getNotificationCount();
            m.Email = userName;
            _context.Machine.Add(m);
            _context.SaveChanges();
            return RedirectToAction("Home");
        }
        public ActionResult Details(int id)
        {
            ViewBag.NotificationCount = getNotificationCount();
            var ma = _context.Machine.Where(temp => temp.MachineID == id).FirstOrDefault();
            return View(ma);
        }
        public ActionResult Delete(int id)
        {
            var ma = _context.Machine.Where(temp => temp.MachineID == id).FirstOrDefault();
            _context.Machine.Remove(ma);
            _context.SaveChanges();
            return RedirectToAction("Personal");
        }
        public ActionResult Edit(int id)
        {
            ViewBag.NotificationCount = getNotificationCount();
            var ma = _context.Machine.Where(temp => temp.MachineID == id).FirstOrDefault();
            return View(ma);
        }
        [HttpPost]
        public ActionResult Edit(Machine ma)
        {
            var m = _context.Machine.Where(temp => temp.MachineID == ma.MachineID).FirstOrDefault();
            m.Price = ma.Price;
            m.Industry = ma.Industry;
            m.Available = ma.Available;
            _context.SaveChanges();
            return RedirectToAction("Home");
        }
        public ActionResult Personal()
        {
            ViewBag.NotificationCount = getNotificationCount();
            string userName=User.Identity.Name;
            var machine = _context.Machine.Where(temp => temp.Email==userName).ToList();
            return View(machine);
        }
        public ActionResult Rented(int id)
        {
            var machine = _context.Machine.Where(temp => temp.MachineID == id).FirstOrDefault();
            machine.Available = false;
            _context.SaveChanges();
            return RedirectToAction("Home");
        }
        public ActionResult Catagory(string searchString)
        {
            var machines = _context.Machine.ToList();
            if (!string.IsNullOrEmpty(searchString))
            {
                ViewBag.w = searchString;
                var machineSearched = machines.Where(temp =>  temp.Catagory.Contains(searchString) && temp.Available == true).ToList();
                return View("Home", machineSearched);
            }
            return View("Home", machines);
        }
        public ActionResult Notification()
        {
            var Notify = _context.Notification.Where(temp => temp.Seen == false && temp.OwnerEmail == User.Identity.Name).ToList();
            int count = 0;
            foreach (var temp in Notify)
            {
                count++;
                temp.Seen=true;
            }
            ViewBag.NotificationCount = count;
            //Session["NotificationCount"] = count;
            _context.SaveChanges();
            return View(Notify);
        }
        public ActionResult Rent(string CustomerEmail, string SellerEmail, int MachineID)
        {
            var tempo=_context.Machine.Where(temp => temp.MachineID == MachineID).FirstOrDefault();
            ViewBag.CustomerEmail = CustomerEmail;
            ViewBag.SellerEmail = SellerEmail;
            ViewBag.MachineID = MachineID;
            Notification n = new Notification();
            n.CustomerEmail = CustomerEmail;
            n.OwnerEmail = SellerEmail;
            n.MachineID = MachineID;
            n.RequestDate = DateTime.Now;
            _context.Notification.Add(n);
            _context.SaveChanges();
            return RedirectToAction("Home");
        }
        public int getNotificationCount()
        {
            var Notify = _context.Notification.Where(temp => temp.Seen == false && temp.OwnerEmail == User.Identity.Name).ToList();
            int count = 0;
            foreach (var temp in Notify)
            {
                count++;
            }
            return count;
        }
        public ActionResult RequestHistory()
        {
            var Not = _context.Notification.ToList();
            Not.Reverse();
            return View(Not);
        }
    }
}