using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using iTech.Models;
using iTech.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;

namespace iTech.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReadController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IPersonService _person;

        public ReadController(IPersonService person)
        {
            _person = person;
        }

        [HttpPost]
        public async Task<ActionResult> Create (IFormFile file)
        {
            var person = new List<Person>();

            using(var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    var rowcount = worksheet.Dimension.Rows;
                   
                    for(int row = 2; row <= rowcount; row++)
                    {
                        
                        person.Add(new Person{
                                Id = Guid.NewGuid(),
                                Name = worksheet.Cells[row, 1].Value.ToString().Trim(),
                                Age = worksheet.Cells[row, 2].Value.ToString().Trim(),
                                Pet1 = worksheet.Cells[row, 3].Value.ToString().Trim(),
                                Pet1Type = worksheet.Cells[row, 4].Value.ToString().Trim(),
                                Pet2 = worksheet.Cells[row, 5].Value.ToString().Trim(),
                                Pet2Type = worksheet.Cells[row, 6].Value.ToString().Trim(),
                                Pet3 = worksheet.Cells[row, 7].Value.ToString().Trim(),
                                Pet3Type = worksheet.Cells[row, 8].Value.ToString().Trim(),
                            });
                    }
                    var result =  _person.InsertAsync(person);                    
                }
            }
                XmlSerializer serialiser = new XmlSerializer(typeof(List<Person>));

                using(var write = new StreamWriter(@"C:\Prog\iTech\xml\itech.xml"))
                {
                    serialiser.Serialize(write, person);
                }

                return Ok(serialiser);
        }

        [HttpGet]
        public  List<Person> Get()
         => _person.GetAll();
            
        
    }
}