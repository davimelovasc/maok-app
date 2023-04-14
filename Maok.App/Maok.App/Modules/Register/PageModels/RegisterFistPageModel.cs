using Maok.App.Modules.Register.Models;
using Maok.App.Modules.Register.PageModels.Parameter;
using Maok.App.Modules.Shared.PageModels;
using Maok.App.Utils;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Maok.App.Modules.Register.PageModels
{
    public class RegisterFistPageModel : BasePageModel<RegisterFistModel>
    {
        public ICommand GoToNameCommand => new CommandWaitCustom(DoNameAsync, this);

        public RegisterFistPageModel()
        {
        }

        public override void Init(object initData)
        {
            base.Init(initData);
            if (initData is RegisterUserParameter parameter)
                Model.User = parameter.User;
        }

        private async Task DoNameAsync(TaskCompletionSource<bool> tsc)
        {
            if (!Model.IsValid)
            {
                await ShowAlertAsync(Model.Erros.ToString(), tsc);
                return;
            }

            await Push<RegisterTermsPageModel>(new RegisterUserParameter(Model.User), tsc: tsc);
        }
    }
}