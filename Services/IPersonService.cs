using iTech.Models;

namespace iTech.Services
{
    public interface IPersonService
    {
        List<Person> GetAll();
        List<Person> InsertAsync(List<Person> person);
    }
}