using Maok.App.Modules.Shared.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Maok.App.Modules.Register.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterBirthDatePage : BasePage
    {
        public RegisterBirthDatePage()
        {
            InitializeComponent();
        }

        private void OnDateSelected(object sender, DateChangedEventArgs e)
        {
            var teste = e.NewDate.Day + "/" + e.NewDate.Month + "/" + e.NewDate.Year;
        }
    }
}