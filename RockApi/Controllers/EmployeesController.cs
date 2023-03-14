using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQRS.Queries;
using DomainModel.CQRS.Queries.GetEmployees;
using DomainModel.CQRS.Queries.GetEmployeesByName;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DomainModel.Classes;
using Persistence.EF.data;

namespace RockApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly PgDataContext dbContext;

        // Query Handler
        private readonly IQueryHandler<GetEmployeesByNameQuery, GetEmployeesByNameQueryResult> queryEmployeByNameHandler;
       private readonly IQueryHandler<GetEmployeeQuery, GetEmployeeQueryResult> queryEmployeHandler;

       public EmployeesController(PgDataContext DataContext,
           IQueryHandler<GetEmployeesByNameQuery, GetEmployeesByNameQueryResult> queryHandler,
           IQueryHandler<GetEmployeeQuery, GetEmployeeQueryResult> queryHandler1)
       {
            this.queryEmployeByNameHandler = queryHandler ?? throw new ArgumentNullException(nameof(queryHandler));

            this.queryEmployeHandler = queryHandler1 ?? throw new ArgumentNullException(nameof(queryHandler1));

            this.dbContext = DataContext;
        }

        // GET: api/Employee/name
        //[HttpGet("{name}")]
        ////[Route("api/employees/name")]
        //public GetEmployeesByNameQueryResult GetByName([FromRoute] GetEmployeesByNameQuery getEmployeesByNameQuery)
        //{
        //    return this.queryEmployeByNameHandler.Handle(getEmployeesByNameQuery);
        //}

        // GET: api/Employee/name
        [HttpGet("{name}")]
        public IActionResult GetByName([FromRoute] GetEmployeesByNameQuery getEmployeesByNameQuery)
        {
            var emp = queryEmployeByNameHandler.Handle(getEmployeesByNameQuery);

            if (emp.Employees.Count() == 0) return NotFound();

            //await context.Customers.Where(a => a.Id == id).FirstOrDefaultAsync();
            return Ok(new Response<GetEmployeesByNameQueryResult>(emp,0,1));
        }

        //// GET: api/Employees
        [HttpGet]
        public GetEmployeeQueryResult Get([FromQuery] GetEmployeeQuery getEmployeeQuery)
        {
            return this.queryEmployeHandler.Handle(getEmployeeQuery);
        }

        // GET: api/Employees/all
        [HttpGet("all")]
        public async Task<IActionResult> GetAll([FromQuery] GetEmployeeQuery query)
        {
            var validFilter = new PaginationFilter(query.PageNumber, query.PageSize);
            query.PageSize = validFilter.PageSize;
            query.PageNumber = validFilter.PageNumber;
            
            var Totale = await Task.Run(() => Conta(query));

            //var Totale = Conta(query);

            IEnumerable<Employee> app = new List<Employee>();

            GetEmployeeQueryResult response = new GetEmployeeQueryResult(app);

            response = await Task.Run(() => this.queryEmployeHandler.Handle(query));
            //return Ok(response);
            return Ok(new ResponseWithTot<GetEmployeeQueryResult>(response, query.PageNumber, query.PageSize,Totale));
        }

        // GET: api/Employees/all
        [HttpGet("allSync")]
        public IActionResult GetAllSync([FromQuery] GetEmployeeQuery query)
        {
            var validFilter = new PaginationFilter(query.PageNumber, query.PageSize);
            query.PageSize = validFilter.PageSize;
            query.PageNumber = validFilter.PageNumber;

            var Totale = Conta(query);

            IEnumerable<Employee> app = new List<Employee>();

            GetEmployeeQueryResult response = new GetEmployeeQueryResult(app);

            response = this.queryEmployeHandler.Handle(query);
            //return Ok(response);
            return Ok(new ResponseWithTot<GetEmployeeQueryResult>(response, query.PageNumber, query.PageSize, Totale));
        }

        private Task<int> Conta1(GetEmployeeQuery query)
        {
            return Task.Run(() =>
            {
                return dbContext.Employees.Where(e => e.company.Equals("APPLE")).ToList().Count();
            });          
        }

        private int Conta(GetEmployeeQuery query)
        {
          return dbContext.Employees.Where(e => e.company.Equals("APPLE")).ToList().Count();    
        }

        // GET: api/Employees/all
        //[HttpGet("all")]
        //public GetEmployeeQueryResult GetAll([FromQuery] GetEmployeeQuery query)
        //{
        //    var validFilter = new PaginationFilter(query.PageNumber, query.PageSize);
        //    query.PageSize = validFilter.PageSize;
        //    query.PageNumber = validFilter.PageNumber;

        //    return this.queryEmployeHandler.Handle(query);
        //}
    }
}