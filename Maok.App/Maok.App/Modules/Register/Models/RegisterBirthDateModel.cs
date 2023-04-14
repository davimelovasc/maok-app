using Maok.App.Modules.Shared.Models;
using System;

namespace Maok.App.Modules.Register.Models
{
    public class RegisterBirthDateModel : BaseModel
    {
        public string ImageBackground { get; set; } = "degrade_fundo_1.png";
        public string Logo { get; set; } = "logo.png";
        public DateTime? DateBirth { get; set; } = DateTime.Now;

        public UserModel User { get; set; }

        public new bool IsValid
        {
            get
            {
                ResetError();

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