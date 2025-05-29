using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using mvcApp.Models;
using mvcApp.Repository.Interfaces;

namespace mvcApp.Controllers
{
    public class PeopleController : Controller
    {

        private readonly IPeopleRepository _repository;

        public PeopleController(IPeopleRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            var lastPerson = _repository.Index();
            return View(lastPerson);
        }

        // ✅ Partial olarak tabloyu yenilemek için
        public IActionResult GetPeoplePartial()
        {
            var lastPerson = _repository.Index();
            return PartialView("_PeopleTable", lastPerson);
        }
        [HttpPost]
        public IActionResult Create([FromForm] Person person)
        {
            Console.WriteLine("Create çağrıldı");

            Console.WriteLine($"Gelen Name: '{person.Name}'"); 

            if (!ModelState.IsValid)
            {
                Console.WriteLine("ModelState errr:");
                foreach (var entry in ModelState)
                {
                    foreach (var error in entry.Value.Errors)
                    {
                        Console.WriteLine($"Alan: {entry.Key}, Hata: {error.ErrorMessage}");
                    }
                }
                return BadRequest("ModelState hatalı");
            }

            Console.WriteLine("ModelState GEÇERLİ, kayıt ekleniyor");
            _repository.Add(person);
            return Ok("Kayıt başarılı");
        }





        [HttpPost]
        public IActionResult Edit([FromForm] Person person)
        {
            if (ModelState.IsValid)
            {
                _repository.Update(person);

                return Ok();
            }

            return BadRequest();
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _repository.Delete(id);
            return Ok();
        }


      
    }
}
