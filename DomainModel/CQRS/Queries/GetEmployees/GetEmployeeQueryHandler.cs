using System;
using DomainModel.Classes;
using DomainModel.CQRS.Queries.GetEmployees;
using DomainModel.Services.Employees;
using System.Collections.Generic;
using CQRS.Queries;

namespace DomainModel.CQRS.Queries.GetEmployees
{
    public class GetEmployeeQueryHandler: IQueryHandler<GetEmployeeQuery, GetEmployeeQueryResult>
    {
        private readonly IGetEmployee employee;

        public GetEmployeeQueryHandler(IGetEmployee employee)
        {
            this.employee = employee ?? throw new ArgumentNullException(nameof(employee));
        }

        public GetEmployeeQueryResult Handle(GetEmployeeQuery query)
        {
          // IEnumerable<Employee> employees = this.employee.Get(query);

          IEnumerable<Employee> employees = this.employee.GetAll(query);

          return new GetEmployeeQueryResult(employees);
        }
    }
}

