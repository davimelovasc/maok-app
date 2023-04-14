using Maok.App.Modules.ResetPassword.Models;
using Maok.App.Modules.Shared.PageModels;
using Maok.App.Utils;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Maok.App.Modules.ResetPassword.PageModels
{
    public class ResetPasswordSendEmailPageModel : BasePageModel<ResetPasswordSendEmailModel>
    {
        public ICommand GoToChangePasswordCommand => new CommandWaitCustom(DoChangePasswordAsync, this);
        public ICommand GoToSendEmailCommand => new CommandWaitCustom(DoSendEmailAsync, this);

        public ResetPasswordSendEmailPageModel()
        {
        }

        private async Task DoChangePasswordAsync(TaskCompletionSource<bool> tsc)
        {
            try
            {
                IsBusy = true;
                if (!Model.IsValid)
                {
                    await ShowAlertAsync(Model.Erros.ToString(), tsc);
                    return;
                }

                //var apiResponse = RestService.For<IHomeService>(Util.GetClient());
                //var eventResponseDto = await apiResponse.GetEvents();

                await Push<ChangePasswordPageModel>(tsc: tsc);
            }
            catch (Exception ex)
            {
                await ShowAlertAsync("Erro ao recuperar sua senha!", null);
            }
            finally
            {
                IsBusy = false;
                tsc?.TrySetResult(true);
            }
        }

        private async Task DoSendEmailAsync(TaskCompletionSource<bool> tsc)
        {
            try
            {
                IsBusy = true;
                //var apiResponse = RestService.For<IHomeService>(Util.GetClient());
                //var eventResponseDto = await apiResponse.GetEvents();

                Model.IsEnabled = false;
                Model.ButtonColor = "#AEA9B9";
            }
            catch (Exception ex)
            {
                await ShowAlertAsync("Erro ao enviar email!", null);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}