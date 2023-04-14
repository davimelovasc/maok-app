using Maok.App.Modules.Shared.Models;

namespace Maok.App.Modules.Register.Models
{
    public class RegisterDocumentModel : BaseModel
    {
        public string ImageBackground { get; set; } = "degrade_fundo_1.png";
        public string Logo { get; set; } = "logo.png";
        public string Document { get; set; }

        public UserModel User { get; set; }

        public RegisterDocumentModel()
        {
        }

        public new bool IsValid
        {
            get
            {
                ResetError();

                if (string.IsNullOrEmpty(Document))
                    Erros.AppendLine("Campo documento é obrigatório");

                return Erros.Length <= 0;
            }
        }

        private void ResetError()
        {
            Erros.Clear();
        }
    }
}