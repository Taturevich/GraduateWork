using System;
using System.Linq;
using BusinessLogic.Entities.Infrastructure;
using BusinessLogic.Enums;
using BusinessLogic.Infrastructure.DAL;
using BusinessLogic.Infrastructure.Extensions;
using BusinessLogic.Infrastructure.Injection;

namespace BusinessLogic.Services
{
    public interface IUserService : IEntityServiceBase<User>
    {
        RegisterResult RegisterUser(string login, string password);

        bool CheckUser(string login, string password);
    }

    [BotEventLog]
    internal class UserService : EntityServiceBase<User>, IUserService
    {
        private readonly IRepository<Password> _passwordRepository;
        public UserService(
            IRepository<User> repository,
            IRepository<Password> passwordRepository)
            : base(repository)
        {
            _passwordRepository = passwordRepository;
        }

        [Transactional]
        public RegisterResult RegisterUser(string login, string password)
        {
            var user = Repository.GetAll().FirstOrDefault(x => x.Name == login);
            if (user != null)
            {
                return RegisterResult.UserExist;
            }

            var newUser = new User
            {
                Name = login,
                Role = Role.User,
            };

            var userPassword = new Password
            {
                Hash = PasswordHasher.HashPassword(password),
                User = newUser
            };

            try
            {
                Repository.Add(newUser);
                _passwordRepository.Add(userPassword);
            }
            catch (Exception)
            {
                return RegisterResult.Error;
            }

            return RegisterResult.Success;
        }

        public bool CheckUser(string login, string password)
        {
            throw new System.NotImplementedException();
        }
    }
}
