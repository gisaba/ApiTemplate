using System;
using DomainModel.Classes;
using DomainModel.CQRS.Queries.GetEmployees;
using DomainModel.Services.Employees;
using Persistence.EF.data;

namespace Persistence.EF.Implementations.SQLServer.Employees
{
    internal class GetEmployee : IGetEmployee
    {
        private readonly DataContext dbContext;

        public GetEmployee(DataContext DataContext)
        {
            this.dbContext = DataContext;
        }

        //public IEnumerable<Employee> Get(UInt16 lastId, UInt16 pageSize, string company, string name)
        public IEnumerable<Employee> Get(GetEmployeeQuery query)
        {
            List<Employee> nextPage = new List<Employee>().ToList();

            using (var context = dbContext)
            {
                /* Offset pagination https://learn.microsoft.com/en-us/ef/core/querying/pagination#offset-pagination */

                if (query.PageNumber > 1000) query.PageSize = 1000;

                var position = query.PageNumber * query.PageSize;

                //int nrec = context.Employees.Where(emp => emp.company.Contains(query.company)).ToList().Count;

                nextPage = context.Employees
                    //.Where(emp => emp.company.Contains(query.company))
                    .OrderBy(e => e.seq_id) // ordinamento su qualsiasi chiave di ordinamento , qui su seq_id per testar corrispondenza
                    .ThenBy(e => e.salary)
                    .Skip(position)
                    .Take(query.PageSize)
                    .ToList();



                /*Keyset pagination https://learn.microsoft.com/en-us/ef/core/querying/pagination#keyset-pagination */

                //if (!String.IsNullOrEmpty(query.company))
                //{

                //    nextPage = context.Employees
                //        .OrderBy(e => e.seq_id)
                //        .Where(e => e.seq_id > query.lastId)
                //        .Where(e => e.company.Contains(query.company))
                //        .Take(query.pageSize)
                //        .ToList();
                //}
                //else
                //{
                //    nextPage = context.Employees
                //        .OrderBy(e => e.seq_id)
                //        .Where(e => e.seq_id > query.lastId)
                //        .Take(query.pageSize)
                //        .ToList();
                //}
            }

            return nextPage;

            /*
               select row_number() over(ORDER BY t.seq_id) 
               as rowid, t.*
               from api.employee t  -- order by t.seq_id
               limit 100
            */

        }

        public IEnumerable<Employee> getbyName(string nameToFind)
        {

            IEnumerable<Employee> Employees = dbContext.Employees.Where(emp => emp.name == nameToFind).ToList();

            return Employees;
        }

        public IEnumerable<Employee> GetAll(GetEmployeeQuery query)
        {
            List<Employee> nextPage = new List<Employee>().ToList();

            using (var context = dbContext)
            {
                /* Offset pagination https://learn.microsoft.com/en-us/ef/core/querying/pagination#offset-pagination */

                nextPage = context.Employees
                    .OrderBy(e => e.seq_id) // ordinamento su qualsiasi chiave di ordinamento , qui su seq_id per testar corrispondenza
                    .ThenBy(e => e.salary)
                    .Skip((query.PageNumber - 1) * query.PageSize)
                    .Take(query.PageSize)
                    .ToList();

                /*Keyset pagination https://learn.microsoft.com/en-us/ef/core/querying/pagination#keyset-pagination */

                //if (!String.IsNullOrEmpty(query.company))
                //{

                //    nextPage = context.Employees
                //        .OrderBy(e => e.seq_id)
                //        .Where(e => e.seq_id > query.lastId)
                //        .Where(e => e.company.Contains(query.company))
                //        .Take(query.pageSize)
                //        .ToList();
                //}
                //else
                //{
                //    nextPage = context.Employees
                //        .OrderBy(e => e.seq_id)
                //        .Where(e => e.seq_id > query.lastId)
                //        .Take(query.pageSize)
                //        .ToList();
                //}
            }

            return nextPage;

            /*
               select row_number() over(ORDER BY t.seq_id) 
               as rowid, t.*
               from api.employee t  -- order by t.seq_id
               limit 100
            */

        }
    }
}

