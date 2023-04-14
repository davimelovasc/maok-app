using Maok.App.Modules.Shared.Enums;
using Maok.App.Utils;
using System;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Maok.App.Modules.Shared.Components.Button
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ButtonComponent : ContentView
    {
        public event EventHandler<EventArgs> Tapped;

        public ButtonComponent()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(ButtonComponent));
        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(ButtonComponent));
        public static readonly BindableProperty TextVerticalOptionsProperty = BindableProperty.Create(nameof(TextVerticalOptions), typeof(LayoutOptions), typeof(ButtonComponent));
        public static readonly BindableProperty TextHorizontalOptionsProperty = BindableProperty.Create(nameof(TextHorizontalOptions), typeof(LayoutOptions), typeof(ButtonComponent));
        public static readonly BindableProperty TextMarginProperty = BindableProperty.Create(nameof(TextMargin), typeof(Thickness), typeof(ButtonComponent));
        public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(double), typeof(ButtonComponent));
        public static readonly BindableProperty FontFamilyProperty = BindableProperty.Create(nameof(FontFamily), typeof(string), typeof(ButtonComponent));
        public static readonly BindableProperty HorizontalTextAlignmentProperty = BindableProperty.Create(nameof(HorizontalTextAlignment), typeof(TextAlignment), typeof(ButtonComponent));
        public static readonly BindableProperty FontAttributesProperty = BindableProperty.Create(nameof(FontAttributes), typeof(FontAttributes), typeof(ButtonComponent));

        public new static readonly BindableProperty BackgroundColorProperty = BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(ButtonComponent));
        public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(ButtonComponent));
        public static readonly BindableProperty ShadowColorProperty = BindableProperty.Create(nameof(ShadowColor), typeof(Color), typeof(ButtonComponent));
        public static readonly BindableProperty RippleColorProperty = BindableProperty.Create(nameof(RippleColor), typeof(Color), typeof(ButtonComponent));

        public static readonly BindableProperty BorderWidthProperty = BindableProperty.Create(nameof(BorderWidth), typeof(double), typeof(ButtonComponent));
        public new static readonly BindableProperty PaddingProperty = BindableProperty.Create(nameof(Padding), typeof(Thickness), typeof(ButtonComponent));
        public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(nameof(CornerRadius), typeof(CornerRadius), typeof(ButtonComponent));
        public static readonly BindableProperty ButtonTypeProperty = BindableProperty.Create(nameof(ButtonType), typeof(ButtonType), typeof(ButtonComponent));

        public static readonly BindableProperty IsRippleEnabledProperty = BindableProperty.Create(nameof(IsRippleEnabled), typeof(bool), typeof(ButtonComponent));
        public static readonly BindableProperty IsBusyProperty = BindableProperty.Create(nameof(IsBusy), typeof(bool), typeof(ButtonComponent));

        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(ButtonComponent));
        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(ButtonComponent));

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public Color TextColor
        {
            get => (Color)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }

        public LayoutOptions TextVerticalOptions
        {
            get => (LayoutOptions)GetValue(TextVerticalOptionsProperty);
            set => SetValue(TextVerticalOptionsProperty, value);
        }

        public LayoutOptions TextHorizontalOptions
        {
            get => (LayoutOptions)GetValue(TextHorizontalOptionsProperty);
            set => SetValue(TextHorizontalOptionsProperty, value);
        }

        public TextAlignment HorizontalTextAlignment
        {
            get => (TextAlignment)GetValue(HorizontalTextAlignmentProperty);
            set => SetValue(HorizontalTextAlignmentProperty, value);
        }

        public Thickness TextMargin
        {
            get => (Thickness)GetValue(TextMarginProperty);
            set => SetValue(TextMarginProperty, value);
        }

        public double FontSize
        {
            get => (double)GetValue(FontSizeProperty);
            set => SetValue(FontSizeProperty, value);
        }

        public string FontFamily
        {
            get => (string)GetValue(FontFamilyProperty);
            set => SetValue(FontFamilyProperty, value);
        }

        public FontAttributes FontAttributes
        {
            get => (FontAttributes)GetValue(FontAttributesProperty);
            set => SetValue(FontAttributesProperty, value);
        }

        public new Color BackgroundColor
        {
            get => (Color)GetValue(BackgroundColorProperty);
            set => SetValue(BackgroundColorProperty, value);
        }

        public Color BorderColor
        {
            get => (Color)GetValue(BorderColorProperty);
            set => SetValue(BorderColorProperty, value);
        }

        public Color ShadowColor
        {
            get => (Color)GetValue(ShadowColorProperty);
            set => SetValue(ShadowColorProperty, value);
        }

        public Color RippleColor
        {
            get => (Color)GetValue(RippleColorProperty);
            set => SetValue(RippleColorProperty, value);
        }

        public double BorderWidth
        {
            get => (double)GetValue(BorderWidthProperty);
            set => SetValue(BorderWidthProperty, value);
        }

        public new Thickness Padding
        {
            get => (Thickness)GetValue(PaddingProperty);
            set => SetValue(PaddingProperty, value);
        }

        public CornerRadius CornerRadius
        {
            get => (CornerRadius)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        public ButtonType ButtonType
        {
            get => (ButtonType)GetValue(ButtonTypeProperty);
            set => SetValue(PaddingProperty, value);
        }

        public bool IsRippleEnabled
        {
            get => (bool)GetValue(IsRippleEnabledProperty);
            set => SetValue(IsRippleEnabledProperty, value);
        }

        public bool IsBusy
        {
            get => (bool)GetValue(IsBusyProperty);
            set => SetValue(IsBusyProperty, value);
        }

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        private void OnClicked(object sender, EventArgs e)
        {
            Tapped?.Invoke(this, e);

            if (Command?.CanExecute(CommandParameter) ?? false)
                Command.Execute(CommandParameter);
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            //if (nameof(ButtonType).Equals(propertyName))
            //{
            //    Style = ButtonType switch
            //    {
            //        ButtonType.Flat => Util.GetResource<Style>("ButtonFlat"),
            //        ButtonType.Primary => Util.GetResource<Style>("ButtonPrimary"),
            //        ButtonType.Outline => Util.GetResource<Style>("ButtonOutlineWithoutBorder"),
            //        _ => Style
            //    };
            //}

            if (nameof(IsEnabled).Equals(propertyName))
            {
                Style = IsEnabled
                    ? Util.GetResource<Style>("ButtonPrimary")
                    : Util.GetResource<Style>("ButtonDisabled");
            }
        }
    }
}