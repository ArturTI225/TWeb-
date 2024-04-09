using System.Linq;
using ArturTI225.BusinessLogic.Helpers;
using ArturTI225.Domain.Entities.Auth;
using ArturTI225.Domain.Enums;
using ArturTI225.Infrastructure;

namespace ArturTI225.BusinessLogic.Managers
{
    public class AuthenticationManager
    {
        private CarsShopContext db = new CarsShopContext();

        public User Authenticate(string username, string password)
        {
            var user = db.Users.FirstOrDefault(u => u.Username == username);
            if (user != null && PasswordHelper.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                return user;
            }
            return null;
        }
        
        public bool IsAuthorized(User user, string requiredRole)
        {
            return user.Role == requiredRole;
        }

        public User Register(string username, string password)
        {
            byte[] passwordHash, passwordSalt;
            PasswordHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);

            var user = new User
            {
                Username = username, 
                PasswordHash = passwordHash, 
                PasswordSalt = passwordSalt, 
                Role = Roles.User.ToString()
            };
            
            db.Users.Add(user);
            db.SaveChanges();
            
            return user;
        }
    }
}