using Maok.App.Modules.Shared.Models;

namespace Maok.App.Modules.Register.Models
{
    public class RegisterNameModel : BaseModel
    {
        public string ImageBackground { get; set; } = "degrade_fundo_1.png";
        public string Logo { get; set; } = "logo.png";
        public string Name { get; set; }
        public UserModel User { get; set; }

        public new bool IsValid
        {
            get
            {
                ResetError();

                if (string.IsNullOrEmpty(Name))
                    Erros.AppendLine("Campo nome é obrigatório");

                return Erros.Length <= 0;
            }
        }

        private void ResetError()
        {
            Erros.Clear();
        }
    }
}
