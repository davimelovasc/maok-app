using Maok.App.Modules.Register.Models;
using Maok.App.Modules.Register.PageModels.Parameter;
using Maok.App.Modules.Register.Services;
using Maok.App.Modules.Shared.PageModels;
using Maok.App.Utils;
using Refit;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Maok.App.Modules.Register.PageModels
{
    public class RegisterEmailPageModel : BasePageModel<RegisterEmailModel>
    {
        public ICommand GoToNameCommand => new CommandWaitCustom(DoNameAsync, this);

        public RegisterEmailPageModel()
        {
        }

        private async Task DoNameAsync(TaskCompletionSource<bool> tsc)
        {
            try
            {
                IsBusy = true;
                if (!Model.IsValid)
                {
                    await ShowAlertAsync(Model.Erros.ToString(), tsc);
                    return;
                }

                var apiResponse = RestService.For<ICadastroService>("https://maok-api.herokuapp.com");
                var checkEmail = await apiResponse.CheckUsername(Model.Email);

                if (!checkEmail.IsSuccessStatusCode)
                {
                    if (checkEmail.StatusCode == System.Net.HttpStatusCode.Conflict)
                    {
                        await ShowAlertAsync("Email já cadastrado!", tsc);
                        return;
                    }
                    else
                    {
                        await ShowAlertAsync("Ocorreu um erro ao efetuar a consulta!", tsc);
                        return;
                    }
                }

                UserModel user = new UserModel();
                user.Username = Model.Email;

                await Push<RegisterNamePageModel>(new RegisterUserParameter(user), tsc: tsc);
            }
            catch (Exception ex)
            {
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