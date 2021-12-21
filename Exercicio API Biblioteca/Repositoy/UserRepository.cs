using Exercicio_API_Biblioteca.Entidades;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Exercicio_API_Biblioteca.Repositoy
{
    public class UserRepository
    {
        private const int LOGIN_FAILED_LIMIT = 3;
        private readonly Dictionary<Guid, User> _users;

        public UserRepository()
        {
            _users ??= new Dictionary<Guid, User>();
        }

        public IEnumerable<User> Get()
        {
            return _users.Values;
        }
        public User Get(Guid id)
        {
            if (_users.TryGetValue(id, out var user))
                return user;

            throw new Exception("Usuário não encontrado");
        }
        public User GetByUsername(string username)
        {
            return _users.Values.Where(u => u.Username == username).FirstOrDefault();

        }

        public User Create(User user)
        {
            user.Id = Guid.NewGuid();
            if (_users.TryAdd(user.Id, user))
                return user;

            throw new Exception("Não foi possivel criar esse usuário");
        }
        public bool Remove(Guid id)
        {
            return _users.Remove(id);
        }

        public User Update(Guid id, User user)
        {
            if (_users.TryGetValue(id, out var userToUpdate))
            {
                userToUpdate.Id = id;
                userToUpdate.Role = user.Role;
                userToUpdate.Username = user.Username;
                userToUpdate.Password = user.Password;

                return Get(id);

            }

            throw new Exception("Não foi possivel atualizar o usuario");

        }
        public LoginResult Login(string username, string password)
        {

            try
            {
                var user = _users.Values.Where(u => u.Username == username && u.Password == password).SingleOrDefault();

                if (user != null)
                {
                    if (user.IsLockout)
                    {
                        if (DateTime.Now <= user.LockoutDate?.AddMinutes(15))
                        {
                            return LoginResult.ErrorResult(UserBlockedException.USER_BLOCKED_EXCEPTION);

                        }
                        else
                        {
                            user.IsLockout = false;
                            user.LockoutDate = null;
                            user.FailedAttempts = 0;
                        }
                    }

                    return LoginResult.SucessResult(user);

                }
                var userExistsForUsername = _users.Values.Where(u => u.Username == username).Any();

                if (userExistsForUsername)
                {
                    user = _users.Values.Where(u => u.Username == username).Single();

                    user.FailedAttempts++;

                    if (user.FailedAttempts > LOGIN_FAILED_LIMIT)
                    {
                        user.IsLockout = true;
                        user.LockoutDate = DateTime.Now;



                        return LoginResult.ErrorResult(UserBlockedException.USER_BLOCKED_EXCEPTION);


                    }

                    return LoginResult.ErrorResult(InvalidPasswordException.INVALID_PASSWORD_EXCEPTION);


                }
                return LoginResult.ErrorResult(InvalidUsernameException.INVALID_USERNAME_EXCEPTION);
            }
            catch (Exception e)
            {
                return LoginResult.ErrorResult(new AuthenticationException(e));
            }
        }




    }
}
