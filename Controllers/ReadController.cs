using System.Globalization;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using ClosedXML.Excel;
using CsvHelper;
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
        private readonly IWebHostEnvironment _environment;

        public ReadController(IPersonService person, IWebHostEnvironment environment)
        {
            _person = person;
            _environment = environment;
        }

        [HttpPost("xlsx")]
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

        // [HttpGet("Get")]
        // public  List<Person> write(IFormFile file)
        // {
        //     var person = new List<Person>();

        //     XmlDocument doc = new XmlDocument();
        //     doc.Load(string.Concat(_environment.WebRootPath, $"/iTech.xml"));
        //     foreach(XmlNode node in doc.SelectNodes("Person"))
        //     {
        //         person.Add(new Person
        //         {
        //             Id = Guid.Parse(node["Id"].InnerText),
        //             Name = node["Name"].InnerText,
        //             Pet1 = node["Pet1"].InnerText,
        //             Pet1Type = node["Pet1Type"].InnerText,
        //             Pet2 = node["Pet2"].InnerText,
        //             Pet2Type = node["Pet2Type"].InnerText,
        //             Pet3 = node["Pet3"].InnerText,
        //             Pet3Type = node["Pet3Type"].InnerText
        //         });
                
        //     }
        //     return person;
            
        // }

        [HttpGet("DataFromXml")]
        public async Task<List<Person>> write()
        {
            var person = new List<Person>();
            
                XDocument doc = XDocument.Load(@"C:\Prog\iTech\xml\iTech.xml");
                foreach(XElement element in doc.Descendants("Person"))
                {
                    Person per = new Person();
                    per.Id = Guid.Parse(element.Element("Id").Value);
                    per.Age = element.Element("Age").Value;
                    per.Pet1 = element.Element(name: "Pet1").Value;
                    per.Pet1Type = element.Element(name: "Pet1Type").Value;
                    per.Pet2 = element.Element(name: "Pet2").Value;
                    per.Pet2Type = element.Element(name: "Pet2Type").Value;
                    per.Pet3 = element.Element(name: "Pet3").Value;
                    per.Pet3Type = element.Element(name: "Pet3Type").Value;
                    person.Add(per);
                }
            
            return person;
        }
        
            
        
    }
}