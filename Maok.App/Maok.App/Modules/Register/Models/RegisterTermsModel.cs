using Maok.App.Modules.Shared.Models;

namespace Maok.App.Modules.Register.Models
{
    public class RegisterTermsModel : BaseModel
    {
        public string ImageBackground { get; set; } = "degrade_fundo_1.png";
        public string Logo { get; set; } = "logo.png";
        public bool IsCheckTerms { get; set; } = false;
        public bool IsCheckPermission { get; set; } = false;

        public UserModel User { get; set; }

        public RegisterTermsModel()
        {
        }

        public new bool IsValid
        {
            get
            {
                ResetError();

                if (!IsCheckTerms || !IsCheckPermission)
                    Erros.AppendLine("Necessário assinar os termos!");

                return Erros.Length <= 0;
            }
        }

        private void ResetError()
        {
            Erros.Clear();
        }
    }
}