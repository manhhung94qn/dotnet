using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GasStationData.Models;

namespace GasStationData.Repositories
{
    public class UserRepository : RepositoryBase<Models.User>, Repositories.IRepositories.IUserRepository
    {
        public UserRepository(GasStationContext dbContext) : base(dbContext)
        {
        }

        public User CheckIsAdmin(string email, string password)
        {
            User user = _dbContext.User.Where(x => x.Email == email && x.Password == password && x.UserType== "A0001").FirstOrDefault();
            if (user != null)
            {
                return user;
            }
            else return null;
        }
    }
}
