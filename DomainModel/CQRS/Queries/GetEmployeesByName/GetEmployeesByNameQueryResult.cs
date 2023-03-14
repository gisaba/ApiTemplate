using System;
using DomainModel.Classes;
using System.Collections.Generic;

namespace DomainModel.CQRS.Queries.GetEmployeesByName
{
    public class GetEmployeesByNameQueryResult
    {
        public GetEmployeesByNameQueryResult(IEnumerable<Employee> employees)
        {
            Employees = employees ?? throw new ArgumentNullException(nameof(employees));
        }

        public IEnumerable<Employee> Employees { get; }
    }
}

