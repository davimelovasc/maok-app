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
    public class RegisterDocumentPageModel : BasePageModel<RegisterDocumentModel>
    {
        public ICommand GoToPasswordCommand => new CommandWaitCustom(DoPasswordAsync, this);

        public RegisterDocumentPageModel()
        {
        }

        public override void Init(object initData)
        {
            base.Init(initData);
            if (initData is RegisterUserParameter parameter)
                Model.User = parameter.User;
        }

        private async Task DoPasswordAsync(TaskCompletionSource<bool> tsc)
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
                var checkDocument = await apiResponse.CheckCpf(Model.Document);

                if (!checkDocument.IsSuccessStatusCode)
                {
                    if (checkDocument.StatusCode == System.Net.HttpStatusCode.Conflict)
                    {
                        await ShowAlertAsync("Cpf já cadastrado!", tsc);
                        return;
                    }
                    else
                    {
                        await ShowAlertAsync("Ocorreu um erro ao efetuar a consulta1", tsc);
                        return;
                    }
                }

                Model.User.Cpf = Model.Document;

                await Push<RegisterPasswordPageModel>(new RegisterUserParameter(Model.User), tsc: tsc);
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