using Maok.App.Modules.Register.Models;
using Maok.App.Modules.Shared.Models;
using Maok.App.Utils;

namespace Maok.App.Modules.ResetPassword.Models
{
    public class ChangePasswordModel : BaseModel
    {
        public string ImageBackground { get; set; } = "degrade_fundo_1.png";
        public string Logo { get; set; } = "logo.png";
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }

        public UserModel User { get; set; }

        public new bool IsValid
        {
            get
            {
                ResetError();

                if (string.IsNullOrEmpty(Password))
                    Erros.AppendLine("Campo senha é obrigatório!");
                else
                {
                    if (!Util.ValidatePassword(Password))
                        Erros.AppendLine("Senha inválida!");

                    if (string.IsNullOrEmpty(PasswordConfirm))
                        Erros.AppendLine("Campo confirmar senha é obrigatório!");
                    else
                        if (PasswordConfirm != Password)
                        Erros.AppendLine("Senhas estão divergentes!");
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