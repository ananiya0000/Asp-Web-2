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
            var machines = _context.Machine.ToList();
            return View(machines);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Machine m)
        {
            string userName = User.Identity.Name;
            m.OwnerEmail = userName;
            _context.Machine.Add(m);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Details(int id)
        {
            var ma = _context.Machine.Where(temp => temp.MachineId == id).FirstOrDefault();
            return View(ma);
        }
        public ActionResult Delete(int id)
        {
            var ma = _context.Machine.Where(temp => temp.MachineId == id).FirstOrDefault();
            _context.Machine.Remove(ma);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            var ma = _context.Machine.Where(temp => temp.MachineId == id).FirstOrDefault();
            return View(ma);
        }
        [HttpPost]
        public ActionResult Edit(Machine ma)
        {
            var m = _context.Machine.Where(temp => temp.MachineId == ma.MachineId).FirstOrDefault();
            m.Name = ma.Name;
            m.Price = ma.Price;
            m.Industry = ma.Industry;
            m.Available = ma.Available;
            return RedirectToAction("Index");
        }
        public ActionResult Personal()
        {
            string userName=User.Identity.Name;
            var machine = _context.Machine.Where(temp => temp.OwnerEmail==userName).ToList();
            return View(machine);
        }
    }
}