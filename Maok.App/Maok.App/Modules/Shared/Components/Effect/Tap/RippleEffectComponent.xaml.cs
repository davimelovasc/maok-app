using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinBackgroundKit.Controls;

namespace Maok.App.Modules.Shared.Components.Effect.Tap
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RippleEffectComponent : MaterialContentView
    {
        public RippleEffectComponent()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty ColorProperty = BindableProperty.Create(nameof(Color), typeof(Color), typeof(RippleEffectComponent), Color.Default);
        public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(RippleEffectComponent), Color.Default);
        public static readonly BindableProperty ShadowColorProperty = BindableProperty.Create(nameof(ShadowColor), typeof(Color), typeof(RippleEffectComponent), Color.Default);
        public static readonly BindableProperty RippleColorProperty = BindableProperty.Create(nameof(RippleColor), typeof(Color), typeof(RippleEffectComponent), Color.Default);
        public static readonly BindableProperty BorderWidthProperty = BindableProperty.Create(nameof(BorderWidth), typeof(double), typeof(RippleEffectComponent), 0D);
        public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(nameof(CornerRadius), typeof(CornerRadius), typeof(RippleEffectComponent), new CornerRadius(0));
        public static readonly BindableProperty ElevationProperty = BindableProperty.Create(nameof(Elevation), typeof(double), typeof(RippleEffectComponent), 0D);
        public static readonly BindableProperty IsRippleEnabledProperty = BindableProperty.Create(nameof(IsRippleEnabled), typeof(bool), typeof(RippleEffectComponent), true);
        public static readonly BindableProperty IsBusyProperty = BindableProperty.Create(nameof(IsBusy), typeof(bool), typeof(RippleEffectComponent), false);

        public Color Color
        {
            get => (Color)GetValue(ColorProperty);
            set => SetValue(ColorProperty, value);
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

        public CornerRadius CornerRadius
        {
            get => (CornerRadius)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        public double Elevation
        {
            get => (double)GetValue(ElevationProperty);
            set => SetValue(ElevationProperty, value);
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
    }
}