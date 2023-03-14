using System;
using System.Data;
using DomainModel.CQRS.Commands.User.AddUser;
using DomainModel.CQRS.Queries.GetUsers;
using DomainModel.Services.User;
using Microsoft.EntityFrameworkCore;
using Persistence.EF.data;
using DomainModel.Classes.User;
using Serilog;
using System.Linq;

namespace Persistence.EF.Implementations.SQLServer.Users
{
    internal class GetUsers : IGetUsers
    {
        private readonly DataContext dbContext;

        public GetUsers(DataContext DataContext)
        {
            this.dbContext = DataContext;
        }

        public IEnumerable<UserModel> Get()
        {
             List<UserModel> users = dbContext.Users
                    .FromSqlRaw($"SELECT * FROM dbo.Users")
                    .ToList();

             /*
                select row_number() over(ORDER BY t.seq_id) 
                as rowid, t.*
                from api.employee t  -- order by t.seq_id
                limit 100
             */

            dbContext.Dispose();

             IEnumerable<UserModel> enumerable = (IEnumerable<UserModel>)users;


             return (IEnumerable<UserModel>)enumerable;
        }

        public IEnumerable<UserModel> getbyUsername(string usenameToFind)
        {
            List<UserModel> users = new List<UserModel>().ToList();

            try
            {
                using (var context = dbContext)
                {
                    UserModel user = context.Users.AsNoTracking().Single(usr => usr.username == usenameToFind);

                    users.Add(user);
                }
            }
            catch { }

            return users;
        }
    }
}
