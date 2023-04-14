using FreshMvvm;
using Maok.App.Modules.Shared.Pages;
using Plugin.SharedTransitions;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Maok.App.Modules.Shared.Navigation
{
    public class NavigationController : SharedTransitionNavigationPage, IFreshNavigationService
    {
        public string NavigationServiceName { get; }

        public NavigationController(Page page)
            : this(page, Constants.DefaultNavigationServiceName)
        {
        }

        public NavigationController(Page page, string navigationPageName)
            : base(page)
        {
            var pageModel = page.GetModel();
            if (pageModel == null)
                throw new InvalidCastException("Pagina sem BindingContext");

            pageModel.CurrentNavigationServiceName = navigationPageName;
            NavigationServiceName = navigationPageName;
            RegisterNavigation();
        }

        protected void RegisterNavigation()
        {
            FreshIOC.Container.Register<IFreshNavigationService>(this, NavigationServiceName);
        }

        internal Page CreateContainerPageSafe(Page page)
        {
            if (page is NavigationPage || page is MasterDetailPage || page is TabbedPage)
                return page;

            return CreateContainerPage(page);
        }

        protected Page CreateContainerPage(Page page)
        {
            try
            {
                return new SharedTransitionNavigationPage(page);
            }
            catch (Exception e)
            {
                //AsyncErrorHandler.HandleException(e);
                return new SharedTransitionNavigationPage(new ErrorPage());
            }
        }

        public Task PushPage(Page page, FreshBasePageModel model, bool modal = false, bool animate = true)
        {
            if (modal)
                return Navigation.PushModalAsync(CreateContainerPageSafe(page), animate);
            return Navigation.PushAsync(page, animate);
        }

        public Task PopPage(bool modal = false, bool animate = true)
        {
            if (modal)
                return Navigation.PopModalAsync(animate);
            return Navigation.PopAsync(animate);
        }

        public Task PopToRoot(bool animate = true)
        {
            return Navigation.PopToRootAsync(animate);
        }

        public async void NotifyChildrenPageWasPopped()
        {
            await Task.Delay(500);
            this.NotifyAllChildrenPopped();
        }

        public Task<FreshBasePageModel> SwitchSelectedRootPageModel<T>() where T : FreshBasePageModel
        {
            return null;
        }
    }
}