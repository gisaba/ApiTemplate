using System;
using CQRS.Queries;
using DomainModel.Classes;
using DomainModel.Services.Aziende;

namespace DomainModel.CQRS.Queries.GetAziendaEmp
{
	public class GetAziendaEmpQueryHandler: IQueryHandler<GetAziendaEmpQuery, GetAziendaEmpQueryResult>
    {
		private readonly IGetAzienda getAzienda;

        public GetAziendaEmpQueryHandler(IGetAzienda getAzienda)
        {
            this.getAzienda = getAzienda ?? throw new ArgumentNullException(nameof(getAzienda));
        }

        public GetAziendaEmpQueryResult Handle(GetAziendaEmpQuery query)
        {
            AziendaEmp getAzienda = this.getAzienda.GetbyId(query.id);

            return new GetAziendaEmpQueryResult(getAzienda);
        }
    }
}

