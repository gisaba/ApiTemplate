using System;
using CQRS.Queries;

namespace DomainModel.CQRS.Queries.GetEmployeesByName
{
    public class GetEmployeesByNameQuery : IQuery<GetEmployeesByNameQueryResult>
    {
       public string name { get; set; }
    }
}

