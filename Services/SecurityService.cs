using CST350Milestone.Models;

namespace CST350Milestone.Services
{
    public class SecurityService
    {
        SecurityDAO securityDAO = new SecurityDAO();

        public bool login(UserModel user)
        {
            return securityDAO.FindUserByUsernameAndPassword(user);
        }

        public bool registration(UserModel user)
        {
            return securityDAO.InsertNewUser(user);
        }

        public UserModel setUserProperties(UserModel user)
        {
            return securityDAO.ReturnUser(user);
        }
    }
}
