using System.ComponentModel.DataAnnotations;

namespace la_mia_pizzeria_static.Models
{
    public class Pizza
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
        public string PhotoPath { get; set; }
        public IFormFile? File { get; set; } 

        public  Pizza(int Id, string Title, string Description, double Price, string Photo)
        {
            this.Id = Id;
            this.Title = Title;
            this.Description = Description;
            this.Price = Price;
            this.PhotoPath = Photo;
        }
        public Pizza()
        {

        }

    }

    public class PizzaList
    {
        public List<Pizza> FoodMenu { get; set; }
        public PizzaList(){
            FoodMenu = new List<Pizza>();
        }
    }
}
