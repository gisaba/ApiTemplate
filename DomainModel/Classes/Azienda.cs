using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DomainModel.Classes
{
	public class Azienda
	{
		public int AziendaId { get; set; }
		public string name { get; set; }
        public List<Employee> Employees { get; set; }
    }
}