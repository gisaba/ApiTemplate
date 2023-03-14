using System;
using DomainModel.Classes;
using DomainModel.Services.Employees;
using System.Collections.Generic;
using DomainModel.CQRS.Queries.GetEmployees;
using CQRS.Queries;

namespace DomainModel.CQRS.Queries.GetEmployeesByName
{
    public class GetEmployeesByNameQueryHandler: IQueryHandler<GetEmployeesByNameQuery, GetEmployeesByNameQueryResult>
    {
        private readonly IGetEmployee employee;

        public GetEmployeesByNameQueryHandler(IGetEmployee employee)
        {
            this.employee = employee ?? throw new ArgumentNullException(nameof(employee));
        }

        public GetEmployeesByNameQueryResult Handle(GetEmployeesByNameQuery query)
        {
            IEnumerable<Employee> employees  = this.employee.getbyName(query.name);

            return new GetEmployeesByNameQueryResult(employees);
        }
    }
}

