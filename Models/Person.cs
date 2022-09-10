using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace iTech.Models
{
    [Keyless]
    public class Person
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string Age { get; set; } = string.Empty;
        public string Pet1 { get; set; } = string.Empty;     
        public string Pet1Type { get; set; } = string.Empty;   
        public string Pet2 { get; set; } = string.Empty;    
        public string Pet2Type { get; set; } = string.Empty;   
        public string Pet3 { get; set; } = string.Empty;
        public string Pet3Type { get; set; } = string.Empty;   
    }
}