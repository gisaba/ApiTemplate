using System;
using DomainModel.Classes;
using DomainModel.CQRS.Queries.GetAziendaEmp;

namespace DomainModel.Services.Aziende
{
	public interface IGetAzienda
	{
        AziendaEmp GetbyId(int id);
	}
}

