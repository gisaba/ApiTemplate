using System;
using CQRS.Queries;
using DomainModel.CQRS.Queries.GetEmployees;

namespace DomainModel.CQRS.Queries.GetEmployees
{
    public class GetEmployeeQuery: IQuery<GetEmployeeQueryResult>
    {
       public string name { get; set; }
       public string company { get; set; }
       public int PageNumber { get; set; }
       public int PageSize { get; set; }
    }
}

