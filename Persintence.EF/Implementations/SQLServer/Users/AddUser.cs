using System;
using DomainModel.Classes.User;
using DomainModel.CQRS.Commands.User.AddUser;
using DomainModel.Services.User;
using Persistence.EF.data;
using Serilog;

namespace Persistence.EF.Implementations.SQLServer.Users
{
    internal class AddUser : IAddUser
    {
        private readonly DataContext dbContext;

        public AddUser(DataContext DataContext)
        {
            this.dbContext = DataContext;
        }

        public void Save(AddUserCommand users)
        {
               try
               {
                UserModel Utente = new UserModel();
                // Utente.Id = users.Id;
                Utente.firstName = users.FirstName;
                Utente.lastName = users.LastName;
                Utente.username = users.Username;
                Utente.timestamp = System.DateTime.Now.ToString();
                Utente.active = true;


                string passwordHash = BCrypt.Net.BCrypt.HashPassword(users.Password);
                Utente.password = passwordHash;

                dbContext.Add(Utente);
                dbContext.SaveChanges();
                dbContext.Dispose();
           }
            catch(Exception Ex)
            {
                Log.Debug("Errore Insert : {@classe} {@message}", this.ToString(), Ex.Message);
                throw new NotImplementedException("Errore Durante Inserimento", Ex.InnerException);
            }       
        }
    }
}