using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Fitnessstudio.Models
{
    public class PersonWithAddress
    {
        public Person Person { get; set; }
        public Anschrift Anschrift { get; set; }

        public string BgColor { get; set; }
        public string Character { get; set; }
        public string Name { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public PersonWithAddress(Person person, Anschrift anschrift)
        {
            Person = person;
            Anschrift = anschrift;
        }
    }




}
