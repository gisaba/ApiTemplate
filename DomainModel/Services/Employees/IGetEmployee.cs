using DomainModel.Classes;
using DomainModel.Classes.User;
using DomainModel.CQRS.Queries.GetEmployees;
using DomainModel.CQRS.Queries.GetUsers;
using System;
using System.Collections.Generic;
using System.Text;


namespace DomainModel.Services.Employees
{
    public interface IGetEmployee
    {
        //IEnumerable<Employee> Get(UInt16 lastId, UInt16 pageSize, string company, string name);

        IEnumerable<Employee> Get(GetEmployeeQuery query);

        IEnumerable<Employee> GetAll(GetEmployeeQuery query);

        IEnumerable<Employee> getbyName(string name);
    }
}
