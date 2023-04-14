using Maok.App.Modules.Home.Enums;
using Maok.App.Modules.Home.Models;
using Maok.App.Modules.Home.Services;
using Maok.App.Modules.ResetPassword.PageModels;
using Maok.App.Modules.Shared.PageModels;
using Maok.App.Utils;
using Refit;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using System;
using Xamarin.Forms;
using Maok.App.Modules.Home.PageModels.Parameter;

namespace Maok.App.Modules.Home.PageModels
{
    public class HomePageModel : BasePageModel<HomeModel>
    {
        public ICommand GoToFilterCommand => new CommandWaitCustom(DoFilterCommandAsync, this);
        public ICommand SendFilterCommand => new CommandWaitCustom(DoSendFilterCommandAsync, this);
        public ICommand GoToEventInfoCommand => new CommandWaitCustom<ContentModel>(DoEventInfoAsync, this);
        public ICommand GoToMenuCommand => new CommandWaitCustom<HomeType>(DoMenuAsync, this);
        public ICommand GoToResetPassword => new CommandWaitCustom(DoResetPasswordAsync, this);
        public ICommand GoToChangeProfile => new CommandWaitCustom(DoChangeProfileAsync, this);
        public ICommand SendVoucherAsync => new CommandWaitCustom(DoSendVoucherAsync, this);
        public ICommand GoToDeleteProfile => new CommandWaitCustom(DoDeleteProfileAsync, this);
        public ICommand GoToSendDeleteProfile => new CommandWaitCustom<bool>(DoSendDeleteProfileAsync, this);
        public ICommand OkCommand => new CommandWaitCustom(DoOKAsync, this);

        public List<FilterTypeModel> filters;

        public HomePageModel()
        {
        }

        public override void Init(object initData)
        {
            base.Init(initData);

            InitDelayedAsync(LoadEventsAsync);
        }

        public override void ReverseInit(object returnedData)
        {
            base.ReverseInit(returnedData);
            if (returnedData is bool isEventInfo)
            {
                if (isEventInfo)
                    InitDelayedAsync(LoadEventsAsync);
            }
        }

        private async Task LoadEventsAsync()
        {
            try
            {
                var apiResponse = RestService.For<IHomeService>(Util.GetClient());
                var eventResponseDto = await apiResponse.GetEvents();

                Model.Event = new EventModel(eventResponseDto);
                Model.Items = new ObservableCollection<ContentModel>();
                Model.Items = Model.Event.Content.ToObservableCollection();
                Model.ItemsOld = new List<ContentModel>();
                Model.ItemsOld = Model.Event.Content;

                Model.ItemHighlight = Model.Items.Where(x => x.Highlight == 1).First();
            }
            catch (Exception ex)
            {
                await ShowAlertAsync("Erro ao buscar eventos!", null);
            }
        }

        private async Task DoFilterCommandAsync(TaskCompletionSource<bool> tsc)
        {
            var stack = new StackLayout
            {
                Spacing = 15,
                Padding = new Thickness(32),
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
            };

            var stackImages = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
            };

            var stackPadlockOpen = new StackLayout();
            var stackPadlockClosed = new StackLayout();

            var labelEventTypeText = new Label
            {
                FontSize = 18,
                Text = "Tipos De Eventos:",
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment = TextAlignment.Start,
            };

            var framePadlockOpen = new Frame
            {
                VerticalOptions = LayoutOptions.End,
                HorizontalOptions = LayoutOptions.Start,
                HasShadow = false,
                BorderColor = Color.Gray,
                Padding = new Thickness(10),
                CornerRadius = 35,
                WidthRequest = 80,
                HeightRequest = 50,
                Content = new Image
                {
                    Source = "padlock_open.png",
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                    WidthRequest = 35,
                    HeightRequest = 35,
                },
            };

            var labelPadlockOpen = new Label
            {
                FontSize = 18,
                Text = "Público",
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment = TextAlignment.Center
            };

            var framePadlockClosed = new Frame
            {
                VerticalOptions = LayoutOptions.End,
                HorizontalOptions = LayoutOptions.Start,
                HasShadow = false,
                BorderColor = Color.Gray,
                Padding = new Thickness(10),
                CornerRadius = 35,
                WidthRequest = 80,
                HeightRequest = 50,
                Content = new Image
                {
                    Source = "padlock_closed.png",
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                    WidthRequest = 35,
                    HeightRequest = 35,
                },
            };

