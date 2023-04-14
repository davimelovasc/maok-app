using Maok.App.Modules.Home.PageModels;
using Maok.App.Modules.Login.Models;
using Maok.App.Modules.Login.Services;
using Maok.App.Modules.Login.Services.Dtos.Request;
using Maok.App.Modules.Register.PageModels;
using Maok.App.Modules.ResetPassword.PageModels;
using Maok.App.Modules.Shared.PageModels;
using Maok.App.Providers;
using Maok.App.Utils;
using Refit;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Maok.App.Modules.Login.PageModels
{
    public class LoginPageModel : BasePageModel<LoginModel>
    {
        public ICommand GoToHomeCommand => new CommandWaitCustom(DoHomeAsync, this);
        public ICommand GoToCadastroCommand => new CommandWaitCustom(DoCadastroAsync, this);
        public ICommand GoToResetPassword => new CommandWaitCustom(DoResetPasswordAsync, this);
        public ICommand GoToLogin => new CommandWaitCustom(DoLoginAsync, this);

        public LoginPageModel()
        {
        }

        private async Task DoHomeAsync(TaskCompletionSource<bool> tsc)
        {
            try
            {
                IsBusy = true;
                Model.IsResetPassword = false;
                if (!Model.IsValid)
                {
                    await ShowAlertAsync(Model.Erros.ToString(), tsc);
                    return;
                }

                LoginRequestDto loginRequest = new LoginRequestDto()
                {
                    Username = Model.Username,
                    Password = Model.Password
                };

                var apiResponse = RestService.For<ILoginService>("https://maok-api.herokuapp.com");
                var token = await apiResponse.Auth(loginRequest);

                AuthProvider.Instance.SetToken(new HttpTokenModel(token));

                await Push<HomePageModel>(tsc: tsc);
            }
            catch (Exception ex)
            {
                await ShowAlertAsync("Username ou senha errado!", tsc);
            }
            finally
            {
                IsBusy = false;
                tsc?.TrySetResult(true);
            }
        }

        private Task DoResetPasswordAsync(TaskCompletionSource<bool> tsc) => Push<ResetPasswordPageModel>(tsc: tsc);

        private Task DoCadastroAsync(TaskCompletionSource<bool> tsc) => Push<RegisterApresentationPageModel>(tsc: tsc);

        private Task DoLoginAsync(TaskCompletionSource<bool> tsc) => Push<LoginPageModel>(tsc: tsc);
    }
}