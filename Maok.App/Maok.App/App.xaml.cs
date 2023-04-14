using Maok.App.Modules.Login.PageModels;
using Maok.App.Modules.Shared.Navigation;
using Maok.App.Utils;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static FreshMvvm.FreshPageModelResolver;


[assembly: ExportFont("Montserrat-ExtraBold.ttf")]
[assembly: ExportFont("Montserrat-Regular.ttf")]
[assembly: ExportFont("Montserrat-Bold.ttf")]
namespace Maok.App
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var page = ResolvePageModel<LoginPageModel>();
            Current.MainPage = new NavigationController(page);
        }

        public static void Logout(LogoutReason logoutReason)
        {
            //if (!AuthProvider.Instance.HasToken)
            //{
            //    return;
            //}

            //try { FreshMvvm.FreshIOC.Container.Resolve<IAuthService>().LogoutAsync(); } catch { }

            //AuthProvider.Instance.ClearToken();

            //Device.BeginInvokeOnMainThread(async () =>
            //{
            //    try
            //    {
            //        FallbackToLoginPage();
            //        await PageMethods.InvokeOnCurrentPageModel<LoginPageModel>(loginPageModel => loginPageModel?.RefreshAccount());

            //        switch (logoutReason)
            //        {
            //            case LogoutReason.Unauthorized:
            //                await PageMethods.ShowAlertAsync("Sessão expirada por inatividade ou finalizada por outro dispositivo."); break;
            //            case LogoutReason.SessionTimeout:
            //                await PageMethods.ShowAlertAsync("Sessão expirada por inatividade."); break;
            //            case LogoutReason.RefreshTokenInvalid:
            //                await PageMethods.ShowAlertAsync("Sessão expirada, por favor logue novamente para continuar."); break;
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        AsyncErrorHandler.HandleException(ex);
            //        FallbackToLoginPage();
            //    }
            //});
        }


        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
