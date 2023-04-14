using Maok.App.Modules.Register.Models;

namespace Maok.App.Modules.Register.PageModels.Parameter
{
    public class RegisterUserParameter
    {
        public UserModel User { get; }

        public RegisterUserParameter(UserModel user)
        {
            User = user;
        }
    }
}
