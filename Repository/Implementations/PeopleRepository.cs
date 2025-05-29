using Microsoft.EntityFrameworkCore;
using mvcApp.Data;
using mvcApp.Models;
using mvcApp.Repository.Interfaces;

namespace mvcApp.Repository.Implementations
{
    public class PeopleRepository : IPeopleRepository
    {
        private readonly AppDbContext _context;

        public PeopleRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(Person person)
        {
            try
            {
                _context.People.Add(person);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine("DB Error: " + ex.Message);
            }
        }

        public void Delete(int id)
        {
            var person = _context.People.Find(id);

            _context.People.Remove(person);
            _context.SaveChanges();
        }

        public List<Person>? Index()
        {
            return _context.People.OrderByDescending(p => p.Id).Take(10).ToList();
        }

        public void Update(Person person)
        {
            var existing = _context.People.FirstOrDefault(p => p.Id == person.Id);

            existing.Name = person.Name;
            existing.Title = person.Title;
            existing.Email = person.Email;
            existing.Adress = person.Adress;

            _context.SaveChanges(); // save
        }
    }
}
