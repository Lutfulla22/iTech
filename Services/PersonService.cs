using EFCore.BulkExtensions;
using iTech.Models;
using Microsoft.EntityFrameworkCore;

namespace iTech.Services
{
    public class PersonService : IPersonService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<PersonService> _logger;

        public PersonService(AppDbContext context, ILogger<PersonService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public List<Person> InsertAsync(List<Person> person)
        {
             _context.BulkInsert(person);
            return person;
        }

        public List<Person> GetAll()
            => _context.People.ToList();
    }
}