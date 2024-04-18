using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ArturTI225.Domain.Entities;
using ArturTI225.Infrastructure;

namespace ArturTI225.WebUI.Controllers
{
    public class CarsSalesPanelController : Controller
    {
        private CarsShopContext db = new CarsShopContext();

        // GET: Orders
        public ActionResult Index()
        {
            var orders = db.Orders.Include(o => o.Car);
            return View(orders.ToList());
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }
        
        // GET: Orders/CreditCalculator/5
        public ActionResult CreditCalculator(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }


        // GET: Orders/Create
        public ActionResult Create()
        {
            ViewBag.CarId = new SelectList(db.Cars, "Id", "Model");
            return View();
        }

        // POST: Orders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,CarId,OrderDate,TotalPrice")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CarId = new SelectList(db.Cars, "Id", "Model", order.CarId);
            return View(order);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.CarId = new SelectList(db.Cars, "Id", "Model", order.CarId);
            return View(order);
        }

        // POST: Orders/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CustomerId,CarId,OrderDate,TotalPrice")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CarId = new SelectList(db.Cars, "Id", "Model", order.CarId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpPost]
        public ActionResult DownloadCreditDetails(int id, decimal interestRate, int loanTerm)
        {
            // Расчёт платежей по кредиту (упрощённо)
            decimal monthlyPayment = (db.Orders.Find(id).TotalPrice * (1 + interestRate / 100 * loanTerm)) / (loanTerm * 12);

            var content = $"Order ID: {id}\nMonthly Payment: {monthlyPayment}\n";
            var byteArray = System.Text.Encoding.UTF8.GetBytes(content);
            var stream = new System.IO.MemoryStream(byteArray);

            return File(stream, "text/plain", $"CreditDetails_Order{id}.txt");
        }

    }
}
