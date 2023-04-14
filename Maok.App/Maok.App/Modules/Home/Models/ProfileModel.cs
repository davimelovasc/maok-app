using Maok.App.Modules.Shared.Models;
using Maok.App.Utils;
using System;
using System.Collections.Generic;

namespace Maok.App.Modules.Home.Models
{
    public class ProfileModel : BaseModel
    {
        public DateTime? DateBirth { get; set; } = DateTime.Now;
        public List<ComboBoxModel> Genders { get; set; }

        public ProfileModel()
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

                if (DateBirth == null)
                    Erros.AppendLine("Campo data de nascimento é obrigatório!");
                else
                {
                    DateTime Today = DateTime.Today;
                    int age = Today.Year - DateBirth.GetValueOrDefault().Year;
                    if (age < 18)
                        Erros.AppendLine("Você precisa ter mais de 18 anos!");
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