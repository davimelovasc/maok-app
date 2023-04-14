using Maok.App.Modules.Shared.Models;

namespace Maok.App.Modules.Register.Models
{
    public class RegisterFistModel : BaseModel
    {
        public string ImageBackground { get; set; } = "degrade_fundo_1.png";
        public string Logo { get; set; } = "logo.png";

        public UserModel User { get; set; }
    }
}