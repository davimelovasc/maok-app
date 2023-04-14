using Maok.App.Modules.Shared.Models;

namespace Maok.App.Modules.ResetPassword.Models
{
    public class ResetPasswordSendEmailModel : BaseModel
    {
        public string ImageBackground { get; set; } = "degrade_fundo_1.png";
        public string Logo { get; set; } = "logo.png";
        public string LogoMao { get; set; } = "logo_mao.png";
        public string ButtonColor { get; set; } = "Transparent";
        public string Code { get; set; }
        public bool IsEnabled { get; set; } = false;

        public new bool IsValid
        {
            get
            {
                ResetError();

                if (string.IsNullOrEmpty(Code))
                    Erros.AppendLine("Campo código obrigatório");

                return Erros.Length <= 0;
            }
        }

        private void ResetError()
        {
            Erros.Clear();
        }
    }
}