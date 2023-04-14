using Maok.App.Modules.Home.Models;
using Maok.App.Modules.Home.PageModels.Parameter;
using Maok.App.Modules.Home.Services;
using Maok.App.Modules.Shared.PageModels;
using Maok.App.Utils;
using Refit;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Maok.App.Modules.Home.PageModels
{
    public class EventInfoPageModel : BasePageModel<EventInfoModel>
    {
        public ICommand SendPresenceCommand => new CommandWaitCustom(DoSendPresenceAsync, this);
        public ICommand OkCommand => new CommandWaitCustom(DoOKAsync, this);
        public ICommand CancelEventCommand => new CommandWaitCustom<bool>(DoCancelEventAsync, this);

        public EventInfoPageModel()
        {
        }

        public override void Init(object initData)
        {
            base.Init(initData);

            if (initData is EventInfoParameter data)
            {
                Model.Id = data.Id;
            }

            InitDelayedAsync(LoadEventAsync);
        }

        private async Task LoadEventAsync()
        {
            try
            {
                var apiResponse = RestService.For<IHomeService>(Util.GetClient());
                var eventResponseDto = await apiResponse.GetEventById(Model.Id);

                Model.EventInformation = new Models.Object.EventInformationModel(eventResponseDto);
                Model.PresenceApproved = !Model.EventInformation.PresenceConfirmed ? true : false;
                if (Model.EventInformation.PresenceConfirmed)
                    Model.ButtonText = "CANCELAR PRESENÇA";
            }
            catch (Exception ex)
            {
                await ShowAlertAsync("Erro ao buscar eventos!", null);
            }
        }

        private async Task DoSendPresenceAsync(TaskCompletionSource<bool> tsc)
        {
            Model.Opacity = 0.2M;
            Model.Sent = true;
            tsc?.TrySetResult(true);
        }

        private async Task DoOKAsync(TaskCompletionSource<bool> tsc)
        {
            try
            {
                IsBusy = true;
                Model.Opacity = 1;
                Model.Sent = false;
                var apiResponse = RestService.For<IHomeService>(Util.GetClient());
                await apiResponse.SendPresence(Model.EventInformation.Id, true);
                Model.IsEnabled = false;
                Model.ButtonColor = "#AEA9B9";
            }
            catch (Exception ex)
            {
                await ShowAlertAsync("Erro ao buscar eventos!", null);
            }
            finally
            {
                IsBusy = false;
                tsc?.TrySetResult(true);
            }
        }

        private async Task DoCancelEventAsync(bool action, TaskCompletionSource<bool> tsc)
        {
            try
            {
                IsBusy = true;
                Model.Opacity = 1;
                Model.Sent = false;
                if (action)
                {
                    var apiResponse = RestService.For<IHomeService>(Util.GetClient());
                    await apiResponse.SendPresence(Model.EventInformation.Id, false);
                    Model.IsEnabled = false;
                    Model.ButtonColor = "#AEA9B9";
                }
            }
            catch (Exception ex)
            {
                await ShowAlertAsync("Erro ao buscar eventos!", null);
            }
            finally
            {
                IsBusy = false;
                tsc?.TrySetResult(true);
            }
        }

        protected override Task DoBackPageAsync(object data, TaskCompletionSource<bool> tsc)
        {
            if (!Model.IsEnabled)
                return base.DoBackPageAsync(true, tsc);
            else
                return base.DoBackPageAsync(false, tsc);
        }
    }
}