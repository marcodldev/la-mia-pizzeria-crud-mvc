
using Microsoft.EntityFrameworkCore;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;

namespace la_mia_pizzeria_static.Models
{
    public class PizzaContext : DbContext
    {
        public DbSet<Pizza> Pizze { get; set; }
        public DbSet<Categorie> Categorie { get; set; }
        public DbSet<Ingrediente> Ingredienti { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=pizzeria_db;Integrated Security=True;TrustServerCertificate=True");
        }

        public void Seed()
        {
                string path = "/img/" ;

            if (!Pizze.Any())
            {
                var seed = new Pizza[]
                {

                  

                new Pizza
                {
                    Name =  "Pizza Margherita",
                    Description = " La pizza Margherita è la tipica pizza napoletana, condita con pomodoro, mozzarella, basilico fresco, sale e olio; è, assieme alla pizza marinara, la più popolare pizza italiana.\r\n\r\n                                Rappresenta sicuramente il simbolo per antonomasia del patrimonio culturale e culinario italiano, diffusa per la sua fama in tutto il mondo.",
                    ImgUrl = path+"Pizza_Margherita_stu_spivack.jpg",
                    Prezzo = 12.90,
                    //Categorie = new Categorie() { Nome = "Vegetariana"}
                },

                new Pizza
                {
                Name =  "Pizza Capricciosa",
                Description = " La pizza capricciosa è una pizza tipica della cucina italiana caratterizzata da un condimento di pomodoro, mozzarella, prosciutto cotto (o spesso anche crudo), funghi (di solito champignon), olive verdi e nere, e carciofini e spesso uova. Sebbene a volte abbia gli stessi ingredienti della pizza alle quattro stagioni, \r\n                                la capricciosa presenta condimenti tutti insieme sparsi e non divisi in quattro spicchi. Risulta inventata nell'omonimo ristorante di Roma nel 1937.",
                ImgUrl =path+"1920px-Pizza_capricciosa.jpg",
                Prezzo = 11.90,
                //Categorie = new Categorie() { Nome = "Vegetariana"}
                },

                new Pizza
                {Name = "Pizza Quattro Stagioni",
                Description = " La pizza alle quattro stagioni è una varietà di pizza che viene preparata con diversi condimenti, ripartiti in quattro diverse sezioni, ognuna delle quali richiama una delle quattro stagioni dell'anno. Generalmente la pizza si compone di 4 ingredienti caratterizzanti della propria stagione: i funghi rappresentano l'autunno, il prosciutto e le olive corrispondono all'inverno, i carciofi figurano la primavera ed infine i pomodori e il basilico l'estate.\r\n                                In alcune versioni, tale ultimo quarto è sostituito con del salamino piccante o con della salsiccia.",
                ImgUrl = path+"Pizza_Quattro_Stagioni.jpg",
                Prezzo = 13.90,
                },

                new Pizza{Name = "Pizza Nostromo",
                Description = " La pizza tonno e cipolle è uno dei gusti più popolari nei menu delle pizzerie. Spesso snobbata perché considerata poco digeribile soprattutto a cena,\r\n                                è in realtà molto più leggera di altre preparazioni, basta prepararla nel modo giusto..",
                ImgUrl = path+"pizza-con-cipolle-e-tonno-tropea.jpg",
                Prezzo = 12.90,
                },

                   };

               Pizze.AddRange(seed);

             }

                if (!Categorie.Any())
                {
                    var seed = new Categorie[]
                    {
                        new()
                        {
                            Nome = "Vegetariana",
                        },
                        new()
                        {
                            Nome = "Piccante"
                        },
                        new()
                        {
                            Nome = "DiMare"
                        }
                    };

                    Categorie.AddRange(seed);
                }

            if (!Ingredienti.Any())
            {
                var seed = new Ingrediente[]
                {
                        new()
                        {
                            Nome = "Mozzarella",
                        },
                        new()
                        {
                            Nome = "Pomodoro"
                        },
                        new()
                        {
                            Nome = "Salame Piccante"
                        },
                        new()
                        {
                            Nome = "Tonno"
                        },
                        new()
                        {
                            Nome = "Cipolla"
                        }
                };

                Ingredienti.AddRange(seed);
            }

            SaveChanges();
            
        }
    }
}
