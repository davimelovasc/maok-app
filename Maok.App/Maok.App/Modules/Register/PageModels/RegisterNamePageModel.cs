using Maok.App.Modules.Register.Models;
using Maok.App.Modules.Register.PageModels.Parameter;
using Maok.App.Modules.Shared.PageModels;
using Maok.App.Utils;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Maok.App.Modules.Register.PageModels
{
    public class RegisterNamePageModel : BasePageModel<RegisterNameModel>
    {
        public ICommand GoToGenderCommand => new CommandWaitCustom(DoGenderAsync, this);

        public RegisterNamePageModel()
        {
        }

        public override void Init(object initData)
        {
            base.Init(initData);
            if (initData is RegisterUserParameter parameter)
                Model.User = parameter.User;
        }

        private async Task DoGenderAsync(TaskCompletionSource<bool> tsc)
        {
            if (!Model.IsValid)
            {
                await ShowAlertAsync(Model.Erros.ToString(), tsc);
                return;
            }

            Model.User.Name = Model.Name;

            await Push<RegisterGenderPageModel>(new RegisterUserParameter(Model.User), tsc: tsc);
        }
    }
}