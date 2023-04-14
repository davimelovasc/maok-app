using Maok.App.Modules.Login.PageModels;
using Maok.App.Modules.ResetPassword.Models;
using Maok.App.Modules.Shared.PageModels;
using Maok.App.Utils;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Maok.App.Modules.ResetPassword.PageModels
{
    public class ChangePasswordPageModel : BasePageModel<ChangePasswordModel>
    {
        public ICommand GoToSendPasswordCommand => new CommandWaitCustom(DoSendPasswordAsync, this);

        public ChangePasswordPageModel()
        {
        }

        private async Task DoSendPasswordAsync(TaskCompletionSource<bool> tsc)
        {
            try
            {
                if (!Model.IsValid)
                {
                    await ShowAlertAsync(Model.Erros.ToString(), tsc);
                    return;
                }

                //var apiResponse = RestService.For<IHomeService>(Util.GetClient());
                //var eventResponseDto = await apiResponse.GetEvents();

                await Push<LoginPageModel>(tsc: tsc);
            }
            catch (Exception ex)
            {
                await ShowAlertAsync("Erro ao recuperar sua senha!", null);
            }
        }
    }
}