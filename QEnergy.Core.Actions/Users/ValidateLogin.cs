using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using QEnergy.Core.Actions.Base;
using QEnergy.Core.Domain.Entities;
using QEnergy.Core.Domain.Models.Users;
using QEnergy.Services.Persistence.EntityFramework;

namespace QEnergy.Core.Actions.Users
{
    public class ValidateLogin : RequestBase<UserModel>
    {
        public ValidateLogin(string username, string password)
            : base(default, default)
        {
            Username = username;
            Password = password;
        }
        public string Username { get; }
        public string Password { get; }
    }

    public class ValidateLoginHandler : RequestHandlerBase<ValidateLogin, UserModel>
    {
        public ValidateLoginHandler(QEnergyDbContext context, ILogger<ValidateLoginHandler> logger) : base(logger, context: context)
        {
        }

        protected override async Task<UserModel> DoHandle(ValidateLogin message)
        {
            UserModel result = null;

            if (!string.IsNullOrWhiteSpace(message.Username) && !string.IsNullOrWhiteSpace(message.Password))
            {
                var user = await context.Set<User>()
                    .Where(x => x.Username == message.Username && x.PasswordHash == message.Password)
                    .Select(x => new UserModel()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Surname = x.Surname,
                        Username = x.Username,
                        PasswordHash = x.PasswordHash,
                        Email = x.Email,
                        CompanyId = x.CompanyId
                    })
                    .FirstOrDefaultAsync();

                if (user != null)
                {
                    var validPassword = user.PasswordHash == message.Password;
                    if (validPassword)
                        result = user;
                    logger.LogWarning(string.Format("ValidateLoginHandler: Incorrect password for User: {0}.", message.Username));
                }
                else
                    logger.LogWarning(string.Format("ValidateLoginHandler: User: {0} not found.", message.Username));
            }
            else
                logger.LogWarning(string.Format("ValidateLoginHandler: No username or password"));

            return result;
        }
    }
}
