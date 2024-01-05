using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp.Entities.Concrete
{
    public class Client : BaseModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }

        [ForeignKey("Company")]
        public int CompanyId { get; set; }
        public Company Company { get; set; }

    }
}
