using Microsoft.AspNetCore.Mvc.Rendering;

namespace la_mia_pizzeria_static.Models
{
    public class PizzaFormModel
    {

        //questo è anche il modello (per il db) oltre che le informazioni da presetnare all'uente
        public Pizza Pizza { get; set; }

        //queste sono informazioni READ da presentare all'utente nel form
        public List<Categorie>? ListaCategorie { get; set; }

        public List<SelectListItem>? Ingredienti { get; set; }
        public List<string>? SelectedIngredienti { get; set; }
    }
}
