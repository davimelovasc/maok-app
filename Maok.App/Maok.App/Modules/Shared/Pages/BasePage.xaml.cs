using FreshMvvm;
using Maok.App.Modules.Shared.Enums;
using Maok.App.Utils;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.Xaml;
using static Xamarin.Essentials.MainThread;
using Page = Xamarin.Forms.PlatformConfiguration.iOSSpecific.Page;

namespace Maok.App.Modules.Shared.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BasePage : FreshBaseContentPage
    {
        private CancellationTokenSource _alertCancellationToken;
        private bool _alertBusy;
        public ScrollView Scroll { get; set; }
        public ContentView InjectBody => InjectBodyInternal;

        private List<AlertOption> _alertOptions;
        public AlertType AlertType { get; set; }

        public List<AlertOption> AlertOptions
        {
            get => _alertOptions;
            set
            {
                _alertOptions = value;
                OnPropertyChanged(nameof(AlertOptions));
            }
        }

        public BasePage()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty HasHeaderProperty = BindableProperty.Create(nameof(HasHeader), typeof(bool), typeof(BasePage), true);
        public static readonly BindableProperty HeaderColorProperty = BindableProperty.Create(nameof(HeaderColor), typeof(Color), typeof(BasePage));
        public static readonly BindableProperty HasHelpProperty = BindableProperty.Create(nameof(HasHelp), typeof(bool), typeof(BasePage), false);
        public static readonly BindableProperty HasFilterProperty = BindableProperty.Create(nameof(HasFilter), typeof(bool), typeof(BasePage), false);
        public static readonly BindableProperty HasInformationProperty = BindableProperty.Create(nameof(HasInformation), typeof(bool), typeof(BasePage), false);
        public static readonly BindableProperty IconsWhiteProperty = BindableProperty.Create(nameof(IconsWhite), typeof(bool), typeof(BasePage), false);
        public static readonly BindableProperty HasBackButtonProperty = BindableProperty.Create(nameof(HasBackButton), typeof(bool), typeof(BasePage), true);
        public static readonly BindableProperty HasWishListProperty = BindableProperty.Create(nameof(HasWishList), typeof(bool), typeof(BasePage), false);
        public static readonly BindableProperty IsWishListProperty = BindableProperty.Create(nameof(IsWishList), typeof(bool), typeof(BasePage), false);
        public static readonly BindableProperty BodyProperty = BindableProperty.Create(nameof(Body), typeof(View), typeof(BasePage));
        public static readonly BindableProperty BodyScrolledProperty = BindableProperty.Create(nameof(BodyScrolled), typeof(View), typeof(BasePage));
        public static readonly BindableProperty UseSafeAreaTopProperty = BindableProperty.Create(nameof(UseSafeAreaTop), typeof(bool), typeof(BasePage), true);
        public static readonly BindableProperty UseSafeAreaBottomProperty = BindableProperty.Create(nameof(UseSafeAreaBottom), typeof(bool), typeof(BasePage), true);
        public static readonly BindableProperty SafeAreaProperty = BindableProperty.Create(nameof(SafeArea), typeof(Thickness), typeof(BasePage), new Thickness(0));
        public static readonly BindableProperty BottomSheetBorderColorProperty = BindableProperty.Create(nameof(BottomSheetBorderColor), typeof(Color), typeof(BasePage), Color.Default);
        public static readonly BindableProperty BottomSheetShadowColorProperty = BindableProperty.Create(nameof(BottomSheetShadowColor), typeof(Color), typeof(BasePage), Color.Default);
        public static readonly BindableProperty BottomSheetColorProperty = BindableProperty.Create(nameof(BottomSheetColor), typeof(Color), typeof(BasePage), Color.Default);
        public static readonly BindableProperty BottomSheetCornerRadiusProperty = BindableProperty.Create(nameof(BottomSheetCornerRadius), typeof(CornerRadius), typeof(BasePage), new CornerRadius(0));
        public static readonly BindableProperty ThemeProperty = BindableProperty.Create(nameof(Theme), typeof(ThemeType), typeof(BasePage), ThemeType.Light);
        public static readonly BindableProperty BackParameterProperty = BindableProperty.Create(nameof(BackParameter), typeof(object), typeof(BasePage), defaultBindingMode: BindingMode.TwoWay);

        protected override void OnAppearing()
        {
            base.OnAppearing();
            OnPropertyChanged(nameof(Theme));
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        public bool HasHeader
        {
            get => (bool)GetValue(HasHeaderProperty);
            set => SetValue(HasHeaderProperty, value);
        }

        public Color HeaderColor
        {
            get => (Color)GetValue(HeaderColorProperty);
            set => SetValue(HeaderColorProperty, value);
        }

        public bool HasHelp
        {
            get => (bool)GetValue(HasHelpProperty);
            set => SetValue(HasHelpProperty, value);
        }

        public bool HasFilter
        {
            get => (bool)GetValue(HasFilterProperty);
            set => SetValue(HasFilterProperty, value);
        }

        public bool HasInformation
        {
            get => (bool)GetValue(HasInformationProperty);
            set => SetValue(HasInformationProperty, value);
        }

        public bool HasWishList
        {
            get => (bool)GetValue(HasWishListProperty);
            set => SetValue(HasWishListProperty, value);
        }

        public bool IsWishList
        {
            get => (bool)GetValue(IsWishListProperty);
            set => SetValue(IsWishListProperty, value);
        }

        public bool IconsWhite
        {
            get => (bool)GetValue(IconsWhiteProperty);
            set => SetValue(IconsWhiteProperty, value);
        }

        public bool HasBackButton
        {
            get => (bool)GetValue(HasBackButtonProperty);
            set => SetValue(HasBackButtonProperty, value);
        }

        public View Body
        {
            get => (View)GetValue(BodyProperty);
            set => SetValue(BodyProperty, value);
        }

        public Layout BodyScrolled
        {
            get => (Layout)GetValue(BodyScrolledProperty);
            set => SetValue(BodyScrolledProperty, value);
        }

        public bool UseSafeAreaTop
        {
            get => (bool)GetValue(UseSafeAreaTopProperty);
            set => SetValue(UseSafeAreaTopProperty, value);
        }

        public bool UseSafeAreaBottom
        {
            get => (bool)GetValue(UseSafeAreaBottomProperty);
            set => SetValue(UseSafeAreaBottomProperty, value);
        }

        public Thickness SafeArea
        {
            get => (Thickness)GetValue(SafeAreaProperty);
            set => SetValue(SafeAreaProperty, value);
        }

        public Color BottomSheetBorderColor
        {
            get => (Color)GetValue(BottomSheetBorderColorProperty);
            set => SetValue(BottomSheetBorderColorProperty, value);
        }

        public Color BottomSheetShadowColor
        {
            get => (Color)GetValue(BottomSheetShadowColorProperty);
            set => SetValue(BottomSheetShadowColorProperty, value);
        }

        public Color BottomSheetColor
        {
            get => (Color)GetValue(BottomSheetColorProperty);
            set => SetValue(BottomSheetColorProperty, value);
        }

        public CornerRadius BottomSheetCornerRadius
        {
            get => (CornerRadius)GetValue(BottomSheetCornerRadiusProperty);
            set => SetValue(BottomSheetCornerRadiusProperty, value);
        }

        public ThemeType Theme
        {
            get => (ThemeType)GetValue(ThemeProperty);
            set => SetValue(ThemeProperty, value);
        }

        public object BackParameter
        {
            get => GetValue(BackParameterProperty);
            set => SetValue(BackParameterProperty, value);
        }

        public Task ShowAlertAsync(string descrition, List<AlertOption> options, AlertType type)
        {
            if (_alertBusy)
                return Task.Delay(1000);

            AlertType = type;
            _alertBusy = true;
            AlertOptions = options ?? new List<AlertOption>();
            //AlertDescrition.Text = descrition;
            var upSwipeGesture = new SwipeGestureRecognizer { Direction = SwipeDirection.Up };
            upSwipeGesture.Swiped += OnSwiped;
            AlertContainer.GestureRecognizers.Add(upSwipeGesture);
            AlertContainer.TranslationY = -(FrameHeader.HeightRequest + SafeArea.Top);
            AlertContainer.IsVisible = true;
            _alertCancellationToken?.Cancel(false);
            _alertCancellationToken = new CancellationTokenSource();
            var opacity = AlertContainer.FadeTo(1);
            var translate = AlertContainer.TranslateTo(0, 10 + SafeArea.Top);
            return Task.WhenAll(opacity, translate).ContinueWith(x =>
            {
                if (x.IsCompleted)
                {
                    Task.Delay(2000, _alertCancellationToken.Token).ContinueWith(c =>
                    {
                        if (c.IsCompleted && !c.IsCanceled)
                            HideAlertAsync();
                    });

                    _alertBusy = false;
                }
            });
        }

        private void OnSwiped(object sender, SwipedEventArgs e)
        {
            HideAlertAsync();
        }

        public Task ShowBottomSheetAsync(View view, double heightContainer, bool isClosed = true, bool isScrolled = true)
        {
            var heightContainerBottom = heightContainer + SafeArea.Bottom;

            if (heightContainerBottom >= DeviceDisplay.MainDisplayInfo.Height)
                heightContainerBottom = DeviceDisplay.MainDisplayInfo.Height - 100;

            ContainerPipe.IsVisible = isClosed;

            if (isClosed)
            {
                var tap = new TapGestureRecognizer();
                tap.Tapped += CloseBottomSheet;
                BottomSheetBackground.GestureRecognizers.Clear();
                BottomSheetBackground.GestureRecognizers.Add(tap);
            }

            BottomSheetContainer.Margin = BottomSheetBackground.Margin = new Thickness(-SafeArea.HorizontalThickness, -SafeArea.VerticalThickness);
            BottomSheetBackground.IsVisible = true;
            BottomSheetContainer.HeightRequest = heightContainerBottom;
            BottomSheetContainer.TranslationY = heightContainerBottom;

            BottomSheetContent.Content = isScrolled ? new ScrollView { Content = view, Margin = new Thickness(-20, 0) } : view;
            BottomSheetContainer.IsVisible = true;

            return Task.WhenAll(
                BottomSheetBackground.FadeTo(1),
                BottomSheetContainer.TranslateTo(0, -SafeArea.Bottom),
                BottomSheetContainer.FadeTo(1));
        }

        public Task ResizeBottomSheetAsync(double heightContainer)
        {
            var heightContainerBottom = heightContainer + SafeArea.Bottom;

            if (heightContainerBottom >= DeviceDisplay.MainDisplayInfo.Height)
                heightContainerBottom = DeviceDisplay.MainDisplayInfo.Height - 100;

            BottomSheetContainer.HeightRequest = heightContainerBottom;

            return Task.WhenAll(BottomSheetContainer.TranslateTo(0, -SafeArea.Bottom));
        }

        public Task HideAlertAsync()
        {
            var translate = AlertContainer.TranslateTo(0, -(FrameHeader.HeightRequest + SafeArea.Top));
            var opacity = AlertContainer.FadeTo(0);
            return Task.WhenAll(translate, opacity).ContinueWith(x =>
            {
                if (x.IsCompleted)
                    BeginInvokeOnMainThread(() => AlertContainer.IsVisible = false);
            });
        }

        private async void AlertOnTapped(object sender, EventArgs e)
        {
            if (_alertBusy)
                return;
            _alertBusy = true;

            _alertCancellationToken.Cancel(false);
            await HideAlertAsync();
            _alertBusy = false;
        }

        private async void BottomSheetOnPan(object sender, PanUpdatedEventArgs e)
        {
            var bottom = -SafeArea.Bottom;
            if (e.StatusType == GestureStatus.Running)
            {
                if (BottomSheetContainer.TranslationY >= -SafeArea.Bottom && e.TotalY > bottom)
                    await BottomSheetContainer.TranslateTo(BottomSheetContainer.X, e.TotalY, 20);
            }
            else if (e.StatusType == GestureStatus.Completed)
            {
                if (BottomSheetContainer.TranslationY >= BottomSheetContainer.HeightRequest / 3)
                    await BottomSheetHideAsync();
                else
                    await BottomSheetContainer.TranslateTo(0, bottom);
            }
        }

        public Task BottomSheetHideAsync()
        {
            return Task.WhenAll(
                BottomSheetBackground.FadeTo(0, 550, Easing.CubicIn),
                BottomSheetContainer.TranslateTo(0, BottomSheetContainer.HeightRequest + SafeArea.Bottom)).ContinueWith(
                x =>
                {
                    if (x.IsCompleted)
                    {
                        Device.BeginInvokeOnMainThread(() => BottomSheetBackground.IsVisible = BottomSheetContainer.IsVisible = false);
                    }
                });
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (nameof(Body).Equals(propertyName))
                body.Content = Body;

            if (nameof(BodyScrolled).Equals(propertyName))
            {
                if (BodyScrolled is ScrollView contentScrolled)
                    Scroll = contentScrolled;
                else
                    Scroll = new ScrollView { Content = BodyScrolled };

                body.Content = Scroll;
            }

            if (propertyName == "SafeAreaInsets")
            {
                SafeArea = Page.SafeAreaInsets(On<iOS>());
                OnPropertyChanged(nameof(UseSafeAreaTop));
                OnPropertyChanged(nameof(UseSafeAreaBottom));
            }

            if (nameof(UseSafeAreaTop).Equals(propertyName))
            {
                if (!HasHeader && UseSafeAreaTop)
                {
                    HasBackButton = false;
                    FrameHeader.IsVisible = true;
                    FrameHeader.Opacity = 1;
                    FrameHeader.HeightRequest = SafeArea.Top;
                }
                else
                {
                    if (Device.RuntimePlatform == Device.iOS)
                        FrameHeader.Padding = UseSafeAreaTop ? new Thickness(0, ((SafeArea.Top / 2) + 5), 0, 0) : FrameHeader.Padding;
                }

                AlertContainer.TranslationY -= SafeArea.Top;
            }

            if (nameof(UseSafeAreaBottom).Equals(propertyName))
            {
                Main.Padding = UseSafeAreaBottom ? new Thickness(0, 0, 0, SafeArea.Bottom) : 0;
            }

            if (nameof(HeaderColor).Equals(propertyName))
            {
                FrameHeader.BackgroundColor = HeaderColor;
            }
        }

        private async void CloseBottomSheet(object sender, EventArgs e)
        {
            await BottomSheetHideAsync();
        }

        //        private void CreateWaterMark()
        //        {
        //            string version = VersionTracking.CurrentVersion;

        //            var label = new Label()
        //            {
        //#if HOMOLPROD || STAGING
        //                Text = $"{SharedConstant.EnvironmentStaging} {version}",
        //#elif HOMOLDEV
        //                Text = $"{SharedConstant.EnvironmentHomol} {version}",
        //#elif DEBUG
        //                Text = $"{SharedConstant.EnvironmentDevelopment} {version}",
        //#endif
        //                FontSize = 12,
        //                HorizontalTextAlignment = TextAlignment.Center,
        //                HorizontalOptions = LayoutOptions.Center,
        //                TextColor = Color.Black,
        //                Padding = 6,
        //                InputTransparent = true,
        //                WidthRequest = 150
        //            };

        //            var frame = new Frame
        //            {
        //                Opacity = .15,
        //                Content = label,
        //                CornerRadius = 5,
        //                Padding = 0,
        //                HasShadow = false,
        //                IsClippedToBounds = true,
        //                BackgroundColor = Color.LightBlue,
        //                VerticalOptions = LayoutOptions.End,
        //                HorizontalOptions = LayoutOptions.End,
        //                Margin = new Thickness(3, 3, 3, label.WidthRequest),
        //                Rotation = -90,
        //                TranslationX = label.WidthRequest / 2 - 10
        //            };

        //            frame.GestureRecognizers.Add(new TapGestureRecognizer()
        //            {
        //                NumberOfTapsRequired = 2,
        //                Command = new Command(async obj =>
        //                {
        //                    await Clipboard.SetTextAsync(CrossFirebasePushNotification.Current.Token);
        //                    MessagingCenter.Send(new ToastEvent("Token do Firebase copiada."), ToastEvent.EVENT);
        //                })
        //            }); ;

        //            Main.Children.Add(frame);
        //            Main.RaiseChild(frame);
        //        }
    }
}
