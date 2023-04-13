namespace la_mia_pizzeria_static.Models
{
    public class Categorie
    {

        public int Id { get; set; } 

        public string Nome { get; set; } = string.Empty;

        public List<Pizza> Pizze { get; set; }

        public static implicit operator Categorie(List<Categorie> v)
        {
            throw new NotImplementedException();
        }
    }
}
