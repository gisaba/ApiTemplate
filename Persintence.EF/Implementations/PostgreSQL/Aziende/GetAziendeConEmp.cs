using System;
using System.Runtime.CompilerServices;
using Persistence.EF.data;
using DomainModel.Services.Aziende;
using Microsoft.EntityFrameworkCore;
using DomainModel.Classes;

[assembly: InternalsVisibleTo("CompositionRoot")]

namespace Persistence.EF.Implementations.PostgreSQL.Aziende
{
	internal class GetAziendeConEmp: IGetAzienda
    {
        private readonly PgDataContext _dbContext;

        public GetAziendeConEmp(PgDataContext context)
		{
			_dbContext = context;
		}

		public AziendaEmp GetbyId(int id)
		{
            AziendaEmp result = new AziendaEmp();

            Azienda GetAzienda = new Azienda();

            try
            {
                 GetAzienda = _dbContext
                                 .Aziendas
                                 .Where(a => a.AziendaId == id)
                                 .Single();

                if (GetAzienda is null) return null;

            // Explicit Loading of Related Data
            // https://learn.microsoft.com/en-us/ef/core/querying/related-data/explicit
                _dbContext.Entry(GetAzienda)
                                  .Collection(b => b.Employees)
                                  .Query()
                                  .Where(s => s.salary >= 10000)
                                  .Load();

            //var emps = _dbContext.Entry(GetAzienda)
            //                      .Collection(b => b.Employees)
            //                      .Query()
            //                      .Where(s => s.salary >= 10000)
            //                      .ToList();

           
                result.expense = _dbContext.Entry(GetAzienda)
                                .Collection(b => b.Employees)
                                .Query()
                                .Sum(a => a.salary);

                result.numero_dipendenti = _dbContext.Entry(GetAzienda)
                   .Collection(b => b.Employees)
                   .Query()
                   .Count();

                result.id = GetAzienda.AziendaId;
                result.name = GetAzienda.name;

                // result.Employees = emps;
                result.Employees = GetAzienda.Employees;
            }
            catch
            {
                //result.expense = int.MaxValue;
                result =  new AziendaEmp();
            }

             return result;
        }
    }
}

