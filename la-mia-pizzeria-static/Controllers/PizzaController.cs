using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

            Pizza pizzaTrovata = ctx.Pizze.Where(pizza => pizza.Id == Id).Include(pizza => pizza.Categorie).
                Include(pizza => pizza.Ingredienti).FirstOrDefault();

            if (pizzaTrovata == null)
            {
                return NotFound($"formData {Id} non trovata");
            }

            return View(pizzaTrovata);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PizzaFormModel formData)
        {

            if (!ModelState.IsValid)
            {
               
                using (PizzaContext ctx = new PizzaContext())
                {
                    List<Categorie> categorie = ctx.Categorie.ToList();
                    formData.ListaCategorie = categorie;

                    List<Ingrediente> ingredienti = ctx.Ingredienti.ToList();
                    List<SelectListItem> listIngredienti = new List<SelectListItem>();

                    foreach(Ingrediente ingrediente in ingredienti)
                    {
                        listIngredienti.Add(
                            new SelectListItem()
                            { Text = ingrediente.Nome, Value = ingrediente.Id.ToString() });
                    }

                    formData.Ingredienti = listIngredienti;
                    return View("Create", formData);
                }
                
            }

            using (PizzaContext ctx = new PizzaContext())
            {
          

                string url = "/img/";
                Pizza nuovaPizza = new Pizza();
                nuovaPizza.Name = formData.Pizza.Name;
                nuovaPizza.Description = formData.Pizza.Description;
                nuovaPizza.ImgUrl = url+formData.Pizza.ImgUrl;
                nuovaPizza.Prezzo = formData.Pizza.Prezzo;
                nuovaPizza.CategorieId = formData.Pizza.CategorieId;
                nuovaPizza.Ingredienti = new List<Ingrediente>();

                if (formData.SelectedIngredienti != null)
                {
                    foreach (string selectedIngredienteId in formData.SelectedIngredienti)
                    {
                        int selectedIntIngredienteId = int.Parse(selectedIngredienteId);
                        Ingrediente ingrediente = ctx.Ingredienti.Where(i => i.Id == selectedIntIngredienteId).FirstOrDefault();
                        nuovaPizza.Ingredienti.Add(ingrediente);
                    }
                }

                ctx.Pizze.Add(nuovaPizza);
                ctx.SaveChanges();

                return RedirectToAction("Index");
            }          
        }

        [Authorize(Roles = "ADMIN")]
        [HttpGet]
        public IActionResult Create()
        {
            using (PizzaContext ctx = new PizzaContext())
            {
                List<Categorie> categories = ctx.Categorie.ToList();
                List<Ingrediente> ingredienti = ctx.Ingredienti.ToList();

                List<SelectListItem> listIngredienti = new List<SelectListItem>();

                foreach (Ingrediente ingrediente in ingredienti)
                {
                    listIngredienti.Add(new SelectListItem()
                    {
                        Text = ingrediente.Nome,
                        Value = ingrediente.Id.ToString()
                    });
                }

                PizzaFormModel model = new PizzaFormModel();
                model.Pizza = new Pizza();
                model.ListaCategorie = categories;
                model.Ingredienti = listIngredienti;

                return View("Create", model);

            }
        }


        [Authorize(Roles = "ADMIN")]
        [HttpGet]
        public IActionResult Update(int Id)
        {
            using (PizzaContext ctx = new PizzaContext())
            {
                Pizza pizzaEdit = ctx.Pizze.Include(p => p.Ingredienti).Where(pizza => pizza.Id == Id).FirstOrDefault();

                if (pizzaEdit == null)
                {
                    return NotFound();
                }
                else
                {
                    List<Categorie> categorie = ctx.Categorie.ToList();

                    List<Ingrediente> ingredienti = ctx.Ingredienti.ToList();
                    List<SelectListItem> listIngredienti = new List<SelectListItem>();

                    foreach (Ingrediente ingrediente in ingredienti)
                    {
                        listIngredienti.Add(new SelectListItem()
                        {
                            Text = ingrediente.Nome,
                            Value = ingrediente.Id.ToString(),
                            Selected = pizzaEdit.Ingredienti.Any(i => i.Id == ingrediente.Id)
                        });
                    }

                    PizzaFormModel model = new PizzaFormModel();
                    model.Pizza = pizzaEdit;
                    model.ListaCategorie = categorie;
                    model.Ingredienti = listIngredienti;

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
                    List<Ingrediente> ingredienti = ctx.Ingredienti.ToList();
                    List<SelectListItem> listIngredienti = new List<SelectListItem>();

                    foreach (Ingrediente ingrediente in ingredienti)
                    {
                        listIngredienti.Add(new SelectListItem()
                        {
                            Text = ingrediente.Nome,
                            Value = ingrediente.Id.ToString(),
                        });
                    }

                    form.Pizza = ctx.Pizze.Where(pizza => pizza.Id == Id).FirstOrDefault();
                    form.ListaCategorie = categorie;
                    form.Ingredienti = listIngredienti;

                    return View("Edit", form);
                }
            }

            string url = "/img/";

            using (PizzaContext ctx = new PizzaContext())
            {
                Pizza pizzaEdit = ctx.Pizze.Include(p => p.Ingredienti).Where(pizza => pizza.Id == Id).FirstOrDefault();

                if (pizzaEdit != null)
                {
                    pizzaEdit.Name = form.Pizza.Name;
                    pizzaEdit.Description = form.Pizza.Description;
                    pizzaEdit.ImgUrl = url + form.Pizza.ImgUrl;
                    pizzaEdit.Prezzo = form.Pizza.Prezzo;
                    pizzaEdit.CategorieId = form.Pizza.CategorieId;

                    pizzaEdit.Ingredienti.Clear();

                    if (form.SelectedIngredienti != null)
                    {
                        foreach (string selectedIngredienteId in form.SelectedIngredienti)
                        {
                            int selectedIntIngredienteId = int.Parse(selectedIngredienteId);
                            Ingrediente ingrediente = ctx.Ingredienti.Where(i => i.Id == selectedIntIngredienteId).FirstOrDefault();
                            pizzaEdit.Ingredienti.Add(ingrediente);
                        }
                    }

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
