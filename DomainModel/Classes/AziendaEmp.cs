using System;
using System.Collections.Generic;

namespace DomainModel.Classes
{
	public class AziendaEmp
	{
        public int id { get; set; }
        public string name { get; set; }
        public int numero_dipendenti { get; set; }
        public int expense { get; set; }
        public List<Employee> Employees { get; set; }
    }
}

