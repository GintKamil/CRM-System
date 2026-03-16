namespace CRMSystem.Modules.Auth.Infrastructure.Security
{
    public interface IPasswordHashingService
    {
        public string PasswordHashing(string password);
        public bool PasswordCheck(string passwordDataBase, string passwordGiven);
    }
    public class PasswordHashingService : IPasswordHashingService
    {
        public string PasswordHashing(string password)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
            return passwordHash;
        }

        public bool PasswordCheck(string passwordDataBase, string passwordGiven)
        {
            if (BCrypt.Net.BCrypt.Verify(passwordGiven, passwordDataBase))
                return true;
            return false;
        }
    }
}
