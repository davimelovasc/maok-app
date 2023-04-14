using Maok.App.Modules.Shared.Models;
using Maok.App.Utils;

namespace Maok.App.Modules.Register.Models
{
    public class RegisterEmailModel : BaseModel
    {
        public string ImageBackground { get; set; } = "degrade_fundo_1.png";
        public string Logo { get; set; } = "logo.png";
        public string Email { get; set; }

        public new bool IsValid
        {
            get
            {
                ResetError();

                if (string.IsNullOrEmpty(Email))
                    Erros.AppendLine("Campo email obrigatório");
                else
                {
                    if (!Email.IsEmail())
                        Erros.AppendLine("E-mail inválido");
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