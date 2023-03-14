using System;
using DomainModel.Classes;

namespace DomainModel.CQRS.Queries.GetAziendaEmp
{
	public class GetAziendaEmpQueryResult
	{
		public GetAziendaEmpQueryResult(AziendaEmp getAzienda)
        {
			AziendaQuery = getAzienda ?? throw new ArgumentNullException(nameof(getAzienda));

        }
		public AziendaEmp AziendaQuery { get; }
    }
}