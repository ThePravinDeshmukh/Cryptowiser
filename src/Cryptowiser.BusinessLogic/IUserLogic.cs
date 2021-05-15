using Cryptowiser.Models;
using System.Collections.Generic;

namespace Cryptowiser.BusinessLogic
{
    public interface IUserLogic
    {
        IEnumerable<User> GetAll();
        User GetById(int id);
    }
}