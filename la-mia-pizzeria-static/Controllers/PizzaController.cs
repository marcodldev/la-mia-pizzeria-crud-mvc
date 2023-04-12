using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace la_mia_pizzeria_static.Controllers
{
    public class PizzaController : Controller
    {

        public PizzaController() 
        {
            System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";
            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;
        }
        public IActionResult Index()
        {
            using var ctx = new PizzaContext();

            var pizze = ctx.Pizze.ToArray();

            return View(pizze);
        }

        [HttpGet]
        public IActionResult Show(int Id) 
        {
            using var ctx = new PizzaContext();

            var pizze = ctx.Pizze.SingleOrDefault(p => p.Id == Id);

            if (pizze == null)
            {
                return NotFound($"Pizza {Id} non trovata");
            }

            return View(pizze);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Pizza pizza)
        {

            if (!ModelState.IsValid)
            {
                return View("Create", pizza);
            }

            using (PizzaContext ctx = new PizzaContext())
            {
                string url = "/img/";
                Pizza nuovaPizza = new Pizza();
                nuovaPizza.Name = pizza.Name;
                nuovaPizza.Description = pizza.Description;
                nuovaPizza.ImgUrl = url+pizza.ImgUrl;

                ctx.Pizze.Add(nuovaPizza);
                ctx.SaveChanges();

                return RedirectToAction("Index");
            }          
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View("Create");
        }


        [HttpGet]
        public IActionResult Update(int Id)
        {
            using (PizzaContext ctx = new PizzaContext())
            {

                Pizza pizzaEdit = ctx.Pizze.Where(pizza => pizza.Id == Id).FirstOrDefault();

                if (pizzaEdit == null)               
                {
                    return NotFound();
                }
                else
                {
                   
                    
                    return View("Edit",pizzaEdit);
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int Id, Pizza pizza)
        {

            if (!ModelState.IsValid)
            {
                return View("Edit", pizza);
            }
            string url = "/img/";

            using (PizzaContext ctx = new PizzaContext())
            {
                
                Pizza pizzaEdit = ctx.Pizze.Where(pizza => pizza.Id == Id).FirstOrDefault();

                if (pizzaEdit != null)
                {
                    pizzaEdit.Name = pizza.Name;
                    pizzaEdit.Description = pizza.Description;
                    pizzaEdit.ImgUrl = url+pizza.ImgUrl;
                    pizzaEdit.Prezzo = pizza.Prezzo;

                     ctx.SaveChanges();

                      return RedirectToAction("Index");
                }
                else
                {
                    return NotFound();
                }
              
            
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int Id)
        {
            using (PizzaContext ctx = new PizzaContext())
            {
                Pizza pizzaDelete = ctx.Pizze.Where(pizza => pizza.Id == Id).FirstOrDefault();

                if (pizzaDelete != null)
                {
                    ctx.Pizze.Remove(pizzaDelete);
                    ctx.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return NotFound();
                }
            }
        }
    }
}
