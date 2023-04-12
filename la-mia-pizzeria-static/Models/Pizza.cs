using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace la_mia_pizzeria_static.Models
{
    [Table("pizza")]
    public class Pizza
    {
        public Pizza() { }

        public Pizza(string name, string description, string imgUrl, double prezzo)
        {
            Name = name;
            Description = description;
            ImgUrl = imgUrl;
            Prezzo = prezzo;
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Il nome è obbligatorio.")]
        [StringLength(25, ErrorMessage = "Il nome della pizza non puo' avere più di 25 caratteri.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "La descrizione è obbligatoria.")]
        [StringLength(700, ErrorMessage = "La descrizione della pizza non puo' avere più di 700 caratteri.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "L'immagine è obbligatoria.")]
        public string ImgUrl { get; set; } = string.Empty;

        [Required(ErrorMessage = "Il prezzo è obbligatorio")]
        public double Prezzo { get; set; }
    }
}
