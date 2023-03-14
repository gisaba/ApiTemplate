using System;
using SimpleInjector;
using DomainModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Persistence.EF.data;

namespace CompositionRoot
{
    /// <summary>
    /// This class contains all the custom application bindings.
    /// </summary>
    internal static class CustomBindings
    {
        internal static void Bind(Container container)
        {
            // Put here the bindings of your own custom services


            //string DB = "SQL";
            string DB = "PG";

            if (DB == "SQL")
            {
                container.Register<
                    Persistence.EF.data.DataContext>(Lifestyle.Scoped);

                container.Register<
                       DomainModel.Services.User.IAuthenticateUser,
                       Persistence.EF.Implementations.SQLServer.Users.AuthenticateUser>(Lifestyle.Scoped);

                container.Register<
                  DomainModel.Services.User.IGetUsers,
                  Persistence.EF.Implementations.SQLServer.Users.GetUsers>(Lifestyle.Scoped);

                container.Register<DomainModel.Services.User.IAddUser,
                    Persistence.EF.Implementations.SQLServer.Users.AddUser>(Lifestyle.Scoped);

                container.Register<DomainModel.Services.User.IUpdateUser,
                    Persistence.EF.Implementations.SQLServer.Users.UpdateUser>(Lifestyle.Scoped);

                container.Register<DomainModel.Services.User.IDeleteUser,
                    Persistence.EF.Implementations.SQLServer.Users.DeleteUser>(Lifestyle.Scoped);

                container.Register<
                   DomainModel.Services.Employees.IGetEmployee,
                   Persistence.EF.Implementations.SQLServer.Employees.GetEmployee>(Lifestyle.Scoped);

            }
            else
            {
                container.Register<
                    Persistence.EF.data.PgDataContext>(Lifestyle.Scoped);

                container.Register<
                       DomainModel.Services.User.IAuthenticateUser,
                       Persistence.EF.Implementations.PostgreSQL.Users.AuthenticateUser>(Lifestyle.Scoped);

                container.Register<
                  DomainModel.Services.User.IGetUsers,
                  Persistence.EF.Implementations.PostgreSQL.Users.GetUsers>(Lifestyle.Scoped);

                container.Register<DomainModel.Services.User.IAddUser,
                    Persistence.EF.Implementations.PostgreSQL.Users.AddUser>(Lifestyle.Scoped);

                container.Register<DomainModel.Services.User.IUpdateUser,
                    Persistence.EF.Implementations.PostgreSQL.Users.UpdateUser>(Lifestyle.Scoped);

                container.Register<DomainModel.Services.User.IDeleteUser,
                    Persistence.EF.Implementations.PostgreSQL.Users.DeleteUser>(Lifestyle.Scoped);

                /**** ESEMPIO MULTI DATABASE **/
                //container.Register<
                //    Persistence.EF.data.DataContext>(Lifestyle.Scoped);

                //container.Register<
                //   DomainModel.Services.Employees.IGetEmployee,
                //   Persistence.EF.Implementations.SQLServer.Employees.GetEmployee>(Lifestyle.Scoped);
                /*****************************/

                container.Register<
                    DomainModel.Services.Employees.IGetEmployee,
                    Persistence.EF.Implementations.PostgreSQL.Employees.GetEmployee>(Lifestyle.Scoped);

                container.Register<DomainModel.Services.Aziende.IGetAzienda,
                    Persistence.EF.Implementations.PostgreSQL.Aziende.GetAziendeConEmp>(Lifestyle.Scoped);
            }
            
            
        }
    }
}