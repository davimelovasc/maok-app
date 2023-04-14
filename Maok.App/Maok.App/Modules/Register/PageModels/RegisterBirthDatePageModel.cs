using Maok.App.Modules.Register.Models;
using Maok.App.Modules.Register.PageModels.Parameter;
using Maok.App.Modules.Shared.PageModels;
using Maok.App.Utils;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Maok.App.Modules.Register.PageModels
{
    public class RegisterBirthDatePageModel : BasePageModel<RegisterBirthDateModel>
    {
        public ICommand GoToDocumentCommand => new CommandWaitCustom(DoDocumentAsync, this);

        public RegisterBirthDatePageModel()
        {
        }

        public override void Init(object initData)
        {
            base.Init(initData);
            if (initData is RegisterUserParameter parameter)
                Model.User = parameter.User;
        }

        private async Task DoDocumentAsync(TaskCompletionSource<bool> tsc)
        {
            if (!Model.IsValid)
            {
                await ShowAlertAsync(Model.Erros.ToString(), tsc);
                return;
            }

            Model.User.BirthDate = Model.DateBirth.GetValueOrDefault().ToString("yyyy-MM-dd");
            await Push<RegisterDocumentPageModel>(new RegisterUserParameter(Model.User), tsc: tsc);
        }
    }
}