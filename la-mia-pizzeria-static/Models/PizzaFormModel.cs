namespace la_mia_pizzeria_static.Models
{
    public class PizzaFormModel
    {

        //questo è anche il modello (per il db) oltre che le informazioni da presetnare all'uente
        public Pizza Pizza { get; set; }

        //queste sono informazioni READ da presentare all'utente nel form
        public List<Categorie>? ListaCategorie { get; set; }

    }
}
