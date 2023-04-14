using FreshMvvm;
using Maok.App.Modules.Home.Pages;
using Maok.App.Modules.Login.Pages;
using Maok.App.Modules.Shared.Navigation;
using Maok.App.Modules.Shared.PageModels;
using Maok.App.Modules.Shared.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Maok.App.Utils
{
    public class PageMethods
    {
        private FreshBasePageModel _currentPageModel;

        public PageMethods(FreshBasePageModel pageModel)
        {
            _currentPageModel = pageModel;
        }

        public static BasePage GetCurrentPage(bool useModalStack = true)
        {
            var currentPage = Application.Current.MainPage;

            var lastPage = currentPage.Navigation.NavigationStack.LastOrDefault();

            if (useModalStack && currentPage.Navigation.ModalStack.Any())
                lastPage = currentPage.Navigation.ModalStack.LastOrDefault();

            if (lastPage is NavigationController navigationController)
                return navigationController.CurrentPage as BasePage;

            return lastPage as BasePage;
        }

        public static async Task InvokeOnCurrentPage(Action<BasePage> action)
        {
            var currentPage = GetCurrentPage();
            if (currentPage != null)
                await Device.InvokeOnMainThreadAsync(() => action(currentPage));
        }

        public static async Task InvokeOnCurrentPageModel<T>(Action<T> action, Action actionFallback = null)
            where T : BasePageModel
        {
            if (GetCurrentPage()?.BindingContext is T currentPageModel)
                await Device.InvokeOnMainThreadAsync(() => action(currentPageModel));
            else
                actionFallback();
        }

        //public static async Task ShowAlertAsync(string message) => await InvokeOnCurrentPage(page => page?.ShowAlertAsync(message, null, AlertType.Error));

        internal static async Task<T> PopTo<T>()
            where T : BasePage
        {
            var navigation = Application.Current.MainPage.Navigation;

            return await PopStackTo<T>(true)
                ?? await PopStackTo<T>(false);
        }

        private static async Task<T> PopStackTo<T>(bool isModal) where T : BasePage
        {
            var navigation = Application.Current.MainPage.Navigation;
            bool popped = false; // da efeito de pop apenas para a primeira pagina da stack
            IReadOnlyList<Page> stack;

            while ((stack = isModal ? navigation.ModalStack : navigation.NavigationStack).Any())
            {
                var lastPage = stack.LastOrDefault();
                if (lastPage is T || lastPage is LoginPage || lastPage is PresentationPage)
                    return lastPage as T;

                if (lastPage is NavigationController navigationController && navigationController.CurrentPage is T targetNavPage)
                    return targetNavPage;

                if (!popped)
                {
                    if (isModal)
                        await navigation.PopModalAsync(true);
                    else
                        await navigation.PopAsync(true);

                    popped = true;
                }
                else
                    navigation.RemovePage(lastPage);
            }

            return null;
        }

        public static async Task PopToRoot()
        {
            while (App.Current.MainPage.Navigation.ModalStack.Count > 0)
            {
                await App.Current.MainPage.Navigation.PopModalAsync(false);
            }

            await InvokeOnCurrentPageModel<BasePageModel>(pageModel => pageModel?.CoreMethods.PopToRoot(false));
        }

        /// <summary>
        /// Método criado para substituir o CoreMethods.PopPageModel, tudo igual ao fonte original
        /// porém utilizamos um delay no pop para esconder o estado vazio da tela durante a animação do pop
        /// </summary>
        /// <param name="modal"></param>
        /// <param name="animate"></param>
        /// <returns></returns>
        public async Task PopPageModel(bool modal = false, bool animate = true)
        {
            string navServiceName = _currentPageModel.CurrentNavigationServiceName;
            if (_currentPageModel.IsModalFirstChild)
            {
                await _currentPageModel.CoreMethods.PopModalNavigationService(animate);
            }
            else
            {
                IFreshNavigationService rootNavigation = FreshIOC.Container.Resolve<IFreshNavigationService>(navServiceName);
                await rootNavigation.PopPage(modal, animate);

                await Task.Delay(500);

                if (modal)
                    this._currentPageModel.RaisePageWasPopped();
            }
        }

        /// <summary>
        /// Método criado para substituir o CoreMethods.PopPageModel, tudo igual ao fonte original
        /// </summary>
        /// <param name="data"></param>
        /// <param name="modal"></param>
        /// <param name="animate"></param>
        /// <returns></returns>
        public async Task PopPageModel(object data, bool modal = false, bool animate = true)
        {
            if (_currentPageModel.PreviousPageModel != null && data != null)
                _currentPageModel.PreviousPageModel.ReverseInit(data);

            await PopPageModel(modal, animate);
        }
    }
}
