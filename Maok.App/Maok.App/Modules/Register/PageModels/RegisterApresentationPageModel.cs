using Maok.App.Modules.Shared.Models;
using Maok.App.Modules.Shared.PageModels;
using Maok.App.Utils;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Maok.App.Modules.Register.PageModels
{
    public class RegisterApresentationPageModel : BasePageModel<BaseModel>
    {
        public string ImageBackground { get; set; } = "degrade_fundo_1.png";
        public string Logo { get; set; } = "logo.png";
        public string BotaoVoltar { get; set; } = "botao_voltar.png";

        public ICommand GoToEmailCommand => new CommandWaitCustom(DoEmailAsync, this);

        public RegisterApresentationPageModel()
        {
        }

        private Task DoEmailAsync(TaskCompletionSource<bool> tsc) => Push<RegisterEmailPageModel>(tsc: tsc);
    }
}