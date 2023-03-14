using System;
using DomainModel.Classes;
using System.Collections.Generic;

namespace DomainModel.CQRS.Queries.GetEmployees
{
    public class GetEmployeeQueryResult
    {
        public GetEmployeeQueryResult(IEnumerable<Employee> employees)
        {
            Employees = employees ?? throw new ArgumentNullException(nameof(employees));
        }

        public IEnumerable<Employee> Employees { get; }
    }
}

