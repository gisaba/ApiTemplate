using System;
using System.Collections.Generic;
using CQRS.Queries;
using DomainModel.Classes;

namespace DomainModel.CQRS.Queries.GetAziendaEmp
{
	public class GetAziendaEmpQuery: IQuery<GetAziendaEmpQueryResult>
	{
		public int id { get; set; }
		//public string name { get; set; }
		//public int numero_dipendenti { get; set; }
		//public int expense { get; set; }
		//public List<Employee> Employees { get; set; }
    }
}