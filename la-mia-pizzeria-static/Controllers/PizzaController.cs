using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

            //var pizze = ctx.Pizze.SingleOrDefault(p => p.Id == Id);

            Pizza pizzaTrovata = ctx.Pizze.Where(pizza => pizza.Id == Id).Include(pizza => pizza.Categorie).FirstOrDefault();

            if (pizzaTrovata == null)
            {
                return NotFound($"form {Id} non trovata");
            }

            return View(pizzaTrovata);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PizzaFormModel pizza)
        {

            if (!ModelState.IsValid)
            {
                using (PizzaContext ctx = new PizzaContext())
                {
                    List<Categorie> categorie = ctx.Categorie.ToList();
                    pizza.ListaCategorie = categorie;
                    return View("Create", pizza);
                }
                
            }

            using (PizzaContext ctx = new PizzaContext())
            {
                string url = "/img/";
                Pizza nuovaPizza = new Pizza();
                nuovaPizza.Name = pizza.Pizza.Name;
                nuovaPizza.Description = pizza.Pizza.Description;
                nuovaPizza.ImgUrl = url+pizza.Pizza.ImgUrl;

                nuovaPizza.CategorieId = pizza.Pizza.CategorieId;

                ctx.Pizze.Add(nuovaPizza);
                ctx.SaveChanges();

                return RedirectToAction("Index");
            }          
        }

        [HttpGet]
        public IActionResult Create()
        {
            using (PizzaContext ctx = new PizzaContext())
            {
                List<Categorie> categories = ctx.Categorie.ToList();

                PizzaFormModel model = new PizzaFormModel();
                model.Pizza = new Pizza();
                model.ListaCategorie = categories;

                return View("Create", model);

            }

            
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
                    List<Categorie> categorie = ctx.Categorie.ToList();


                    PizzaFormModel model = new PizzaFormModel();
                    model.Pizza = pizzaEdit;
                    model.ListaCategorie = categorie;
                    
                    return View("Edit", model);
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int Id, PizzaFormModel form)
        {

            if (!ModelState.IsValid)
            {
                using (PizzaContext ctx = new PizzaContext())
                {
                    List<Categorie> categorie = ctx.Categorie.ToList();

                    form.Pizza = ctx.Pizze.Where(pizza => pizza.Id == Id).FirstOrDefault();
                    form.ListaCategorie = categorie;

                    return View("Edit", form);
                }

               
            }
            string url = "/img/";

            using (PizzaContext ctx = new PizzaContext())
            {
                
                Pizza pizzaEdit = ctx.Pizze.Where(pizza => pizza.Id == Id).FirstOrDefault();

                if (pizzaEdit != null)
                {
                    pizzaEdit.Name = form.Pizza.Name;
                    pizzaEdit.Description = form.Pizza.Description;
                    pizzaEdit.ImgUrl = url+form.Pizza.ImgUrl;
                    pizzaEdit.Prezzo = form.Pizza.Prezzo;
                    pizzaEdit.CategorieId = form.Pizza.CategorieId;
                   
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
