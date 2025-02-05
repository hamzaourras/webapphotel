using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace webapphotel.Model
{
    public class TableTest
    {
        [Key]
        public Guid AdvancedId { get; set; }
        public string? Description { get; set; }
    }
}
