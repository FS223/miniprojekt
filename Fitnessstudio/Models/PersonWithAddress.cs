using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Fitnessstudio.Models
{
    public class PersonWithAddress
    {
        public Person Person { get; set; }
        public Anschrift Anschrift { get; set; }

        public PersonWithAddress(Person person, Anschrift anschrift) 
        { 
            Person = person;
            Anschrift = anschrift;
        }
    }

}
