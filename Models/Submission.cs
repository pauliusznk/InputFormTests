using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InputFormTests.Models
{    
    public class Submission
       {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public int? Age { get; set; }
        public string Country { get; set; }
        public string Notes { get; set; }

        public Submission(string firstName, string secondName, int? age, string country, string notes)
        {
            FirstName = firstName;
            SecondName = secondName;
            Age = age;
            Country = country;
            Notes = notes;
        }
    }
}
