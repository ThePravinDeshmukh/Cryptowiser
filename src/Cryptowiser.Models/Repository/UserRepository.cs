﻿using Cryptowiser.Models.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cryptowiser.Models.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly CryptowiserContext _context;
        public UserRepository(CryptowiserContext context)
        {
            _context = context;
        }

        public User Authenticate(string Username, string password)
        {
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(password))
                return null;

            var user = _context.Users.SingleOrDefault(x => x.Username == Username);

            // check if Username exists
            if (user == null)
                return null;

            // check if password is correct
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            // authentication successful
            return user;
        }
        public IEnumerable<User> GetAll()
        {
            return _context.Users;
        }

        public User GetById(int id)
        {
            return _context.Users.Find(id);
        }
        public User Create(User user, string password)
        {
            // validation
            if (string.IsNullOrWhiteSpace(password))
                throw new ValidationException("VE001", Constants.PASSWORD_EMPTY_ERROR);

            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            if (_context.Users.Any(x => x.Username == user.Username))
                throw new ValidationException("VE002", Constants.USERNAME_TAKEN);

            _context.Users.Add(user);
            _context.SaveChanges();

            return user;
        }
        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException(Constants.PASSWORD);
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException(Constants.VALUE_EMPTY_ERROR, Constants.PASSWORD);

            using var hmac = new System.Security.Cryptography.HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
        public void Update(User userParam, string password = null)
        {
            var user = _context.Users.Find(userParam.Id);

            if (user == null)
                throw new ValidationException("VE003", Constants.USER_NOT_FOUND);

            // update username if it has changed
            if (!string.IsNullOrWhiteSpace(userParam.Username) && userParam.Username != user.Username)
            {
                // throw error if the new username is already taken
                if (_context.Users.Any(x => x.Username == userParam.Username))
                    throw new ValidationException("VE003", Constants.USERNAME_TAKEN);

                user.Username = userParam.Username;
            }

            // update user properties if provided
            if (!string.IsNullOrWhiteSpace(userParam.FirstName))
                user.FirstName = userParam.FirstName;

            if (!string.IsNullOrWhiteSpace(userParam.LastName))
                user.LastName = userParam.LastName;

            // update password if provided
            if (!string.IsNullOrWhiteSpace(password))
            {
                CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
            }

            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var user = _context.Users.Find(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            string passwordHash = Constants.PASSWORD_HASH;
            if (password == null) throw new ArgumentNullException(Constants.PASSWORD);
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException(Constants.VALUE_EMPTY_ERROR, Constants.PASSWORD);
            if (storedHash.Length != 64) throw new ArgumentException(Constants.INVALID_LENGTH_64, passwordHash);
            if (storedSalt.Length != 128) throw new ArgumentException(Constants.INVALID_LENGTH_128, passwordHash);

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }
    }
}
