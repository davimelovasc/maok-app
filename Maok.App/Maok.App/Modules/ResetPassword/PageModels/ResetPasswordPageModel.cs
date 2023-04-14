using Maok.App.Modules.ResetPassword.Models;
using Maok.App.Modules.Shared.PageModels;
using Maok.App.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Maok.App.Modules.ResetPassword.PageModels
{
    public class ResetPasswordPageModel : BasePageModel<ResetPasswordModel>
    {
        public ICommand GoToResetPasswordSendEmailCommand => new CommandWaitCustom(DoResetPasswordSendEmailAsync, this);

        public ResetPasswordPageModel()
        {
        }

        private async Task DoResetPasswordSendEmailAsync(TaskCompletionSource<bool> tsc)
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

                await Push<ResetPasswordSendEmailPageModel>(tsc: tsc);
            }
            catch (Exception ex)
            {
                await ShowAlertAsync("Erro ao recuperar sua senha!", null);
            }
        }
    }
}