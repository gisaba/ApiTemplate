using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQRS.Queries;
using DomainModel.CQRS.Queries.GetAziendaEmp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RockApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AziendeController : ControllerBase
    {
        private readonly IQueryHandler<GetAziendaEmpQuery, GetAziendaEmpQueryResult> queryHandler;

        public AziendeController(IQueryHandler<GetAziendaEmpQuery, GetAziendaEmpQueryResult> queryHandler)
        {
            this.queryHandler = queryHandler ?? throw new ArgumentNullException(nameof(queryHandler));
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] GetAziendaEmpQuery getAziendaEmpQuery)
        {
          
            var azienda = queryHandler.Handle(getAziendaEmpQuery);

            if (azienda.AziendaQuery.name is null)
                return NotFound();

            //await context.Customers.Where(a => a.Id == id).FirstOrDefaultAsync();
            var res = new Response<GetAziendaEmpQueryResult>(azienda, 0, 1);
            res.Message = "Employees with salary >= 10000";
            return Ok(res);
        }
    }
}