            var labelPadlockClosed = new Label
            {
                FontSize = 18,
                Text = "Privado",
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment = TextAlignment.Center,
            };

            var button = new Button
            {
                BackgroundColor = Color.Transparent,
                BorderColor = Color.FromHex("5D25C8"),
                BorderWidth = 1,
                Text = "APLICAR",
                TextColor = Color.FromHex("5D25C8"),
                WidthRequest = 250,
                HeightRequest = 50,
                CornerRadius = 20,
                Command = SendFilterCommand
            };

            stackPadlockOpen.Children.Add(framePadlockOpen);
            stackPadlockOpen.Children.Add(labelPadlockOpen);

            stackPadlockClosed.Children.Add(framePadlockClosed);
            stackPadlockClosed.Children.Add(labelPadlockClosed);

            stackImages.Children.Add(stackPadlockOpen);
            stackImages.Children.Add(stackPadlockClosed);

            stack.Children.Add(labelEventTypeText);
            stack.Children.Add(stackImages);
            stack.Children.Add(button);

            framePadlockOpen.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(async () =>
                {
                    if (filters != null)
                    {
                        var filter = filters.FirstOrDefault(x => x.EventType == EventType.Public);
                        if (filter == null)
                        {
                            framePadlockOpen.BackgroundColor = Color.FromHex("5D25C8");

                            filter = new FilterTypeModel()
                            {
                                EventType = EventType.Public,
                                EventSelected = true
                            };

                            filters.Add(filter);
                        }
                        else
                        {
                            if (filter.EventSelected.GetValueOrDefault())
                            {
                                filters.Remove(filters.Single(x => x.EventType == EventType.Public));
                                framePadlockOpen.BackgroundColor = Color.White;
                            }
                            else
                            {
                                filters.Single(x => x.EventType == EventType.Public).EventSelected = true;
                                framePadlockOpen.BackgroundColor = Color.FromHex("5D25C8");
                            }
                        }
                    }
                    else
                    {
                        framePadlockOpen.BackgroundColor = Color.FromHex("5D25C8");

                        var filter = new FilterTypeModel()
                        {
                            EventType = EventType.Public,
                            EventSelected = true
                        };

                        filters = new List<FilterTypeModel>();
                        filters.Add(filter);
                    }
                })
            });

            framePadlockClosed.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(async () =>
                {
                    if (filters != null)
                    {
                        var filter = filters.FirstOrDefault(x => x.EventType == EventType.Private);
                        if (filter == null)
                        {
                            framePadlockClosed.BackgroundColor = Color.FromHex("5D25C8");

                            filter = new FilterTypeModel()
                            {
                                EventType = EventType.Private,
                                EventSelected = true
                            };

                            filters.Add(filter);
                        }
                        else
                        {
                            if (filter.EventSelected.GetValueOrDefault())
                            {
                                filters.Remove(filters.Single(x => x.EventType == EventType.Private));
                                framePadlockClosed.BackgroundColor = Color.White;
                            }
                            else
                            {
                                filters.Single(x => x.EventType == EventType.Private).EventSelected = true;
                                framePadlockClosed.BackgroundColor = Color.FromHex("5D25C8");
                            }
                        }
                    }
                    else
                    {
                        framePadlockClosed.BackgroundColor = Color.FromHex("5D25C8");

                        var filter = new FilterTypeModel()
                        {
                            EventType = EventType.Public,
                            EventSelected = true
                        };

                        filters = new List<FilterTypeModel>();
                        filters.Add(filter);
                    }
                })
            });

            var scroll = new ScrollView
            {
                Content = stack,
                BackgroundColor = Color.White,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
            };

            await MyBasePage.ShowBottomSheetAsync(scroll, 600);
            tsc?.TrySetResult(true);
        }

        private async Task DoSendFilterCommandAsync(TaskCompletionSource<bool> tsc)
        {
            try
            {
                await MyBasePage?.BottomSheetHideAsync();
                IsBusy = true;
                //if (filters != null && filters.Any())
                //{
                //    if (filters.Select(x => x.EventType == EventType.Public).First())
                //        Model.Items = Model.ItemsOld.Where(x => x.IsPublic).ToObservableCollection();
                //    else
                //        Model.Items = Model.ItemsOld.Where(x => !x.IsPublic).ToObservableCollection();
                //}
                //else
                //    Model.Items = Model.ItemsOld.ToObservableCollection();
            }
            catch (Exception ex)
            {
                await ShowAlertAsync("Erro ao realizar filtragem!", null);
            }
            finally
            {
                IsBusy = false;
                tsc?.TrySetResult(true);
            }

        }

        private async Task DoMenuAsync(HomeType type, TaskCompletionSource<bool> tsc)
        {
            switch (type)
            {
                case HomeType.Home:
                    IsBusy = true;
                    Model.IsHome = true;
                    Model.IsTicket = false;
                    Model.IsCalendar = false;
                    Model.IsProfile = false;
                    Model.IsDelete = false;
                    await Task.Delay(400);
                    IsBusy = false;
                    tsc?.TrySetResult(true);
                    break;

                case HomeType.Ticket:
                    IsBusy = true;
                    Model.IsHome = false;
                    Model.IsTicket = true;
                    Model.IsCalendar = false;
                    Model.IsProfile = false;
                    Model.IsDelete = false;
                    await Task.Delay(400);
                    IsBusy = false;
                    tsc?.TrySetResult(true);
                    break;

                case HomeType.Calendar:
                    IsBusy = true;
                    Model.IsHome = false;
                    Model.IsTicket = false;
                    Model.IsCalendar = true;
                    Model.IsProfile = false;
                    Model.IsDelete = false;
                    await Task.Delay(400);
                    IsBusy = false;
                    tsc?.TrySetResult(true);
                    break;

                case HomeType.Profile:
                    IsBusy = true;
                    Model.IsHome = false;
                    Model.IsTicket = false;
                    Model.IsCalendar = false;
                    Model.IsProfile = true;
                    await Task.Delay(400);
                    IsBusy = false;
                    tsc?.TrySetResult(true);
                    break;
            }
        }

        private async Task DoSendVoucherAsync(TaskCompletionSource<bool> tsc)
        {
            try
            {
                IsBusy = true;
                if (!Model.IsValid)
                {
                    await ShowAlertAsync(Model.Erros.ToString(), tsc);
                    return;
                }

                var apiResponse = RestService.For<IHomeService>(Util.GetClient());
                await apiResponse.SendVoucher(Model.VoucherId);
                Model.PopUp = true;
                Model.Opacity = 0.2M;
            }
            catch (Exception ex)
            {
                await ShowAlertAsync("Voucher inválido ou já utilizado", null);
            }
            finally
            {
                IsBusy = false;
                tsc?.TrySetResult(true);
            }
        }

        private async Task DoOKAsync(TaskCompletionSource<bool> tsc)
        {
            IsBusy = true;
            Model.VoucherId = "";
            Model.Opacity = 1;
            Model.PopUp = false;
            IsBusy = false;
            tsc?.TrySetResult(true);
        }

        private Task DoEventInfoAsync(ContentModel model, TaskCompletionSource<bool> tsc) => Push<EventInfoPageModel>(new EventInfoParameter(model.Id), tsc: tsc);

        private Task DoResetPasswordAsync(TaskCompletionSource<bool> tsc) => Push<ResetPasswordPageModel>(tsc: tsc);

        private Task DoChangeProfileAsync(TaskCompletionSource<bool> tsc) => Push<ProfilePageModel>(tsc: tsc);

        private async Task DoDeleteProfileAsync(TaskCompletionSource<bool> tsc)
        {
            IsBusy = true;
            Model.IsDelete = true;
            IsBusy = false;
            //await Push<EventInfoPageModel>(new EventInfoParameter(model.Id)
            tsc?.TrySetResult(true);
        }

        private async Task DoSendDeleteProfileAsync(bool action, TaskCompletionSource<bool> tsc)
        {
            try
            {
                IsBusy = true;
                if (action)
                {
                    //var apiResponse = RestService.For<IHomeService>(Util.GetClient());
                    //var eventResponseDto = await apiResponse.GetEvents();
                }
            }
            catch (Exception ex)
            {
                await ShowAlertAsync("Erro ao validar Voucher!", null);
            }
            finally
            {
                Model.IsDelete = false;
                IsBusy = false;
                tsc?.TrySetResult(true);
            }
        }
    }
}