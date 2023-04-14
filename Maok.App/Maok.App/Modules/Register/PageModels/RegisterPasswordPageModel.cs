using Maok.App.Modules.Register.Models;
using Maok.App.Modules.Register.PageModels.Parameter;
using Maok.App.Modules.Shared.PageModels;
using Maok.App.Utils;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Maok.App.Modules.Register.PageModels
{
    public class RegisterPasswordPageModel : BasePageModel<RegisterPasswordModel>
    {
        public ICommand GoToTermsCommand => new CommandWaitCustom(DoTermsAsync, this);

        public RegisterPasswordPageModel()
        {
        }

        public override void Init(object initData)
        {
            base.Init(initData);
            if (initData is RegisterUserParameter parameter)
                Model.User = parameter.User;
        }

        private async Task DoTermsAsync(TaskCompletionSource<bool> tsc)
        {
            if (!Model.IsValid)
            {
                await ShowAlertAsync(Model.Erros.ToString(), tsc);
                return;
            }

            Model.User.Password = Model.Password;

            await Push<RegisterTermsPageModel>(new RegisterUserParameter(Model.User), tsc: tsc);
        }
    }
}