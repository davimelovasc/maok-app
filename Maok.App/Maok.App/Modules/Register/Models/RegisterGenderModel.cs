using Maok.App.Modules.Shared.Models;
using Maok.App.Utils;
using System.Collections.Generic;

namespace Maok.App.Modules.Register.Models
{
    public class RegisterGenderModel : BaseModel
    {
        public string ImageBackground { get; set; } = "degrade_fundo_1.png";
        public string Logo { get; set; } = "logo.png";

        public UserModel User { get; set; }
        public List<ComboBoxModel> Genders { get; set; }

        public RegisterGenderModel()
        {
            Genders = Util.GetGenders();
        }

        public ComboBoxModel GenderSelected { get; set; }

        public new bool IsValid
        {
            get
            {
                ResetError();

                if (GenderSelected == null)
                    Erros.AppendLine("Campo gênero obrigatório");
                else
                    if (string.IsNullOrEmpty(GenderSelected.Value))
                    Erros.AppendLine("Campo gênero obrigatório");

                return Erros.Length <= 0;
            }
        }

        private void ResetError()
        {
            Erros.Clear();
        }
    }
}