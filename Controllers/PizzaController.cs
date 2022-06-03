using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using la_mia_pizzeria_static.Models;
namespace la_mia_pizzeria_static.Controllers
{
    public class PizzaController : Controller
    {
        // GET: PizzaController
        public static PizzaList PizzaMenu = null;
        
        public ActionResult Index()
        {
            if(PizzaMenu == null)
            {
                Pizza FirstPizza = new Pizza(1, "Margherita", "Pizza margherita", 7.99, "~/img/prima_pizza.jpg");
                Pizza SecondPizza = new Pizza(2, "Boscaiola", "Pizza Boscaiola", 8.99, "~/img/seconda_pizza.jpg");
                Pizza ThirdPizza = new Pizza(3, "Salsiccia", "Pizza Salsiccia", 9.99, "~/img/terza_pizza.jpg");
                PizzaMenu = new();
                PizzaMenu.FoodMenu.Add(FirstPizza);
                PizzaMenu.FoodMenu.Add(SecondPizza);
                PizzaMenu.FoodMenu.Add(ThirdPizza);
            }
            return View(PizzaMenu);
        }

        // GET: PizzaController/Details/5
        public ActionResult Details(int id)
        {
            foreach(var p in PizzaMenu.FoodMenu)
                if(p.Id == id)
                    ViewData["Pizza"] = p;
            return View();
        }

        // GET: PizzaController/Create
        public ActionResult Create()
        {

            Pizza NewPizza = new Pizza
            {
                Id = 0,
                Title = "",
                Description = "",
                Price = 0.00,
                File = null
            };
            return View(NewPizza);
        }

        // POST: PizzaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Store(Pizza Result)
        {
            if(!ModelState.IsValid)
                return View("Create",Result);
            //Estrazione File e salvataggio su file system.
            //Agendo su Request ci prendiamo il file e lo salviamo su file system.
            string PhotoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files");

            if (!Directory.Exists(PhotoPath))
                Directory.CreateDirectory(PhotoPath);

            FileInfo fileInfo = new FileInfo(Result.File.FileName);
            string fileName = Result.Title + fileInfo.Extension;

            string fileNameWithPath = Path.Combine(PhotoPath, fileName);

            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
            {
                Result.File.CopyTo(stream);
            }


            Pizza NewPizza = new Pizza()
            {
                Id = Result.Id,
                Title = Result.Title,
                Description = Result.Description,
                Price = Result.Price,
                PhotoPath = $"/Files/{fileName}",
                File= Result.File
            };
            PizzaMenu = new();
            PizzaMenu.FoodMenu.Add(NewPizza);
            return View("Index", PizzaMenu);
        }

        // GET: PizzaController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PizzaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PizzaController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PizzaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
