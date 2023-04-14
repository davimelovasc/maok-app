using Maok.App.Modules.Login.PageModels;
using Maok.App.Modules.Register.Models;
using Maok.App.Modules.Register.PageModels.Parameter;
using Maok.App.Modules.Register.Services;
using Maok.App.Modules.Register.Services.Dtos.Request;
using Maok.App.Modules.Shared.PageModels;
using Maok.App.Utils;
using Refit;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Maok.App.Modules.Register.PageModels
{
    public class RegisterTermsPageModel : BasePageModel<RegisterTermsModel>
    {
        public ICommand GoToNameCommand => new CommandWaitCustom(DoNameAsync, this);

        public RegisterTermsPageModel()
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
            try
            {
                if (!Model.IsValid)
                {
                    await ShowAlertAsync(Model.Erros.ToString(), tsc);
                    return;
                }

                Model.User.ExternalId = 10;
                UserRequestDto userRequest = new UserRequestDto(Model.User);

                var apiResponse = RestService.For<ICadastroService>("https://maok-api.herokuapp.com");
                var message = await apiResponse.RegisterUser(userRequest);
                await Push<LoginPageModel>(tsc: tsc);
            }
            catch (Exception ex)
            {
                await ShowAlertAsync("Ocorreu um erro ao efetuar o cadastro!", tsc);
                tsc?.TrySetResult(true);
            }
            finally
            {
                IsBusy = false;
                tsc?.TrySetResult(true);
            }
        }
    }
}