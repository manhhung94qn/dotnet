using GasStationData.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GasStationData.Repositories.IRepositories
{
    public interface IUserRepository: IRepositoryBase<Models.User>
    {
        User CheckIsAdmin(string email, string password);
    }
}
