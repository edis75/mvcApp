using mvcApp.Models;

namespace mvcApp.Repository.Interfaces
{
    public interface IPeopleRepository
    {
        public List<Person>? Index();
        public void Add(Person person);
        public void Update(Person person);
        public void Delete(int id);
    }
}
