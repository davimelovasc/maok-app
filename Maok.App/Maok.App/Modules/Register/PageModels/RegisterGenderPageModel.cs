using Maok.App.Modules.Register.Models;
using Maok.App.Modules.Register.PageModels.Parameter;
using Maok.App.Modules.Shared.Models;
using Maok.App.Modules.Shared.PageModels;
using Maok.App.Utils;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Maok.App.Modules.Register.PageModels
{
    public class RegisterGenderPageModel : BasePageModel<RegisterGenderModel>
    {
        public ICommand GoToBirthDateCommand => new CommandWaitCustom(DoBirthDateAsync, this);

        private ComboBoxModel _genderSelected;
        public ComboBoxModel GenderSelected
        {
            get
            {
                return _genderSelected;
            }
            set
            {
                SetProperty(ref _genderSelected, value);
            }
        }

        public RegisterGenderPageModel()
        {
        }

        public override void Init(object initData)
        {
            base.Init(initData);
            if (initData is RegisterUserParameter parameter)
                Model.User = parameter.User;
        }

        private async Task DoBirthDateAsync(TaskCompletionSource<bool> tsc)
        {
            Model.GenderSelected = GenderSelected;
            if (!Model.IsValid)
            {
                await ShowAlertAsync(Model.Erros.ToString(), tsc);
                return;
            }

            if (Model.GenderSelected.Value == "Masculino")
                Model.User.Gender = "M";
            else
                Model.User.Gender = "F";

            await Push<RegisterBirthDatePageModel>(new RegisterUserParameter(Model.User), tsc: tsc);
        }
    }
}