namespace CRMSystem.Modules.Auth.Infrastructure.Security
{
    public interface IRandomCodeService
    {
        public int gettingARandomNumber();
    }
    public class RandomCodeService : IRandomCodeService
    {
        public int gettingARandomNumber()
        {
            Random random = new Random();
            return random.Next(100000, 999999);
        }
    }
}
