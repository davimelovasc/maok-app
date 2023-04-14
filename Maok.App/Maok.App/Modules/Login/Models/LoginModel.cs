using Maok.App.Modules.Shared.Models;

namespace Maok.App.Modules.Login.Models
{
    public class LoginModel : BaseModel
    {
        public string ImageBackground { get; set; } = "degrade_fundo_1.png";
        public string Logo { get; set; } = "logo.png";
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsResetPassword { get; set; }

        public LoginModel()
        {
        }

        public new bool IsValid
        {
            get
            {
                ResetError();

                if (IsResetPassword)
                {
                    if (string.IsNullOrEmpty(Username))
                        Erros.AppendLine("Campo email é obrigatório");
                }
                else
                {
                    if (string.IsNullOrEmpty(Username))
                        Erros.AppendLine("Campo email é obrigatório");

                    if (string.IsNullOrEmpty(Password))
                        Erros.AppendLine("Campo senha é obrigatório");
                }

                return Erros.Length <= 0;
            }
        }

        private void ResetError()
        {
            Erros.Clear();
        }
    }
}