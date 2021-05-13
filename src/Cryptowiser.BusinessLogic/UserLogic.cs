using Cryptowiser.Models;
using Cryptowiser.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Albellicart.BusinessLogic
{
    public class UserLogic : IUserLogic
    {
        private readonly IUserRepository _UserRepository;
        public UserLogic(IUserRepository UserRepository)
        {
            _UserRepository = UserRepository;
        }
        public IEnumerable<User> GetAll()
        {
            return _UserRepository.GetAll();
        }
        public User GetById(int id)
        {
            return _UserRepository.GetById(id);
        }
        public User Create(User user, string password)
        {
            return _UserRepository.Create(user, password);
        }
    }
}
