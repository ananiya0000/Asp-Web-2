using Assig.Models;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

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
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
        public ActionResult Home()
        {
            var machines = _context.Machine.Where(temp=>temp.Available==true).ToList();
            return View(machines);
        }
        public ActionResult Search(string searchString)
        {
            var machines = _context.Machine.ToList();
            if (!string.IsNullOrEmpty(searchString))
            {
                ViewBag.w = searchString;
                var machineSearched = machines.Where(temp => temp.MachineName.Contains(searchString) || temp.Industry.Contains(searchString) && temp.Available==true).ToList();
                return View("Home", machineSearched);
            }
            return View("Home",machines);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Machine m)
        {
            string userName = User.Identity.Name;
            m.Email = userName;
            _context.Machine.Add(m);
            _context.SaveChanges();
            return RedirectToAction("Home");
        }
        public ActionResult Details(int id)
        {
            var ma = _context.Machine.Where(temp => temp.MachineID == id).FirstOrDefault();
            return View(ma);
        }
        public ActionResult Delete(int id)
        {
            var ma = _context.Machine.Where(temp => temp.MachineID == id).FirstOrDefault();
            _context.Machine.Remove(ma);
            _context.SaveChanges();
            return RedirectToAction("Home");
        }
        public ActionResult Edit(int id)
        {
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
                var machineSearched = machines.Where(temp =>  temp.Industry.Contains(searchString) && temp.Available == true).ToList();
                return View("Home", machineSearched);
            }
            return View("Home", machines);
        }
    }
}