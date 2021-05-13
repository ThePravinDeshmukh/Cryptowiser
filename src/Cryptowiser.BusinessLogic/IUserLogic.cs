using Cryptowiser.Models;
using System.Collections.Generic;

namespace Albellicart.BusinessLogic
{
    public interface IUserLogic
    {
        IEnumerable<User> GetAll();
        User GetById(int id);
    }
}