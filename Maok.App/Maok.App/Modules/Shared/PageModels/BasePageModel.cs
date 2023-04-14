using FreshMvvm;
using Maok.App.Modules.Shared.Enums;
using Maok.App.Modules.Shared.Navigation;
using Maok.App.Modules.Shared.Pages;
using Maok.App.Modules.Shared.Services.Dtos.Base;
using Maok.App.Utils;
using Maok.App.Utils.Constants;
using Maok.App.Utils.Handlers;
using Newtonsoft.Json;
using PropertyChanged;
using Refit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Maok.App.Modules.Shared.PageModels
{
    [Preserve(AllMembers = true)]
    [AddINotifyPropertyChangedInterface]
    public class BasePageModel : FreshBasePageModel, INotifyPropertyChanged
    {
        public BasePage MyBasePage => CurrentPage as BasePage;
        public string Title { get; set; }
        public bool IsBusy { get; set; }
        private string _error;
        private string ClassName => GetType().Name;
        private string LoadingTimerName => "Loading " + ClassName;

        public virtual ICommand GoToBackPageCommand => new CommandWaitCustom(DoBackPageAsync, this);
        public virtual ICommand ActionBarCommand => new CommandWaitCustom(DoActionBarAsync, this);
        //public virtual ICommand GoToDashboardCommand => new CommandWaitCustom(DoDashboardAsync, this);
        //public virtual ICommand GoToExternalLinkCommand => new CommandWaitCustom<string>(DoExternalLink, this);

        protected PageMethods PageMethods { get; }

        public BasePageModel()
        {
            PageMethods = new PageMethods(this);
        }

        public override void Init(object initData)
        {
            base.Init(initData);
            //FreshIOC.Container.Resolve<IAnalyticsService>().SetCurrentScreen(ClassName.Replace("PageModel", ""), ClassName);
        }

        public override void ReverseInit(object returnedData)
        {
            base.ReverseInit(returnedData);
        }

        protected async Task InitDelayedAsync(params Func<Task>[] asyncFuncs)
        {
            try
            {
                IsBusy = true;

                await Task.Delay(SharedConstant.InitDelayTime);

                var tasks = asyncFuncs.Select(af => af());

                await Task.WhenAll(tasks);

                await AfterInit();
            }
            finally
            {
                IsBusy = false;

                await Task.Delay(1);
            }
        }

        protected async Task InitAsync(params Func<Task>[] asyncFuncs)
        {
            IsBusy = true;

            var tasks = asyncFuncs.Select(af => af());

            await Task.WhenAll(tasks).ContinueWith(t =>
            {
                Device.BeginInvokeOnMainThread(() => IsBusy = false);
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T backfield, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(backfield, value))
            {
                return false;
            }

            backfield = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        protected async Task LoadAsync(params Func<Task>[] asyncFuncs)
        {
            var tasks = asyncFuncs.Select(af => af());

            await Task.WhenAll(tasks);
        }

        protected virtual async Task AfterInit()
        {
        }

        protected virtual Task DoBackPageAsync(object data, TaskCompletionSource<bool> tsc)
        {
            return PageMethods.PopPageModel(data, CurrentPage.GetType().Name.Contains("Modal")).ContinueWith(x =>
            {
                if (x.IsCompleted || x.IsCanceled || x.IsFaulted)
                    tsc?.TrySetResult(true);
            });
        }

        private Task DoActionBarAsync(TaskCompletionSource<bool> tsc)
        {
            return Push<BasePageModel>(tsc: tsc);
        }

        //protected async Task DoDashboardAsync(TaskCompletionSource<bool> tsc)
        //{
        //    await DoDashboardAsync(tsc: tsc, null);
        //}

        //public async Task DoExternalLink(string url, TaskCompletionSource<bool> tsc)
        //{
        //    if (await this.ShowConfirmAsync("Link Externo", "Você será redirecionado para um link externo, que será acessado fora do aplicativo.", "Abrir"))
        //    {
        //        await Browser.OpenAsync(url, BrowserLaunchMode.External);
        //    }
        //    tsc?.TrySetResult(true);
        //}

        //protected async Task DoDashboardAsync(TaskCompletionSource<bool> tsc, DashboardParameter parameter = null)
        //{
        //    const string dash = NavigationRoute.RouteName.Dashboard;
        //    FreshIOC.Container.Unregister<IFreshNavigationService>(dash);
        //    await Task.Delay(100);
        //    await Push<DashboardPageModel>(parameter, newNavigationRouteName: dash, tsc: tsc);
        //}

        //public void ShowToast(string message) => MessagingCenter.Send(new ToastEvent(message), ToastEvent.EVENT);

        public Task ShowAlertAsync(string description, TaskCompletionSource<bool> tsc = null)
        {
            return MainThread.InvokeOnMainThreadAsync(() => MyBasePage.ShowAlertAsync(description.TrimEnd('\n').Trim(), null, AlertType.Error).ContinueWith(x =>
            {
                if (x.IsCompleted || x.IsCanceled || x.IsFaulted)
                    tsc?.TrySetResult(true);
            }));
        }

        public Task ShowAlertErrorApiAsync(TaskCompletionSource<bool> tsc = null) => ShowAlertAsync(GetErrorApi(), tsc);

        public void Pop(bool modal = false) => PageMethods.PopPageModel(modal, true);

#if DEBUG

        public async Task Push<TPageModel>(object data = null, bool modal = false, string newNavigationRouteName = "", TaskCompletionSource<bool> tsc = null, bool animation = true) where TPageModel : FreshBasePageModel
        {
#else

        public Task Push<TPageModel>(object data = null, bool modal = false, string newNavigationRouteName = "", TaskCompletionSource<bool> tsc = null, bool animation = true) where TPageModel : FreshBasePageModel
        {
#endif
            try
            {
                if (!string.IsNullOrEmpty(newNavigationRouteName))
                {
                    var page = FreshPageModelResolver.ResolvePageModel<TPageModel>(data);
                    var navigationController = new NavigationController(page, newNavigationRouteName);
#if DEBUG
                    await CoreMethods.PushNewNavigationServiceModal(navigationController, page.GetModel(), animation);
                    tsc?.TrySetResult(true);
                    return;

#else
                    return CoreMethods.PushNewNavigationServiceModal(navigationController, page.GetModel(), animate: animation).ContinueWith(x =>
                    {
                        if (x.IsCompleted || x.IsCanceled || x.IsCanceled)
                            tsc?.TrySetResult(true);
                    }); ;
#endif
                }

#if DEBUG
                await CoreMethods.PushPageModel<TPageModel>(data, modal, animation);
                tsc?.TrySetResult(true);

#else
                return CoreMethods.PushPageModel<TPageModel>(data, modal, animation).ContinueWith(x =>
                {
                    if (x.IsCompleted)
                        tsc?.TrySetResult(true);
                    else if (x.IsFaulted || x.IsCanceled)
                    {
                        AsyncErrorHandler.HandleException(x.Exception);
                        tsc?.TrySetResult(true);
                    }
                });
#endif
            }
            catch (Exception e)
            {
                AsyncErrorHandler.HandleException(e);
#if !DEBUG
                return Task.CompletedTask;
#endif
            }
        }

        public bool HasErrorApi => !string.IsNullOrEmpty(_error);

        public void SetErrorApi(ApiException exception)
        {
            AsyncErrorHandler.HandleException(exception);

            _error = "Não foi possível efetuar a consulta, tente novamente mais tarde.";

            if (exception.StatusCode == HttpStatusCode.GatewayTimeout ||
                exception.StatusCode == HttpStatusCode.BadGateway)
            {
                _error = "Verifique sua conexão com a internet e tente novamente";
                return;
            }

            if (string.IsNullOrEmpty(exception.Content) || exception.Content.StartsWith("<"))
            {
                return;
            }

            try
            {
                var error = JsonConvert.DeserializeObject<ErrorResponseDto>(exception.Content);

                var formattedError = error?.ToString();

                if (!String.IsNullOrEmpty(formattedError))
                    _error = formattedError;

                if (_error.ToLower().Contains("http"))
                {
                    GetErrorApi();
                    App.Logout(LogoutReason.ServerError);
                }
            }
            catch (Exception ex)
            {
                AsyncErrorHandler.HandleException(ex);
            }
        }

        public string GetErrorApi()
        {
            var error = _error;
            _error = string.Empty;
            return error;
        }

        protected async Task<bool> TryApiCallBlockAsync(Func<Task> func, TaskCompletionSource<bool> tsc = null)
        {
            try
            {
                IsBusy = true;
                await func();
                tsc?.TrySetResult(true);
                return true;
            }
            catch (ApiException e)
            {
                SetErrorApi(e);
                return false;
            }
            finally
            {
                IsBusy = false;

                if (HasErrorApi)
                {
                    //await ShowAlertErrorApiAsync(tsc);
                }
            }
        }

        //public Task OpenHelpModalAsync(string title, FormattedString description, TaskCompletionSource<bool> tsc = null)
        //{
        //    return OpenHelpModalAsync(title, description, Util.GetResource<Color>("WhiteColor"),
        //        Util.GetResource<Color>("VitreoColor"), tsc);
        //}

        //public Task OpenHelpModalAsync(string title, FormattedString description, Color backgroundColor, Color textColor, TaskCompletionSource<bool> tsc = null)
        //{
        //    return Push<HelpModalPageModel>(new HelpModalParameter(title, description, backgroundColor, textColor), true, tsc: tsc);
        //}
    }

    [Preserve(AllMembers = true)]
    [AddINotifyPropertyChangedInterface]
    public class BasePageModel<T> : BasePageModel
    {
        public T Model { get; set; } = Activator.CreateInstance<T>();
    }
}